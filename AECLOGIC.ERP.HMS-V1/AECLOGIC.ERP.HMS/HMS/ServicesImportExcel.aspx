<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ServicesImportExcel.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.ServicesImportExcel" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">
    function ValidateSave()
    {
    
    if (!chkDropDownList("<%=ddlVendor.ClientID %>", "Vendor"))
                return false;
    
    if (!chkDropDownList("<%=ddlWorkSite.ClientID %>", "WorkSite"))
                return false;
    if (!chkDropDownList("<%=ddlDestRepre.ClientID %>", "Rep at Dest"))
                return false;
    if (!chkDropDownList("<%=ddlOriRepre.ClientID %>", "Rep at Origin"))
                return false;
    if (!chkDropDownList("<%=ddlResourcetype.ClientID %>", "Material"))
                return false;
    if (!chkDropDownList("<%=ddlOrigin.ClientID %>", "Origin"))
                return false;   
    if (!chkDropDownList("<%=ddlDestination.ClientID %>", "Destination"))
                return false;           
                
    }
    
    </script>

    <table cellpadding="1" cellspacing="1" width="100%">
       
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" valign="top">
                <asp:FileUpload ID="fileupload" runat="server" Width="400" TabIndex="1" />
                <asp:Button ID="Button1" runat="server" Text="Import" OnClick="btnImport_Click" 
                    CssClass="savebutton" TabIndex="2" />
                <asp:HyperLink ID="hyperlink1" runat="server" Text="Excel Format" Target="_blank"
                    NavigateUrl="~/Reports/GDNs.xls" TabIndex="3"></asp:HyperLink>
                <asp:HiddenField ID="hdFileName" runat="server" />
                <asp:HiddenField ID="hdFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="left">
                <div id="dvTUQD" runat="server" visible="false">
                    <asp:UpdatePanel ID="upMain" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlOuterTop" runat="server" BackColor="#DCDCDC" Width="750px">
                                <asp:Panel ID="panel1" runat="server" BackColor="#DCDCDC" Width="740px" Style="margin-left: auto;
                                    margin-right: auto; margin-bottom: auto; margin-top: auto;">
                                    <table>
                                        <tr>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDate" runat="server" Width="25px" MaxLength="1" 
                                                    TabIndex="1">D</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Vendor<span style="color: #FF0000"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlVendor" runat="server" DataValueField="ID" DataTextField="Name"
                                                    AppendDataBoundItems="true" Width="200" CssClass="droplist" TabIndex="2">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                    TargetControlID="ddlVendor" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                WorkSite<span style="color: #FF3300"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlWorkSite" runat="server" DataValueField="ID" DataTextField="Name"
                                                    OnSelectedIndexChanged="Vendor_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true"
                                                    Width="200" CssClass="droplist" TabIndex="3">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender2" runat="server"
                                                    TargetControlID="ddlWorkSite" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                SO NO<span style="color: #FF0000"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPONO" runat="server" DataValueField="PONO" DataTextField="PONO"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="200" CssClass="droplist"
                                                     TabIndex="4">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender3" runat="server"
                                                    TargetControlID="ddlPONO" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Vehicle No
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVehicleNo" runat="server" Width="25px" MaxLength="1" 
                                                    TabIndex="5">M</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                TripSheet No
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTripSheet" runat="server" Width="25px" MaxLength="1" 
                                                    TabIndex="6">B</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Royalty Challan NO
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtroyaltychalana" runat="server" Width="25px" MaxLength="1" 
                                                    TabIndex="7">c</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Dis Qty
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" runat="server" Width="25px" MaxLength="1" TabIndex="8">J</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Rep at Dest<span style="color: #FF3300"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDestRepre" runat="server" DataValueField="ID" DataTextField="Name"
                                                    AppendDataBoundItems="true" Width="200" CssClass="droplist" TabIndex="9">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender4" runat="server"
                                                    TargetControlID="ddlDestRepre" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Rep at Origin<span style="color: #FF3300"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOriRepre" runat="server" DataValueField="ID" DataTextField="Name"
                                                    AppendDataBoundItems="true" Width="200" CssClass="droplist" TabIndex="10">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender5" runat="server"
                                                    TargetControlID="ddlOriRepre" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Material <span style="color: #FF3300"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlResourcetype" DataValueField="ID" DataTextField="Name" AppendDataBoundItems="true"
                                                    runat="server" CssClass="droplist" Width="200" TabIndex="11">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender6" runat="server"
                                                    TargetControlID="ddlResourcetype" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Origin <span style="color: #FF3300"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrigin" runat="server" DataValueField="ID" DataTextField="Name"
                                                    AppendDataBoundItems="true" Width="200" CssClass="droplist" TabIndex="12">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender7" runat="server"
                                                    TargetControlID="ddlOrigin" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Destination <span style="color: #FF3300"><sup>*</sup></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDestination" runat="server" DataValueField="ID" DataTextField="Name"
                                                    AppendDataBoundItems="true" Width="200" CssClass="droplist" TabIndex="13">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender8" runat="server"
                                                    TargetControlID="ddlDestination" PromptText="Type to search" PromptCssClass="PromptText"
                                                    PromptPosition="Top" IsSorted="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnMapExcel" Text="Map" runat="server" Width="100px" OnClick="btnMapExcel_Click"
                                                    CssClass="savebutton" TabIndex="14" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnMapExcel" />
<Ajax:PostBackTrigger ControlID="btnMapExcel"></Ajax:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnMapExcel"></asp:PostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
            <td colspan="3" align="left">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div id="DVMAP" style="overflow: auto; height: 300px" runat="server">
                    <asp:GridView ID="GridViewExcel" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" ForeColor="Black">
                        <FooterStyle BackColor="#CCCC99" />
                        <RowStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvMapping" runat="server" CssClass="gridview" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" ForeColor="Black"
                    AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="SR.NO" DataField="SrNo" />
                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"
                            HtmlEncode="false" />
                        <asp:BoundField HeaderText="Vendor" DataField="Vendor" />
                        <asp:BoundField HeaderText="WorkSite" DataField="WorkSite" />
                        <asp:BoundField HeaderText="Vehicle" DataField="Vehicle" />
                        <asp:BoundField HeaderText="Qty" DataField="Qty" />
                        <asp:BoundField HeaderText="TripSheet" DataField="TripSheet" />
                        <asp:BoundField HeaderText="Material" DataField="Material" />
                        <asp:BoundField HeaderText="RoyaltyChallan" DataField="RoyaltyChalanaNO" Visible=false />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="VendorID" Text='<% #Eval("VendorID") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PONo" Text='<% #Eval("PONO") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="WorkSiteID" Text='<% #Eval("WorkSiteID") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="OriginID" Text='<% #Eval("OriginID") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DestID" Text='<% #Eval("DestID") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="RepAtDest" Text='<% #Eval("RepAtDest") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="RepAtOrigin" Text='<% #Eval("RepAtOrigin") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="ResourceID" Text='<% #Eval("ResourceID") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text="Send to Waiting" Visible="false" OnClick="BtnSave_Click"
                    CssClass="savebutton" TabIndex="15" />
            </td>
        </tr>
    </table>
</asp:Content>
