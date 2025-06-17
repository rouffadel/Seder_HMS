<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="EditBillGoods.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.EditBillGoods" Title="" %>

<%@ OutputCache Location="None" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
           // alert(HdnKey);
            document.getElementById('<%=ddlWS_hid.ClientID %>').value = HdnKey;
        }
</script>
    <asp:UpdatePanel ID="updEditbillGoods" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                
                <tr>
                    <td colspan="3">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="False" SuppressHeaderPostbacks="True">
                          
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" ContentCssClass="accordionContent"
                                    HeaderCssClass="accordionHeader">
                                    <Header>
                                        Search Criteria
                                    </Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    PO No :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Vendor :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVendor" runat="server" AppendDataBoundItems="True" CssClass="droplist"
                                                        DataTextField="Name" DataValueField="ID" Width="200px">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="LEDestRepre" runat="server" Enabled="True" IsSorted="True"
                                                        PromptCssClass="PromptText" QueryPattern="Contains" TargetControlID="ddlVendor">
                                                    </cc1:ListSearchExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    GDN No :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgdn" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Worksite :
                                                </td>
                                                <td>
                                                
                                                      <asp:HiddenField ID="ddlWS_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>


                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Bill No :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBillNo" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" CssClass="savebutton btn btn-primary">
                                                    </asp:Button>
                                                    <asp:Button ID="btClear" runat="server" Text="Reset" Width="100px" OnClick="btClear_Click1" CssClass="savebutton btn btn-danger">
                                                    </asp:Button>
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
                    <td style="width: 92px">
                        <asp:Label ID="lblUBG" runat="server" Text="Unbilled GDNs/SDNs"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblBilledgdns" runat="server" Text="Billed GDNs/SDNs"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px">
                        <asp:ListBox EnableViewState="true" SelectionMode="Multiple" ID="lstGdn3" runat="server"
                            Height="100px" Width="95px" CssClass="droplist" AutoPostBack="false" Font-Bold="False">
                        </asp:ListBox>
                    </td>
                    <td align="center" valign="middle" style="width: 8px">
                        <asp:Button ID="btnAdd" Text="<< Send to UnBilled" runat="server" CssClass="savebutton"
                            OnClick="btnAdd_Click" />
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Generate Bill >>" CssClass="savebutton"
                            OnClick="btnGenBill_Click" />
                    </td>
                    <td>
                        <asp:ListBox ID="lstGdn1" runat="server" Height="100px" Width="95px" CssClass="droplist"
                            SelectionMode="Multiple" Rows="10" Font-Bold="False"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>
                            <asp:Button ID="btnForcetoBill" runat="server" CssClass="savebutton" OnClick="btnForcetoBill_Click"
                                Text=" Force to Bill >>" /></strong>
                    </td>
                    <td style="width: 8px">
                        <strong>BillNo:
                            <asp:DropDownList ID="ddlBillNo" runat="server" AutoPostBack="True" CssClass="droplist"
                                DataTextField="Name" DataValueField="ID" AppendDataBoundItems="True" Width="96px"
                                Height="18px">
                            </asp:DropDownList>
                            <asp:Button ID="btnSelectBill" runat="server" OnClick="ddlBillNo_SelectedIndexChanged"
                                Text="Go" />
                        </strong>
                    </td>
                    <td align="left">
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <strong>
                            <asp:Label ID="lblBGdn" runat="server" Text="Billed GDNs/SDNs"></asp:Label></strong>
                    </td>
                    <td align="left">
                        <strong>Billing Period (This will go as naration to the account transaction.)</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 8px">
                        <asp:ListBox ID="lstGdn2" runat="server" Height="100px" Width="80px" CssClass="droplist"
                            Rows="10" Font-Bold="False"></asp:ListBox>
                        <cc1:ListSearchExtender ID="lseGdn2" runat="server" TargetControlID="lstGdn2" QueryPattern="Contains"
                            PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to Search">
                        </cc1:ListSearchExtender>
                    </td>
                    <td align="left" valign="top">
                        From
                        <asp:TextBox ID="txtFrmDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtenderFrmdDate" TargetControlID="txtFrmDate"
                            runat="server" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        To
                        <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtenderToDate" TargetControlID="txtToDate" runat="server"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <asp:Button ID="btnChange" runat="server" Text="Change" CssClass="savebutton" OnClick="btnChange_Click" />
                    </td>
                </tr>
            </table>
            <div class="UpdateProgressCSS">
                <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                    <ProgressTemplate>
                        <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                            <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                <img src="IMAGES/Loading.gif" alt="update is in progress" />
                                <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" /></asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
