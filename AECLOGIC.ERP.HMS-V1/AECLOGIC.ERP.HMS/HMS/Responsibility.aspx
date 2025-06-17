<%@ Page Language="C#"  AutoEventWireup="True" CodeBehind="Responsibility.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Responsibility" Title="" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<%--content   --%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script type="text/javascript" language="javascript">
function Valid()
{
     if(document.getElementById('<%=txtResponsibility.ClientID%>').value=="")
      {    
         alert("Please Enter Employee Duties ");
         return false;
      }
      
}
    </script>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="Includes/CSS/StyleSheet.css" />
   </head>
    <body>
     <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <table style="width:100%;" align="center">
    <tr><td>
        <asp:HyperLink ID="HyperLink1" NavigateUrl="#" runat="server" Font-Bold="True" 
            Font-Size="Smaller" ForeColor="#006600">Responsibilities</asp:HyperLink> | 
            <asp:LinkButton
            ID="lnkTasks" runat="server" OnClick="lnkTasks_Click" Font-Bold="True" Font-Size="Smaller" 
            ForeColor="#006600">Tasks</asp:LinkButton> | 
            <asp:LinkButton ID="lnkTodoList"
                runat="server" Font-Bold="True" Font-Size="Smaller" OnClick="lnkTodoList_Click" 
            ForeColor="#006600">To-Do List</asp:LinkButton></td></tr>
        
        
        
       
        <tr>
            <td colspan="2" class="pageheader" style="height: 22px">
            Date:
            <asp:Label ID="lbldate" class="Headding" runat="server" Text="lblDate"></asp:Label>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="pageheader">
                Responsibilities
            </td>
        </tr>
        <tr>
                                  <td colspan="2" >
                                      <b> Note*</b>  Paste from Wordpad/Notepd (Don&#39;t use MS-Word ) to maintain stable format
                                  </td>
                                  
                               </tr>
        <tr>
            <td colspan="2" style="height: 174px">
            <asp:TextBox runat="server" ID="txtResponsibility" TextMode="MultiLine" Columns="50" Rows="10" Text=" " Width="950px"  Height="600px"/><br />
            <ajax:HtmlEditorExtender ID="htmlExttxtResponsibility"   EnableSanitization="false" TargetControlID="txtResponsibility"  runat="server"/>
           
          </td>
       
       </tr>
        <tr>
          <td style="width: 103px">
           <asp:Button ID="btnsubmit" runat="server" Text="Submit" Visible="false"
            CssClass="savebutton" Width="100px" OnClick="btnsubmit_Click"  /></td>
          <td style="text-align: left; width: 362px;">
          
          </td>
        
        </tr>
        <tr>
          <td>
             <asp:Label ID="hdnEmpid" runat="server" Visible="false"></asp:Label>
          </td>
        </tr>
    </table>
   <%-- </form>
    </body>
    </html>--%>
</asp:Content>