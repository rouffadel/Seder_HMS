<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ReportView" EnableEventValidation="false"  %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <div style="overflow-x: scroll;overflow-y:scroll;width:83%;">
    <table><tr><td style="height:600px;">
  <%--   <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
          WaitMessageFont-Size="14pt" Width="1094px" Height="978px"  >
                               
                            </rsweb:ReportViewer>--%>
        
        
<rsweb:ReportViewer ID="ReportViewer1" ZoomMode="Percent" ZoomPercent="100" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" Width="100%"  Height="550px" runat="server" HyperlinkTarget="_Top" SizeToReportContent="True" AsyncRendering="false" />
        
        </td></tr></table>
         </div>
    </asp:Content> 
