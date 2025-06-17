<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="LedgerBalances.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.LedgerBalances" Title="Untitled Page" %>

<%@ Register Namespace="AjaxControlToolkit" TagPrefix="ajax" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="LedgerBalUP1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
              <script language="javascript" type="text/javascript">

function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
           // alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }


    </script>
            <table id="LB1" runat="server" cellpadding="2" cellspacing="2" width="100%">
                
                <tr>
                    <td valign="top">
                        <table width="100%">
                          
                            <tr>
                                <td>
                                    <cc1:Accordion ID="LedgerBalAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="LedgerBalAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <b>WorkSite:</b>
                                                              
                                                                &nbsp;&nbsp;&nbsp; 
                                                                 <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>


                                                                




                                                                
                                                                <b>Employee:</b>
                                                                <asp:DropDownList ID="ddlEmployee" AutoPostBack="true" CssClass="droplist" runat="server"
                                                                    OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" TabIndex="2" AccessKey="1" ToolTip="[Alt+1]">
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                                                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                                    TargetControlID="ddlEmployee" />
                                                                <asp:TextBox ID="txtFilter" Visible="false" runat="server" TabIndex="3"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFilter"
                                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]">
                                                                </cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="BtnEmpSearch" runat="server" ToolTip="Use Filter for Instant Search"
                                                                    CssClass="btn btn-primary" OnClick="BtnEmpSearch_Click" Text="Search" TabIndex="4" AccessKey="i"/>
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
                                    <div class="UpdateProgressCSS">
                                        <ajax:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/updateProgress.gif" ImageAlign="AbsMiddle"
                                                    Height="62px" Width="82px" />
                                                please wait...
                                            </ProgressTemplate>
                                        </ajax:UpdateProgress>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:GridView ID="GvLedgerBalances" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="GhostWhite"
                                        HeaderStyle-CssClass="tableHead" ShowFooter="true" Width="100%" OnRowCommand="GvLedgerBalances_RowCommand"
                                        OnRowDataBound="GvLedgerBalances_RowDataBound" EmptyDataText="No Ledger found under the selected group."
                                        CssClass="gridview">
                                        <Columns>
                                            <asp:HyperLinkField ItemStyle-CssClass="Ledgers" HeaderText="Ledger (Head of Account)"
                                                DataTextField="Ledger" DataNavigateUrlFields="LedgerId" DataNavigateUrlFormatString="LedgerBalancesDetails.aspx?LedgerId={0}"
                                                Target="_blank" />
                                            <asp:TemplateField HeaderText="Debit">
                                                <ItemStyle Width="150" HorizontalAlign="Right" />
                                                <HeaderStyle Width="150" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLedgerName" runat="server" Text='<%# AECLOGIC.ERP.HMS.NUM.NumberFormatting(Convert.ToDouble(Eval("DEBIT").ToString()))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <%=TotalDebits%></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit">
                                                <ItemStyle Width="150" HorizontalAlign="Right" />
                                                <HeaderStyle Width="150" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLedgerName" runat="server" Text='<%#AECLOGIC.ERP.HMS. NUM.NumberFormatting(Convert.ToDouble(Eval("CREDIT").ToString()))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <%=TotalCredits%></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Balances
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkBalances" runat="server" Text='<%#Eval("balance") %>' Target="_blank"
                                                        NavigateUrl='<%#String.Format("LedgerBalancesDetails.aspx?LedgerId={0}",Eval("LedgerId").ToString()) %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <b>
                                                        <%=TotalBalance%></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:Paging ID="pgledger" runat="server" />
                                </td>
                            </tr>
                        </table>
                      
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
