<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpReconSal.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpReconSal" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table id="tblview" runat="server" width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                    <Header>
                                        Click here to Search</Header>
                                    <Content>
                                        <table id="Table1" class="SearchStlye" width="100%">
                                            <tr>
                                                <td>Month:
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="01">January</asp:ListItem>
                                                    <asp:ListItem Value="02">February</asp:ListItem>
                                                    <asp:ListItem Value="03">March</asp:ListItem>
                                                    <asp:ListItem Value="04">April</asp:ListItem>
                                                    <asp:ListItem Value="05">May</asp:ListItem>
                                                    <asp:ListItem Value="06">June</asp:ListItem>
                                                    <asp:ListItem Value="07">July</asp:ListItem>
                                                    <asp:ListItem Value="08">August</asp:ListItem>
                                                    <asp:ListItem Value="09">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;
                                                      Year:
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3" Width="90">
                                                                </asp:DropDownList>
                                                    <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" />
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
                        <asp:GridView ID="gvmnth" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="60%" EmptyDataText="No Record(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview" OnRowCommand="gvmnth_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="Worksite" DataField="Site_Name" />
                                <asp:BoundField HeaderText="WSEmpCount" DataField="wsempcount" />
                                <asp:BoundField HeaderText="EmpCount" DataField="empcount" />
                                <asp:BoundField HeaderText="Diffrence" DataField="diff" />
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmnth" runat="server" Text='<%#Eval("Month")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary" Text="Show" CommandName="Sel" CommandArgument='<%#Eval("Categary")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblincmng" runat="server" Text="Incoming Employees" Visible="false" Font-Bold="true" Font-Size="14px" ForeColor="#0066ff"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvwswise" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="60%" EmptyDataText="No Record(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview" OnRowCommand="gvwswise_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="SNO" DataField="SNO" />
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkemp" Text='<%# Eval("EMPName") %>' runat="server" CommandName="att" CommandArgument='<%#Eval("empid") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Last Att Date" DataField="lastattdate" />
                                <asp:BoundField HeaderText="Att Site" DataField="attsitename" />
                                <asp:BoundField HeaderText="Present Site" DataField="SiteName" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Outgoing Employees" Font-Bold="true" Visible="false" Font-Size="14px" ForeColor="#0066ff"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvout" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="60%" EmptyDataText="No Record(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview" OnRowCommand="gvout_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="SNO" DataField="SNO" />
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkemp" Text='<%# Eval("EMPName") %>' runat="server" CommandName="att" CommandArgument='<%#Eval("empid") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Last Att Date" DataField="lastattdate" />
                                <asp:BoundField HeaderText="Att Site" DataField="attsitename" />
                                <asp:BoundField HeaderText="Present Site" DataField="SiteName" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
