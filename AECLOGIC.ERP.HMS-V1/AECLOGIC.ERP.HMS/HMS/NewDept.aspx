<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="NewDept.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.site_NewDept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validatesave() {
            var valuedept = document.getElementById('<%=TextBox1.ClientID%>');

            if ($.trim(valuedept).length == 0) {
                alert("Please Enter Department");
                document.getElementById('<%=TextBox1.ClientID%>').focus();
                return false;

            }
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddldept_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">

                <tr>
                    <td>
                        <asp:MultiView ID="mainview" runat="server">
                            <asp:View ID="Newvieew" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 81px">Department<span style="color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 81px">Status
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active"
                                                TabIndex="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 100Px" colspan="2">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                                Text="Submit" Width="100px" AccessKey="s" TabIndex="3"
                                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:View>
                            <asp:View ID="EditView" runat="server">
                                <table width="699">
                                    <tr>
                                        <td>
                                            <cc1:Accordion ID="NewDeptAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="628">
                                                <Panes>
                                                    <cc1:AccordionPane ID="NewDeptAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="ddldept_hid" runat="server" />
                                                                        <asp:TextBox ID="txtsearchdept" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="txtsearchdept"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtsearchdept"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                                  <td style="padding-right:240px">
                                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" />
                                                                   </td>
                                                                  
                                                                    <td >
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
                                            <asp:GridView ID="gvEditdept" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="DepartmentUId" OnRowCommand="gvEditdept_RowCommand" Width="90%"
                                                CssClass="gridview" HeaderStyle-CssClass="tableHead" AllowSorting="true" OnSorting="gvEditdept_Sorting"
                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                OnRowDataBound="gvEditdept_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Name" ItemStyle-Width="500px">

                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdt" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Bind("DepartmentUId")%>'
                                                                ControlStyle-ForeColor="Blue" CommandName="Edt"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt" Text='<%#GetText()%>' CommandArgument='<%#Bind("DepartmentUId")%>'
                                                                CommandName="Del"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 17px">
                                            <uc1:Paging ID="EmpListPaging" runat="server" />
                                        </td>
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
