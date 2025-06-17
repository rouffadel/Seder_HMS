<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EMPCustomReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EMPCustomReport" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
  <%--  <script type="text/javascript" src="Includes/JS/Validation.js">--%>
           <script language="javascript" type="text/javascript">

function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
           // alert(HdnKey);
            document.getElementById('<%=ddlWS_hid.ClientID %>').value = HdnKey;
        }


    </script>
    <table width="100%">
        <tr>
            <td>
                <asp:CheckBoxList ID="chkListFields" runat="server" RepeatColumns="4" Width="100%" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]">
                    <asp:ListItem Selected="True" Value="0">Photo</asp:ListItem>
                    <asp:ListItem Selected="True" Value="1">EmpID</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Historical ID</asp:ListItem>
                    <asp:ListItem Selected="True" Value="3">Name</asp:ListItem>
                    <asp:ListItem Selected="True" Value="4">Department</asp:ListItem>
                    <asp:ListItem Selected="True" Value="5">Designation</asp:ListItem>
                    <asp:ListItem Selected="True" Value="6">Phone</asp:ListItem>
                    <asp:ListItem Selected="True" Value="7">DOB</asp:ListItem>
                    <asp:ListItem Selected="True" Value="8">DOJ</asp:ListItem>
                    <asp:ListItem Selected="True" Value="9">Permanent Address</asp:ListItem>
                    <asp:ListItem Selected="True" Value="10">Residence Address</asp:ListItem>
                    <asp:ListItem Selected="True" Value="11">WorkSite</asp:ListItem>
                    <asp:ListItem Selected="True" Value="12">Salary</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
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
                                            <b>Worksite: </b>
                                        
                                             <asp:HiddenField ID="ddlWS_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>







                                            &nbsp;&nbsp;
                                           
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnSaveButton" runat="server" CssClass="savebutton" OnClick="btnSaveButton_Click" TabIndex="3" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                Text="Generate Report" />
                                            &nbsp;
                                            <asp:Button ID="btnExpExcel" runat="server" CssClass="savebutton" Text="Export to Excel"
                                                OnClick="btnExpExcel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
        </tr>
        </td>
        <tr>
            <td>
                <asp:GridView ID="gvReport" AutoGenerateColumns="false" runat="server" Width="100%"
                    HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    CssClass="gridview">
                    <Columns>
                        <asp:ImageField DataImageUrlField="Photo" HeaderText="Photo" DataImageUrlFormatString="\EmpImages\{0}">
                        </asp:ImageField>
                        <asp:BoundField DataField="EmpID" HeaderText="EmpID" />
                        <asp:BoundField DataField="OLdEmpID" HeaderText="Historical ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" />
                        <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" />
                        <asp:BoundField DataField="PerAddress" HeaderText="Permanent Address" />
                        <asp:BoundField DataField="ResAddress" HeaderText="Residence Address" />
                        <asp:BoundField DataField="WorkSite" HeaderText="Worksite" />
                        <asp:BoundField DataField="Salary" HeaderText="Salary" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="EMPCustomReportPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
