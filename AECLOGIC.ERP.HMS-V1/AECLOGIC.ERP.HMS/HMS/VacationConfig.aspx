<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="VacationConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.VacationConfig" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function UpdateVS(ID, ctrl) {
           
            var Result = AjaxDAL.UpdateVS(ID, ctrl.checked);

        }

        function UpdateFS(ID, ctrl) {
            var Result = AjaxDAL.UpdateFS(ID, ctrl.checked);

        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
         <ContentTemplate>
        
        <asp:GridView ID="GridAccess" AutoGenerateColumns="false" runat="server" BorderColor="#FF9900"
            BorderStyle="Groove" CssClass="gridview" GridLines="Vertical" Width="50%" OnRowDataBound="GridAccess_RowDataBound">
            <columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="lblMenuID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VS">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckVS" runat="server" Text="VS"
                        Checked='<%#Bind("VS")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FS">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckFS" runat="server" 
                        Text="FS" Checked='<%#Bind("FS")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
        </columns>
        </asp:GridView>

             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
