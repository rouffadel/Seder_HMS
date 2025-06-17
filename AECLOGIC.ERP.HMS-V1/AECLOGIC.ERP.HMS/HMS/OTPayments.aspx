<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="OTPayments.aspx.cs" Inherits="AECLOGIC.ERP.HMS.OTPayments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
            <table width="100%">
                <tr>
                    <td>

                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                            SelectedIndex="0">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                                                Search Criteria
                                                            </Header>
                                    <Content>
                                        <div>

                                            <table>
                                                <tr>

                                                    <b>Worksite</b>&nbsp;<asp:DropDownList ID="ddlWorksite" Visible="false" runat="server" OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged" AutoPostBack="True"
                                                        CssClass="droplist">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    &nbsp;
                                                                                   <strong>Department</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddlDepartment" Visible="false" runat="server"
                                                                                       AutoPostBack="True" CssClass="droplist">
                                                                                       <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                   </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>

                                                    &nbsp;<b>EMPID</b>
                                                    <asp:TextBox ID="txtEMPID" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEMPID"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter EMP ID]"></cc1:TextBoxWatermarkExtender>

                                                </tr>
                                                <tr>

                                                    <b>
                                                        <asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label>:</b>
                                                    <asp:DropDownList ID="ddlDesif2" Visible="false" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearchDesi" OnTextChanged="GetDesignation" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListDesi" ServicePath="" TargetControlID="txtSearchDesi"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSearchDesi"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Designation Name]"></cc1:TextBoxWatermarkExtender>

                                                    &nbsp;<b>Month</b>
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                        ToolTip="[Alt+2]">
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
                                                    </asp:DropDownList><br />
                                                    &nbsp;<b>Year</b>
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                        AccessKey="3">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                                                                        OnClick="btnSearch_Click" />
                                                    &nbsp;&nbsp;
                                                                                         <asp:Button ID="btnSync" OnClientClick="return confirm('Are you sure to synchronise OT?')"
                                                                                             ToolTip="Sync Emp if Record not found in Grid " runat="server" Text="Sync OT" CssClass="btn btn-warning"
                                                                                             Tag="0" OnClick="btnSync_Click" TabIndex="8" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                 <asp:Button ID="btnApproveSalas" OnClientClick="return confirm('Are you sure to Approve OT?')"
                                                                     ToolTip="Sync Emp if Record not found in Grid " runat="server" Text="OT Approve"
                                                                     Tag="1" OnClick="btnSync_Click" TabIndex="8" CssClass="btn btn-success" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                              
                                                                                
                                                </tr>


                                            </table>

                                        </div>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: left; vertical-align: top;">

                        <asp:GridView ID="gdvOTpaymentLst" runat="server" AutoGenerateColumns="False" CellPadding="1"
                            CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None"
                            EmptyDataText="No Records Found"
                            EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gdvWS_RowCommand"
                            OnRowDataBound="gdvWS_RowDataBound"
                            Width="100%" CssClass="gridview">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempID" Text='<%#Eval("EmpId")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--  <asp:BoundField DataField="EmpId" HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                <asp:BoundField DataField="EmpName" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                    SortExpression="EmpName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <%--  <asp:BoundField DataField="NWHS" HeaderText="Normal WHS" HeaderStyle-HorizontalAlign="Left" 
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblNWHS" ToolTip="Normal Working Hours" runat="server" Text="Normal WHS"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNWHSitem" runat="server" Text='<%#Eval("NWHS")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblWOWHS" ToolTip="Weekoff Working Hours" runat="server" Text="WO WHS"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeekofWHS" runat="server" Text='<%#Eval("WWHS")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblPHWHS" ToolTip="PublicHoliday Working Hours" runat="server" Text="PH WHS"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecialWHS" runat="server" Text='<%#Eval("SWHS") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <%-- <asp:BoundField DataField="NSDWHS" HeaderText="Normal SD WHS" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblNWHS" ToolTip="Normal SpecialDay Working Hours" runat="server" Text="Normal SD WHS"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNWHSitem1" runat="server" Text='<%#Eval("NSDWHS")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblNWHS" ToolTip="WeekOff SpecialDay Working Hours" runat="server" Text="WO SD WHS"></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblSDWOWHS" runat="server" Text='<%#Eval("WSDWHS")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderStyle-HorizontalAlign="right"
                                    ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblNWHS" ToolTip="PublicHoliday SpecialDay  Working Hours" runat="server" Text="PH SD WHS"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSDWHS" runat="server" Text='<%#Eval("SSDWHS") %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate/Hour" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTDSAmount" runat="server" Text='<%#Eval("RateofWHS")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPFAmount" runat="server" Text='<%# GetOTAmount(decimal.Parse(Eval("Amount").ToString())).ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lblPFTot" runat="server" Text='<%# GetOT().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ApprovalText")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" runat="server" Text="Details" CssClass="btn btn-primary" CommandName="Edt"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EmpId")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>

                            </Columns>


                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                        <asp:GridView ID="gvrOTSal" runat="server" AutoGenerateColumns="False" CellPadding="1"
                            CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None"
                            EmptyDataText="No Records Found"
                            EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>


                                <asp:BoundField DataField="EmpID" HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left"
                                    Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle Width="10px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPName" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Salary" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Sal_M")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Sal_D")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shift" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Shift_Hr")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="160px" />
                                    <HeaderStyle Width="160px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Breaks" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("BreakHrs")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Working Hrs " HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("WorkingHrs")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle Width="90px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Salary / Hr" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Sal_H")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>


                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                            </Columns>


                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <asp:GridView ID="gvrOTHrs" runat="server" AutoGenerateColumns="False" CellPadding="1"
                        CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None"
                        EmptyDataText="No Records Found"
                        EmptyDataRowStyle-CssClass="EmptyRowData" ShowFooter="True" CssClass="gridview">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <Columns>



                            <asp:BoundField DataField="dtemp" HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="100px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InTime" HeaderText="In Time" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="90px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OutTim" HeaderText="Out Time" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="90px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <%-- <asp:BoundField DataField="ActHrs" HeaderText="Act Working Hrs " HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                 <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="OT Hrs" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("OTHrs")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemStyle Width="90px" />
                                <HeaderStyle Width="90px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="OT Coeft." HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("OT_Coef")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemStyle Width="90px" />
                                <HeaderStyle Width="90px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Salary / Hr." HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemStyle Width="90px" />
                                <HeaderStyle Width="90px" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right">

                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Amount")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b>
                                        <asp:Label ID="Label6_1" runat="server" Text='<%# TotNoEMP.ToString()%>'></asp:Label></b>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemStyle Width="120px" />
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="10px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="160px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>


                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
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

