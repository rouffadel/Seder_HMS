<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="SRNPreReq.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.SRNPreReq" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
<script language="javascript" type="text/javascript">

    function ClosePo(PONo, ItemId) {
        var answer = window.confirm("Total Indents Qty is Recieved.So WO# " + PONo + " will be Closed. Do you wish to Continue?");
        if (answer)
            SRNPreReq.MMS_CLOSEPO(PONo, ItemId);
        window.location = "SRNStatus.aspx?ID=3";
    }           
      
 function Validate(Id) {
            var Obj = getObj('<%=gvRecieve.ClientID%>');
            if (Id == "1") {
                for (i = 1; i < Obj.rows.length; i++) {
                    for (j = 0; j < Obj.rows[i].cells[4].childNodes.length; j++) {
                        if (Obj.rows[i].cells[4].childNodes[j].value == "") {
                            alert("Enter Received Qty");
                            Obj.rows[i].cells[4].childNodes[j].focus(); return false;
                        }

                    }
                }
                return true;
            }
            else {
                for (i = 1; i < Obj.rows.length; i++) {
                    for (j = 0; j < Obj.rows[i].cells[5].childNodes.length; j++) {
                        if (Obj.rows[i].cells[5].childNodes[j].value == "") {
                            alert("Enter Approved Qty");
                            Obj.rows[i].cells[5].childNodes[j].focus(); return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
  </script>
    <table>
       
        <tr>
            <td>
                <asp:GridView ID="gvRecieve" runat="server" CssClass="gridview" AutoGenerateColumns="false" 
                    HeaderStyle-CssClass="tableHead"  OnRowDataBound="gvRecieve_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="SRN Item Id">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSRNItemID" Text='<% #Eval("SRNItemID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGoods" Text='<% #Eval("ResourceName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="UOM" Text='<% #Eval("AU_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SRN Qty">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGDNQuantity" Width="60" Text='<% #Eval("ReqQty") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recvd Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblArvdQty" runat="server" Text='<% #Eval("RcvdQty")  %>' Visible="false"></asp:Label>
                                <asp:TextBox runat="server" ID="txtArrivedQuantity" Width="90" Text='<% #Eval("RcvdQty") %>'></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apprd Qty">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtRcvdQty" Width="90" Text='<% #Eval("AccptQty") %>'></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dist(Km)">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldistance" Text='<% #Eval("Distance") %>' Visible="false"></asp:Label>
                                 <asp:TextBox ID="txtKMs" runat="server" Text='<% #Eval("Distance") %>'></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fetxtAmount" runat="server" TargetControlID="txtKMs"
                                    FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:TextBox runat="server" TextMode="MultiLine" Width="150" Height="60" ID="txtComments"></asp:TextBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Received/Approved By">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlCheckedInBy" DataValueField="ID" DataTextField="Name"
                                    CssClass="droplist" DataSource='<%# BindDropDownList() %>'>
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="lseCheckedInBy" runat="server" PromptCssClass="PromptText" PromptText="type to search" 
                                     PromptPosition="Top" QueryPattern="Contains" TargetControlID="ddlCheckedInBy" IsSorted="true"></cc1:ListSearchExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="tableHead" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_OnClick"
                    Visible="true" CssClass="savebutton btn btn-success" />
            </td>
        </tr>
    </table>
</asp:Content>
