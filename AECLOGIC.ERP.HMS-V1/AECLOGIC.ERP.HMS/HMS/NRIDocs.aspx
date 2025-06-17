<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="NRIDocs.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMSV1.NRIDocsV1" EnableEventValidation="false" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script>  function GetEmpid(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSEmp .ClientID %>').value = HdnKey; }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
            <script language="javascript" type="text/javascript">
                function SetTarget() {
                    document.forms[0].target = "_blank";
                }
                function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("Do you want to Close Work Order?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            </script>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td colspan="2" class="pageheader">Documents
                         <asp:LinkButton ID="lnkExpandControlID" Style="vertical-align: middle; border: 0px;"
                             runat="server">
                             <asp:Image ID="imgImageControlID" ImageAlign="AbsBottom" runat="server"></asp:Image>
                             <asp:Label ID="lblTextLabelID" runat="server"></asp:Label>
                         </asp:LinkButton>
                        <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" SuppressPostBack="true" TargetControlID="pnlTaskDetails"
                            CollapsedSize="0" ExpandedSize="450" Collapsed="True" ExpandControlID="lnkExpandControlID"
                            CollapseControlID="lnkExpandControlID" AutoCollapse="False" AutoExpand="False"
                            ScrollContents="True" TextLabelID="lblTextLabelID" CollapsedText="Process..."
                            ExpandedText=" Hide Details" ImageControlID="imgImageControlID" ExpandedImage="~/Images/dashminus.gif"
                            CollapsedImage="~/Images/dashplus.gif" ExpandDirection="Vertical" />
                        <asp:Panel ID="pnlTaskDetails" runat="server" CssClass="box box-primary">
                            <table>
                                <tr>
                                    <td>Step1: If Service Resource Item NOT found in the Dropdown, Click '<<'Add New'>>', Click Go, Select Group, Select Resource Service, 
                                    Click Add/Configure to bring the New Item to the first Dropdown. 
                                <br />
                                        Step2:  PMS >> Rush Order >> Select Work Order >> Select Parent Accounts Services Group >> 
                                    Select Services Items /Resources >> Follow the regular Work order process 
                                <br />
                                        Step3 : HMS >> Services menu group >> Click SDN >> Select the Work Order so created in the above step and 
                                    perform usual SDN process >> Click Process >> Receive >> Upload Proof of the document if any>> Complete the process
                                <br />
                                        <img src="../Images/Help_SatuatoryItems.png" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:RadioButtonList ID="rbTaxasion" Font-Bold="true" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rbTaxasion_SelectedIndexChanged">
                            <asp:ListItem Text="In-Process" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Post-Process" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table id="tblUnRecon" runat="server" width="100%">
                <tr>
                    <td colspan="2" class="savebutton">Process Items
                    </td>
                </tr>
                <tr>
                    <td>Document Type<b>:</b><asp:DropDownList ID="ddlItems" Width="250" runat="server" CssClass="droplist">
                    </asp:DropDownList>
                        &nbsp;
                Work Order No<b>:</b><asp:TextBox ID="txtWO" runat="server" Height="21px" Width="40px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" Font-Bold="True" CssClass="btn btn-success" />
                        &nbsp;
                        <asp:Label ID="lblGroup" Font-Bold="true" runat="server" Text="Group"></asp:Label>
                        &nbsp;<asp:DropDownList ID="ddlGroup" AutoPostBack="true" runat="server" CssClass="droplist"
                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Label ID="lblNewItems" runat="server" Font-Bold="true" Text="Items"></asp:Label>
                        &nbsp;<asp:DropDownList ID="ddlNewItems" runat="server" CssClass="droplist">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btnAddNew" runat="server" Text="Configure" OnClick="btnAddNew_Click" Font-Bold="True"
                            CssClass="btn btn-primary" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:GridView ID="gvUnReconciled" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="tableHead"
                            EmptyDataText="No Records found!" OnRowCommand="gvUnReconciled_RowCommand" Width="70%">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNItemID" runat="server" Text='<%#Eval("SRNItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNID" runat="server" Text='<%#Eval("SRNID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResourceID" runat="server" Text='<%#Eval("ResourceID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPODetID" runat="server" Text='<%#Eval("PodetID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnReconse" CommandName="Reconse" CssClass="btn btn-primary" CommandArgument='<%#Eval("SRNItemID")%>'
                                            Text="Resolve" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO NO" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                            CommandName="View" Text='<%#Eval("PONO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PONAME" HeaderText="WO-NAME" />
                                <asp:BoundField Visible="false" DataField="InvoiceImg" HeaderText="InvoiceImg" />
                                <asp:BoundField DataField="WorkSite" HeaderText="WorkSite" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" />
                                <asp:BoundField Visible="false" DataField="Remarks" HeaderText="Remarks" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkImage" NavigateUrl="#"
                                            onclick='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>'
                                            runat="server"
                                            Visible='<%#ViewVisible(DataBinder.Eval(Container.DataItem, "SRNItemID").ToString(),2)%>'>Image</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill TransID" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillTransID" runat="server" Text='<%#Eval("BillTransID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post To PrePaid Expenses" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCostToPrePaidExp" CommandName="PrePaidExpenses" OnClientClick="return confirm('Are you sure to Post PrePaid Expenses!')"
                                            CssClass="btn btn-success" CommandArgument='<%#Eval("POID")%>'
                                            Visible='<%#ViewVisiblePrePaidExp(DataBinder.Eval(Container.DataItem,"BillTransID").ToString(),DataBinder.Eval(Container.DataItem,"DetailEntered").ToString(),DataBinder.Eval(Container.DataItem,"PrePaidExpAdjTransID").ToString()) %>'
                                            Text="PrePaid Expenses" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adj TransID" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrePaidExpAdjTransID" runat="server" Text='<%#Eval("PrePaidExpAdjTransID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="Paging1" Visible="false" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table id="tblEdit" runat="server" width="100%">
                        <tr id="trEdit" runat="server" visible="false">
                            <td align="left">
                                <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                    SelectedIndex="0">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                                                Search Criteria
                                                            </Header>
                                            <Content>
                                                <asp:UpdatePanel ID="updAttendance" runat="server">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr><td>
                                                                <asp:CheckBox ID="chkallEmp" runat="server" AutoPostBack="true" Text=" Show All Employees" OnCheckedChanged="chkallEmp_CheckedChanged" />
                                                                &nbsp;
                                                &nbsp;
                                                 <asp:CheckBox ID="chkhijri" runat="server" AutoPostBack="true" Text=" Hijri" OnCheckedChanged="chkhijri_CheckedChanged" />
                                                                &nbsp;
                                                    &nbsp; &nbsp; 
                                                <asp:Label ID="lblempid" runat="server" Text="Empid"></asp:Label>
                                                                <asp:TextBox ID="txtempid" runat="server" Height="21px" Width="220px"></asp:TextBox>
                                                                <asp:HiddenField ID="hdSEmp" runat="server" Value="0" />
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtempid"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    OnClientItemSelected="GetEmpid" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtempid"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Employee ID/NAME]"></cc1:TextBoxWatermarkExtender>
                                                                From Date :
                                                <asp:TextBox ID="txtFromDay" Width="90Px" placeholder="dd MMM yyyy" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtDayCalederExtender" runat="server" TargetControlID="txtFromDay" Format="dd MMM yyyy"
                                                                    PopupButtonID="txtDOB"></cc1:CalendarExtender>
                                                                To Date :
                                                <asp:TextBox ID="txtToDay" Width="90Px" placeholder="dd MMM yyyy" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtToDayCalederExtender" runat="server" TargetControlID="txtToDay" Format="dd MMM yyyy"
                                                                    PopupButtonID="txtDOB"></cc1:CalendarExtender>
                                                                Sponsor:
                                                <asp:TextBox ID="txtSponsor" Width="90Px" placeholder="Sponsor" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSearch" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                </cc1:Accordion>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProgessdetails" runat="server" Text="" Font-Bold="true"></asp:Label>
                                &nbsp;&nbsp; Bill TransID: <asp:Label ID="lblgBillTransID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvEdit" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="tableHead"
                                    EmptyDataText="No Records found!" OnRowCommand="gvEdit_RowCommand">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkToTransfer" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblESItemsID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                <%-- EmpStatututoryItems primary ID--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNItemID" runat="server" Text='<%#Eval("SRNItemID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNID" runat="server" Text='<%#Eval("SRNID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WO-NAME" Visible="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                                    CommandName="View" Text='<%#Eval("WOName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ResourceName" HeaderText="Doc-NAME" Visible="false" />
                                        <asp:BoundField Visible="false" DataField="InvoiceImg" HeaderText="InvoiceImg" />
                                        <asp:BoundField Visible="false" DataField="ApprovedBy" HeaderText="ApprovedBy" />
                                        <asp:BoundField Visible="false" DataField="CreatedOn" HeaderText="CreatedOn" />
                                        <asp:TemplateField HeaderText="Employee" HeaderStyle-Width="185px" ItemStyle-Width="180px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlMachinery" DataSource='<%#ViewState["BindEMPBYWO"]%>' AutoPostBack="true" OnSelectedIndexChanged="ddlMachinery_SelectedIndexChanged"
                                                    DataTextField="Name" DataValueField="EmpId" runat="server" Style="width: 180px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From [dd/mm/yyyy]" HeaderStyle-Width="110px" ItemStyle-Width="105px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtVFrom"  Text='<%#DataBinder.Eval(Container.DataItem,"From").ToString() == "" ? DataBinder.Eval(Container.DataItem,"From").ToString() : GetHijriDate(DataBinder.Eval(Container.DataItem,"From").ToString(),DataBinder.Eval(Container.DataItem,"DFrom").ToString()) %>' runat="server" Width="105px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtVFromCal" runat="server" PopupButtonID="txtVFrom"
                                            TargetControlID="txtVFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To [dd/mm/yyyy]" HeaderStyle-Width="110px" ItemStyle-Width="105px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtVTo" Text='<%#DataBinder.Eval(Container.DataItem, "To").ToString() == "" ? DataBinder.Eval(Container.DataItem, "To").ToString() : GetHijriDate(DataBinder.Eval(Container.DataItem, "To").ToString(),DataBinder.Eval(Container.DataItem, "DTo").ToString()) %>' runat="server" Width="105px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtVToCal" runat="server" PopupButtonID="txtVTo"
                                            TargetControlID="txtVTo" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id Number" HeaderStyle-Width="110px" ItemStyle-Width="105px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNumber" Style="width: 105px;" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sponsorship" HeaderStyle-Width="110px" ItemStyle-Width="105px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAltNumber" Style="width: 105px;" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue City" HeaderStyle-Width="90px" ItemStyle-Width="85px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCity" runat="server" Style="width: 85px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issuer" HeaderStyle-Width="45px" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtIssuer" Style="width: 80px;" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="105px" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" Style="width: 100px;" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proof" HeaderStyle-Width="70px" ItemStyle-Width="70px" Visible="false">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="ImgUpload" Style="width: 60px;" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExt" runat="server" Text='<%#Eval("Ext")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Save">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" ToolTip="Save" Text="..." CssClass="btn btn-success" CommandName="Edt" CommandArgument='<%#Eval("SRNItemID")%>'
                                                    runat="server">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Save All"
                                    OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnProcess" runat="server" CssClass="btn btn-danger" Text="Close WO" OnClientClick="Confirm()"
                                    OnClick="btnProcess_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table id="tblReconciled" runat="server" width="100%">
                <tr>
                    <td colspan="2" class="savebutton"></td>
                </tr>
                <tr id="trReconciled" runat="server">
                    <td>
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                        Search Criteria</Header>
                                   
                                    <Content>
                                         <asp:UpdatePanel ID="upPostProcess" runat="server">
                                          <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>Worksite</b>&nbsp;<asp:DropDownList ID="ddlRecWs" runat="server" AutoPostBack="false"
                                                    CssClass="droplist" Width="150px">
                                                </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender4" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlRecWs">
                                                    </cc1:ListSearchExtender>
                                                    &nbsp;
                                                                                   <strong>Department</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddlRecDept" runat="server"
                                                                                       AutoPostBack="false" CssClass="droplist">
                                                                                   </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender5" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlRecDept">
                                                    </cc1:ListSearchExtender>
                                                    &nbsp;
                                                                                     <b>
                                                                                         <asp:Label ID="Label1" runat="server" Text="Designation"></asp:Label>:</b>
                                                    <asp:DropDownList ID="ddlRecDesg" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender6" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlRecDesg">
                                                    </cc1:ListSearchExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Document Type:</b>&nbsp;<asp:DropDownList ID="ddlRecItems" AutoPostBack="false" CssClass="droplist"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    <b>&nbsp; Employee:</b><asp:DropDownList ID="ddlSearcMech" Width="300" CssClass="droplist"
                                                        AutoPostBack="false" runat="server">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender7" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlSearcMech">
                                                    </cc1:ListSearchExtender>
                                                    &nbsp;
                                            <asp:CheckBox ID="chkhijriPost" runat="server" Text=" Hijri" AutoPostBack="true" OnCheckedChanged="chkhijriPost_CheckedChanged" />
                                                    &nbsp;
                                            Sponsor: 
                                            <asp:TextBox ID="txtPSponsor" runat="server"></asp:TextBox>
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search"
                                                        OnClick="btnRecSearch_Click" />
                                                     Export to Excel: <asp:ImageButton ID="btnExpactExporttoExcel" ToolTip="Export to Excel Report" runat="server" ImageUrl="../Images/ExportToExcel.bmp"
                                                            CssClass="savebutton" Width="30px" Height="30px" Text="Export to Excel" OnClick="btnExpactExporttoExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                          </ContentTemplate>
                                         <Triggers>
                                            <asp:PostBackTrigger ControlID="btnExpactExporttoExcel" />
                                        </Triggers>
                                        </asp:UpdatePanel>
                                        </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvRecosiled" EmptyDataText="No Records found!" AutoGenerateColumns="false"
                            CssClass="gridview" runat="server" OnRowCommand="gvRecosiled_RowCommand" OnRowDataBound="gvRecosiled_RowDataBound" Width="100%">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNItemID" runat="server" Text='<%#Eval("SRNItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNID" runat="server" Text='<%#Eval("SRNID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPODetID" runat="server" Text='<%#Eval("PodetID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" DataField="InvoiceImg" HeaderText="InvoiceImg" />
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpName" HeaderText="Emp Name" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="DOB" HeaderText="DOB" />
                                <asp:BoundField DataField="Age" HeaderText="Age" />
                                <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# GetDepartment(DataBinder.Eval(Container.DataItem, "DeptNo").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="Profession" HeaderText="Profession" Visible="true" />
                                <asp:BoundField DataField="From" HeaderText="Valid From[dd/mm/yyyy]" HeaderStyle-Width="100px" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="To" HeaderText="Valid Upto[dd/mm/yyyy]" HeaderStyle-Width="100px" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="Numeber" HeaderText="Id Number" />
                                <asp:BoundField DataField="AltNumber" HeaderText="Sponsorship" />
                                <asp:BoundField DataField="IssuePlace" HeaderText="Issue City" />
                                <asp:BoundField DataField="Issuer" HeaderText="Issuer" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" Visible="false" />
                                <asp:BoundField ItemStyle-ForeColor="Red" DataField="Status" HeaderText="Status" />
                                <asp:BoundField Visible="false" DataField="InvoiceImg" />
                                <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                            CommandName="View" Text='<%#Eval("PONO") %>' />
                                        <br />
                                        <asp:Label ID="lblTransID" runat="server" Text='<%#Eval("TransID")%>' Visible="false"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("PONAME")%>' Visible="false"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("ApprovedBy")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkImage" PostBackUrl='<%#ViewImage(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "EmpDocID").ToString())%>'
                                            OnClick='<%#ViewImage(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "EmpDocID").ToString())%>'
                                            runat="server">Image</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="anchor__grd vw_grd" OnClientClick="SetTarget();" CommandName="View">View</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 17px">
                        <uc1:Paging ID="PageTax" Visible="true" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblFinalEdit" runat="server" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvFinalEdit" Width="100%" AutoGenerateColumns="false" CssClass="gridview"
                            runat="server" EmptyDataText="No Records found!" OnRowCommand="gvFinalEdit_RowCommand" OnRowDataBound="gvFinalEdit_RowDataBound">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNItemID" runat="server" Text='<%#Eval("SRNItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNID" runat="server" Text='<%#Eval("SRNID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO NO">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                            CommandName="View" Text='<%#Eval("PONO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PONAME" HeaderText="WO-NAME" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkImage" PostBackUrl='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>'
                                            OnClick='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>'
                                            runat="server">Image</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" />
                                <asp:TemplateField HeaderText="Employee">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlMachinery" CssClass="droplist" SelectedIndex='<%# GetMechIndex(Eval("EmpID").ToString())%>'
                                            DataSource='<%#ViewState["Machinery"]%>' DataTextField="Name" DataValueField="EmpId"
                                            runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valid From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtVFrom" Text='<%#Bind("[From]")%>' Width="80px" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtVFrom"
                                            TargetControlID="txtVFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upto">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtVTo" Text='<%#Bind("[To]")%>' Width="80px" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtVTo"
                                            TargetControlID="txtVTo" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTempTO" Text='<%#Bind("To")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTempFrom" Text='<%#Bind("From")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNumber" Text='<%#Bind("Numeber")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sponsorship">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAltNumber" Text='<%#Bind("AltNumber")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Place">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIssuePlace" Text='<%#Bind("IssuePlace")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issuer">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIssuer" Text='<%#Bind("Issuer")%>' Width="120px" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proof">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="ImgUpload" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" Text='<%#Bind("Remarks")%>' runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblImgExta" runat="server" Text='<%#Bind("Ext")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CommandName="Edt" CssClasss="anchor__grd edit_grd " CommandArgument='<%#Eval("SRNItemID")%>'
                                            runat="server">Update</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
