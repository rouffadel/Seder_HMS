<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="EmpAirTicketAuthorisations.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.EmpAirTicketAuthorisationsV1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .MyCalendar .ajax__calendar_container {
            background-color: White;
            color: black;
            border: 1px solid #646464;
        }

            .MyCalendar .ajax__calendar_container td {
                background-color: White;
                padding: 0px;
            }
    </style>
    <script language="javascript" type="text/javascript">
        function ViewShowGrades() {
            window.open("EMPGradeConfig.aspx", '_blank');
            return false;
        }
        function ViewPage() {
            window.open("ConfigAirTicket.aspx?Key=1", '_blank');
            return false;
        }
        function OnlyNumeric(evt) {
            var chCode = evt.keyCode ? evt.keyCode : evt.charCode ? evt.charCode : evt.which;
            if (chCode >= 48 && chCode <= 57 ||
             chCode == 46) {
                return true;
            }
            else
                return false;
        }
        function validatesave() {
            if (document.getElementById('<%=  txtempid.ClientID  %>').value == "") {
                alert("Please Enter employee id!");
                document.getElementById('<%=txtempid.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=  ddlrelation.ClientID %>').selectedIndex == 0) {
                alert("Select Relation!");
                document.getElementById('<%=ddlrelation.ClientID %>').focus();
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
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }
        function GetdeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //alert(HdnKey);
            document.getElementById('<%=ddlsdepartment_hid.ClientID %>').value = HdnKey;
        }
        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlsemp_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td colspan="2" class="pageheader">Air Ticket
                         <asp:LinkButton ID="lnkExpandControlID" Style="vertical-align: middle; border: 0px;"
                             runat="server"> <asp:Image ID="imgImageControlID" ImageAlign="AbsBottom" runat="server"></asp:Image><asp:Label ID="lblTextLabelID" runat="server"></asp:Label></asp:LinkButton>
                        <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" SuppressPostBack="true" TargetControlID="pnlTaskDetails"
                            CollapsedSize="0" ExpandedSize="250" Collapsed="True" ExpandControlID="lnkExpandControlID"
                            CollapseControlID="lnkExpandControlID" AutoCollapse="False" AutoExpand="False"
                            ScrollContents="True" TextLabelID="lblTextLabelID" CollapsedText="Process..."
                            ExpandedText=" Hide Details" ImageControlID="imgImageControlID" ExpandedImage="~/Images/dashminus.gif"
                            CollapsedImage="~/Images/dashplus.gif" ExpandDirection="Vertical" />
                        <asp:Panel ID="pnlTaskDetails" runat="server" CssClass="box box-primary">
                            <table>
                                <tr>
                                    <td>Airlines steps
                                        <br />
To authorize air tickets to employees follow the steps below
                                        <br />
Step 1. HMS >> Airlines >> Employee Vs Airticket >>Add.
                                        <br />
Step 2. If LVRD is missing then it must be set in employee masters.
                                        <br />
Step 3. Employee grade entitlement of Air Ticket must be set in masters.
                                        <br />
Step 4. Intended Airlines must be created and configured.
                                        <br />
Step 5. Update Ticket account by clicking the LVRD button for the selected employee.
                                        <br />
Step 6. Click Utilised Amount button to open Ticket Issues grid.
                                        <br />
Step 7. Click Add Passenger Button in Ticket Issues. For other family members use Add Passenger button.
                                        <br />
Step 8. Click Save button for saving the Ticket Issues.
                                        <br />
Step 9. Every time follow steps 5 to 8 before processing Vacation or Final settlement or Enchasement.
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td colspan="2">
                        <%--<div class="UpdateProgressCSS">
                            <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="updpnl">
                                <ProgressTemplate>
                                    <div class="overlay">
                                        <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                            <span style="color: green; font-weight: bold">Loading...</span>
                                            <img src="../IMAGES/updateProgress.gif" alt="update is in progress" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="tblNewk" runat="server" Visible="False" CssClass="DivBorderTeal" Width="80%">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <b>Employeee ID:</b><span style="color: #ff0000">*</span>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </td>
                                    <td vertical-align: middle>
                                        <asp:TextBox runat="server" ID="txtempid" ToolTip="Search Employee ID!" Width="150px" onkeypress="return OnlyNumeric(event);" OnTextChanged="txtempid_changed" AutoPostBack="True" Placeholder="search employee!"></asp:TextBox><asp:ImageButton runat="server" ID="tickimgk" src="../Images/tick_16.png"></asp:ImageButton><asp:ImageButton runat="server" ID="notfoundk" src="../Images/Searching.png"></asp:ImageButton>
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblempdetails" Style="font-family: Verdana; font-size: 12px"></asp:Label></td>
                                </tr>
                            </table>
                            <table style="text-align: left">
                                <tr>
                                    <td>
                                        <b>Select Relation:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlrelation" runat="server" CssClass="droplist" TabIndex="1"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Select Passenger Type:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpassengerType" runat="server" CssClass="droplist" TabIndex="2"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Select Booking Class:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBookingClass" runat="server" CssClass="droplist" TabIndex="3"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>From City:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlfromCity" runat="server" CssClass="droplist" TabIndex="4"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>To City:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlToCity" runat="server" CssClass="droplist" TabIndex="5"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Frequency:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlfrequency" runat="server" CssClass="droplist" TabIndex="6"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Tickets:</b><span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtTickets" Text="0.0" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right" style="border-top-width: 1px; border-top-color: #0094ff; border-top-style: dotted">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                            OnClick="btnSubmit_Click"
                                            OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="7"
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-danger" Width="70px"
                                            OnClick="btn_reste_Click" AccessKey="r" TabIndex="8" ToolTip="[Alt+r OR Alt+r+Enter]" />
                                        <asp:Label runat="server" ID="lblmsg"></asp:Label>
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
                                                                    <asp:ListItem Text="Delete" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:Label ID="lblwork" runat="server" Text="Worksite:"></asp:Label>
                                                                <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                                <asp:TextBox ID="txtSearchWorksite" OnTextChanged="txtSearchWorksite_TextChanged" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label>
                                                                <asp:HiddenField ID="ddlsdepartment_hid" runat="server" />
                                                                <asp:TextBox ID="txtdeptsearch" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionDepartmentList" ServicePath="" TargetControlID="txtdeptsearch"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetdeptID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdeptsearch"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Label ID="lblemp" runat="server" Text="Employee:"></asp:Label>
                                                                <asp:HiddenField ID="ddlsemp_hid" runat="server" />
                                                                <asp:TextBox ID="textempid" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionemployeeList" ServicePath="" TargetControlID="textempid"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textempid"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnSub" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"
                                                    TabIndex="18" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
                                    <asp:Label ID="Label8" Text="Authorised Employees - Unutilized Tickets" runat="server" Font-Bold="true" Font-Size="14" ForeColor="Blue"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnTypeLea" runat="server" CssClass="btn btn-success" OnClientClick="javascript:return ViewShowGrades();" Text="Show Grades" Width="100" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <asp:GridView ID="gvRMItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%"
                                        CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" ShowHeaderWhenEmpty="true"
                                        OnRowDataBound="gvRMItem_RowDataBound">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Worksite" DataField="Site_Name" />
                                            <asp:TemplateField HeaderText="Relation" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Relation_nm")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From City" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("from_City")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To City">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("To_City")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passenger Type" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("passenger")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Booking Class" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("BookingClas")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Frequency">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("Frequency")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eligibility" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("Tickets")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Ticket" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label115" runat="server" Text='<%#Eval("AvailbaleTickets")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Labe8" runat="server" Text='<%#Eval("Grade")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ticket Issues" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAmount" runat="server" Width="50px" Text='<%#Eval("AMOUNT")%>' CommandArgument='<%#Eval("ID")%>' ToolTip="View Ticket History/Issue New Ticket"
                                                        CommandName="Amount" CssClass="btn btn-success"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpidNew" runat="server" Text='<%#Eval("EMPID")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TO_cityID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTO_cityID" runat="server" Text='<%#Eval("TO_cityID")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LVRD">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkLVRD" runat="server" Width="65px" Text='<%#Eval("LVRD")%>' CommandArgument='<%#Eval("ID")%>' ToolTip="Update Ticket A/c Till Date."
                                                        CommandName="LVRD" CssClass="btn btn-primary"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Refresh" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="LVRD" ID="btnTaskRefresh" runat="server" CommandArgument='<%#Eval("ID")%>'
                                                        CssClass="btn btn-small btn-primary"> <i class="fa fa-refresh"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("ID")%>'
                                                        CommandName="Edt" CssClass="anchor__grd edit_grd "></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDel" runat="server" Text='<%#GetText()%>' CommandArgument='<%#Eval("ID")%>'
                                                        CommandName="Del" CssClass="anchor__grd dlt"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpAirID" runat="server" Text='<%#Eval("ID")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Page">
                                                <ItemTemplate>
                                                    <%--<asp:HyperLink ID="lnkPrint" runat="server" CssClass="anchor__grd edit_grd" text="Print Data" 
                                                    Target="_blank" NavigateUrl='<%#String.Format("AdvanceRcvryBillWisePrint.aspx").ToString()%>'></asp:HyperLink>--%>
                                                    <asp:Button ID="btnPO" runat="server" CssClass="btn btn-danger" CommandArgument="1" CommandName="ViewPage" Text="AirLines NOT configured" Width="130"
                                                        Visible='<%# GetURL(DataBinder.Eval(Container.DataItem, "AirlinesCount").ToString())%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="from_cityID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfrom_cityID" runat="server" Text='<%#Eval("from_cityID")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="bookingClassID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbookingClassID" runat="server" Text='<%#Eval("bookingClassID")%>'> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PassengerTypeID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPassengerTypeID" runat="server" Text='<%#Eval("PassengerTypeID")%>'> </asp:Label>
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
                            <tr>
                                <td>
                                    <asp:Label ID="lblTicketAcc" Text="Ticket Issues" runat="server" Font-Bold="true" Font-Size="14" ForeColor="Blue"></asp:Label>
                                    &nbsp;&nbsp;                                   
                                    Name:
                                    <asp:Label runat="server" Text="" ID="lblEmpName" Font-Size="11" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="rowEmpDetails">
                                <td>
                                    <%--   <asp:UpdatePanel ID="upDetails" runat="server">                                                                                           
                                         <ContentTemplate>--%>
                                    <tr>
                                        <td style="font-size: medium">
                                            <asp:Label runat="server" Text="" ID="lblEmpidTwo" CssClass="hiddencol"></asp:Label>
                                            <asp:Label runat="server" Text="" ID="lblCity"></asp:Label>
                                            Tickets
                                            <asp:Label runat="server" Text="" ID="lblTickets"></asp:Label>
                                            <asp:Label runat="server" Text="" ID="lblToCityID" CssClass="hiddencol"></asp:Label>
                                            Minimum Days of Work: 
                                            <asp:Label runat="server" Text="" ID="lblMinimumWorkingDays"></asp:Label>
                                            ; Days of Work:
                                            <asp:Label runat="server" Text="" ID="lblDaysOfWork"></asp:Label>
                                            <asp:Button ID="btnADD" Text="ADD" runat="server" OnClick="btnADD_Click" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:GridView ID="gvTicketsInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                HeaderStyle-CssClass="tableHead" Width="100%" ShowFooter="true" OnRowCommand="gvTicketsInfo_RowCommand"
                                                CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" OnRowDataBound="gvTicketsInfo_RowDataBound"
                                                OnRowCancelingEdit="gvTicketsInfo_RowCancelingEdit" OnRowDeleting="gvTicketsInfo_RowDeleting" OnRowEditing="gvTicketsInfo_RowEditing"
                                                OnRowUpdating="gvTicketsInfo_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="RecStatus" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRecStatus" runat="server" Text='<%# Eval("RecStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DET_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDETID" runat="server" Text='<%# Eval("DET_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="slno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblSlno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>--%>
                                                            <asp:Label ID="lblSlno" runat="server" Text='<%# Eval("Slno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Passenger Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtEmpName1" runat="server" Text=""></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="detrelationID" HeaderText="detrelationID" ReadOnly="true"
                                                        ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                                                    <asp:TemplateField HeaderText="Relation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRelation" runat="server" Text='<%# Eval("Relation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlRelation" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlRelation1" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="detPassengerTypeID" HeaderText="detPassengerTypeID" ReadOnly="true"
                                                        ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                                                    <asp:TemplateField HeaderText="Passenger Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPassengerType" runat="server" Text='<%# Eval("PassType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlPassengerType" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlPassengerType1" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="detbookingClassID" HeaderText="detbookingClassID" ReadOnly="true"
                                                        ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                                                    <asp:TemplateField HeaderText="Booking Class" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBookingClass" runat="server" Text='<%# Eval("BookingClass") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlBookingClass" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlBookingClass1" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="AirLinesID" HeaderText="AirLinesID" ReadOnly="true"
                                                        ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                                                    <asp:TemplateField HeaderText="AirLines" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAirLines" runat="server" Text='<%# Eval("AirLines") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlAirLines" runat="server" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="GetFare1" AutoPostBack="true"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlAirLines1" runat="server" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="GetFare" AutoPostBack="true"></asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDueDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DueDate") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDueDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DueDate") %>'></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="MyCalendar"
                                                                TargetControlID="txtDueDate" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDueDate1" runat="server" Width="100px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                                                                TargetControlID="txtDueDate1" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Next Due Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNextDueDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NextDueDate") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNextDueDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NextDueDate") %>'></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender8" runat="server" CssClass="MyCalendar"
                                                                TargetControlID="txtNextDueDate" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNextDueDate1" runat="server" Width="100px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender9" runat="server" CssClass="MyCalendar"
                                                                TargetControlID="txtNextDueDate1" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtAmount1" runat="server" Text="0.00" Width="100px" ToolTip='Take management decision to enter amount as full and final.'></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Tickets" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoofTickets" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoofTickets") %>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNoofTickets" runat="server" Width="100px" Text='<%#DataBinder.Eval(Container.DataItem, "NoofTickets") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtNoofTickets1" runat="server" Text="0.00" Width="100px"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle Width="80px" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add Passenger" OnClick="btnADD_Click" CssClass="btn btn-success" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A/c Trans">
                                                        <HeaderStyle HorizontalAlign="Center" Width="15" />
                                                        <ItemStyle HorizontalAlign="Center" Width="15" />
                                                        <ItemTemplate>
                                                            <asp:Button Visible="false" ID="btnJournal" runat="server" Text='<%#GetJVText(DataBinder.Eval(Container.DataItem, "JVTransID").ToString()) %>' OnClick="btnJournal_Click" CssClass="btn btn-success" />
                                                            <asp:Label ID="lblAccTrans" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JVTransID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                                            </asp:GridView>
                                            <tr>
                                                <td>
                                                    <div style="height: 150px;">
                                                        <asp:Button runat="server" ID="btnSave" Text="Ticket Issue" OnClick="btnSave_Click" CssClass="btn btn-success" />
                                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </td>
                                    </tr>
                                    <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
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
