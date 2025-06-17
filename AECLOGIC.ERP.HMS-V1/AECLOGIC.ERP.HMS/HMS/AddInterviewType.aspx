<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AddInterviewType.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AddInterviewType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <form id="form1">
    <div>
    <div></div><div></div>
        <asp:Label ID="Label1" runat="server" Text="<B>Add New InterView Type</B>" 
            Font-Bold="True" ForeColor="#CC6600"></asp:Label>
      <table>
      <tr><td></td></tr>
      <tr><td></td></tr>
      <tr><td> <asp:Label ID="Label2" runat="server" Text="Add InterView Type:" 
              Font-Bold="True"></asp:Label>
              &nbsp;&nbsp;
              <asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
         </tr><tr><td></td></tr>
         <tr><td>
           
             &nbsp;&nbsp;&nbsp;&nbsp;
           
             <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="True" 
                 ForeColor="#CC6600" onclick="btnSubmit_Click" CssClass="btn btn-success" /> 
             &nbsp;&nbsp; 
             <asp:Button ID="btnBack" runat="server" Text="Back" Font-Bold="True" 
                 ForeColor="#CC6600" onclick="btnBack_Click"  CssClass="btn btn-primary"/></td></tr></table>           
                   
                    
                    
    </div>
    </form>
</body>
</html>
    </asp:Content>

