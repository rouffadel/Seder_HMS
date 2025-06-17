<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ListOfHolidaysConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ListOfHolidaysConfig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkName('<%=txtHoliday.ClientID%>', " Holiday", "true", "[Holiday]"))
                return false;
            if (!chkDate('<%=txtDate.ClientID%>', "Date", "true", ""))
                return false;
            if (!chkDropDownList('<%=ddlHDType.ClientID%>', " Leave Type", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlProfileType.ClientID%>', "Profile Type", "", ""))
                return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                    <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderOlive" Visible="false"
                            Width="50%">
                        <table id="tblNew" runat="server" visible="false">
                            <tr>
                                <td style="width: 124px">
                                    Holiday<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHoliday" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    Date<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" Width="120" TabIndex="2"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                                        PopupButtonID="txtDOB" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    Holiday Type<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHDType" CssClass="droplist" runat="server" TabIndex="3">
                                    </asp:DropDownList>
                                    &nbsp; PH-Public Holiday, WO- Week Off
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    Emp Nature<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProfileType" CssClass="droplist" runat="server" TabIndex="4">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    Status
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" TabIndex="5" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                        OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" AccessKey="s"
                                        TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
</asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="LstOfHolidayConAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="165%">
                                        <Panes>
                                            <cc1:AccordionPane ID="LstOfHolidayConAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                                <b>Year:&nbsp; </b>
                                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" CssClass="droplist"
                                                                    TabIndex="7" AccessKey="1" ToolTip="[Alt+1]" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <b>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;Nature:&nbsp;</b>
                                                                <asp:DropDownList ID="ddlEmpNature" AutoPostBack="true" CssClass="droplist" runat="server"
                                                                    TabIndex="8" AccessKey="2" ToolTip="[Alt+2]" OnSelectedIndexChanged="ddlEmpNature_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%">
                                    <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview"
                                        Width="165%">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Holiday">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHoliday" runat="server" Text='<%#Eval("Holiday")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Holiday Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHolidayType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfileType" runat="server" Text='<%#Eval("Nature")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("HDId")%>'
                                                        CommandName="Edt" CssClass="anchor__grd edit_grd"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
