<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="Allowances.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Allowances" %>
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



        }
        function getvisible() {
         
            var key = '<%=this.Request.QueryString["key"]%>';
            if (key=="1")
            {
                alert()
                document.getElementById("<%= tblNew.ClientID %>").style.display = '';
                document.getElementById("<%= pnltblNew.ClientID %>").style.display = '';
                document.getElementById("<%= tblEdit.ClientID %>").style.display = 'none';
                document.getElementById("<%= tblOrder.ClientID %>").style.display = 'none';
                document.getElementById("<%= gvAllowances.ClientID %>").style.display = 'none';
             }
            else if (key == "2")
            {
               document.getElementById("<%= tblNew.ClientID %>").style.display = 'none';
                document.getElementById("<%= pnltblNew.ClientID %>").style.display = 'none';
                document.getElementById("<%= tblEdit.ClientID %>").style.display = 'none';
                document.getElementById("<%= tblOrder.ClientID %>").style.display = '';
                document.getElementById("<%= gvAllowances.ClientID %>").style.display = 'none';
            }
            else
            {
                document.getElementById("<%= tblNew.ClientID %>").style.display = 'none';
                document.getElementById("<%= pnltblNew.ClientID %>").style.display = 'none';
                document.getElementById("<%= tblEdit.ClientID %>").style.display = '';
                document.getElementById("<%= tblOrder.ClientID %>").style.display = 'none';
                document.getElementById("<%= gvAllowances.ClientID %>").style.display = 'none';
                
            }
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
                            <asp:TextBox ID="txtSName" runat="server" Width="360px" TabIndex="2" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" AccessKey="s"
                                TabIndex="3" ToolTip="[Alt+s OR Alt+s+Enter]" />
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
                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead">
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("AllowId")%>'
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
                <br />
                <table id="tblOrder" runat="server" visible="false">
                    <tr>
                        <td style="vertical-align: top; width: 200px">
                            <asp:ListBox ID="lstDepartments" runat="server" Height="400px" TabIndex="1"
                                ToolTip="[Alt+1]"></asp:ListBox>
                        </td>
                        <td style="vertical-align: middle; width: 100px">
                            <asp:Button ID="btnFirst" runat="server" Text="Move First" CssClass="btn btn-success"  Width="80px" OnClick="btnFirst_Click"
                                AccessKey="2" TabIndex="5" ToolTip="[Alt+2]" /><br />
                            <br />
                            <asp:Button ID="btnUp" runat="server" Text="Move Up" Width="80px" CssClass="btn btn-primary" OnClick="btnUp_Click"
                                AccessKey="3" TabIndex="6" ToolTip="[Alt+3]" /><br />
                            <br />
                            <asp:Button ID="btnDown" runat="server" Text="Move Down" Width="80px" CssClass="btn btn-warning" OnClick="btnDown_Click"
                                AccessKey="4" TabIndex="7" ToolTip="[Alt+4]" /><br />
                            <br />
                            <asp:Button ID="btnLast" runat="server" Text="Move Last" Width="80px" CssClass="btn btn-danger" OnClick="btnLast_Click"
                                AccessKey="5" TabIndex="8" ToolTip="[Alt+5]" /><br />
                            <br />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Button ID="btnOrder" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                OnClick="btnOrder_Click" AccessKey="s" TabIndex="9" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
