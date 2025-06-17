<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="TradeVsResource.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TradeVsResource" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Validate() {
            //for Worksite
            if (!chkDropDownList('<%=ddlResource.ClientID%>', 'Resource'))
                return false;
            //for Manager
            if (!chkDropDownList('<%=ddlCategory.ClientID %>', 'Trade'))
                return false;
            return true;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td colspan="2" class="pageheader"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 113px">Budget Resource<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:HiddenField ID="hddID" runat="server" />
                                    <asp:DropDownList ID="ddlResource" Width="130px" runat="server" CssClass="droplist" TabIndex="1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 113px">Designation<span style="color: #ff0000">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCategory" CssClass="droplist" Width="130px" runat="server"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Map" OnClick="btnSubmit_Click"
                                        CssClass="savebutton btn btn-success" Width="60px" OnClientClick="javascript:return Validate()"
                                        AccessKey="s" TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    &nbsp; &nbsp;
                                      <asp:Button ID="btn_Reset" runat="server" Text="Reset" OnClick="btnReset_Click"
                                          CssClass="savebutton btn btn-danger" Width="60px"
                                          AccessKey="s" TabIndex="6" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 70%">
                            <tr>
                                <td>
                                    <cc1:accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50" Width="55%"
                                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                        SelectedIndex="0">
                                        <panes>
                                                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                                Search Criteria
                                                            </Header>
                                                            <Content> <table>
                                                            <tr>
                                <td >
                                   Budget Resource &nbsp; <asp:DropDownList ID="ddlSercResource" AutoPostBack="true" Width="120px" runat="server" OnSelectedIndexChanged="ddlSercResource_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                </td>  
                                <td >
                                   Designation&nbsp;<asp:DropDownList ID="ddlSercCategory" CssClass="droplist"  Width="110px" runat="server">
                                       <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                </td>
                                 <td >
                                      <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton btn btn-primary"
                                Width="60px"  OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                                                            </table>
                          </Content>
                                                        </cc1:AccordionPane>
                                                    </panes>
                                    </cc1:accordion>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                        GridLines="Both" Width="55%" CellPadding="4" OnRowCommand="gdvWS_RowCommand"
                                        CssClass="gridview" HeaderStyle-CssClass="tableHead"
                                        AllowSorting="true" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BackColor="GhostWhite" DataKeyNames="DesigID">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Budget Resource">
                                                <HeaderStyle Width="100" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hddResvsID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ID")%>'></asp:HiddenField>
                                                    <asp:Label ID="lblResName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ResourceName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation">
                                                <HeaderStyle Width="100" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="60" />
                                                <ItemStyle Width="60" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandName="Edt" CommandArgument='<%#Eval("ResourceID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:paging id="CategoryConfigPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
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
