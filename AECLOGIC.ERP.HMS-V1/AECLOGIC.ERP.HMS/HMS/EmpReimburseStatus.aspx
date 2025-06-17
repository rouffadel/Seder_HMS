<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpReimburseStatus.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpReimburseStatus" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
               
                <table id="tblOther" runat="server" width="100%" visible="false">
                    <tr>
                        <td>
                            <cc1:Accordion ID="EmpReimbStatusAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                Height="106px" Width="100%">
                                <Panes>
                                    <cc1:AccordionPane ID="EmpReimbStatusAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="padding-left: 100Px">
                                                        <asp:DropDownList ID="ddlFilterEmp" AutoPostBack="true" runat="server"  CssClass="droplist" OnSelectedIndexChanged="ddlFilterEmp_SelectedIndexChanged" AccessKey="1" ToolTip="[Alt+1]" TabIndex="1">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..."
                                                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                            TargetControlID="ddlFilterEmp" />
                                                        <asp:TextBox ID="txtFilterEmp" runat="server" AccessKey="2" ToolTip="[Alt+2]" TabIndex="2"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtFilterEmp"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name Search]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        &nbsp;<asp:TextBox ID="txtFilterEmpID" runat="server" AccessKey="3" ToolTip="[Alt+3]" TabIndex="3"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtFilterEmpID"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter ByEmpID]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <asp:Button ID="btnFilter" CssClass="savebutton" runat="server" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="4" Text="Search" OnClick="btnFilter_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </td>
                    </tr>
                </table>
                <table id="tblView" runat="server" width="100%">
                    <tr>
                        <td class="pageheader">
                            Reimbursements Status
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                                EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvView_RowCommand" CssClass="gridview">
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Name" DataField="Name" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#FormatInput(Eval("Status")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" CommandName="View" CommandArgument='<%#Eval("Status") %>'
                                                runat="server">View</asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="EmpReimbursementStatusPaging" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <asp:HiddenField ID="hdnSearchChangeStatus" Value="0" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tblCommon" runat="server" visible="false" width="100%">
                    <tr>
                        <td class="pageheader">
                            Itmes View
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                                Width="100%" EmptyDataText="No Records Found" CssClass="gridview">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item_No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPK" runat="server" Text='<%#Eval("ERItemID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RItem" HeaderText="ReimburseItem" />
                                    <asp:TemplateField HeaderText="Units of Measure">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' Enabled="false"
                                                DataTextField="AU_Name" DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                                runat="server" AutoPostBack="false"  CssClass="droplist" >
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UnitRate" HeaderText="Unit Rate" />
                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField HeaderStyle-Width="140Px" DataField="Purpose" HeaderText="Purpose" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DateOfSpent" HeaderText="DateOfSpent" />
                                    <asp:BoundField DataField="DOS" HeaderText="DOS" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblERID" runat="server" Text='<%#Eval("ERID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkView" NavigateUrl='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                                runat="server">Proof</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table id="tblReject" runat="server" visible="false" width="100%">
                    <tr>
                        <td class="pageheader">
                            Rejected Itmes
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvShowRej" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                                Width="100%" EmptyDataText="No Records Found" CssClass="gridview">
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="ReimburseItem" DataField="RItem" />
                                    <asp:TemplateField HeaderText="Units of Measure">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' Enabled="false"
                                                DataTextField="AU_Name" DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                                runat="server" AutoPostBack="false"  CssClass="droplist" >
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Unit Rate" DataField="UnitRate" />
                                    <asp:BoundField HeaderText="Quantity" DataField="Qty" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                    <asp:BoundField HeaderStyle-Width="140Px" HeaderText="Purpose" DataField="Purpose" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Spent On" DataField="DateOfSpent" />
                                    <asp:BoundField HeaderText="Submitted On" DataField="DOS" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPK" runat="server" Text='<%#Eval("ERItemID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblERID" runat="server" Text='<%#Eval("ERID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkView" NavigateUrl='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                                runat="server">Proof</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderStyle-Width="140Px" HeaderText="Reason" DataField="Reason" />
                                    <asp:BoundField HeaderText="RejectedBy" DataField="RejectedBy" />
                                    <asp:BoundField HeaderText="RejectedOn" DataField="RejectedOn" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
