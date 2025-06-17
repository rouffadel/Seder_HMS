<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacationSettlementRev4.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.VacationSettlementRev4V1" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script>
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
        function GetstID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtempid1 .ClientID %>').value = HdnKey;
        }
        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtEmpNameHidden .ClientID %>').value = HdnKey;
        }
        //Check Number
        function chkNumber(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; }
            }
            if (val != '') {
                var rx = new RegExp("[0-9]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    elm.value = '';
                    elm.focus();
                    return false;
                }
            } return true;
        }
        function chkDecimal(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark)) { return false; }
            }
            if (val != '') {
                var rx = new RegExp("^(-?[1-9]+\\d*([.]\\d+)?)$|^(-?0[.]\\d*[1-9]+)$|^0$");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    elm.value = '';
                    elm.focus();
                    return false;
                }
            } return true;
        }
        //^[1-9]\d*(\.\d+)?$
        function Valid() {
            if (!chkNumber('<%=txtManualPayableDays.ClientID %>', 'Payable Days', true, ''))
                return false;
            if (!chkDecimal('<%=txtAlDays.ClientID %>', 'AL Days', true, ''))
                return false;
            return true;
        }
       
    </script>
    <style>
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .rounded_corners {
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            overflow: hidden;
        }
            .rounded_corners td {
                border: 0.5px solid #A1DCF2;
                font-family: Arial;
                font-size: 10pt;
                text-align: left;
            }
                .rounded_corners td:hover {
                    background-color: #0094ff;
                    color: white;
                }
            .rounded_corners th {
                border: 0.5px solid #A1DCF2;
                font-family: Arial;
                font-size: 10pt;
                text-align: left;
                font-weight: bold;
            }
            .rounded_corners table table td {
                border-style: dotted;
            }
        fieldset {
            display: block;
            margin-left: 2px;
            margin-right: 2px;
            padding-top: 0.35em;
            padding-bottom: 0.625em;
            padding-left: 0.75em;
            padding-right: 0.75em;
            border: 2px groove;
            border-color: #0094ff;
            border-radius: 15px 0px;
        }
        a {
            color: black;
            font-family: Verdana;
            color: black;
            cursor: pointer;
        }
            a:link {
                text-decoration: none;
            }
            a:visited {
                text-decoration: none;
            }
            a:hover {
                text-decoration: underline;
                cursor: pointer;
            }
            a:active {
                text-decoration: underline;
            }
        .td_remarak {
            font-style: italic;
            font-size: 9px;
            font-family: Verdana;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) {
            var oGrid = document.getElementById(grid);
            var IPs = oGrid.getElementsByTagName("input");
            for (var iCount = 0; iCount < IPs.length; ++iCount) {
                if (IPs[iCount].type == 'checkbox')
                    IPs[iCount].checked = hChkBox.checked;
            }
        }
        function OnlyNumeric(evt) {
            var chCode = evt.keyCode ? evt.keyCode : evt.charCode ? evt.charCode : evt.which;
            if (chCode >= 48 && chCode <= 57 ||
             chCode == 46) {
                return true;
            }
            else
                return false;
        }
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Future Date Cannot be Accepted!");
                sender._selectedDate = new Date();
                // set the date back to the today
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                return false;
            }
        }
        function validatesave() {
            if (document.getElementById('<%=txtempid.ClientID  %>').value == "") {
                alert("Please Enter employee id!");
                document.getElementById('<%=txtempid.ClientID %>').focus()
                return false;
            }
        }
        //chaitanya:for validation purpose
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="updpnl" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="UpdateProgressCSS">
                            <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="updpnl">
                                <ProgressTemplate>
                                    <div class="overlay">
                                        <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                            <span style="color: green; font-weight: bold">Loading...</span>
                                            <img src="../IMAGES/updateProgress.gif" alt="update is in progress" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="tblNewk" runat="server" Visible="False" CssClass="DivBorderTeal" Width="80%">
                            <table style="border-width: 1px; border-style: dotted; border-color: #0094ff; border-top-left-radius: 20px; border-bottom-right-radius: 20px">
                                <tr>
                                    <td>
                                        <b>Employeee ID:</b><span style="color: #ff0000">*</span>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </td>
                                    <td style="background-color: white; vertical-align: middle; border-radius: 10px">
                                        <asp:TextBox runat="server" ID="txtempid" Style="border-color: white" ToolTip="Search Employee ID!" Width="150px" onkeypress="return OnlyNumeric(event);" OnTextChanged="txtempid_changed" AutoPostBack="True" Placeholder="search employee!"></asp:TextBox><asp:ImageButton runat="server" ID="tickimgk" src="../Images/tick_16.png"></asp:ImageButton><asp:ImageButton runat="server" ID="notfoundk" src="../Images/Searching.png"></asp:ImageButton>
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblempdetails" Style="font-family: Verdana; font-size: 12px"></asp:Label></td>
                                </tr>
                            </table>
                            <table width="60%">
                                <tr>
                                    <td colspan="2">
                                        <fieldset>
                                            <legend><b>Employee Details:</b>  </legend>
                                            <table width="100%">
                                                <tr>
                                                    <td><b>DOJ:</b></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblDOJ" ForeColor="Green"> </asp:Label>
                                                    </td>
                                                    <td><b>Re-Join Date[DOR](if any?):</b> </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblROJ" ForeColor="Green"> </asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Annual Salary:</b> </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblsal" ForeColor="Green"> </asp:Label>
                                                    </td>
                                                    <td><b>Last sattlement Date(if any?): </b></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lbllastsattlemtdt" ForeColor="Green"> </asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><b>End Of Contract[EOC]:</b></td>
                                                    <td colspan="3">
                                                        <asp:Label runat="server" ID="lbldateofEndCOntract" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lbldurations" ForeColor="Green"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table style="text-align: left">
                                <tr runat="server" id="trwarning">
                                    <td style="vertical-align: middle">
                                        <img src="../Images/HandWarn.ico" title="Message" width="32px" height="32px" alt="Warning:" /></td>
                                    <td style="vertical-align: middle">
                                        <span style="color: red; font-weight: bold">*</span> <span style="background-color: yellow">[For the <b>Vacation sattlement</b> Employee should be <b>Contract To Hire [CTH].</b> To set Employee as CTH , follow Link go to Assign Tab :  <a id="a1" href="EmployeeList.aspx" runat="server" target="_blank">Edit Employee</a>]  </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date Of Sattle[DOS]:<span style="color: #ff0000">*</span></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDOS" ToolTip="Date of Sattle"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtcal" runat="server" TargetControlID="txtDOS" Format="dd-MMM-yyyy"
                                            PopupButtonID="txtDOS"></cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" runat="server" id="tddetails">
                                        <table class="rounded_corners">
                                            <thead>
                                                <tr>
                                                    <td colspan="3" style="font-weight: bold; color: #0094ff; background-color: white; font-size: 12px;">
                                                        <img src="../Images/HandWarn.ico" title="Message" width="20px" height="20px" alt="Warning:" /><span>Calculated Details:: </span></td>
                                                </tr>
                                            </thead>
                                            <tr>
                                                <td>
                                                    <b>Days of Worked[DW]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDW" runat="server" Text="0" ForeColor="Green"></asp:Label>
                                                </td>
                                                <td class="td_remarak ">([DOS]-[if [DOR]?[DOJ]])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Disallowed Days[DD]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDD" runat="server" Text="0" ForeColor="Green"></asp:Label>
                                                </td>
                                                <td class="td_remarak "></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Deff([DW]-[DD]):</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDeff" runat="server" ToolTip="([DW]-[DD]) Days." Text="0" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                </td>
                                                <td class="td_remarak ">([DW]-[DD]) Days.</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Total Services[Ts]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTs" runat="server" Text="0" ToolTip="([EOC]-[DOJ]) Days." ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([EOC]-[DOJ]) Days.</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>AL Gross:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAlGross" runat="server" Text="0" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Deff]/11)Days</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Attendnace:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblatt" runat="server" Text="0" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">(For Current Month:[Paybale]+[Absent]+[Penalities])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>OT Hr(if any?):</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOTHr" runat="server" Text="0" ForeColor="Green"></asp:Label>
                                                </td>
                                                <td class="td_remarak "></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A1[salary]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA1" runat="server" Text="0.00" ToolTip="(salary for the current month Attendnace as per Normal Payslip)" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">(salary for the current month as per Normal Payslip)</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A2[salary]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA2" runat="server" Text="0.00" ToolTip="(Enhancement of Al Gross)" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Al Gross]*[Basic/30])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A3[salary]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA3" runat="server" Text="0.00" ToolTip="(OT Hr*[[Basic/30]/8])" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">((OT Hr*[[Basic/30/]8]))</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A4[Gratuity]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA4" runat="server" Text="0.00" ToolTip="As per completion of Service below 5 years" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">[For 5 Yr below CTH EMployee (if completed [Basic]/2 OR [Basic]/2) ] Per Year</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A5[Gratuity]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA5" runat="server" Text="0.00" ToolTip="As per completion of Service above 5 years" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">[[For 5+ Yr CTH EMployee (if completed [Basic] OR [Basic]*2/3) ] Per Year]</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A6[salary]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA6" runat="server" Text="0.00" ToolTip="Salary for the Non-payemnt[needed clarification]" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">[salary for non-pay]</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>A7[Ded]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblA7" runat="server" Text="0.00" ToolTip="Ticket Deduction" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">[Click to details...]</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D1[AL]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD1" runat="server" Text="0.00" ToolTip="([DD]*Tpt Allowances])" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([DD]*Tpt Allowances])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D2[AL]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD2" runat="server" Text="0.00" ToolTip="([DD]*Food Allowances])" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([DD]*Food Allowances])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D3[salary]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD3" runat="server" Text="0.00" ToolTip="([All outstanding salary]+[Travels Advances])" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([All outstanding salary]+[Travels Advances])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D4[Ded]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD4" runat="server" Text="0.00" ToolTip="(Deductions For Medical Cards)" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Deductions For Medical Cards])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D5[Ded]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD5" runat="server" Text="0.00" ToolTip="(Deductions For Balance Iqama)" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Deductions For Balance Iqama])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D6[Ded]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD6" runat="server" Text="0.00" ToolTip="(Absent Penality from Current Month)" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Absent Penality from Current Month])</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>D7[Ded]:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblD7" runat="server" Text="0.00" ToolTip="(Traffic Violation)" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Traffic Violation])</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="color: #0094ff; font-weight: bold; background-color: white;">
                                                    <b>Totat Sattlement for the Vacations:</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Net Amount:</b><span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblnetAmt" runat="server" Text="0.00" ToolTip="([Basic]+[Sum of All Allowances]+[Sum of All Deductions])" ForeColor="Green"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="td_remarak ">([Basic]+[Sum of All Allowances]+[Sum of All Deductions])</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="tractions" runat="server">
                                    <td colspan="2" align="right" style="border-top-width: 1px; border-top-color: #0094ff; border-top-style: dotted">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                            OnClick="btnSubmit_Click"
                                            AccessKey="s" TabIndex="7" OnClientClick="javascript:return validatesave();"
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-danger" Width="70px"
                                            OnClick="btn_reste_Click" AccessKey="r" TabIndex="8" ToolTip="[Alt+r OR Alt+r+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table id="tblView" width="100%" runat="server">
                            <tr>
                                <td>
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
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>&nbsp;Employee Name&nbsp;
                                                        <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                                                <asp:TextBox ID="txtEmpName" runat="server" Height="22px" Width="300px" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;
						                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                                     MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
                                                                     UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                     CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempID">
                                                                 </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpName"
                                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:CheckBox ID="chkFinal1" runat="server" Text="Final" Enabled="true" />
                                                                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click1" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvVacation" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" GridLines="Both"
                                        EmptyDataText="No Records Found" Width="100%" CssClass="gridview" OnRowDataBound="gvVacation_RowDataBound" OnRowCommand="GVVacation_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Empid" HeaderText="EmpId" Visible="true"></asp:BoundField>
                                            <asp:BoundField DataField="EmpName" HeaderText="EmployeeName"></asp:BoundField>
                                            <asp:BoundField DataField="SettlementDate" HeaderText="Settlement Date"></asp:BoundField>
                                            <asp:BoundField DataField="AddedAmount" HeaderText="Added Amount"></asp:BoundField>
                                            <asp:BoundField DataField="DeductedAmount" HeaderText="Deducted Amount"></asp:BoundField>
                                            <asp:BoundField DataField="TotalAmt" HeaderText="Total Amount"></asp:BoundField>
                                            <asp:TemplateField  HeaderText="Proof">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("vid")%>'
                                                        CommandName="Edt"></asp:LinkButton>--%>
                                                         <a id="A6" target="_blank" href='<%# Eval("FilePath") %>'
                                                            runat="server" class="btn btn-primary">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkstatus" CssClass="anchor__grd vw_grd" CommandArgument='<%#Eval("vid")%>'
                                                        OnClientClick="return confirm('Are you Sure?');" runat="server" CommandName="App"
                                                        Text='Approve'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="anchor__grd vw_grd" Text="Print" CommandArgument='<%#Eval("Empid")%>' CommandName="Print"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" CssClass="anchor__grd vw_grd" CommandArgument='<%#Eval("vid")%>'
                                            OnClientClick="return confirm('Are you Sure?');" runat="server" CommandName="Del"
                                            Text='Delete'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                             <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcutdate" runat="server" CssClass="anchor__grd edit_grd" Text='<%#Eval("SettlementDate")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table id="tblNew" runat="server" visible="false" width="100%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="DesigAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="DesigAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="5" cellspacing="0" style="min-width: 80%">
                                                        <tr style="vertical-align: middle">
                                                            <td>
                                                                <b>
                                                                    <asp:TextBox ID="txtename" Height="20" runat="server"></asp:TextBox>
                                                                    <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtename"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetstID"
                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtename"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>--%>
                                                                    <asp:HiddenField ID="txtempid1" runat="server" Value="0"></asp:HiddenField>
                                                                </b>
                                                                <asp:CheckBox ID="chkFinal" runat="server" Text="Final" OnCheckedChanged="chkFinal_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                                                <asp:Button runat="server" Text="Search" ID="btnSearchLeaveDet" CssClass="btn btn-primary" OnClick="btnSearchLeaveDet_Click" Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <b>
                                                                    <asp:TextBox ID="txtdate" Height="20" runat="server" CssClass="hiddencol"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calextnd" runat="server" TargetControlID="txtdate" PopupButtonID="txtdate"
                                                                        Format="dd/MM/yyyy" Enabled="false" OnClientDateSelectionChanged="CheckDateEalier"></cc1:CalendarExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                                        TargetControlID="txtdate"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Date Of Settelement]"></cc1:TextBoxWatermarkExtender>
                                                                    <cc1:FilteredTextBoxExtender FilterType="Custom,Numbers" ID="fiterdwtr" runat="server"
                                                                        TargetControlID="txtdate" ValidChars="/" Enabled="True"></cc1:FilteredTextBoxExtender>
                                                                    <br /></td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkViewAttendance" runat="server" Text="ViewAttendance" Visible="false" OnClick="lnkViewAttendance_Click"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;
                                                                 <asp:LinkButton ID="lnkViewLeaveGrant" runat="server" Text="Leave Application" Visible="false" OnClick="lnkViewLeaveGrant_Click"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lnksalryBrkup" runat="server" Text="Salary Breakup" Visible="false" OnClick="lnksalryBrkup_Click"></asp:LinkButton>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="btnPrint" runat="server" Text="Print" Visible="false" OnClick="btnPrint_Click"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvGranted" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="gridview" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvGranted_RowCommand"
                                        AlternatingRowStyle-BackColor="GhostWhite"
                                        OnRowDataBound="gvGranted_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="LID" Visible="false" />
                                            <asp:BoundField HeaderText="EmpID" DataField="EmpID" Visible="false" />
                                            <asp:BoundField HeaderStyle-Width="500Px" HeaderText="Name" DataField="name" />
                                            <asp:BoundField HeaderText="DOJ" DataField="DOJ" />
                                            <asp:BoundField HeaderText="LVRD" DataField="LVRD" />
                                            <asp:BoundField HeaderText="Auth Salary" DataField="AuthSalary" />
                                            <asp:BoundField HeaderText="Type of Leave" DataField="LeaveType" />
                                            <asp:BoundField HeaderText="Avable Leaves" DataField="AvailableLeaves" />
                                            <asp:BoundField HeaderText="Appld-On" DataField="AppliedOn" />
                                            <asp:BoundField HeaderText="From" DataField="LeaveFrom" />
                                            <asp:BoundField HeaderText="Till" DataField="LeaveUntil" />
                                            <asp:BoundField HeaderText="Days" DataField="AppliedDays" />
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkReason" runat="server" Text="Read"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Granted From" DataField="GrantedFrom" />
                                            <asp:BoundField HeaderText="Granted Until" DataField="GrantedUntil" />
                                            <asp:BoundField HeaderText="Granted Days" DataField="GrantedDays" />
                                            <asp:TemplateField HeaderStyle-Width="30Px" HeaderText="Apprvd By">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblOkby" runat="server" Text='<%#Bind("OkBy")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderStyle-Width="120Px" HeaderText="Granted On" DataField="GrantedOn" />
                                            <asp:BoundField HeaderStyle-Width="150Px" HeaderText="Admin-Comments" DataField="Comment" />
                                            <asp:BoundField HeaderStyle-Width="150Px" HeaderText="Emp-Response" DataField="CommentReply" />
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" CommandName="edt" CssClass="btn btn-primary" CommandArgument='<%#Eval("LID")%>'
                                                        runat="server">Process</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tbshow" runat="server" width="100%">
                                        <tr>
                                            <td>
                                                <table id="tblremarks" runat="server" visible="false">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvRemarks" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                                                                EmptyDataText="No Records Found" Width="100%" CssClass="gridview">
                                                                <Columns>
                                                                    <asp:BoundField DataField="status" HeaderText="Level" />
                                                                    <asp:BoundField DataField="Name" HeaderText="Remarked By" />
                                                                    <asp:BoundField DataField="remarks" HeaderText="Remarks" ItemStyle-Width="500px" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td colspan="2"> <b>Last Vacation Date : &nbsp;&nbsp;
                                                    <asp:Label ID="LVDate" runat="server" Font-Bold="true"></asp:Label>
                                                    <b>&nbsp;Return Date:</b>&nbsp;&nbsp;
                                                    <asp:Label ID="RDate" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td>Last working day &nbsp;&nbsp;
                                    <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Payable Days
                                    <asp:TextBox ID="txtManualPayableDays" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                            <tr>
                                                  <td>AL Days
                                                    <asp:TextBox ID="txtAlDays" runat="server" Width="50px" Text="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkEnch" runat="server" Text="Encashment" Visible="false" />
                                                <asp:CheckBox ID="chkHRALOP" runat="server" Text="Include HRA for LOP" Visible="false" />
                                                <div id="dvfnactive" runat="server" visible="false">
                                                    <b>Exit Type </b>
                                                    <asp:DropDownList ID="ddlEmpDeAct" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Button ID="btnSearch" Text="Compute" CssClass="btn btn-success" runat="server" OnClick="btnSearch_Click" Visible="false" OnClientClick="javascript:return Valid();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 100%;">
                                        <table runat="server" id="tblN" width="100%">
                                            <tr>
                                                <td style="text-align: left" colspan="2">
                                                    <asp:Table ID="tblAtt" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                                                    </asp:Table>
                                                    <asp:GridView ID="gvAttLog" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                                                        <Columns>
                                                            <asp:BoundField DataField="INOutType" HeaderText="IN/OUT" />
                                                            <asp:BoundField DataField="INOUTTym" HeaderText="TIME" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPresendays" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblNoOfDays" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="dtlvacation" runat="server" HeaderStyle-CssClass="datalistHead" Width="100%" >
                                        <ItemTemplate>
                                            <div class="DivBorderOlive" style="margin-bottom: 20px">
                                                <table style="width: 100%; background-color: #efefef;">
                                                    <tr>
                                                        <td>
                                                            <b>EmployeeName :</b>
                                                            <asp:Label ID="lblempname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>EmployeeId :</b>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>DateOfJoin :</b>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Dateofjoin")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Balance Annual Leaves:</b>
                                                            <asp:Label ID="lblAAL" runat="server" Text='<%#Eval("AvailableLeaves")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Requested Leaves :</b>
                                                            <asp:Label ID="lblRAL" runat="server" Text='<%#Eval("RequestedLeaves")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Granted Leaves :</b>
                                                            <asp:Label ID="lblGAL" runat="server" Text='<%#Eval("GrantedDays")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>OverTime Hours :</b>
                                                            <asp:Label ID="lblOT" runat="server" Text='<%#Eval("OTHours")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </div>
                                            </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                 </tr>
                            <tr>
                                <td>
                                    <div class="DivBorderOlive" style="margin-bottom: 20px">
                                    <asp:GridView ID="GVVacation1" Visible="true" runat="server" AlternatingRowStyle-BackColor="GhostWhite" 
                                        AutoGenerateColumns="false" ShowFooter="true" 
                                        OnRowDataBound="GVVacation_RowDataBound" 
                                        HeaderStyle-CssClass="tableHead" Width="100%" CssClass="gridview">
                                        <Columns>
                                            <asp:BoundField DataField="Description" HeaderText="Items"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Value" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtA1" OnTextChanged="QtyChanged" Style="text-align: right" AutoPostBack="true" Width="120px" Height="20px" runat="server" Text='<%#Bind("Amount")%>'
                                                        onkeypress="return NumberOnly()"></asp:TextBox>
                                                    <br />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 17px">
                                                        <asp:Label ID="lbl1" runat="server" Text="Total= "></asp:Label>
                                                        <asp:Label ID="lblvalue" runat="server" Text="0.000"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Additionals" >
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLoanSelect" runat="server" OnCheckedChanged="loanAmountChange" Visible="false" AutoPostBack="true" />
                                                     <asp:LinkButton ID="lnkupload" runat="server" Text="choose file" OnClick="lnkuploadclick" Visible="false" />
                                                    <asp:Label ID="lblFileName" runat="server" Visible ="false"></asp:Label>
                                                    <asp:TextBox ID="txtA6" Style="text-align: left" runat="server" Width="140px" Height="20px" Text='<%#Bind("Additionals")%>'></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtA6"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Description]"></cc1:TextBoxWatermarkExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pages">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkatt_Viewk" AutoPostBack="true" Text="Attendance" Visible="false" OnClick="lnkatt_Viewk_click" CssClass="anchor__grd vw_grd" ToolTip="Click to complete attendance details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkatt_add" AutoPostBack="true" Text="Adv.Att" Visible="false" OnClick="lnkatt_add_click" CssClass="anchor__grd vw_grd" ToolTip="Click to complete advance attendance details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkALGross" AutoPostBack="true" Text="AL A/c" Visible="false" OnClick="lnkALGross_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Leave Details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkOT" AutoPostBack="true" Text="OT" Visible="false" OnClick="lnkOT_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Over Time Details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkAirTicket" AutoPostBack="true" Text="AirTicket" Visible="false" OnClick="lnkAirTicket_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Get AirTicket Details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkGratuity" AutoPostBack="true" Text="Gratuity" Visible="false" OnClick="lnkGratuity_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Get Gratuity Details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkLoans" AutoPostBack="true" Text="Loans" Visible="false" OnClick="lnkLoans_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Employe Advance Details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkAbsentPenalities" AutoPostBack="true" Text="Abs Pen" Visible="false" OnClick="lnkABS_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Absent Penalities Details"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkEmpPen" AutoPostBack="true" Text="Emp Pen" Visible="false" OnClick="lnkEmpPen_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Employee Penalities Details"></asp:LinkButton>
                                                    <asp:Button ID="btnCal" Style="text-align: right" AutoPostBack="true" Width="30px" Height="30px" runat="server" Text='Get' CssClass="btn btn-success" OnClick="btnCal_Click" Visible="false"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Details" HeaderText="Details" />
                                        </Columns>
                                        <FooterStyle />
                                        <FooterStyle BackColor="#cccccc" ForeColor="Black" HorizontalAlign="left" />
                                    </asp:GridView>
                                        <asp:Button ID="btnupe" runat="server" Text="Button" style="display:none;"/>
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" BackgroundCssClass="modalBackground" runat="server" CancelControlID="btnuploadcancel" TargetControlID="btnupe" PopupControlID="pnupload" >
                                            </cc1:ModalPopupExtender>
                                            <asp:Panel ID="pnupload" runat="server" GroupingText="upload File" BackColor="White" style="height:100px;" >
                                            <asp:HiddenField ID="hdnupload" runat="server" />
                                            <asp:HiddenField ID="hdnAirticketFile" runat="server" />
                                            <asp:HiddenField ID="hdnuploadFileRowValue" runat="server" />
                                            <asp:FileUpload ID="flpup" runat="server" autopostback="true" /><br />
                                            <asp:Button ID="btnfileupload" runat="server" Text="Upload File" OnClick="loanuploadFile" />
                                            <asp:Button ID="btnuploadcancel" runat="server" Text="cancel" />
                                            </asp:Panel>
                                        <asp:HiddenField ID="hdnLoanAmount" runat="server" Visible="false" />
                                    </div>
                                </td>
                           </tr>
                            <tr>
                                <td>Document:
                                
                                    <asp:FileUpload ID="fudDocument1" runat="server" Width="100%"  AccessKey="1" ToolTip="[Alt+1]" Visible="false" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="DivBorderOlive1" style="margin-bottom: 20px; text-align: center; font: bold; font-size: 20px">
                                        <asp:Button ID="btnAccPost" OnClick="btnAccPost_Click" runat="server" CssClass="btn btn-success" Text="Save" Visible="false"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table id="tblEdit" runat="server" visible="false" width="100%">
                            <tr>
                                <td>
                                    <asp:DataList ID="dtlvacationEdit" runat="server" HeaderStyle-CssClass="datalistHead"
                                        Width="100%" >
                                        <ItemTemplate>
                                            <div class="DivBorderOlive" style="margin-bottom: 20px">
                                                <table style="width: 100%; background-color: #efefef;">
                                                    <tr>
                                                        <td>
                                                            <b>EmployeeName :</b>
                                                            <asp:Label ID="lblempname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>EmployeeId :</b>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>DateOfJoin :</b>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Dateofjoin")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Balance Annual Leaves:</b>
                                                            <asp:Label ID="lblAAL" runat="server" Text='<%#Eval("AvailableLeaves")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Requested Leaves :</b>
                                                            <asp:Label ID="lblRAL" runat="server" Text='<%#Eval("RequestedLeaves")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Granted Leaves :</b>
                                                            <asp:Label ID="lblGAL" runat="server" Text='<%#Eval("GrantedDays")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>OverTime Hours :</b>
                                                            <asp:Label ID="lblOT" runat="server" Text='<%#Eval("OTHours")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:GridView ID="GVVacationedit" Visible="true" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="GVVacation_RowCommand" 
                                                    DataSource='<%#BindVacationDetails(Eval("vid").ToString())%>' OnRowDataBound="GVVacation_RowDataBound" 
                                                    HeaderStyle-CssClass="tableHead" Width="100%" CssClass="gridview">
                                                    <Columns>
                                                        <asp:BoundField DataField="Description" HeaderText="Credit/Debit"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtA1" OnTextChanged="QtyChangedEdit" Style="text-align: right" AutoPostBack="true" Width="120px" Height="20px" runat="server" Text='<%#Bind("Amount")%>'></asp:TextBox>
                                                                <br />
                                                                <%--<asp:LinkButton ID="lnkEAL" Text="Master data must be supplied to populate this field" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="lnkEAL_Click"></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 17px">
                                                                    <asp:Label ID="lbl1" runat="server" Text="Total= "></asp:Label>
                                                                    <asp:Label ID="lblvalue" runat="server" Text="0.000"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkLoanSelectEdit" runat="server" Visible="false" />
                                                                <asp:LinkButton ID="hlnkView" CssClass="btn btn-primary" Style="cursor: pointer" OnClick="DocNavigateUrl"
                                                                    runat="server" Visible="false" >Proof</asp:LinkButton>
                                                                <asp:TextBox ID="txtA6" Style="text-align: right" runat="server" Width="120px" Height="20px" Text='<%#Bind("Additionals")%>'></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtA6"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Description]"></cc1:TextBoxWatermarkExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkatt_Viewk" AutoPostBack="true" Text="Attendance" Visible="false" OnClick="lnkatt_Viewk_click" CssClass="anchor__grd vw_grd" ToolTip="Click to complete attendance details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkatt_add" AutoPostBack="true" Text="Adv.Att" Visible="false" OnClick="lnkatt_add_click" CssClass="anchor__grd vw_grd" ToolTip="Click to complete advance attendance details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkALGross" AutoPostBack="true" Text="AL A/c" Visible="false" OnClick="lnkALGross_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Leave Details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkOT" AutoPostBack="true" Text="OT" Visible="false" OnClick="lnkOT_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Over Time Details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkAirTicket" AutoPostBack="true" Text="AirTicket" Visible="false" OnClick="lnkAirTicket_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Get AirTicket Details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkGratuity" AutoPostBack="true" Text="Gratuity" Visible="false" OnClick="lnkGratuity_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Get Gratuity Details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkLoans" AutoPostBack="true" Text="Loans" Visible="false" OnClick="lnkLoans_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Employee Advances"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkAbsentPenalities" AutoPostBack="true" Text="Abs Pen" Visible="false" OnClick="lnkABS_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Absent Penalities Details"></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkEmpPen" AutoPostBack="true" Text="Emp Pen" Visible="false" OnClick="lnkEmpPen_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Employee Penalities Details"></asp:LinkButton>
                                                                <asp:Button ID="btnCal" Style="text-align: right" AutoPostBack="true" Width="30px" Height="30px" runat="server" Text='Get' CssClass="btn btn-success" OnClick="btnCal_Click" Visible="false"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Details" HeaderText="Details" />
                                                    </Columns>
                                                    <FooterStyle />
                                                    <FooterStyle BackColor="#cccccc" ForeColor="Black" HorizontalAlign="left" />
                                                </asp:GridView>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:HiddenField ID="hdnLoanuploadEdit" runat="server" />
                                    <asp:HiddenField ID="hdnAirticketFileEdit" runat="server" />
                                    <asp:HiddenField ID="hdnLoanCheck" runat="server" />
                                    <asp:HiddenField ID="hdnD3LoanAmtEdit" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="DivBorderOlive1" style="margin-bottom: 20px; text-align: center; font: bold; font-size: 20px">
                                        <asp:Button ID="Button4" OnClick="btnAccPostUpdate_Click" runat="server" CssClass="btn btn-success" Text="Update" Visible="false"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div id="dvadvatt" runat="server">
                <asp:GridView ID="gvAdvAttendance" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                    OnRowCommand="gvAdvAttendance_RowCommand" OnRowDataBound="gvAdvAttendance_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Empid" DataField="Empid" Visible="false" />
                        <asp:BoundField HeaderText="EmpName" DataField="name" Visible="true" />
                        <asp:TemplateField HeaderText="1" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance1" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="2" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance2" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="3" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance3" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="4" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance4" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance5" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="6" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance6" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance7" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="8" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance8" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="9" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance9" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance10" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="11" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance11" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="12" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance12" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="13" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance13" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="14" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance14" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="15" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance15" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="16" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance16" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="17" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance17" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="18" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance18" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="19" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance19" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="20" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance20" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="21" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance21" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="22" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance22" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="23" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance23" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="24" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance24" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="25" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance25" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="26" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance26" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="27" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance27" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="28" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance28" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="29" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance29" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="30" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="ddlAttendance30" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="31" Visible="false">
                            <ItemStyle Width="50" />
                            <ItemTemplate>
                                <asp:DropDownList Width="50" ID="DropDownList31" CssClass="droplist" runat="server" DataTextField="ShortName"
                                    DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkchange" runat="server" CommandName="UPD"
                                    Text="Save" CommandArgument='<%#Bind("EmpId")%>' CssClass="btn btn-primary"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-success" OnClick="btnClose_Click" />
            </div>
            <div>
                <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                    CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="369px" Width="600px" Style="display: none" CssClass="box box-primary">
                    <table>
                        <tr>
                            <td>
                                <h1><b>Expat Documentation Expenses</b></h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvExpactdetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                    HeaderStyle-CssClass="tableHead" CssClass="gridview" GridLines="Both" OnRowDataBound="gvExpactdetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkESelectAll" Text="All" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEToTransfer" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SRN ID" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSEmpId" runat="server" Text='<%#Eval("SRNID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource Name" HeaderStyle-Width="160px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("ResourceName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From_Date" HeaderStyle-Width="160px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("From_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To_Date" HeaderStyle-Width="160px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo" runat="server" Text='<%#Eval("To_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="160px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("expCut")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Paging ID="GrtPaging" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel7" runat="server" CssClass="box box-primary" Style="border-style: groove; background-color: #ffd800; display: none; width: 50%">
                                    <div style="background-color: #0094ff; width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="left">Complete Attendance Detail(s)  &nbsp;
                                                    <asp:Label runat="server" ID="lblmonth"></asp:Label>
                                                    :</td>
                                                <td align="right">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Close" ImageUrl="~/Images/close.png" Width="20px" Height="20px" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <center>
                                        <div class="rounded_corners">
                                            <table align="left" width="100%" style="background-color: white; margin: 2px 2px 2px 2px">
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:GridView ID="grdattendnacestatus" runat="server" CssClass="gridview" AutoGenerateColumns="false" ForeColor="#333333">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label12" runat="server" Text='<%#Eval("Leavenm")%>' Width="250px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="#Days" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label13" runat="server" Text='<%#Eval("att_count")%>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payble?" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%#Eval("pay_sts")%>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                        <span><b>#Total Count:</b> </span>
                                                        <asp:Label runat="server" ID="lblcountatt"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: green"> <b>#Payable Days:</b> </span>
                                                        <asp:Label runat="server" ID="lblpayD"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: red"> <b>#Non-Payable Days:</b> </span>
                                                        <asp:Label runat="server" ID="lblNonD"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; border: none">
                                                        <img src="../Images/HandWarn.ico" title="Message" width="32px" height="32px" /></td>
                                                    <td style="vertical-align: middle">
                                                        <asp:Table ID="tblAtt_1" Width="100%" runat="server" BorderWidth="2" GridLines="Both"></asp:Table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" runat="server" id="pnlNonHoliday1">
                                                        <br />
                                                        <span style="font-weight: bold; font-family: Verdana; font-size: 10px">
                                                            <ul>Non-Payable Holidays [From Holidays Rules] Applied Details: </ul>
                                                        </span></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;" runat="server" id="pnlNonHoliday2">
                                                        <img src="../Images/HandWarn.ico" title="Message" width="32px" height="32px" /></td>
                                                    <td style="vertical-align: middle" runat="server" id="pnlNonHoliday">
                                                        <asp:GridView ID="grd_nonPayRules" runat="server" CssClass="gridview"  AutoGenerateColumns="false" ForeColor="#333333">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="On Date" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label16" runat="server" Text='<%#Eval("date")%>' Width="250px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rule Name" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label17" runat="server" Text='<%#Eval("rulename")%>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Found Combination" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label18" runat="server" Text='<%#Eval("comb")%>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <span style="font-weight: bold; font-family: Verdana; font-size: 10px">[ After Holidays Rules:]</span>  <span style="color: green"><b>#Payable Days:</b> </span>
                                                        <asp:Label runat="server" ID="lblpayDH"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<span style="color: red"> <b>#Non-Payable Days:</b> </span>
                                                        <asp:Label runat="server" ID="lblnonpayDH"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;" runat="server" id="pnlNonHoliday4">
                                                        <span style="font-weight: bold"><b>Note:</b></span></td>
                                                    <td runat="server" id="pnlNonHoliday3"><span style="font-style: italic; background-color: yellow; font-size: 9pt">To understand Holiday Rules All Holidays natures[ <span style="font-weight: bold">eg.</span>WO,PH,CL,EL] may be considered as <span style="font-weight: bold">Absent</span> [if any Non-Payable Holiday Rules will be applied!].<span style="font-weight: bold"> To know more click.. <a title="Leave & Vacation -> Configurations -> Holiday Rules -> View [QAID:161]" href="HolidayPaidRules.aspx" target="_blank">HOLIDAYS RULES </a></span></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </center>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnUpdate" CommandName="Update" runat="server" CssClass="btn btn-success" Text="Deduct" OnClick="btnUpdate_Click" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" />
                </asp:Panel>
                <asp:LinkButton ID="lnkbtn" runat="server" Style="display: none" />
                <asp:Panel ID="Panel1" runat="server" BackColor="White" Height="369px" Width="600px" Style="display: none" TargetControlID="btnShowPopup">
                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="lnkbtn" PopupControlID="Panel7"
                        CancelControlID="ImageButton1" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID = "btnfileupload" /> 
            <asp:PostBackTrigger ControlID = "btnAccPost" />
   </Triggers>
    </asp:UpdatePanel>
   <%-- <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="updpnl">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
