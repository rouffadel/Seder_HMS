<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmsStock.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmsStock" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function check(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text=" Clearance List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Handing Over Emp:</strong>
                        <asp:Label ID="lblname" runat="server"></asp:Label>&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Taking Over Emp:</strong>
                        <asp:Label ID="lblTakeoverEmp" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblems" runat="server" Text="EMS" Visible="false"></asp:Label>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkems" runat="server" OnClick="lnkems_search">Equipment View</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkems1" runat="server" OnClick="lnkems1_search">Regestration View</asp:LinkButton>
                            </td>
                        </tr>
                    </td>
                </tr>
                <%--  grid view ems(euipment)--%>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Equipment Pending List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" BackColor="white" ForeColor="blue" BorderColor="ActiveBorder"
                            BorderStyle="dashed" BorderWidth="1" Height="20" Visible="false">Equipment View</asp:Label>
                        <asp:GridView ID="gvems" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="100%" CssClass="gridview" EmptyDataText="No Records">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEqptID" runat="server" Text='<%#Eval("SubResID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Corp ID" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubResourceName" runat="server" Text='<%#Eval("SubResourceName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eqpt Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEqptName" runat="server" Text='<%#Eval("ResourceName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shift1 Operator" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFOperator" runat="server" Text='<%#Eval("FOperator")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shift2 Operator" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSOperator" runat="server" Text='<%#Eval("SOperator")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work-Site" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEqptSiteName" runat="server" Text='<%#Eval("Site_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkheadmms" runat="server" AutoPostBack="false" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemmms" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <%--  grid view ems(regestration)--%>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Regestration Pending List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" BackColor="white" ForeColor="blue" BorderColor="ActiveBorder"
                            BorderStyle="dashed" BorderWidth="1" Height="20" Visible="false">Regestration View</asp:Label>
                        <asp:GridView ID="gdvreg" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                            Width="100%"
                            EmptyDataText="No Records">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ActID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Equipt Id" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("subresourcename")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Equipment Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("resourcename")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("ActName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WorkSite" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("site_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Handing Over Emp" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkhd" runat="server" AutoPostBack="false" onclick="check(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chlitm" runat="server" />
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
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Save"
                            OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-warning" Text="Back"
                            OnClick="btnback_Click" />
                    </td>
                </tr>
                <%-- cleared list equipment gridview--%>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Equipment Cleared List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvalereadycleared" runat="server" AutoGenerateColumns="false" ToolTip="gvLoanView"
                            HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                            CssClass="gridview">
                            <Columns>
                                <asp:BoundField DataField="HandingOverEmpid" HeaderText="HandingOverEmp" />
                                <asp:BoundField HeaderText="TakingOverEmp" DataField="TakingOverEmpid" />
                                <asp:BoundField DataField="departmentname" HeaderText="Department Name" />
                                <asp:BoundField DataField="Clearenceitems" HeaderText="Cleared Item" />
                                <asp:BoundField DataField="createdon" HeaderText="Cleared Date" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <%-- cleared list regestration gridview--%>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Visible="true" Text="Regestration Cleared List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvregalreadycleared" runat="server" Visible="true" AutoGenerateColumns="false" ToolTip="gvLoanView"
                            HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                            CssClass="gridview">
                            <Columns>
                                <asp:BoundField DataField="HandingOverEmpid" HeaderText="HandingOverEmp" />
                                <asp:BoundField HeaderText="TakingOverEmp" DataField="TakingOverEmpid" />
                                <asp:BoundField DataField="departmentname" HeaderText="Department Name" />
                                <asp:BoundField DataField="Clearenceitems" HeaderText="Cleared Item" />
                                <asp:BoundField DataField="createdon" HeaderText="Cleared Date" />
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
