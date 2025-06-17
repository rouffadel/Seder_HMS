<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="ConfigOffenses.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ConfigOffenses" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkName('<%=txtName.ClientID%>', "Name", "true", "[Name]"))
                return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false">
                <table id="tblNew" runat="server" visible="false">
                    <tr>
                        <td class="pageheader">Add Penalties Items
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Name</b> <span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                OnClick="btnSubmit_Click"
                                OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="2"
                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table id="tblEdit" runat="server" visible="false" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvRMItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview" Width="50%">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="50%" />
                            <Columns>
                                <asp:TemplateField HeaderText="RMItemId">
                                    <ItemTemplate>
                                        <asp:Label ID="Sno" runat="server" Text='<%#Eval("RMItemId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd " CommandArgument='<%#Eval("RMItemId")%>'
                                            CommandName="Edt"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt " Text="Delete" CommandArgument='<%#Eval("RMItemId")%>' CommandName="Del"></asp:LinkButton>
                                    </ItemTemplate>
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
                <tr>
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="ReimburseItemsPaging" runat="server" />
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
