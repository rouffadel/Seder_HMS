<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ContactsList.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.ContactsList" Title="" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
       
        <tr>
            <td colspan="2">
                Trades &nbsp;&nbsp;<asp:DropDownList ID="ddlCategory" CssClass="droplist" runat="server"
                    Width="200px">
                </asp:DropDownList>
                &nbsp;&nbsp; Ref Name &nbsp;&nbsp;<asp:TextBox ID="txtReferenceName" runat="server"
                    Width="140px"></asp:TextBox>
                &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search"
                    CssClass="savebutton" Width="100px" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="dvcontacts" runat="server" visible="false">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td colspan="2" style="width: 100%">
                                <asp:GridView ID="gvContacts" Width="100%" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found"
                                    EmptyDataRowStyle-CssClass="EmptyRowData" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrefname" runat="server" Text='<%#Eval("RepName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact1" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Phone1")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact2" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContact2" runat="server" Text='<%#Eval("Phone2")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 17px">
                                <uc1:Paging ID="EmpListPaging" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
