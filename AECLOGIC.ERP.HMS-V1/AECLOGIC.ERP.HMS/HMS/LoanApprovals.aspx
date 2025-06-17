<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanApprovals.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.LoanApprovals" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) {
            var oGrid = document.getElementById(grid);
            var IPs = oGrid.getElementsByTagName("input");
            for (var iCount = 0; iCount < IPs.length; ++iCount) {
                if (IPs[iCount].type == 'checkbox')
                    IPs[iCount].checked = hChkBox.checked;
            }
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
          <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }
        function GETEmp_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <table id="tblvew" runat="server" width="100%">
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
                                                Search Criteria
                                           </Header>
                                    <Content>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <strong>Worksite&nbsp;
					                                                <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                        <asp:TextBox ID="txtSearchWorksite" runat="server" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="140px"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            OnClientItemSelected="GetID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                        EmployeeName/ID&nbsp
                                                        <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                                                        <asp:TextBox ID="TxtEmp" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="TxtEmp"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TxtEmp"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]"></cc1:TextBoxWatermarkExtender>
                                                        &nbsp;&nbsp
                                                Month:                                          
                                      <asp:DropDownList ID="ddlmonth" runat="server" Width="100" CssClass="droplist">
                                          <asp:ListItem Value="0">--All--</asp:ListItem>
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
                                                        &nbsp;&nbsp;
                                            Year:
                                          &nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" Width="100" />
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                        <asp:GridView ID="gvloans" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="gridview" EmptyDataText="No Records Found" OnRowDataBound="gvloans_RowDataBound" OnRowCommand="gvloans_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="20px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkESelectAll" Text="All" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEToTransfer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="monthid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmonthid" runat="server" Text='<%#Eval("monthid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" runat="server" Text='<%#Eval("empid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name of Employee" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpname" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan IDs">
                                    <ItemTemplate>
                                        <asp:Label ID="lblloanid" runat="server" Text='<%#Eval("loanid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Installment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcurrinst" runat="server" Text='<%#Eval("currinst") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="B/F Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrevPending" runat="server" Text='<%#Eval("PrevPending") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cum Ded">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCumDed" runat="server" Text='<%#Eval("CumDed") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Appx Sal">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAppxSal" runat="server" Text='<%#Eval("AppxSal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recmd Ded" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRecDed" runat="server" AutoPostBack="true" OnTextChanged="txtRecDed_TextChanged" Text='<%#Eval("RecDed") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="C/F Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCumBal" runat="server" Text='<%#Eval("CumBal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkApprove" runat="server" CssClass="btn btn-success" Text="Save" CommandName="App"
                                            CommandArgument='<%#Eval("Empid")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvloanApprvd" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="gridview" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:BoundField HeaderText="MonthID" DataField="MonthID" />
                                <%--<asp:BoundField HeaderText="Month" DataField="Month" />
                        <asp:BoundField HeaderText="Year" DataField="year" />--%>
                                <asp:BoundField HeaderText="Empid" DataField="Empid" />
                                <asp:BoundField HeaderText="Name of Employee" DataField="Name" ItemStyle-Width="300px" />
                                <asp:BoundField HeaderText="Loan IDs" DataField="loanid" />
                                <asp:BoundField HeaderText="Current Inst" DataField="CurrInst" />
                                <asp:BoundField HeaderText="B/F Amt" DataField="PrevPending" />
                                <asp:BoundField HeaderText="Cum Ded" DataField="CumDed" />
                                <asp:BoundField HeaderText="Appx Sal" DataField="AppxSal" />
                                <asp:BoundField HeaderText="Recmd Amt" DataField="RecDed" />
                                <asp:BoundField HeaderText="C/F Amt" DataField="CumBal" />
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
                    <td>
                        <asp:Button ID="btnsave" runat="server" Text="Save" Visible="false" CssClass="btn btn-success" OnClick="btnsave_Click" />
                    </td>
                </tr>
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
