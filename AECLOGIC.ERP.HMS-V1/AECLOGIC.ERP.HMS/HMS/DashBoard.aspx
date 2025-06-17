<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="DashBoard.aspx.cs" Inherits="AECLOGIC.ERP.HMS.DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<table  align="left">
<tr>
  <td>
               <asp:Button ID="BtnAnlExcel" Text="Analyze with Excel" runat="server" 
                   onclick="BtnAnlExcel_Click" />
             </td>
</tr>
       <tr>
             <td align="left" valign="top">
             <asp:ListBox ID="lstDashBoard" runat="server" 
                     onselectedindexchanged="lstDashBoard_SelectedIndexChanged" 
                     AutoPostBack="true" Height="126px">
             <asp:ListItem Text ="Employee History" Value="0"></asp:ListItem>
             <asp:ListItem Text ="Statutory Accounts" Value="1"></asp:ListItem>
             <asp:ListItem Text ="Employee List" Value="2"></asp:ListItem>
             <asp:ListItem Text ="Employee Salary" Value="3"></asp:ListItem>
             <asp:ListItem Text ="Employee Gratuity" Value="4"></asp:ListItem>
             <asp:ListItem Text ="Salary Budget" Value="5"></asp:ListItem>
             <asp:ListItem Text ="Mobile Phone Bills" Value="6"></asp:ListItem>
             <asp:ListItem Text ="Relocation History" Value="7"></asp:ListItem>

             </asp:ListBox>

             </td>
             
             <td>
                  <asp:CheckBoxList ID="ChkColumns" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"></asp:CheckBoxList>
                 <asp:GridView ID="grdDynamic" runat="server" CssClass="gridview">
                 </asp:GridView>
             </td>

       </tr>

</table>
</asp:Content>

