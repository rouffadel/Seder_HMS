<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="EmpWork.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpWork" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
<script language="javascript" type="text/javascript">
    function ValidEmpWork() {
        if (document.getElementById('<%=txtWork.ClientID%>').value == "") {
            alert("Please enter your work.!");
            document.getElementById('<%=txtWork.ClientID%>').focus();
            return false;
        } 
    }
</script>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                           
                             <tr>
                                 <td>
                                      <asp:MultiView ID="mainview" runat="server">
                    <asp:View ID="NewView" runat="server" >
    <asp:Panel ID="pnlCreatePostiong" runat="server" CssClass="box box-primary">

        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
           
            <tr>
                <td colspan="2" align="right">
                    <asp:Label ID="lblDate" runat="server" CssClass="pageheader"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="pageheader" colspan="2">
                    Employee Work:&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="1">
                                <b>Enter Work:</b>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtWork" runat="server" Rows="8" CausesValidation="True" TextMode="MultiLine"
                                    Width="350Px" BorderColor="#CC6600" BorderStyle="Inset" Height="80px" TabIndex="1"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtWork"
                                    WatermarkCssClass="Watermark" WatermarkText="[Enter Your Work!. Max 700 Characters only!]">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td width="450Px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 124px">
                            </td>
                            <td style="padding-left: 100Px" colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="javascript:return ValidEmpWork();"
                                    AccessKey="s" TabIndex="2" ToolTip="[Alt+s OR Alt+s+Enter]"  />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click"
                                    AccessKey="b" TabIndex="3" ToolTip="[Alt+b OR Alt+b+Enter]" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </asp:Panel>
                        </asp:View>
                                          <asp:View ID="Editview" runat="server">
    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvWork" AutoGenerateColumns="false" runat="server" Width="100%"
                    OnRowCommand="gvWork_RowCommand" EmptyDataText="No Records Found" CssClass="gridview">
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="EmpworkID">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkID" runat="server" Text='<%#Eval("EmpworkID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Task">
                            <ItemTemplate>
                                <asp:Label ID="lblTas" runat="server" Text='<%#Eval("Task")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                             
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandArgument='<%#Eval("EmpworkID")%>'  CommandName="Edt" CssClass="anchor__grd edit_grd"
                                    runat="server">Edit</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("EmpworkID")%>' CommandName="Dlt" CssClass="anchor__grd dlt"
                                    runat="server">Delete</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
                        <td style="height: 17px">
                            <uc1:paging ID="EmpWorkPaging" runat="server" />
                        </td>
                    </tr>
    </table>
                                              </asp:View>
                                           </asp:MultiView>
                                 </td>
                             </tr>
                         </table>
</ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
