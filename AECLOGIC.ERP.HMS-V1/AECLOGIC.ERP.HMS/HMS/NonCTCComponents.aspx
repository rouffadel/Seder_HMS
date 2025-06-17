<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="NonCTCComponents.aspx.cs" Inherits="AECLOGIC.ERP.HMS.NonCTCComponents" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">

        function validatesave() {

            if (!chkName('<%=txtName.ClientID%>', "Name", "true", "[Short Name]"))
                return false;

            if (!chkName('<%=txtSName.ClientID%>', "Long Name", "true", "[Long Name]"))
                return false;

            if (!chkNumber('<%=txtNoOFYears.ClientID%>', "NoofYears", "true", "[NoofYears]"))
                return false;

        }

    </script>
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
                                Name<span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Width="180px" MaxLength="10" TabIndex="1"></asp:TextBox>
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
                                Type<span style="color: #ff0000">*</span>
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rbtlstType" runat="server" RepeatDirection="Horizontal"
                                    TabIndex="3">
                                    <asp:ListItem Value="1" Text="Yearly" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Non-Yearly"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                NoofYears<span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoOFYears" runat="server" Width="360px" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                    OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" AccessKey="s"
                                    TabIndex="4" ToolTip="[Alt+s OR Alt+s+Enter]" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <table id="tblEdit" runat="server" visible="false" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvAllowances" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                CssClass="gridview" ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" 
                                OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" 
                                onrowdatabound="gvAllowances_RowDataBound">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Long Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("LongName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("YearlyType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No of Years">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoofYears" runat="server" Text='<%#Eval("NoofYears")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd"  CommandArgument='<%#Eval("CompID")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                  
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                            <uc1:Paging ID="AllowancePaging" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
