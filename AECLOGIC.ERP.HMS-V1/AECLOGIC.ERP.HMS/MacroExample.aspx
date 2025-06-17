<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="True"
    CodeBehind="MacroExample.aspx.cs" Inherits="MacroExample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Create Word Document" 
                    CssClass="savebutton" onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Open Existing and modify"  
                    CssClass="savebutton" onclick="Button2_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
