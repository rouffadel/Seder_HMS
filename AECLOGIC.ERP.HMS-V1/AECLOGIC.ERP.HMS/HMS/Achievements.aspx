<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="Achievements.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Achievements" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<script type="text/javascript" language="javascript">
function Valid()
{
     if(document.getElementById('<%=txtduties.ClientID%>').value=="")
      {    
         alert("Please Enter Employee Duties ");
         return false;
      }
      if(document.getElementById('<%=txtachievements.ClientID%>').value=="")
      {    
         alert("Please Enter Achievements ");
         return false;
      }
}
    </script>
    
    
    <table style="width: 415px;" align="center">
        <tr>
            <td style="vertical-align: top; width: 103px">
            Date
            </td>
            <td style="height: 22px">
                &nbsp;<asp:Label ID="lbldate" runat="server" Text="lblDate"></asp:Label></td>
        </tr>
       <tr>
           <td style="vertical-align: top; width: 103px">
             Duties
           
           </td>
           <td>
           <asp:TextBox ID="txtduties" runat="server" MaxLength="500" TextMode="multiline" Height="75px" Width="98%"></asp:TextBox>
           
           </td>
       
       </tr>
     <tr>
           <td style="vertical-align: top; width: 103px">
             Achievements
           
           </td>
           <td>
           
           <asp:TextBox ID="txtachievements" runat="server" MaxLength="500" TextMode="multiline" Height="75px" Width="98%"></asp:TextBox>
           </td>
       
       </tr>
        <tr>
          <td style="width: 103px">
           <asp:Button ID="btnsubmit" runat="server" Text="Submit"  CssClass="savebutton" Width="100px" OnClick="btnsubmit_Click" /></td>
          <td style="text-align: left">
          
          </td>
        
        </tr>
        
    </table>

</asp:Content>

