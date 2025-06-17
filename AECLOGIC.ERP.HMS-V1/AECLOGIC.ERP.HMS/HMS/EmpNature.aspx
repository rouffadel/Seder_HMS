<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpNature.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeaveProfile" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkName('<%=txtNature.ClientID%>', " Employee Nature", "true", "[ Employee Nature]"))
                return false;
            if (!chkName('<%=txtdays.ClientID%>', "Description", "true", "[ Employee Nature]"))
                return false;
            //              if (!chkNumber('<%=txtdays.ClientID%>', "No of Days","true","[days]"))
            //             return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td>
            <asp:MultiView ID="mainview" runat="server">
                            <asp:View ID="Newvieew" runat="server">
                <table  >
                    <tr>
                        <td colspan="2" class="pageheader" >
                            Employee Nature
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Employee Nature<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNature" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Description<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdays" runat="server" Width="300" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Status
                        </td>
                        <td>
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" 
                                TabIndex="3" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                OnClick="btnSubmit_Click" 
                                OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="4" 
                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
                <br />
              </asp:View>
                 <asp:View ID="EditView" runat="server">
                <table  >
                <tr>
               <td>
                <cc1:accordion ID="NewDeptAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" 
                        TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" 
                        SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="NewDeptAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rblstStatus" runat="server" RepeatDirection="Horizontal" TabIndex="1"  OnSelectedIndexChanged="rblstStatus_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:accordion>
               </td>
                </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="gridview"
                                 GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee Nature">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Nature")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("NoofWD")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" ControlStyle-ForeColor="Blue"  runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("NatureOfEmp")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" Text='<%#GetText()%>' CommandArgument='<%#Bind("NatureOfEmp")%>'
                                                                CommandName="Del" CssClass="anchor__grd dlt"></asp:LinkButton></ItemTemplate>
                                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
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
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="updpnl">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
</asp:Content>
