<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="DocMaster.aspx.cs" Inherits="AECLOGIC.ERP.HMS.DocMaster" %>
<%@ Register  Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajax" %>  
 <%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
   <asp:Button ID="btn" runat="server" Text="submit" OnClick="btn_Click" /><br />
<ajax:HtmlEditorExtender runat="server"   EnableSanitization="false"/> 

</asp:Content>

