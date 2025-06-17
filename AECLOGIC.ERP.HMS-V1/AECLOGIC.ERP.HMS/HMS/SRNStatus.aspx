<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="SRNStatus.aspx.cs" 
    Inherits="AECLOGIC.ERP.HMS.SRNStatus" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ OutputCache Location="None" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function ClosePo(PONo, ItemId) {
            var answer = window.confirm("Total Indents Qty is Recieved.So PO# " + PONo + " will be Closed. Do you wish to Continue?");
            if (answer)
                SRNStatus.MMS_CLOSEPO(PONo, ItemId);
            window.location = "SRNStatus.aspx?ID=3";
        }           
      
        function CancelAsyncPostBack() {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //alert(prm);
            if (prm.get_isInAsyncPostBack()) {
                prm.abortPostBack();
            }
        }
         function SelectAll(hChkBox, grid, tCtrl) 
                      {
                       var oGrid = document.getElementById(grid);
                        var IPs = oGrid.getElementsByTagName("input");
                         for (var iCount = 0; iCount < IPs.length; ++iCount)
                          {
                           if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked;
                            }
                             }

   
        function Validate(Id) {
            var Obj = getObj('<%=gvRecieve.ClientID%>');
            if (Id == "1") {
                for (i = 1; i < Obj.rows.length; i++) {
                    for (j = 0; j < Obj.rows[i].cells[4].childNodes.length; j++) {
                        if (Obj.rows[i].cells[4].childNodes[j].value == "") {
                            alert("Enter Received Qty");
                            Obj.rows[i].cells[4].childNodes[j].focus(); return false;
                        }

                    }
                }
                return true;
            }
            else {
                for (i = 1; i < Obj.rows.length; i++) {
                    for (j = 0; j < Obj.rows[i].cells[5].childNodes.length; j++) {
                        if (Obj.rows[i].cells[5].childNodes[j].value == "") {
                            alert("Enter Approved Qty");
                            Obj.rows[i].cells[5].childNodes[j].focus(); return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }


        function ChkSRN(SRNID) {
            var Res = SRNStatus.SRNProofs(SRNID);
            if (Res.value != "") {
                if (Res.value == 1) {
                    var Res1 = SRNStatus.SRNItems(SRNID);
                    if (Res1.value <= 0) {
                        alert('No Goods Found in SRN ' + SRNID);
                    } else
                        window.location = "SRNPreReq.aspx?ID=0&SRNID=" + SRNID;
                }
                else {
                    var answer = confirm("Click OK to upload Invoice/Proof OR Cancel to skip uploading.")
                    if (answer)
                        window.location = "SrnItemsProof.aspx?ID=0&SRNID=" + SRNID;
                    else
                        window.location = "SRNPreReq.aspx?ID=0&SRNID=" + SRNID;
                }
            }
        }

        function ChkProof(SRNID) {
            var Res = SRNStatus.SRNProofs(SRNID);
          
                    var answer = confirm("Click OK to upload Invoice/Proof OR Cancel to skip uploading.")
                    if (answer)
                        window.location = "SrnItemsProof.aspx?ID=0&SRNID=" + SRNID;
                    else
                        return false;
                    
                }
          
            
            
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlWorkSites_hid.ClientID %>').value = HdnKey;
        }


        
    </script>

    <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">
                
                <tr Width="100%">
                    <td >
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
                                                <td colspan="2">
                                                    WO No:&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                    <asp:TextBox ID="txtPONo" width="100px" runat="server" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                     PSDN/PSRN No:&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                    <asp:TextBox ID="txtSRNNo" width="90px" runat="server" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                             
                                                    Vendor:&nbsp;&nbsp;&nbsp;&nbsp;
                                                
                                                    <asp:DropDownList ID="ddlVendor" runat="server" DataValueField="ID" DataTextField="Name" TabIndex="2" AccessKey="v" ToolTip="[Alt+v OR Alt+v+Enter]" 
                                                        AppendDataBoundItems="true" Width="150" CssClass="droplist">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                        TargetControlID="ddlVendor" PromptText="Type to search" PromptCssClass="PromptText"
                                                        PromptPosition="Top" IsSorted="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                               
                                                   
                                       
                                                    Worksite:&nbsp;&nbsp;&nbsp;&nbsp;
                                                
                                              
                                                     <asp:HiddenField ID="ddlWorkSites_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="120px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>&nbsp;&nbsp;&nbsp;&nbsp;
                                               <tr>
                                                   <td colspan="2">
                                                    Dispatch From Date:&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                    <asp:TextBox ID="txtGdnFromDate" runat="server" Width="60" TabIndex="5" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderIndDate" TargetControlID="txtGdnFromDate"
                                                        runat="server" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp;
                                              
                                                    Dispatch To Date:&nbsp;&nbsp;&nbsp;&nbsp;
                                                
                                                    <asp:TextBox ID="txtGdnToDate" runat="server"  Width="60" TabIndex="6" AccessKey="y" ToolTip="[Alt+y OR Alt+y+Enter]"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderCancelDate" TargetControlID="txtGdnToDate"
                                                        runat="server" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp;
                                              
                                                
                                                    <asp:Button Width="50" ID="btnSearch"  OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary" Text="Search" TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"/>
                                                    <asp:Button Width="50" ID="btClear" OnClick="btClear_Click" runat="server" CssClass="btn btn-danger" Text="Reset" TabIndex="8" AccessKey="b" ToolTip="[Alt+b OR Alt+b+Enter]"/>
                                                </td>
                                                   </td>
                                                </tr>
                                            </tr>
                                         
                                        </table>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <table  Width="100%">
                            <tr>
                                <td >
                                    <asp:GridView ID="gvSRN" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                                        CssClass="gridview" OnRowCommand="gvSRN_RowCommand" OnRowDataBound="gvSRN_RowDataBound" width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Check">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" Text="Check All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPrereq" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PSDN/PSRN">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="SrnName" Text='<% #Eval("ID") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="GdnDate" Text='<% #Eval("Startdate") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Services">
                                                <ItemTemplate>
                                                    <a id="lnkGoods" runat="server" width="50" style="cursor: pointer;" onclick='<% #GetGoodsItems(Eval("ID").ToString())%>'>
                                                        Services </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Origin" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Origin" Text='<% #Eval("Origin") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Destination" Text='<% #Eval("Destination") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Vehicle" Text='<% #Eval("Vehicle") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="vendor_name" Text='<% #Eval("vendor_name") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proof ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="TSNO" runat="server" Text='<% #Eval("TripSheet") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Process">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkID" runat="server" CssClass="anchor__grd vw_grd" Text="Receive" OnClientClick='<% #String.Format("javascript:return ChkSRN({0});",Eval("ID").ToString())%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server"  CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<% #Eval("ID")%>'
                                                        CommandName="Edt" visible='<%#IsEditble()%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<% #Eval("ID")%>'
                                                        CommandName="Del"  visible='<%#IsDelete()%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proof">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" OnClientClick='<% #String.Format("javascript:return ChkProof({0});",Eval("ID").ToString())%>'
                                                        runat="server" CssClass="anchor__grd edit_grd" Text="Proof" CausesValidation="false" 
                                                        Visible='<%#ViewVisible(DataBinder.Eval(Container.DataItem, "SRNItemID").ToString(),1)%>'/> 

                                                        &nbsp;

                                                         <asp:HyperLink ID="lnkImage" NavigateUrl="#" onclick='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>'
                                            runat="server" Visible='<%#ViewVisible(DataBinder.Eval(Container.DataItem, "SRNItemID").ToString(),2)%>'>View</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            There are Currently No Record(s) Found.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:Paging ID="TasksPaging" runat="server" CurrentPage="1" NoOfPages="1" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvRecieve" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                            HeaderStyle-CssClass="tableHead"   OnRowDataBound="gvRecieve_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="PSDNItem ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSRNItemID" Text='<% #Eval("SRNItemID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGoods" Text='<% #Eval("ResourceName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="UOM" Text='<% #Eval("AU_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PSDN Qty">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGDNQuantity" Width="60" Text='<% #Eval("ReqQty") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recvd Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArvdQty" runat="server" Text='<% #Eval("RcvdQty")  %>' Visible="false"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtArrivedQuantity" Width="90" Text='<% #Eval("RcvdQty") %>'></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apprd Qty">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRcvdQty" Width="90" Text='<% #Eval("AccptQty") %>'></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dist(Km)" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbldistance" Text='<% #Eval("Distance") %>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtKMs" runat="server" Text='<% #Eval("Distance") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" TextMode="MultiLine" Width="150" Height="60" ID="txtComments"></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received/Approved By">
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlCheckedInBy" DataValueField="ID" DataTextField="Name"
                                            CssClass="droplist" DataSource='<%# BindDropDownList() %>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                There are Currently No Record(s) Found.
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSelectSrn" runat="server" OnClick="btnSelectSrn_Click" Visible="false"
                            CssClass="savebutton btn btn-success" Width="80px" />
                        <asp:Button ID="btnSendBack" runat="server" Text="Send Back to Drafts" Visible="false"
                            OnClick="btnSendBack_Click" CssClass="savebutton btn btn-primary" Width="120px" 
                            AccessKey="b" TabIndex="9" ToolTip="[Alt+b OR Alt+b+Enter]" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_OnClick"
                            Visible="false" CssClass="savebutton" Width="91px" AccessKey="s" 
                            TabIndex="10" ToolTip="[Alt+s OR Alt+s+Enter]" />
                         <asp:Button ID="btnDelete" runat="server" Text="Delete All" Visible="false"
                            OnClick="btnDelete_Click" CssClass="savebutton btn btn-danger" Width="107px" 
                            TabIndex="11" />
                    </td>
                </tr>
            </table>
            <div class="UpdateProgressCSS">
                <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                    <ProgressTemplate>
                        <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                            <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                <img src="IMAGES/Loading.gif" alt="update is in progress" />
                                <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" TabIndex="12" /></asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
