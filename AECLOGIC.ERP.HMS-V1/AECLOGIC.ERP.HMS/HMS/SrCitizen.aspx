<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="SrCitizen.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SrCitizen" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script language="javascript" type="text/javascript" src="JS/common.js"></script>

    <script language="javascript" type="text/javascript">

        function validatesave()
        {

//            if (!chkName('<%=txtAge.ClientID%>', "Name", "true", "[Short Name]"))
//             return false;

         if (!chkDropDownList('<%=ddlAssessmentYear.ClientID%>', "Assessment  Year", "", ""))
             return false;

             
            
             
        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
      <tr><td> <AEC:Topmenu ID="topmenu" runat="server" /></td></tr> 
        <tr>
            <td class="pageheader">
                Payroll >> Allowances
                <br />
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblNew" runat="server" visible="false">
                   <tr>
                        <td colspan="2" class="pageheader">
                             Allowances</td>
                    </tr>
                     <tr>
                        <td style="width: 124px" >
                           AssessmentYear<span style="color: #ff0000">*</span></td>
                        <td>
                        <asp:DropDownList ID="ddlAssessmentYear" runat="server" CssClass="droplist" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Gender
                        </td>
                        <td>
                             <asp:RadioButtonList ID="rdblstgender" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Age From<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAge" runat="server" Width="160px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" />
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tblEdit" runat="server" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvAllowances" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                 <asp:TemplateField HeaderText="Assemet Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("AssessmentYear")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Age From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("AgeFrom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("SrCitizenID")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField>
                                                <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDel" runat="server" Text="Delete" CommandArgument='<%#Eval("LeaveType")%>'  CommandName="Del"></asp:LinkButton></ItemTemplate>
                                       </asp:TemplateField>--%>
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
            </td>
        </tr>
    </table>

</asp:Content>

