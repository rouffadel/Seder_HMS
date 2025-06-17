<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="AllowanceBilling.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AllowanceBilling" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">
        function HideDiv() {
            var divPrePaid = document.getElementById("divPrePaid");
            divPrePaid.style.display = "block";
        }
        function ShowHideDiv(chkPassport) {
            var divPrePaid = document.getElementById("divPrePaid");
            divPrePaid.style.display = chkPassport.checked ? "block" : "none";
            var con1 = document.getElementById("lblprePaidText");
            var con2 = document.getElementById("ddlPrepadiFor");
            con1.style.display = chkPassport.checked ? "block" : "none";
            con1.style.display = chkPassport.checked ? "block" : "none";
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table id="tblView" runat="server" visible="false" width="100%">
                <tr>
                    <td class="pageheader">Hiring Lands & Buildings
                    </td>
                </tr>
                <%--<tr>
                    <td>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <cc1:Accordion ID="gvViewAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                            Height="106px" Width="100%" CssClass="gridview">
                            <Panes>
                                <cc1:AccordionPane ID="gvViewAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>WorkSite :&nbsp;&nbsp;&nbsp; </b>
                                                    <asp:DropDownList ID="ddlWorkSite" runat="server" Visible="false" AutoPostBack="True" CssClass="droplist" OnSelectedIndexChanged="ddlWorkSite_SelectedIndexChanged" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"
                                                        Style="height: 22px" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TextWorksite" OnTextChanged="GetWorksite" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionWorksiteListsearch" ServicePath="" TargetControlID="TextWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TextWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp;<asp:LinkButton ID="lnkAll" Font-Bold="true" CssClass="anchor__grd vw_grd" Visible="false" runat="server"
                                                        OnClick="lnkAll_Click">View All</asp:LinkButton>
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
                        <div class="UpdateProgressCSS">
                            <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/updateProgress.gif" ImageAlign="AbsMiddle"
                                        Height="62px" Width="82px" />
                                    please wait...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="100%" EmptyDataText="No Record(s) Found" ShowFooter="true"
                            EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview"
                            OnRowCommand="gvView_RowCommand" OnRowDataBound="gvView_RowDataBound">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="WO NO">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                            CommandName="View" Text='<%#Eval("WONo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="WO-Date" DataField="WoDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                    HtmlEncode="false" />
                                <asp:BoundField HeaderText="For WorkSite" DataField="ForWorkSite" HeaderStyle-Width="15%">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Vendor" DataField="VendorName">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Hired Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHItem" runat="server" Text='<%#FormatItem(Eval("Item"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                <asp:TemplateField HeaderText="Term">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHType" runat="server" Text='<%#FormatType(Eval("HireType"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lbltotCount" runat="server" Text='<%# GetTotal1().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SubmittedOn" HeaderText="SubmittedOn" />
                                <asp:BoundField DataField="SubmittedBy" HeaderText="SubmittedBy" HeaderStyle-Width="15%" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkDoc" NavigateUrl="#" onclick='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem,"WONo").ToString())%>'
                                            runat="server">Docs</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkConfig" CssClass="selected" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"WONo")).ToString() %>'
                                            CommandName="Config" runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" CommandName="Del" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"WONo")).ToString() %>'
                                            OnClientClick="return confirm('Are you sure to Inactive?')" runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table id="tblVerify" runat="server" visible="false" width="100%">
                <tr>
                    <td class="pageheader">&nbsp;WO Payments
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>WorkSite:&nbsp;&nbsp; </b>
                        <asp:DropDownList ID="ddlWS" CssClass="droplist" runat="server" AccessKey="w"
                            ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                        </asp:DropDownList>
                        <b>&nbsp;&nbsp; Month:&nbsp; </b>
                        <asp:DropDownList ID="ddlMonth" CssClass="droplist" runat="server" TabIndex="2">
                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        <b>&nbsp;&nbsp; Year:&nbsp;&nbsp; </b>
                        <asp:DropDownList ID="ddlYear" CssClass="droplist" runat="server" TabIndex="3">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                            OnClick="btnSearch_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                            TabIndex="4" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvVerify" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="gridview"
                            HeaderStyle-CssClass="tableHead" Width="100%" EmptyDataText="No Record(s) Found"
                            EmptyDataRowStyle-CssClass="EmptyRowData">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="WO No" SortExpression="pono">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpo" runat="server" Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"WONo")).ToString() %>'
                                            Width="70"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Hire From" DataField="HireFrom" />
                                <asp:BoundField HeaderText="Vendor Name" DataField="VendorName" SortExpression="vendor_name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Payment Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHType" runat="server" Text='<%#FormatType(Eval("HireType"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="TransID" DataField="TransID" />
                                <asp:BoundField HeaderText="Paid On" DataField="BilledOn" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lbltotCount" runat="server" Text='<%# GetTotal2().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table id="tblCancelPO" width="100%" visible="false" runat="server">
                <tr>
                    <td>
                        <asp:Label runat="server" Font-Size="12"
                            Text="Raise WO in PMS-->Purchase Requests -> Raising/Purchase Orders -> Rush All Resource Items under service Group 25:Hiring Lands & Buildings Work Order."
                            ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Label runat="server" Font-Size="12"
                            Text="Raise with Quantity = 12  for 1 year and Resource Units in months."
                            ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Label runat="server" Font-Size="12"
                            Text="Bill cycle must be in Months since this is integrated to Prepaid Expenses if rent is paid in advance"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:Accordion ID="gvWOReportAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                            Height="106px" Width="100%" CssClass="gridview">
                            <Panes>
                                <cc1:AccordionPane ID="gvWOReportAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>WorkSite:</b>
                                                    <asp:DropDownList ID="ddlWSWOC" CssClass="droplist" runat="server" Visible="false" AutoPostBack="True" ShowFooter="true"
                                                        OnSelectedIndexChanged="ddlWSWOC_SelectedIndexChanged" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchworksiteWOC" OnTextChanged="GetWorksiteSearch" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionWorkList" ServicePath="" TargetControlID="txtSearchworksiteWOC"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchworksiteWOC"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp;<asp:LinkButton ID="lnkShowAll0" runat="server" Font-Bold="True" CssClass="btn btn-success" OnClick="lnkShowAll0_Click"
                                                        Visible="false">Show All</asp:LinkButton>
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
                        <div class="UpdateProgressCSS">
                            <asp:UpdateProgress ID="updateProgress1" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/updateProgress.gif" ImageAlign="AbsMiddle"
                                        Height="62px" Width="82px" />
                                    please wait...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvWOReport" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                            AllowSorting="true" HeaderStyle-CssClass="tableHead" ShowFooter="true" EmptyDataText="No Record(s) Found"
                            OnRowCommand="gvWOReport_RowCommand">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPOCancel" CssClass="btn btn-danger" runat="server" CommandArgument='<%#Eval("PONO") %>'
                                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemId" runat="server" Text='<%#Eval("ItemId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO NO">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                            CommandName="View" Text='<%#Eval("PONO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="WO Name" DataField="PONAME" />
                                <asp:BoundField HeaderText="For Project" DataField="WorkSite" />
                                <asp:BoundField HeaderText="PO Qty" DataField="Qty" />
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='<%#Bind("AMOUNT")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lbltotCount" runat="server" Text='<%# GetTotal3().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                <asp:TemplateField HeaderText="Term">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHType" runat="server" Text='<%#FormatType(Eval("TypeId"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecoveryStartFrom" Text='<%#Format(Eval("Status"))%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPOclose" CssClass="anchor__grd dlt" OnClientClick="return confirm('Are u sure to Close this WO ?');"
                                            runat="server" CommandArgument='<%#Eval("PONO") %>' CommandName="Close" Text="Close"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkConfig" CssClass="btn btn-primary" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"PONO")).ToString() %>'
                                            CommandName="Config" runat="server">Setup</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table id="tblMain" visible="false" runat="server" width="100%">
                <tr>
                    <td colspan="2" class="pageheader">Lands &amp; Buildings Details
                    </td>
                </tr>
                <tr>
                    <td style="width: 231px" class="style1">
                        <b>PO/WO No:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWO" runat="server" MaxLength="16" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1" style="width: 231px">
                        <b>Type:</b>
                    </td>
                    <td>&nbsp;<asp:DropDownList ID="ddlType" CssClass="droplist" runat="server">
                        <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Hired Land" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Hired Building" Selected="True" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table id="tblItemview" runat="server" visible="false" width="100%">
                <tr>
                    <td class="pageheader">Sub Wo View
                    </td>
                </tr>
                <tr>
                    <td style="height: 149px">
                        <asp:GridView ID="gvItemview" Width="100%" CssClass="gridview" AutoGenerateColumns="false"
                            runat="server">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItem" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="PoDetID" DataField="PodetID" />
                                <asp:BoundField HeaderText="WO" DataField="PoNo" />
                                <asp:BoundField HeaderText="Wo Name" DataField="ResourceName" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 200Px">
                        <asp:Button ID="BtnItemsSelect" runat="server" CssClass="btn btn-success" Text="Send"
                            OnClick="BtnItemsSelect_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                    </td>
                </tr>
            </table>
            <table id="tblHired" visible="false" runat="server" width="100%">
                <tr>
                    <td style="width: 230px">
                        <b>Purpose:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblHPurpose" runat="server" Font-Bold="False" ForeColor="#0033CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px">
                        <b>Owner Name:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblHVName" runat="server" Font-Bold="False" ForeColor="#0033CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Owner Mobile:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblHVMobile" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Owner Address:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHLVAddress" runat="server" BorderColor="#CC6600" BorderStyle="Outset"
                            Height="50px" TextMode="MultiLine" Width="270px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Aggrement On:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblHArmentOn" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                        &nbsp;*WO Date
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Hire Type:</b>
                    </td>
                    <td>&nbsp;<asp:DropDownList ID="ddlHLHirType" Enabled="false" runat="server" CssClass="droplist" AutoPostBack="True">
                        <asp:ListItem Text="Instant" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Fortnightly" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Monthly" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Quarterly" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Half-Yearly" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Yearly" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trHLDays1" runat="server" visible="true">
                    <td style="width: 230px" class="style3">
                        <b>Hire From:&nbsp;&nbsp; </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHLFromDay" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtHLFromDay"
                            PopupButtonID="txtHLFromDay" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        * Default date is WO Date,Change if Required.
                    </td>
                </tr>
                <tr id="trHLQty" runat="server" visible="true">
                    <td style="width: 230px" class="style3">
                        <b>Qty</b>
                    </td>
                    <td>&nbsp;<asp:Label ID="lblHLQty" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr id="trHLMonth2" runat="server" visible="true">
                    <td style="width: 230px" class="style3">
                        <b>Amount(Rs):</b>
                    </td>
                    <td>&nbsp;<asp:Label ID="lblHAmount" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                        &nbsp;
                        <asp:LinkButton ID="lnkAmtHike" runat="server" OnClientClick="return confirm('Do u want To Edit Amount?');"
                            Font-Bold="True" OnClick="lnkAmtHike_Click" Visible="False">Rent-Hike</asp:LinkButton>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Land/Building Address:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHLLAddress" runat="server" BorderColor="#CC6600" BorderStyle="Outset"
                            Height="50px" TextMode="MultiLine" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>For WorkSite:</b>
                    </td>
                    <td>&nbsp;<asp:DropDownList ID="ddlHLWS" CssClass="droplist" Enabled="true" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Land/Building Specification:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHLBAOL" runat="server" BorderColor="#CC6600" BorderStyle="Outset"
                            Height="50px" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b>Documents/Aggrements:</b>
                    </td>
                    <td>&nbsp;
                        <asp:LinkButton ID="lnkHUpload" runat="server" CssClass="btn btn-primary" Font-Bold="True" OnClientClick="javascript:return ShowUploadCtrl();">Upload xs</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px">
                        <b>Security Deposit:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAdvance" Text="0.0" runat="server"></asp:TextBox>&nbsp; * Enter
                        If Advance is Paid to Owner.
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Select Ledger:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLedger" CssClass="droplist" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px">
                        <b>Is Advance Rent?:</b>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkAR" Text="Yes" runat="server" Font-Bold="True" AutoPostBack="true" OnCheckedChanged="chkAR_CheckedChanged" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkWORenewal" Text="Recurring" runat="server" Font-Bold="True" ForeColor="Red" />
                        <div id="divPrePaid" runat="server">
                            <asp:Label Text="Prepaid for (Months)" runat="server" ID="lblprePaidText" />
                            &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlPrepadiFor" runat="server" Width="70px"></asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px" class="style3">
                        <b></b>
                    </td>
                    <td>&nbsp;
                        <asp:Button ID="btnHLSave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnHLSave_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
