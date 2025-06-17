<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="EmployeeLog.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeLog" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <table width="100%" align="left">
    <tr>
                <td class="pageheader" colspan="2">
                    Emploee Log&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" >
                    <table width="50%">
                     
                                 <tr>
                 <td>
                 Log Date
                 
                 </td>
                  <td>
                  <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                PopupButtonID="txtDOB" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                 
                 </td>
            </tr>
            <tr>
                 <td>
                     Emp Name
                 
                 </td>
                  <td>
                 <asp:DropDownList ID="ddlEmp" runat="server"></asp:DropDownList>
                 
                 </td>
            </tr>
   <tr>
            <td>
                First Occurrence</td>
            <td>
            <asp:TextBox ID="txtIntime" runat="server" ></asp:TextBox>
            </td>
           
        </tr>
        
        <tr>
                 <td>
                 
                 Target Company
                 </td>
                  <td>
                      <asp:TextBox ID="txtCompName" runat="server" Height="23px" Width="137px" ></asp:TextBox>
                 
                
                 </td>
            </tr>
            
            <tr>
                 <td>
                 
                 Worksite
                 </td>
                  <td>
                      <asp:DropDownList ID="ddlWorksite" runat="server" 
                                                                             CssClass="droplist"
                                                                            AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"></asp:DropDownList>
                 </td>
            </tr>
            

            <tr>
                 <td valign="top">
                 
                 Purpose
                 </td>
                  <td  valign="top">
                      <asp:TextBox ID="txtPurpose" runat="server" Height="51px" Width="243px" ></asp:TextBox>
                 
                 </td>
            </tr>


           

            <tr id="trsoccurance" runat="server" visible="false">
                 <td>
                 
                 Second Occurrence
                 </td>
                  <td>
            <asp:TextBox ID="txtOutTime" runat="server"></asp:TextBox>

                     
                 
                 </td>
            </tr>

            <tr id="trremarks" runat="server" visible="false">
                 <td valign="top">
                 
                 Remarks
                 </td>
                  <td  valign="top">
                      <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="73px" 
                          Width="243px" ></asp:TextBox>
                 
                 </td>
            </tr>
            
            
            <tr>
                 <td>
                  <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" onclick="btnSubmit_Click" 
                          />
                 
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
                    Employee Log List&nbsp;
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
                                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]" Width="10%"></asp:TextBox>
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
                                                            
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       <td>
                                                      
                                                            Employee&nbsp;
                                                            <asp:DropDownList ID="ddlSeaEmp" runat="server"></asp:DropDownList>
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
                 
                 <asp:GridView ID="grdVisitorsLog" runat="server" AutoGenerateColumns="false" 
                         EmptyDataText="No Records Found" 
                     CssClass="gridview" onrowcommand="grdVisitorsLog_RowCommand" Width="100%" 
                     onrowdatabound="grdVisitorsLog_RowDataBound">
                    <Columns>
                            <asp:BoundField  DataField="Name" HeaderText="EmpName" />
                            <asp:BoundField  DataField="LogDate" HeaderText="LogDate" />
                            <asp:BoundField DataField="TimeIN" HeaderText="First Occurrence" />
                            <asp:TemplateField HeaderText="Second Occurrence">
                                  <ItemTemplate >
                                      <asp:TextBox ID="txtOutTime" runat="server" Text='<%#Eval("TimeOut")%>' Width="70%"></asp:TextBox>
                                  </ItemTemplate>
                            
                            </asp:TemplateField>

                            
                            <asp:BoundField DataField="ScheDuration" HeaderText="Duration" />
                            <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                            <asp:BoundField DataField="Worksite" HeaderText="Worksite" />
                            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />

                             <asp:TemplateField HeaderText="Remarks">
                                  <ItemTemplate >
                                      <asp:TextBox ID="txtRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:TextBox>
                                  </ItemTemplate>
                            
                            </asp:TemplateField>

                            <asp:TemplateField>
                                   <ItemTemplate>
                                         <asp:LinkButton ID="lnkSave" runat="server" Text="Save" CommandArgument='<%#Eval("ElogID")%>' CommandName="Save"></asp:LinkButton>
                                   </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                   <ItemTemplate>
                                         <asp:LinkButton ID="lnkEdt" runat="server" Text="Edit" CommandArgument='<%#Eval("ElogID")%>' CommandName="Edt"></asp:LinkButton>
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

