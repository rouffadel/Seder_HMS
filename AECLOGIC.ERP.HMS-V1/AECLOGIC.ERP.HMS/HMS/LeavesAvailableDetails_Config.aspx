<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeavesAvailableDetails_Config.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.HMS.LeavesAvailableDetails_Config" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Validate() {
            var dtDate = document.getElementById("txtGrantedFrom").value; // tbDate = name of text box
            var currentDate = getCalendarDate()
            if (dtDate > currentDate) {
                strErrMsg = strErrMsg + "Select proper Date.. \n";
                return (strErrMsg);
            }
        }
        function ViewTypeLeaves() {
            window.open("TypeOfLeaves.aspx", '_blank');
            return false;
        }
        function ViewLeaveEntitlement() {
            var GV = document.getElementById('<%=lblGrade.ClientID%>').innerHTML;

            window.open("LeaveEntitlement.aspx?G=" + GV, '_blank');
            return false;
        }
        function ViewSalaryRev() {
            var GV = document.getElementById('<%=txtempID.ClientID%>').value;

            window.open("EmpSalHikes.aspx?EmpID=" + GV, '_blank');
            return false;
        }
        function ViewShowGrades() {


            window.open("EMPGradeConfig.aspx", '_blank');
            return false;
        }
        function ViewShowEncashment_AL() {
            window.open("Encashment_AL.aspx", '_blank');
            return false;
        }

    </script>
    <ajax:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <table style="width: 98%;">
               
                <tr>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlYear" runat="server" TabIndex="3" AccessKey="y" ToolTip="[Alt+y]" CssClass="droplist"
                            Width="100px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Visible="false" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Button ID="btnsave" runat="server" Text="Refresh" CssClass="btn btn-primary" OnClick="btnsave_Click"
                            AccessKey="s" TabIndex="8" ToolTip="[Alt+s OR Alt+s+Enter]" /></td>


                    <td></td>
                    td></td>
                    <td>
                        <asp:Button ID="btnback" Text="Back" runat="server" OnClick="btnback_Click" CssClass="btn btn-primary" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
             
                <tr style="font-size: 14px;">
                    <td style="width: 20%;">
                        <b>Name </b>
                        <asp:Label ID="lblEMPName" runat="server" Font-Bold="true" ForeColor="Navy" Text="" />
                    </td>
                    <td style="width: 15%;">
                        <b>Date of Joining :</b>
                        <asp:Label ID="lblEMPDOJ" runat="server" Font-Bold="true" ForeColor="Navy" Text="" />
                    </td>
                    <td style="width: 15%;">
                        <b>LVRD </b>
                        <asp:Label ID="lblLVRD" runat="server" Font-Bold="true" ForeColor="Navy" Text="" />
                        <asp:LinkButton ID="lnkLVRD" runat="server" OnClientClick="javascript:return ViewShowEncashment_AL();" Text="Show Grades" Width="100" />

                    </td>
                    <td style="width: 12%;">
                        <b>A/C Start Date </b>
                        <asp:Label ID="lblActDt" runat="server" Font-Bold="true" ForeColor="Navy" Text="" />
                        <asp:LinkButton ID="lnkActDt" runat="server" OnClientClick="javascript:return ViewShowEncashment_AL();" Text="Show Grades" Width="100" />

                    </td>
                    <td style="width: 10%;"><b>Grade </b>
                        <asp:Label ID="lblGrade" runat="server" Font-Bold="true" ForeColor="Navy" Text="" />
                        <asp:LinkButton ID="btnTypeLea" runat="server" OnClientClick="javascript:return ViewShowGrades();" Text="Show Grades" Width="100" />
                        <asp:HiddenField ID="txtempID" runat="server" />
                        <asp:HiddenField ID="txtempname_hid" runat="server" />
                    </td>
                    <td style="width: 20%;">
                        <b>Calculation is based on </b>
                        <asp:Label ID="lblCalByPD" runat="server" Font-Bold="true" ForeColor="Navy" Text="" />
                    </td>
                </tr>
             
                <tr>
                    <td colspan="6" style="margin-left: 40px">
                        <asp:DataList ID="dtlWOProgress" runat="server" HeaderStyle-CssClass="datalistHead"
                            Width="100%" 
                            OnItemDataBound="dtlWOProgress_ItemDataBound" CssClass="gridview">

                            <ItemTemplate>
                                <div class="DivBorderOlive" style="margin-bottom: 20px">
                                    <table style="width: 100%; background-color: #efefef;">

                                        <tr>
                                            <td style="width: 30%;"></td>
                                            <td style="width: 40%;">
                                                <b>Max Eligibility :</b>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='<%#Bind("MaxLeaveElgYear")%>' />
                                            </td>
                                            <%-- <td style="width: 30%;"></td>--%>
                                            <td style="width: 30%;">
                                                <b>Is C/Fwd :</b>
                                                <asp:Label ID="lblCFWD" runat="server" Font-Bold="true" ForeColor="Navy" Text='<%#Bind("IsCFrwd")%>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"></td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GVVoucherDetails" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                        AutoGenerateColumns="false" DataSource='<%#BindTransdetails(Eval("LeaveType").ToString())%>'
                                        OnRowCommand="gvEditTasks_RowCommand" OnRowDataBound="gvEditTasks_RowDataBound" ShowFooter="true"
                                        HeaderStyle-CssClass="tableHead" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Leave Type" ItemStyle-Width="180px" />
                                            <asp:BoundField DataField="ActionDate" HeaderText="Date" ItemStyle-Width="80px" />
                                         
                                            <asp:TemplateField HeaderText="Cr">
                                                <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCr" runat="server" CssClass="droplist" Style="text-align: right;"
                                                        Text='<%#Eval("Cr") %>' Width="60px"></asp:TextBox>
                                                    <asp:HiddenField ID="HidOpenID_IsCr" runat="server" Value='<%#Eval("IsCFrwd") %>' />
                                                    <asp:HiddenField ID="HidOpenID" runat="server" Value='<%#Eval("OpenID") %>' />
                                                    <asp:Label ID="txtCr_lbl" Visible="false" runat="server" Style="text-align: right;"
                                                        Text='<%#Eval("Cr") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCrTotal" runat="server" Text='<% #TotCrBal.ToString()%>' Font-Bold="true"
                                                        Width="60px" Style="text-align: right;"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr">
                                                <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDr" runat="server" Style="text-align: right;"
                                                        Text='<%#Eval("Dr") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblDrTotal" runat="server" Text='<% #TotDrBal.ToString()%>' Font-Bold="true"
                                                        Width="60px" Style="text-align: right;"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtActionDt" runat="server" TabIndex="4" Width="80px"></asp:TextBox>
                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="320px" />
                                            <asp:TemplateField HeaderText="">
                                                <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("LTID") %>'
                                                        CommandName="Edt" Text="Update" CssClass="btn btn-success"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table style="width: 100%; background-color: #efefef;">

                                        <tr>
                                            <td style="width: 30%;"></td>
                                            <td style="width: 40%;">
                                                <b>Closing Balance :</b>
                                                <asp:Label ID="lblTask" runat="server" Font-Bold="true" Text='<%#Bind("Bal")%>' />
                                            </td>
                                            <td style="width: 30%;"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"></td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Content>
