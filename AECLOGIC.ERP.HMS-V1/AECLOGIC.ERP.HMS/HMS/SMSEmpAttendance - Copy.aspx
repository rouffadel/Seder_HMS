<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="SMSEmpAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SMSEmpAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">


        function SearchModified() {
            $get('<%=hdnSearchChange.ClientID %>').value = "1";
        }

        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.grdSMSAttendance.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.grdSMSAttendance.ClientID %>');
            var TargetChildControl = "chkAll";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }

        function CheckLeaveCombination(Ctrl, EmpID, txtIn, lblSiteID, UserID, Row, txtid, Status) {
            var locStatus = $get(Status).value;
            var locInTime = $get(txtIn).value;

            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;
            if (locStatus != 1 && locStatus != 2 && locStatus != 7)//&& locStatus.value != 9
            {
                var ResultVal = AjaxDAL.CheckAvailable(locStatus, EmpID);
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
                    var ResultStatus = AjaxDAL.HR_MarkAttandace(EmpID, locStatus.value, lblSiteID, UserID);
                    if (ResultStatus.value.Column1 == "0") {
                        alert("Not Updated");
                        return false;
                    }
                }
            }
            else {
                var ResultStatus = AjaxDAL.HR_MarkAttandaceBySMS(EmpID, locStatus, lblSiteID, UserID, locInTime);
                document.getElementById(Row).style.backgroundColor = "#ffffff";
                if (ResultStatus.value.Column1 == "0") {
                    alert("Not Updated");
                    return false;
                }
            }

            if (locStatus == "2") {
                document.getElementById(txtIn).value = d.toLocaleTimeString();
            }
            else {
                document.getElementById(txtIn).value = "";

            }
        }

        // Validation for Checkbox is either checked or not
        function validateCheckBox() {
            var isValid = false;
            var gvChk = document.getElementById('<%= grdSMSAttendance.ClientID %>');
            for (var i = 1; i < gvChk.rows.length; i++) {
                var chkInput = gvChk.rows[i].getElementsByTagName('input');
                if (chkInput != null) {
                    if (chkInput[0].type == "checkbox") {
                        if (chkInput[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            alert("Please select atleast one Employee.");
            return false;
        }


    </script>
    <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblWS" runat="server" Text="WorkSite:"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlWS" CssClass="droplist"  runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlDept"  CssClass="droplist" runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblShift" runat="server" Text="Shift:"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlShift" CssClass="droplist"  runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="75px" Height="25px"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                        <tr>
                            <td align="left">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="dvgrd" runat="server" visible="false" style="width: 100%">
                                                <table style="width: 100%" id="tblgrd" runat="server"  >
                                                    <tr>
                                                        <td>
                                                            <ajax:UpdatePanel ID="updgrid" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="grdSMSAttendance" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                                                        GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="5"
                                                                        EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="grdSMSAttendance_RowCommand"
                                                                        OnRowDataBound="grdSMSAttendance_RowDataBound">
                                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll(this);" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DeptID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDeptID" Text='<%#Eval("DeptID")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                                                                ControlStyle-Width="200px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Shift" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShift" runat="server" Text='<%#Eval("ShiftName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle Width="50" />
                                                                                <HeaderStyle Width="50" />
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="grdddlAttStatus" runat="server" Width="45px" CssClass="droplist"
                                                                                        DataTextField="ShortName" DataValueField="ID" DataSource='<%# BindStatus()%>'
                                                                                        SelectedIndex='<%# GetStatusIndex(Eval("Status").ToString())%>'>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="In Time">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtIN" runat="server" Width="140" Text='<%#Bind("DTTime")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sender Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSenderName" runat="server" Text='<%#Eval("SenderName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sender Worksite">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSenderWS" runat="server" Text='<%#Eval("SenderSite")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPunch" runat="server" CommandName="Punch" Text="Punch"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDel" runat="server" CommandName="Del" Text="Delete"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="TTransID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTransID" Text='<%#Eval("TTransID")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SiteID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSiteID" Text='<%#Eval("SiteID")%>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EditRowStyle BackColor="#999999" />
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </ajax:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnPunchAll" runat="server" Text="Punch All" CssClass="savebutton"
                                                                Width="127px" OnClick="btnPunchAll_Click" OnClientClick="javascript:validateCheckBox()" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <uc1:Paging ID="SMSEMPAttendancePaging" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Content>
