<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewSRNBillDetails.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ViewSRNBillDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <title>Welcome To Material Management Systems</title>
    <link rel="stylesheet" type="text/css" href="Includes/CSS/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/base.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/sddm.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/filter.css" />

    <div>
     <asp:GridView ID="gvDetailReport" ShowFooter="true" runat="server" 
            AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" CssClass="gridview">
            <Columns>
             <asp:TemplateField HeaderText="Sr NO">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="SRNO" Text='<% #Eval("RowNumber") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SRN" SortExpression="SRN">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="SRN" Text='<% #GenerateGDN(Eval("SRN").ToString()) %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GRN" SortExpression="GRN">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="SRNItem" Text='<% #GenerateGRN(Eval("SRNItem").ToString()) %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100" HeaderText="WO NAME" SortExpression="WONAME">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="WONAME" Width="130" Text='<% #Eval("WONAME") %>'></asp:Label></ItemTemplate>
                    <ItemStyle Wrap="true" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100" HeaderText="Goods" SortExpression="MaterialName">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="MaterialName" Text='<% #Eval("MaterialName") %>'></asp:Label></ItemTemplate>
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
                <asp:TemplateField HeaderText="WO Qty" SortExpression="WOQty">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="WOQty" Text='<% #Eval("WOQty") %>'></asp:Label></ItemTemplate>
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
                <%--<asp:TemplateField HeaderText="Report">
                    <ItemTemplate>
                        <a id="GRNReport" runat="server" width="50" style="cursor: pointer;" onclick='<% #GenerateGRNReport(Eval("SRNItem").ToString())%>'>
                            Report</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
            <EmptyDataTemplate>
                There are Currently No Record(s) Found.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
 </asp:Content>