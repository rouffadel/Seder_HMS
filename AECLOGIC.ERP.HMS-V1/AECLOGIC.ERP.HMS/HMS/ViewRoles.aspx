<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewRoles.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ViewRoles" Title="" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<table align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
     
       
      
      
      <tr>
         <td colspan="2" style="height: 153px">
           <asp:GridView ID="grdEmployees" runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdEmployees_RowCommand" ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
              <Columns>
              
              <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Label ID="lblempid" runat="server" Text='<%#Eval("EmpID") %>' Visible="false"></asp:Label>
                            
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                            
                                 <asp:Label ID="lblName" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                       <HeaderStyle Width="150" />
                     <ItemStyle Width="150" />
                            <ItemTemplate>
                            
                            
                                <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>   
                            
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Roles" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                  <asp:DropDownList  ID="ddlRoles"  runat="server"  SelectedIndex='<%# GetRolesIndex(Eval("RoleID").ToString())%>'  DataTextField = "RoleName" DataValueField = "RoleID" DataSource='<%# BindRoles()%>' Enabled='<%# RolesEnabled(Eval("RoleID").ToString())%>'  AutoPostBack="false"  CssClass="droplist"  ></asp:DropDownList>
                            
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     
                     
                     <asp:TemplateField>
                       <HeaderStyle Width="60" />
                     <ItemStyle Width="60" />
                            <ItemTemplate>
                            
                            <asp:LinkButton ID="lnkAssign" runat="server" CommandArgument='<%#Eval("EmpId") %>' CommandName="AssignRole" Text="Re-Assign"></asp:LinkButton>
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     <asp:TemplateField>
                       <HeaderStyle Width="40" />
                     <ItemStyle Width="40" />
                            <ItemTemplate>
                            
                            <asp:LinkButton ID="lnkremove" runat="server" CommandArgument='<%#Eval("EmpId") %>' CommandName="DelRole" Text="Delete"></asp:LinkButton>
                             </ItemTemplate>
                     
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

