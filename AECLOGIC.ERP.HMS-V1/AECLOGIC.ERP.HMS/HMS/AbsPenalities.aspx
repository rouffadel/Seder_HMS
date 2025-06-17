<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AbsPenalities.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AbsPenalities"
    MasterPageFile="../Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
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
        function validateSave() {
            if (document.getElementById('<%=ddlMonth.ClientID%>').value == "0") {
                alert("Select Month");
                document.getElementById('<%=ddlMonth.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').value == "") {
                alert("Select Year");
                document.getElementById('<%=ddlYear.ClientID%>').focus();
                return false;
            }
        }
        //chaitanya:validation for a textbox below code
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                <Panes>
                    <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                        ContentCssClass="accordionContent">
                        <Header>
                                                    Search Criteria</Header>
                        <Content>
                            <table id="tblstatus" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td align="left">Worksite:-
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlWS" Visible="false" AutoPostBack="true" runat="server" Width="200" CssClass="droplist"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                    </td>
                                    <td align="left">
                                        <%-- Designation:-
                                            <asp:DropDownList ID="ddlDept" Visible="false" AutoPostBack="true" runat="server" Width="200" CssClass="droplist"
                                                TabIndex="2">
                                            </asp:DropDownList>
                                <asp:TextBox ID="txtsearchDept" OnTextChanged="GetDep" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListDesg" ServicePath="" TargetControlID="txtsearchDept"
                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </cc1:AutoCompleteExtender>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtsearchDept"
                                    WatermarkCssClass="watermark" WatermarkText="[Enter Designation Name]">
                                </cc1:TextBoxWatermarkExtender>--%>
                                Month:<asp:DropDownList ID="ddlMonth" CssClass="droplist" runat="server" TabIndex="3" AccessKey="3" ToolTip="[Alt+3]">
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
                                        Year:<asp:DropDownList ID="ddlYear" CssClass="droplist" runat="server" AccessKey="4" ToolTip="[Alt+4]"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                        &nbsp;EmpID<asp:TextBox ID="txtEmpID" Width="50Px" Height="20px" runat="server" AccessKey="4" ToolTip="[Alt+4]"
                                            TabIndex="4" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Button ID="btnSync" runat="server" Text="Calculate" CssClass="btn btn-success" Width="60px"
                                            OnClick="btnSync_Click" />
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" Width="40px"
                                            OnClick="btnsearch_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                                              <asp:HyperLink ID="nknavigate" runat="server" Text="View Rule Position" Font-Size="12px" Font-Bold="true" Target="_blank" NavigateUrl="~/HMS/AbsentPenalties.aspx"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblNote" Text="Note: SYNC and APPROVE as many times as you edit data any where in the Salary process" ForeColor="Red"
                                            Font-Bold="true" Font-Size="Medium" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
                                <asp:Label ID="lblPLA" runat="server" Text="Penalty Limited to All :-    "></asp:Label>
                                        <asp:Label ID="lblRecord" Font-Bold="true" runat="server" Text="5"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </cc1:AccordionPane>
                </Panes>
            </cc1:Accordion>
            <table id="tblViewApproved" runat="server" width="80%">
                <tr>
                    <td>
                        <asp:GridView ID="gvViewApproved" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" OnRowCommand="gvViewApproved_RowCommand"
                            EmptyDataText="No Records Found" Width="100%" CssClass="gridview" OnRowDataBound="gvViewApproved_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" Text="All" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkToTransfer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EMP Id" ControlStyle-Width="15px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" Text='<%#Eval("Empid") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempname" Text='<%#Eval("empname") %>' runat="server" Style="width: 250px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Occurances">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpreviousoccurances" runat="server" Text='<%#Eval("previousoccurances") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="This Occurance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOccurance" runat="server" Text='<%#Eval("Occurance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Occurances">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaloccurance" runat="server" Text='<%#Eval("totaloccurance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Absents">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAbsents" runat="server" Text='<%#Eval("Absents") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Penalities">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpenalities" runat="server" Text='<%#Eval("Penalities") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkApprove" runat="server" CssClass="btn btn-success" Text="Approve" CommandName="App"
                                            CommandArgument='<%#Eval("Empid")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Transid" HeaderText="TransID" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementAprovedPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnTransferAcc" CssClass="btn btn-success" ToolTip="Transfer To Accounts Dept."
                            runat="server" Text="Approve All" OnClick="btnTransferAcc_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                    <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                        <img src="IMAGES/Loading.gif" alt="update is in progress" />
                        <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" />
                    </asp:Panel>
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
