<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ReAssignPassword.aspx.cs"
    Inherits="AECLOGIC.ERP.HMSV1.ReAssignPasswordV1" Title="" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Valid() {
            if (!chkNumber('<%=txtempid.ClientID %>', 'EmpID', true, ''))
                return false;
        }

        function ValidSearch() {
            if (!chkDropDownList('<%=ddlworksites.ClientID %>', 'Worksite'))
                return false;

            if (!chkDropDownList('<%=ddldepartments.ClientID %>', 'Departments'))
                return false;
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


    </script>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
        
       
        <tr>
            <td>
                <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                    <Panes>
                        <cc1:AccordionPane ID="ViewAppLstAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                <asp:Label ID="lblWS" runat="server" Text="Worksite:"></asp:Label>
                                            </b>&nbsp;<asp:DropDownList ID="ddlworksites" CssClass="droplist" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged"
                                                AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                            </asp:DropDownList>
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>

                                            &nbsp;&nbsp;&nbsp;&nbsp;<b>
                                                <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label>
                                            </b>&nbsp;&nbsp;<asp:DropDownList ID="ddldepartments" Visible="false" CssClass="droplist" runat="server"
                                                AccessKey="1" ToolTip="[Alt+1]" TabIndex="2">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="160px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                            </cc1:TextBoxWatermarkExtender>

                                            &nbsp;&nbsp;&nbsp;&nbsp;<b>
                                                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                                            </b>
                                            <asp:TextBox ID="txtusername" runat="server" MaxLength="50" AccessKey="2" ToolTip="[Alt+2]"
                                                TabIndex="3">&nbsp;&nbsp;&nbsp;&nbsp;</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <b>
                                                <asp:Label ID="lblEmpID" runat="server" Text="EmpID:"></asp:Label>
                                            </b>&nbsp;&nbsp;
                                            <asp:TextBox ID="txtempid" runat="server" AccessKey="3" ToolTip="[Alt+3]" TabIndex="4"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtempid" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                                Width="100px" />
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
            <td colspan="2" style="height: 50px">
                <asp:GridView ID="grdEmployees" EmptyDataText="No Employee(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdEmployees_RowCommand"
                    HeaderStyle-CssClass="tableHead" ForeColor="#333333" GridLines="Both" CssClass="gridview"
                    OnRowDataBound="grdEmployees_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblempid" runat="server" Text='<%#Eval("EmpId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="150" />
                            <HeaderStyle Width="150" />
                            <ItemTemplate>
                                <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserName" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUserName" Width="180px" runat="server" MaxLength="20" Text='<%#Eval("UName")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPassword" Width="180px" runat="server" TextMode="Password"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="80" />
                            <HeaderStyle Width="80" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAssign" runat="server" CommandArgument='<%#Eval("EmpId") %>'
                                    CommandName="ReSet" Text="ReSet" CssClass="btn btn-danger"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="ReasignPaging" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
