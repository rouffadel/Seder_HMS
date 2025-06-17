<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="SSRSExample.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SSRSExample" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

    <rsweb:ReportViewer ID="RptSampleSSRS" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px">
        <ServerReport ReportPath="/HMS/SampleEmpData" 
            ReportServerUrl="http://bssprojects.com/ReportServer" />
    </rsweb:ReportViewer>
</asp:Content>