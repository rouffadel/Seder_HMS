<%@ Page Title="" Language="C#"   AutoEventWireup="True" 
    CodeBehind="EmpWorkingDetails.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpWorkingDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table width="100%">
       
        <tr>
            <td>
                <cc1:Accordion ID="EmpWorkDetailsAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="1046px">
                    <Panes>
                        <cc1:AccordionPane ID="EmpWorkDetails" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria
                            </Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label>:
                                            <asp:DropDownList ID="ddlworksites" visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" CssClass="droplist" AccessKey="w"
                                                ToolTip="[Alt+w+Enter]">
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

                                            <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:
                                            <asp:DropDownList ID="ddldepartments" visible="false" runat="server" CssClass="droplist" TabIndex="2">
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

                                             <asp:Label ID="lblEmpNature" runat="server" Text="EmpNature"></asp:Label>:
                                            <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>:
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist">
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
                                             
                                            
                                             
                                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>:
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                             
                                            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>:
                                            <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"
                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                            
                                             <asp:Label ID="lblEmpid" runat="server" Text="EmpID"></asp:Label>:
                                            <asp:TextBox ID="txtempid" runat="server"></asp:TextBox>
                                             

                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" Width="80px"
                                                AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="6" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Joined" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Relieved"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="WorkingHistory"></asp:ListItem>
                                            </asp:RadioButtonList>
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
                <asp:GridView ID="grdEmpWrkDetails" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                    CssClass="gridview" EmptyDataText="No Records Found" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Type" HeaderText="EmpType" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Nature" HeaderText="EmpNature" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblWorksite" runat="server" Text='<%#Eval("Site_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateofJoin" HeaderText="Date of join" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DateofReleave" HeaderText="Date of releave" HeaderStyle-HorizontalAlign="Left"
                            Visible="false" />
                    </Columns>
                    <EmptyDataRowStyle CssClass="EmptyRowData" />
                    <HeaderStyle CssClass="tableHead" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="EmpWorkingDetailsPaging" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
