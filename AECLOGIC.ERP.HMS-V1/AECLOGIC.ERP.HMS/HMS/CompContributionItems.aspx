<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="CompContributionItems.aspx.cs" Inherits="AECLOGIC.ERP.HMS.CompContributionItems" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">

        function validatesave() {
            if (!chkName('<%=txtName.ClientID%>', "Name", "true", "[Short Name]"))
                return false;

            if (!chkName('<%=txtSName.ClientID%>', "Name", "true", "[Long Name]"))
                return false;

            if (!chkDropDownList('<%=ddlAccess.ClientID%>', "Access", "true", ""))
                return false;
        }

    </script>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                    <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false"
                            Width="50%">
                        <table id="tblNew" runat="server" visible="false">
                            <tr>
                                <td colspan="2" class="pageheader">
                                    New Name
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Short Name<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="180px" MaxLength="10" 
                                        TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Long Name<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSName" runat="server" Width="360px" TabIndex="2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Access<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAccess" CssClass="droplist" runat="server" 
                                        TabIndex="3">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Contribution" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Deduction" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                        OnClick="btnSubmit_Click" 
                                        OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="4" 
                                        ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
</asp:Panel>
                        <br />
                        <table id="tblType" width="100%" runat="server" visible="false">
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
                                                            <td style="width: 53px">
                                                                <b>Type:</b>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList AutoPostBack="true" ID="rbItemsList" runat="server" RepeatDirection="Horizontal"
                                                                    TextAlign="Left" OnSelectedIndexChanged="rbItemsList_SelectedIndexChanged" TabIndex="5">
                                                                    <asp:ListItem Text="Contribution" Selected="True" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Deduction" Value="2"></asp:ListItem>
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
                        </table>
                        <table id="tblEdit" width="100%" runat="server" visible="false">
                            <tr>
                                <td style="width: 100%">
                                    <asp:GridView ID="gvAllowances" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Short Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Long Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("LongName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("Itemid")%>'
                                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
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
                        <br />
                        <table id="tblOrder" runat="server" visible="false">
                            <tr>
                                <td style="vertical-align: top; width: 200px">
                                    <asp:ListBox ID="lstDepartments" runat="server"  Height="400px" AccessKey="1" 
                                        TabIndex="6" ToolTip="[Alt+1]"></asp:ListBox>
                                </td>
                                <td style="vertical-align: middle; width: 100px">
                                    <asp:Button ID="btnFirst" runat="server" Text="Move First" CssClass="btn btn-success" Width="80px" 
                                        OnClick="btnFirst_Click" AccessKey="2" TabIndex="7" ToolTip="[Alt+2]" /><br />
                                    <br />
                                    <asp:Button ID="btnUp" runat="server" Text="Move Up" CssClass="btn btn-primary" Width="80px" 
                                        OnClick="btnUp_Click" AccessKey="3" TabIndex="8" ToolTip="[Alt+3]" /><br />
                                    <br />
                                    <asp:Button ID="btnDown" runat="server" Text="Move Down" CssClass="btn btn-warning" Width="80px" 
                                        OnClick="btnDown_Click" AccessKey="4" ToolTip="[Alt+4]" /><br />
                                    <br />
                                    <asp:Button ID="btnLast" runat="server" Text="Move Last" CssClass="btn btn-danger" Width="80px" 
                                        OnClick="btnLast_Click" AccessKey="5" TabIndex="10" ToolTip="[Alt+5]" /><br />
                                    <br />
                                </td>
                                <td style="vertical-align: middle">
                                    <asp:Button ID="btnOrder" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                        OnClick="btnOrder_Click" AccessKey="s" TabIndex="11" 
                                        ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
