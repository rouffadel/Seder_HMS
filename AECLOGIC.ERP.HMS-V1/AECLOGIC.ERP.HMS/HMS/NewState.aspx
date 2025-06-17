<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="NewState.aspx.cs" Inherits="AECLOGIC.ERP.HMS.NewState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New State</title>
    <link rel="stylesheet" type="text/css" href="Includes/CSS/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/base.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/sddm.css" />
    <script src="Includes/JS/Validation.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" src="Includes/JS/onload.js" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        function validate() {

            if (!chkDropDownList('<%=ddlCountry.ClientID %>', 'Country'))
                return false;

            if (!chkName('<%=txtNewState.ClientID %>', 'New State', true, '[Enter New State]'))
                return false;
           
        }
    </script>
    <base  target="_self" />
</head>
<body>
    <form id="form1" runat="server">
  
    <div>
         <asp:Label runat="server" id="lblStatus" Text="" Font-Size="14px"></asp:Label>
        <table>
            <tbody>
                <tr>
                    <td colspan="2" align="center" class="pageheader">
                        Add New State
                    </td>
                 
                </tr>
                <tr>
                    <td>
                        Country
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" CssClass="droplist"  runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        New State <sup style="color: #FF0000">*</sup>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewState" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td colspan="2" align="center" >
                <asp:Label ID="lblState" runat="server" style="color: #FF0000"></asp:Label></td></tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                    </td>
                   </tr>
            </tbody>
        </table>
      
    </div>
    </form>
</body>
</html>
