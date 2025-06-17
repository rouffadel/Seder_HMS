<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vacationemployees.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
     Inherits="AECLOGIC.ERP.HMS.vacationemployees" %>

 
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlsemp_hid.ClientID %>').value = HdnKey;
        }
    </script>
     <table>
                            <tr>
                                <td>
                                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="628">
                                        <Panes>
                                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>

                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                             
                                                            <td>
                                                                 <asp:Label ID="lblemp" runat="server" Text="Employee:"></asp:Label>
                                                                 <asp:HiddenField ID="ddlsemp_hid" runat="server" />
                                             <asp:TextBox ID="textempid" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionemployeeList" ServicePath="" TargetControlID="textempid"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textempid"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]">
                                           </cc1:TextBoxWatermarkExtender>

                                                <asp:Button ID="btnsearch" Text="Search" runat="server" CssClass="btn btn-primary" OnClick="btnsearch_Click" />
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
                                    <asp:GridView ID="gvVacation" runat="server" AutoGenerateColumns="False" width="100%" 
                                        CssClass="gridview"   EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" >
                                        <Columns>
                                            <asp:BoundField HeaderText="Empid" DataField="Empid"/>
                                            <asp:BoundField HeaderText="Name" DataField="Name"/>
                                            <asp:BoundField HeaderText="Leave Type" DataField="LeaveType"/>

                                            <asp:BoundField HeaderText="Days" DataField="Days"/>

                                            <asp:BoundField HeaderText="From Date" DataField="Fromdate"/>

                                            <asp:BoundField HeaderText="To Date" DataField="Todate"/>
                                            <asp:BoundField HeaderText="Report Date" DataField="Reportdate"/>


                                        </Columns>
                                        </asp:GridView>
                                    </td>
             </tr>
          <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
         </table>
</asp:Content>