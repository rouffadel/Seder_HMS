<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/CommonMaster.master" AutoEventWireup="True" CodeBehind="ViewMultipleAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ViewMultipleAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td style="height: 26px; text-align: left;">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                                            Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>Worksite&nbsp;<asp:DropDownList
                                                    ID="ddlWorksite" runat="server" AutoPostBack="True"
                                                    CssClass="droplist"
                                                    AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"
                                                    OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                                    Department&nbsp;<asp:DropDownList ID="ddlDepartment"
                                                        runat="server" AutoPostBack="True" CssClass="droplist"
                                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged1">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    EmpID &nbsp;<asp:DropDownList ID="ddlEmp" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender3" QueryPattern="Contains" runat="server" TargetControlID="ddlEmp" PromptText="Type to search..." PromptCssClass="PromptText" PromptPosition="Top" IsSorted="true"></cc1:ListSearchExtender>
                                                    EmpNature&nbsp;<asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist"
                                                        OnSelectedIndexChanged="ddlEmpNature_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDay" runat="server" Height="21px" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                                    &nbsp;
                                                            <asp:Image ID="imgDay" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDay"
                                                        PopupPosition="BottomLeft" TargetControlID="txtDay"></cc1:CalendarExtender>
                                                    <asp:Button ID="btnDaySearch" ToolTip="Generate Day Report" runat="server" CssClass="btn btn-primary"
                                                        OnClick="btnDaySearch_Click" Text="Day Report" />
                                                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="droplist">
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
                                                    &nbsp;
                                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="droplist">
                                                            </asp:DropDownList>
                                                    <asp:Button ID="btnMonthReport" ToolTip="Generate Month Report" runat="server" CssClass="btn btn-primary"
                                                        OnClick="btnMonthReport_Click" Text="Month Report" />
                                                    <asp:Button ID="btnSynctoNormal" ToolTip="Sync to Normal Logs" runat="server" CssClass="btn btn-success"
                                                        OnClick="btnSynctoNormal_Click" Text="Sync to Normal Logs" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rblstStatus" runat="server" RepeatDirection="Horizontal" TabIndex="1">
                                                        <asp:ListItem Text="Merged" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Unmerged" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
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
                    <td>
                        <asp:GridView ID="gvMultipleAtt" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                            EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Empid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                                <asp:BoundField DataField="Intime" HeaderText="Intime" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Outtime" HeaderText="Outtime" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="WHS" HeaderText="WHS" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="18px" TextMode="MultiLine" Width="200px"
                                            Text='<%#Bind("Remarks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
