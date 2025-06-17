<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="HireBills.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HireBills"
    MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlWorkSites_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:HyperLink ID="lnkApprovals" runat="server" CssClass="btn btn-primary" Text="Pending" NavigateUrl="HireBills.aspx?state=1" />
                        &nbsp;|&nbsp;<asp:HyperLink ID="lnkApproved" CssClass="btn btn-success" runat="server" NavigateUrl="HireBills.aspx?state=2"
                            Text="Approved" />
                        &nbsp;|
                        <asp:HyperLink ID="lnkRejected" CssClass="btn btn-danger" runat="server" NavigateUrl="HireBills.aspx?state=3"
                            Text="Rejected" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader"
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
                                                <td style="width: 120px;">WO No:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPONo" runat="server" AccessKey="1" ToolTip="[Alt+1]" TabIndex="1"></asp:TextBox>
                                                </td>
                                                <td>Vendor  
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVendor" runat="server" DataValueField="ID" DataTextField="Name"
                                                        AppendDataBoundItems="true" Width="150" CssClass="droplist" AccessKey="2" ToolTip="[Alt+2]" TabIndex="2">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server" TargetControlID="ddlVendor"
                                                        PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                        IsSorted="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 120px;">Bill No:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBillNo" runat="server" AccessKey="3" ToolTip="[Alt+3]" TabIndex="3"></asp:TextBox>
                                                </td>
                                                <td>Worksite
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="ddlWorkSites_hid" runat="server" />
                                                    <asp:TextBox ID="txtSearchWorksite" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button Width="100" ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary" Text="Search" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="5" />
                                                    <asp:Button Width="100" ID="btClear" OnClick="btClear_Click" runat="server" CssClass="btn btn-danger" Text="Reset" AccessKey="b" ToolTip="[Alt+b OR Alt+b+Enter]" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="droplist" AccessKey="4" ToolTip="[Alt+4]" TabIndex="7">
                                                        <asp:ListItem Value="1" Text="Posted to Accounts"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Drafts in Accounts" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
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
                    <td colspan="2" style="width: 100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvAutoBilling" Visible="true" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                        AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="No Records Found" Width="100%"
                                        OnRowCommand="GVAutoBilling_RowCommand" HeaderStyle-CssClass="tableHead" OnRowDataBound="gvAutoBilling_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="BillingID" DataField="BillingID" />
                                            <asp:TemplateField HeaderText="WO NO">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                                        CommandName="View" Text='<%#Eval("WONO")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvendor" runat="server" Text='<%#Eval("VendorName")%>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WorkSite">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWS" runat="server" Text='<%#Eval("Site_Name")%>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Billed On">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBilledOn" runat="server" Text='<%#Eval("BilledOn")%>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Period">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatementPeriod" runat="server" Text='<%#Eval("BillingPeriod")%>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<% #Eval("Amount")%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--     <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="savebutton" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Approve"/>--%>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkApprove" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("BillingID") %>'
                                                        CommandName="Approve"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            There are Currently No Record(s) Found.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:Paging ID="pgGoods" runat="server" CurrentPage="1" NoOfPages="1" Visible="True" />
                                </td>
                            </tr>
                        </table>
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
