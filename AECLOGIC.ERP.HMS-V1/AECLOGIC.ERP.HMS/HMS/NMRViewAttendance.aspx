<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="NMRViewAttendance.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.NMRViewAttendance" Title="View Attendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function Validate(dtNow, dtText) {
            var count = 0;
            if (document.getElementById('<%=txtDay.ClientID%>').value == "") {
                alert("Please Select Date");
                return false;
            }
            else {
                var Today = new Date(dtNow);
                var dt = new String(document.getElementById('<%=txtDay.ClientID%>').value);
                dt = dt.split('/')[1] + '/' + dt.split('/')[0] + '/' + dt.split('/')[2];
                var Rep = new Date(dt);
                if (Rep > Today) {
                    alert("Date must be less than or equal to Current Date");
                    return false;
                }
            }
        }
 
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
       
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                        Height="88px" Width="100%">
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <strong><span style="font-size: 11pt">
                                                                    <asp:Label ID="lblHead" runat="server" CssClass="pageheader"></asp:Label></span></strong>
                                                                <br></br>
                                                                <asp:CheckBox ID="chkDay" runat="server" Visible="false" />
                                                                &nbsp;&nbsp;<asp:Label ID="lblDayWise" runat="server" Text="Day Wise"></asp:Label>
                                                                &nbsp;&nbsp;<asp:TextBox ID="txtDay" runat="server"></asp:TextBox>
                                                                &nbsp;&nbsp;
                                                                <asp:Image ID="imgDay" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDay"
                                                                    PopupPosition="BottomLeft" TargetControlID="txtDay">
                                                                </cc1:CalendarExtender>
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnDaySearch" runat="server" CssClass="btn btn-success" OnClick="btnDaySearch_Click"
                                                                    Text="Generate Day Report" />
                                                                &nbsp;
                                                                <asp:CheckBox ID="chkMonth" runat="server" Visible="false" />
                                                                &nbsp;&nbsp;<asp:Label ID="lblMonth" runat="server" Text="Month Wise"></asp:Label>
                                                                &nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" CssClass="droplist" >
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                                                &nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true"  CssClass="droplist" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                               
                                                                </asp:DropDownList>
                                                                <br></br>
                                                                <br></br>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <table style="border-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid;
                                        border-bottom: #d56511 1px solid;" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td>
                                                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                                    Height="106px" Width="100%">
                                                    <Panes>
                                                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                                Search Criteria</Header>
                                                            <Content>
                                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="height: 26px; text-align: left;">
                                                                            <strong>Worksite&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlWorksite" runat="server"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged" CssClass="droplist" >
                                                                            </asp:DropDownList>
                                                                            </strong>
                                                                        </td>
                                                                        <td align="right">
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong> Department&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                                                                ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="droplist" >
                                                                            </asp:DropDownList>
                                                                            </strong>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </Content>
                                                        </cc1:AccordionPane>
                                                    </Panes>
                                                </cc1:Accordion>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td style="text-align: left" colspan="2">
                                                <asp:Table ID="tblAtt" runat="server" BorderWidth="2" GridLines="Both">
                                                </asp:Table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdn" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
