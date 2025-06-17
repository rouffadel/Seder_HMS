<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MackAttan.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MackAttan" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                SelectedIndex="0">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Site
                                                        <asp:DropDownList ID="ddlworksites" runat="server" CssClass="droplist" AccessKey="w"
                                                            ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                        </asp:DropDownList>
                                                        &nbsp;Department
                                                        <asp:DropDownList ID="ddldepartments" runat="server" CssClass="droplist" TabIndex="2">
                                                        </asp:DropDownList>
                                                        &nbsp;Historical ID:<asp:TextBox ID="txtOldEmpID" Width="90" runat="server" AccessKey="1"
                                                            ToolTip="[Alt+1]" TabIndex="3"></asp:TextBox>
                                                        &nbsp;EmpID:<asp:TextBox ID="txtEmpID" Width="60Px" runat="server" CssClass="droplist"
                                                            AccessKey="2" ToolTip="[Alt+2]" TabIndex="4"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                        &nbsp;Name:<asp:TextBox ID="txtusername" Width="90" runat="server" MaxLength="30"
                                                             CssClass="droplist" AccessKey="3" ToolTip="[Alt+3]"
                                                            TabIndex="5"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                            CssClass="savebutton" Width="80px" AccessKey="i" ToolTip="[Alt+i]" TabIndex="6" />
                                                </tr>
                                            </table>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </td>
                        </tr>
        <tr><td>
            <table>

                <tr>
                    <td>

<table><tr>
    <td>Name</td>
    <td>
        <asp:Label runat="server" Text="" ID="lblEMPName"></asp:Label>
        <asp:HiddenField runat="server" ID="hfID" Value="0" ></asp:HiddenField>
    </td>

       </tr>

    <tr><td>Date From<asp:TextBox ID="txtFromDay" Width="70Px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txtDayCalederExtender" runat="server" TargetControlID="txtFromDay"
                                                                                        PopupButtonID="txtDOB">
                                                                                    </cc1:CalendarExtender></td></tr>
    <tr><td>Date To<asp:TextBox ID="txtToDay" Width="70Px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="txtToDayCalederExtender" runat="server" TargetControlID="txtToDay"
                                                                                        PopupButtonID="txtDOB">
                                                                                    </cc1:CalendarExtender></td></tr>
</table>
                    </td>
                    <td>
 for Calculation

                    </td>
                    
                </tr>
            </table>


            </td></tr>
                    </table>
    </asp:Content>