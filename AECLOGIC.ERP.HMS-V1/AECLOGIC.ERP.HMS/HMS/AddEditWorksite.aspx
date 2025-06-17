<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AddEditWorksite.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.CMS_AddEditWorksite" Title="" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


    <script language="javascript" type="text/javascript">
        var popupWindow = null;
        function Confirm() {
            var con
            if (rbShowActive.checked)
                con = confirm("Do you want delete this Worksite ?");
            else

                if (con == true) {
                    return true;
                }
                else {
                    return false;
                }
        }
        function validate() {
            if (document.getElementById('<%=txtWS.ClientID%>').value == "") {
                alert("Please Enter Worksite Name ");
                document.getElementById('<%=txtWS.ClientID%>').focus();
                return false;
            }
            if (!chkDropDownList('<%=ddlState.ClientID%>', 'State'))
                return false;

            if (document.getElementById('<%=txtaddress.ClientID%>').value == "") {

            }
        }
        function OpenPopup() {
            if (popupWindow && !popupWindow.closed)
                popupWindow.focus();
            else {
                popupWindow = window.open("NewState.aspx", "List", "toolbar=no, location=no,status=yes,menubar=no,scrollbars=yes,resizable=no, width=400,height=300,left=430,top=100");
                //  return false;
            }
        }
        function parent_disable() {
            if (popupWindow && !popupWindow.closed)
                popupWindow.focus();
        }

    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div onfocus="parent_disable();" onclick="parent_disable();">



                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                    <tr>
                        <td>
                            <%-- <asp:MultiView ID="mainview" runat="server">--%>
                            <div id="Newvieew" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" class="pageheader"></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 72px">
                                            <b>Worksite</b><span style="color: red">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWS" runat="server" Width="316px" MaxLength="50" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 72px">
                                            <b>Address</b><span style="color: red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Height="77px" Width="60%"
                                                TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            <b>Country </b><span style="color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" TabIndex="3"
                                                CssClass="droplist" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">
                                            <b>State</b><span style="color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlState" CssClass="droplist" runat="server" TabIndex="4">
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lnkState" Text="Add New" CssClass="btn btn-primary" runat="server" OnClientClick="OpenPopup()">
                                            </asp:LinkButton>
                                            <asp:Button ID="btnFresh" runat="server" Text="Refresh" CssClass="btn btn-success" OnClick="btnFresh_Click"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 72px" valign="top">
                                            <b>Status</b>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" TabIndex="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 125Px" colspan="2">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn btn-success"
                                                Width="100px" OnClientClick="javascript:return validate();" AccessKey="s" TabIndex="6"
                                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </div>
                            <div id="EditView" runat="server">
                                <table width="699">
                                    <tr>
                                        <td>
                                            <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="628">
                                                <Panes>
                                                    <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                    Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>

                                                                    <td>&nbsp;
                                                                <asp:RadioButton ID="rbShowActive" AutoPostBack="true" runat="server" Checked="True" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"
                                                                    GroupName="show" Text="Active" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                                                        <asp:RadioButton ID="rbShowInactive" AutoPostBack="true" runat="server" GroupName="show"
                                                                            Text="Deleted" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <b>Worksite Name </b>&nbsp
                                                                <asp:TextBox ID="txtSWorksitename" runat="server"></asp:TextBox>
                                                                        &nbsp 
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                            Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetSearchVendorDetails" ServicePath="" TargetControlID="txtSWorksitename"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem"
                                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server"
                                                                            TargetControlID="txtSWorksitename"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite to Search]" Enabled="True"></cc1:TextBoxWatermarkExtender>
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
                                            <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" Width="90%" OnRowCommand="gdvWS_RowCommand"
                                                OnRowDataBound="gdvWS_RowDataBound" HeaderStyle-CssClass="tableHead" AllowSorting="True"
                                                CssClass="gridview" OnSorting="gdvWS_Sorting" EmptyDataText="No Records Found"
                                                EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                                <Columns>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrjID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Site_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Worksites">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Site_Name")%>' ItemStyle-Width="990px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd " CommandName="Edt" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Site_ID")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt " Text='<%#GetText()%>' CommandName="Del" OnClientClick="return confirm('Do you want to Continue?')"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Site_ID")%>'></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 17px">
                                            <uc1:Paging ID="EmpListPaging" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%-- </asp:MultiView>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdn" runat="server" />
            <%--    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                    <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                        <img src="IMAGES/Loading.gif" alt="update is in progress" />
                        <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" />
                    </asp:Panel>
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div> 
              </ContentTemplate>
    </asp:UpdatePanel> --%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>

    </asp:UpdateProgress>
</asp:Content>
