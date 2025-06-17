<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EditAttendanceByMonth.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EditAttendanceByMonth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <AEC:Topmenu ID="topmenu1" runat="server" />
    <script language="javascript" type="text/javascript">

        function CheckLeaveCombination(Status, EmpID, txtIn, ddlWS, UserID, Row, txtid, dt, txtOut, txtRemarks) {
            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;
            if (Status.value != 1 && Status.value != 2 && Status.value != 7)//&& Status.value != 9
            {
                var ResultVal = AjaxDAL.CheckAvailable(Status.value, EmpID);
                if (ResultVal.value.Result == 0)//ResultVal.erorr == null && 
                {
                    document.getElementById(txtid).value = "";
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed');
                    return false;
                }
                if (ResultVal.value.Result == 2) {
                    document.getElementById(txtid).value = "";
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed! Machineries/Materials were under his Care/Supervision ask him to transfer those Responsibilities!');
                    return false;
                }
                if (ResultVal.value.Result == 1) {
                    document.getElementById(Row).style.backgroundColor = "#ffffff";
                    var ResultStatus = AjaxDAL.HR_MarkAttandace(EmpID, Status.value, $get(ddlWS).value, UserID);
                    if (ResultStatus.value.Column1 == "0") {
                        alert("Not Updated");
                        return false;
                    }
                }
            }
            else {
                if (Status.value == "2") {
                    document.getElementById(txtIn).value = d.toLocaleTimeString();
                }
                else {
                    document.getElementById(txtIn).value = "";
                }
                var rtValFrd = AjaxDAL.UpdateFullAtt(EmpID, Status.value, dt, $get(txtIn).value, $get(txtOut).value, $get(txtRemarks).value, $get(ddlWS).value, UserID);
            }

        }

        function CheckLeaveCombination1(EmpID, txtIn, ddlWS, UserID, Row, txtid, dt, txtOut, txtRemarks,ddlStatus) {
            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;
            if ($get(ddlStatus).value != 1 && $get(ddlStatus).value != 2 && $get(ddlStatus).value != 7)//&& Status.value != 9
            {
                var ResultVal = AjaxDAL.CheckAvailable(Status.value, EmpID);
                if (ResultVal.value.Result == 0)//ResultVal.erorr == null && 
                {
                    document.getElementById(txtid).value = "";
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed');
                    return false;
                }
                if (ResultVal.value.Result == 2) {
                    document.getElementById(txtid).value = "";
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed! Machineries/Materials were under his Care/Supervision ask him to transfer those Responsibilities!');
                    return false;
                }
                if (ResultVal.value.Result == 1) {
                    document.getElementById(Row).style.backgroundColor = "#ffffff";
                    var ResultStatus = AjaxDAL.HR_MarkAttandace(EmpID, Status.value, $get(ddlWS).value, UserID);
                    if (ResultStatus.value.Column1 == "0") {
                        alert("Not Updated");
                        return false;
                    }
                }
            }
            else {
                if ($get(ddlStatus).value == "2") {

                    if (document.getElementById(0).checked) {
                        document.getElementById(tx0tIn).value = d.toLocaleTimeString();
                    }
                }
                else {
                    document.getElementById(txtIn).value = "";
                }
                
                var rtValFrd = AjaxDAL.UpdateFullAtt(EmpID, $get(ddlStatus).value, dt, $get(txtIn).value, $get(txtOut).value, $get(txtRemarks).value, $get(ddlWS).value, UserID);
            }

        }

        function SelectAllOut(CheckBox) {
            var Result = AjaxDAL.GetServerDate();
            //            var d = new Date();
            //            d = Result.value;
            TotalChkBx = parseInt('<%= this.gdvAttend.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gdvAttend.ClientID %>');
            var TargetChildControl = "chkOut";
            var TargetChildOutControl = "txtOUT";
            var TRs = TargetBaseControl.getElementsByTagName("TR");
            for (var iTR = 0; iTR < TRs.length; ++iTR) {

                var Inputs = TRs[iTR].getElementsByTagName("input");
                var Labels = TRs[iTR].getElementsByTagName("SPAN");
                if (Inputs.length > 2) {
                    if (Inputs[4].type == 'checkbox' && Inputs[5].type == 'text') {
                        Inputs[4].checked = CheckBox.checked;
                        var EmpId = Labels[0].innerText;

                    }
                }
            }
        }

        function valids() {
          
            //For EmpType
            if (document.getElementById('<%=ddlEmp.ClientID%>').selectedIndex == 0) {
                alert("Please Select Employee type");
                document.getElementById('<%=ddlEmp.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <table cellspacing="0" cellpadding="0" style="width: 100%; border: 0px;">
       
        <tr>
            <td>
                <asp:Label ID="lblDate" runat="server" CssClass="pageheader"></asp:Label>
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td>
                <table style="border-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid;
                    border-bottom: #d56511 1px solid;" border="0" cellpadding="3" cellspacing="3"
                    width="98%">
                    <tr>
                        <td style="text-align: left;">
                            <strong>
                                <asp:Label ID="lblWS" runat="server" Text="Worksite:"></asp:Label>
                                &nbsp;<asp:DropDownList ID="ddlWorksite" AutoPostBack="true" runat="server" CssClass="droplist"
                                    OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged">
                                </asp:DropDownList>
                            </strong><strong>
                                <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label>
                                &nbsp;<asp:DropDownList ID="ddlDept" AutoPostBack="true" CssClass="droplist" runat="server"
                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search"
                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                    TargetControlID="ddlDept" />
                            </strong><strong>
                                <asp:Label ID="lblEmp" runat="server" Text="Employee:"></asp:Label>
                                &nbsp;<asp:DropDownList ID="ddlEmp" AutoPostBack="true" CssClass="droplist" runat="server">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search"
                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                    TargetControlID="ddlEmp" />
                            </strong><strong>&nbsp;
                                <asp:Label ID="lblMonth" runat="server" Text="Month Wise"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="droplist"
                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="droplist">
                                </asp:DropDownList>
                            </strong><strong>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click" OnClientClick="javascript:return valids();" />
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: top;" colspan="2">
                            <asp:UpdatePanel ID="updpgridview" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                        CellSpacing="1" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDataBound="gdvAttend_RowDataBound">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Dt" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDt" runat="server" Text='<%#Eval("DisplayDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attendance">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="71px" CssClass="droplist"
                                                        DataTextField="ShortName" DataValueField="ID" DataSource='<%# BindAttendanceType()%>'>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnHid" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="In Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIN" runat="server" Width="100" Text='<%#Bind("intime")%>' Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Out">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllOut" Text="Out" onclick="SelectAllOut(this);" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOut" runat="server"  OnCheckedChanged="chkOut_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Out Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("Outtime")%>' Enabled="true"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Height="18px" TextMode="MultiLine" Width="200px"
                                                        Text='<%#Bind("Remarks")%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
