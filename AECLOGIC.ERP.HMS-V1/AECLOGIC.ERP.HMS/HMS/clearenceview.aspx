<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clearenceview.aspx.cs" Inherits="AECLOGIC.ERP.HMS.WebForm1" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function showApp(EmpID) {
            // window.showModalDialog("../HMS/Checkcustodi.aspx?Empid=" + EmpID, "", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
            //  window.showModalDialog("../HMS/Checkcustodi.aspx?Empid=" + EmpID +"", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <asp:MultiView ID="mainview" runat="server">
                    <asp:View ID="Newvieew" runat="server">
                        <asp:TextBox ID="txtname" Visible="false" runat="server"></asp:TextBox>
                        <tr>
                            <asp:Label ID="Label2" runat="server" Text="Clearances Pending List" Font-Size="16px" ForeColor="blue"></asp:Label>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblemp" runat="server" Text="Handing Over Emp"></asp:Label>&nbsp;:
                               <asp:Label ID="lblname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gdvWSclr" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gdvWSclr_RowCommand"
                                    OnRowDataBound="gdvWSclr_RowDataBound" HeaderStyle-CssClass="tableHead" AllowSorting="True"
                                    CssClass="gridview" EmptyDataText="Clearance Not applicable,Since the employee does not hold any company items or liable to pay any dues"
                                    EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" Visible="false" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkApprove" Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deptid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDept" runat="server" Text='<%#Eval("departmentuid")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CIDMAST" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCIDMAST" runat="server" Text='<%#Eval("CIDMAST")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ClearID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblClearID" runat="server" Text='<%#Bind("ClearID")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DepartmentUId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldepartID" runat="server" Text='<%#Bind("ClearID")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="departmentname" HeaderText="Department">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Clearance Type">
                                            <HeaderStyle Width="350" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"itemname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taking Over Emp" HeaderStyle-Width="250px" ItemStyle-Width="250px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlMachinery"
                                                    DataTextField="Name" DataValueField="EmpId" runat="server" Style="width: 250px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" Visible="false" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In Custody">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton ID="lnkcst" runat="server" 
                                                        CommandName="incostod" Text="In Custody" style="width:500px"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkmms" CommandName="incostod" runat="server" CommandArgument='<%#Eval("departmentuid")%>' CssClass="anchor__grd edit_grd">View</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvhd" runat="server" Font-Size="10px" ForeColor="blue"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 17px">
                                <uc1:Paging ID="EmpListPaging" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" Visible="false" CssClass="btn btn-success" Text="Save All"
                                    OnClick="btnUpdate_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="gvmms" runat="server" HeaderStyle-CssClass="gv" EmptyDataText="No records Found"
                                    AlternatingRowStyle-BackColor="GhostWhite" AutoGenerateColumns="false" DataKeyNames="ResourceID,EmpId" CssClass="gridview" Width="80%">
                                    <Columns>
                                        <asp:BoundField DataField="ResourceName" HeaderText="Material Name" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="IssuedQty" HeaderText="Issued Qty" />
                                        <asp:BoundField DataField="ReturnQty" HeaderText="Return Qty" />
                                        <asp:BoundField DataField="BalQty" HeaderText="Balance Qty" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnback" runat="server" CssClass="btn btn-primary" Text="Back"
                                    OnClick="btnback_Click" />
                            </td>
                        </tr>
                    </asp:View>
                </asp:MultiView>
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
