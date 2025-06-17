<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master" 
    CodeBehind="AdvancedLeaveApplication.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.AdvancedLeaveApplicationV1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <style>
        .tooltipCss {
            position: absolute;
            border: 1px solid gray;
            margin: 1em;
            padding: 3px;
            background: #A4D162;
            font-family: Trebuchet MS;
            font-weight: normal;
            color: black;
            font-size: 11px;
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
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
            <%--  alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }
        function GetEmpID(source, eventArgs) {
            debugger;
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=Emp_hid.ClientID %>').value = HdnKey;
         }
        function GETEmp_ID(source, eventArgs) {
                    var HdnKey = eventArgs.get_value();
                    document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
                }
        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gdvAttend.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gdvAttend.ClientID %>');
            var TargetChildControl = "chkAll";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function validateCheckBox() {
            var isValid = false;
            var gvChk = document.getElementById('<%= gdvAttend.ClientID %>');
            for (var i = 1; i < gvChk.rows.length; i++) {
                var chkInput = gvChk.rows[i].getElementsByTagName('input');
                if (chkInput != null) {
                    if (chkInput[0].type == "checkbox") {
                        if (chkInput[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            alert("Please select atleast one Employee.");
            return false;
        }
        //
        function NoOfLeaveValidation() {
            if (!chkNumber('<%=txtReqLeaves.ClientID %>', ' leaves', true, ' ')) {
                return false;
            }
            if (!chkDropDownList('<%=ddlSumLeaveType.ClientID%>', 'LeaveType'))
                return false;
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <div id="dvLeaveApply" runat="server" visible="true">
            </div>
            <table id="tblmain" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td colspan="2" class="pageheader">Available Leaves:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvEMPID" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                            Width="40%">
                            <Columns>
                                <asp:BoundField DataField="EmpID" HeaderStyle-Width="3%" HeaderText="EmpID" />
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="DateOfJoin" HeaderText="Date Of Join" />
                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                <asp:BoundField DataField="BalC" HeaderText="Closing Balance" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 100px">
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
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Leave Type &nbsp;
                        <asp:DropDownList  ID="ddlLeaveTypeSelf" CssClass="droplist" runat="server" DataTextField="Name"
    AutoPostBack="true" DataValueField="TypeId" OnSelectedIndexChanged="ddlLeaveTypeSelf_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    </tr>
                <tr>
                    <td colspan="2">
                       &nbsp;
                            <asp:DropDownList ID="ddlSumLeaveType" CssClass="droplist" runat="server" Enabled="false"
                                TabIndex="5">
                            </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:RadioButton ID="rbdays" AutoPostBack="true" runat="server" GroupName="show" Text="Days"
                            OnCheckedChanged="rbdays_CheckedChanged" Style="font-weight: bold" Visible="false" />
                        <asp:RadioButton ID="rbperiod" AutoPostBack="true" runat="server" GroupName="show" Text="Period" Style="font-weight: bold"
                            OnCheckedChanged="rbperiod_CheckedChanged" Checked="True" />
                    </td>
                </tr>
                <tr id="lblEnterleaves" runat="server" visible="false">
                    <td align="left" colspan="2">
                        <b>&nbsp;Enter Required Leaves:</b>&nbsp;&nbsp;
                <asp:TextBox ID="txtReqLeaves" runat="server" TabIndex="1"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMELeaves" runat="server" TargetControlID="txtReqLeaves"
                            WatermarkText="[Enter No Of Leaves]"></cc1:TextBoxWatermarkExtender>
                        &nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Font-Bold="True"
                            OnClick="btnSubmit_Click" Text="Submit" OnClientClick="javascript:return NoOfLeaveValidation();"
                            AccessKey="i" TabIndex="2" ToolTip="[Alt+i OR Alt+i+Enter]" Visible="false" />
                    </td>
                </tr>
                <tr id="lblperiod" runat="server" visible="true">
                    <td colspan="2">
                        <b>&nbsp;Leave From:</b>&nbsp;&nbsp;
                <asp:TextBox ID="txtStPrd" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtDatePlaceIssue"
                            TargetControlID="txtStPrd" Format="dd MMM yyyy"></cc1:CalendarExtender>
                        <b>&nbsp;Leave Untill:</b>&nbsp;&nbsp;
                <asp:TextBox ID="txtEndPrd" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtDatePlaceIssue"
                            TargetControlID="txtEndPrd" Format="dd MMM yyyy"></cc1:CalendarExtender>
                    </td>
                </tr>                
            </table>
           
            <table id="tblTravel" runat="server" >
                <tr>
                    <td>
                        <asp:Label ID="lblTSector" runat="server" font-Bold="True" Text="Travel Sector" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <asp:RadioButton ID="rdbTktCompany" runat="server" GroupName="Ticket" AutoPostBack="true" Checked="true" Style="font-weight: bold" Text="Ticket from company" OnCheckedChanged="rdbTktCompany_CheckedChanged"/>                                
                         <asp:RadioButton ID="rdbTktSelf" runat="server" GroupName="Ticket" AutoPostBack="true" Style="font-weight: bold" Text="Self Ticket" OnCheckedChanged="rdbTktSelf_CheckedChanged"/> 
                         <asp:RadioButton ID="rdbNone" runat="server" GroupName="Ticket" AutoPostBack="true" Style="font-weight: bold" Text="None" OnCheckedChanged="rdbNone_CheckedChanged"/><br />                         
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>From Place:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFrPlace" runat="server" CssClass="droplist" TabIndex="4" Width="140px" Font-Size="Small"></asp:DropDownList>
                    </td>
                    <td>
                        <b>To Place:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlToPlace" runat="server" CssClass="droplist" TabIndex="5" Width="140px" Font-Size="Small"></asp:DropDownList>
                    </td>
                </tr>               
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" font-Bold="True" Text="Exit Re-Entry" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                        <td colspan="2">
                            <asp:RadioButton ID="rdbExitEntryReq" GroupName="Entry" runat="server" Style="font-weight: bold" Text="Required" Checked="True" AutoPostBack="true" OnCheckedChanged="rdbExitEntryReq_CheckedChanged"/>                              
                            <asp:RadioButton ID="rdbExitEntryNotReq" GroupName="Entry" runat="server" Style="font-weight: bold" Text="Not Required" AutoPostBack="true" OnCheckedChanged="rdbExitEntryNotReq_CheckedChanged" />                                                                                   
                        </td>
                </tr> 
                <tr>
                    <td>
                         <asp:Label ID="lblNoOfDays" runat="server" Text="No. Of Days" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNoDays" runat="server" AutoPostBack="true" DataTextField="Days" DataValueField="Days"  CssClass="droplist" TabIndex="5" Width="100px" Font-Size="Small">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="15"></asp:ListItem>
                            <asp:ListItem Value="30"></asp:ListItem>
                            <asp:ListItem Value="45"></asp:ListItem>
                            <asp:ListItem Value="60"></asp:ListItem>
                            <asp:ListItem Value="75"></asp:ListItem>
                            <asp:ListItem Value="90"></asp:ListItem>
                            <asp:ListItem Value="120"></asp:ListItem>
                        </asp:DropDownList>                        
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGuarantor" runat="server" font-Bold="True" Text="Guarantor" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                        <td colspan="2">
                            <asp:RadioButton ID="rdbGuarantorReq" GroupName="Guarantor" runat="server" AutoPostBack="true" Checked="true" Style="font-weight: bold" Text="Required" OnCheckedChanged="rdbGuarantorReq_CheckedChanged" />                              
                            <asp:RadioButton ID="rdbGuarantorNotReq" GroupName="Guarantor" runat="server" AutoPostBack="true" Style="font-weight: bold" Text="Not Required" OnCheckedChanged="rdbGuarantorNotReq_CheckedChanged"  />                            
                        </td>
                </tr> 
                <tr>
                    <td>
                        <b>Guarantor ID:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdn_ID" runat="server" />
                        <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                        <asp:TextBox ID="txtSearchEmpApr" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters="" Enabled="True"
                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="txtSearchEmpApr"
                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtSearchEmpApr"
                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee ID]"></cc1:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtFilter" Visible="false" runat="server" CssClass="droplist" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtFilter"
                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]"></cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;<asp:Button ID="btnsubmitPrd" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                        CssClass="btn btn-success" Font-Bold="True" />
                    </td>
                </tr>
            </table>                
            <asp:Panel ID="pnltblAllowed" runat="server" CssClass="DivBorderOlive" Visible="false"
                Width="80%">
                <table id="tblAllowed" visible="false" runat="server" width="100%">
                    <tr id="forDays">
                        <td style="width: 157px">
                            <b>Leave From:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAFrom" runat="server" TabIndex="3" OnTextChanged="txtAFrom_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtDatePlaceIssue"
                                TargetControlID="txtAFrom" Format="dd MMM yyyy"></cc1:CalendarExtender>
                        </td>
                        <td>
                            <b>Leave Until:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAUntil" runat="server" TabIndex="4"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDatePlaceIssue"
                                TargetControlID="txtAUntil" Format="dd MMM yyyy"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr id="forPeriod">
                        <td style="width: 157px">
                            <b>No.Of Days Leaves:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoofDays" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 166px">
                            <b>Please Enclose Reason:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApReson" runat="server" BackColor="White" BorderColor="#CC6600"
                                BorderStyle="Inset" Rows="8" TextMode="MultiLine" Width="400Px" Height="80px"
                                TabIndex="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUploadProof" runat="server" Text="Upload Proof"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 157px"></td>
                        <td>
                            <asp:Button ID="btnApply" CssClass="btn btn-primary" runat="server" Text="Apply" OnClick="btnApply_Click"
                                AccessKey="s" TabIndex="5" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnltblNotAllow" runat="server" CssClass="DivBorderOlive" Visible="false"
                Width="60%">
                <table id="tblNotAllowed" visible="false" runat="server" width="100%">
                    <tr id="NotAllowDays">
                        <td style="width: 96px">
                            <b>Leave From: </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNAFrom" runat="server" TabIndex="6" AutoPostBack="true" OnTextChanged="txtNAFrom_TextChanged" Width="15%"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDatePlaceIssue"
                                TargetControlID="txtNAFrom" Format="dd MMM yyyy"></cc1:CalendarExtender>
                            &nbsp;&nbsp;
                    <b>Leave Until:</b>
                            <asp:TextBox ID="txtNAUntil" runat="server" TabIndex="7" Enabled="false" Width="15%"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtDatePlaceIssue"
                                TargetControlID="txtNAUntil" Format="dd MMM yyyy"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr id="NotAllowforPeriod">
                        <td style="width: 26px">
                            <b>No.Of Days Leaves:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRsnleaves" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 56px">
                            <b>Enclose Reason:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReason" runat="server" BackColor="White" BorderColor="#CC6600"
                                BorderStyle="Inset" Rows="8" TextMode="MultiLine" Width="400Px" Height="80px"
                                TabIndex="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 26px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px; width: 26px;">&nbsp;
                        </td>
                        <td style="height: 25px">&nbsp;&nbsp;
                <asp:Button ID="btnRequest" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnRequest_Click"
                    AccessKey="s" TabIndex="9" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div id="dvgr" runat="server" visible="false">
                <table id="tblHREdit" runat="server" width="100%">
                    <tr>
                        <td colspan="2" class="pageheader">Enter Employee Leave Details:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                    Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAddLeaAppWS" runat="server" Text="Worksite:"></asp:Label>
                                                        <asp:DropDownList ID="ddlWS" runat="server" Visible="false" CssClass="droplist" ToolTip="[Alt+w OR Alt+w+Enter]" AutoPostBack="true" OnSelectedIndexChanged="ddlWS_SelectedIndexChanged1"
                                                            TabIndex="16" AccessKey="w">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="ddlWs_hid" runat="server" />
                                                        <asp:HiddenField ID="Emp_hid" runat="server" />
                                                        <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            OnClientItemSelected="GetID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblDept" runat="server" Text="Department:"></asp:Label>
                                                        <asp:DropDownList ID="ddlDept" Visible="false" runat="server" CssClass="droplist" TabIndex="17" AccessKey="1"
                                                            ToolTip="[Alt+1]">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                        Employee:<asp:DropDownList ID="ddlEmp" Visible="false" AutoPostBack="true" CssClass="droplist" runat="server"
                                                            TabIndex="7" AccessKey="6" ToolTip="[Alt+6]">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtSearchEmp"  Height="22px" Width="175px" runat="server" ></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="txtSearchEmp"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" OnClientItemSelected="GetEmpID"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchEmp"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]"></cc1:TextBoxWatermarkExtender>
                                                        &nbsp;<asp:Label ID="lblCount" Visible="false" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
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
                            <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gdvAttend_RowCommand"
                                CssClass="gridview" OnRowDataBound="gdvAttend_RowDataBound" Width="100%">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="White" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle />
                                        <HeaderStyle />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll(this);" TabIndex="19" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" TabIndex="20" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmpID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmpName" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Applied on" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAppOn" OnTextChanged="txtAppOn_TextChanged" Enabled="false" runat="server" Width="100px"></asp:TextBox>
                                            <%--<cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtSpentOn"
                                        TargetControlID="txtAppOn" Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>--%>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAppOn" Format="dd MMM yyyy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applied From">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrsntFrm" runat="server" Width="80px" OnTextChanged="txtGrsntFrm_Click" AutoPostBack="true"></asp:TextBox>
                                            <%--OnTextChanged="Unnamed_Click" AutoPostBack="true" --%>
                                            <%--  <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtGrsntFrm"
                                        Format="dd MMM yyyy">
                                    </cc1:CalendarExtender>--%>
                                            <cc1:CalendarExtender ID="CalendarExtender4" CssClass="MyCalendar" runat="server"
                                                TargetControlID="txtGrsntFrm" Format="dd MMM yyyy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applied Untill">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrsntUtl" runat="server" Width="80px" OnTextChanged="Unnamed_Click" AutoPostBack="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtGrsntUtl" CssClass="MyCalendar"
                                                TargetControlID="txtGrsntUtl" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No of Days">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNoofDays" runat="server" Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proof">
                                        <ItemTemplate>
                                            <asp:FileUpload ID="UploadProof" runat="server" Width="180px" Text="..." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReason" runat="server" Width="150px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available" visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblLAvl" runat="server" Text='<%#Bind("TotLeave")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Join">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoj" runat="server" Text='<%#Bind("DateOfJoin")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="130" />
                                        <HeaderStyle Width="130" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="130" ID="ddlLeaveType" CssClass="droplist" runat="server" DataTextField="Name"
                                                AutoPostBack="true" DataValueField="TypeId" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Bal." HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="180" />
                                        <HeaderStyle Width="180" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="180" ID="grdddlLeaveType" CssClass="droplist" runat="server" DataTextField="Name"
                                                AutoPostBack="false" DataValueField="LeaveType" Enabled="false" >
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSub" runat="server" CommandName="sub" Text="Submit" CssClass="btn btn-success"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                        </td>
                    </tr>
                    <tr>
                      <td>  
                        <asp:Label ID="lblAlBal" runat="server" font-Bold="True" Text="AL Bal :" Font-Size="Medium"></asp:Label>
                         <asp:Label ID="lblALBalValue" runat="server" font-Bold="True" Text="0" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                    <tr>
                        <td>
                             <table id="tblTravelHelpOthers" runat="server" >
                <tr>
                    <td>
                        <asp:Label ID="lblHelpOther" runat="server" font-Bold="True" Text="Travel Sector" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <asp:RadioButton ID="rdbHelpTktCompany" runat="server" GroupName="Ticket" AutoPostBack="true" Checked="true" Style="font-weight: bold" Text="Ticket from company" OnCheckedChanged="rdbHelpTktCompany_CheckedChanged"/>                                
                         <asp:RadioButton ID="rdbHelpTktSelf" runat="server" GroupName="Ticket" AutoPostBack="true" Style="font-weight: bold" Text="Self Ticket" OnCheckedChanged="rdbHelpTktSelf_CheckedChanged"/>                         
                         <asp:RadioButton ID="rdbHelpNone" runat="server" GroupName="Ticket" AutoPostBack="true" Style="font-weight: bold" Text="None" OnCheckedChanged="rdbHelpNone_CheckedChanged"/><br />                         
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>From Place:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHelpFrPlace" runat="server" CssClass="droplist" TabIndex="4" Width="140px" Font-Size="Small"></asp:DropDownList>
                    </td>
                    <td>
                        <b>To Place:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHelpToPlace" runat="server" CssClass="droplist" TabIndex="5" Width="140px" Font-Size="Small"></asp:DropDownList>
                    </td>
                </tr>               
                <tr>
                    <td>
                        <asp:Label ID="lblExitEntry" runat="server" font-Bold="True" Text="Exit Re-Entry" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                        <td colspan="2">
                            <asp:RadioButton ID="rdbHelpExitEntryReq" GroupName="Entry" runat="server" Style="font-weight: bold" Text="Required" Checked="True" AutoPostBack="true" OnCheckedChanged="rdbHelpExitEntryReq_CheckedChanged"/>                              
                            <asp:RadioButton ID="rdbHelpExitEntryNotReq" GroupName="Entry" runat="server" Style="font-weight: bold" Text="Not Required" AutoPostBack="true" OnCheckedChanged="rdbHelpExitEntryNotReq_CheckedChanged" />                                                                                   
                        </td>
                </tr> 
                <tr>
                    <td>
                         <asp:Label ID="lblHelpNoOfDays" runat="server" Text="No. Of Days" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHelpNoDays" runat="server" AutoPostBack="true" DataTextField="Days" DataValueField="Days"  CssClass="droplist" TabIndex="5" Width="100px" Font-Size="Small">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="15"></asp:ListItem>
                            <asp:ListItem Value="30"></asp:ListItem>
                            <asp:ListItem Value="45"></asp:ListItem>
                            <asp:ListItem Value="60"></asp:ListItem>
                            <asp:ListItem Value="75"></asp:ListItem>
                            <asp:ListItem Value="90"></asp:ListItem>
                            <asp:ListItem Value="120"></asp:ListItem>
                        </asp:DropDownList>                        
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblHelpGuarantor" runat="server" font-Bold="True" Text="Guarantor" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                        <td colspan="2">
                            <asp:RadioButton ID="rdbHelpGuarantorReq" GroupName="Guarantor" runat="server" AutoPostBack="true" Checked="true" Style="font-weight: bold" Text="Required" OnCheckedChanged="rdbHelpGuarantorReq_CheckedChanged" />                              
                            <asp:RadioButton ID="rdbHelpGuarantorNotReq" GroupName="Guarantor" runat="server" AutoPostBack="true" Style="font-weight: bold" Text="Not Required" OnCheckedChanged="rdbHelpGuarantorNotReq_CheckedChanged"  />                            
                        </td>
                </tr> 
                <tr>
                    <td>
                        <b>Guarantor ID:</b><span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <asp:TextBox ID="txtHelpSearchEmpApr" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters="" Enabled="True"
                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="txtHelpSearchEmpApr"
                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtHelpSearchEmpApr"
                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee ID]"></cc1:TextBoxWatermarkExtender>
                        <asp:TextBox ID="TextBox2" Visible="false" runat="server" CssClass="droplist" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtFilter"
                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]"></cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnsave" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnsave_Click"
                                OnClientClick="return validateCheckBox();" TabIndex="21" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                </table>
                    </tr>
                    </table>
                
               
            </div>
            <table id="tblView" runat="server" visible="true" width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" Visible="false" runat="server" Text="Worksite:"></asp:Label>
                                                    <asp:TextBox ID="txtworksiteemp" Visible="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtworksiteemp"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtworksiteemp"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblemp" Visible="false" runat="server" Text="Employee ID/Name:"></asp:Label>
                                                    <asp:TextBox ID="txtEmp" Visible="false" Height="22px" Width="175px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtEmp"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtEmp"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]"></cc1:TextBoxWatermarkExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
                                                    &nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" Font-Bold="true"
                                                TabIndex="10" AccessKey="1"
                                                ToolTip="[Alt+1]">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                                    &nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlYear" runat="server" TabIndex="11" AccessKey="2" Font-Bold="true"
                                                ToolTip="[Alt+2]" CssClass="droplist">
                                            </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="100px"
                                                TabIndex="13" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="btnSubmit_Click1" />
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
                        <asp:GridView ID="gvLeaves" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            OnRowCommand="gvLeaves_RowCommand" OnRowDataBound="gvLeaves_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="LID" DataField="LID" Visible="true" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Empid" DataField="Empid" />
                                <asp:BoundField HeaderText="Date" DataField="AppliedOn" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Days" DataField="AppliedDays" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="From" DataField="LeaveFrom" />
                                <asp:BoundField HeaderText="To" DataField="LeaveUntil" />
                                <asp:BoundField HeaderText="Reason" DataField="Reason" />
                                <asp:BoundField HeaderText="Issued From" DataField="GrantedFrom" />
                                <asp:BoundField HeaderText="Issued Until" DataField="GrantedUntil" />
                                <asp:BoundField HeaderText="Issued Days" DataField="GrantedDays" />
                                <asp:BoundField HeaderText="Issued By" DataField="GrantedBy" />
                                <asp:BoundField HeaderText="Issued On" DataField="GrantedOn" />
                                <asp:BoundField HeaderText="HR-Comment" DataField="Comment" />
                                <asp:BoundField HeaderText="Reply To HR" DataField="CommentReply" />
                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Type of Leave" DataField="LeaveType" />
                                
                                <asp:BoundField HeaderText="Fly Ticket" DataField="FileName" />                                 
                                
                                <%--  <asp:CommandField ShowSelectButton="True" SelectText="Download" ControlStyle-Font-Bold="true" ControlStyle-ForeColor="Blue">
                                <ControlStyle ForeColor="Blue"></ControlStyle>
                                </asp:CommandField> --%>                            
                                
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign ="Center">
                                    <ItemTemplate> 
                                        <a id="A6" target="_blank" href='<%# DataBinder.Eval(Container.DataItem,"FilePath") %>'
                                                        runat="server" class="btn btn-primary">Download</a>                                                                 
                                    </ItemTemplate>
                                </asp:TemplateField>   
               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRep" CommandName="Rep" CommandArgument='<%#Eval("LID")%>'
                                            runat="server" CssClass="btn btn-primary">Reply</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkInd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LID") %>' CssClass="btn btn-danger link__cust__style" NavigateUrl='<%# String.Format("ViewEmpLeaveDetails.aspx?LID={0}", DataBinder.Eval(Container.DataItem,"LID").ToString()) %>' Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CommandName="edt" Visible="false" CommandArgument='<%#Eval("LID")%>'
                                            runat="server" CssClass="anchor__grd edit_grd">View</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" CommandName="Del" Visible="false" CommandArgument='<%#Eval("LID")%>'
                                            runat="server" CssClass="anchor__grd dlt">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="AdvancedLeaveAppPaging" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblReply" runat="server" visible="false" width="100%">
                <tr>
                    <td colspan="2" class="pageheader">Reply To HR
                    </td>
                </tr>
                <tr>
                    <td class="style3" style="width: 120px">
                        <b>Enter Reply:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReply" runat="server" BorderColor="#CC6600" BorderStyle="Inset"
                            Rows="4" TextMode="MultiLine" Width="300px" Height="70px" TabIndex="14"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnReply" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnReply_Click"
                            AccessKey="s" TabIndex="15" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsave" />
            <asp:PostBackTrigger ControlID="btnApply" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
