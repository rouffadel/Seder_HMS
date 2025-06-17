<%@ Page Title="" Language="C#" AutoEventWireup="True" 
    CodeBehind="ExceRptReporting.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ExceRptReporting" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceholder1" runat="Server">
     <script language="javascript" type="text/javascript">
         //chaitanya:code for validation
         function isNumber(evt) {
             var iKeyCode = (evt.which) ? evt.which : evt.keyCode
             if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                 return false;

             return true;
         }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
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
                                            Search Criteria</Header>
                                        <Content>
                                            <table>
                                                <tr>
                                                    
                                                    <td>
                                                        Site
                                                        <asp:DropDownList ID="ddlworksites" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" runat="server" CssClass="droplist" AccessKey="w"
                                                            ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        &nbsp;Department
                                                        <asp:DropDownList ID="ddldepartments" Visible="false" runat="server" CssClass="droplist" TabIndex="2">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartment" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                                </cc1:TextBoxWatermarkExtender>

                                                        &nbsp;Historical ID:<asp:TextBox ID="txtOldEmpID" Width="90" runat="server" AccessKey="1"
                                                            ToolTip="[Alt+1]" TabIndex="3" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                        &nbsp;EmpID:<asp:TextBox ID="txtEmpID" Width="60Px" runat="server" CssClass="droplist"
                                                            AccessKey="2" ToolTip="[Alt+2]" TabIndex="4"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                        &nbsp;Name:<asp:TextBox ID="txtusername" Width="90" runat="server" MaxLength="30"
                                                            OnTextChanged="btnSearch_Click" CssClass="droplist" AccessKey="3" ToolTip="[Alt+3]"
                                                            TabIndex="5"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                            CssClass="savebutton" Width="80px" AccessKey="i" ToolTip="[Alt+i]" TabIndex="6" />
                                                </tr>
                                            </table>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                    EmptyDataText="No Records Found" CssClass="gridview" HeaderStyle-CssClass="tableHead"
                    EmptyDataRowStyle-CssClass="EmptyRowData" Width="100%">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="EmpId" HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left"
                            Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="name" HeaderText="EmpName" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile1" HeaderText="Mobile1" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Reptname" HeaderText="ReportingTo" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
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
            <td style="height: 17px">
                <uc1:Paging ID="EmpListPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
  
</asp:content>
