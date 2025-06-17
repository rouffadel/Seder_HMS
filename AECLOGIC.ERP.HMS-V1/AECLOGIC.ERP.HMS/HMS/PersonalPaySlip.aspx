<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="PersonalPaySlip.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PersonalPaySlip" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table>
       
        <tr>
            <td>
                <cc1:Accordion ID="PersonalPSAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="PersonalPSAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <b>Month:</b>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist"  
                                                AccessKey="1" ToolTip="[Alt+1]" TabIndex="1">
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
                                            &nbsp;&nbsp;&nbsp;&nbsp; <b>Year: </b>&nbsp;<asp:DropDownList ID="ddlYear" runat="server"
                                                TabIndex="2" ToolTip="[Alt+2]" AccessKey="2" CssClass="droplist"  >
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btn btn-success" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="3"/>
                                           
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
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
