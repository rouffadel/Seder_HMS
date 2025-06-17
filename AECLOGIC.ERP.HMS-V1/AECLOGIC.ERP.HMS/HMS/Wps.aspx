<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="Wps.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Wps" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ValidSearch() {
            if (!chkNumber('<%=txtEmpID.ClientID %>', 'EmpID', false, '')) {
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td width="60%">
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                    <td align="right"></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="SalariesAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SalariesAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">Worksite
                                                                <asp:DropDownList ID="ddlworksites" runat="server" CssClass="droplist" AccessKey="w"
                                                                    ToolTip="[Alt+w OR Alt+w+Enter]" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" TabIndex="1">
                                                                </asp:DropDownList>
                                                                &nbsp;&nbsp;Department
                                                                <asp:DropDownList ID="ddldepartments" runat="server" CssClass="droplist" TabIndex="2"
                                                                    AccessKey="1" ToolTip="[Alt+1]">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp;
                                                                Designation
                                                                <asp:DropDownList ID="ddlDesg" runat="server" CssClass="droplist">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Month
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]">
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
                                                                &nbsp;Year
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3">
                                                                </asp:DropDownList>
                                                                &nbsp;EmpID<asp:TextBox ID="txtEmpID" Width="50Px" runat="server" AccessKey="4" ToolTip="[Alt+4]"
                                                                    TabIndex="5">
                                                                </asp:TextBox>
                                                                &nbsp;Name<asp:TextBox ID="txtEmpName" runat="server" TabIndex="6" AccessKey="5"
                                                                    ToolTip="[Alt+5]"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                                                    TargetControlID="txtEmpName"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="savebutton" OnClientClick="javascript:return ValidSearch();" Width="80px" />
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
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td id="tbAccruals" style="width: 100%;" class="tableHead" runat="server">
                                    <table border="0" cellpadding="0" style="width: 100%;" cellspacing="0">
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnOutPutExcel" runat="server" Text="Export to Excel" OnClientClick="return confirm('Are u Sure to Export?')"
                                                    OnClick="BtnExportGrid_Click" CssClass="savebutton" TabIndex="9" />
                                                <asp:Label ID="lblResultCount" runat="server" />
                                            </td>
                                            <td>
                                                <b>&nbsp;</b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderTeal" Width="100%">
                                        <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                            EmptyDataText="No Records Found"
                                            ShowFooter="True" HeaderStyle-CssClass="tableHead" HeaderStyle-Font-Size="X-Small" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AgentID" HeaderText="Agent ID" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AccountNO" HeaderText="Employee Account with Agent" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="StartDate" HeaderText="Pay Start Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EndDate" HeaderText="Pay End Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DaysinMonth" HeaderText="Days in Period" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Income Fixed Component" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIFC" runat="server" Text='<%#Bind("IFComp")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Income Variable Component" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                    FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIVC" runat="server" Text='<%#Bind("IVC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NoofLeaves" HeaderText="Days on Leave for Period" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Right" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <tr>
                                            <td style="height: 17px">
                                                <uc1:Paging ID="EmpListPaging" runat="server" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOutPutExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
