<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="History.aspx.cs" Inherits="AECLOGIC.ERP.HMS.History" Title="" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width: 100%">
<tr>
   <td class="pageheader">
    History of Tasks
   </td>
</tr>

                                    <tr>
                                        <td>
                                       
                                               <asp:GridView ID="gvAchievements" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                                GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                                 EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview"  >
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                   
                                                 <asp:TemplateField HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="TaskName" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("TaskName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="DueDate" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("DueDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    
                                                   
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    
                                                </Columns>
                                                <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EditRowStyle BackColor="#999999" />
                                            </asp:GridView>
                                               
                                               
                                               
                                        
                                            
                                        
                                        </td>
                                    </tr>
                                    
                                </table>
</asp:Content>

