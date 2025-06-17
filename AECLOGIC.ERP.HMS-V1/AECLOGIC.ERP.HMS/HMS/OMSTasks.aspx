<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="OMSTasks.aspx.cs" Inherits="AECLOGIC.ERP.HMS.OMSTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <table><tr><td>
    <asp:Label ID="Label1" runat="server" Text="Enter ProjectID"></asp:Label>
    <asp:TextBox ID="txtPID"
        runat="server">8</asp:TextBox>
    <asp:Button ID="btnCall" runat="server" Text="Task List" 
        onclick="btnCall_Click" />
            <asp:Button ID="btnCallSAP" runat="server" Text="Call SAP" onclick="btnCallSAP_Click" Visible=false
         />
         </td></tr>
<tr><td>
    <asp:GridView ID="gvDataList" runat="server">
    </asp:GridView>
</td></tr>
</table>
</asp:Content>

