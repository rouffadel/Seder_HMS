<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="Designations.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Designations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validatesave() {
            <%--if (document.getElementById('<%=txtName.ClientID%>').value == "") {
                alert("Please Enter Designation");
                return false;
            }--%>
            <%--   if (!chkName('<%=txtName.ClientID%>', "Designation", true, "")) {
                return false;
            }--%>
            if (document.getElementById('<%=txtName.ClientID%>').value == "") {
                alert("Please Enter Designation");
                document.getElementById('<%=txtName.ClientID%>').focus();
                return false;
            }
        }

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddldesignation_hid.ClientID %>').value = HdnKey;
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
                        <table id="tblNew" runat="server" visible="false">
                            <tr>
                                <td colspan="2" class="pageheader">
                                    New Designation
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    <b>Designation</b><span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 140Px">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                        OnClick="btnSubmit_Click" 
                                        OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="2" 
                                        ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="tblEdit" runat="server" visible="false" width="80%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="DesigAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="83%">
                                        <Panes>
                                            <cc1:AccordionPane ID="DesigAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                          
                                                                <td>
                                                                        <asp:HiddenField ID="ddldesignation_hid" runat="server" />
                                                                        <asp:TextBox ID="txtsearchdesign" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="txtsearchdesign"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtsearchdesign"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Desigination Name]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                                  <td style="padding-right:240px">
                                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" />
                                                                   </td>
                                                              <td>
                                                                <asp:RadioButtonList ID="rblDesg" AutoPostBack="true" runat="server" Font-Bold="True" TabIndex="1"
                                                                    OnSelectedIndexChanged="rblDesg_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
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
                                <td style="width: 100%">
                                    <asp:GridView ID="gvRMItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" Width="83%"
                                        CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" 
                                        onrowdatabound="gvRMItem_RowDataBound">
                                        <%--<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Designation">
                                                <HeaderStyle Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="100" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd "  Text="Edit" CommandArgument='<%#Eval("DesigId")%>'
                                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt " Text='<%#GetText()%>' CommandArgument='<%#Eval("DesigId")%>'
                                                        CommandName="Del"></asp:LinkButton></ItemTemplate>
                                                <ItemStyle Width="120" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                
                                            </asp:TemplateField>
                                        </Columns>
                                      <%--  <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />--%>
                                    </asp:GridView>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="SalariesUpdPanel">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...</ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
