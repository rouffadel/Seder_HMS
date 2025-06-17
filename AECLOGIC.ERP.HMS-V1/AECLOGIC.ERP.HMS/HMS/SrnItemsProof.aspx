<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="SrnItemsProof.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SrnItemsProof" Title="SDN Item Proof UpLoad" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
  <div class="InputHeaderLabel">
        Proof Upload
    </div>
    <div id="dvItems" runat="server" visible="false">
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvItemDetails" runat="server" DataKeyNames="SRNItemId" AutoGenerateColumns="false"
                        CssClass="gridview">
                        <Columns>
                            <asp:BoundField HeaderText="WO No & Name" DataField="WoName" />
                            <asp:BoundField HeaderText="Item" DataField="Item" />
                            <asp:BoundField HeaderText="UOM" DataField="Au_Name" />
                            <asp:BoundField HeaderText="WO_Qty" DataField="Qty" />
                            <asp:BoundField HeaderText="Balance_Qty" DataField="RelQty" />
                            <asp:TemplateField HeaderText="Upload Proof">
                                <ItemTemplate>
                                    <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" Text="Upload" runat="server" 
                        OnClick="btnUpload_OnClick" CssClass="savebutton btn btn-success" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

