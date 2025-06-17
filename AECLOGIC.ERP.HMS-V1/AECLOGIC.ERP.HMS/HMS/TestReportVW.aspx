<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="TestReportVW.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TestReportVW" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="RDLC_Reports\TestReport.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>

