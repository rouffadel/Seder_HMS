<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="PLeaveAvailable.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PLeaveAvailable" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<table style="width:100%">
<tr>
                                <td colspan="2">
                                  <asp:GridView ID="gvLeavesAvailable"  runat="server" AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="Both" 
                                  EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" HeaderStyle-CssClass="tableHead" CssClass="gridbdr">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"  />
                                <Columns>
                                        <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("EmpID")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmpName" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblLeaveType" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                               
                                       </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Available CL" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblCLDays" runat="server" Text='<%#Eval("CL")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="Available EL" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblELDays" runat="server" Text='<%#Eval("EL")%>'></asp:Label>
                                                
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
</table>
</asp:Content>

