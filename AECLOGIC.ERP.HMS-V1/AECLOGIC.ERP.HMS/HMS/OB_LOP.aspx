<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OB_LOP.aspx.cs" Inherits="AECLOGIC.ERP.HMS.OB_LOP" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (document.getElementById('<%=txtEmpId.ClientID%>').value == "") {
                alert("Please Enter EmpId");
                return false;
            }
            if (document.getElementById('<%=txtLOP.ClientID%>').value == "") {
                alert("Please Enter LOP Days");
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="mainview" runat="server">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td style="width: 75px">
                                        <b>Emp Id</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpId" runat="server" Width="250" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        <b>Emp Name</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpName" runat="server" Width="500" TabIndex="1" ReadOnly></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 75px">
                                        <b>LOP</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLOP" runat="server" Width="250" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 100Px" colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" OnClick="btnSubmit_Click"
                                            OnClientClick="javascript:return validatesave();" AccessKey="s"
                                            TabIndex="2" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="tblEdit" runat="server" visible="false">
                                <tr>
                                    <td>
                                        <cc1:Accordion ID="CateAccordion" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                            <Panes>
                                                <cc1:AccordionPane ID="CateAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent">
                                                    <Header>
                                                    Search Criteria</Header>
                                                    <Content>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                            <tr>
                                                                <td style="width: 75px">
                                                                    <b>Emp Id</b><span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSEmpId" runat="server" Width="50" TabIndex="1"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 75px">
                                                                    <b>Emp Name</b><span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSEmpName" runat="server" Width="200" TabIndex="1" ReadOnly></asp:TextBox>
                                                                </td>
                                                                <td style="padding-left: 100Px">
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click"
                                                                        AccessKey="s"
                                                                        TabIndex="2" ToolTip="[Alt+s OR Alt+s+Enter]" />
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
                                    <td style="width: 100%">
                                        <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                            OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%"
                                            CssClass="gridview" OnRowDataBound="gvEmp_RowDataBound">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Pk_OBId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LOP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("LOP")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("Pk_OBId")%>'
                                                            CommandName="Edt"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDel" runat="server" Text="Delete" CommandArgument='<%#Eval("Pk_OBId")%>'
                                                            CommandName="Del"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 17px">
                                        <uc1:Paging ID="EmpListPaging" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
