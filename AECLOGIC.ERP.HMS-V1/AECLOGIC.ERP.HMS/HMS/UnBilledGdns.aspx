<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="UnBilledGdns.aspx.cs" Inherits="AECLOGIC.ERP.HMS.UnBilledGdns" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">
    function resetSearch() {
        document.getElementById('<%=ddlVendor.ClientID %>').selectedIndex = 0;
        document.getElementById('<%=txtGdn.ClientID %>').value = "";
        document.getElementById('<%=ddlWorkSite.ClientID %>').selectedIndex = 0;
        return false;
    }
    </script>

    <asp:UpdatePanel ID="upUnBills" runat="server">
        <ContentTemplate>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:HyperLink ID="lnkApprovals" runat="server" Text="Corroborate" NavigateUrl="BillsApproval.aspx?state=1" />
                            &nbsp;|&nbsp;<asp:HyperLink ID="lnkApproved" runat="server" NavigateUrl="BillsApproval.aspx?state=2"
                                Text="Approved" />
                            &nbsp;|
                            <asp:HyperLink ID="lnkRejected" runat="server" NavigateUrl="BillsApproval.aspx?state=3"
                                Text="Rejected" />
                            &nbsp;|
                            <asp:HyperLink ID="lnkUnbilled" runat="server" Text="UnBilled" NavigateUrl="UnBilledGdns.aspx"></asp:HyperLink>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:Accordion ID="MyAccordion" runat="server" AutoSize="None" ContentCssClass="accordionContent"
                                FadeTransitions="false" FramesPerSecond="40" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" RequireOpenedPane="false" SelectedIndex="-1"
                                SuppressHeaderPostbacks="true" TransitionDuration="50">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass="accordionContent"
                                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            Click here to Search</Header>
                                        <Content>
                                            <table id="Table1" class="SearchStlye" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        GdnId:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtGdn" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        Vendor:
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlVendor" CssClass="SearchStlye" AutoPostBack="false" runat="server" />
                                                        <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender3" runat="server"
                                                            TargetControlID="ddlVendor" PromptText="Type to search" PromptCssClass="PromptText"
                                                            PromptPosition="Top" IsSorted="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:Button ID="btnGet" runat="server" CssClass="SearchButtonStlye" OnClick="btnGet_Click"
                                                            Text=" Search" Width="80" />
                                                        &nbsp;&nbsp;<asp:Button ID="btnreset" runat="server" CssClass="SearchButtonStlye"
                                                            OnClientClick="javascript:return resetSearch();" Text=" Reset" Width="80" />
                                                    </td>
                                                    <td align="left">
                                                        WorkSite:
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlWorkSite" CssClass="SearchStlye" AutoPostBack="false" runat="server" />
                                                        <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                            TargetControlID="ddlWorkSite" PromptText="Type to search" PromptCssClass="PromptText"
                                                            PromptPosition="Top" IsSorted="true" />
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
                        <td align="left">
                            <asp:GridView ID="gvDetailReport" ShowFooter="true" runat="server" AllowSorting="true"
                                AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <Columns>
                                    <asp:TemplateField HeaderText="SlNo" SortExpression="SlNo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SlNo" Text='<% #Eval("RowNumber") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GDN" SortExpression="GDN">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="GDN" Text='<% #GenerateGDN(Eval("GDN").ToString()) %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GRN" SortExpression="GRN">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="GRN" Text='<% #GenerateGRN(Eval("GRN").ToString()) %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100" HeaderText="PO NAME" SortExpression="PONAME">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="PONAME" Width="130" Text='<% #Eval("PONAME") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100" HeaderText="Goods" SortExpression="MaterialName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="MaterialName" Text='<% #Eval("MaterialName") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="100" HeaderText="Vendor" SortExpression="MaterialName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblVendor" Text='<% #Eval("Vendor") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Vehicle" SortExpression="Vehicle">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Vehicle" Text='<% #Eval("Vehicle") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TS" SortExpression="Vehicle">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="TripSheet" Text='<% #Eval("TripSheet") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="50" HeaderText="D Date" SortExpression="Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Date" Text='<% #Eval("Date") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100" HeaderText="R Date" SortExpression="RDate">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="RDate" Text='<% #Eval("RDate") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" SortExpression="Unit">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Unit" Text='<% #Eval("Unit") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Qty" SortExpression="POQty">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="POQty" Text='<% #Eval("POQty") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disp Qty" SortExpression="DispQty">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="DispQty" Text='<% #Eval("DispQty") %>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblDispQty" Text='<% #TotDispQty.ToString("#,#0.00") %>' runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Qty" SortExpression="InwardQty">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="InwardQty" Text='<% #Eval("InwardQty") %>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblInwardQty" Text='<% #TotInwardQty.ToString("#,#0.00") %>' runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acpt Qty" SortExpression="AcceptedQty">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="AcceptedQty" Text='<% #Eval("AcceptedQty") %>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblAcceptedQty" Text='<% #TotAccQty.ToString("#,#0.00") %>' runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" SortExpression="Rate">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Rate" Text='<% #Eval("Rate") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxes" SortExpression="Tax">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Tax" Text='<% #Eval("Tax") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charges" SortExpression="OtherCharges">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="OtherCharges" Text='<% #Eval("OtherCharges") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Amount" Text='<%# AECLOGIC.ERP.HMS.NUM.NumberFormatting(Convert.ToDouble(Eval("Amount").ToString()))%>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalAmt" Text='<% #TotAmt.ToString("#,#0.00") %>' runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Report">
                                        <ItemTemplate>
                                            <a id="GRNReport" runat="server" width="50" style="cursor: pointer;" onclick='<% #GenerateGRNReport(Eval("GRN").ToString())%>'>
                                                Report</a>
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
                            <AEC:Paging ID="taskPaging" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
