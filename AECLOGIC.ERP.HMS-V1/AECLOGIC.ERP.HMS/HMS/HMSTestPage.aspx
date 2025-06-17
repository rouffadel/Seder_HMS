<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HMSTestPage.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.HMSTestPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">--%>
<%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        </asp:Content>
 <%--   </form>
</body>
</html>
    </asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <iframe src="WebForm1.aspx" id="tstPage" style="height:600px;width:800px"></iframe>
    </asp:Content>