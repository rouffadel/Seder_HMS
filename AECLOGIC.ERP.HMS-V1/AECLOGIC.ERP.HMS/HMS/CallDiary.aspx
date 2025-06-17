<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="CallDiary.aspx.cs" Inherits="AECLOGIC.ERP.HMS.CallDiary" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <table width="100%" align="left">
    <tr>
                <td class="pageheader" colspan="2">
                    Call Diary&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" width="80%">
                    <table>
                     
                                 <tr>
                 <td>
                 Caller
                 
                 </td>
                  <td>
                 
                 <asp:TextBox ID="txtCaller" runat="server" Height="28px" Width="189px"></asp:TextBox>

                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="true"
                                                            MinimumPrefixLength="1" ServiceMethod="GetVistList" ServicePath="" TargetControlID="txtCaller"
                                                            UseContextKey="true" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>

                 </td>
            </tr>
    <tr>
                 <td>
                 Company
                 
                 </td>
                  <td>
                 <asp:TextBox ID="txtCompany" runat="server" Height="27px" Width="186px" ></asp:TextBox>
                  <cc1:AutoCompleteExtender ID="ACEtxtWs" runat="server" DelimiterCharacters="" Enabled="true"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtCompany"
                                                            UseContextKey="true" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>
                 </td>
            </tr><tr>
                 <td>
                 Seeking
                 
                 </td>
                  <td>
                 <asp:DropDownList ID="ddlEmp" runat="server"></asp:DropDownList>
                 
                 </td>
            </tr><tr>
                 <td valign="top">
                 
                 Message
                 </td>
                  <td valign="top">
                      <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="70px" 
                          Width="190px"></asp:TextBox>
                 
                 </td>
            </tr><tr>
                 <td>
                  <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" 
                         onclick="btnSubmit_Click" />
                 
                 </td>
                  
            </tr><tr>
                 <td colspan="2">
                 
                 
                 </td>
            </tr>
                     </table>
                </td>
            
            </tr>

            <tr>
                <td align="left">
                     <table width="100%">
                     <tr>
                <td class="pageheader" colspan="2">
                    Call Diary List&nbsp;
                </td>
            </tr>

            <tr>
                            <td>
                                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                                Search Criteria</Header>
                                            <Content>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lblFrom" runat="server" Text="From Date"></asp:Label>
                                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"  Width="10%"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Image ID="imgDay" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDay"
                                                                PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                            </cc1:CalendarExtender>
                                                            &nbsp;

                                                            <asp:Label ID="lblTo" runat="server" Text="To Date"></asp:Label>
                                                            &nbsp;<asp:TextBox ID="txtTodate" runat="server" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"  Width="10%"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                                                                PopupPosition="BottomLeft" TargetControlID="txtTodate">
                                                            </cc1:CalendarExtender>
                                                            &nbsp;
                                                            &nbsp;
                                                            Seeking&nbsp;
                                                            <asp:DropDownList ID="ddlSeaEmp" runat="server"></asp:DropDownList> <br />
                                                            
                                                            
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                         Caller&nbsp;
                                                            <asp:TextBox ID="txtSeaSeeking" runat="server"></asp:TextBox>
                                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="true"
                                                            MinimumPrefixLength="1" ServiceMethod="GetVistList" ServicePath="" TargetControlID="txtSeaSeeking"
                                                            UseContextKey="true" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>&nbsp;

                                                            Company&nbsp;
                                                            <asp:TextBox ID="txtSeaCompnay" runat="server"></asp:TextBox>
                                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="true"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSeaCompnay"
                                                            UseContextKey="true" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>

                                                         &nbsp;
                                                           
                                                            <asp:Button ID="btnDaySearch" ToolTip="Search" runat="server" CssClass="savebutton" OnClick="btnDaySearch_Click"
                                                                 Text="Search" />
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
            <td  colspan="2" align="left">
                 
                 <asp:GridView ID="grdCallDiary" runat="server" AutoGenerateColumns="false" 
                         EmptyDataText="No Records Found" 
                     onrowcommand="grdCallDiary_RowCommand" CssClass="gridview"  Width="100%">
                    <Columns>
                            <asp:BoundField  DataField="Caller" HeaderText="Caller" />
                            <asp:BoundField DataField="Company" HeaderText="Company" />
                            <asp:BoundField DataField="Name" HeaderText="Seeking" />
                            <asp:BoundField DataField="Message" HeaderText="Message" />
                            <asp:TemplateField>
                                   <ItemTemplate>
                                         <asp:LinkButton ID="lnkEdt" runat="server" Text="Edit" CommandArgument='<%#Eval("DiaryID")%>' CommandName="Edt"></asp:LinkButton>
                                   </ItemTemplate>
                            </asp:TemplateField>
                    
                    </Columns>
                 
                 </asp:GridView>
                 
                 </td>
            </tr>
       
                 
           

             <tr>
                        <td style="height: 17px">
                            <uc1:paging ID="EmpWorkPaging" runat="server" />
                        </td>
                    </tr>
                     
                     </table>
                </td>
            
            </tr>


            
    </table>

</asp:Content>

