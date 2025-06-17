<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyPayrollAuthorisations.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.MonthlyPayrollAuthorisations" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceholder1" runat="Server">      
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
               
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
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
                                                                   <td> 
                                                                       
                                                                        Worksite:
                                                                       &nbsp;&nbsp;                                                           
                                                                <asp:TextBox ID="txtSearchWorksite" Height="22px" Width="140px"
                                                                     runat="server" AutoPostBack="True" >                                                                   
                                                                    </asp:TextBox> <%--onkeypress="return EnterEvent(event)"--%>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Department:
                                                                 <asp:TextBox ID="txtSearchdept"  Height="22px" Width="140px" 
                                                                     runat="server" AutoPostBack="True" >                                                                   
                                                                 </asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Dept Name]">
                                                                </cc1:TextBoxWatermarkExtender>                                                    
                                                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                 EmpId:
                                                  &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtemp" runat="server" Width="200px" ToolTip="Select Employees From the below populating List"
                                                AccessKey="e"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="ACESearchProject" runat="server" DelimiterCharacters=""
                                                Enabled="true" MinimumPrefixLength="2" ServiceMethod="GetEmpdetails"
                                                ServicePath="" TargetControlID="txtemp" UseContextKey="true" CompletionInterval="10"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                FirstRowSelected="True">
                                            </cc1:AutoCompleteExtender>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtemp"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter EmpId]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                        &nbsp;&nbsp;Month:&nbsp;
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]" Width="90">
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
                                                               &nbsp;&nbsp;Year:
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3"  Width="90">
                                                                </asp:DropDownList>
                                                                       &nbsp;&nbsp;&nbsp;
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                                                    Width="80px" />  
                                                                       </td>                                                              
                                                                     </tr>
                                                        

                                                             

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
                                    <asp:GridView ID="gvmonthlypayroll" runat="server" AutoGenerateColumns="True" CssClass="gridview"
                                        Width="100%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                       HeaderStyle-CssClass="tableHead"
                                         AllowSorting="True" AlternatingRowStyle-BackColor="GhostWhite" OnRowDataBound="gvmonthlypayroll_RowDataBound">
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />                                      
                                        <Columns>
                                                                   
                                         </Columns>
                                        </asp:GridView>
                                        </td>
                                </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
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
                please wait...</ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:content>

