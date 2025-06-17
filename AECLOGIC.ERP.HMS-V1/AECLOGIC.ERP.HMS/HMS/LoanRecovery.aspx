<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanRecovery.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LoanRecovery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
      <script language="javascript" type="text/javascript">

          function GetID(source, eventArgs) {
              var HdnKey = eventArgs.get_value();
              document.getElementById('<%=empid_hd.ClientID %>').value = HdnKey;
          }

          function SelectAll(hChkBox, grid, tCtrl) {
              var oGrid = document.getElementById(grid);
              var IPs = oGrid.getElementsByTagName("input");
              for (var iCount = 0; iCount < IPs.length; ++iCount) {
                  if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked;
              }
          }

          </script>

  
     <table id="tblProcess" runat="server" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="gvViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="106px" Width="70%">
                    <Panes>
                        <cc1:AccordionPane ID="gvViewAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td >
                                           <b>
                                            Employee Name:</b> 
                                            <asp:HiddenField ID="empid_hd" runat="server" />
                                            <asp:TextBox ID="txtempid" Width="150" runat="server"></asp:TextBox>
                                            
                                    <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="true"
                                            MinimumPrefixLength="1" ServiceMethod="GetEmpidList" ServicePath="" TargetControlID="txtEmpid" UseContextKey="true" OnClientItemSelected="GetID"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" 
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true" 
                                            FirstRowSelected="True"></cc1:AutoCompleteExtender>
                                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtempid"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]">
                                           </cc1:TextBoxWatermarkExtender>

                                               &nbsp;                                             
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
                                        &nbsp;&nbsp;
                                            Year:
                                          &nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlyear" runat="server"  CssClass="droplist" Width="100"/> 



                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                 <asp:GridView ID="gvVeiw" runat="server" AutoGenerateColumns="false"  EmptyDataText="No Records Found"
                     CssClass="gridview" Width="100%" OnRowDataBound="gvVeiw_RowDataBound">
                    <Columns>
                          <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkToTransfer" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblrowid" Text='<%#Eval("rowid")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Empid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpID" Text='<%#Eval("Empid")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            
                       <asp:BoundField DataField="Name" HeaderText="EmpName" />

                        <asp:TemplateField HeaderText="AdjDays">
                            <ItemTemplate>
                                <asp:Label ID="lbladjdays" Text='<%#Eval("AdjDays")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="AdjAmt">
                            <ItemTemplate>
                                <asp:Label ID="lblAdjAmt" Text='<%#Eval("AdjAmt")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                         <asp:TemplateField HeaderText="Monthid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblmonth" Text='<%#Eval("monthid")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:BoundField HeaderText="Month" DataField="MonthName" />

                          <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                                <asp:Label ID="lblyear" Text='<%#Eval("Year")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        
                        </Columns>
                     </asp:GridView>
                </td>
              </tr>
             <tr>
                <td style="height: 17px">
                    <uc1:Paging ID="EmpReimbursementAprovedPaging" runat="server" />
                </td>
            </tr>
         <tr>
             <td>
                 <asp:Button ID="btnrecmnd" Text="Recommend" runat="server" CssClass="btn btn-primary" OnClick="btnrecmnd_Click" />
             </td>
         </tr>
         </table>
    </asp:Content>




