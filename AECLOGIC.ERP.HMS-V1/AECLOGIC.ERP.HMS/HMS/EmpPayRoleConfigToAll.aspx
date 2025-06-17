<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="EmpPayRoleConfigToAll.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpPayRoleConfigToAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function UpdateWages(ctrl, wageid, empid) {
            var Result = AjaxDAL.UpdateWages(empid, wageid, ctrl.checked);
            alert(Result.value);
        }
        function UpdateAllowances(ctrl, allowid, empid) {
            var Result = AjaxDAL.UpdateAllowances(empid, allowid, ctrl.checked);
            alert(Result.value);
        }
        function UpdateEmpContribution(ctrl, id, empid) {
            var Result = AjaxDAL.UpdateEmpContribution(empid, id, ctrl.checked);
            alert(Result.value);
        }
        function UpdateEmpDeductions(ctrl, id, empid) {
            var Result = AjaxDAL.UpdateEmpDeductions(empid, id, ctrl.checked);
            alert(Result.value);
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
        //chaitanya:for validation purpose
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnEMPId" runat="server" />
            <table style="width: 100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="180%">
                            <tr>
                                <td colspan="2">
                                    <cc1:Accordion ID="SalariesAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SalariesAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <b>Worksite</b>&nbsp;
                                                                <asp:DropDownList ID="ddlworksites" Visible="false" CssClass="droplist" AutoPostBack="true" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"
                                                                    TabIndex="1" runat="server" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;&nbsp; <b>Department</b>&nbsp;
                                                                <asp:DropDownList ID="ddldepartments" Visible="false" CssClass="droplist" runat="server" TabIndex="2"
                                                                    AccessKey="1" ToolTip="[Alt+1]">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartment" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;&nbsp;
                                                            <b>Name</b>
                                                            &nbsp;
                                                                <asp:TextBox ID="txtEMPName" runat="server" Text=""> </asp:TextBox>
                                                            &nbsp;&nbsp;
                                                             <b>ID</b>
                                                            &nbsp;
                                                                <asp:TextBox ID="txtID" runat="server" Text="" onkeypress="javascript:return isNumber(event)"> </asp:TextBox>
                                                            &nbsp;&nbsp;
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="80px"
                                                                    TabIndex="3" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="btnSearch_Click" />
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
                                    <span style="color: #ff6a00; font-size: 15px; font-weight: bold"><b>Select Employee(s) with commons</b></span>
                                </td>
                                <td><span style="color: #ff6a00; font-size: 15px; font-weight: bold"><b>Set what are all authorised</b> </span></td>
                            </tr>
                            <tr>
                                <td style="width: 40%">
                                    <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                        GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                                        EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" TabIndex="5" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this);" TabIndex="4" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                    </asp:GridView>
                                    <uc1:Paging ID="AddAttpaging" runat="server" />
                                </td>
                                <td style="vertical-align: top; width: 60%">
                                    <ajaxToolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <ajaxToolkit:AccordionPane ID="paneWages" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                <Header>
                                                    <b>Wages</b>
                                                </Header>
                                                <Content>
                                                    <asp:CheckBoxList ID="cblWages" runat="server" DataTextField="Name" DataValueField="WagesID"
                                                        TabIndex="6">
                                                    </asp:CheckBoxList>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="paneAllowences" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                <Header>
                                                    <b>CTC Allowances</b>
                                                </Header>
                                                <Content>
                                                    <asp:CheckBoxList ID="cblAllowences" TabIndex="7" runat="server" DataTextField="Name"
                                                        DataValueField="AllowId">
                                                    </asp:CheckBoxList>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="panelnonctc" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                <Header>
                                                    <b>NON-CTC Allowances</b>
                                                </Header>
                                                <Content>
                                                    <asp:CheckBoxList ID="chknonctc" TabIndex="7" runat="server" DataTextField="Name"
                                                        DataValueField="AllowId">
                                                    </asp:CheckBoxList>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="paneContribution" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                <Header>
                                                    <b>Company Contribution</b>
                                                </Header>
                                                <Content>
                                                    <asp:CheckBoxList ID="cblContributions" runat="server" TabIndex="8" DataTextField="NAME"
                                                        DataValueField="Itemid ">
                                                    </asp:CheckBoxList>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="paneDeduct" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                <Header>
                                                    <b>Deduct Statutory</b>
                                                </Header>
                                                <Content>
                                                    <asp:CheckBoxList ID="cblDeductions" runat="server" TabIndex="9" DataTextField="NAME"
                                                        DataValueField="Itemid ">
                                                    </asp:CheckBoxList>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                        </Panes>
                                    </ajaxToolkit:Accordion>
                                    <asp:Button ID="btnAll" runat="server" Text="Apply Authorisations" CssClass="btn btn-success" OnClick="btnAll_Click"
                                        AccessKey="s" TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
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
