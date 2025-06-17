<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="NMRSalaries.aspx.cs" Inherits="AECLOGIC.ERP.HMS.NMRSalaries" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
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
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">Worksite
                                                                <asp:DropDownList ID="ddlworksites" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" CssClass="droplist" AccessKey="w"
                                                                    ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                                </asp:DropDownList>
                                                                &nbsp;&nbsp;Department
                                                                <asp:DropDownList ID="ddldepartments" runat="server" CssClass="droplist" TabIndex="2"
                                                                    AccessKey="1" ToolTip="[Alt+1]">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp;
                                                                <asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>
                                                                <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist">
                                                                </asp:DropDownList>
                                                                &nbsp;Month
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
                                                                    TabIndex="5"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                &nbsp;Name<asp:TextBox ID="txtEmpName" runat="server" TabIndex="6" AccessKey="5"
                                                                    ToolTip="[Alt+5]"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                                                    TargetControlID="txtEmpName"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="savebutton"
                                                                    Width="80px" />
                                                                &nbsp;
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
                                <td>&nbsp;<asp:RadioButton ID="rbActive" runat="server" AutoPostBack="True" Checked="True"
                                    GroupName="Emp" Text="Active NMRList" OnCheckedChanged="rbActive_CheckedChanged"
                                    TabIndex="7" />
                                    <asp:RadioButton ID="rbInActive" runat="server" AutoPostBack="True" GroupName="Emp"
                                        Text="Inactive NMRList" OnCheckedChanged="rbInActive_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                        Width="50%" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        ShowFooter="True" HeaderStyle-CssClass="tableHead"
                                        AllowSorting="True" AlternatingRowStyle-BackColor="GhostWhite">
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="EmpID" HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="NetAmount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# GetNetAmount(decimal.Parse(Eval("NetAmount").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblNetAmountTot" runat="server" Text='<%# GetAmount().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" />
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
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
