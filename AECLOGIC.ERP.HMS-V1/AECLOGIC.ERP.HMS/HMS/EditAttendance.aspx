<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="EditAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.EditAttendanceV1" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                        var Result = AjaxDAL.HR_UpdateOutTimeEdit(EmpId, document.getElementById('<%=txtDay.ClientID%>').value);
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
        
        function EndRequestEventHandler(sender, args) {
            if (document.getElementById("<%=hdn.ClientID%>").value == "1") {
                alert("Timings Updated Successfully");
                return true;
            }
        }
        function GetOutTime(chkid, outtime, txtid, InID) {
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
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }
        function GetInTime(ddlid, outtime, txtid, txtOut, chkOut) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(ddlid).selectedIndex == 1) {
                document.getElementById(txtid).value = d;
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
        function CheckLeaveCombination(Status, EmpID, txtIn, txtOut, chkOut, Row) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            if (Status.value != 1 && Status.value != 2 && Status.value != 7) {
                var ResultVal = AjaxDAL.CheckAvailable(Status.value, EmpID);
                if (ResultVal.erorr == null && ResultVal.value.Result == 0) {
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed');
                    return false;
                }
                else {
                    document.getElementById(Row).style.backgroundColor = "#ffffff";
                }
            }
            if (Status.value == "2") {
                document.getElementById(txtIn).value = d;
            }
            else {
                document.getElementById(txtIn).value = "";
                document.getElementById(txtOut).value = "";
                document.getElementById(chkOut).checked = false;
                document.getElementById(txtOut).disabled = true;
                document.getElementById(txtIn).disabled = true;
            }
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
                <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
            }
        //chaitanya:for validation purpose
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
        function ConfirmSave() {
            var empname = document.getElementById('<%=txtSingleEmpName.ClientID %>');
            var dtFromDt = document.getElementById('<%=txtFromDate.ClientID %>');
            var dtToDt = document.getElementById('<%=txtToDate.ClientID %>');
            var cnfm = confirm("Are you sure you want to save Empid-" + empname.value + " with FromDate and ToDates - (" + dtFromDt.value + " to " + dtToDt.value + ") ?");
            if (cnfm) {
                return true;
            } else {
                return false;
            }
        }
       function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtsingleEmpNameHidden.ClientID %>').value = HdnKey;
          }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 500px;">
        <tr>
            <td>
                <asp:UpdatePanel ID="updmaintable" runat="server">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" style="width: 100%; border: 0px;">
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" style="aborder-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;"
                                        border="0"
                                        cellpadding="3" cellspacing="3">
                                        <tr id="trDate" runat="server" visible="false">
                                            <td style="border: 2px outset #CC6600;">
                                                <strong>From</strong>
                                                <asp:TextBox ID="txtFrom" Width="70Px" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="txtFrom"
                                                    PopupButtonID="txtFrom"></cc1:CalendarExtender>
                                                <strong>&nbsp;Upto</strong>
                                                <asp:TextBox ID="txtUpto" Width="70Px" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="Calendarextender2" runat="server" TargetControlID="txtUpto"
                                                    PopupButtonID="txtUpto"></cc1:CalendarExtender>
                                                <hr />
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>
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
                                                                <asp:UpdatePanel ID="updEdtendance" runat="server">
                                                                    <ContentTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <%--  &nbsp;<asp:ImageButton ID="ImgBetwenDates" ToolTip="Attendance between corresponding dates"
                                                                                        runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" OnClick="ImgBetwenDates_Click" />--%>
                                                                                    &nbsp; &nbsp;WorkSite:
                                                                                     <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                                    Department&nbsp;
                                                                                    <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                                                    <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    <cc1:TextBoxWatermarkExtender
                                                                                        ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdepartment" WatermarkCssClass="Watermarktxtbox"
                                                                                        WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                                     <asp:CheckBox ID="chkVAcation" Text="In Vacation" Checked="false" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp; Date<asp:TextBox ID="txtDay" Height="22px" Width="189px" runat="server" AutoPostBack="true"
                                                                                    OnTextChanged="txtDay_TextChanged"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txtDayCalederExtender" runat="server" TargetControlID="txtDay"
                                                                                        PopupButtonID="txtDOB"></cc1:CalendarExtender>
                                                                                    <strong>EmpId</strong><asp:TextBox ID="txtempid" Height="22px" Width="189px" runat="server" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="true"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetEmpidList" ServicePath="" TargetControlID="txtEmpid" UseContextKey="true"
                                                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                                        FirstRowSelected="True">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    <cc1:TextBoxWatermarkExtender
                                                                                        ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtempid" WatermarkCssClass="Watermarktxtbox"
                                                                                        WatermarkText="[Enter EMP ID]"></cc1:TextBoxWatermarkExtender>
                                                                                    &nbsp;<strong>Name</strong>
                                                                                    <asp:TextBox ID="txtName" Height="22px" Width="189px" runat="server"></asp:TextBox><cc1:TextBoxWatermarkExtender
                                                                                        ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtName" WatermarkCssClass="Watermarktxtbox"
                                                                                        WatermarkText="[Enter Name]"></cc1:TextBoxWatermarkExtender>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_empname" ServicePath="" TargetControlID="txtName"
                                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" Height="21px" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </Content>
                                                        </cc1:AccordionPane>
                                                    </Panes>
                                                </cc1:Accordion>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnOK" runat="server" CssClass="savebutton" Height="21px" Visible="false"
                                                    Text="ApplyAll" />
                                                <asp:DropDownList ID="ddlAttType" CssClass="droplist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAttType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;
                                                <asp:TextBox ID="txttime" runat="server" Height="22px" Width="189px" ToolTip="Enter IN Time HH:MM AM/PM"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender
                                                    ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txttime" WatermarkCssClass="Watermarktxtbox"
                                                    WatermarkText="[Enter IN Time HH:MM [24hrs]]"></cc1:TextBoxWatermarkExtender>
                                                <asp:TextBox ID="txtouttime" runat="server" Height="22px" Width="189px" ToolTip="Enter OUT Time HH:MM AM/PM"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender
                                                    ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtouttime" WatermarkCssClass="Watermarktxtbox"
                                                    WatermarkText="[Enter OUT Time HH:MM [24hrs]]"></cc1:TextBoxWatermarkExtender>
                                                <asp:Button ID="btnApplyselected" runat="server" CssClass="btn btn-primary" Height="21px" Text="ApplySelected" OnClick="btnApplyselected_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="2">
                                                <hr />
                                                <asp:UpdatePanel ID="updpgridview" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                            CellSpacing="1" DataKeyNames="Empid" ForeColor="#333333" GridLines="None" OnRowDataBound="gdvAttend_RowDataBound"
                                                            OnRowCommand="gdvAttend_RowCommand" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
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
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpID" runat="server" Style="display: none" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="droplist" Width="71px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                                            AutoPostBack="true" DataTextField="ShortName" DataValueField="ID"
                                                                            DataSource='<%# BindAttendanceType()%>'>
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="btnHid" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIN" runat="server" Text='<%#Bind("InTime")%>' Width="100"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAllOut" Text="Out" onclick="SelectAllOut(this);" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkOut" runat="server" OnCheckedChanged="chkOut_CheckedChanged"
                                                                            AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("OutTime")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" Height="18px" TextMode="MultiLine" Width="200px"
                                                                            Text='<%#Bind("Remarks")%>' ReadOnly="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-warning" Text="Update" CommandArgument='<%#Bind("EmpId")%>'
                                                                            CommandName="upd"></asp:LinkButton>
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
                                                        <uc1:Paging ID="EditAttpaging" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                                    SelectedIndex="0">
                                                    <Panes>
                                                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                               Single Emp - Multiple Date entries
                                                            </Header>
                                                            <Content>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <strong>Emp Name</strong>
                                                                                    <asp:HiddenField ID="txtsingleEmpNameHidden" runat="server" />
                                                                                    <asp:TextBox ID="txtSingleEmpName" Height="20px" runat="server" TabIndex="6" AccessKey="5"
                                                                                            ToolTip="[Alt+5]">                                                              
                                                                                        </asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtSingleEmpName"
                                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                        <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                                                                            TargetControlID="txtSingleEmpName"></cc1:TextBoxWatermarkExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr><td>From Date<asp:TextBox ID="txtFromDate" Height="22px" Width="189px" runat="server" AutoPostBack="true"
                                                                                    OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txtFromDateCalendarExtender" runat="server" TargetControlID="txtFromDate"
                                                                                        PopupButtonID="txtDOB1"></cc1:CalendarExtender>
                                                                                    To Date<asp:TextBox ID="txtToDate" Height="22px" Width="189px" runat="server" AutoPostBack="true"
                                                                                        OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="txtToDateCalendarExtender" runat="server" TargetControlID="txtToDate"
                                                                                            PopupButtonID="txtDOB2"></cc1:CalendarExtender>
                                                                             </td></tr>
                                                                            <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnSearch1" runat="server" Text="Search" Height="21px" CssClass="btn btn-primary" OnClick="btnSearch1_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </Content>
                                                        </cc1:AccordionPane>
                                                    </Panes>
                                                </cc1:Accordion>
                                            </td>
                                        </tr>
                                      <tr>
    <td>
       
        <asp:Label ID="lblPaidDays" Text="Previous Month:" runat="server" Visible="false" Font-Size="14px" Font-Bold="true" ForeColor="Green"></asp:Label>


    </td>
</tr>
                                    <tr><td>
                                            <asp:GridView ID="gdvMonthReportPrv" runat="server" AutoGenerateColumns="true"
                                                HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                                                CssClass="gridview" OnRowDataBound="gdvMonthReportPrv_RowDataBound" Visible="false">
                                                <Columns>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                                                              <tr>
    <td>
       
        <asp:Label ID="lblPaidDaysC" Text="Courrent Month:" runat="server" Visible="false" Font-Size="14px" Font-Bold="true" ForeColor="Green"></asp:Label>


    </td>
</tr>
                                    <tr><td>
                                        <asp:GridView ID="gdvMonthReport" runat="server" AutoGenerateColumns="true"
                                            HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                                            CssClass="gridview" OnRowDataBound="gdvMonthReport_RowDataBound" Visible="false">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="2">
                                                <hr />
                                                <asp:UpdatePanel ID="upMultipleDates" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvEmpMultipleDates" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                            CellSpacing="1" DataKeyNames="Empid" ForeColor="#333333" GridLines="None" OnRowDataBound="gvEmpMultipleDates_RowDataBound"
                                                            OnRowCommand="gvEmpMultipleDates_RowCommand" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect1" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpID1" runat="server" Style="display: none" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlStatus1" runat="server" CssClass="droplist" Width="71px" 
                                                                            DataTextField="ShortName" DataValueField="ID"
                                                                            DataSource='<%# BindAttendanceType()%>'>
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="btnHid1" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIN1" runat="server" Text='<%#Bind("InTime")%>' Width="100"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOUT1" runat="server" Width="100" Text='<%#Bind("OutTime")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks1" runat="server" Height="18px" TextMode="MultiLine" Width="200px"
                                                                            Text='<%#Bind("Remarks")%>' ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdate1" runat="server" CssClass="btn btn-warning" Text="Update" CommandArgument='<%#Bind("EmpId")%>'
                                                                            CommandName="upd" OnClientClick="javascript: return ConfirmSave();"></asp:LinkButton>
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
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdn" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="updmaintable">
                    <ProgressTemplate>
                        <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
                        please wait...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
    </table>
</asp:Content>
