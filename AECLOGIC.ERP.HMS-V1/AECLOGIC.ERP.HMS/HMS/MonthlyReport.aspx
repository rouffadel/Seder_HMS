<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="MonthlyReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MonthlyReport" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Validate() {

           <%-- if (document.getElementById('<%=ddlWorksite.ClientID%>').value == "0") {
                alert("Please Select Worksite");
                return false;--%>
           // }
            if (document.getElementById('<%=ddlMonth.ClientID%>').value == "0") {
                alert("Please Select Month");
                return false;
            }

        }


        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
               
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <strong>Worksite :&nbsp;
                                                                          
                                                <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                        </strong>&nbsp;&nbsp; <strong>Month :&nbsp;</strong><asp:DropDownList ID="ddlMonth"
                                                                            runat="server" AutoPostBack="true" CssClass="droplist">
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
                                                                        &nbsp;&nbsp; <strong>Year :&nbsp;</strong><asp:DropDownList ID="ddlYear" runat="server"
                                                                            AutoPostBack="true" CssClass="droplist"></asp:DropDownList>
                                                                            
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="btnMonthReport" runat="server" CssClass="savebutton" Text="Generate Month Report"
                                                                            OnClick="btnMonthReport_Click" OnClientClick="javascript:return Validate();" />
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:Accordion>
                                            <tr>
                                                <td>
                                                    &nbsp
                                                </td>
                                            </tr>
                                          
                                            <tr>
                                                <td>
                                                    &nbsp
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <asp:GridView ID="grdMonthlyReport" runat="server" AutoGenerateColumns="false" ForeColor="#333333"
                                                        GridLines="Both" Width="50%" CellPadding="4" HeaderStyle-CssClass="tableHead"
                                                        EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="pageheader" CssClass="gridview" >
                                                        <Columns>
                                                            <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="NumofPresents" HeaderText="Number of Presents" HeaderStyle-HorizontalAlign="Left" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
