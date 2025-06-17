<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="EmpOTConfig.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.EmpOTConfig" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
            <script type="text/javascript" language="javascript">
                function SelectAll(CheckBox) {
                    TotalChkBx = parseInt('<%= this.grdEmpMessAtt.Rows.Count %>');
                    var TargetBaseControl = document.getElementById('<%= this.grdEmpMessAtt.ClientID %>');
                    var TargetChildControl = "chkAll";
                    var Inputs = TargetBaseControl.getElementsByTagName("input");
                    for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                        if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                            Inputs[iCount].checked = CheckBox.checked;
                    }
                }
                function validateCheckBox() {
                    var isValid = false;
                    var gvChk = document.getElementById('<%= grdEmpMessAtt.ClientID %>');
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
                function GetID(source, eventArgs) {
                    var HdnKey = eventArgs.get_value();
                    //  alert(HdnKey);
                    document.getElementById('<%=ddlWorkSite_hid.ClientID %>').value = HdnKey;
     }
     function GETDEPT_ID(source, eventArgs) {
         var HdnKey = eventArgs.get_value();
         //  alert(HdnKey);
         document.getElementById('<%=ddlDept_hid.ClientID %>').value = HdnKey;
     }
     function GETDesig_ID(source, eventArgs) {
         var HdnKey = eventArgs.get_value();
         //  alert(HdnKey);
         document.getElementById('<%=ddlDesif2_hid.ClientID %>').value = HdnKey;
     }
            </script>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" id="tblSearch">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="LstOfHolidayConAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="80%">
                                        <panes>
                                    <cc1:AccordionPane ID="LstOfHolidayConAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="width:80px;">
                                                        <b>
                                                            <asp:Label ID="lblWorksite" runat="server" Text="Worksite"></asp:Label>:</b>
                                                        </td>
                                                    <td>
                                                        &nbsp;
                                                           <asp:HiddenField ID="ddlWorkSite_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="130px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:</b> &nbsp;
                                                        </td>
                                                    <td>
                                                          <asp:HiddenField ID="ddlDept_hid" runat="server" />
                                                 <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="130px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID"  >
                                            </cc1:AutoCompleteExtender>        
                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                 <td >
                                                  <b><asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label>:</b>
                                                     </td>
                                                    <td>
                                                      <asp:HiddenField ID="ddlDesif2_hid" runat="server" />
                                                 <asp:TextBox ID="textDesg" AutoPostBack="true" Height="22px" Width="130px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Desigination" ServicePath="" TargetControlID="textDesg"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDesig_ID"  >
                                            </cc1:AutoCompleteExtender>        
                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textDesg"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Desigination Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td>
                                                     <b>
                                                            <asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>:</b>
                                                   </td><td>
                                                        <asp:DropDownList ID="ddlEmpNature" AutoPostBack="true" Height="22px" Width="130px" runat="server" CssClass="droplist">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td> <asp:Label ID="lblEMPID" runat="server" Text="Emp ID"></asp:Label>:</b>
                                                        </td><td>
                                                    <asp:TextBox ID="txtEMPID" Height="22px" Width="130px" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton btn btn-primary" OnClick="btnSearch_Click" />
                                                        </td><td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Content>
                                    </cc1:AccordionPane>
                                </panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" id="tblgrid">
                            <tr>
                                <td style="width: 100%">
                                    <asp:GridView ID="grdEmpMessAtt" runat="server" CssClass="gridview" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" Width="80%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Checked='<%#Eval("Status")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Site_Name" HeaderText="WorkSite" />
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrade" runat="server" Text='<%#Eval("Category")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWS" runat="server" Text='<%#Eval("WS")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OT Hrs" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOTHrs" runat="server" Text='<%#Eval("OTHrs") %>' Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAll" runat="server" Text="Submit" CssClass="savebutton btn btn-success"
                                        OnClientClick="javascript:return validateCheckBox();" OnClick="btnAll_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpMessAttPaging" runat="server" />
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
