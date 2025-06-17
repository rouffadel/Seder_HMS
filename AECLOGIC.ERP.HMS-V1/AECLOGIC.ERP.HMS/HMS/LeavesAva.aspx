<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="LeavesAva.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.LeavesAva" Title="Untitled Page" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table>
       
        <tr>
            <td>
                Worksite &nbsp&nbsp : &nbsp &nbsp
                <asp:DropDownList ID="ddlworksite" CssClass="droplist" runat="server">
                </asp:DropDownList>
                &nbsp&nbsp Department &nbsp&nbsp :&nbsp &nbsp
                <asp:DropDownList ID="ddldept" runat="server">
                </asp:DropDownList>
                &nbsp &nbsp &nbsp
                <asp:Button ID="btnsearch" runat="server" Text="SEARCH" OnClick="btnsearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdsearch" runat="server" EmptyDataText="NO records found" AutoGenerateColumns="false"
                    CellPadding="2" CellSpacing="2" CssClass="gridview">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("EmpId")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Available CL">
                            <ItemTemplate>
                                <asp:Label ID="lblcl" runat="server" Text='<%#Eval("CL")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Available EL">
                            <ItemTemplate>
                                <asp:Label ID="lblel" runat="server" Text='<%#Eval("EL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
