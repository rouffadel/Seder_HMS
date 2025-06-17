<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewImage.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ViewImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
  
    <div>
    <asp:UpdatePanel ID="updpanel" runat="server"><ContentTemplate>
    <table width="100%">
    <tr><td>
        <asp:Image ID="Image1" Width="80%" Height="50%" runat="server" /></td></tr>
    </table>
    </ContentTemplate></asp:UpdatePanel></div>
   
</asp:Content>