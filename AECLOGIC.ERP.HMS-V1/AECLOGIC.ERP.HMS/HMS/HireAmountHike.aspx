<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="HireAmountHike.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HireAmountHike" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Rent Amount</title>
    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/sddm.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
    <tr><td class="pageheader">Edit Rent Amount</td></tr>
    <tr><td style="width:160Px"><b>PO/WO No:</b></td><td> 
        <asp:Label ID="lblPO" ForeColor="Blue" runat="server"></asp:Label></td></tr>
        <tr><td><b>Amount:</b></td><td>
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox></td></tr>
            <tr><td></td><td>
                <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Text="Save" 
                    onclick="btnSave_Click" /></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
