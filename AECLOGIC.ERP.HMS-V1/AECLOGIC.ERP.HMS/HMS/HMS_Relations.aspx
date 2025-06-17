<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HMS_Relations.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.HMS_Relations" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="BookingClasContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkName('<%=txtRelationType.ClientID%>', "RelationType", true, "")) {
                   return false;
               }
           }
           function GetRelTypeId(source, eventArgs) {
               var HdnKey = eventArgs.get_value();
               document.getElementById('<%=RelType_hd.ClientID %>').value = HdnKey;
           }
    </script>
    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:MultiView ID="mainview" runat="server">
                            <asp:View ID="Newview" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 81px">Relation Type:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRelationType" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 81px">Status
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" TabIndex="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Submit" Width="100px" AccessKey="s" TabIndex="3"
                                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:View>
                            <asp:View ID="EditView" runat="server">
                                <table width="700">
                                    <tr>
                                        <td>
                                            <cc1:Accordion ID="NewRelationTypeAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="RelationTypeAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblRelType" Text="Relation Type" runat="server"></asp:Label>
                                                                        <asp:HiddenField ID="RelType_hd" runat="server" />
                                                                        <asp:TextBox ID="txtReltype" Height="22px" Width="189px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtReltype"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetRelTypeId" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem ">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtReltype"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Relation Type]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;<asp:Button ID="btnsearch" Text="Search" Height="22px" runat="server" OnClick="btnsearch_Click" CssClass="btn btn-primary" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rblstStatus" runat="server" RepeatDirection="Horizontal" TabIndex="1"
                                                                            OnSelectedIndexChanged="rblstStatus_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
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
                                        <td>
                                            <asp:GridView ID="gvEditRelationType" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="ID" OnRowCommand="gvEditRelationType_RowCommand" Width="100%"
                                                CssClass="gridview" HeaderStyle-CssClass="tableHead" AllowSorting="true" OnSorting="gvRelationType_Sorting"
                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                OnRowDataBound="gvEditRelationType_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="Relation Type" ItemStyle-Width="550px">
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdt" runat="server" Text="Edit" CommandArgument='<%#Bind("ID")%>'
                                                                ControlStyle-ForeColor="Blue" CommandName="Edt" CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" Text='<%#GetText()%>' CommandArgument='<%#Bind("ID")%>'
                                                                CommandName="Del" CssClass="anchor__grd dlt"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 17px">
                                            <uc1:Paging ID="EmpListPaging" runat="server" />
                                            </t>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="updpnl">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
