<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkcustodi.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Checkcustodi" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function showAppcust(EmpID) {
            //  window.showModalDialog("../HMS/clearenceview.aspx?Empid=" + EmpID , "", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
        }
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
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text=" Clearance List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <asp:TextBox ID="txtname" Visible="false" runat="server"></asp:TextBox>
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
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Pending List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:GridView ID="gvmms" runat="server" HeaderStyle-CssClass="gv" EmptyDataText="No records Found"
                            AlternatingRowStyle-BackColor="GhostWhite" AutoGenerateColumns="false" DataKeyNames="ResourceID,EmpId" CssClass="gridview" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkheadmms" runat="server" AutoPostBack="false" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemmms" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ResourceID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResourceID" runat="server" Text='<%#Bind("ResourceID")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Name" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblresourcename" runat="server" Text='<%#Bind("ResourceName")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="BalQty" HeaderText="Balance Qty" />
                                 <asp:BoundField DataField="Au_Name" HeaderText="UOM" />
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
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Save"
                            OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-primary" Text="Back"
                            OnClick="btnback_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text=" Cleared List" Font-Size="16px" ForeColor="blue"></asp:Label>
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
