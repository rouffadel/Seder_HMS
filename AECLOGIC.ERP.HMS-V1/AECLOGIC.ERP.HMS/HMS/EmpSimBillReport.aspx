<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpSimBillReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpSimBillReport" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="SimBillRptAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="SimBillRptAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label>:
                                                      
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
                                                    <asp:Label ID="lblFrom" runat="server" Text="From"></asp:Label>: &nbsp;
                                                    <asp:DropDownList ID="ddlFrmMnth" runat="server" CssClass="droplist">
                                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>:
                                                    <asp:DropDownList ID="ddlToMnth" runat="server" CssClass="droplist">
                                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
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
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="4" OnClick="btnShow_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
            <asp:Panel ID="pnlEmpSimBillRpt" runat="server">
                <rsweb:ReportViewer ID="SimBillRV" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                    Width="100%">
                </rsweb:ReportViewer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
