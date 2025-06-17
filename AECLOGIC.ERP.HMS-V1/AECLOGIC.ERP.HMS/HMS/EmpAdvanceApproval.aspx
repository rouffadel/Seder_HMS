<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="EmpAdvanceApproval.aspx.cs"
    Inherits="AECLOGIC.ERP.HMSV1.EmpAdvanceApprovalV1" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.grdApproval.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.grdApproval.ClientID %>');
            var TargetChildControl = "chkSelect";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
         }
         function GetID(source, eventArgs) {
             var HdnKey = eventArgs.get_value();
             document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;        
           <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
         }
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDept_hid.ClientID %>').value = HdnKey;
             <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }

    </script>



    <table visible="false" id="tblMain" runat="server" width="100%">
        <tr>
            <td colspan="2" class="pageheader" style="height: 21px">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Select Employee</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                <asp:TextBox ID="TxtEmpy" OnTextChanged="GetEmpList" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                    MinimumPrefixLength="1" ServiceMethod="GetCompletionEmpList" ServicePath="" TargetControlID="TxtEmpy"
                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                </cc1:AutoCompleteExtender>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="TxtEmpy"
                    WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>



                &nbsp;<asp:TextBox ID="txtFilter" runat="server" TabIndex="2"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFilter"
                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]"></cc1:TextBoxWatermarkExtender>
                &nbsp;&nbsp;
                        <asp:Button ID="btnFind" runat="server" ToolTip="Use Filter For Instant Search" Text="Search"
                            CssClass="btn btn-primary" OnClick="btnFind_Click" AccessKey="i" TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td>
                <b>Employee Salary</b>
            </td>
            <td>

                <asp:TextBox ID="txtSal" runat="server" ReadOnly="True" TabIndex="4"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBSal" runat="server" TargetControlID="txtSal" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;
                        * /Month
            </td>
        </tr>
        <tr>
            <td>
                <b>Select Type</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlAdjType" runat="server" AutoPostBack="True" CssClass="droplist"
                    OnSelectedIndexChanged="ddlAdjType_SelectedIndexChanged" TabIndex="5">
                    <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Advance" Value="1"></asp:ListItem>
                </asp:DropDownList>
                &nbsp; * Advance/Loan
            </td>
        </tr>
        <tr>
            <td>
                <b>&nbsp;</b>
            </td>
            <td></td>
        </tr>
    </table>

    <table id="tblAdvance" runat="server" visible="false" width="100%">
        <tr>
            <td style="width: 180Px; height: 22px;">
                <b>Advance Amount</b>
            </td>
            <td style="height: 22px">
                <asp:TextBox ID="txtAdvAmt" runat="server" TabIndex="6"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBAdvAmt" runat="server" TargetControlID="txtAdvAmt" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                <cc1:TextBoxWatermarkExtender
                    ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtAdvAmt" WatermarkCssClass="Watermarktxtbox"
                    WatermarkText="[Enter Advance]"></cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td>
                <b>Recovery Type</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlRecoverType" CssClass="droplist" AutoPostBack="true" runat="server"
                    OnSelectedIndexChanged="ddlRecoverType_SelectedIndexChanged" TabIndex="7">
                    <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Instant" Value="1"></asp:ListItem>
                    <asp:ListItem Text="EMIs" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px; height: 23px;">
                <b>No of Recovery Months</b>
            </td>
            <td style="height: 23px">
                <asp:TextBox ID="txtAdvNM" runat="server" AutoPostBack="True" OnTextChanged="txtAdvNM_TextChanged"
                    TabIndex="8"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4"
                        runat="server" TargetControlID="txtAdvNM" WatermarkCssClass="Watermarktxtbox"
                        WatermarkText="[Enter EMI Months]"></cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Installment Amount</b>
            </td>
            <td>
                <asp:TextBox ID="txtAdvInstAmt" Enabled="true" runat="server" ReadOnly="True" TabIndex="9"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBAdvInstAmt" runat="server" TargetControlID="txtAdvInstAmt" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;*
                        /Month
            </td>
        </tr>
        <tr>
            <td>
                <b>Amount RequireOn</b>
            </td>
            <td>
                <asp:TextBox ID="txtAPaidOn" runat="server" TabIndex="10"></asp:TextBox><cc1:CalendarExtender
                    ID="CalendarExtender1" runat="server" TargetControlID="txtAPaidOn" PopupButtonID="txtAPaidOn"
                    Format="dd/MM/yyyy"></cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Recovery Start Month</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlMonth" CssClass="droplist" runat="server" TabIndex="11">
                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
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
                <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Year:</b>&nbsp;
                        <asp:DropDownList ID="ddlYear" CssClass="droplist" runat="server" TabIndex="12">
                        </asp:DropDownList>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Gross Salary</b>
            </td>
            <td>
                <asp:TextBox ID="txtAdvGSal" runat="server" ReadOnly="True" TabIndex="13"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBAdvGSal" runat="server" TargetControlID="txtAdvGSal" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;
                        * During Recovery Months
            </td>
        </tr>
        <tr>
            <td style="width: 180Px; vertical-align: middle">
                <b>Remarks</b>
            </td>
            <td>
                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table id="tblASave" visible="false" runat="server" width="100%">
        <tr>
            <td style="width: 180Px"></td>
            <td>
                <asp:Button ID="btnAdvSave" ToolTip="Save Advance" CssClass="btn btn-success" runat="server"
                    Text="Save" OnClick="btnAdvSave_Click" Height="21px" OnClientClick="javascript:return CheckAdvances();"
                    AccessKey="s" TabIndex="14" />
                &nbsp;&nbsp;
                        <asp:Button ID="btnAdvCancel" runat="server" CssClass="btn btn-danger" Text="Cancel"
                            OnClick="btnAdvCancel_Click" AccessKey="b" TabIndex="15" ToolTip="[Alt+b OR Alt+b+Enter]" />
            </td>
        </tr>
    </table>

    <table id="tblLoan" runat="server" width="100%" visible="false">
        <tr>
            <td>
                <b>Loan Type</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlLoanRecoveryType" runat="server" CssClass="droplist" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlLoanRecoveryType_SelectedIndexChanged" TabIndex="16">
                    <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Flat" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Reducing" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Loan Amount(Rs)</b>
            </td>
            <td>
                <asp:TextBox ID="txtLAmt" runat="server" Height="18px" TabIndex="17"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBLAmt" runat="server" TargetControlID="txtLAmt" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                <cc1:TextBoxWatermarkExtender
                    ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtLAmt" WatermarkCssClass="Watermarktxtbox"
                    WatermarkText="[Enter Loan Amount]"></cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>No of Recovery Months</b>
            </td>
            <td>
                <asp:TextBox ID="txtLRecoveryMonths" runat="server" OnTextChanged="txtLRecoveryMonths_TextChanged"
                    TabIndex="18"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBLRecoveryMonths" runat="server" TargetControlID="txtLRecoveryMonths" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7"
                    runat="server" TargetControlID="txtLRecoveryMonths" WatermarkCssClass="Watermarktxtbox"
                    WatermarkText="[Enter EMI Months]"></cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>ServiceTax On Interest(%)</b>
            </td>
            <td>
                <asp:TextBox ID="txtST" runat="server" TabIndex="19"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBST" runat="server" TargetControlID="txtST" FilterType="Numbers,Custom" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;* *
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Rate of Interest(%)</b>
            </td>
            <td>
                <asp:TextBox ID="txtLRI" runat="server" AutoPostBack="True" OnTextChanged="txtLRI_TextChanged"
                    Height="18px" TabIndex="20"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBLRI" runat="server" TargetControlID="txtLRI" FilterType="Numbers,Custom" ValidChars="."></cc1:FilteredTextBoxExtender>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8"
                    runat="server" TargetControlID="txtLRI" WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter Interest]"></cc1:TextBoxWatermarkExtender>
                &nbsp;* *
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Intrest+ServiceTax</b>
            </td>
            <td>
                <asp:TextBox ID="txtLIns" runat="server" Height="18px" TabIndex="21"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBLins" runat="server" TargetControlID="txtLIns" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;*/Month
            </td>
        </tr>
        <tr>
            <td style="width: 180Px; height: 13px;">
                <b>Installment Amount(Rs)</b>
            </td>
            <td style="height: 13px">
                <asp:TextBox ID="txtLIAmt" runat="server" TabIndex="22"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBLIAmt" runat="server" TargetControlID="txtLIAmt" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                * Monthly
            </td>
        </tr>
        <tr>
            <td>
                <b>Amount PaidOn</b>
            </td>
            <td>
                <asp:TextBox ID="txtLPaidOn" runat="server" TabIndex="23"></asp:TextBox><cc1:CalendarExtender
                    ID="CalendarExtender2" runat="server" TargetControlID="txtLPaidOn" PopupButtonID="txtLPaidOn"
                    Format="dd/MM/yyyy"></cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Recovery Start Month</b>
            </td>
            <td>&nbsp;<asp:DropDownList ID="ddlCurrentMonth" CssClass="droplist" runat="server" TabIndex="24">
                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
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
                <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Year:</b>&nbsp;
                        <asp:DropDownList ID="ddlCurrentYear" CssClass="droplist" runat="server" TabIndex="25">
                        </asp:DropDownList>
                &nbsp;&nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Gross Salary(Rs)</b>
            </td>
            <td>
                <asp:TextBox ID="txtLGrossSal" runat="server" Height="18px" ReadOnly="True" TabIndex="26"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBLGrossSal" runat="server" TargetControlID="txtLGrossSal" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;* During Recovery&nbsp; Months
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b></b>
            </td>
            <td></td>
        </tr>
    </table>
    <table id="tblLsave" visible="false" runat="server" width="100%">
        <tr>
            <td style="width: 180Px">
                <b></b>
            </td>
            <td>
                <asp:Button ID="btnLSave" CssClass="btn btn-success" ToolTip="Save Flat Loan" runat="server"
                    Text="Save" OnClick="btnLSave_Click" TabIndex="27" />
                &nbsp;&nbsp;
                        <asp:Button ID="btnLCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnLCancel_Click"
                            TabIndex="28" />
                &nbsp; ** Default Options, use
                        <asp:HyperLink ID="hlnkOptions" ToolTip="Click to get Options page" ForeColor="Blue"
                            NavigateUrl="~/Options.aspx" runat="server" CssClass="btn btn-primary">Options</asp:HyperLink>
                &nbsp;page if required to change.
            </td>
        </tr>
    </table>

    <table id="tblReduce" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <b>Loan Type</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlReduce" CssClass="droplist" runat="server" TabIndex="29">
                    <asp:ListItem Text="Reducing" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Loan Amount(Rs)</b>
            </td>
            <td>
                <asp:TextBox ID="txtReduceLoanAmount" runat="server" TabIndex="30"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBReduceLoanAmount" runat="server" TargetControlID="txtReduceLoanAmount" FilterType="Custom,Numbers"></cc1:FilteredTextBoxExtender>
                <cc1:TextBoxWatermarkExtender
                    ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtReduceLoanAmount"
                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter Loan Amount]"></cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>No of Recovery Months</b>
            </td>
            <td>
                <asp:TextBox ID="txtReduceEMIs" runat="server" TabIndex="31"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBReduceEMIs" runat="server" TargetControlID="txtReduceEMIs" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                <cc1:TextBoxWatermarkExtender
                    ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtReduceEMIs"
                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter EMI Months]"></cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Rate of Interest(%)</b>
            </td>
            <td>
                <asp:TextBox ID="txtReduceRI" runat="server" Height="18px" TabIndex="32"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBReduceRI" runat="server" TargetControlID="txtReduceRI" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;*
                        *
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>ServiceTax On Interest(%)</b>
            </td>
            <td>
                <asp:TextBox ID="txtReduceST" runat="server" TabIndex="33"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FTBReduceST" runat="server" TargetControlID="txtReduceST" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                &nbsp;*
                        *
            </td>
        </tr>
        <tr>
            <td>
                <b>Amount PaidOn</b>
            </td>
            <td>
                <asp:TextBox ID="txtTAPaidOn" runat="server" TabIndex="34"></asp:TextBox><cc1:CalendarExtender
                    ID="CalendarExtender3" runat="server" TargetControlID="txtTAPaidOn" PopupButtonID="txtTAPaidOn"
                    Format="dd/MM/yyyy"></cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b>Recovery Start Month</b>
            </td>
            <td>&nbsp;<asp:DropDownList ID="ddlRecoveryMonths" CssClass="droplist" runat="server"
                TabIndex="35">
                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
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
                <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Year:</b>&nbsp;
                        <asp:DropDownList ID="ddlReducingYear" CssClass="droplist" runat="server" TabIndex="36">
                        </asp:DropDownList>
                &nbsp;&nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 180Px">
                <b></b>
            </td>
            <td></td>
        </tr>
    </table>
    <table id="tblReduceSave" runat="server" visible="false" width="100%">
        <tr>
            <td style="width: 180Px"></td>
            <td>
                <asp:Button ID="btnReduceSave" runat="server" ToolTip="Save Reduced Loan" CssClass="btn btn-success"
                    Text="Save" OnClick="btnReduceSave_Click" TabIndex="37" />&nbsp;
                        <asp:Button ID="btnReduceCancel" runat="server" CssClass="btn btn-danger" Text="Cancel"
                            TabIndex="38" />&nbsp; ** Default Options, use
                        <asp:HyperLink ID="hlnkOptions2" ToolTip="Click to get Options page" ForeColor="Blue"
                            runat="server" NavigateUrl="~/Options.aspx" CssClass="btn btn-primary">Options</asp:HyperLink>
                &nbsp;page if required to change.
            </td>
        </tr>
    </table>

    <table id="tblApproval" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <b>WorkSite:</b><asp:DropDownList ID="ddlWs" Visible="false" CssClass="droplist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWs_SelectedIndexChanged" TabIndex="39"
                                                AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:HiddenField ID="ddlWs_hid" runat="server" />
                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                            <b>Department:</b><asp:DropDownList ID="ddlDept" Visible="false" CssClass="droplist" runat="server"
                                                TabIndex="40" AccessKey="1" ToolTip="[Alt+1]">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="ddlDept_hid" runat="server" />
                                            <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                            &nbsp;
                                            <asp:Label ID="lblstatus" runat="server" Text="Status"></asp:Label>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlStatus" runat="server">
                                                <asp:ListItem Text="Pending" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Name:</b><asp:TextBox ID="txtEmpName" runat="server" AutoPostBack="true" TabIndex="41" AccessKey="2"
                                                ToolTip="[Alt+2]"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9"
                                                    runat="server" TargetControlID="txtEmpName" WatermarkCssClass="Watermarktxtbox"
                                                    WatermarkText="[Filter Name]"></cc1:TextBoxWatermarkExtender>
                                            <b>&nbsp;EmpID:</b><asp:TextBox Width="60" ID="txtEmpID" TabIndex="42" runat="server"
                                                AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"
                                                TabIndex="43" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />

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
                <asp:GridView ID="grdApproval" runat="server" AutoGenerateColumns="false" ToolTip="gvApproval"
                    HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                    CssClass="gridview" OnRowCommand="grdApproval_RowCommand" OnRowEditing="grdApproval_RowEditing" OnRowDataBound="gvApproval_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LoanID">
                            <ItemTemplate>
                                <asp:Label ID="lblLoanID" runat="server" Text='<%# Eval("LoanID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="LoanID" HeaderText="LoanID" />--%>

                        <asp:BoundField DataField="EmpID" HeaderText="EmpID" Visible="false" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                       <%-- <asp:BoundField DataField="LoanCategory" HeaderText="Category" /> --%>
                        <asp:BoundField DataField="LoanAmount" HeaderText="LoanAmount" />




                        <asp:BoundField DataField="DueAmount" HeaderText="DueAmount" />
                        <asp:BoundField DataField="AmtReqOn" HeaderText="Amount Req On" />
                        <asp:BoundField DataField="RecmndBy" HeaderText="Recommended By" />
                        <asp:BoundField DataField="NoofEMIs" ItemStyle-HorizontalAlign="Center" HeaderText="NoofEMIs" />

                        <asp:TemplateField HeaderText="RecoveryFrom">
                            <ItemTemplate>
                                <asp:Label ID="lblRecoveryStartFrom" Text='<%#FormatMonth(Eval("RecoveryStartFrom"))%>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="RecoveryYear" HeaderText="Year" />

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"  Width="145px" Text='<%#Eval("Remarks")%>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="145px" />
                            <ItemStyle Width="145px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="EMI">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlnkView" NavigateUrl="#" OnClick='<%#ViewDetails(Eval("LoanID").ToString()) %>'
                                    runat="server" CssClass="anchor__grd vw_grd">View</asp:HyperLink>
<asp:HyperLink ID="lnkInd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LoanID") %>' CssClass="btn btn-danger link__cust__style"
 				NavigateUrl='<%# String.Format("ViewEmpLoanDetails.aspx?LoanId={0}", DataBinder.Eval(Container.DataItem,"LoanID").ToString()) %>' Target="_blank" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnlEdit" CommandName="Edit" CommandArgument='<%#Eval("LoanID")%>'
                                    runat="server" CssClass="anchor__grd edit_grd">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkApprove" CommandName="Approve" CommandArgument='<%#Eval("LoanID")%>'
                                    runat="server" Text="Confirmation Approval" CssClass="btn btn-primary"> </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" CommandName="Reject" CommandArgument='<%#Eval("LoanID")%>'
                                    runat="server" Text="Reject" CssClass="btn btn-danger"> </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" CommandName="Del" CommandArgument='<%#Eval("LoanID")%>'
                                    runat="server"  Text="Delete"   CssClass="anchor__grd dlt" > </asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Proof">
                            <ItemTemplate>
                                <a id="A6" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "LoanID").ToString()) %>'
                                    runat="server" class="btn btn-primary">View</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Remarks" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" CommandName="view" CssClass="btn btn-primary" CommandArgument='<%#Eval("LoanID")%>'
                                    runat="server">View</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemStyle Width="25px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="HR Proof" HeaderStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                <asp:LinkButton ID="lnkHRext" runat="server" CommandName="HRext" ToolTip="View"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"LoanID")  %>' CssClass="btn btn-primary"
                                    
                                    Visible='<%#HRVisible(DataBinder.Eval(Container.DataItem,"HRext").ToString())%>'>
                                                    <img src="../images/Search-icon16.png" alt="View" />
                                    </asp:LinkButton>
                                <asp:Label ID="lblHRext" runat="server" Text='<%# Eval("HRext") %>' Visible="false"></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="EmpListPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnapprove" runat="server" CssClass="btn btn-success" OnClick="btnapprove_Click" Text="Approve All" Visible="false" Enabled="False"/>
            </td>
        </tr>


    </table>
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
</asp:Content>

