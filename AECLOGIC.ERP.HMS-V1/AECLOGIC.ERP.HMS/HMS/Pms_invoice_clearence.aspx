<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pms_invoice_clearence.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Pms_invoice_clearence" MasterPageFile="~/Templates/CommonMaster.master" %>

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
                        <asp:Label ID="Label4" runat="server" Text=" Pending List" Font-Size="16px" ForeColor="blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvInVoice" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" CssClass="gridview"
                            EmptyDataText="No Records Found" Width="100%" GridLines="Both">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkhd" runat="server" AutoPostBack="false" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chlitm" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="Proofid" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Visible="false" Text='<%#Eval("proofid")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="true" HeaderText="Po Id" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpoID" runat="server" Text='<%#Eval("pono")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpoName" runat="server" Text='<%#Eval("poname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInVoiceNo" runat="server" Text='<%#Eval("billno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="180" />
                                    <HeaderStyle Width="180" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblinvoiceSiteName" runat="server" Text='<%#Eval("site_name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Handing Over Emp" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle />
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblscustody" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
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
