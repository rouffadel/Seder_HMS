<%@ Page Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="EmployeeOrder.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.EmployeeOrder" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Validate() {

            //for Worksite
            if (!chkDropDownList('<%=ddlworksites.ClientID%>', 'Worksite'))
                return false;
            return true;
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
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                
                <tr>
                    <td>
                        <cc1:Accordion ID="CateAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="CateAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                   <b>Worksite</b>  &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlworksites" Visible="false" CssClass="droplist" AutoPostBack="true" TabIndex="1" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"
                                                        runat="server" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                     <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>

                                                    &nbsp;&nbsp;&nbsp;&nbsp;<b> Department</b> &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddldepartments" Visible="false" TabIndex="2" AccessKey="1" ToolTip="[Alt+1]"
                                                        CssClass="droplist" runat="server">
                                                       <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                            </cc1:TextBoxWatermarkExtender>

                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary"
                                                        Width="100px" OnClientClick="javascript:return Validate()" OnClick="btnSearch_Click" TabIndex="3" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
                        <div id="dvemplist" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        Employee List
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 200px">
                                        <asp:ListBox ID="lstEmployee" runat="server" Height="400px" CssClass="box box-primary" TabIndex="4"></asp:ListBox>
                                    </td>
                                    <td style="vertical-align: middle; width: 100px">
                                        <asp:Button ID="btnFirst" runat="server" Text="Move First" CssClass="btn btn-success" Width="80px" 
                                            OnClick="btnFirst_Click" TabIndex="5" /><br />
                                        <br />
                                        <asp:Button ID="btnUp" runat="server" Text="Move Up" CssClass="btn btn-primary" Width="80px" 
                                            OnClick="btnUp_Click" TabIndex="6" /><br />
                                        <br />
                                        <asp:Button ID="btnDown" runat="server" Text="Move Down" CssClass="btn btn-warning" Width="80px" 
                                            OnClick="btnDown_Click" TabIndex="7" /><br />
                                        <br />
                                        <asp:Button ID="btnLast" runat="server" Text="Move Last" CssClass="btn btn-danger" Width="80px" 
                                            OnClick="btnLast_Click" TabIndex="8" /><br />
                                        <br />
                                    </td>
                                    <td style="vertical-align: middle">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                            OnClick="btnSubmit_Click" AccessKey="s" TabIndex="9" 
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </div>
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
