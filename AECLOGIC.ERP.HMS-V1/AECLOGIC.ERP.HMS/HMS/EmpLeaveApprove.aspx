<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpLeaveApprove.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpLeaveApprove" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table>
        
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
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
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label>:
                                                        <asp:DropDownList ID="ddlworksites" runat="server" CssClass="droplist" AccessKey="w"
                                                            ToolTip="[Alt+w+Enter]" TabIndex="1">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:
                                                        <asp:DropDownList ID="ddldepartments" runat="server" CssClass="droplist" TabIndex="2">
                                                        </asp:DropDownList>
                                                        &nbsp;<asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>
                                                        <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblEmpID" runat="server" Text="EmpID"></asp:Label>:
                                                        <asp:TextBox ID="txtEmpID" Width="50" runat="server" AccessKey="2" ToolTip="[Alt+2]"
                                                            TabIndex="4"></asp:TextBox>
                                                        <asp:Label ID="lblName" runat="server" Text="Emp Name"></asp:Label>:
                                                        <asp:TextBox ID="txtusername" Width="90" runat="server" MaxLength="30" CssClass="droplist"
                                                            AccessKey="3" ToolTip="[Alt+3]" TabIndex="5"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" Width="80px"
                                                            AccessKey="i" OnClick="btnSearch_Click" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="6" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdEmpLeaveApp" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                    CssClass="gridview" EmptyDataText="No Records Found" 
                    HeaderStyle-CssClass="tableHead" 
                    onrowdatabound="grdEmpLeaveApp_RowDataBound" 
                    onrowcommand="grdEmpLeaveApp_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="EmpID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name") %>' ToolTip='<%#Eval("OldEmpID")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblWorksite" runat="server" Text='<%#Eval("SiteName")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recommended">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlReco1" runat="server" CssClass="droplist" DataTextField="name"
                                    DataValueField="EMPID" >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..."
                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                    TargetControlID="ddlReco1">
                                </cc1:ListSearchExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level1">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlReco2" runat="server" CssClass="droplist" DataTextField="name"
                                    DataValueField="EMPID" >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                    TargetControlID="ddlReco2">
                                </cc1:ListSearchExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level2">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlReco3" runat="server" CssClass="droplist" DataTextField="name"
                                    DataValueField="EMPID">
                                </asp:DropDownList>
                                 <cc1:ListSearchExtender ID="ListSearchExtender3" IsSorted="true" PromptText="Type Here To Search..."
                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                    TargetControlID="ddlReco3">
                                </cc1:ListSearchExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Upd" CommandArgument='<%#Eval("EmpId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="EmptyRowData" />
                    <HeaderStyle CssClass="tableHead" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="EmpLeaveApprovePaging" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
