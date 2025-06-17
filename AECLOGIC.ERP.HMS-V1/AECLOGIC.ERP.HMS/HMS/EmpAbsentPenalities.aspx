<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpAbsentPenalities.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.HMS.EmpAbsentPenalities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script>

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWS_hid.ClientID %>').value = HdnKey;          
        }

        function GetDepID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDept_hid.ClientID %>').value = HdnKey;
        }
    </script>
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
                                                        <td>
                        <asp:DropDownList ID="ddlWS" Visible="false" runat="server" Width="200" CssClass="droplist" 
                                         TabIndex="1">
                                    </asp:DropDownList>
                                <%--</td>
                                <td>--%>  <asp:HiddenField ID="ddlWS_hid" runat="server" />
                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                                                        </strong>&nbsp;&nbsp;</td>
                                                        <td>
                                   <asp:HiddenField ID="ddlDept_hid" runat="server" />
                               <asp:DropDownList ID="ddlDept" Visible="false" runat="server" Width="200" CssClass="droplist" AutoPostBack="True"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlDept"></cc1:ListSearchExtender>
                                    <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetDepID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <td>
                                                             EmpID&nbsp;<asp:TextBox ID="txtEmpID" Width="50" runat="server" AccessKey="1"
                                                                            ToolTip="[Alt+1]"></asp:TextBox>
                                                                          
                                                            
                                                        </td>
                                                        <td>
                                                        
                                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="droplist"
                                                                OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
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
                                                            &nbsp;
                                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="droplist"
                                                                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;
                                                            <asp:Button ID="btnMonthReport" ToolTip="Generate Month Report" runat="server" CssClass="savebutton"
                                                                OnClick="btnMonthReport_Click" Text="Month Report" />
                                                            &nbsp;
                                                        
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
           <asp:GridView ID="gvHolidays" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                PageSize="10" AllowPaging="true" OnPageIndexChanging="gvHolidays_PageIndexChanging" 
               OnRowCommand="gvHolidays_RowCommand" OnRowDataBound="GvHolidays_OnRowDataBound">
                <Columns>
                    <asp:BoundField DataField="EmpID" HeaderText="EmpID"  />
                    <asp:BoundField DataField="EmpName" HeaderText="EmpName"  />
                    <asp:BoundField DataField="TotalPnalities" HeaderText="Total Pnalities"  />            
                     <asp:BoundField DataField="Basesalary" HeaderText="Base Salary"  />
                     <asp:BoundField DataField="PenalityAmount" HeaderText="Penality Amount"  />
                   
                      <asp:TemplateField HeaderText="Details">
                       <ItemTemplate>
                     <a id="lblUploadProof" runat="server" width="50" onclick='<% #GetCompliance(Eval("EmpID").ToString())%>'>
                                Details </a>
                       </ItemTemplate>
                      </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="droplist" Enabled="false">
                                <asp:ListItem Text="Terminate" Value="0">
                                </asp:ListItem>
                                <asp:ListItem Text="Penality" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                              <asp:LinkButton ID="Emp" runat="server" Text="Add" CommandName="Add"
                                                        CommandArgument='<%#Eval("EmpId")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                  
                </Columns>
           </asp:GridView>
        </td>
    </tr>
</table>
    </asp:Content>
