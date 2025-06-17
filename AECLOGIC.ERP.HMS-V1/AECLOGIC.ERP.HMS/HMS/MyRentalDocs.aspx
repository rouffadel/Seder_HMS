<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="MyRentalDocs.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.MyRentalDocs" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

 <script language="javascript" type="text/javascript">
    
     function validsEmpRental() {


         

         //For amount
         if (!Reval('<%=txtAmount.ClientID%>', "amount", true, "")) {
             return false;
         }

         //txtFrom

         if (!Reval('<%=txtFrom.ClientID%>', "From date", true, "")) {
             return false;
         }

     }


    </script>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <asp:Panel ID="pnltblEdit" runat="server" CssClass="box box-primary" Visible="false"
        Width="50%">
        <table id="tblEdit" runat="server" visible="false" width="100%">
            
            <tr><td>Employee:</td><td> <asp:Label ID="lblemp" runat="server"  Forecolor="Green"  tooltip="It will be applicable to only for the current Logged-in user"  ></asp:Label> <span style="color: #ff0000">(It will be applicable to only for the current Logged-in user)</span> </td></tr>
            <tr>
                <td style="width: 170">
                    <b>
                        <asp:Label ID="lblEmpRenAmt" runat="server" Text="Amount:"></asp:Label>
                        <span style="color: #ff0000">*</span> </b>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" TabIndex="2"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="fteoutQty" runat="server" TargetControlID="txtAmount"
                        ValidChars="." FilterType="Numbers,custom" FilterMode="ValidChars">
                    </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>Proof:</b>
                </td>
                <td>
                    <asp:FileUpload ID="FileProof" CssClass="savebutton" runat="server" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>
                        <asp:Label ID="lblEmpFrm" runat="server" Text="From:"></asp:Label>
                        <span style="color: #ff0000">*</span> </b>
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" TabIndex="4"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrom"
                        PopupButtonID="txtFrom" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                        ID="Fl7" runat="server" TargetControlID="txtFrom" ValidChars="/">
                    </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>Upto:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtUpto" runat="server" TabIndex="5"></asp:TextBox><cc1:CalendarExtender
                        ID="CalendarExtender1" runat="server" TargetControlID="txtUpto" PopupButtonID="txtUpto"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b></b>
                </td>
                <td>
                    <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSave_Click"
                        OnClientClick="javascript:return validsEmpRental();" AccessKey="s" TabIndex="6"
                        ToolTip="[Alt+s OR Alt+s+Enter]" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Reset" OnClick="btnCancel_Click"
                        AccessKey="b" TabIndex="7" ToolTip="[Alt+b OR Alt+b+Enter]" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="tblView" runat="server" visible="false" width="100%">
        
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvView" Width="100%" CssClass="gridview" AutoGenerateColumns="false"
                    runat="server" OnRowCommand="gvView_RowCommand">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDocID" runat="server" Text='<%#Eval("HRDocID")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="EmpID" />
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Disignation" DataField="Designation" />
                        <asp:BoundField HeaderText="Department" DataField="DepartmentName" />
                        <asp:BoundField HeaderText="Site" DataField="Site_Name" />
                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                        <asp:BoundField HeaderText="From" DataField="FromDate1" />
                        <asp:BoundField HeaderText="Upto" DataField="ToDate1" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlnkproof" class="btn btn-primary" Enabled='<%#Visible(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                    NavigateUrl="#" OnClick='<%#ProofView(DataBinder.Eval(Container.DataItem, "EmpID").ToString(),DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                    runat="server">Proof</asp:HyperLink></ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="EmpRentalDocsPaging" runat="server" />
            </td>
        </tr>
    </table>
   </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>

