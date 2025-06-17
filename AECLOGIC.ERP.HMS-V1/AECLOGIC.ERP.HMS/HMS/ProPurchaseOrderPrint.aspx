<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ProPurchaseOrderPrint.aspx.cs"
    ValidateRequest="false" Inherits="AECLOGIC.ERP.HMS.ProPurchaseOrderPrint" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchage Order</title>
    <link rel="Stylesheet" rev="Stylesheet" type="text/css" href="Includes/CSS/StyleSheet.css" />
    <base target="_blank"></base>
</head>
<body style="margin-top: 0px;" id="Body" runat="server">
    <form id="form1" runat="server">
    <div class="styleprint">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td valign="top" style="height: 70px" id="trHeader" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" style="height: 50px" id="TblHeader"
                        runat="server">
                        <tr>
                            <td valign="top" align="left">
                                <img alt="Logo" border="0" src="Images/Logo.gif" height="50px" />
                            </td>
                            <td valign="top" align="left">
                                <span class="ClientCompanyPart1"><asp:Label ID="CompName1" runat="server" Text="<%$AppSettings:CompanyNamePart1%>"></asp:Label></span><span class="ClientCompanyPart2">
                                    <asp:Label ID="Label2" runat="server" Text="<%$AppSettings:CompanyNamePart2%>"></asp:Label></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="border: 1px solid; border-collapse: collapse;
                        width: 100%">
                        <tr>
                            <td valign="top" style="vertical-align: top;" id="TDPrint" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" frame="border">
                                    <tr>
                                        <td style="height: 120px; vertical-align: top;">
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%" frame="border" style="border: 1px solid;
                                                border-collapse: collapse; padding-left: 2px;">
                                                <tr>
                                                    <td width="14%" valign="top">
                                                        <asp:Label ID="LBLpoNo_text" runat="server" Text="PO Ref No:" />
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblPoNO" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="14%" valign="top">
                                                        <asp:Label ID="lbldatewo_po" runat="server" Text="PO Date:"></asp:Label>
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblPODate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="14%" valign="top">
                                                        <asp:Label ID="lblfor" runat="server" Text="Purchase Order For:"></asp:Label>
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblPOName" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td width="14%" valign="top">
                                                        Buyer TIN:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblTIN" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="14%" valign="top">
                                                        Budget Code:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblBudgetCode" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="14%" valign="top">
                                                        For Project:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblProject" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="14%" valign="top">
                                                        Indent Ref#:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblIndentNumberDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="14%" valign="top">
                                                        Indent Date:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblindentdate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" width="14%">
                                                        Indented By:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblIndentedBy" runat="server"></asp:Label>
                                                    </td>
                                                    <td valign="top" width="14%">
                                                        Vetted By:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblVettedBy" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="14%" valign="top">
                                                        Vendor Details:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblVendorAddress" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="14%" valign="top">
                                                        Ship to:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <div id="lblworksiteAddress" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr1" runat="server" visible="false">
                                                    <td width="14%" valign="top">
                                                        Note:
                                                    </td>
                                                    <td valign="top" align="center" style="vertical-align: middle;" width="36%">
                                                        <asp:Label ID="lblNote" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td colspan="2" valign="top">
                                                    </td>
                                                </tr>
                                                <tr id="Tr2">
                                                    <td width="14%" valign="top">
                                                        Vendor Rep & Contact #:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblvendrepcontact" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="14%" valign="top">
                                                        Goods Receiver & Contact #:
                                                    </td>
                                                    <td valign="top" width="36%">
                                                        <asp:Label ID="lblstoreHead" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" runat="server" visible="false">
                                                            <td width="14%" valign="top">
                                                                &nbsp;
                                                            </td>
                                                            <td valign="top" width="36%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="14%" valign="top">
                                                                Goods Monitors & Contact #:
                                                            </td>
                                                            <td valign="top" width="36%">
                                                                <asp:Label ID="lblMonitors" runat="server" />
                                                            </td>
                                                        </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px; vertical-align: top;">
                                            <asp:GridView ID="GVdetails" runat="server" AutoGenerateColumns="false" Style="width: 100%" CssClass="gridview">
                                                <Columns>
                                                    <asp:BoundField HeaderText="S#" DataField="SLNO">
                                                        <HeaderStyle Width="20" />
                                                        <ItemStyle Width="20" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Item Desc" DataField="ITEMDESC" HtmlEncode="false">
                                                        <HeaderStyle Width="200" />
                                                        <ItemStyle Width="200" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Image ID="itemImage" runat="server" ImageUrl='<%# ImageUrl(Eval("imageurl").ToString())%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Specification/Make" DataField="SPEC" HtmlEncode="false" />
                                                    <asp:BoundField HeaderText="A/U" DataField="UNITS">
                                                        <HeaderStyle Width="30" />
                                                        <ItemStyle Width="30" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Qty" DataField="QTY">
                                                        <HeaderStyle Width="30" />
                                                        <ItemStyle Width="30" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Rate" DataField="RATE">
                                                        <HeaderStyle Width="30" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px; vertical-align: top; padding-left: 4px;">
                                            <b>
                                                <asp:Label ID="lblvalueinwordsLabel" runat="server" Text="Order Value in Words:" /></b>
                                            <asp:Label ID="lblvalueinwords" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px; vertical-align: top; padding-left: 4px;">
                                            <b>Terms and Conditions:</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" class="TermsCSS">
                                            <asp:GridView ID="GVTerms" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="gridview"
                                                Style="width: 100%" GridLines="None" CellSpacing="0" CellPadding="0">
                                                <RowStyle CssClass="TermsCSS" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Medium" Text="*"></asp:Label>  
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="" HtmlEncode="false" DataField="term" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <img src="IMAGES/EndOfText.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%" frame="border" style="border: 1px solid;
                                    border-collapse: collapse;">
                                    <tr>
                                        <td align="left" width="30%" valign="top">
                                            <b>Buyer Representative</b>
                                            <br />
                                            Name:&nbsp;<asp:Label ID="lblBssName" runat="server" />
                                            <br />
                                            Designation:&nbsp;<asp:Label ID="lblBssDesc" runat="server" />
                                            <%--<DESi>--%>
                                            <br />
                                            For:<b><asp:Label ID="Label3" runat="server" Text="<%$AppSettings:Company%>"></asp:Label></b>
                                        </td>
                                        <td width="20%">
                                        </td>
                                        <td valign="top" width="30%">
                                            <b>Vendor Representative:</b>
                                            <br />
                                            Name:<br />
                                            Designation:<br />
                                            For:
                                            <asp:Label ID="lblVendorRep" runat="server" Font-Bold="true" />
                                        </td>
                                        <td width="20%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trFooter" runat="server">
                <td align="center" valign="top">
                    <br />
                    <br />
                    <asp:Label ID="lblHeadOfficeAddress" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
