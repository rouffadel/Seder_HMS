<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="OTConfiguration.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.OTConfiguration" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkDropDownList('<%=ddlWages.ClientID%>', "OT", "", ""))
                return false;
           <%-- if (!chkDropDownList('<%=ddlFinancial.ClientID%>', " Financial Year", "", ""))
                return false;--%>
            if (!chkFloatNumber('<%=txtCentage.ClientID%>', " Centage on CTC", "true", ""))
                return false;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false" Width="50%">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td colspan="2" class="pageheader">OverTime Allowance
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Calculation On<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWages" CssClass="droplist" runat="server" TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Type<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLeaveType" CssClass="droplist" runat="server" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">OT Type <span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOTTypes" CssClass="droplist" runat="server" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Name <span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCentage" runat="server" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Coeft <span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOTValue" runat="server" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Special Day Coeft <span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOTSDValue" runat="server" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                            OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" TabIndex="4"
                                            AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false" width="100%">
                             <tr>
                        <td>
                            <cc1:Accordion ID="WagesCalAccordion" runat="server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="WagesCalAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr id="trFyear" runat="server" visible="false">
                                                    <td colspan="2">
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
                                    <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        CssClass="gridview" ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvLeaveProfile_RowCommand"
                                        HeaderStyle-CssClass="tableHead"
                                        OnRowDataBound="gvLeaveProfile_RowDataBound">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                            Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Calculation On">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("CalOnName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWages" runat="server" Text='<%#Eval("TypeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OT Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="Coeft">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCentage" runat="server" Text='<%#Convert.ToDouble(Eval("OTValue")).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                HeaderText="Special Day Coeft">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Convert.ToDouble(Eval("OTSDValue")).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd " CommandArgument='<%#Eval("OTConfigID")%>'
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
                        </table>
                        <br />
                        <asp:Panel ID="pnltblOrder" runat="server" CssClass="box box-primary" Visible="false" Width="50%">
                            <table id="tblOrder" runat="server" visible="false">
                                <tr>
                                    <td style="vertical-align: top; width: 200px">
                                        <asp:ListBox ID="lstDepartments" runat="server" Height="400px" TabIndex="6" AccessKey="1"
                                            ToolTip="[Alt+1]"></asp:ListBox>
                                    </td>
                                    <td style="vertical-align: middle; width: 100px">
                                        <asp:Button ID="btnFirst" runat="server" Text="Move First" CssClass="btn btn-success" Width="80px" OnClick="btnFirst_Click"
                                            TabIndex="7" AccessKey="1" ToolTip="[Alt+2]" /><br />
                                        <br />
                                        <asp:Button ID="btnUp" runat="server" Text="Move Up" Width="80px" CssClass="btn btn-primary" OnClick="btnUp_Click"
                                            TabIndex="8" AccessKey="2" ToolTip="[Alt+3]" /><br />
                                        <br />
                                        <asp:Button ID="btnDown" runat="server" Text="Move Down" Width="80px" CssClass="btn btn-warning" OnClick="btnDown_Click"
                                            TabIndex="9" AccessKey="3" ToolTip="[Alt+4]" /><br />
                                        <br />
                                        <asp:Button ID="btnLast" runat="server" Text="Move Last" Width="80px" CssClass="btn btn-danger" OnClick="btnLast_Click"
                                            TabIndex="10" AccessKey="4" ToolTip="[Alt+5]" /><br />
                                        <br />
                                    </td>
                                    <td style="vertical-align: middle">
                                        <asp:Button ID="btnOrder" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                            OnClick="btnOrder_Click" TabIndex="11" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
