<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employeejobwisedes.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.employeejobwisedes" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlWorkSite_hid.ClientID %>').value = HdnKey;
            }
              <%--  function GETDEPT_ID(source, eventArgs) {
                    var HdnKey = eventArgs.get_value();
                    //  alert(HdnKey);
                    document.getElementById('<%=ddlDept_hid.ClientID %>').value = HdnKey;
                }
            function GETResp_ID(source, eventArgs) {
                var HdnKey = eventArgs.get_value();
                //  alert(HdnKey);
                document.getElementById('<%=txtResponsibilitiessearch_hid.ClientID %>').value = HdnKey;
            }--%>
        function GETDesg_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=desigination_hid.ClientID %>').value = HdnKey;
            }
            function GETSpec_ID(source, eventArgs) {
                var HdnKey = eventArgs.get_value();
                //  alert(HdnKey);
                document.getElementById('<%=specification_hid.ClientID %>').value = HdnKey;
            }
            function GETwrk_ID(source, eventArgs) {
                var HdnKey = eventArgs.get_value();
                //  alert(HdnKey);
                document.getElementById('<%=worksite_hid.ClientID %>').value = HdnKey;
            }
    </script>
    <div id="dvView" runat="server">
        <table>
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
                                            <td>
                                                <asp:HiddenField ID="worksite_hid" runat="server" />
                                                <asp:Label ID="lblworksite" runat="server" Visible="true" Text="Worksite"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:TextBox ID="worksite" OnTextChanged="txtSearchWorksite_TextChanged" Visible="true" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionjobWorkList" ServicePath="" TargetControlID="worksite"
                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETwrk_ID">
                                                </cc1:AutoCompleteExtender>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="worksite"
                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite]"></cc1:TextBoxWatermarkExtender>
                                                <asp:Label ID="lblEmpname" runat="server" Text="Employee Name"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:HiddenField ID="ddlWorkSite_hid" runat="server" />
                                                <asp:TextBox ID="txtEmpname" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployee" ServicePath="" TargetControlID="txtEmpname"
                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                </cc1:AutoCompleteExtender>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpname"
                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                                <asp:HiddenField ID="desigination_hid" runat="server" />
                                                <asp:Label ID="lbldesg" runat="server" Visible="true" Text="Designation"></asp:Label>
                                                <asp:TextBox ID="desigination" Visible="true" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionjobDesginationList" ServicePath="" TargetControlID="desigination"
                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDesg_ID">
                                                </cc1:AutoCompleteExtender>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="desigination"
                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Desigination]"></cc1:TextBoxWatermarkExtender>
                                                <asp:HiddenField ID="specification_hid" runat="server" />
                                                <asp:Label ID="lbltrade" runat="server" Visible="true" Text="Category"></asp:Label>
                                                <asp:TextBox ID="specification" Visible="true" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionjobSpecificationList" ServicePath="" TargetControlID="specification"
                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETSpec_ID">
                                                </cc1:AutoCompleteExtender>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="specification"
                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Specification]"></cc1:TextBoxWatermarkExtender>
                                                <asp:Button ID="Button2" runat="server" Text="Search" OnClick="btnSearch_Click"
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
                <td>
                    <asp:GridView ID="gvjobterm" AutoGenerateColumns="false" runat="server"
                        GridLines="Both" CssClass="gridview" Width="100%" OnRowCommand="gvjobterm_RowCommand" OnRowDataBound="gvjobterm_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="empid" HeaderText="Employee ID" Visible="false" />
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" ItemStyle-Width="450px" />
                            <asp:BoundField DataField="Site" HeaderText="Worksite" />
                            <asp:BoundField DataField="departmentname" HeaderText="Department" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="respdescription" HeaderText="jobResponsibilities" ItemStyle-Width="300px" ItemStyle-Wrap="true" />
                            <asp:BoundField DataField="jobdescription" HeaderText="JobDescription" ItemStyle-Width="300px" />
                            <asp:TemplateField HeaderText="Job Commencement Report">
                                <ItemTemplate>
                                    <asp:FileUpload ID="UploadProof" runat="server" Width="180px" />
                                    <asp:HyperLink ID="A1" Target="_blank" href='<%# DocNavigateUrlnew(DataBinder.Eval(Container.DataItem, "Path").ToString(),DataBinder.Eval(Container.DataItem, "EmpID").ToString()) %>'
                                        runat="server" Visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Path").ToString().ToString()) %>'>
                                View</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnksave" Text="Save" runat="server" CssClass="btn btn-success" CommandName="Sav" CommandArgument='<%#Eval("empid")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Visible="false" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 17px">
                    <uc1:Paging ID="EmpListPaging" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
