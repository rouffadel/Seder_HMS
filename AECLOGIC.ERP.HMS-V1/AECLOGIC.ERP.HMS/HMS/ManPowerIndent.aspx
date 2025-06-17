<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManPowerIndent.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.ManPowerIndent"
    MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function ShowValidateMul() {

            if (!chkDropDownList('<%=ddlWorksiteID.ClientID%>', 'WorkSite'))
                return false;
            if (!chkDropDownList('<%=ddlProjectID.ClientID%>', 'Project'))
                return false;
            if (!Reval("<%=txtReqReference.ClientID %>", "Reference", '[Enter Refernece'))
                return false;
            
            var elSel = document.getElementById('<%=lbxItems.ClientID%>');

            var j = 0;
            for (i = 0; i < elSel.options.length; i++) {
                if (elSel.options[i].selected) {
                    if (elSel.options[i].value == 0) {
                        alert("Select Valid Item");
                        return false;
                    }
                    j++;
                }
            }
            if (j == 0) {
                alert("Select atleast one Item");
                return false;
            }
            if (!chkDropDownList('<%=ddlSpecialisation.ClientID%>', 'Specialisation'))
                return false;
            if (!chkDropDownList('<%=ddlDesignation.ClientID%>', 'Designation'))
                return false;
            if (!Reval("<%=txtMPNos.ClientID %>", "Man Power", '[Enter Man Power (Nos)]'))
                return false;
            if (!Reval("<%=txtFrom.ClientID %>", " Req Date", '[Enter Date]'))
                return false;
        }
    </script>
    <asp:Panel ID="pnltblEdit" runat="server" CssClass="box box-primary" Visible="true"
        Width="100%">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 50%; height: 100%">
           
            <tr>
                <td >
                    <asp:Label ID="lblWorksiteID" runat="server">Worksite<span style="color: #ff0000">*</span></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorksiteID" runat="server" Width="200px" MaxLength="30" TabIndex="2" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlWorksiteID_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblProjectID" runat="server">Project<span style="color: #ff0000">*</span></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProjectID" runat="server" Width="200px" MaxLength="30" TabIndex="3">
                    </asp:DropDownList>
                  

                </td>
            </tr>
            <tr>

                <td >
                    <asp:Label ID="lblReqReference" runat="server">Req Reference<span style="color: #ff0000">*</span></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtReqReference" runat="server" Width="200px" MaxLength="30" TabIndex="7">
                        </asp:TextBox>
                  
                </td>

            </tr>
            <tr>
                <td>Resource&#39;s Search </td>
                <td>
                    <asp:TextBox ID="txtnewsearch" runat="server" Width="150"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearchNew" runat="server" CssClass="btn btn-primary
                        btn-xs nomrg" OnClick="btnSearchNew_Click" Text="Search" />
                </td>
            <tr>
                <td>Resource Group<span style="color: #ff0000">*</span> </td>
                <td>
                    <asp:DropDownList ID="ddsrg" runat="server" AccessKey="w" AutoPostBack="True" CssClass="droplist" OnSelectedIndexChanged="ddsrg_SelectedIndexChanged" ToolTip="Alt+w OR Alt+t+Enter" Width="250">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">Resource Item<span style="color: #ff0000">*</span> </td>
                <td>
                    <asp:ListBox ID="lbxItems" runat="server" AccessKey="r" CssClass="droplist full__width" Height="200px" Rows="30" SelectionMode="Multiple" ToolTip="Alt+r OR Alt+r+Enter" Width="200px"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td>Specialisation<span style="color: #ff0000">*</span> </td>
                <td>
                    <asp:DropDownList ID="ddlSpecialisation" runat="server" AccessKey="w" CssClass="droplist" 
                         ToolTip="Alt+w OR Alt+t+Enter" Width="250">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Designation<span style="color: #ff0000">*</span> </td>
                <td>
                    <asp:DropDownList ID="ddlDesignation" runat="server" AccessKey="w" CssClass="droplist" 
                       ToolTip="Alt+w OR Alt+t+Enter" Width="250">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>Req Man Power(No&#39;s)<span style="color: #ff0000">*</span> </td>
                <td>
                    <asp:TextBox ID="txtMPNos" runat="server"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="wetxtName" runat="server" TargetControlID="txtMPNos" WatermarkCssClass="watermarked" WatermarkText="[Enter Man Power(No's)]">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td>Req Date<span style="color: #ff0000">*</span> </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMM yyyy" PopupButtonID="txtFrom" TargetControlID="txtFrom">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>&nbsp; </td>
                <td>
                    <asp:Button ID="btnADDMultiple" runat="server" AccessKey="m" CssClass="btn btn-success" Height="21px" OnClick="btnADDMultiple_Click" OnClientClick="javascript:return ShowValidateMul()" Text="Save" />
                </td>
            </tr>


        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDetails" runat="server" CssClass="box box-primary" Visible="true"
        Width="100%">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
               
                <td>WorkSite: &nbsp;&nbsp;                                                
                     <asp:DropDownList ID="ddlWorkSite22" runat="server" Width="100px"  AutoPostBack="true"                        
                          OnSelectedIndexChanged="ddlWorkSite22_SelectedIndexChanged" ></asp:DropDownList>
                    &nbsp;&nbsp; 
                    Project &nbsp;&nbsp;                                                
                    <asp:DropDownList ID="ddlProject1" runat="server" Width="150px" MaxLength="30" TabIndex="3">
                    </asp:DropDownList>
                    Specialisation &nbsp;                
                    <asp:DropDownList ID="ddlSpecialisation1" runat="server" AccessKey="w" CssClass="droplist"
                        ToolTip="Alt+w OR Alt+t+Enter" Width="150">
                    </asp:DropDownList> Designation  &nbsp;&nbsp;              
                    <asp:DropDownList ID="ddlDesignation1" runat="server" AccessKey="w" CssClass="droplist" ToolTip="Alt+w OR Alt+t+Enter" Width="150">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                        Format="dd MMM yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                    </cc1:CalendarExtender>
                        <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                        Format="dd MMM yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate">
                    </cc1:CalendarExtender>
                    <asp:Button ID="btnHMSMpSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnHMSMpSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvMPReq" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                        GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="4"
                        EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                        CssClass="gridview" OnRowCommand="gvMPReq_RowCommand"
                        OnRowDataBound="gvMPReq_RowDataBound">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Check" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"><%-- 0--%>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHeader" AutoPostBack="true" runat="server"
                                        ToolTip="Toogle Check/Uncheck All" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkPrereq" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField > <%-- 1--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblOMSMPReqID" runat="server" Text='<%#Eval("OMSMPReqID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="WorkSite" HeaderText="WorkSite" /> <%-- 2--%>
                            <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" /> <%-- 3--%>
                            <asp:BoundField DataField="ReqReference" HeaderText="ReqReference" /> <%-- 4--%>
                            <asp:BoundField DataField="ResourceName" HeaderText="Resource Name" /> <%-- 5--%>
                            <asp:BoundField DataField="Category" HeaderText="Specialisation" /> <%-- 6 --%>
                            <asp:BoundField DataField="Designation" HeaderText="Designation" /> <%-- 7 --%>
                            <asp:BoundField DataField="ReqDate" HeaderText="Req Date" /> <%-- 8 --%>
                            <asp:BoundField DataField="MPNos" HeaderText="MP Nos" /> <%-- 9--%>
                            <asp:TemplateField HeaderText="MP Nos"> <%-- 10--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMPHours" runat="server" Text='<%#Bind("MPNos") %>' Width="50px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
            
                            <asp:TemplateField> <%-- 11--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Upd" Text="Update" CommandArgument='<%#Bind("OMSMPReqID")%>'
                                        CssClass="btn btn-success"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField> <%-- 12--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApprove" runat="server" CommandName="Approve" Text="Approve"
                                        CommandArgument='<%#Bind("OMSMPReqID")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField> <%-- 13 --%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkReject" runat="server" CommandName="Reject" Text="Reject" CommandArgument='<%#Bind("OMSMPReqID")%>'
                                        CssClass="btn btn-primary"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height: 17px">
                    <uc1:Paging ID="MPReq" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
