<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyWorkingDaysAndPayableDays.aspx.cs" 
     Inherits="AECLOGIC.ERP.HMS.MonthlyWorkingDaysAndPayableDays" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    
    <div id="dvView" runat="server">                                
        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">                                             
                                                <tr>
                                                <td>
                                                      Work Site:
                                                                       &nbsp;&nbsp;                                                           
                                                                <asp:TextBox ID="txtSearchWorksite" Height="22px" Width="140px"
                                                                     runat="server" AutoPostBack="True" >                                                                   
                                                                    </asp:TextBox> <%--onkeypress="return EnterEvent(event)"--%>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Department:
                                                                 <asp:TextBox ID="txtSearchdept"  Height="22px" Width="140px" 
                                                                     runat="server" AutoPostBack="True" >                                                                   
                                                                 </asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Dept Name]">
                                                                </cc1:TextBoxWatermarkExtender> 
                                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                            Emp Name:
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
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Emp Name]">
                                           </cc1:TextBoxWatermarkExtender>                                      
                                                           
                                              &nbsp;&nbsp;&nbsp;&nbsp;                                             
                                           Month:                                          
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
                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                            Year:
                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlyear" runat="server"  CssClass="droplist" Width="100"/> 
                                                                                    
                                                  &nbsp;&nbsp;&nbsp;                   <br />
                                                    <asp:Button ID="btnsearch" Width="100" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnsearch_Click"/>
                                                    <asp:Button ID="btnSynch" Width="100" runat="server" CssClass="btn btn-primary" Text="Sync" OnClick="btnSynch_Click"/>
                                            </td>
                                           </tr>
                                     </table>                                                                  
                               </Content>
                           </cc1:AccordionPane>
                       </Panes>
           </cc1:Accordion>        
     <%--<div>--%>
        <table width=100%>
           <tr>
               <td>
                   <asp:GridView ID="gvmnwrkngdays" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%" >
                       <Columns>
                           <asp:TemplateField  HeaderText="Emp Name">
                               <ItemTemplate>
                                   <asp:Label ID="lblemp" runat="server" Text='<%# Eval("EmpID") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>


                            <asp:TemplateField HeaderText="Pay StartDate" >
                               <ItemTemplate>
                                   <asp:Label ID="lblmnth" runat="server" Text='<%# Eval("Paystdate") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:TemplateField  HeaderText="Pay EndDate">
                               <ItemTemplate>
                                  <asp:Label ID="lblyr" runat="server" Text='<%# Eval("PayEnddate") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>


                           <asp:TemplateField HeaderText="Month Days">
                               <ItemTemplate>
                                   <asp:Label ID="txtpay" runat="server" Text='<%# Eval("TotalDays") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payable Days">
                               <ItemTemplate>
                                   <asp:Label ID="txttotal" runat="server" Text='<%# Eval("PayableDays") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                            </Columns>                      
                   </asp:GridView>
               </td>
           </tr>
           <tr>
               <td>
                    <uc1:paging ID="taskpaging" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
               </td>
           </tr>
        </table>
         </div>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...</ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    </asp:Content>
