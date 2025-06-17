<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="BillsApproval.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.BillsApproval" Title=" " %>

<%@ OutputCache Location="None" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="Help.ascx" TagName="Help" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">

      
        function CancelAsyncPostBack() {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //alert(prm);
            if (prm.get_isInAsyncPostBack()) {
                prm.abortPostBack();
            }
        }

    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:HyperLink ID="lnkApprovals" runat="server" Text="Corroborate" NavigateUrl="BillsApproval.aspx?state=1" />
                        &nbsp;|&nbsp;<asp:HyperLink ID="lnkApproved" runat="server" NavigateUrl="BillsApproval.aspx?state=2"
                            Text="Approved" />
                        &nbsp;|
                        <asp:HyperLink ID="lnkRejected" runat="server" NavigateUrl="BillsApproval.aspx?state=3"
                            Text="Rejected" />
                        &nbsp;|
                        <asp:HyperLink ID="lnkUnbilled" runat="server" Text="UnBilled" NavigateUrl="UnBilledGdns.aspx"></asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
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
                                                <td style="width: 120px;">
                                                    PO No
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Vendor
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVendor" runat="server" DataValueField="ID" DataTextField="Name"
                                                        AppendDataBoundItems="true" Width="150" CssClass="droplist">
                                                    </asp:DropDownList>
                                                       <cc1:ListSearchExtender QueryPattern="Contains"  ID="LEDestRepre" runat="server" IsSorted="true" 
                                                        PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search" 
                                                        TargetControlID="ddlVendor" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 120px;">
                                                    Bill No
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBillNo" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Worksite
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWorkSites" DataValueField="ID" DataTextField="Name" AppendDataBoundItems="true"
                                                        Width="150" CssClass="droplist" runat="server">
                                                    </asp:DropDownList>
                                                       <cc1:ListSearchExtender QueryPattern="Contains"  ID="ListSearchExtender1" runat="server" IsSorted="true" 
                                                        PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search" 
                                                        TargetControlID="ddlWorkSites" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="droplist">
                                                        <asp:ListItem Value="1" Text="Posted to Accounts" Selected="True" ></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Drafts in Accounts" ></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 5px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button Width="100" ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" />
                                                    <asp:Button Width="100" ID="btClear" OnClick="btClear_Click" runat="server" Text="Reset" />
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
                    <td colspan="2" style="width: 100%">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvAutoBilling" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        OnRowCommand="GVAutoBilling_RowCommand" HeaderStyle-CssClass="tableHead" OnRowDataBound="gvAutoBilling_RowDataBound" CssClass="gridview">
                                        <SelectedRowStyle CssClass="selected" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkBill" runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField HeaderText="Bill No" Target="_blank" DataTextField="BillNO" DataNavigateUrlFields="BillNO"
                                                DataNavigateUrlFormatString="ViewBillDetails.aspx?id={0}" />
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="Vendor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<% #Eval("vendor_name") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Worksite">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkSite" runat="server" Text='<% #Eval("Site_Name") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Bill Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOtherCharges" runat="server" Text='<%#Eval("BillName") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="Goods With Qty,UoM & Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGoods" runat="server" Text='<% #Eval("Goods") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="POName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPOName" runat="server" Text='<% #Eval("POName") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="StatementPeriod" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatementPeriod" runat="server" Text='<%#Eval("StatementPeriod")%>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Goods Quantity" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<% #Eval("Quantity") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# AECLOGIC.ERP.HMS.NUM.NumberFormatting(Convert.ToDouble(Eval("Amount").ToString()))%>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnk" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"poid").ToString())%>'
                                                        CommandName="View" Text="PO" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="lnkApprove" runat="server" CommandArgument='<%# Eval("BillNo") %>'
                                                        CommandName="Approve" Text="Approve" CssClass="savebutton"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillNo" runat="server" Text='<% #Eval("BillNo") %>' Width="100%"
                                                        Visible="false"></asp:Label>
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
                                    <uc1:Paging ID="pgGoods" runat="server" CurrentPage="1" NoOfPages="1" Visible="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnApprove" runat="server" Text="Approve All" 
                                        OnClick="btnApprove_Click" CssClass="savebutton" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvEditQtys" runat="server" AutoGenerateColumns="false" DataKeyNames="GDNID,GDNITEMID"
                                        CssClass="gridview">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="GDN">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGDN" Text='<% #Eval("GDNID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="GRN">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGRN" Text='<% #Eval("GDNITEMID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WorkSite">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlWS" runat="server" AutoPostBack="false" DataSource="<%# BindWS()%>"
                                                       CssClass="droplist"  DataTextField="Name" DataValueField="ID" SelectedIndex='<%# GetWSIndex(Eval("WorkSite").ToString())%>'>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlVDR" runat="server" AutoPostBack="false" DataSource="<%# BindVendor()%>"
                                                       CssClass="droplist"  DataTextField="Name" DataValueField="ID" SelectedIndex='<%# GetVdrIndex(Eval("Vendor").ToString())%>'>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Order">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlWO" runat="server" AutoPostBack="false" DataSource="<%# BindWO()%>"
                                                     CssClass="droplist" DataTextField="Name" DataValueField="ID" SelectedIndex='<%# GetWOIndex(Eval("WO").ToString())%>'>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDate" Text='<% #Eval("Date") %>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderIndDate" TargetControlID="txtDate" runat="server"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="Disp Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDispQty" onblur="javascript:return QtyUpdate();"
                                                        Text='<% #Eval("DispQty") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="In Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtInQty" Text='<% #Eval("InQty") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="Acpt Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtAcceptQty" Text='<% #Eval("AcptQty") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="TS">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtTS" Text='<% #Eval("TS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50" HeaderText="Distance">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDistace" Text='<% #Eval("Distance") %>'></asp:TextBox>
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
                                    <asp:Button Text="Update" ID="btnUpdateQtys" OnClick="btnUpdateQtys_OnClick" 
                                        runat="server" CssClass="savebutton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="UpdateProgressCSS">
                <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                    <ProgressTemplate>
                        <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                            <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                <img src="IMAGES/Loading.gif" alt="update is in progress" />
                                <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" /></asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvAutoBilling" />
<ajax:AsyncPostBackTrigger ControlID="gvAutoBilling"></ajax:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
