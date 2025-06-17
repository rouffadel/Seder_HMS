<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="SIMAllocationList.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SIMAllocationList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function SearchModified() {
            $get('<%=hdnSearchChange.ClientID %>').value = "1";
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
                    //elm.value='';
                    elm.focus();
                    return false;
                }
            } return true;
        }

        //For Dropdown list
        function chkDropDownList(object, msg) {

            var elm = getObj(object);

            if (elm.selectedIndex == 0) {
                alert("Select " + msg + "!!!");
                elm.focus();
                return false;
            } return true;
        }

        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            } else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            } else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            } else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }

        //For  CheckBox

        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gveditkbipl.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gveditkbipl.ClientID %>');
            var TargetChildControl = "chkSelect";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function SelectDeSelectHeader(CheckBox) {
            TotalChkBx = parseInt('<%= this.gveditkbipl.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gveditkbipl.ClientID %>');
            var TargetChildControl = "chkSelect";
            var TargetHeaderControl = "chkSelectAll";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            var flag = false;
            var HeaderCheckBox;
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) >= 0)
                    HeaderCheckBox = Inputs[iCount];
                if (Inputs[iCount] != CheckBox && Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) == -1) {
                    if (CheckBox.checked) {
                        if (!Inputs[iCount].checked) {
                            flag = false;
                            HeaderCheckBox.checked = false;
                            return;
                        }
                        else
                            flag = true;
                    }
                    else if (!CheckBox.checked)
                        HeaderCheckBox.checked = false;
                }
            }
            if (flag)
                HeaderCheckBox.checked = CheckBox.checked
        }

        function validateCheckBox() {
            var isValid = false;
            var gvChk = document.getElementById('<%= gveditkbipl.ClientID %>');
            for (var i = 1; i < gvChk.rows.length; i++) {
                var chkInput = gvChk.rows[i].getElementsByTagName('input');
                if (chkInput != null) {
                    if (chkInput[0].type == "checkbox") {
                        if (chkInput[0].checked) {
                            isValid = true;
                            alert("Updated");
                            return true;
                        }
                    }
                }
            }
            alert("Please select atleast one Employee.");
            return false;
        }

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlworksites_hid.ClientID %>').value = HdnKey;
        }
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddldepartments_hid.ClientID %>').value = HdnKey;
        }


    </script>
    <asp:UpdatePanel ID="SimAllocationUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                
              
                <tr>
                    <td align="left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <b>Worksite:</b>
                                                             
                                                                 <asp:HiddenField ID="ddlworksites_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<b>Department:</b>
                                                             
                                                                 <asp:HiddenField ID="ddldepartments_hid" runat="server" />
                                                 <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID"  >
                                            </cc1:AutoCompleteExtender>        
                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<b>Filter Employee:</b>
                                                                <asp:TextBox ID="txtusername" runat="server" MaxLength="50" OnTextChanged="btnSearch_Click"
                                                                    TabIndex="3" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    TabIndex="4" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="savebutton"
                                                                    Width="80px" />
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                        DataKeyNames="EmpId" EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found"
                                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="gveditkbipl_RowDataBound">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this);" Text="All" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SIM ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="EmpID" DataField="EmpId" Visible="false" />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Design" HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" />
                                            <asp:BoundField DataField="Category" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Trades" />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Worksite">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorksite" runat="server" Text='<%#Eval("Site_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Phone No" DataField="SIMNo" />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount Limit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAmountLimit" runat="server" Text='<%#Eval("AmountLimit1")%>'
                                                        Width="120px"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID="fteAmtlimit" TargetControlID="txtAmountLimit" runat="server"
                                                        FilterType="Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("SID")%>' CommandName="Edt"
                                                        Text="Update"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
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
                                    <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdateAll" runat="server" ToolTip="Update All Checked Records"
                                        CssClass="savebutton" OnClick="btnUpdateAll_Click" Text="Update All" OnClientClick="javascript:validateCheckBox()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
