<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="AttendanceImportDevice.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AttendanceImportDevice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table cellpadding="2" cellspacing="0" border="0" width="100%">
      
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Min OutTime &nbsp; &nbsp; Hours<asp:DropDownList ID="ddlstarttime" runat="server"
                    CssClass="droplist">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                    <asp:ListItem Text="12" Value="0"></asp:ListItem>
                </asp:DropDownList>
                Minutes<asp:TextBox ID="txtMinutes" runat="server" CssClass="droplist" Width="40"></asp:TextBox>
                <asp:DropDownList ID="ddlTimeFormat" runat="server" CssClass="droplist">
                    <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                    <asp:ListItem Text="PM" Value="2" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:FileUpload ID="fileupload" runat="server" Width="400" />
                <asp:Button ID="Button1" runat="server" Text="Import" OnClick="btnImport_Click" CssClass="savebutton" />
            </td>
        </tr>
    </table>
</asp:Content>
