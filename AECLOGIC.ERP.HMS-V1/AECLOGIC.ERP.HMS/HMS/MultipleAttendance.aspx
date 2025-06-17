<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="MultipleAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MultipleAttendance"
    MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAllOut(CheckBox) {
            //var Result = AjaxDAL.GetServerDate();
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            //            var d = new Date();
            //            d = Result.value;
            TotalChkBx = parseInt('<%= this.gdvAttend.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gdvAttend.ClientID %>');
            var TargetChildControl = "chkOut";
            var TargetChildOutControl = "txtOUT";
            var TRs = TargetBaseControl.getElementsByTagName("TR");
            for (var iTR = 0; iTR < TRs.length; ++iTR) {
                var Inputs = TRs[iTR].getElementsByTagName("input");
                var Labels = TRs[iTR].getElementsByTagName("SPAN");
                if (Inputs.length > 2) {
                    if (Inputs[2].type == 'checkbox' && Inputs[3].type == 'text') {
                        Inputs[2].checked = CheckBox.checked;
                        var EmpId = Labels[0].innerText;
                        if (Inputs[2].checked == true) {
                            Inputs[3].value = Result.value;
                        }
                        else {
                            Inputs[3].value = "";
                        }
                    }
                }
            }
        }
        function SelectAll(CheckBox) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            TotalChkBx = parseInt('<%= this.gdvAttend.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gdvAttend.ClientID %>');
            var TargetChildControl = "chkSelect";
            var TRs = TargetBaseControl.getElementsByTagName("TR");
            for (var iTR = 0; iTR < TRs.length; ++iTR) {
                var Inputs = TRs[iTR].getElementsByTagName("input");
                var Labels = TRs[iTR].getElementsByTagName("SPAN");
                if (Inputs.length > 2) {
                    if (Inputs[0].type == 'checkbox' && Inputs[3].type == 'text')
                        Inputs[0].checked = CheckBox.checked;
                    var EmpId = Labels[0].innerText;
                    if (Inputs[0].checked == true) {
                        Inputs[1].value = Result.value;
                    }
                    else {
                        Inputs[1].value = "";
                    }
                }
            }
        }
        function GetInTime(chkid, outtime, txtid, InID, EmpID) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(chkid).checked) {
                document.getElementById(txtid).value = d;
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }
        function GetOutTime(chkid, outtime, txtid, InID, EmpID) {
            var Result = AjaxDAL.GetServerDateIn24HrsFormate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(InID).value == "") {
                alert("Please Enter In Time");
                document.getElementById(chkid).checked = false;
                document.getElementById(txtid).value = "";
                document.getElementById(InID).focus();
                return false;
            }
            if (document.getElementById(chkid).checked) {
                document.getElementById(txtid).value = d;
                Result = AjaxDAL.HR_UpdateOutTime(EmpID);
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }
        function ValidSearch() {
            // EmPID
            if (!chkNumber('<%=txtEmpID.ClientID %>', 'EmpID', false, '')) {
             return false;
         }
     }
     function GetID(source, eventArgs) {
         var HdnKey = eventArgs.get_value();
         document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
     }
     function DeptID(source, eventArgs) {
         var HdnKey = eventArgs.get_value();
         document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
        }
        function DesgID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDesif2_hid.ClientID %>').value = HdnKey;
       }
       //chaitanya:for validation purpose below code
       function isNumber(evt) {
           var iKeyCode = (evt.which) ? evt.which : evt.keyCode
           if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
               return false;
           return true;
       }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                            SelectedIndex="0">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                                                Search Criteria
                                                            </Header>
                                    <Content>
                                        <asp:UpdatePanel ID="updAttendance" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>Worksite</b>&nbsp;
                                                                                    <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;
                                              <strong>Department</strong>&nbsp;&nbsp;
                                                    <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                            <asp:TextBox ID="textdept" OnTextChanged="Getdept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_dept" ServicePath="" TargetControlID="textdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="DeptID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="textdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <tr>
                                                            <td>
                                                                <b>
                                                                    <asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label>:</b>
                                                                <asp:HiddenField ID="ddlDesif2_hid" runat="server" />
                                                                <asp:TextBox ID="textdesg" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList_desg" ServicePath="" TargetControlID="textdesg"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="DesgID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textdesg"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Desigination Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:DropDownList ID="ddlDesif2" Visible="false" runat="server" CssClass="droplist">
                                                                </asp:DropDownList>&nbsp;
                                                         &nbsp;EmpID<asp:TextBox ID="txtEmpID" Width="150Px" runat="server" AccessKey="4" ToolTip="[Alt+4]"
                                                             TabIndex="5" onkeypress="javascript:return isNumber(event)">
                                                         </asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList_empid" ServicePath="" TargetControlID="txtEmpID"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEmpID"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter EmpId]"></cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>Name</td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmpName" Width="150Px" runat="server" TabIndex="6" AccessKey="5"
                                                                    ToolTip="[Alt+5]"></asp:TextBox>
                                                            </td>
                                                            <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                                                TargetControlID="txtEmpName"></cc1:TextBoxWatermarkExtender>
                                                            <td>&nbsp; &nbsp;&nbsp;
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                                                    OnClick="btnSearch_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSearch" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>Date :
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                            PopupButtonID="txtDOB" Format="dd MMM yyyy"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; vertical-align: top;" colspan="2">
                        <asp:GridView ID="gdvAttend" runat="server" CssClass="gridview" AutoGenerateColumns="False" CellPadding="1"
                            CellSpacing="1" DataKeyNames="EmpId" GridLines="None"
                            EmptyDataText="No Records Found"
                            EmptyDataRowStyle-CssClass="EmptyRowData"
                            OnRowDataBound="gdvAttend_RowDataBound" Width="100%"
                            OnRowCommand="gdvAttend_RowCommand">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Style="display: none;"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="In Time">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIN" runat="server" Width="100" Text='<%#Bind("InTime")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllOut" Text="Out" onclick="SelectAllOut(this);" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkOut" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Out Time">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("OutTime")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="18px" TextMode="MultiLine" Width="200px"
                                            Text='<%#Bind("Remarks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-success" Text="Save" CommandName="save" CommandArgument='<%#Bind("EmpId")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMID" runat="server" Visible="false" Text='<%#Bind("MID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
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
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="ViewApplilstPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px">
                        <div style="vertical-align: top">Today</div>
                        <asp:TextBox ID="txttime" runat="server" Height="22px" Width="189px" ToolTip="Enter IN Time HH:MM AM/PM"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender
                            ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txttime" WatermarkCssClass="Watermarktxtbox"
                            WatermarkText="[Enter IN Time HH:MM [24hrs]]"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td style="width: 50px">
                        <div style="vertical-align: top">
                            Tomorrow 
                                         <asp:CheckBox ID="chktmrw" runat="server" AutoPostBack="true" OnCheckedChanged="chktmrw_CheckedChanged" />
                        </div>
                        <asp:TextBox ID="txtouttime" runat="server" Height="22px" Width="189px" ToolTip="Enter OUT Time HH:MM AM/PM"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender
                            ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtouttime" WatermarkCssClass="Watermarktxtbox"
                            WatermarkText="[Enter OUT Time HH:MM [24hrs]]" />
                        <asp:Button ID="btnfill" runat="server" CssClass="btn btn-primary" Height="21px" Text="Fill" OnClick="btnfill_Click" />
                        <asp:Button ID="btnSaveAll" runat="server" Text="Save All" CssClass="btn btn-success"
                            OnClick="btnSaveAll_Click" />
                        <asp:Button ID="btnSavetmrw" runat="server" Text="Multiday Save" CssClass="btn btn-success" Visible="false" OnClick="btnSavetmrw_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="SalariesUpdPanel">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
