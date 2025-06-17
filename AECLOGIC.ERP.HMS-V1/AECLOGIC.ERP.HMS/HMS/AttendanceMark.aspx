<%@ Page Title="" Language="C#"   AutoEventWireup="True" 
    CodeBehind="AttendanceMark.aspx.cs"  Inherits="AECLOGIC.ERP.HMS.AttendanceMark" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    
            <table width="100%">
               
                <tr>
                    <td colspan="2" class="pageheader">
                        List of Employees Involved in Marking Today Attendance:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMark" Width="40%" AutoGenerateColumns="false" CssClass="gridview"
                            runat="server" ShowFooter="true">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="EmpID" DataField="EmpID" Visible="false"/>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:TemplateField HeaderText="Mark Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Text='<%#Bind("MarkCount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lbltotCount" runat="server" Text='<%# GetTotal().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="style1" style="width: 377px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="pageheader">
                        Site Wise Attendance Marked By Employees:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                            SelectedIndex="0" Height="85px" Width="774px">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria
                                    </Header>
                                    <Content>
                                        <asp:UpdatePanel ID="updEdtendance" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td class="style1" style="width: 377px">
                                                            <b>Date Wise View: </b>
                                                            <asp:TextBox ID="txtDay" runat="server" AutoPostBack="true" OnTextChanged="txtDay_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDay"
                                                                PopupButtonID="txtDOB" Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            WorkSite:
                                                    <asp:DropDownList ID="ddlworksite" runat="server" DataValueField="ID" DataTextField="Name" AutoPostBack="true" TabIndex="2" AccessKey="v" ToolTip="[Alt+v OR Alt+v+Enter]" 
                                                        AppendDataBoundItems="true" Width="150" CssClass="droplist" OnSelectedIndexChanged="ddlworksite_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                        TargetControlID="ddlworksite" PromptText="Type to search" PromptCssClass="PromptText"
                                                        PromptPosition="Top" IsSorted="true" />
                                                </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvListOfMark" AutoGenerateColumns="false" Width="70%" CssClass="gridview"
                            runat="server" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="SiteID" Visible="false" DataField="SiteId" />
                                <asp:BoundField HeaderStyle-Width="20%" HeaderText="EmpID" DataField="EmpID" Visible="false"/>
                                <asp:BoundField HeaderStyle-Width="40%" HeaderText="Marked By" DataField="EmpName" />
                                <asp:BoundField HeaderStyle-Width="40%" HeaderText="Site" DataField="Site_Name"/>
                                <asp:BoundField HeaderStyle-Width="40%" HeaderText="Mark Count" DataField="Marked" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        
</asp:Content>
