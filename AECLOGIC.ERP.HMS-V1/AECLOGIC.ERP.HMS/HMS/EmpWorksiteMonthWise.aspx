<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpWorksiteMonthWise.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.EmpWorksiteMonthWise" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div id="dvView" runat="server">
                <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="70%">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                            Search Criteria</Header>
                            <Content>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>Month:<span style="color: red">*</span>
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
                                        </td>
                                        <td>Year:<span style="color: red">*</span>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlyear" runat="server" CssClass="droplist" Width="100" />
                                        </td>
                                        <td>Emp Name:<span style="color: red">*</span>
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
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="true" CssClass="gridview"
                                Width="100%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                HeaderStyle-CssClass="tableHead" AlternatingRowStyle-BackColor="GhostWhite">
                                <EmptyDataRowStyle CssClass="EmptyRowData" />
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
