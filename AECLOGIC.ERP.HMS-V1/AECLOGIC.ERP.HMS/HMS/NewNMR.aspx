<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="NewNMR.aspx.cs" Inherits="AECLOGIC.ERP.HMS.NewNMR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }
        function GetdepartmentID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=department_hid.ClientID %>').value = HdnKey;
        }
        function designationId(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=desigination_hid.ClientID %>').value = HdnKey;
        }
        function categoryID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            // alert(HdnKey);
            document.getElementById('<%=category_hid.ClientID %>').value = HdnKey;
        }
        function validatesave() {
            <%--if (document.getElementById('<%=txtusername0.ClientID%>').value == "") {
                alert("Please Enter Name ");
                document.getElementById('<%=txtusername0.ClientID%>').focus();
                return false;
            }--%>
            <%-- if (!chkName('<%=txtusername0.ClientID%>', "Name", true, "")) {
                return false;
            }--%>
            <%-- var e = document.getElementById('<%=txtSearchWorksite0.ClientID%>');
           // var val = e.innerHTML();
            if (!chkDropDownList('<%=ddlworksites0.ClientID%>', "WorkSite", "", ""))
                return false;--%>
            <%--if (!chkName('<%=ddlworksites0.ClientID%>', "WorkSite", true, "")) {
                return false;
            }--%>
            <%-- if (!chkName('<%=ddldepartments0.ClientID%>', "Departments", true, "")) {
                return false;
            }
            if (!chkName('<%=ddlDesignation.ClientID%>', "Designation", true, "")) {
                return false;
            }--%>
            <%--  if (!chkName('<%=ddlcategory.ClientID%>', "Category", true, "")) {
                return false;
            }--%>
            <%-- if (!chkDropDownList('<%=ddlworksites0.ClientID%>', "WorkSite", "", ""))
                return false;
            if (!chkDropDownList('<%=ddldepartments0.ClientID%>', "Department", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlDesignation.ClientID%>', "Designation", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlcategory.ClientID%>', "Category", "", ""))
                return false;--%>
        <%--  if (document.getElementById('<%=ddldepartments0.ClientID%>').value =="") {
                alert("Please Select Department ");
                document.getElementById('<%=ddldepartments0.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlworksites0.ClientID%>').selectedIndex == 0) {
                alert("Please Select Worksite");
                document.getElementById('<%=ddlworksites0.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0) {
                alert("Please Select Designation ");
                document.getElementById('<%=ddlDesignation.ClientID%>').focus();
                return false;
            }
          if (document.getElementById('<%=ddlcategory.ClientID%>').selectedIndex == 0) {
                alert("Please Select Category");
                document.getElementById('<%=ddlcategory.ClientID%>').focus();
                return false;
            }--%>
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td align="left">
                        <div id="dvNewEdit" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td>&nbsp;
                                <table cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td style="width: 150px; background-color: #3c8dbc" class="tableHead">New /Edit
                                        </td>
                                        <td class="tableHead" style="background-color: #3c8dbc">
                                            <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Name <span style="color: #FF0000">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtusername0" runat="server" MaxLength="50" OnTextChanged="btnSearch_Click"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px; vertical-align: top;">Address
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="200" Height="50px" OnTextChanged="btnSearch_Click"
                                                Width="500px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Contact Number
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContact" AutoPostBack="true" runat="server" MaxLength="13" Width="140px"
                                                OnTextChanged="txtContact_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">WorkSite <span style="color: #FF0000">*</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                            <asp:TextBox ID="txtSearchWorksite0" Height="22px" Width="140px" runat="server" AutoPostBack="false"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite0"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtSearchWorksite0"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Department <span style="color: #FF0000">*</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="department_hid" runat="server" />
                                            <asp:TextBox ID="txtSearchdept0" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept0"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetdepartmentID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSearchdept0"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Designation <span style="color: #FF0000">*</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="desigination_hid" runat="server" />
                                            <asp:TextBox ID="txtDesignationSerach0" Height="22px" Width="140px" runat="server" AutoPostBack="false"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdesi" ServicePath="" TargetControlID="txtDesignationSerach0"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="designationId">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtDesignationSerach0"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Designation Name]"></cc1:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">Specialization <span style="color: #FF0000">*</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="category_hid" runat="server" />
                                            <asp:TextBox ID="txtCategorySearch0" Height="22px" Width="140px" runat="server" AutoPostBack="false"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListCat" ServicePath="" TargetControlID="txtCategorySearch0"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="categoryID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtCategorySearch0"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Specialization Name]"></cc1:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="savebutton btn btn-success"
                                                Width="60px" />
                                        </td>
                                    </tr>
                                </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvgr" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50" Width="80%"
                                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                            SelectedIndex="0">
                                            <Panes>
                                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent">
                                                    <Header>
                                                                Search Criteria
                                                            </Header>
                                                    <Content>
                                                        <table>
                                                            <tr>
                                                                <td>Worksite &nbsp;<asp:DropDownList Visible="false" ID="ddlworksites" runat="server"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" CssClass="droplist">
                                                                </asp:DropDownList>
                                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;Department&nbsp;<asp:DropDownList ID="ddldepartments" Visible="false" runat="server" CssClass="droplist">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;
                                Designation
                                                                    <asp:DropDownList ID="ddlSearDesigantion" Visible="false" CssClass="droplist" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtDesignationSerach" OnTextChanged="GetDesignation" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListdesi" ServicePath="" TargetControlID="txtDesignationSerach"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDesignationSerach"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Designation Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;
                                Specialization 
                                                                    <asp:DropDownList ID="ddlSearCategory" CssClass="droplist" Visible="false" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtCategorySearch" OnTextChanged="GetCategory" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListCat" ServicePath="" TargetControlID="txtCategorySearch"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtCategorySearch"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Specialization Name]"></cc1:TextBoxWatermarkExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Name Filter Text&nbsp;<asp:TextBox ID="txtusername"
                                                                    runat="server" MaxLength="50" OnTextChanged="btnSearch_Click"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox
                                                                        ID="chkActive" Style="vertical-align: middle" runat="server" Checked="True" Text="Active EmployeeList" />&nbsp;&nbsp;<asp:Button
                                                                            ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="savebutton btn btn-primary"
                                                                            Width="90px" />
                                                                    &nbsp;<asp:RadioButton ID="rbemp" runat="server" AutoPostBack="True" Checked="True" GroupName="Emp" Font-Size="12px"
                                                                        Text="Labour Emp" OnCheckedChanged="rbemp_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbnmr" runat="server" AutoPostBack="True" GroupName="Emp" Font-Size="12px"
                                                Text="NMR" OnCheckedChanged="rbemp_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <asp:GridView ID="gvDisplay" runat="server" AutoGenerateColumns="False" DataKeyNames="NMRId"
                                            ForeColor="#333333" GridLines="Both" OnRowCommand="gvDisplay_RowCommand" HeaderStyle-CssClass="tableHead"
                                            Width="80%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                            CssClass="gridview">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("NMRName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="200"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "SiteId").ToString())%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="150"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="150"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                <asp:BoundField DataField="Category" HeaderText="Specialization" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <HeaderStyle HorizontalAlign="Center" Width="60"></HeaderStyle>
                                                    <ItemStyle ForeColor="Navy" HorizontalAlign="Center" Width="60"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%-- CommandArgument='<%#Container.DataItemIndex %>'--%>
                                                        <asp:LinkButton ID="LinkButton1" CommandName="Edt" CommandArgument='<%# Eval("NMRId") %>'
                                                            Text="Edit" runat="server" CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderStyle HorizontalAlign="Center" Width="60"></HeaderStyle>
                                                    <ItemStyle ForeColor="Navy" HorizontalAlign="Center" Width="60"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSwitch" CommandArgument='<%#Container.DataItemIndex %>' CommandName="Deactive"
                                                            Text='<%#gvDisplay.Columns[6].HeaderText %>' runat="server" CssClass="anchor__grd dlt"></asp:LinkButton>
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
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
