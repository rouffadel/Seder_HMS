<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GetServiceItems.aspx.cs" Inherits="AECLOGIC.ERP.HMS.GetServiceItems" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <base target="_self" />
    <link href="Includes/CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

    
        <table>
        
            <tr>
                <td>
                    <asp:GridView ID="gvItems" runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="false"
                        CssClass="gridview">
                        <SelectedRowStyle CssClass="selected" />
                        <HeaderStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="SRNItem">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="SRNName" Text='<% #GenerateSRN(Eval("SRNItemId").ToString()) %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO_Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="POName" Text='<% #Eval("PoName") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goods">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Item" Text='<% #Eval("Item") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="RelQty" Text='<% #Eval("RelQty") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="RelQty" Text='<% #Eval("Unit") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            There are Currently No Record(s) Found.
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
</asp:Content>
