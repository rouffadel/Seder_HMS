<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalSetteld.aspx.cs"  Inherits="AECLOGIC.ERP.HMS.FinalSetteld" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
  <script language="javascript" type="text/javascript">
      function GetEmpID(source, eventArgs) {
          var HdnKey = eventArgs.get_value();
          document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
        }
    </script>
   
 <table width="100%">
     <tr>
         <td>
             <table id="tblView"  width="100%" runat="server">
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
                                               <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                   <tr>
                                                       <td>
                                                            &nbsp;Employee Name&nbsp;
                                                        <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                                           <asp:TextBox ID="txtEmpName" runat="server" Height="22px" Width="300px" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;
						                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
							                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
							                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
							                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID" >
						                                        </cc1:AutoCompleteExtender>
					                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpName"
						                                        WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
					                                        </cc1:TextBoxWatermarkExtender>

                                                           <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />

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
         </td>
     </tr>
   <tr>
       <td>
            <asp:GridView ID="gvFinal" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" GridLines="Both"
                                EmptyDataText="No Records Found" Width="100%" CssClass="gridview" OnRowDataBound="gvFinal_RowDataBound" >
                 <Columns>
                       <asp:BoundField DataField="EmpName" HeaderText="EmployeeName"></asp:BoundField>
                     <asp:BoundField DataField="SettlementDate" HeaderText="Settlement Date"></asp:BoundField>
                    
                      <asp:BoundField DataField="AddedAmount" HeaderText="Added Amount"></asp:BoundField>
                      <asp:BoundField DataField="DeductedAmount" HeaderText="Deducted Amount"></asp:BoundField>
                     <asp:BoundField DataField="TotalAmt" HeaderText="Total Amount"></asp:BoundField>
                    
                 </Columns>
            </asp:GridView> 
       </td>
   </tr>
      <tr>
                <td style="height: 17px">
                    <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                </td>
            </tr>

 </table>
    </asp:Content>