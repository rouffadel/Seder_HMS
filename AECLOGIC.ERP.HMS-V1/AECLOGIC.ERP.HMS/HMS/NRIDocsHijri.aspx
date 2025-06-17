<%@ Page Title="View In Hijri" Language="C#" MasterPageFile="~/Templates/CommonMaster.master"   AutoEventWireup="True" Inherits="AECLOGIC.ERP.HMS.NRIDocsHijri" Codebehind="NRIDocsHijri.aspx.cs" %>

<%@ Register   Src="~/HMS/HijriGregDatePicker.ascx" TagPrefix="huc1" TagName="HijriGregDatePicker" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td colspan="2">
                <AEC:Topmenu ID="topmenu" runat="server" />

            </td>
        </tr>
        <tr>
             <td colspan="2" class="pageheader">
                        Documents
                         <asp:LinkButton ID="lnkExpandControlID" Style="vertical-align: middle; border: 0px;"
                    runat="server">
                    <asp:Image ID="imgImageControlID" ImageAlign="AbsBottom" runat="server"></asp:Image>
                    <asp:Label ID="lblTextLabelID" runat="server"></asp:Label></asp:LinkButton>
                          <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" SuppressPostBack="true" TargetControlID="pnlTaskDetails"
                    CollapsedSize="0" ExpandedSize="90" Collapsed="True" ExpandControlID="lnkExpandControlID"
                    CollapseControlID="lnkExpandControlID" AutoCollapse="False" AutoExpand="False"
                    ScrollContents="True" TextLabelID="lblTextLabelID" CollapsedText="Process..."
                    ExpandedText=" Hide Details" ImageControlID="imgImageControlID" ExpandedImage="~/Images/dashminus.gif"
                    CollapsedImage="~/Images/dashplus.gif" ExpandDirection="Vertical" />
                         <asp:Panel ID="pnlTaskDetails" runat="server">
                                    <table><tr><td>
                        Option 1 : Goto HMS >> Services >> QuickTrans >> Select Work Order >> Select Parent Accounts Services Group >> Select Service Item/Resource AND follow regular WORK ORDER Process<br />
                        Option 2 : 
Step 1 : Goto PMS >> Rush Order >> Select Work Order >> Select Parent Accounts Services Group >> Select Services Items /Resources >> Follow the regular Work order process </br>
                        Step 2 : Goto HMS &gt;&gt; Services menu group &gt;&gt; Click SDN &gt;&gt; Select the Work Order 
                        so created in the above step and perform usual SDN process &gt;&gt; Click Process &gt;&gt; 
                        Receive &gt;&gt; Upload Proof of the document if any&gt;&gt; Complete the process
                                        </td></tr></table>
                             </asp:Panel>
                    </td>
        </tr>
<%--        <tr>
            <td colspan="2"> <cc1:Accordion  ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" >
                                                    <Panes>
                                                        <cc1:AccordionPane ID="AccordionPane3" runat="server" HeaderCssClass="accordionHeader" Collapsed="true"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                               Process
                                                            </Header>
                                                            <Content>
                                                               
                                                                        <table><tr><td>
                        Option 1 : Goto HMS >> Services >> QuickTrans >> Select Work Order >> Select Parent Accounts Services Group >> Select Service Item/Resource AND follow regular WORK ORDER Process<br />
                        Option 2 : 
Step 1 : Goto PMS >> Rush Order >> Select Work Order >> Select Parent Accounts Services Group >> Select Services Items /Resources >> Follow the regular Work order process </br>
                        Step 2 : Goto HMS &gt;&gt; Services menu group &gt;&gt; Click SDN &gt;&gt; Select the Work Order 
                        so created in the above step and perform usual SDN process &gt;&gt; Click Process &gt;&gt; 
                        Receive &gt;&gt; Upload Proof of the document if any&gt;&gt; Complete the process
                                                                            </td></tr></table>
                                                                      
                                                            </Content>
                                                        </cc1:AccordionPane>
                                                    </Panes>
                                                </cc1:Accordion></td>

        </tr>--%>


        <tr>
            <td colspan="2" align="left">
                <asp:RadioButtonList ID="rbTaxasion" Font-Bold="true" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="rbTaxasion_SelectedIndexChanged">
                    <asp:ListItem Text="Process" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Show in Hijri" Selected="True" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:HyperLink Text="Show in Gregarian" ID="hylink" runat="server" NavigateUrl="NRIDocs.aspx"></asp:HyperLink>
            </td>
        </tr>
    </table>
    <table id="tblUnRecon" runat="server" width="100%">
        <tr>
            <td colspan="2" class="savebutton">Process Items
            </td>
        </tr>
        <tr>


            <td>Document Type<b>:</b><asp:DropDownList ID="ddlItems" Width="250" AutoPostBack="true" runat="server" CssClass="droplist"
                OnSelectedIndexChanged="ddlItems_SelectedIndexChanged">
            </asp:DropDownList>
                &nbsp;
                        <%--<asp:LinkButton ID="lnkAddNew" Font-Bold="true" runat="server" OnClick="lnkAddNew_Click">Add</asp:LinkButton>--%>
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
                        <asp:Button ID="btnAddNew" runat="server" Text="Add" OnClick="btnAddNew_Click" Font-Bold="True"
                            ForeColor="#CC6600" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>

            <td align="left">
                <asp:GridView ID="gvUnReconciled" AutoGenerateColumns="false" CssClass="gridview"
                    EmptyDataText="No Records found!" runat="server" OnRowCommand="gvUnReconciled_RowCommand" Width="60%">
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
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--  <asp:HyperLink ID="hlkReconse" Text="Reconsolise" OnClick='<%#Reconsolise(DataBinder.Eval(Container.DataItem, "SRNItemID").ToString(),DataBinder.Eval(Container.DataItem, "ResourceID").ToString(),DataBinder.Eval(Container.DataItem, "SRNID").ToString()) %>'  NavigateUrl='<%#Reconsolise(DataBinder.Eval(Container.DataItem, "SRNItemID").ToString(),DataBinder.Eval(Container.DataItem, "ResourceID").ToString(),DataBinder.Eval(Container.DataItem, "SRNID").ToString()) %>' runat="server">HyperLink</asp:HyperLink>--%>
                                <asp:LinkButton ID="btnReconse" CommandName="Reconse" CommandArgument='<%#Eval("SRNItemID")%>'
                                    Text="Resolve" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WO NO" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                    CommandName="View" Text='<%#Eval("PONO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PONAME" HeaderText="WO-NAME" />
                        <asp:BoundField Visible="false" DataField="InvoiceImg" HeaderText="InvoiceImg" />
                        <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" />
                        <asp:BoundField Visible="false" DataField="Remarks" HeaderText="Remarks" />
                        <%--<asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" />--%>
                        <%--<asp:TemplateField>
<ItemTemplate>
    <asp:HyperLink ID="lnkInvImage" NavigateUrl="#" onclick='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "EdnInvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "EDNId").ToString())%>' runat="server">Invoice</asp:HyperLink>
</ItemTemplate>
</asp:TemplateField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkImage" NavigateUrl="#" onclick='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>'
                                    runat="server"
                                    Visible='<%#ViewVisible(DataBinder.Eval(Container.DataItem, "SRNItemID").ToString(),2)%>'>Image</asp:HyperLink>
                                <%--<asp:LinkButton ID="lnkImage" PostBackUrl='<%#ViewImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>' OnClick='<%#ViewImage(DataBinder.Eval(Container.DataItem, "InvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "SRNItemID").ToString())%>' runat="server">Image</asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>
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
                                            <tr>
                                                <td>Worksite</b>&nbsp;<asp:DropDownList ID="ddlWorksite" runat="server" AutoPostBack="True"
                                                    CssClass="droplist" Width="150px">
                                                </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlWorksite">
                                                    </cc1:ListSearchExtender>
                                                    &nbsp;
                                                                                   <strong>Department</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddlDepartment" runat="server"
                                                                                       AutoPostBack="True" CssClass="droplist">
                                                                                   </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlDepartment">
                                                    </cc1:ListSearchExtender>
                                                    &nbsp;
                                                                               
                                                                                     <b>
                                                                                         <asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label>:</b>

                                                    <asp:DropDownList ID="ddlDesif2" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender3" runat="server" QueryPattern="Contains"
                                                        PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlDesif2">
                                                    </cc1:ListSearchExtender>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="savebutton" Text="Search"
                                                        OnClick="btnSearch_Click" />

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
                <asp:GridView ID="gvEdit" Width="100%" AutoGenerateColumns="false" CssClass="gridview"
                    runat="server" EmptyDataText="No Records found!" OnRowCommand="gvEdit_RowCommand">
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
                        <asp:TemplateField HeaderText="WO-NAME">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                    CommandName="View" Text='<%#Eval("WOName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField>
<ItemTemplate>
    <asp:HyperLink ID="lnkInvImage" NavigateUrl="#" onclick='<%#ViewInvImage(DataBinder.Eval(Container.DataItem, "EdnInvoiceImg").ToString(),DataBinder.Eval(Container.DataItem, "EDNId").ToString())%>' runat="server">Invoice</asp:HyperLink>
</ItemTemplate>
</asp:TemplateField>--%>
                        <%-- <asp:BoundField DataField="PONAME" HeaderText="WO-NAME" />--%>
                        <asp:BoundField DataField="ResourceName" HeaderText="Doc-NAME" />

                        <asp:BoundField Visible="false" DataField="InvoiceImg" HeaderText="InvoiceImg" />
                        <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" />
                        <asp:TemplateField HeaderText="Employee">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMachinery" CssClass="droplist" DataSource='<%#ViewState["Machinery"]%>'
                                    DataTextField="Name" DataValueField="EmpId" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valid From">
                            <ItemTemplate>
                                <asp:TextBox ID="txtVFrom" Width="80px" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtVFrom"
                                    TargetControlID="txtVFrom" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upto">
                            <ItemTemplate>
                                <asp:TextBox ID="txtVTo" Width="80px" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtVTo"
                                    TargetControlID="txtVTo" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Number">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Alt Number">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAltNumber" runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Issue Place">
                            <ItemTemplate>
                                <asp:TextBox ID="txtIssuePlace" runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issuer">
                            <ItemTemplate>
                                <asp:TextBox ID="txtIssuer" runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Proof">
                            <ItemTemplate>
                                <asp:FileUpload ID="ImgUpload" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandName="Edt" CommandArgument='<%#Eval("SRNItemID")%>'
                                    runat="server">Update</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

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
                                <table cellpadding="0" cellspacing="0" style="width: 100%">

                                    <tr>
                                        <td>Worksite</b>&nbsp;<asp:DropDownList ID="ddlRecWs" runat="server" AutoPostBack="True"
                                            CssClass="droplist" Width="150px">
                                        </asp:DropDownList>
                                            <cc1:ListSearchExtender ID="ListSearchExtender4" runat="server" QueryPattern="Contains"
                                                PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlRecWs">
                                            </cc1:ListSearchExtender>
                                            &nbsp;
                                                                                   <strong>Department</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddlRecDept" runat="server"
                                                                                       AutoPostBack="True" CssClass="droplist">
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
                                            <b>Document Type:</b>&nbsp;<asp:DropDownList ID="ddlRecItems" AutoPostBack="true" CssClass="droplist" onselectedindexchanged="ddlRecItems_SelectedIndexChanged"
                                                runat="server">
                                            </asp:DropDownList>
                                            <b>&nbsp; Employee:</b><asp:DropDownList ID="ddlSearcMech" Width="300" CssClass="droplist"
                                                AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Search"
                            OnClick="btnRecSearch_Click" />
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
            <td>&nbsp;
            </td>
        </tr>
        <tr>

            <td>
                <asp:GridView ID="gvRecosiled" EmptyDataText="No Records found!" AutoGenerateColumns="false"
                    CssClass="gridview" runat="server" OnRowCommand="gvRecosiled_RowCommand" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="WO NO">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                    CommandName="View" Text='<%#Eval("PONO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TransID">
                            <ItemTemplate>
                                <asp:Label ID="lblTransID" runat="server" Text='<%#Eval("TransID")%>'></asp:Label>
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
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPODetID" runat="server" Text='<%#Eval("PodetID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PONAME" HeaderText="WO-NAME" />
                        <asp:BoundField Visible="false" DataField="InvoiceImg" HeaderText="InvoiceImg" />
                        <asp:BoundField DataField="From" HeaderText="Valid From" />
                        <asp:BoundField DataField="To" HeaderText="Valid Upto" />

                        <asp:BoundField DataField="Numeber" HeaderText="Number" />
                        <asp:BoundField DataField="AltNumber" HeaderText="Alt Number" />
                        <asp:BoundField DataField="IssuePlace" HeaderText="IssuePlace" />
                        <asp:BoundField DataField="Issuer" HeaderText="Issuer" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                        <asp:BoundField DataField="ApprovedBy" HeaderText="Submitted By" />
                        <asp:BoundField ItemStyle-ForeColor="Red" DataField="Status" HeaderText="Status" />
                        <asp:BoundField Visible="false" DataField="InvoiceImg" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkImage" PostBackUrl='<%#ViewImage(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "ID").ToString())%>'
                                    OnClick='<%#ViewImage(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "ID").ToString())%>'
                                    runat="server">Image</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--12-02-2016--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" CommandName="Edt" CommandArgument='<%#Eval("SRNItemID")%>'
                                    runat="server">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnlDelete" CommandName="Del" CommandArgument='<%#Eval("SRNItemID")%>'
                                    runat="server">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>

            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="PageTax" runat="server" />
            </td>
        </tr>
    </table>
    <table id="tblFinalEdit" runat="server" width="100%" align="left" >
        <tr>
            <td>
                <table>
                    <tr>
                        <td>WO NO  
                        </td>
                        <td>
                            <asp:TextBox ID="txtWONO1" runat="server" ReadOnly="true" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>WO-NAME
                        </td>
                        <td>
                            <asp:TextBox ID="txtWoName1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Approved By
                        </td>
                        <td>
                            <asp:TextBox ID="txtApprovedBy1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>CreatedOn
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedOn1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Employee
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlMachinery1" CssClass="droplist" 
                            DataSource='<%#ViewState["Machinery"]%>' DataTextField="Name" DataValueField="EmpId"
                            runat="server">
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Valid From 
                        </td>
                        <td>
                            <huc1:hijrigregdatepicker ID="HijriGregDatePicker1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Up To 
                        </td>
                        <td>
                            <huc1:hijrigregdatepicker ID="HijriGregDatePicker2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Number
                        </td>
                        <td>
                             <asp:TextBox ID="txtNumber1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Alt Number
                        </td>
                        <td>
                             <asp:TextBox ID="txtAltNumber1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Issue Place
                        </td>
                        <td>
                             <asp:TextBox ID="txtIssuePlace1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Issuer
                        </td>
                        <td>
                             <asp:TextBox ID="txtIssuer1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Proof
                        </td>
                        <td>                            
                            <asp:FileUpload ID="ImgUpload1" runat="server" />                            
                        </td>
                    </tr>
                      <tr>
                        <td>
                             Remarks
                        </td>
                        <td >
                             <asp:TextBox ID="txtRemarks1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>                    
                        <td style="display:none">
                            <asp:TextBox ID="txtExtention1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>                    
                        <td style="display:none">
                            <asp:TextBox ID="txtSRNID1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="display:none">
                            <asp:TextBox ID="txtSRNItemID1" runat="server"></asp:TextBox>
                        </td>
                    </tr>                   
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  CssClass="savebutton"/>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>

        <tr>
            <td>
                <asp:GridView ID="gvFinalEdit" Width="100%" AutoGenerateColumns="false" CssClass="gridview"
                    runat="server" EmptyDataText="No Records found!" OnRowCommand="gvFinalEdit_RowCommand">
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
                                            TargetControlID="txtVFrom" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                <%--<huc1:HijriGregDatePicker ID="txtVFrom" runat="server" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upto">
                            <ItemTemplate>
                                <asp:TextBox ID="txtVTo" Text='<%#Bind("[To]")%>' Width="80px" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtVTo"
                                    TargetControlID="txtVTo" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <%--<huc1:HijriGregDatePicker ID="txtVTo"  runat="server" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Number">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumber" Text='<%#Bind("Numeber")%>' runat="server"></asp:TextBox>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Alt Number">
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
                                <asp:TextBox ID="txtIssuer" Text='<%#Bind("Issuer")%>' runat="server"></asp:TextBox>

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
                                <asp:LinkButton ID="lnkEdit" CommandName="Edt" CommandArgument='<%#Eval("SRNItemID")%>'
                                    runat="server">Update</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                    <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                        <img src="IMAGES/Loading.gif" alt="update is in progress" />
                        <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" />
                    </asp:Panel>
                </asp:Panel>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


</asp:Content>
