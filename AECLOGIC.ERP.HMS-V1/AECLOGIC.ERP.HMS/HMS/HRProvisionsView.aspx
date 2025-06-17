<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRProvisionsView.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HRProvisionsView" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=txtSearchemp_hid.ClientID %>').value = HdnKey;
        }
         </script>

     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
       
         <tr>
             <td>
                <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" CssClass="btn btn-warning" Width="100" Height="17px" Text="Back" />
             </td>
         </tr>
         <tr>
            <td>
               
                        <table style="width: 100%; height: 100%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                        <Panes>
                                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                        <td>
                                                             <asp:HiddenField ID="txtSearchemp_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchemp" AutoPostBack="false" Height="22px" Width="150px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchemp"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchemp"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employeee Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                            &nbsp;&nbsp;
                                                              <asp:DropDownList ID="ddlyear" runat="server" Width="100" CssClass="droplist">
                                                                  </asp:DropDownList>
                                                              &nbsp;&nbsp;
                                                             <asp:DropDownList ID="ddlmonth" runat="server" Width="100" CssClass="droplist" >
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
                                      
                                                            <td>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"  Width="53"/>  
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
<asp:GridView ID="gvview" runat="server" 
     AutoGenerateColumns="false" Width="100%" CssClass="gridview" EmptyDataText="No Records Found"
     EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
<Columns>
   <asp:BoundField DataField="EmpName" HeaderText="Emp Name" />
     <asp:BoundField DataField="Year" HeaderText="Year" />
     <asp:BoundField DataField="Month" HeaderText="Month" />
     <asp:BoundField DataField="Amount" HeaderText="Amount" />
     <asp:BoundField DataField="ALAccured" HeaderText="AL Accured" />
     <asp:BoundField DataField="NoOfTickets" HeaderText="No Of Tickets" />
     <asp:BoundField DataField="AirTicketAmount" HeaderText="AirTicket Amount" />
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
                   
           </td>
        </tr>
         <asp:HiddenField ID="hdn" runat="server" />
</table>
</asp:Content>
  

  