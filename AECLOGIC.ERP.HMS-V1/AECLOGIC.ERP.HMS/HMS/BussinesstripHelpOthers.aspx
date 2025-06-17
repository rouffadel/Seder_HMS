<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BussinesstripHelpOthers.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMSV1.BussinesstripHelpOthersV1" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
  <%--  <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>--%>
            <asp:Label runat="server" ID="Label1" Text="" Font-Size="14px"></asp:Label>
            <style>
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script language="javascript" type="text/javascript">
                function GETEmp_ID(source, eventArgs) {
                    var HdnKey = eventArgs.get_value();
                    document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
                }
                function validatesave() {
                    if (document.getElementById('<%=  ddlEmp_hid.ClientID  %>').value == "") {
                        alert("Please Select Employee!");
                        document.getElementById('<%=TxtEmp.ClientID %>').focus();
                        return false;
                    }
                    if (document.getElementById('<%=  ddlTravelMode.ClientID %>').selectedIndex == 0) {
                        alert("Select Travel Mode!");
                        document.getElementById('<%=ddlTravelMode.ClientID %>').focus();
                        return false;
                    }
                   <%-- if (document.getElementById('<%=  ddlBookingClass.ClientID%>').selectedIndex == 0) {
                        alert("Select  Booking Class!");
                        document.getElementById('<%=ddlBookingClass.ClientID%>').focus();
                        return false;
                    }--%>
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
                //$("#ddlTravelMode").change(function () {
                //    var TravelValue = $('#ddlTravelMode').val();
                //    if (TravelValue == "1") {
                //        $('#ddlTravelMode').css('display', 'block');
                //    }
                //});
                function GetSelectedTextValue(ddlFruits) {
                    debugger;
                    var selectedText = ddlFruits.options[ddlFruits.selectedIndex].innerHTML;
                    var selectedValue = ddlFruits.value;
                    if (selectedValue == "1") {
                        $("#lblbooking").show();
                        $('#ContentPlaceholder1_lblbooking').css('display', 'block');
                        $("#ddlBookingClass").show();
                        $('#ContentPlaceholder1_ddlBookingClass').css('display', 'block');
                    }
                    else
                    {
                        $("#lblbooking").hide();
                        $('#ContentPlaceholder1_lblbooking').css('display', 'none');
                        $("#ddlBookingClass").hide();
                        $('#ContentPlaceholder1_ddlBookingClass').css('display', 'none');
                    }
                }
            </script>
            <table id="tblAdd" visible="false" runat="server" width="70%">
                <tr>
                    <td>
                        <b>Employee:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdn_ID" runat="server" />
                        <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                        <asp:TextBox ID="TxtEmp" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="TxtEmp"
                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="TxtEmp"
                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtFilter" Visible="false" runat="server" CssClass="droplist" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFilter"
                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]"></cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 21px"></td>
                </tr>
                <tr>
                    <td>
                        <b>Travel Mode:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTravelMode" runat="server" CssClass="droplist" TabIndex="2"  onchange="GetSelectedTextValue(this)" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label id="lblbooking" runat="server"  Text="Booking Class:" style="display: none;"></asp:Label>
                        <%--<b>Booking Class:</b><span style="color: #ff0000">*</span>--%>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBookingClass" runat="server" CssClass="droplist" style="display: none;" TabIndex="3"></asp:DropDownList>
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
                        <b>To Worksite:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWorksite" runat="server" CssClass="droplist" TabIndex="5"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>From Date</strong><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" Width="70Px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="txtFrom" Format="dd MMM yyyy"
                            PopupButtonID="txtFrom"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>&nbsp;To Date</strong><span style="color: #ff0000">*</span></td>
                    <td>
                        <asp:TextBox ID="txtTo" Width="70Px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="Calendarextender2" runat="server" TargetControlID="txtTo" Format="dd MMM yyyy"
                            PopupButtonID="txtUpto"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Remarks:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRemarks" Text="" Width="180px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Document:</td>
                    <td>
                        <asp:FileUpload ID="fudDocument" runat="server" Width="100%" TabIndex="1" AccessKey="1"
                            ToolTip="[Alt+1]" /></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" ToolTip="Add" CssClass="btn btn-success" OnClientClick="javascript:return validatesave();"
                            runat="server" Text="Add" OnClick="btnSave_Click" AccessKey="s" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvRemiItems" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" OnRowDeleting="gvRemiItems_RowDeleting"
                            CssClass="gridview"
                            Width="100%" OnRowCommand="gvRemiItems_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Travel Mode ID" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTravelModeID" runat="server" Text='<%#Eval("TravelModeID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Travel Mode" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTravelMode" runat="server" Text='<%#Eval("TravelMode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Worksite ID" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblworksiteid" runat="server" Text='<%#Eval("worksiteid")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Worksite" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblworksite" runat="server" Text='<%#Eval("worksite")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Booking Class ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookingclassID" runat="server" Text='<%#Eval("BookingClassID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Booking Class" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookingclass" runat="server" Text='<%#Eval("BookingClass")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromCity" runat="server" Text='<%#Eval("fromCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFCity" runat="server" Text='<%#Eval("fCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToCity" runat="server" Text='<%#Eval("ToCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTCity" runat="server" Text='<%#Eval("TCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From Date" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To Date" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Filetype">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfiletype" runat="server" Text='<%#Eval("filetype")%>'></asp:Label>
                                        <asp:Label ID="lblFilePath" runat="server" Width="100%" TabIndex="1" AccessKey="1" Visible="false" Text='<%#Eval("filePath")%>'
                                            ToolTip="[Alt+1]" /></td>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnlEdit" CommandName="Edt" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("EmpID")%>'
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnlDelete" CommandName="Del" CssClass="anchor__grd dlt" CommandArgument='<%#Eval("EmpID")%>'
                                            runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server"
                            CssClass="btn btn-primary" Text="Save" />
                    </td>
                </tr>
            </table>
            <table id="tblView" runat="server" width="100%" visible="false">
                <tr>
                    <td>
                        <cc1:Accordion ID="gvViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="106px" Width="100%">
                            <Panes>
                                <cc1:AccordionPane ID="gvViewAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>ID:</b>
                                                    <asp:TextBox ID="txtID" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <asp:Button ID="btnFilter" CssClass="btn btn-primary" runat="server" Text="Search" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="btnFilter_Click" TabIndex="4" />
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
                        <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvView_RowCommand"
                            OnRowDataBound="gvView_RowDataBound"
                            CssClass="gridview">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproval" AutoPostBack="true" OnCheckedChanged="chkApproval_CheckedChanged"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRSelect" AutoPostBack="true" OnCheckedChanged="chkRSelect_CheckedChanged"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" Text='<%#Eval("Id") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" runat="server" Text='<%#Eval("Empid") %>' ToolTip='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requested Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("MDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Travel Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArrivalDate" runat="server" Text='<%#Eval("ArrivalDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EStatus" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEStatus" runat="server" Text='<%#Eval("EStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreated" runat="server" Text='<%#Eval("Created") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" CssClass="btn btn-success" CommandArgument='<%#Eval("ID") %>' CommandName="View"
                                            runat="server">Req Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd " CommandArgument='<%#Eval("ID") %>' CommandName="Edt"
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkApprove" CssClass="btn btn-success" CommandArgument='<%#Eval("ID") %>' CommandName="Apr"
                                            runat="server">Approve</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger" CommandArgument='<%#Eval("ID") %>' CommandName="Rej"
                                            runat="server">Reject</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkACC" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("ID") %>' CommandName="ACC"
                                            runat="server" Visible="false">A/C Post</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- the above line put at the end of the gridview --%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkusinessView" CssClass="btn btn-success" CommandArgument='<%#Eval("ID") %>' CommandName="BussinessView"
                                            runat="server">Approver Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvChildDetails" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvChildDetails_RowCommand"
                            OnRowDataBound="gvChildDetails_RowDataBound"
                            CssClass="gridview">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Travel Mode ID" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTravelModeID" runat="server" Text='<%#Eval("TravelModeID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Travel Mode" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTravelMode" runat="server" Text='<%#Eval("TravelMode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Booking Class ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookingclassID" runat="server" Text='<%#Eval("BookingClassID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Booking Class" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookingclass" runat="server" Text='<%#Eval("BookingClass")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromCity" runat="server" Text='<%#Eval("fromCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From City" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFCity" runat="server" Text='<%#Eval("fCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToCity" runat="server" Text='<%#Eval("ToCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTCity" runat="server" Text='<%#Eval("TCity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="To Worksite">
                                    <ItemTemplate>
                                        <asp:Label ID="lblworksite" runat="server" Text='<%#Eval("Worksite")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From Date" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To Date" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100Px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransid" Visible="false" runat="server" Text='<%#Eval("TransID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Proof">
                                    <ItemTemplate>
                                        <a id="A6" target="_blank" href='<%# Eval("FilePath") %>'
                                            runat="server" class="btn btn-primary" Visible='<%# DataBinder.Eval(Container.DataItem,"FileVisiable")%>'>View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExpenses" CssClass="btn btn-primary" CommandArgument='<%#Eval("ID") %>' CommandName="Exp"
                                            runat="server">Expense</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetails" CssClass="btn btn-primary" CommandArgument='<%#Eval("ERID") %>' CommandName="Det"
                                            runat="server" Visible="false">Details</asp:LinkButton>
                                        <asp:Label ID="lblERID" Visible="false" runat="server" Text='<%#Eval("ERID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd " CommandArgument='<%#Eval("ERID") %>' CommandName="Edt"
                                            runat="server" Visible="false">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementPendingRejPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvShow" CssClass="gridview" runat="server" AutoGenerateColumns="false"
                            Width="80%" >
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ReimburseItem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRate" Text='<%#Eval("UnitRate") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQty" Text='<%#Eval("Qty") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>
            </table>
            <table id="tblExpenses" runat="server" visible="false">
                <tr>
                    <td colspan="2">
                        <b>Reimburse Items</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListBox ID="lstItems" Rows="6" Width="300Px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" ToolTip="Add multiple Items at once" CssClass="btn btn-success"
                            runat="server" Text="Add" OnClick="btnAdd_Click" AccessKey="s" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvExpenses" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="90%" OnRowCommand="gvExpenses_RowCommand" CssClass="gridview">
                            <Columns>
                                <asp:TemplateField Visible="false" HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" Text='<%#Eval("ID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80Px" HeaderText="ReimburseItem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units of Measure" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' DataTextField="AU_Name"
                                            DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                            runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" Text='<%#Eval("UnitRate") %>' OnTextChanged="txtUnitrete_TextChanged"
                                            AutoPostBack="true" Width="40" runat="server" ReadOnly="true"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl5" runat="server" TargetControlID="txtRate" ValidChars="." />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount per Day">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("Qty") %>' Style="text-align: right;"
                                            OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" Width="40" ReadOnly="true"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ID="Fl6" runat="server" TargetControlID="txtQty" ValidChars="." />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" Text='<%#Eval("Amount")%>' AutoPostBack="true" Width="60" runat="server" ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnlDelete" CommandName="Del" CommandArgument='<%#Eval("ERID")%>'
                                            runat="server" CssClass="anchor__grd dlt">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnExpenses" OnClick="btnExpenses_Click" runat="server" CssClass="btn btn-success" Text="Save" />
                    </td>
                </tr>
            </table>

       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel> AssociatedUpdatePanelID="UpdatePanel1"--%>
    <asp:UpdateProgress ID="upProg" runat="server">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
