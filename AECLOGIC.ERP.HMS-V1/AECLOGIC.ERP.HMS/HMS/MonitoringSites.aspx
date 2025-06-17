<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="MonitoringSites.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MonitoringSites" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Valid() {

            //  Monitoring Engineer
            if (!chkDropDownList('<%=ddlME.ClientID %>', 'Monitoring Engineer'))
                return false;
            //Monitoring WorkSite
            if (!chkDropDownList('<%=ddlMWS.ClientID %>', 'Monitoring WorkSite'))
                return false;
            //Monitoring From
            if (!chkDate('<%=txtMFrom.ClientID %>', 'Date of Birth', true, '', true))
                return false;
        }
    </script>
    
    <table width="100%" id="tblAdd" runat="server" visible="false">
        <tr>
            <td colspan="2">
                <b>WorkSite:</b><asp:DropDownList ID="ddlWs" CssClass="droplist" runat="server">
                </asp:DropDownList>
                <b>&nbsp;&nbsp;&nbsp; Department:</b><asp:DropDownList ID="ddlDept" CssClass="droplist"
                    runat="server">
                </asp:DropDownList>
                <b>&nbsp;&nbsp;&nbsp;&nbsp; EmpName:</b><asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    EmpID:</b><asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <b>Monitoring Engineer*:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlME" runat="server" CssClass="droplist">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                    TargetControlID="ddlME" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCount" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 26px; width: 25%;">
                <b>Monitoring WorkSite*:</b>
            </td>
            <td style="height: 26px">
                <asp:DropDownList ID="ddlMWS" CssClass="droplist" runat="server" Style="margin-left: 0px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <b>Monitoring From*:</b>
            </td>
            <td>
                <asp:TextBox ID="txtMFrom" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <b>Monitoring Upto:</b>
            </td>
            <td>
                <asp:TextBox ID="txtMUpto" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Assign" OnClick="btnSubmit_Click"
                    OnClientClick="javascript:return Valid();" />
            </td>
        </tr>
    </table>
    <table id="tblEditing" width="100%" runat="server" visible="false">
        <tr>
            <td class="pageheader">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="No Records Found" Width="100%" OnRowCommand="gvView_RowCommand"
                    CssClass="gridview">
                    <Columns>
                        <asp:BoundField Visible="false" DataField="MIID" />
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" />
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Monitoring Site" DataField="Site_Name" />
                        <asp:BoundField HeaderText="Monitoring From" DataField="MonitoringFrom" />
                        <asp:BoundField HeaderText="Monitoring Upto" DataField="MonitoringUpto" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandArgument='<%#Eval("MIID") %>' CommandName="Edt"
                                    runat="server">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" ForeColor="Red" OnClientClick="return confirm('Are you Sure?');"
                                    CommandArgument='<%#Eval("MIID") %>' CommandName="Del" runat="server">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
