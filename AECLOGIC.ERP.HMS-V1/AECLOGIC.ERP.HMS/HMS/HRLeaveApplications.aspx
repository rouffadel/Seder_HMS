<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="HRLeaveApplications.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.HRLeaveApplicationsV1" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
        }
    </script>
  
    <table id="tblHR" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="1300px">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent" style="height: auto; overflow: auto; display: block;width:1300px">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="ddlWs_hid" runat="server" />

                                            <asp:Label ID="lblAddLeaAppWS" runat="server" Text="Worksite:"></asp:Label>
                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                 OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                           
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label>
                                            <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                          
                                                 Employee:
                                                            <asp:TextBox ID="txtSearchEmp" Height="22px" Width="175px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmp"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtSearchEmp"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                            &nbsp;<asp:Label ID="lblCount" Visible="false" runat="server" ForeColor="Blue" Text="Label"></asp:Label>


                                           
                                            <asp:Label ID="lblMonth" runat="server" Text="Month Wise:"></asp:Label>

                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist"
                                                TabIndex="1" AccessKey="1" ToolTip="[Alt+1]" Width="80">
						<asp:ListItem Value="0">All</asp:ListItem>
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
                                            &nbsp;&nbsp;Year:
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist"
                                                TabIndex="2" AccessKey="2" ToolTip="[Alt+2]" Width="70">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="100px"
                                                TabIndex="3" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="btnSubmit_Click" />
                                            
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
                <asp:GridView ID="gvLeaveApptoHr" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvLeaveApptoHr_RowCommand"
                    OnRowDataBound="gvLeaveApptoHr_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="LID" Visible="false" />
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" Visible="false" />
                        <asp:BoundField HeaderText="Name" DataField="name" />
                        <asp:BoundField HeaderText="App-On" ControlStyle-Font-Bold="true" DataField="AppliedOn" />
                        <asp:BoundField HeaderText="From" DataField="LeaveFrom" />
                        <asp:BoundField HeaderText="Till" DataField="LeaveUntil" />
                        <asp:BoundField HeaderText="Days" DataField="AppliedDays" />
                        <asp:BoundField HeaderText="Reasons" DataField="Reason" />
                        <asp:TemplateField HeaderText="Status" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Suggested From" DataField="GrantedFrom" />
                        <asp:BoundField HeaderText="Suggested Until" DataField="GrantedUntil" />
                        <asp:BoundField HeaderText="Suggested Days" DataField="GrantedDays" />

                        <asp:TemplateField HeaderText="Verified By">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblVerfdBy" runat="server" Text='<%#Bind("GrantedBy")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Admin-Comments" DataField="Comment" />
                        <asp:BoundField HeaderText="Emp-Response" DataField="CommentReply" Visible="false"/>
                        <asp:BoundField HeaderText="Type of Leave" DataField="LeaveType" />

                        <asp:TemplateField HeaderText="Proof" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a id="A1" target="_blank" href='<%# DocNavigateUrlnew(DataBinder.Eval(Container.DataItem, "proof").ToString(),DataBinder.Eval(Container.DataItem, "LID").ToString()) %>'
                                    runat="server" visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Proof1").ToString().ToString()) %>' class="anchor__grd vw_grd">View</a>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkApprove" CommandName="Apprv" CommandArgument='<%#Eval("LID")%>'
                                    runat="server" CssClass="btn btn-success ">Approve</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkreject" CommandName="Rejct" CommandArgument='<%#Eval("LID")%>'
                                    runat="server" CssClass="btn btn-danger">Reject</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                         
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandName="edt" CommandArgument='<%#Eval("LID")%>'
                                    runat="server" CssClass="btn btn-primary ">Process</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
			<asp:BoundField HeaderText="Req. Arr. Date" ControlStyle-Font-Bold="true" DataField="ReqArrDate" />
                        <asp:TemplateField HeaderText="Approver Details">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkInd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LID") %>' CssClass="btn btn-danger link__cust__style" NavigateUrl='<%# String.Format("ViewEmpLeaveDetails.aspx?LID={0}", DataBinder.Eval(Container.DataItem,"LID").ToString()) %>' Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="HRLeaveAppInprocessPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <asp:HiddenField ID="hdnSearchInProcess" Value="0" runat="server" />
            </td>
        </tr>
    </table>
    <table id="tblHREdit" visible="false" runat="server" width="100%" style="border-spacing: 0px; border-collapse: collapse">
        <tr>
            <td colspan="2" align="center">
                <asp:HyperLink ID="hlnkLCB" NavigateUrl="~/LeavesCombinations.aspx" runat="server" Style="display: none"><b>View LeaveCombination</b></asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkViewCalender" runat="server" Font-Bold="True" OnClick="lnkViewCalender_Click">View Calender</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkCloseCal" runat="server" Font-Bold="True" OnClick="lnkCloseCal_Click">Close Calender</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Calendar ID="Cal" Visible="false" runat="server" BorderColor="#CC6600" BorderStyle="Outset">
                    <SelectedDayStyle BackColor="#CC00FF" />
                    <WeekendDayStyle ForeColor="Blue" />
                    <TodayDayStyle BackColor="#CC6600" />
                    <OtherMonthDayStyle ForeColor="Fuchsia" />
                    <NextPrevStyle BackColor="#CCCCCC" />
                    <DayHeaderStyle BackColor="#CC6600" ForeColor="Lime" />
                    <TitleStyle Font-Bold="True" ForeColor="#0000CC" />
                </asp:Calendar>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                <b>EmpID:</b>
            </td>
            <td>
                <asp:Label ID="lblEmpID" runat="server" Font-Bold="True" ForeColor="#CC6600"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                <b>Name:</b>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                &nbsp;&nbsp;
                Date of Join:
                &nbsp;
                 <asp:Label ID="lblDateofJoin" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                &nbsp;&nbsp;
                Available:
                &nbsp;

                  <asp:Label ID="lblAvailable" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvAvailLeaves" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                    Width="40%">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Leave Type" />
                        <asp:BoundField DataField="Bal" HeaderText="Balance" />
                      
                    </Columns>
                </asp:GridView>
            </td>

        </tr>
        <tr>
            <td style="width: 124px">
                <b>Applied On:</b>
            </td>
            <td>
                <asp:Label ID="lblAppliedOn" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                <b>Leave From:</b>
            </td>
            <td>
                <asp:Label ID="lblLevFrom" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                <b>Leave Until:</b>
            </td>
            <td>
                <asp:Label ID="lblLevUntil" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <b>Applied Days:</b>
            </td>
            <td>
                <asp:Label ID="lblnoofdays" runat="server" Font-Bold="True" ForeColor="#CC6600"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <b>Leave Type:</b>
            </td>
            <td>
                <asp:Label ID="lblLeaveType" runat="server" Font-Bold="True" ForeColor="#CC6600"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="width: 124px">
                <b>Reason:</b>
            </td>
            <td>
                <asp:Label ID="lblReason" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>Employee Reply:</b>
            </td>
            <td>
                <asp:Label ID="lblempreply" runat="server" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                <b>Granted From:</b>
            </td>
            <td>
                <asp:TextBox ID="txtGrantedFrom" Width="80px" runat="server" TabIndex="4"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtGrantedFrom"
                    PopupButtonID="txtGrantedFrom" Format="dd MMM yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 124px">
                <b>Granted Until:</b>
            </td>
            <td>
                <asp:TextBox ID="txtGrantedUntil" AutoPostBack="true" Width="80Px" runat="server"
                    OnTextChanged="txtGrantedUntil_TextChanged" TabIndex="5"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtGrantedUntil"
                    PopupButtonID="txtGrantedUntil" Format="dd MMM yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <b>Granted Days:</b>
            </td>
            <td>
                <asp:Label ID="lblGrantedDays" Font-Bold="True" ForeColor="#CC6600" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:LinkButton ID="lnkclearenceview" runat="server" Font-Bold="True" OnClick="lnkViewclearence_Click" CssClass="btn btn-success">View Clearance</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <b>Status:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="droplist"
                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" TabIndex="6">
                    <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                 
                    <asp:ListItem Text="Granted" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <a id="hlnkProof" target="_blank" runat="server" class="btn btn-success">View Proof</a>
            </td>
            <td>
                <asp:Label ID="lblProof" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style>
            <td>
                <b>Comment:</b>
            </td>
            <td style="width: 124px">
                <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" BorderColor="#CC6600"
                    BorderStyle="Outset" Rows="4" Width="250px" TabIndex="7"></asp:TextBox><cc1:TextBoxWatermarkExtender
                        ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtComment" WatermarkCssClass="Watermark"
                        WatermarkText="[Enter Your Comment Here!]">
                    </cc1:TextBoxWatermarkExtender>
            </td>
            <td style="vertical-align: top">

                <asp:GridView ID="gvCmnts" runat="server" AutoGenerateColumns="False" Width="50%"
                    CssClass="gridview" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                    EmptyDataRowStyle-CssClass="EmptyRowData">
                    <Columns>
                        <asp:BoundField HeaderText="Commented By" DataField="CommentedBy" />
                        <asp:BoundField HeaderText="Comment" DataField="Comments" />
                        <asp:BoundField HeaderText="Status" DataField="status" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="divLeaves" runat="server">
                    <table>
                        <tr>
                            <td>Openning Balance</td>
                            <td>
                                <asp:TextBox ID="txtOpenningBal" runat="server" Width="70px"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                            </td>
                        </tr>

                    </table>
                </div>

            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnsave" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnsave_Click"
                    AccessKey="s" TabIndex="8" ToolTip="[Alt+s OR Alt+s+Enter]" />
            </td>
        </tr>
    </table>
    <table id="tblGrantedLeaves" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="1300px">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent" >
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Worksite:"></asp:Label>
                                            <asp:TextBox ID="txtSerachWorkApr" OnTextChanged="GetWorkApr" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSerachWorkApr"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSerachWorkApr"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                           
                                                &nbsp;&nbsp;
                                                <asp:Label ID="Label3" runat="server" Text="Department:"></asp:Label>
                                            <asp:TextBox ID="txtSearchDeptApr" OnTextChanged="GetDeptApr" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtSearchDeptApr"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtSearchDeptApr"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                           
                                                 Employee:
                                                            <asp:TextBox ID="txtSearchEmpApr" AutoPostBack="false" Height="22px" Width="175px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmpApr"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtSearchEmpApr"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>


                                            <asp:Label ID="lblMnth" runat="server" Text="Month Wise:"></asp:Label>

                                            <asp:DropDownList ID="ddlMonthGrant" runat="server" CssClass="droplist"
                                                TabIndex="9" AccessKey="1" ToolTip="[Alt+1]" Width="80">
						<asp:ListItem Value="0">All</asp:ListItem>
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
                                            &nbsp;Year:
                                            <asp:DropDownList ID="ddlYearGrant" TabIndex="10" AccessKey="2" ToolTip="[Alt+2]"
                                                runat="server" CssClass="droplist" Width="60">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnSubmitGrant" runat="server" Text="Search" CssClass="btn btn-primary"
                                                TabIndex="11" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" Width="100px" OnClick="btnSubmitGrant_Click" />                              
                                               
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
                <asp:GridView ID="gvGranted" runat="server" AutoGenerateColumns="False" Width="106%"
                    CssClass="gridview" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                    EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvGranted_RowCommand"
                    AlternatingRowStyle-BackColor="GhostWhite"
                    OnRowDataBound="gvGranted_RowDataBound">
                    <Columns>
                       <%-- <asp:BoundField DataField="LID" Visible="false" />--%>
                         <asp:TemplateField HeaderText="LID" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblLID" runat="server" Text='<%#Eval("LID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" Visible="false" />
                        <asp:BoundField HeaderStyle-Width="100Px" HeaderText="Name" DataField="name" />
                        <asp:BoundField HeaderText="Appld-On" DataField="AppliedOn" />
                        <asp:BoundField HeaderText="From" DataField="LeaveFrom" Visible="False"/>
                        <asp:BoundField HeaderText="Till" DataField="LeaveUntil" Visible="False"/>
                        <asp:BoundField HeaderText="Days" DataField="AppliedDays" Visible="false"/>


                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReason" runat="server" Text="Read"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Status" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Granted From" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblGF" runat="server" Text='<%#FormatInput(Eval("GrantedFrom")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
			<asp:BoundField HeaderText="Granted From" DataField="GrantedFrom" />
                        <asp:BoundField HeaderText="Granted Until" DataField="GrantedUntil" />
                        <asp:BoundField HeaderText="Granted Days" DataField="GrantedDays" HeaderStyle-Width="50Px" />
                        <asp:TemplateField HeaderStyle-Width="30Px" HeaderText="Apprvd By">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblOkby" runat="server" Text='<%#Bind("OkBy")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Width="120Px" HeaderText="Granted On" DataField="GrantedOn" />
                        <asp:BoundField HeaderStyle-Width="150Px" HeaderText="Admin-Comments" DataField="Comment" Visible="False"/>
                        <asp:BoundField HeaderStyle-Width="150Px" HeaderText="Emp-Response" DataField="CommentReply" Visible="False"/>


                        <asp:TemplateField HeaderStyle-Width="30Px" HeaderText="Type of Leave">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLeaveType" runat="server" Text='<%#Bind("LeaveType")%>' CssClass="btn btn-primary"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandName="edt" CssClass="btn btn-primary" CommandArgument='<%#Eval("LID")%>'
                                    runat="server">Process</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCancel" CommandName="can" CssClass="btn btn-primary" CommandArgument='<%#Eval("LID")%>'
                                    runat="server">Cancel</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderText="Air Ticket">
                                <ItemTemplate>       
                                    <asp:FileUpload ID="UploadProof" runat="server" Width="100px" size="50" ToolTip="Select only pdf file." />                     
                                </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Upload" HeaderStyle-Width="50Px">
                                <ItemTemplate>                         
                                     <asp:LinkButton ID="lnkUpload" CommandName="Upld" runat="server" CommandArgument='<%#Eval("LID")%>' CssClass="btn btn-primary" ForeColor="Blue" Width="50px">Upload</asp:LinkButton> 
                                     <asp:LinkButton ID="LinkCncel" CommandName="Cncel" runat="server" CommandArgument='<%#Eval("LID")%>' CssClass="btn btn-primary" ForeColor="Blue" BackColor="#fa603f" Width="50px">Cancel</asp:LinkButton> 
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ticket" ItemStyle-HorizontalAlign ="Center">
                                    <ItemTemplate> 
                                        <a id="A6" target="_blank" href='<%# DataBinder.Eval(Container.DataItem,"FilePath") %>'
                                                        runat="server" class="btn btn-primary">View</a>                                                                 
                                    </ItemTemplate>
                                </asp:TemplateField>
 
                        <asp:TemplateField>
                            <ItemTemplate>
                              <asp:LinkButton ID="lnkVS" CommandName="VS" CssClass="btn btn-success" CommandArgument='<%#Eval("Empid")%>'
                                    runat="server">Vacation Settlement</asp:LinkButton>
									<asp:HyperLink ID="lnkIndVS" runat="server" Text="Leave Details" CssClass="btn btn-danger link__cust__style" NavigateUrl='<%# String.Format("ViewEmpLeaveDetails.aspx?LID={0}", DataBinder.Eval(Container.DataItem,"LID").ToString()) %>' Target="_blank" />
									</ItemTemplate>
								 
                            <ItemStyle HorizontalAlign="Center" />
                             <HeaderStyle Width="50px" />
                             <ItemStyle Width="50px" />
                        </asp:TemplateField>
				        <asp:TemplateField HeaderText="Approver Details" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                        <asp:HyperLink ID="lnkIndVS1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LID") %>' CssClass="btn btn-danger link__cust__style" NavigateUrl='<%# String.Format("ViewEmpLeaveDetails.aspx?LID={0}", DataBinder.Eval(Container.DataItem,"LID").ToString()) %>' Target="_blank" />
					 <a id="A2" target="_blank" href='<%# DocNavigateUrlnew(DataBinder.Eval(Container.DataItem, "proof").ToString(),DataBinder.Eval(Container.DataItem, "LID").ToString()) %>'
                                    runat="server" visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Proof1").ToString().ToString()) %>' class="anchor__grd vw_grd">Proof</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="HRLeaveAppGrantedPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <asp:HiddenField ID="hdnSearchGranted" Value="0" runat="server" />
            </td>
        </tr>
    </table>
    <table id="tblRejected" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane3" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>

                                            <asp:Label ID="Label4" runat="server" Text="Worksite:"></asp:Label>
                                            <asp:TextBox ID="txtSerachWorkRej" OnTextChanged="GetWorkRej" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSerachWorkRej"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtSerachWorkRej"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                           
                                                &nbsp;&nbsp;
                                                <asp:Label ID="Label5" runat="server" Text="Department:"></asp:Label>
                                            <asp:TextBox ID="txtSearchDeptRej" OnTextChanged="GetDeptRej" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtSearchDeptRej"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtSearchDeptRej"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                           
                                                 Employee:
                                                            <asp:TextBox ID="txtSerachEmpRej" Height="22px" Width="175px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSerachEmpRej"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtSerachEmpRej"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                            &nbsp;<asp:Label ID="Label6" Visible="false" runat="server" ForeColor="Blue" Text="Label"></asp:Label>



                                            <asp:Label ID="lblRejDt" runat="server" Text="Month Wise:"></asp:Label>

                                            <asp:DropDownList ID="ddlMonthRej" runat="server" CssClass="droplist"
                                                TabIndex="12" AccessKey="1" ToolTip="[Alt+1]" Width="80">
						<asp:ListItem Value="0">All</asp:ListItem>
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
                                            &nbsp;
                                            Year:
                                            <asp:DropDownList ID="ddlYearRej" runat="server" CssClass="droplist"
                                                TabIndex="13" ToolTip="[Alt+2]" AccessKey="2" Width="60">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                             <asp:Button ID="btnSubmitRej" runat="server" Text="Search" CssClass="btn btn-primary"
                                                 TabIndex="14" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" Width="100px" OnClick="btnSubmitRej_Click" />
                                        </td>                                      
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvRejected" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="gridview" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                    EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvRejected_RowCommand"
                    AlternatingRowStyle-BackColor="GhostWhite">
                    <Columns>
                        <asp:BoundField DataField="LID" Visible="false" />
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" Visible="false" />
                        <asp:BoundField HeaderStyle-Width="300Px" HeaderText="Name" DataField="name" />
                        <asp:BoundField HeaderText="App-On" DataField="AppliedOn" />
                        <asp:BoundField HeaderText="From" DataField="LeaveFrom" />
                        <asp:BoundField HeaderText="Until" DataField="LeaveUntil" />
                        <asp:BoundField HeaderText="Days" DataField="AppliedDays" />
                        <asp:BoundField HeaderStyle-Width="230Px" HeaderText="Reasons" DataField="Reason" />
                        <asp:TemplateField HeaderText="Status" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Width="160Px" HeaderText="Rejected By" DataField="GrantedBy" />
                        <asp:BoundField HeaderStyle-Width="160Px" HeaderText="Rejected On" DataField="GrantedOn" />
                        <asp:BoundField HeaderStyle-Width="200Px" HeaderText="Admin-Comments" DataField="Comment" />
                        <asp:BoundField HeaderStyle-Width="200Px" HeaderText="Emp-Response" DataField="CommentReply" />
                        <asp:BoundField HeaderText="Type of Leave" DataField="LeaveType" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandName="Process" CommandArgument='<%#Eval("LID")%>'
                                    runat="server" CssClass="btn btn-primary ">Process</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>



                   <%--     <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandName="edt" CssClass="anchor__grd vw_grd" CommandArgument='<%#Eval("LID")%>'
                                    runat="server">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="HRLeaveAppRejPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <asp:HiddenField ID="hdnSearchRej" Value="0" runat="server" />
            </td>
        </tr>
    </table> 

      <table id="tblCanceled" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="Accordion5" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane6" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                 <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                             <asp:Label ID="Label7" runat="server" Text="Worksite:"></asp:Label>
                                              <asp:TextBox ID="txtcanceledWS" OnTextChanged="GetWorkRej" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtcanceledWS"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtcanceledWS"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>
                                                &nbsp;&nbsp;

                                             <asp:Label ID="Label8" runat="server" Text="Department:"></asp:Label>
                                            <asp:TextBox ID="txtCanceledDept" OnTextChanged="GetDeptRej" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtCanceledDept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtCanceledDept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>

                                              Employee:
                                                            <asp:TextBox ID="txtCanceledEmp" Height="22px" Width="175px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender12" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtCanceledEmp"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtCanceledEmp"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]">
                                            </cc1:TextBoxWatermarkExtender>

                                              <asp:Label ID="Label9" runat="server" Text="Month Wise:"></asp:Label>

                                            <asp:DropDownList ID="ddlCanceledMnth" runat="server" CssClass="droplist"
                                                TabIndex="12" AccessKey="1" ToolTip="[Alt+1]" Width="80">
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
                                            &nbsp;
                                               &nbsp;
                                            Year:
                                            <asp:DropDownList ID="ddlyearCanceled" runat="server" CssClass="droplist"
                                                TabIndex="13" ToolTip="[Alt+2]" AccessKey="2" Width="60">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                             <asp:Button ID="btnCanceledsearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                                 TabIndex="14" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" Width="100px"  />
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
                 <asp:GridView ID="gvCanceled" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="gridview" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                    EmptyDataRowStyle-CssClass="EmptyRowData"
                    AlternatingRowStyle-BackColor="GhostWhite">
                    <Columns>
                        <asp:BoundField DataField="LID" Visible="false" />
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" Visible="false" />
                        <asp:BoundField HeaderStyle-Width="300Px" HeaderText="Name" DataField="name" />
                        <asp:BoundField HeaderText="App-On" DataField="AppliedOn" />
                        <asp:BoundField HeaderText="From" DataField="LeaveFrom" />
                        <asp:BoundField HeaderText="Until" DataField="LeaveUntil" />
                        <asp:BoundField HeaderText="Granted On" DataField="GrantedOn" />
                         <asp:BoundField HeaderText="Granted From" DataField="GrantedFrom" />
                         <asp:BoundField HeaderText="Granted Until" DataField="GrantedUntil" />
                        <asp:BoundField HeaderText="Canceled By" DataField="CanceledBy" />

                    </Columns>
               </asp:GridView>
            </td>
               </tr>
           <tr>
            <td style="height: 17px">
                <uc1:Paging ID="HRLeaveAppCanPaging" runat="server" />
            </td>
        </tr>
          </table>

</asp:Content>
