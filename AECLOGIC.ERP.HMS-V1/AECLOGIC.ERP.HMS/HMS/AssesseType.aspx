<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="AssesseType.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AssesseType" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validatesave() {


            if (!chkName('<%=txtName.ClientID%>', "Name", "true", "")) {
                return false;
            }
            if (!chkNumber('<%=txtAge.ClientID%>', "Age", "true", "")) {
                return false;
            }


        }

    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
       
        <tr>
            <td>
            <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderOlive" Visible="false"
                            Width="50%">
                <table id="tblNew" runat="server" visible="false">
                    <tr>
                        <td colspan="2" class="pageheader">
                            New AssesseType
                        </td>
                    </tr>
                    <tr>
                        <td>
                            AssesseType<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Min Age<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAge" runat="server" MaxLength="2" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SrCitizen
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdblSrCitizen" runat="server" 
                                RepeatDirection="Horizontal" TabIndex="3">
                                <asp:ListItem Selected="True" Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Gender
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdblstgender" runat="server" 
                                RepeatDirection="Horizontal" TabIndex="4">
                                <asp:ListItem Selected="True" Text="Male" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                OnClick="btnSubmit_Click" 
                                OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="5" 
                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
</asp:Panel>
                <br />
                <table id="tblEdit" runat="server" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvFinancialYear" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvFinancialYear_RowCommand" HeaderStyle-CssClass="tableHead" 
                                CssClass="gridview" onrowdatabound="gvFinancialYear_RowDataBound">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="AssesseType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Assesse")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SrCitezen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHoliday" runat="server" Text='<%#Eval("SrCitezen")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Age">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinAge" runat="server" Text='<%#Eval("Age")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("AssesseId")%>'
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
            </td>
        </tr>
    </table>
</asp:Content>
