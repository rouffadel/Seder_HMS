<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRProvisions.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HRProvisions" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetWorkID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=Txtwrk_hid.ClientID %>').value = HdnKey;
        }
        function GetDeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=TxtDept_hid.ClientID %>').value = HdnKey;
        }
        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtSearchemp_hid.ClientID %>').value = HdnKey;
           }
           function SelectAll(hChkBox, grid, tCtrl) {
               var oGrid = document.getElementById(grid);
               var IPs = oGrid.getElementsByTagName("input");
               for (var iCount = 0; iCount < IPs.length; ++iCount) {
                   if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked;
               }
           }
    </script>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <table style="width: 100%; height: 100%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                        <Panes>
                                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:HiddenField ID="Txtwrk_hid" runat="server" />
                                                                <asp:TextBox ID="Txtwrk" Height="22px" Width="150px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptWorkList" ServicePath="" TargetControlID="Txtwrk"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetWorkID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="Txtwrk"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                             <asp:HiddenField ID="TxtDept_hid" runat="server" />
                                                                <asp:TextBox ID="TxtDept" AutoPostBack="true" Height="22px" Width="150px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="TxtDept"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetDeptID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TxtDept"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                             <asp:HiddenField ID="txtSearchemp_hid" runat="server" />
                                                                <asp:TextBox ID="txtSearchemp" AutoPostBack="false" Height="22px" Width="150px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchemp"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchemp"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Employeee Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                              <asp:DropDownList ID="ddlyear" runat="server" Width="100" CssClass="droplist">
                                                              </asp:DropDownList>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                             <asp:DropDownList ID="ddlmonth" runat="server" Width="100" CssClass="droplist">
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
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" Width="53" />
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
                                    <asp:GridView ID="gvProvisions" OnRowDataBound="gvProvisions_RowDataBound" runat="server"
                                        AutoGenerateColumns="false" Width="100%" CssClass="gridview" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Center" Width="5" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPrereq" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Empid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%# Eval("Empid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Work Site" DataField="Sitename" />
                                            <asp:BoundField HeaderText="Department" DataField="Departmentname" />
                                            <asp:BoundField HeaderText="Employee" DataField="EmpName" />
                                            <asp:TemplateField HeaderText="Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmnth" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Salary" HeaderStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkSalary" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"Empid").ToString())%>'
                                                        CommandName="View" Text='<%#Eval("GrossSalary") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AL Accured" HeaderStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlkALAccured" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl1(DataBinder.Eval(Container.DataItem,"Empid").ToString())%>'
                                                        CommandName="View" Text='<%#Eval("ALAccured") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Tickets">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfTickets" runat="server" Text='<%# Eval("NoOfTickets") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AirTicket">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAirTicketAmount" runat="server" Text='<%# Eval("AirTicketAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GrossSalary" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrossSalary" runat="server" Text='<%# Eval("GrossSalary") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ALAccured" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblALAccured" runat="server" Text='<%# Eval("ALAccured") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trans Id" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransId" runat="server" Text='<%#Eval("transid")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnapprove" runat="server" CssClass="btn btn-success" OnClick="btnapprove_Click" Text="Approve All" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:HiddenField ID="hdn" runat="server" />
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="upMain">
            <ProgressTemplate>
                <img src="IMAGES/updateProgress.gif" alt="update is in progress" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
