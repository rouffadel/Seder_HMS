<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="CompanyContribution.aspx.cs" Inherits="AECLOGIC.ERP.HMS.CompanyContribution" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) { var oGrid = document.getElementById(grid); var IPs = oGrid.getElementsByTagName("input"); for (var iCount = 0; iCount < IPs.length; ++iCount) { if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked; } }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="CompContribAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="CompContribAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Month:</b>&nbsp;
                                                        <asp:DropDownList ID="ddlMonth" CssClass="droplist" runat="server" TabIndex="1" AccessKey="1"
                                                            ToolTip="[Alt+1]">
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
                                                    <b>&nbsp;Year:</b>&nbsp;
                                                        <asp:DropDownList ID="ddlYear" CssClass="droplist" runat="server" Width="69px" TabIndex="2"
                                                            AccessKey="2" ToolTip="[Alt+2]">
                                                        </asp:DropDownList>
                                                    <b>&nbsp; Select Type:&nbsp; </b>
                                                    <asp:DropDownList ID="ddlItem" CssClass="droplist" runat="server" TabIndex="3" AccessKey="3"
                                                        ToolTip="[Alt+3]">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"
                                                            TabIndex="4" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvEmpList" AutoGenerateColumns="false" Width="85%" runat="server"
                            ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            ShowFooter="true" HeaderStyle-CssClass="tableHead" OnRowDataBound="gvEmpList_RowDataBound"
                            CssClass="gridview">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="5%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEmp" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("EmpName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Contribution Type" DataField="LongName" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='<%#Bind("ContributionAmount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lbltotCount" runat="server" Text='<%# GetTotal().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Bind("Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemID" runat="server" Text='<%#Bind("ItemId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransID" runat="server" Text='<%#Bind("TransID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
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
