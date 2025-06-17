<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="ConfigAirTicket.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ConfigAirTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function OnlyNumeric(evt) {
            var chCode = evt.keyCode ? evt.keyCode : evt.charCode ? evt.charCode : evt.which;
            if (chCode >= 48 && chCode <= 57 ||
             chCode == 46 ||
             chCode == 8) {
                return true;
            }
            else
                return false;
        }
        function ValidAddCity() {
            if (!chkDropDownList('<%=ddlCityCountry.ClientID%>', "Country")) {
                return false;
            }
            if (!chkDropDownList('<%=ddlCityState.ClientID%>', "State")) {
                return false;
            }
            if (!chkName('<%=txtCityName.ClientID%>', "City", true, "")) {
                return false;
            }
        }
        function validatesave() {
            if (document.getElementById('<%= ddlfromCity.ClientID %>').selectedIndex == 0) {
                alert("Select From City!");
                document.getElementById('<%=ddlfromCity.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%= ddlToCity.ClientID %>').selectedIndex == 0) {
                alert("Select To City!");
                document.getElementById('<%=ddlToCity.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%= ddlToCity.ClientID %>').selectedIndex != 0 && document.getElementById('<%= ddlfromCity.ClientID %>').selectedIndex != 0) {
                if (document.getElementById('<%= ddlfromCity.ClientID %>').selectedIndex == document.getElementById('<%= ddlToCity.ClientID %>').selectedIndex) {
                    alert("From City & To City can not be same! Please make genuine selection.");
                    document.getElementById('<%=ddlToCity.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=  ddlAirlines.ClientID %>').selectedIndex == 0) {
                alert("Select Airline!");
                document.getElementById('<%=ddlAirlines.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=  ddlpassengerType.ClientID%>').selectedIndex == 0) {
                alert("Select  Passenger Type!");
                document.getElementById('<%=ddlpassengerType.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%= ddlBookingClass.ClientID   %>').selectedIndex == 0) {
                alert("Select  Booking Class!");
                document.getElementById('<%=ddlBookingClass.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%= txtrate.ClientID  %>').value == "") {
                alert("Please Enter Rate!");
                document.getElementById('<%=txtrate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%= txtrate.ClientID  %>').value == "0") {
                alert("Rate must more than Zero");
                document.getElementById('<%=txtrate.ClientID%>').focus();
                return false;
            }
        }
        function GetAirLinesId(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=AirLinesId_hd.ClientID %>').value = HdnKey;
        }
        function GetPasngrTypeID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=PassengrType_hd.ClientID %>').value = HdnKey;
        }
        function GetBookngClsID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=BookngCls_hd.ClientID %>').value = HdnKey;
        }
        function GetFrmCityID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=FrmCity_hd.ClientID %>').value = HdnKey;
        }
        function GetToCityID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=Tocity_hd.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="tblNewk" runat="server" Visible="False" CssClass="DivBorderTeal" Width="80%">
                            <table>
                                <tr>
                                    <td>
                                        <b>From City:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlfromCity" runat="server" CssClass="droplist" TabIndex="1"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnnewcity" runat="server" Text="AddNew" CssClass="btn btn-primary" OnClick="btnnewcity_Click" />
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlAddCity" runat="server" Height="100px"
                                            Visible="False">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCityCoun" runat="server" Text="Country"></asp:Label>
                                                        <span style="color: #FF0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCityCountry" runat="server" CssClass="droplist" TabIndex="29"
                                                            AccessKey="1" ToolTip="[Alt+1]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCityState" runat="server" Text="State"></asp:Label>
                                                        <span style="color: #FF0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCityState" runat="server" CssClass="droplist" TabIndex="30"
                                                            AccessKey="2" ToolTip="[Alt+2]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCityCityName" runat="server" Text="City"></asp:Label>
                                                        <span style="color: #FF0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCityName" runat="server" TabIndex="31" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnCitySubmit" runat="server" CssClass="btn btn-success" Text="Submit"
                                                            OnClick="btnCitySubmit_Click" OnClientClick="javascript:return ValidAddCity();"
                                                            TabIndex="32" />
                                                        <asp:Button ID="btnCityCancel" runat="server" CssClass="btn btn-danger" Text="Cancel"
                                                            OnClick="btnCityCancel_Click" TabIndex="33" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>To City:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlToCity" runat="server" CssClass="droplist" TabIndex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Select Airline:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAirlines" runat="server" CssClass="droplist" TabIndex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Select Passenger Type:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpassengerType" runat="server" CssClass="droplist" TabIndex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Select Booking Class:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBookingClass" runat="server" CssClass="droplist" TabIndex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Enter Rate:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <%--       <input  type="text"  id="txtrate" runat="server"  title ="Enter Fare rate!"  />--%>
                                        <asp:TextBox runat="server" ID="txtrate" ToolTip="Enter Fare rate!" Width="100px" onkeypress="return OnlyNumeric(event);" Placeholder="Rate!"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="border-top-width: 1px; border-top-color: #0094ff; border-top-style: dotted">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                            OnClick="btnSubmit_Click"
                                            OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="2"
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-danger" Width="70px"
                                            OnClick="btn_reste_Click" AccessKey="r" TabIndex="3" ToolTip="[Alt+r OR Alt+r+Enter]" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblNote" Text="If you do NOT get the Dropdown values "
                                            runat="server" Font-Bold="true" Font-Size="14" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label6" Text="please check for Deactivation of any masters like City, Airlines Etc.,"
                                            runat="server" Font-Bold="true" Font-Size="14" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table id="tblEdit" runat="server" visible="false" width="100%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="DesigAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="DesigAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblDesg" AutoPostBack="true" runat="server" Font-Bold="True" TabIndex="1"
                                                                    OnSelectedIndexChanged="rblDesg_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAirLines" Text="AirLines" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="AirLinesId_hd" runat="server" />
                                                                <asp:TextBox ID="txtAirlines" Height="22px" Width="189px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtAirlines"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetAirLinesId">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtAirlines"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter AirLine Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Label ID="lblPassengerType" Text="Passenger Types" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="PassengrType_hd" runat="server" />
                                                                <asp:TextBox ID="txtPaasngrType" Height="22px" Width="189px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetPassengerType" ServicePath="" TargetControlID="txtPaasngrType"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetPasngrTypeID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPaasngrType"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Passenger Type]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Label ID="lblBokingcls" Text="Booking Class" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="BookngCls_hd" runat="server" />
                                                                <asp:TextBox ID="txtBookngCls" Height="22px" Width="189px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetBookingClass_Search" ServicePath="" TargetControlID="txtBookngCls"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetBookngClsID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtBookngCls"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Booking Class]"></cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFrmCity" Text="From City" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="FrmCity_hd" runat="server" />
                                                                <asp:TextBox ID="txtfrmcity" Height="22px" Width="189px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="Get_City_Search" ServicePath="" TargetControlID="txtfrmcity"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetFrmCityID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtfrmcity"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter From City]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Label ID="lbltocity" Text="To City" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="Tocity_hd" runat="server" />
                                                                <asp:TextBox ID="txttocity" Height="22px" Width="189px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="Get_City_Search" ServicePath="" TargetControlID="txttocity"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetToCityID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txttocity"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter To City]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="btnsearch" Text="Search" runat="server" CssClass="btn btn-success" OnClick="btnsearch_Click" />
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
                                        OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%"
                                        CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="From City" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("from_City")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("To_City")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Airlines">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Airlinename")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passenger Types">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("passenger")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Booking Class">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("BookingClas")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("Fare_rate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("ID")%>'
                                                        CommandName="Edt" CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDel" runat="server" Text='<%#GetText()%>' CommandArgument='<%#Eval("ID")%>'
                                                        CommandName="Del" CssClass="btn btn-danger"></asp:LinkButton>
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
                                <td></td>
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
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
