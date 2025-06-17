<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="WageAllowance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.WageAllowance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <AEC:Topmenu ID="topmenu" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:Accordion ID="CompContItemsAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="CompContItemsAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>Year:</b><asp:DropDownList ID="ddlYear" CssClass="droplist" runat="server" TabIndex="1"
                                                        AccessKey="1" ToolTip="[Alt+1]">
                                                    </asp:DropDownList>
                                                    <b>Month:</b><asp:DropDownList ID="ddlMonth" CssClass="droplist" AutoPostBack="true"
                                                        AccessKey="2" ToolTip="[Alt+2]" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                                                        TabIndex="2">
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
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvView" Width="50%" ShowFooter="true" AutoGenerateColumns="false"
                            CssClass="gridview" runat="server" AlternatingRowStyle-BackColor="GhostWhite">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="Site Name" DataField="Site_Name" />
                                <asp:TemplateField HeaderText="CTC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCTC" runat="server" Text='<%#Eval("CTC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFCTC" runat="server" Text='<%#ViewState["TCTC"]%>'></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ActualPay">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActPay" runat="server" Text='<%#Eval("Actualpay") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFActPay" runat="server" Text='<%#ViewState["TActualpay"]%>'></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diff">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiff" runat="server" Text='<%#Eval("Diff") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFDiff" runat="server" Text='<%#ViewState["TDiff"]%>'></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
