<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="AssignRoles.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AccessRights" Title="" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<table align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
   
      <tr>
         
         <td>
         Enter EmployeeID&nbsp;&nbsp;
         <asp:TextBox ID="txtempid" runat="server" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Button ID="btngo" runat="server" Text="Search" OnClick="btnSearch_Click"  CssClass="savebutton" Width="100px"/></td>
      
      </tr>
      <tr>
         
           <td valign="middle">
           Worksite &nbsp;<asp:DropDownList ID="ddlworksites" runat="server"></asp:DropDownList>&nbsp;Departments&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:DropDownList ID="ddldepartments" CssClass="droplist"  runat="server"></asp:DropDownList>&nbsp;&nbsp;Employee <asp:TextBox ID="txtusername" runat="server" MaxLength="50"></asp:TextBox> <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="savebutton" Width="100px" />
         
         
         
         </td>
      
      </tr>
      
      
      <tr>
         <td style="height: 153px">
           <asp:GridView ID="grdEmployees" EmptyDataText="No Employee(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData" runat="server" 
                AutoGenerateColumns="false" Width="100%" OnRowCommand="grdEmployees_RowCommand" HeaderStyle-CssClass="tableHead" ForeColor="#333333" 
                GridLines="Both" AlternatingRowStyle-BackColor="GhostWhite" CssClass="gridview">
              <Columns>
              
              <asp:TemplateField>
              
                            <ItemTemplate>
                            
                                <asp:Label ID="lblempid" runat="server" Text='<%#Eval("EmpId") %>' Visible="false"></asp:Label>
                            
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                   
                     <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                    
                            <ItemTemplate>
                             
                                 <asp:Label ID="lblName" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                      <ItemStyle Width="150" />
                     <HeaderStyle Width="150" />
                            <ItemTemplate>
                                <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>   
                            
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Roles" HeaderStyle-HorizontalAlign="Left">
                      <ItemStyle Width="150" />
                     <HeaderStyle Width="150" />
                            <ItemTemplate>
                                  <asp:DropDownList Width="150" ID="ddlRoles" CssClass="droplist"  runat="server"  SelectedIndex='<%# GetRolesIndex(Eval("RoleID").ToString())%>'  DataTextField = "RoleName" DataValueField = "RoleID"  DataSource='<%# BindRoles()%>'></asp:DropDownList>
                            
                             </ItemTemplate>
                     
                     </asp:TemplateField>
                     
                     
                     
                     <asp:TemplateField> <ItemStyle Width="80" />
                     <HeaderStyle Width="80" />
                            <ItemTemplate>
                            
                            <asp:LinkButton ID="lnkAssign" runat="server" CommandArgument='<%#Eval("EmpId") %>' CommandName="AssignRole" Text="AssignRole"></asp:LinkButton>
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

