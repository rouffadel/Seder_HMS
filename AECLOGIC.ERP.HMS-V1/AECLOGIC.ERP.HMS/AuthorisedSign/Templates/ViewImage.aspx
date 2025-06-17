<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewImage.aspx.cs" Inherits="ViewImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <ajax:UpdatePanel ID="updpanel" runat="server"><ContentTemplate>
    <table width="100%">
    <tr><td>
        <asp:Image ID="Image1" Width="80%" Height="50%" runat="server" /></td></tr>
    </table>
    </ContentTemplate></ajax:UpdatePanel></div>
    </form>
</body>
</html>
