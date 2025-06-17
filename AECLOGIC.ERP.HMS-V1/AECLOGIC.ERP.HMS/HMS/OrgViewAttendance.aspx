<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OrgViewAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.OrgViewAttendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
</head>
<body>
    <form id="form1"  runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td valign="top" >
                        <strong><span style="font-size: 11pt">
                            <asp:Label ID="lblHead" runat="server" CssClass="pageheader"></asp:Label></span></strong></td>
                </tr>
                 <tr>
                 <td>
                   &nbsp;
                 </td>
                </tr>
                <tr>
                    <td align="left">
                        
                      
                             Month : 
                             <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" 
                                 onselectedindexchanged="ddlMonth_SelectedIndexChanged" CssClass="droplist" >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        Year : <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" 
                                 onselectedindexchanged="ddlYear_SelectedIndexChanged" CssClass="droplist" >
                        </asp:DropDownList>
                        
                        
                        
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                <td style="text-align: left">
                                    <asp:Table ID="tblAtt" runat="server" BorderWidth="2" GridLines="Both">
                                    </asp:Table>
                                </td>
                                </tr>
                                
                                <asp:HiddenField ID="hdn" runat="server"/>
                </table>
    </div>
    </form>
</body>
</html>
