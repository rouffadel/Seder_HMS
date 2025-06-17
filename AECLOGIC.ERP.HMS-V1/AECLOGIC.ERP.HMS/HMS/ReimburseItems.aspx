<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="ReimburseItems.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ReimburseItems" MasterPageFile="~/Templates/CommonMaster.master" %>

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
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">

                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false"
                            Width="50%">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td colspan="2" class="pageheader">Add Reimburse Items
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Name<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                            OnClick="btnSubmit_Click"
                                            OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="2"
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false" width="80%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvRMItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvWages_RowCommand" Width="100%" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd " Text="Edit" CommandArgument='<%#Eval("RMItemId")%>'
                                                        CommandName="Edt"></asp:LinkButton>
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

