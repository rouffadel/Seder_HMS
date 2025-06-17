<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Attendance.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMSV1.AttendanceV1" Title="Attendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gdvAttend.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gdvAttend.ClientID %>');
            var TargetChildControl = "chkSelect";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
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
                    if (Inputs[2].type == 'checkbox' && Inputs[3].type == 'text') {
                        Inputs[2].checked = CheckBox.checked;
                        var EmpId = Labels[0].innerText;
                        var Result = AjaxDAL.HR_UpdateOutTime(EmpId);
                        if (Inputs[2].checked == true) {
                            Inputs[3].value = Result.value;
                        }
                        else {
                            Inputs[3].value = "";
                        }
                    }
                }
            }
        }
        function SelectAllYesterdayOut(CheckBox) {
            var Result = AjaxDAL.GetServerDate();
            //            var d = new Date();
            //            d = Result.value;
            TotalChkBx = parseInt('<%= this.gvOutTime.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gvOutTime.ClientID %>');
            var TargetChildControl = "chkOut";
            var TargetChildOutControl = "txtOUT";
            var TRs = TargetBaseControl.getElementsByTagName("TR");
            for (var iTR = 0; iTR < TRs.length; ++iTR) {
                var Inputs = TRs[iTR].getElementsByTagName("input");
                var Labels = TRs[iTR].getElementsByTagName("SPAN");
                if (Inputs.length > 2) {
                    if (Inputs[1].type == 'checkbox' && Inputs[2].type == 'text') {
                        Inputs[1].checked = CheckBox.checked;
                        var EmpId = Labels[0].innerText;
                        var Result = AjaxDAL.HR_UpdateYesterDayOutTime(EmpId);
                        Inputs[2].value = Result.value;
                    }
                }
            }
        }
        function EndRequestEventHandler(sender, args) {
            if (document.getElementById("<%=hdn.ClientID%>").value == "1") {
                alert("Timings Updated Successfully");
                return true;
            }
        }
        function GetOutTime(chkid, outtime, txtid, InID, EmpID) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(InID).value == "") {
                alert("Please Enter In Time");
                document.getElementById(chkid).checked = false;
                document.getElementById(txtid).value = "";
                document.getElementById(InID).focus();
                return false;
            }
            if (document.getElementById(chkid).checked) {
                document.getElementById(txtid).value = d;
                Result = AjaxDAL.HR_UpdateOutTime(EmpID);
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }
        function GetInTime(ddlid, outtime, txtid, txtOut, chkOut) {
            var d = new Date();
            if (document.getElementById(ddlid).value == "2") {
                document.getElementById(txtid).value = d.toLocaleTimeString();
            }
            else {
                document.getElementById(txtid).value = "";
                document.getElementById(txtOut).value = "";
                document.getElementById(chkOut).checked = false;
            }
        }
        function Validate(id) {
            //var reg = /^[0-2]{1,2}[:][0-9]{2}[:][0-9]{2}$/;
            //      if(reg.test(document.getElementById(id).value) == false)
            //      {
            //       alert("Please Enter Proper In Time");
            //       document.getElementById(id).focus();
            //       return false;
            //      }
        }
        function CheckLeaveCombination(ddlStatus, EmpID, txtIn, ddlWS, UserID, Row, txtid, ddlGProjects) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            var Status = $get(ddlStatus).value;
            if (Status != 1 && Status != 2 && Status != 7 && Status != 13 && Status != 14 && Status != 15) {
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
                var ResultStatus = AjaxDAL.HR_MarkAttandace(EmpID, Status, $get(ddlWS).value, UserID, $get(ddlGProjects).value);
                document.getElementById(Row).style.backgroundColor = "#ffffff";
                if (ResultStatus.value.Column1 == "0") {
                    alert("Not Updated");
                    return false;
                }
            }
            if (Status.value == "2") {
                document.getElementById(txtIn).value = d;
            }
            else {
                document.getElementById(txtIn).value = "";
            }
        }
        function UpdateAttendance(Status, EmpId, txtIn, txtOut, chkOut, txtRemarks, ddlWS) {
            var ResultVal = AjaxDAL.UpdateAttendance($get(Status).value, EmpId, $get(txtIn).value, $get(txtOut).value, $get(txtRemarks).value, $get(ddlWS).value);
            if (ResultVal.erorr == null && ResultVal.value != null) {
                alert("updated");
                return false;
            }
        }
        function UpdateRemarks(EmpId, txtRemarks) {
            var ResultRemarks = AjaxDAL.HR_UpdateRemarks($get(txtRemarks).value, EmpId);
            if (ResultRemarks.erorr == null && ResultRemarks.value != null) {
                alert("Updated");
                return false;
            }
        }
        function PINOutTime(chkid, outtime, txtid, InID, EmpID) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(InID).value == "") {
                alert("Please Enter In Time");
                document.getElementById(chkid).checked = false;
                document.getElementById(txtid).value = "";
                document.getElementById(InID).focus();
                return false;
            }
            if (document.getElementById(chkid).checked) {
                document.getElementById(txtid).value = d;
                var Result = AjaxDAL.HR_PINOutTime(EmpID);
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }
        //chaitanya;;;for validation purpose below javascript
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
        <%--function MyFunction() {
            if (<%=Server.HtmlEncode(Session["UserId"].ToString())%>==1) {
               // $("#dvreverse").css("display", "block");
                document.getElementById("used").style.display = '';
                document.getElementById("exp").style.display = '';
                //$("#dvreverse1").css("display", "block");
                //$("#dvreverse2").css("display", "block");
            }
            else {
             //   $("#dvreverse").css("display", "block");
                document.getElementById("used").style.display = 'none';
                document.getElementById("exp").style.display = 'none';
                //$("#dvreverse1").css("display", "none");
                //$("#dvreverse2").css("display", "none");
            }
           // return;
        }--%>
    </script>
    <script type="text/javascript">
        
       $(document).ready(function () {
        // function MyFunction() {

            if (<%=Server.HtmlEncode(Session["UserId"].ToString())%>==1) {
                //$("#dvreverse").css("display", "none");
                //$("#dvreverse").css("display1", "none");
                //$("#dvreverse").css("display2", "none");
                document.getElementById("used").style.display = '';
                document.getElementById("exp").style.display = '';
                //$("#dvreverse1").css("display", "block");
                //$("#dvreverse2").css("display", "block");
            }
            else {
                //$("#dvreverse").css("display", "block");
                //$("#dvreverse1").css("display", "block");
                //$("#dvreverse2").css("display", "block");
                document.getElementById("used").style.display = 'none';
                document.getElementById("exp").style.display = 'none';
                //$("#dvreverse1").css("display", "none");
                //$("#dvreverse2").css("display", "none");
            }
        });
    </script>
    <table style="vertical-align: top; width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" style="vertical-align: top;">
                <asp:UpdatePanel ID="updmaintable" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: top;">
                            <tr>
                                <td valign="top" style="vertical-align: top; color: green; font-size: large">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="tdOutTime" runat="server" visible="false">
                                <td align="left" style="vertical-align: top;">
                                    <div id="dvYesterdayPending" runat="server" visible="false">
                                        <table style="border-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;"
                                            border="0" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <td class="pageheader" style="height: 26px; text-align: left; width: 471px; color: blue">Yesterday&#39;s Attendance Pending Out-times
                                                </td>
                                                <td align="right" style="height: 26px">
                                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" ForeColor="#CC6600" CssClass="btn btn-primary"
                                                        Font-Bold="True" OnClick="btnRefresh_Click" ToolTip="Click Refresh after Select all OutTime CheckBoxes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <asp:GridView ID="gvOutTime" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                        CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Found"
                                                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDataBound="gvOutTime_RowDataBound" CssClass="gridview">
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="EmpID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Name" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Attendance">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlStatus" CssClass="droplist" runat="server" Width="71px">
                                                                        <asp:ListItem Text="PIN" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Button ID="btnHid" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="In Time">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtIN" runat="server" Width="100" Text='<%#Bind("InTime")%>' Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAllOut" Text="Out" onclick="SelectAllYesterdayOut(this);" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkOut" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("OutTime")%>' Enabled="false"></asp:TextBox>
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
                                                        <HeaderStyle CssClass="tableHead" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    </asp:GridView>
                                                    <uc1:Paging ID="OutTimePaging" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr id="tdGap" runat="server" visible="false">
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <table style="border-right: #d56511 1px solid; vertical-align: top; border-top: #d56511 1px solid; border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;"
                                        border="0"
                                        cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td colspan="2">
                                                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                                    SelectedIndex="0">
                                                    <Panes>
                                                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                                Search Criteria
                                                            </Header>
                                                            <Content>
                                                                <asp:UpdatePanel ID="updAttendance" runat="server">
                                                                    <ContentTemplate>
                                                                        
                                                                        <table>
                                                                             <div  id="dvreverse" runat="server">
                                                                            <tr id="used" >
                                                                                <td></span>Worksite</b>&nbsp;<asp:DropDownList ID="ddlWorksite" runat="server" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddl_SelectedIndexChanged" CssClass="droplist">
                                                                                </asp:DropDownList>
                                                                                    <strong>Project</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddlProject" runat="server"
                                                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" CssClass="droplist">
                                                                                    </asp:DropDownList>
                                                                                    <strong>Department</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddlDepartment" runat="server"
                                                                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_SelectedIndexChanged" CssClass="droplist">
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;
                                                                                     <b>Shift</b>&nbsp;
                                                                                        <asp:DropDownList
                                                                                            ID="ddlShift" AutoPostBack="true" runat="server" Visible="true" CssClass="droplist"
                                                                                            OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    <b>EmpID/Emp Name</b>&nbsp;&nbsp;
                                                                                        <asp:DropDownList
                                                                                            ID="ddlempid" runat="server" Visible="true" CssClass="droplist">
                                                                                        </asp:DropDownList>
                                                                                    <cc1:ListSearchExtender ID="ListSearchExtender3" QueryPattern="Contains" runat="server" TargetControlID="ddlempid" PromptText="Type to search..." PromptCssClass="PromptText" PromptPosition="Top" IsSorted="true"></cc1:ListSearchExtender>
                                                                                    <strong>EmpID</strong>&nbsp;&nbsp;
                                                                                <asp:TextBox ID="TxtEmpID" Height="22px" Width="70px" runat="server" AutoPostBack="True" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TxtEmpID"
                                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Id]"></cc1:TextBoxWatermarkExtender>
                                                                                    <strong>Employee Name</strong>&nbsp;&nbsp;
                                                                                    <asp:TextBox ID="txtename" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtename"
                                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter EmployeeName]"></cc1:TextBoxWatermarkExtender>
                                                                                    <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btn btn-warning" Height="21px" OnClick="btnIMportExcel_Click" />
                                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" Height="21px" OnClick="btnSearch_Click" />
                                                                                </td>
                                                                            </tr>
                                                                             </div>
                                                                            <tr>
                                                                                <td>
                                                                                    <div
                                                                                        <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                        </span><b><span style="float: left;">
                                                                                            <asp:DropDownList ID="ddlAttType" CssClass="droplist" runat="server">
                                                                                            </asp:DropDownList>
                                                                                            &nbsp;<asp:Button ID="btnOK" runat="server" CssClass="btn btn-success" Height="21px" OnClick="btnOK_Click" OnClientClick=""
                                                                                                Text="Mark" />
                                                                                            </td>
                                                                                            </tr>
                                                                                             <div  id="dvreverse1" runat="server">
                                                                                             <tr  id="exp">
                                                                                            <td>
                                                                                            <asp:Button ID="btnPrint" CssClass="btn btn-primary" ToolTip="View Attendance Slip" runat="server"
                                                                                                Text="TimeSheet Acknowledgement" Height="21px" OnClick="btnPrint_Click" />
                                                                                            &nbsp;&nbsp;
                                                                                         <asp:ImageButton ID="btnDayExporttoExcel" ToolTip="Export to Excel Day Report" runat="server" ImageUrl="../Images/ExportToExcel.bmp"
                                                                                             CssClass="savebutton" Width="30px" Height="30px" Text="Export to Excel" OnClick="btnDayExporttoExcel_Click" />
                                                                                            &nbsp;&nbsp;
                                                                                          <asp:ImageButton ID="Button1" ToolTip="Import From Excel Day Report" runat="server" CssClass="savebutton" ImageUrl="../Images/importFromEXcel.bmp"
                                                                                              Text="Import From Excel" Width="30px" Height="30px" OnClick="Button1_Click" />
                                                                                                </td></tr>
                                                                                            </div>
                                                                                            </table>
                                                                                    </div>
                                                                                    <div style="float: right; font-size: 12px; color: red" id="dvreverse2" runat="server">
                                                                                        1. Marked employees records move to last pages .<br />
                                                                                        2. Once attendance is marked, you can not edit here in this page.<br />
                                                                                        3. Re-Marking will not affect already marked records
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                            
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnDayExporttoExcel" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </Content>
                                                        </cc1:AccordionPane>
                                                    </Panes>
                                                </cc1:Accordion>
                                            </td>
                                        </tr>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top;" colspan="2">
                                    <hr />
                                    <asp:UpdatePanel ID="updpgridview" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1" CssClass="gridview"
                                                CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None" OnRowDataBound="gdvAttend_RowDataBound"
                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Attendance">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="71px" CssClass="droplist"
                                                                DataTextField="ShortName" DataValueField="ID" DataSource='<%# BindAttendanceType()%>'>
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnHid" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCL" Font-Bold="true" runat="server" Text='<%#Bind("CL")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEL" Font-Bold="true" runat="server" Text='<%#Bind("EL")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSL" Font-Bold="true" runat="server" Text='<%#Bind("SL")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In Time">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIN" runat="server" Width="100" Text='<%#Bind("InTime")%>' Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Style="display: none;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllOut" Text="Out" onclick="SelectAllOut(this);" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkOut" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out Time">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("OutTime")%>' Enabled="false"></asp:TextBox>
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
                                                    <asp:TemplateField HeaderText="Project">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlGProjects" runat="server" Width="151px" CssClass="droplist"
                                                                DataTextField="ProjectName" DataValueField="ProjectID" DataSource='<%# ViewState["Projects"]%>'>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle CssClass="tableHead" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                            <uc1:Paging ID="AddAttpaging" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        </td> </tr> </table>
                        <asp:HiddenField ID="hdn" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <img src="IMAGES/updateProgress.gif" alt="update is in progress" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
