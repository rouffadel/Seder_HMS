<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ApplicantStatus.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ApplicantStatus" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div id="dvAdd" runat="server">
        <asp:Panel ID="pnlAddAss" runat="server" CssClass="DivBorderOlive" Width="50%">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblAppStatus" runat="server" Text="Applicant Status"></asp:Label><span
                            style="color: #ff0000">*</span>:
                        <asp:TextBox ID="txtAppStatus" runat="server"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMExtAppStatus" runat="server" TargetControlID="txtAppStatus"
                            WatermarkText="[Enter Applicant Status]">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkStatus" runat="server" Text="Status" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div id="dvEdit" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="50%">
                        <Panes>
                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                ContentCssClass="accordionContent">
                                <Header>
                                    Search Criteria</Header>
                                <Content>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                &nbsp;
                                                <asp:RadioButtonList ID="rbStatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbStatus_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="Active" Value="1" Enabled="true"></asp:ListItem>
                                                    <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
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
                    <asp:GridView ID="grdAppStatus" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                        EmptyDataText="No Records Found" OnRowCommand="grdAppStatus_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="AppStatusName" HeaderStyle-Width="20%" HeaderText="Applicant Type" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edt" CommandArgument='<%#Eval("AppStatusID") %>'
                                        Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkStatus" runat="server" Text='<%#GetText()%>' CommandName="Status"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AppStatusID")%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
