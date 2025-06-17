<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="LeavesAvailable.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeavesAvailable" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;        
        }
        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtempname_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="LeavesAvaAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="109%">
                            <Panes>
                                <cc1:AccordionPane ID="LeavesAvaAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HiddenField ID="ddlWs_hid" runat="server" />
                                                    Worksite: &nbsp;<asp:DropDownList ID="ddlworksites" runat="server" Visible="false"
                                                        TabIndex="1" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" CssClass="droplist" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        OnClientItemSelected="GetID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp;Departments:&nbsp<asp:DropDownList ID="ddldepartments" Visible="false"
                                                        TabIndex="2" AccessKey="1" ToolTip="[Alt+1]" CssClass="droplist" runat="server"
                                                        OnSelectedIndexChanged="ddldepartments_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblEmpName" runat="server" Text="EmpName"></asp:Label>:
                                               <asp:HiddenField ID="txtempname_hid" runat="server" />
                                                    <asp:TextBox ID="txtEmpName" runat="server" TabIndex="4"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionEmpNameList" ServicePath="" TargetControlID="txtEmpName"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="txtWMEtxtEmpName" runat="server" TargetControlID="txtEmpName"
                                                        WatermarkText="[Filter EmpName]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                                        TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" Width="100px" OnClick="btnSearch_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSnc" runat="server" Text="Sync" CssClass="btn btn-warning"
                                                        TabIndex="5" ToolTip="" Width="100px" OnClick="btnSync_Click" />
                                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
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
                    <td colspan="2">
                        <asp:DataList ID="dtlWOProgress" runat="server" HeaderStyle-CssClass="datalistHead"
                            Width="100%" OnItemCommand="dtlWOProgress_ItemCommand"
                            OnItemDataBound="dtlWOProgress_ItemDataBound">
                            <ItemTemplate>
                                <div class="DivBorderOlive" style="margin-bottom: 20px">
                                    <table id="" style="width: 100%; background-color: #efefef;">
                                        <tr style="font-size: 13px;">
                                            <td style="width: 30%;">
                                                <b>Name </b>
                                                <asp:LinkButton ID="TransID" runat="server" Font-Bold="true" ForeColor="Blue" Text='<%#Bind("EMPName")%>' CommandName="det" CommandArgument='<%#Bind("empid")%>'></asp:LinkButton>
                                            </td>
                                            <td style="width: 25%;">
                                                <b>Date Of Join :</b>
                                                <asp:Label ID="lblTask" runat="server" Font-Bold="false" ForeColor="Blue" Text='<%#Bind("DateOfJoin")%>' />
                                            </td>
                                            <td style="width: 15%;"><b>Grade </b>
                                                <asp:LinkButton ID="lnlkgrade" Font-Bold="true" runat="server" Text='<%#Bind("Grade")%>' CommandName="Grade" CommandArgument='<%#Bind("Grade")%>'></asp:LinkButton>
                                            </td>
                                            <td style="width: 30%;"><b>Closing Balance  </b>
                                                <asp:Label ID="lblClosingBal" runat="server" Font-Bold="true" ForeColor="Blue" Text='<%#Bind("BalC")%>' />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:LinkButton ID="Linkexpand" runat="server" Text="Expand" OnClick="Unnamed_Click" CssClass="btn btn-primary"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Url") %>' Text="Details All" CssClass="btn btn-success" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gvexpand" Visible="false" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                        AutoGenerateColumns="false" DataSource='<%#BindTransdetails(Eval("EMPID").ToString())%>'
                                        HeaderStyle-CssClass="tableHead" Width="100%" CssClass="gridview">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="Bal" HeaderText="Closing balance Config" ItemStyle-Width="120px" />
                                            <asp:TemplateField Visible="true" HeaderText="Details">
                                                <HeaderTemplate>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hln_Details_leav" runat="server" NavigateUrl='<%# Eval("Url") %>' Text="Details" CssClass="btn btn-success" />
                                                    <asp:HyperLink ID="hln_Config_Leav" runat="server" NavigateUrl='<%# Eval("UrlConfig") %>' Text='<%# Eval("Configtext") %>' CssClass="btn btn-success" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="LeavesAvaliablePaging" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblgrade" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:GridView ID="gvEMPTrade" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            HeaderStyle-CssClass="tableHead" BorderWidth="2px" Width="100%">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"
                                CssClass="gridview" />
                            <Columns>
                                <asp:TemplateField HeaderText="Position Category" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPositionCategory" runat="server" Text='<%#Eval("PositionCategory")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic From" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalaryFrom" runat="server" Text='<%#Eval("SalaryFrom")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic TO" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalaryTo" runat="server" Text='<%#Eval("SalaryTo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Housing (%)" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHRA" runat="server" Text='<%#Eval("HRA")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tpt (%)" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Food" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFood" runat="server" Text='<%#Eval("Food")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AL" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAnnualLeave" runat="server" Text='<%#Eval("AnnualLeave")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entitlement" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFamilyEntitlement" runat="server" Text='<%#Eval("FamilyEntitlement")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tickets" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTickets" runat="server" Text='<%#Eval("Tickets")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VISA" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExitEntryVISA" runat="server" Text='<%#Eval("ExitEntryVISA")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Medical" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMedical" runat="server" Text='<%#Eval("Medical")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MedicalNos" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMedicalNos" runat="server" Text='<%#Eval("MedicalNos")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table id="tbldetails" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:GridView ID="gvReport" AutoGenerateColumns="false" runat="server" Width="100%"
                            HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            CssClass="gridview">
                            <Columns>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
