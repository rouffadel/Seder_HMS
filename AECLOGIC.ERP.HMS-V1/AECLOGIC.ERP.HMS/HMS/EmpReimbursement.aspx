<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="EmpReimbursement.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpReimbursement" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Label runat="server" ID="Label1" Text="" Font-Size="14px"></asp:Label>
            <style>
                .MyCalendar .ajax__calendar_container {
                    background-color: White;
                    color: black;
                    border: 1px solid #646464;
                }

                    .MyCalendar .ajax__calendar_container td {
                        background-color: White;
                        padding: 0px;
                    }
            </style>
            <script language="javascript" type="text/javascript">
                function SelectAll(hChkBox, grid, tCtrl) { var oGrid = document.getElementById(grid); var IPs = oGrid.getElementsByTagName("input"); for (var iCount = 0; iCount < IPs.length; ++iCount) { if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked; } }
                function CheckItem(ctrl, ID) {
                    var ResultVal = AjaxDAL.HR_IsVerified(ctrl.checked, ID);
                    if (ctrl.checked == true) {
                        alert("Checked");
                    }
                    else {
                        alert("UnChecked");
                    }
                }
                function Multiplying(contorl) {
                    if (checkdecmial(contorl) == false) {
                        contorl.focus();
                        return false;
                    }
                    var gridFields = document.getElementById("<%=gvEdit.ClientID %>");
                    if (gridFields != null) {
                        var totalRows = gridFields.rows.length;
                        var rowIndex;
                        var Qty, Rate;
                        for (rowIndex = 1; rowIndex < totalRows; rowIndex++) {
                            Qty = gridFields.rows[rowIndex].cells(4).children[0];
                            Rate = gridFields.rows[rowIndex].cells(5).children[0];
                            var tot = (Qty.value * Rate.value)
                            tot = Math.round(tot * 100) / 100;
                            gridFields.rows[rowIndex].cells(6).innerText = tot;
                        }
                    }
                }
                function CheckUnitSelect1() {
                    var Obj = document.getElementById('<%=gvEdit.ClientID%>');
            for (i = 1; i < Obj.rows.length; i++) {
                for (j = 0; j < Obj.rows[i].cells[3].childNodes.length; j++) {
                    if (Obj.rows[i].cells[3].childNodes[j].value == "0") {
                        alert("Select Unit of Measure!");
                        Obj.rows[i].cells[3].childNodes[j].focus(); return false;
                    }
                }
            }
            for (i = 1; i < Obj.rows.length; i++) {
                for (j = 0; j < Obj.rows[i].cells[4].childNodes.length; j++) {
                    if (Obj.rows[i].cells[4].childNodes[j].value <= "") {
                        alert("Please enter Unitrate!");
                        Obj.rows[i].cells[4].childNodes[j].focus();
                        return false;
                    }
                    if (Obj.rows[i].cells[4].childNodes[j].value <= "00000000000") {
                        alert("Please enter proper Unitrate!");
                        Obj.rows[i].cells[4].childNodes[j].focus();
                        return false;
                    }
                }
            }
            for (i = 1; i < Obj.rows.length; i++) {
                for (j = 0; j < Obj.rows[i].cells[5].childNodes.length; j++) {
                    if (Obj.rows[i].cells[5].childNodes[j].value == "") {
                        alert("Please enter Quantity!");
                        Obj.rows[i].cells[5].childNodes[j].focus();
                        return false;
                    }
                    if (Obj.rows[i].cells[5].childNodes[j].value <= "00000000000") {
                        alert("Please enter proper Quantity!");
                        Obj.rows[i].cells[5].childNodes[j].focus();
                        return false;
                    }
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
        }
        function GETEmp_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
        }
        //chaitanya:for validation below code
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
        function GetEMPNameID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=emp_hid.ClientID %>').value = HdnKey;
        }
            </script>
            <table id="tblAdd" visible="false" runat="server" width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                            Height="106px" Width="100%">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <b>WorkSite:</b>
                                                    <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                    <asp:TextBox ID="TxtWorksite" OnTextChanged="GetWorksite" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionWorksiteList" ServicePath="" TargetControlID="TxtWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="TxtWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    <b>Department:</b>
                                                    <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                    <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtdepartment"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                    <b>Employee:</b><asp:DropDownList ID="ddlEmp" Visible="false" runat="server" AutoPostBack="True"
                                                        CssClass="droplist" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                                                        PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                        TargetControlID="ddlEmp" />
                                                    <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                                                    <asp:TextBox ID="TxtEmp" OnTextChanged="GetEmployelist" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="TxtEmp"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="TxtEmp"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                                    <asp:TextBox ID="txtFilter" Visible="false" runat="server" CssClass="droplist" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFilter"
                                                        WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]"></cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="txtSearch" Visible="true" runat="server" ToolTip="Use Filter for Instant Search"
                                                        CssClass="btn btn-primary" OnClick="txtSearch_Click" Text="Search" TabIndex="5" AccessKey="4" />
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
                    <td colspan="2" style="height: 21px"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <b>Reimburse Items</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListBox ID="lstItems" Rows="6" Width="300Px" runat="server" CssClass="box box-primary" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td style="padding-left: 160Px">
                        <asp:Button ID="btnSave" ToolTip="Add multiple Items at once" CssClass="btn btn-success"
                            runat="server" Text="Add" OnClick="btnSave_Click" AccessKey="s" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvRemiItems" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            CssClass="gridview"
                            Width="100%" OnRowCommand="gvRemiItems_RowCommand">
                            <Columns>
                                <asp:TemplateField Visible="false" HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" Text='<%#Eval("ID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80Px" HeaderText="ReimburseItem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units of Measure">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' DataTextField="AU_Name"
                                            DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                            runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" Text='<%#Eval("UnitRate") %>' OnTextChanged="txtUnitrete_TextChanged"
                                            AutoPostBack="true" Width="40" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl5" runat="server" TargetControlID="txtRate" ValidChars="." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("Qty") %>' Style="text-align: right;"
                                            OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" Width="40"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl6" runat="server" TargetControlID="txtQty" ValidChars="." />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Purpose">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPurpose" runat="server" Height="50" Text='<%# DataBinder.Eval(Container.DataItem,"Purpose") %>'
                                            TextMode="MultiLine" Width="200"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1"
                                                runat="server" TargetControlID="txtPurpose" WatermarkCssClass="Watermarktxtbox"
                                                WatermarkText="[Enter Purpose Here..]"></cc1:TextBoxWatermarkExtender>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spent On">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSpentOn" Text='<%#Eval("DateOfSpent")%>' runat="server" Width="70" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtSpentOn" CssClass="MyCalendar"
                                            OnClientDateSelectionChanged="checkDate" TargetControlID="txtSpentOn" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                        <%-- <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                    ID="Fl7" runat="server" TargetControlID="txtSpentOn" ValidChars="/" />--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proof">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="UploadProof" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnlDelete" CommandName="Del" CssClass="anchor__grd dlt" CommandArgument='<%#Eval("ID")%>'
                                            runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 200Px">
                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" OnClientClick="javascript:return CheckUnitSelect();"
                            CssClass="btn btn-primary" Text="Save" />
                    </td>
                </tr>
            </table>
            <table id="tblView" runat="server" width="100%" visible="false">
                <tr>
                    <td>
                        <cc1:Accordion ID="gvViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="106px" Width="100%">
                            <Panes>
                                <cc1:AccordionPane ID="gvViewAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>Employee Name:</b>
                                                    <asp:HiddenField ID="emp_hid" runat="server" />
                                                    <asp:TextBox ID="txtSearchemp" OnTextChanged="GetEmp" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionFilterEmpList" ServicePath="" TargetControlID="txtSearchemp"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEMPNameID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSearchemp"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="btnFilter" CssClass="btn btn-primary" runat="server" Text="Search" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="btnFilter_Click" TabIndex="4" />
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
                        <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvView_RowCommand"
                            OnRowDataBound="gvView_RowDataBound"
                            CssClass="gridview">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproval" AutoPostBack="true" OnCheckedChanged="chkApproval_CheckedChanged"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRSelect" AutoPostBack="true" OnCheckedChanged="chkRSelect_CheckedChanged"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd " CommandArgument='<%#Eval("EmpID") %>' CommandName="Edt"
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementPendingRejPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <asp:HiddenField ID="hdnSearchChangePendingRej" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblViewApproved" runat="server" width="100%" visible="false">
                <tr>
                    <td>
                        <cc1:Accordion ID="gvViewApprovedAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="106px" Width="100%">
                            <Panes>
                                <cc1:AccordionPane ID="gvViewApprovedAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>Employee Name:</b>&nbsp;&nbsp;
&nbsp;                                           
                                            <asp:DropDownList ID="ddlTransfer" AutoPostBack="true" Visible="false" runat="server" OnSelectedIndexChanged="ddlTransfer_SelectedIndexChanged"
                                                CssClass="droplist" AccessKey="1" ToolTip="[Alt+1]" TabIndex="1">
                                            </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender3" IsSorted="true" PromptText="Type Here To Search..."
                                                        PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                        TargetControlID="ddlTransfer" />
                                                    <asp:TextBox ID="txtSearchEmpTransfer" OnTextChanged="GetTransferEmp" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionEmpTransferList" ServicePath="" TargetControlID="txtSearchEmpTransfer"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtSearchEmpTransfer"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp;&nbsp;
                                            <b>EmpID:</b>
                                                    &nbsp;<asp:TextBox ID="txtTransferEmpID" runat="server" CssClass="droplist" AccessKey="3" ToolTip="[Alt+3]" TabIndex="3" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBTransferEmpID" runat="server" TargetControlID="txtTransferEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtTransferEmpID"
                                                        WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter ByEmpID]"></cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="btnTranfer" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnTranfer_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="4" />
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
                        <asp:GridView ID="gvViewApproved" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvViewApproved_RowCommand" CssClass="gridview"
                            OnRowDataBound="gvViewApproved_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkToTransfer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ERID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblERID" Text='<%#Eval("ERID") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ApprovedBy" DataField="ApprovedBy" />
                                <asp:BoundField HeaderText="Approved On" DataField="ApprovedOn" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkView" CssClass="anchor__grd vw_grd" NavigateUrl="#" OnClick='<%#ViewItemsNavigateUrl(DataBinder.Eval(Container.DataItem, "ERID").ToString())%>'
                                            runat="server">View</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementAprovedPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5Px">
                        <asp:Button ID="btnTransferAcc" CssClass="btn btn-primary" ToolTip="Transfer To Accounts Dept."
                            runat="server" Text="A/C Posting" OnClick="btnTransferAcc_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <asp:HiddenField ID="hdnSearchChangeAproved" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblEdit" runat="server" visible="false" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvEdit" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="100%" OnRowCommand="gvEdit_RowCommand" CssClass="gridview">
                            <Columns>
                                <asp:TemplateField Visible="false" HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" Text='<%#Eval("ID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SL.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80Px" HeaderText="ReimburseItem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units of Measure">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' DataTextField="AU_Name"
                                            DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                            runat="server" AutoPostBack="false" CssClass="droplist">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" Text='<%#Eval("UnitRate") %>'
                                            OnTextChanged="txtUnitreteEdit_TextChanged" AutoPostBack="true" Width="40" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl2" runat="server" TargetControlID="txtRate" ValidChars="." />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("Qty") %>' Style="text-align: right;"
                                            Width="40" OnTextChanged="txtQuantityEdit_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl3" runat="server" TargetControlID="txtQty" ValidChars="." />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Purpose">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPurpose" runat="server" Height="50" Text='<%# DataBinder.Eval(Container.DataItem,"Purpose") %>'
                                            TextMode="MultiLine" Width="200"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1"
                                                runat="server" TargetControlID="txtPurpose" WatermarkCssClass="Watermarktxtbox"
                                                WatermarkText="[Enter Purpose Here..]"></cc1:TextBoxWatermarkExtender>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spent On">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSpentOn" Text='<%#Eval("DateOfSpent")%>' runat="server" Width="70"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtSpentOn"
                                            TargetControlID="txtSpentOn" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl4" runat="server" TargetControlID="txtSpentOn" ValidChars="/" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proof">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="UploadProof" Enabled="true" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 300Px">
                        <asp:Button ID="BtnEditSave" CssClass="btn btn-success" ToolTip="Save Changes" runat="server"
                            Text="Save" OnClick="BtnEditSave_Click" AccessKey="i" />&nbsp;
                    </td>
                </tr>
            </table>
            <table id="tblTransfered" visible="false" runat="server" width="100%">
                <tr>
                    <td class="pageheader">Transfered Amount To Accounts Dept
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvTransfered" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvTransfered_RowCommand" CssClass="gridview">
                            <Columns>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTASelect" AutoPostBack="true" OnCheckedChanged="chkTASelect_CheckedChanged"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ERID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblERID" runat="server" Text='<%#Eval("ERID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="TransID" DataField="TransID" />
                                <asp:BoundField HeaderText="Trans Date" DataField="TransDate" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Transfered On" DataField="ApprovedOn" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementTransferdPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <asp:HiddenField ID="hdnSearchChangeTransAmt" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblShow" runat="server" width="100%" visible="false">
                <tr>
                    <td class="pageheader">Itmes View
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            OnRowDataBound="gvShow_RowDataBound" Width="100%" CssClass="gridview"
                            EmptyDataText="No Records Found" OnRowCommand="gvShow_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" Visible="true" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelectOne" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item_No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPK" runat="server" Text='<%#Eval("ERItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RItem" HeaderText="ReimburseItem" />
                                <asp:TemplateField HeaderText="Units of Measure">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' Enabled="false"
                                            DataTextField="AU_Name" DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                            runat="server" AutoPostBack="false" CssClass="droplist">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnitRate" HeaderText="Unit Rate" />
                                <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField HeaderStyle-Width="140Px" DataField="Purpose" HeaderText="Purpose" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DateOfSpent" HeaderText="DateOfSpent" />
                                <asp:BoundField DataField="DOS" HeaderText="DOS" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblERID" runat="server" Text='<%#Eval("ERID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkView" CssClass="btn btn-primary" Style="cursor: pointer" onclick='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                            runat="server">Proof</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd " CommandName="Edt" CommandArgument='<%#Eval("ERItemID")%>'
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger" CommandName="Reject" CommandArgument='<%#Eval("ERItemID")%>'
                                            OnClientClick="return confirm('Are you Sure?');" runat="server">Reject</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnlDelete" CssClass="anchor__grd dlt " CommandName="Del" CommandArgument='<%#Eval("ERItemID")%>'
                                            runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 300Px">
                        <asp:Button ID="btnApprove" ToolTip="Select the Items before Approve" CssClass="btn btn-success"
                            runat="server" Text="Recommend" OnClick="btnApprove_Click" AccessKey="i" />
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table id="tblShowRej" runat="server" width="100%" visible="false">
                <tr>
                    <td class="pageheader">Rejected Itmes
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvShowRej" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="100%" EmptyDataText="No Records Found" OnRowDataBound="gvShowRej_RowDataBound"
                            OnRowCommand="gvShowRej_RowCommand" CssClass="gridview">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" Visible="true" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelectOne" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ReimburseItem" DataField="RItem" />
                                <asp:TemplateField HeaderText="Units of Measure">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' Enabled="false"
                                            DataTextField="AU_Name" DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                            runat="server" AutoPostBack="false" CssClass="droplist">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Unit Rate" DataField="UnitRate" />
                                <asp:BoundField HeaderText="Quantity" DataField="Qty" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                <asp:BoundField HeaderStyle-Width="140Px" HeaderText="Purpose" DataField="Purpose" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Spent On" DataField="DateOfSpent" />
                                <asp:BoundField HeaderText="Submitted On" DataField="DOS" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPK" runat="server" Text='<%#Eval("ERItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblERID" runat="server" Text='<%#Eval("ERID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkView" Style="cursor: pointer" onclick='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                            runat="server">Proof</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-Width="140Px" HeaderText="Reason" DataField="Reason" />
                                <asp:BoundField HeaderText="RejectedBy" DataField="RejectedBy" />
                                <asp:BoundField HeaderText="RejectedOn" DataField="RejectedOn" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd " CommandName="Edt" CommandArgument='<%#Eval("ERItemID")%>'
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 300Px">
                        <asp:Button ID="btnRApprove" ToolTip="Select the Items before Approve"
                            runat="server" Text="Approve" CssClass="btn btn-success" OnClick="btnApprove_Click" AccessKey="i" />
                    </td>
                </tr>
            </table>
            <table id="tblRejReason" runat="server" visible="false" width="100%">
                <tr>
                    <td class="pageheader">Enclose Reason
                    </td>
                </tr>
                <tr>
                    <td style="width: 180Px">
                        <b>Enter Reason:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReason" runat="server" BorderColor="#CC6600" BorderStyle="Outset"
                            Height="70px" TextMode="MultiLine" Width="250px"></asp:TextBox><cc1:TextBoxWatermarkExtender
                                ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtReason" WatermarkCssClass="Watermark"
                                WatermarkText="[Enter Reason To Reject!]"></cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180Px"></td>
                    <td style="padding-left: 50Px">
                        <asp:Button ID="btnRejReaSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnRejReaSave_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="BtnEditSave" />
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
