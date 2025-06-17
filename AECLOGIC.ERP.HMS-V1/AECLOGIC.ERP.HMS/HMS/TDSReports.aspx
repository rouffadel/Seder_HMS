<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="TDSReports.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TDSReports" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <AEC:Topmenu ID="topmenu" runat="server" />
    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="TDSRptAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="TDSRptAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label>
                                                    <span style="color: #ff0000">*&nbsp; </span><span>:<span style="color: #ff0000">&nbsp;
                                                    </span></span>
                                                    <asp:DropDownList ID="ddlWS" CssClass="droplist" runat="server" TabIndex="1" AccessKey="w"
                                                        ToolTip="[Alt+w OR Alt+w+Enter]">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:
                                                    <asp:DropDownList ID="ddlDept" CssClass="droplist" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFrom" runat="server" Text="From Date"></asp:Label>
                                                    <span style="color: #ff0000">*</span><span> : </span>&nbsp;
                                                    <asp:TextBox ID="txtFrom" runat="server" Width="80px" TabIndex="2" AccessKey="t"
                                                        ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrom"
                                                        PopupButtonID="txtFrom" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTo" runat="server" Text="To Date"></asp:Label>
                                                    <span style="color: #ff0000">*</span> <span>&nbsp;: </span>
                                                    <asp:TextBox ID="txtTo" runat="server" Width="80px" TabIndex="3" AccessKey="y" ToolTip="[Alt+y OR Alt+y+Enter]"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                        PopupButtonID="txtTo" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpID" runat="server" Width="75"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtWMExtEmpID" runat="server" TargetControlID="txtEmpID"
                                                        WatermarkText="[Filter EmpID]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtWMExtEmpName" runat="server" TargetControlID="txtEmpName"
                                                        WatermarkText="[Filter Name]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" TabIndex="4"
                                                        AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlTDS" runat="server" Width="100%">
        <rsweb:ReportViewer ID="TDSRV" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            Width="100%">
        </rsweb:ReportViewer>
    </asp:Panel>
</asp:Content>
