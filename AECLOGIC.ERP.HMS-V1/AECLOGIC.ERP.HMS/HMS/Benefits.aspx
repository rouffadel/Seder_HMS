<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Benefits.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.BenefitsV1" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) {
            var oGrid = document.getElementById(grid);
            var IPs = oGrid.getElementsByTagName("input");
            for (var iCount = 0; iCount < IPs.length; ++iCount) {
                if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked;
            }
        }
        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
        }
        function GetEmpID_Acpost(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=HiddenField1.ClientID %>').value = HdnKey;
          }
    </script>
    <asp:UpdatePanel ID="salariesupdpanel" runat="server">
        <ContentTemplate>
            <table id="tblAdd" runat="server" width="70%">
                <tr>
                    <td style="width:150px;height:26px;">
                        <b>WorkSite:</b>
                        <asp:DropDownList ID="ddlWorksite" runat="server" CssClass="droplist" Visible="false"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                        </asp:DropDownList>
                       
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtSearchWorksite"
                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                        </td>
                    <td>
                         <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                    </td>
                    </tr>
                <tr>
                      <td style="width:150px;height:26px;">    
                        <b>Department:</b><asp:DropDownList ID="ddlDepartment" Visible="false" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="droplist" TabIndex="2" AccessKey="1" ToolTip="[Alt+1]">
                        </asp:DropDownList>
                       
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtdept"
                            WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                        </td>
                    <td>
                         <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                    </td>
                         </tr>
                <tr>
                           <td style="width:150px;height:26px;">    <b>Employee:</b>
                               <asp:DropDownList ID="ddlEmp" Visible="false" runat="server" AutoPostBack="True"
                            CssClass="droplist" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                            TargetControlID="ddlEmp" />
                        
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmp"
                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtSearchEmp"
                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtFilter" runat="server" CssClass="droplist" Visible="false" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFilter"
                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Search]"></cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="txtSearch" runat="server" Visible="false" ToolTip="Use Filter for Instant Search"
                            CssClass="btn btn-primary" OnClick="txtSearch_Click" Text="Search" TabIndex="5" AccessKey="4" />
                               </td>
                           <td>
                               <asp:TextBox ID="txtSearchEmp" OnTextChanged="GetHeadEmp" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                           </td>
                       </tr> 
                <tr>
                            <td style="width:150px;height:26px;"> 
                            <b>Remarks:</b><span style="color: #ff0000">*</span>
                        
                    </td>
                            <td>
                                 <asp:TextBox runat="server" ID="txtRemarks" Text="" Width="180px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                </tr>
                <tr>
                    <td style="width:150px;height:26px;"> 
                             <b>Upload Proof:</b><span style="color: #ff0000">*</span>
                      
                        </td>
                    <td>
                       <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                    </td>
                        </tr>
                <tr>
                   <td style="width:150px;height:26px;"> 
                        <b>Reimburse Item:</b><span style="color: #ff0000">*</span>
                        
                       </td>
                    <td>
                        <asp:DropDownList ID="ddlItem" runat="server"></asp:DropDownList>
                    </td>
                        </tr>
                <tr> <td style="width:150px;height:26px;"> 
                <b>Benefit Amount:</b><span style="color: #ff0000">*</span>
                  </td>
                <td>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                              </td>     </tr>        
                <tr>
                   <td style="width:150px;height:26px;"> 
                                      <b>   Month: </b>     </td>
                       <td>
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

                </tr>
                <tr>
                                   <td style="width:150px;height:26px;"> 
                                      <b>          Year:</b></td>
                                         <td>
                                             <asp:DropDownList ID="ddlyear" runat="server" CssClass="droplist" Width="100" />
                                             </td>
                    </tr>
                <tr><td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success"
                            runat="server" Text="Add" OnClick="btnSave_Click" AccessKey="s" />
                    </td>
                       
                </tr>
            </table>
            <table id="tblView" runat="server" style="width: 100%;">
                <tr>
                    <td>
                        <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                                    Search Criteria</Header>
                                    <Content>
                                        Name:
                                <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                        <asp:TextBox ID="txtEmpName" Height="20px" runat="server" TabIndex="6" AccessKey="5"
                                            ToolTip="[Alt+5]">                                                              
                                        </asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                            TargetControlID="txtEmpName"></cc1:TextBoxWatermarkExtender>
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" />
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvBenefit" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvBenefit_RowCommand" CssClass="gridview"
                            OnRowDataBound="gvBenefit_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkToTransfer" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="Bid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBid" Text='<%#Eval("Bid")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="Empid">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" Text='<%#Eval("Empid")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="EmpName" DataField="Empname" />
                                <asp:TemplateField HeaderText="Benefit Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmt" Text='<%#Eval("BenfitAmt")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Month" DataField="month" />
                                <asp:BoundField HeaderText="Year" DataField="year" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" Text='<%#Eval("status")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" Text='<%#Eval("Remarks")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkApprove" CommandName="Approve" CssClass="btn btn-primary" CommandArgument='<%#Eval("Bid")%>'
                                            runat="server">Approve</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" CommandName="Reject" CssClass="btn btn-danger" CommandArgument='<%#Eval("Bid")%>'
                                            runat="server">Reject</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdt" CommandName="Edt" CssClass="btn btn-warning" CommandArgument='<%#Eval("Bid")%>'
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdlt" CommandName="Del" CssClass="btn btn-danger" CommandArgument='<%#Eval("Bid")%>'
                                            runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proof" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a id="A1" target="_blank" href='<%# DocNavigateUrlnew(DataBinder.Eval(Container.DataItem, "Proof").ToString(),DataBinder.Eval(Container.DataItem, "Bid").ToString()) %>'
                                    runat="server" visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Proof").ToString().ToString()) %>' class="anchor__grd vw_grd">View</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpListPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5Px">
                        <asp:Button ID="btnTransferAcc" CssClass="btn btn-primary" ToolTip="Transfer To Accounts Dept."
                            runat="server" Text="A/C Posting" OnClick="btnTransferAcc_Click" Visible="false" />
                    </td>
                </tr>
            </table>
            <table id="tbledt" runat="server" visible="false">
                <tr>
                    <td>
                        <b>Month:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEdtMonth" runat="server" Width="100" CssClass="droplist">
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
                </tr>
                <tr>
                    <td>
                        <b>Year:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlEdtYear" runat="server" CssClass="droplist" Width="100" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Benefit Amount:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEdtAmt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnupdate_Click" />
                    </td>
                </tr>
            </table>
            <table id="tblacposted" runat="server" visible="false" width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                                    Search Criteria</Header>
                                    <Content>
                                        Name:
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:TextBox ID="TextBox1" Height="20px" runat="server" TabIndex="6" AccessKey="5"
                                            ToolTip="[Alt+5]">                                                              
                                        </asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="TextBox1"
                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID_Acpost">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="[Filter Name]"
                                            TargetControlID="TextBox1"></cc1:TextBoxWatermarkExtender>
                                        <asp:Button ID="btnsearchacposted" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearchacposted_Click" />
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvTransfered" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records Found" Width="100%" CssClass="gridview">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                <asp:BoundField HeaderText="Month" DataField="month" />
                                <asp:BoundField HeaderText="Year" DataField="year" />
                                <asp:BoundField HeaderText="TransID" DataField="TransID" />
                                <asp:BoundField HeaderText="Trans Date" DataField="TransDate" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementTransferdPaging" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="salariesupdpanel">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
