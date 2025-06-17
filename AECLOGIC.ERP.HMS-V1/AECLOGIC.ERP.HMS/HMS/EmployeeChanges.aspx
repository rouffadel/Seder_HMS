<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EmployeeChanges.aspx.cs"
    Inherits="AECLOGIC.ERP.HMSV1.EmployeeChangesV1" Title="" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        //        function SearchModified() {
        //            $get('<%=hdnSearchChange.ClientID %>').value = "1";
        //        }
        function HeadsAssign(ctrl, SiteID, DeptID, HeadID, UserID, EmpID, Mgnr) {
            var locSiteID = $get(SiteID).value;
            var locDeptID = $get(DeptID).value;
            var locHeadID = $get(HeadID).value;
            var locUserID = UserID;
            var locEmpID = EmpID;
            var locMgnr = Mgnr;
            if (locDeptID == 0) {
                alert('Please Choose Department!');
                return true;
            }
            var Result = AjaxDAL.GetDeptHeadsForTransfer(locEmpID, locDeptID, locSiteID);
            if (Result.value > 0) {
                if (locHeadID != 0) {
                    AjaxDAL.UpdSiteDeptChanges(locEmpID, locSiteID, locDeptID, locHeadID, locUserID);
                    alert('Transferred');
                }
                else {
                    alert('Please Choose Head to Assign!');
                    return true;
                }
            }
            else {
                var ans = confirm("No Head found in the selected Department! If you wish you can Transfer the Employee to become new Department Head only. Else he can not be transferred without a predefined Head");
                if (ans) {
                    AjaxDAL.UPDATESetAsDeptHead(locSiteID, locDeptID, locEmpID, locUserID);
                    alert('Transferred and set as Dept Head.!');
                    return true;
                }
                else {
                    AjaxDAL.UpdSiteDeptChanges(locEmpID, locSiteID, locDeptID, locHeadID, locUserID);
                    return false;
                }
            }
            return true;
        }
        function check() {
        }
        function Validate() {
            //for Worksite
         <%--   if (!chkDropDownList('<%=ddlworksites.ClientID%>', 'Worksite'))
                return false;--%>
            return true;
        }
        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gveditkbipl.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gveditkbipl.ClientID %>');
            var TargetChildControl = "chkSelect";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function SelectDeSelectHeader(CheckBox) {
            TotalChkBx = parseInt('<%= this.gveditkbipl.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gveditkbipl.ClientID %>');
            var TargetChildControl = "chkSelect";
            var TargetHeaderControl = "chkSelectAll";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            var flag = false;
            var HeaderCheckBox;
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) >= 0)
                    HeaderCheckBox = Inputs[iCount];
                if (Inputs[iCount] != CheckBox && Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) == -1) {
                    if (CheckBox.checked) {
                        if (!Inputs[iCount].checked) {
                            flag = false;
                            HeaderCheckBox.checked = false;
                            return;
                        }
                        else
                            flag = true;
                    }
                    else if (!CheckBox.checked)
                        HeaderCheckBox.checked = false;
                }
            }
            if (flag)
                HeaderCheckBox.checked = CheckBox.checked
        }
        //For Dropdown list
        function chkDropDownList(object, msg) {
            var elm = getObj(object);
            if (elm.selectedIndex == 0) {
                alert("Select " + msg + "!!!");
                elm.focus();
                return false;
            } return true;
        }
        //geting the object
        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            }
            else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            }
            else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            }
            else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }
        // focus
    <%--   function window.onload() {
           document.getElementById("<%= txtEmpID.ClientID%>").focus();
       }--%>
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlworksites_hid.ClientID %>').value = HdnKey;
        }
        function GetSpeID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlSpecialization_hid.ClientID %>').value = HdnKey;
        }
      <%--  function GetWSID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlworksitesTransit_hid.ClientID %>').value = HdnKey;
        }--%>
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddldepartments_hid.ClientID %>').value = HdnKey;
        }
        //chaitanya:validation for below code
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left">
                <div id="dvTransEmp" runat="server" visible="false">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
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
                                                        <td>From Worksite&nbsp;
                                                                <asp:DropDownList ID="ddlworksites" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" CssClass="droplist"
                                                                    AccessKey="w" ToolTip='[Alt+w OR Alt+w+Enter]' TabIndex="1" Width="20%">
                                                                </asp:DropDownList>
                                                            <asp:HiddenField ID="ddlworksites_hid" runat="server" />
                                                            <asp:TextBox ID="txtSearchWorksite" AutoPostBack="true" OnTextChanged="GetWork" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionWorksiteList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;Specialization&nbsp;&nbsp;
                                                                 <asp:DropDownList ID="ddlSpecialization" runat="server" Visible="false" AutoPostBack="true" CssClass="droplist"
                                                                     AccessKey="w" ToolTip='[Alt+w OR Alt+w+Enter]' TabIndex="1" Width="20%">
                                                                 </asp:DropDownList>
                                                            <asp:HiddenField ID="ddlSpecialization_hid" runat="server" />
                                                            <asp:TextBox ID="txtSpecialization" AutoPostBack="true" OnTextChanged="GetSpecialization" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionSpecializationList" ServicePath="" TargetControlID="txtSpecialization"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                OnClientItemSelected="GetSpeID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSpecialization"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Specialization Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;From Dept&nbsp;&nbsp;
                                                                <asp:HiddenField ID="ddldepartments_hid" runat="server" />
                                                            <asp:TextBox ID="txtdepartment" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                OnClientItemSelected="GETDEPT_ID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;EmpID:
                                                                <asp:TextBox ID="txtEmpID" runat="server" Width="65px" AccessKey="2" ToolTip='[Alt+2]' TabIndex="3" onkeypress="javascript:return isNumber(event)">
                                                                </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                            &nbsp;&nbsp;Name:<asp:TextBox ID="txtEmpname" runat="server" AccessKey="3" ToolTip='[Alt+3]' TabIndex="4"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath=""
                                                                TargetControlID="txtEmpName" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                ShowOnlyCurrentWordInCompletionListItem="true" FirstRowSelected="True">
                                                            </cc1:AutoCompleteExtender>
                                                            &nbsp;
                                                                <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-success" Width="100px" TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                    OnClick="btnSearch_Click" />
                                                            &nbsp;<asp:HyperLink ID="hlnkView" NavigateUrl="#" Font-Bold="true" onclick="javascript:return window.open('DeptHeadView.aspx', '_blank','height=500, width=550,status=no, resizable= yes');"
                                                                runat="server" TabIndex="6" CssClass="btn btn-primary">View Heads</asp:HyperLink>
                                                            &nbsp;
                                                                <asp:Button ID="btnLnkDeptHead" Visible="false" runat="server" CssClass="btn btn-primary"
                                                                    ToolTip="Click To Assign Department Heads" Text="Dept Heads" OnClick="btnLnkDeptHead_Click" />
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
                                <div id="dvgrd" runat="server" visible="false" style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                                    GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                                    OnRowCommand="gveditkbipl_RowCommand" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                    OnRowDataBound="gveditkbipl_RowDataBound" CssClass="gridview">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemStyle />
                                                            <HeaderStyle />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this);" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpID" Text='<%#Eval("EmpId")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="site_name" HeaderText="Worksite" />
                                                        <asp:BoundField DataField="Category" HeaderText="Specialization" />
                                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtdesignation" Width="100px" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Worksite" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="180" />
                                                            <HeaderStyle Width="180" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList Width="180" ID="grdddlworksites" CssClass="droplist" runat="server" DataTextField="Site_Name"
                                                                    AutoPostBack="true" DataValueField="Site_ID" DataSource='<%# BindSites()%>' OnSelectedIndexChanged="grdddlworksites_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Department" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="180" />
                                                            <HeaderStyle Width="180" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList Width="180" ID="grdddldepartments" runat="server" DataTextField="DepartmentName"
                                                                    DataValueField="DepartmentUId" AutoPostBack="true" CssClass="droplist"
                                                                    OnSelectedIndexChanged="grdddldepartments_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <%--DataSource='<%# BindDepts()%>' --%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="New Reporting-To" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="180" />
                                                            <HeaderStyle Width="180" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList Width="180" ID="grdddlHeads" runat="server" DataTextField="name" CssClass="droplist"
                                                                    AutoPostBack="true" DataValueField="HeadId">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Proof" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkchange" runat="server" CommandName="UPD" Text="Transfer" CommandArgument='<%#Bind("EmpId")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnUpdateAll" runat="server" Text="Transfer Selected" ToolTip="All Checked Employees will be Transfered"
                                                    CssClass="btn btn-primary" Width="127px" OnClick="btnUpdateAll_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>Worksite&nbsp;
                                                                <asp:DropDownList ID="ddlFillWS" runat="server" AutoPostBack="true" CssClass="droplist"
                                                                    AccessKey="w" ToolTip='[Alt+w OR Alt+w+Enter]' TabIndex="1" Width="160px" OnSelectedIndexChanged="ddlFillWS_SelectedIndexChanged">
                                                                </asp:DropDownList></td>
                                            <td>Department&nbsp;
                                                                <asp:DropDownList ID="ddlFillDepartment" runat="server" AutoPostBack="true" CssClass="droplist"
                                                                    AccessKey="w" ToolTip='[Alt+w OR Alt+w+Enter]' TabIndex="1" Width="160px" OnSelectedIndexChanged="ddlFillDepartment_SelectedIndexChanged">
                                                                </asp:DropDownList></td>
                                            <td>Reporting To: &nbsp;
                                                                <asp:DropDownList ID="ddlFillReportingTo" runat="server" CssClass="droplist"
                                                                    AccessKey="w" ToolTip='[Alt+w OR Alt+w+Enter]' TabIndex="1" Width="220px">
                                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnFill" runat="server" Text="Fill" OnClick="btnFill_Click" CssClass="btn btn-primary" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 17px">
                                <uc1:Paging ID="EmployeeChangesPaging" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvTransferedEmp" runat="server" visible="false">
                    <table>
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
                                                        <td>Destination Worksite&nbsp;
                                                                <asp:DropDownList ID="ddlTransitWS" runat="server" CssClass="droplist"
                                                                    AccessKey="w" ToolTip='[Alt+w OR Alt+w+Enter]' TabIndex="1" Width="10%" Height="20px">
                                                                </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                             Emp Name:
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtemp" runat="server" Width="200px" ToolTip="Select Employee From the below populating List"
                                                AccessKey="e"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="ACESearchProject" runat="server" DelimiterCharacters=""
                                                                Enabled="true" MinimumPrefixLength="2" ServiceMethod="GetEmpDetail"
                                                                ServicePath="" TargetControlID="txtemp" UseContextKey="true" CompletionInterval="10"
                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                FirstRowSelected="True">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtemp"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Emp Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkTransitEmp" runat="server" Text="Transit Employees" Font-Size="Small" Font-Bold="true" OnClick="lnkTransitEmp_Click" CssClass="btn btn-primary"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkConfirmEmp" runat="server" Text="Confirmed Arrivals" Font-Size="Small" Font-Bold="true" OnClick="lnkConfirmEmp_Click" CssClass="btn btn-primary"></asp:LinkButton>
                                                            <div style="float: right; font-size: 12px; color: red">
                                                                After Transfer,the employees are already Considered to be on the strength of the receiving Worksite.<br />
                                                                It is the responsibility of the receiving worksite to make sure the employee safety in transit is taken care of.<br />
                                                                This table is just an indication of confirmation of the receiving Worksite.
                                                            </div>
                                                            <br>
                                                            <asp:Button ID="btnEmpid" Text="Order By EmpNo" runat="server" OnClick="btnempid_Click" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnEmpdesc" Text="Order By Desc" runat="server" OnClick="btnEmpdesc_Click" CssClass="btn btn-primary" />
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
                                <asp:Label ID="lblTransist" runat="server" Visible="false" Font-Bold="true" Font-Size="Small"></asp:Label>
                                <asp:GridView ID="GvTransit" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                    OnRowCommand="GvTransit_RowCommand" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                    CssClass="gridview">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSid" runat="server" CommandName="UPD" Text='<%#Bind("Sid")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpId" HeaderText="Employee Id" Visible="false" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="FromSite" HeaderText="From WorkSite" />
                                        <asp:BoundField DataField="FromDepartment" HeaderText="From Department" />
                                        <asp:BoundField DataField="PresentSitename" HeaderText="To WorkSite" />
                                        <asp:BoundField DataField="PresentDepartmentName" HeaderText="To Department" />
                                        <asp:BoundField DataField="Transferredby" HeaderText="Transferred By" />
                                        <asp:BoundField DataField="date" HeaderText="Date" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkConfirm" runat="server" CommandName="UPD" Text="Confirm" CommandArgument='<%#Bind("EmpId")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Upload">
                                            <ItemTemplate>
                                                <a id="A6" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "SID").ToString()) %>'
                                                    runat="server" class="btn btn-primary">View</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GVArrived" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                    EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                    CssClass="gridview">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="EmpId" HeaderText="Employee Id" Visible="false" />
                                        <asp:BoundField DataField="name" HeaderText="Employee Name" />
                                        <%--  <asp:BoundField DataField="ConfirmedBy" HeaderText="Confirmed By" />--%>
                                        <asp:BoundField DataField="ArrivedOn" HeaderText="Arrived On" />
                                    </Columns>
                                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvOMS" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>
                                <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane3" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                            Request From OMS</Header>
                                            <Content>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton
                                                                ID="lbnPending" runat="server" Text="Pending" OnClick="lbnPending_Click"></asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton
                                                        ID="lbnApprove" runat="server" Text="Approved" OnClick="lbnApprove_Click"></asp:LinkButton>&nbsp;|&nbsp;
                                                    <asp:LinkButton ID="lbnReject" runat="server" Text="Reject" OnClick="lbnReject_Click"></asp:LinkButton></strong>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Project &nbsp;&nbsp;                                                
                                                    <asp:DropDownList ID="ddlOMSProject" runat="server" Width="150px" MaxLength="30" TabIndex="3">
                                                    </asp:DropDownList>
                                                            Specialisation &nbsp;                
                                                    <asp:DropDownList ID="ddlOMSSpecialisation" runat="server" AccessKey="w" CssClass="droplist"
                                                        ToolTip="Alt+w OR Alt+t+Enter" Width="150">
                                                    </asp:DropDownList>
                                                            Designation  &nbsp;&nbsp;              
                                                    <asp:DropDownList ID="ddlOMSDesignation" runat="server" AccessKey="w" CssClass="droplist"
                                                        ToolTip="Alt+w OR Alt+t+Enter" Width="150">
                                                    </asp:DropDownList>
                                                            <asp:TextBox ID="txtOMSFromDate" runat="server" Width="100px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                Format="dd MMM yyyy" PopupButtonID="txtOMSFromDate" TargetControlID="txtOMSFromDate"></cc1:CalendarExtender>
                                                            <asp:TextBox ID="txtOMSToDate" runat="server" Width="100px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                Format="dd MMM yyyy" PopupButtonID="txtOMSToDate" TargetControlID="txtOMSToDate"></cc1:CalendarExtender>
                                                            <asp:Button ID="btnOMSMpSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnOMSMpSearch_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvMPReq" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                                                GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                                CssClass="gridview" OnRowCommand="gvMPReq_RowCommand"
                                                                OnRowDataBound="gvMPReq_RowDataBound">
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOMSMPReqID" runat="server" Text='<%#Eval("OMSMPReqID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                                                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name" />
                                                                    <asp:BoundField DataField="SubTaskName" HeaderText="SubTask Name" />
                                                                    <asp:BoundField DataField="JabNAme" HeaderText="Activity Name" />
                                                                    <asp:BoundField DataField="ResourceName" HeaderText="Resource Name" />
                                                                    <asp:BoundField DataField="Category" HeaderText="Specialisation" />
                                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                    <asp:BoundField DataField="ReqDate" HeaderText="Req Date" />
                                                                    <asp:BoundField DataField="MPHours" HeaderText="Hours" />
                                                                    <asp:BoundField DataField="MPNos" HeaderText="Man Days" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkApprove" runat="server" CommandName="Approve" Text="Approve" CommandArgument='<%#Bind("OMSMPReqID")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkReject" runat="server" CommandName="Reject" Text="Reject" CommandArgument='<%#Bind("OMSMPReqID")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 17px">
                                                            <uc1:Paging ID="MPReq" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                </cc1:Accordion>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvHMS" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>
                                <cc1:Accordion ID="Accordion3" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane4" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                            Request From HMS</Header>
                                            <Content>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>WorkSite: &nbsp;&nbsp;                                                
                                                     <asp:DropDownList ID="ddlWorksiteID" runat="server" Width="120px" MaxLength="30" TabIndex="2" AutoPostBack="true"
                                                         OnSelectedIndexChanged="ddlWorksiteID_SelectedIndexChanged">
                                                     </asp:DropDownList>
                                                            &nbsp;&nbsp; 
                                                    Project &nbsp;&nbsp;                                                
                                                    <asp:DropDownList ID="ddlProjectID" runat="server" Width="150px" MaxLength="30" TabIndex="3">
                                                    </asp:DropDownList>
                                                            Specialisation &nbsp;                
                                                    <asp:DropDownList ID="ddlSpecialisation" runat="server" AccessKey="w" CssClass="droplist"
                                                        ToolTip="Alt+w OR Alt+t+Enter" Width="150">
                                                    </asp:DropDownList>
                                                            Designation  &nbsp;&nbsp;              
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" AccessKey="w" CssClass="droplist" ToolTip="Alt+w OR Alt+t+Enter" Width="150">
                                                    </asp:DropDownList>
                                                            <asp:TextBox ID="txtFrom" runat="server" Width="100px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                Format="dd MMM yyyy" PopupButtonID="txtFrom" TargetControlID="txtFrom"></cc1:CalendarExtender>
                                                            <asp:TextBox ID="txtTo" runat="server" Width="100px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                Format="dd MMM yyyy" PopupButtonID="txtTo" TargetControlID="txtTo"></cc1:CalendarExtender>
                                                            <asp:Button ID="btnHMSMpSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnHMSMpSearch_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:GridView ID="gvMpHMS" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                                                GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                                CssClass="gridview" OnRowCommand="gvMpHMS_RowCommand"
                                                                OnRowDataBound="gvMpHMS_RowDataBound">
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOMSMPReqID" runat="server" Text='<%#Eval("OMSMPReqID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="WorkSite" HeaderText="WorkSite" />
                                                                    <%-- 2--%>
                                                                    <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" />
                                                                    <%-- 3--%>
                                                                    <asp:BoundField DataField="ReqReference" HeaderText="ReqReference" />
                                                                    <%-- 4--%>
                                                                    <asp:BoundField DataField="ResourceName" HeaderText="Resource Name" />
                                                                    <asp:BoundField DataField="Category" HeaderText="Specialisation" />
                                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                    <asp:BoundField DataField="ReqDate" HeaderText="Req Date" />
                                                                    <asp:BoundField DataField="MPHours" HeaderText="Hours" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                    <asp:BoundField DataField="MPNos" HeaderText="Man Days" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 17px">
                                                            <uc1:Paging ID="gvMpHMSPageing" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                </cc1:Accordion>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
