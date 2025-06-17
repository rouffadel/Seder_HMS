<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="Shifts.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Shifts" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) {
            var oGrid = document.getElementById(grid);
            var IPs = oGrid.getElementsByTagName("input");
            for (var iCount = 0; iCount < IPs.length; ++iCount) {
                if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked;
            }
        }
        function Done() {
            alert("Shifts Jumbled Sucessfully!")
        }
        function valids() {
            var isValid = false;
            var gvChk = document.getElementById('<%= gvEmp.ClientID %>');
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
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="tblJumble" runat="server" visible="false" width="100%" style="border-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;"
                border="0" cellpadding="3" cellspacing="3">
                <%--<tr bgcolor=" #605C5B" >--%>
                <tr>
                    <td class="pageheader">Bulk Jumbling of Shifts
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        <b>WorkSite:&nbsp; </b>
                        <asp:DropDownList ID="ddlWs2" runat="server" CssClass="droplist" TabIndex="1" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]">
                        </asp:DropDownList>
                        <b>Department:&nbsp; </b>
                        <asp:DropDownList ID="ddlDepart" runat="server" CssClass="droplist" AccessKey="1" ToolTip="[Alt+1]"
                            TabIndex="2">
                        </asp:DropDownList>
                        <b>&nbsp; Designation: </b>
                        <asp:DropDownList ID="ddlDesif2" runat="server" CssClass="droplist"
                            TabIndex="3" AccessKey="2" ToolTip="[Alt+2]">
                        </asp:DropDownList>
                        <b>&nbsp;Select Shift<span style="color: #ff0000">*</span>:&nbsp; </b>
                        <asp:DropDownList ID="ddlshifts" runat="server" CssClass="droplist" AccessKey="3" ToolTip="[Alt+3]"
                            TabIndex="4">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExtender3" IsSorted="true" PromptText="Type Here To Search..."
                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                            TargetControlID="ddlDesif2" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnJumble" runat="server" CssClass="btn btn-primary" OnClick="btnJumble_Click"
                            OnClientClick="return confirm('Are you Sure?');" Text="Jumble" Visible="False"
                            ToolTip="Clock wise Shifts Jumbling " TabIndex="5" AccessKey="s" />
                    </td>
                </tr>
            </table>
            <table id="tblMain" visible="false" runat="server" width="100%">
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
                                                <td>&nbsp;<b>WorkSite:</b><asp:DropDownList ID="ddlWorksite" Visible="false" CssClass="droplist" runat="server" AccessKey="w" AutoPostBack="true" OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged" ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" AutoPostBack="true" Height="22Px" Width="150Px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp; <b>Department:</b><asp:DropDownList ID="ddlDept" CssClass="droplist" Visible="false" runat="server" AccessKey="1" ToolTip="[Alt+1]" TabIndex="2">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="165px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                    <b>Designation: </b>
                                                    <asp:DropDownList ID="ddlDesignation" CssClass="droplist" Visible="false" runat="server" AccessKey="5" ToolTip="[Alt+5]" TabIndex="6">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                                                        PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                        TargetControlID="ddlDesignation" />
                                                    &nbsp;&nbsp;
                                                     <asp:TextBox ID="Textdesg" OnTextChanged="Getdesglist" Height="22px" Width="168px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletiondesglist" ServicePath="" TargetControlID="Textdesg"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="Textdesg"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Desigination Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp; <b>&nbsp;Employee Name/ID: </b>
                                                    <asp:DropDownList ID="ddlName" CssClass="droplist" runat="server" Visible="false" AccessKey="2" ToolTip="[Alt+2]" TabIndex="3">
                                                    </asp:DropDownList>
                                                    &nbsp;<cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..."
                                                        PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                        TargetControlID="ddlName" />
                                                    &nbsp;
                                                      <asp:TextBox ID="textsearchemp" OnTextChanged="GetempSearch" Height="22px" Width="175px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListemp" ServicePath="" TargetControlID="textsearchemp"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textsearchemp"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]"></cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>&nbsp;Select Shift:&nbsp; </b>
                                                    <asp:DropDownList ID="ddlShift" runat="server" Visible="false" CssClass="droplist" AccessKey="4" ToolTip="[Alt+4]" TabIndex="5">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                                 <asp:TextBox ID="textshift" OnTextChanged="Getshiftlist" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionshiftlist" ServicePath="" TargetControlID="textshift"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="textshift"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Shift Name]"></cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click"
                                                        Text="Search" Width="80px" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="7" />
                                                    &nbsp;<asp:Button ID="btnShifting" runat="server" CssClass="btn btn-primary" Text="Shifting"
                                                        OnClick="btnShifting_Click" TabIndex="8" />
                                                    &nbsp;&nbsp;
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
                    <td>&nbsp;
                    </td>
                </tr>
                <tr id="trNote" runat="server" visible="false">
                    <td class="pageheader">Choose Shift Type
                    </td>
                </tr>
                <tr id="trChoose" runat="server" visible="false">
                    <td style="padding-left: 50Px">
                        <asp:RadioButtonList ID="rbChoose" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                            Font-Bold="True" OnSelectedIndexChanged="rbChoose_SelectedIndexChanged" ToolTip="Few: Employee wise. Bulk:Designation wise Jumble in Cyclic Order" TabIndex="9"
                            ForeColor="#0033CC">
                            <asp:ListItem Text="Few" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Bulk Jumble" Value="2"></asp:ListItem>
                            <asp:ListItem Text="None" Selected="True" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trgv" runat="server" visible="false">
                    <td>
                        <asp:GridView ID="gvEmp" HeaderStyle-CssClass="tableHead" runat="server" AutoGenerateColumns="False"
                            Width="100%" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            CssClass="gridview" OnRowDataBound="gvEmp_RowDataBound">
                            <EmptyDataRowStyle CssClass="EmptyRowData" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkToTransfer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Shift" DataField="ShiftName" />
                                <asp:BoundField DataField="Mobile1" HeaderText="Mobile1" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mobile2" HeaderText="Mobile2" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:Paging ID="ShiftsPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblMove" visible="false" runat="server" width="100%">
                <tr>
                    <td style="padding-left: 200Px">
                        <asp:Label ID="lblMove" runat="server" Text="Move To:" CssClass="droplist" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlMoveShift" CssClass="droplist" runat="server" Height="16px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 260Px">
                        <asp:Button ID="btnMove" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnMove_Click"
                            OnClientClick="javascript:return valids();" TabIndex="10" />
                        <asp:Label ID="lblWarn" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
