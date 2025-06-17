<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="ServiceApproval.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ServiceApprovalPage" %>

<%@ OutputCache Location="None" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:content ID="Content1" contentplaceholderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">


        function CancelAsyncPostBack() {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //alert(prm);
            if (prm.get_isInAsyncPostBack()) {
                prm.abortPostBack();
            }
        }

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlWorkSites_hid.ClientID %>').value = HdnKey;
    }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <table width="100%">
               
                <tr>
                    <td colspan="2">
                        <asp:HyperLink ID="lnkApprovals" runat="server" Text="Waiting" NavigateUrl="ServiceApproval.aspx?state=1" />
                        &nbsp;|&nbsp;<asp:HyperLink ID="lnkApproved" runat="server" NavigateUrl="ServiceApproval.aspx?state=2"
                            Text="Approved" />
                        &nbsp;|
                        <asp:HyperLink ID="lnkRejected" runat="server" NavigateUrl="ServiceApproval.aspx?state=3"
                            Text="Rejected" />
                            
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td >
                        <cc1:Accordion ID="MyAccordion" runat="server"  HeaderCssClass="accordionHeader"
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
                                            <td colspan="2">
                                                    WO No:
                                                
                                                    <asp:TextBox ID="txtPONo" runat="server" width="100px" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                
                                               
                                                    Bill No:
                                               
                                           
                                                    <asp:TextBox ID="txtBillNo" runat="server" width="90px" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        
                                            
                                                    Vendor
                                               
                                                    <asp:DropDownList ID="ddlVendor" TabIndex="2" runat="server" DataValueField="ID" DataTextField="Name" AccessKey="v" ToolTip="[Alt+v OR Alt+v+Enter]"
                                                        AppendDataBoundItems="true" Width="150" CssClass="droplist">
                                                    </asp:DropDownList>
                                                     <cc1:ListSearchExtender QueryPattern="Contains"  ID="ListSearchExtender1" runat="server" TargetControlID="ddlVendor"
                                                            PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                            IsSorted="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                            
                                                    Worksite
                                             
                                                
                                                    <asp:HiddenField ID="ddlWorkSites_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" AutoPostBack="true" Height="22px" Width="130px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>&nbsp;&nbsp;&nbsp;&nbsp;

                                                    From Date
                                               
                                                    <asp:TextBox ID="txtGdnFromDate" runat="server" width="90px" TabIndex="5" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderIndDate" TargetControlID="txtGdnFromDate"
                                                        runat="server" Format="dd MMM yyyy">
                                                    </cc1:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                    To Date
                                            
                                                    <asp:TextBox ID="txtGdnToDate" runat="server" width="90px" TabIndex="6" AccessKey="y" ToolTip="[Alt+y OR Alt+y+Enter]"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderToDate" TargetControlID="txtGdnToDate"
                                                        runat="server" Format="dd MMM yyyy">
                                                    </cc1:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp;
                                             
                                            
                                           
                                                
                                                    <asp:Button Width="50" ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary" Text="Search" TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"/>
                                                    <asp:Button Width="50" ID="btClear" OnClick="btClear_Click" runat="server" CssClass="btn btn-danger" Text="Reset" TabIndex="8" AccessKey="b" ToolTip="[Alt+b OR Alt+b+Enter]"/>
                                               
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                               
                                                    <asp:DropDownList ID="ddlStatus" runat="server" width="130px" CssClass="droplist" TabIndex="9">
                                                        <asp:ListItem Value="1" Text="Posted to Accounts"  Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Drafts in Accounts"></asp:ListItem>
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
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvAutoBilling" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                        AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="No Records Found"
                                        OnRowCommand="GVAutoBilling_RowCommand" HeaderStyle-CssClass="tableHead" OnRowDataBound="gvAutoBilling_RowDataBound">
                                        <SelectedRowStyle CssClass="selected" />
                                        <Columns>
                                            <asp:HyperLinkField HeaderText="SRN Bill NO" Target="_blank" DataTextField="SRNBillNO" DataNavigateUrlFields="SRNBillNO" DataNavigateUrlFormatString="ViewSRNBillDetails.aspx?id={0}" />
                                             
                                             <asp:TemplateField HeaderText="Vendor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVendor" runat="server" Text='<% #Eval("Vendor")%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Worksite">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWS" runat="server" Text='<% #Eval("Worksite")%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="SRN Bill Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOtherCharges" runat="server" Text='<%#Eval("SRNBillName") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StatementPeriod">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatementPeriod" runat="server" Text='<%#Eval("StatementPeriod")%>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="POName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPOName" runat="server" Text='<% #Eval("WOName")%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service With Qty,UoM & Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGoods" runat="server" Text='<% #Eval("Goods")%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                           
                                            
                                            <asp:TemplateField HeaderText="Service Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<% #Eval("Quantity")%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">  
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# AECLOGIC.ERP.HMS.NUM.NumberFormatting(Convert.ToDouble(Eval("Amount").ToString())) %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="View ">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnk" runat="server" CssClass="anchor__grd vw_grd" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"poid").ToString())%>'
                                                        CommandName="View" Text="View WO" />
                                                </ItemTemplate>
                                                 </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="lnkApprove" runat="server" CommandArgument='<%# Eval("SRNBillNo") %>'
                                                        CommandName="Approve" Text="Approve" CssClass="savebutton anchor__grd edit_grd"></asp:Button>
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
            <div class="UpdateProgressCSS">
                <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                    <ProgressTemplate>
                        <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                            <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                <img src="IMAGES/Loading.gif" alt="update is in progress" />
                                <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" Tabindex="10" /></asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvAutoBilling" />
        </Triggers>
    </asp:UpdatePanel>
</asp:content>

