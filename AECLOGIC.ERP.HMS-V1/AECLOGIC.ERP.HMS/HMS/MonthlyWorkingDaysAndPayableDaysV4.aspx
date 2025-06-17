<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyWorkingDaysAndPayableDaysV4.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.MonthlyWorkingDaysAndPayableDaysV4" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="salariesupdpanel" runat="server">
        <ContentTemplate>


            <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                <Panes>
                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                        ContentCssClass="accordionContent">
                        <Header>
                                            Search Criteria</Header>
                        <Content>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;                                             
                                           Month:                                          
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
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                            Year:
                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlyear" runat="server" CssClass="droplist" Width="100" />

                                        &nbsp;&nbsp;      
                                                    <asp:Button ID="btnsearch" Width="100" runat="server" CssClass="btn btn-primary" Text="Fetch Records" OnClick="btnsearch_Click" />
                                        &nbsp;&nbsp;&nbsp;   
                                                    <asp:Button ID="btnzeropayabledaysemp" Text="Zero Payable daysEmployees" Visible="false" Width="150" runat="server" CssClass="btn btn-danger" OnClick="btnzeropayabledaysemp_Click" /><br />
                                        <asp:Label ID="lblNote" Text="Note: SYNC When you edit in ATTENDANCE" ForeColor="Red"
                                            Font-Bold="true" Font-Size="Medium" runat="server"></asp:Label>
                                        &nbsp;&nbsp;
                                                  
                                                     
                                        <asp:HyperLink ID="nknavigate" runat="server" Text="Leave Rules" Font-Size="12px" Font-Bold="true" Target="_blank" NavigateUrl="~/HMS/HolidayPaidRules.aspx"></asp:HyperLink>

                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </cc1:AccordionPane>
                </Panes>
            </cc1:Accordion>





            <table id="tblunsync" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:GridView ID="gvunsync" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                            OnRowCommand="gvunsync_RowCommand" OnRowDataBound="gvunsync_RowDataBound" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="ws" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwsid" runat="server" Text='<%# Eval("Site_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Worksite">
                                    <ItemTemplate>
                                        <asp:Label ID="lblws" runat="server" Text='<%# Eval("Site_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlyeargrid" Enabled="false" runat="server" Width="60" DataTextField="Name" DataValueField="ID" CssClass="droplist" DataSource='<%# bindyear()%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlmonthgrid" runat="server" Width="100" CssClass="droplist" Enabled="false">
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Computed Datetime">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllastsyncgrid" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCompute" runat="server" CssClass="btn btn-success" Text="Compute" CommandName="com"
                                            CommandArgument='<%#Eval("Site_ID")%>' Enabled='<%# !Convert.ToBoolean(Eval("CanLock")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Records">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdisplay" runat="server" CssClass="btn btn-primary" Text="Show" CommandName="dis"
                                            CommandArgument='<%#Eval("Site_ID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Str Nos ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStrNos" runat="server" Text='<%# Eval("StrNos") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MissNos">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkMissNo" Text='<%# Eval("MissNos") %>' CssClass="btn btn-primary" runat="server" CommandName="MissNo" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FS">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFS" Text='<%# Eval("FS") %>' CssClass="btn btn-danger" runat="server" CommandName="FS" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zero Payble Days">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkZeroPayableDays" Text='<%# Eval("ZeroPDays") %>' CssClass="btn btn-danger" runat="server" CommandName="ZPD" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:Paging ID="Paging1" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                    </td>
                </tr>
            </table>
            <div id="msgempy" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblmsg1" runat="server" Text="Missing Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvmsngemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="Pagingmsg" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>

                    </tr>
                </table>
            </div>
            <div id="fsemp" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblsFS" runat="server" Text="FS Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvfsemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="Pagingfs" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>

                    </tr>
                </table>
            </div>
            <div id="zeroPayabledaysemp" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblmsg" runat="server" Text="Zero Payable Days Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvzeroPayabledaysemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="56%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="PagingzeroPayabledaysemp" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </div>


            <div id="dvView" runat="server" visible="false">

                <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                            Search Criteria</Header>
                            <Content>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>Emp Name:
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtemp" runat="server" Width="200px" ToolTip="Select Employee From the below populating List"
                                                AccessKey="e"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="ACESearchProject" runat="server" DelimiterCharacters=""
                                                Enabled="true" MinimumPrefixLength="2" ServiceMethod="GetEmpDetail"
                                                ServicePath="" TargetControlID="txtemp" UseContextKey="true" CompletionInterval="10"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                FirstRowSelected="True">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtemp"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Emp Name]"></cc1:TextBoxWatermarkExtender>

                                            <asp:Button ID="btnempsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnempsearch_Click" />
                                            <asp:LinkButton ID="lbllastsync" ForeColor="#0000ff" Font-Bold="true" Font-Size="14px" runat="server" OnClick="lbllastsync_Click"></asp:LinkButton>

                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvmnwrkngdays" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvmnwrkngdays_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Emp Name">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmpID") %>'></asp:Label>--%>
                                            <asp:Label ID="lnkemp" Text='<%# Eval("EmpID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Pay StartDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmnth" runat="server" Text='<%# Eval("Paystdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay EndDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblyr" runat="server" Text='<%# Eval("PayEnddate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Month Days">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpay" runat="server" Text='<%# Eval("TotalDays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payable Days">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="txttotal" runat="server" Text='<%# Eval("PayableDays") %>'></asp:Label>--%>
                                            <asp:LinkButton ID="lnkPay" Text='<%# Eval("PayableDays") %>' runat="server" CommandName="Pay" CommandArgument='<%#Eval("EmpID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adj Days">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPendPay" Text='<%# Eval("PendingPayableDays") %>' runat="server" CommandName="PPay" CommandArgument='<%#Eval("EmpID") %>'></asp:LinkButton>
                                            <%--<asp:Label ID="txttotal" runat="server" Text='<%# Eval("PendingPayableDays") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="taskpaging" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Table ID="tblAtt" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                            </asp:Table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:GridView ID="gdvAdjDiffDet" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Emp Name">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmpID") %>'></asp:Label>--%>
                                            <asp:Label ID="lnkemp" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Pay Days">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="txttotal" runat="server" Text='<%# Eval("PayableDays") %>'></asp:Label>--%>
                                            <asp:Label ID="lnkPay" Text='<%# Eval("PrePayableDays") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Pay Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkPendPay" Text='<%# Eval("CurrPayableDays") %>' runat="server"></asp:Label>
                                            <%--<asp:Label ID="txttotal" runat="server" Text='<%# Eval("PendingPayableDays") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAdj" Text="1. Previous Month Adjustments:" runat="server" Visible="false" Font-Size="16px" Font-Bold="true" ForeColor="OrangeRed"></asp:Label>
                            <br />
                            <asp:Label ID="lblPaidDays" Text="a. Paid Days:" runat="server" Visible="false" Font-Size="14px" Font-Bold="true" ForeColor="Green"></asp:Label>


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvMonthReport" runat="server" AutoGenerateColumns="true"
                                HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                                CssClass="gridview" OnRowDataBound="gdvMonthReport_RowDataBound" Visible="false">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblActualEligibility" Text="b. Actual Eligibility:" runat="server" Visible="false" Font-Size="14px" Font-Bold="true" ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvCurrentDetails" runat="server" AutoGenerateColumns="true"
                                HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                                CssClass="gridview" OnRowDataBound="gdvCurrentDetails_RowDataBound" Visible="false">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>


