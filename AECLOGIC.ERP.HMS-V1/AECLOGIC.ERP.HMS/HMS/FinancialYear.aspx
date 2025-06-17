<%@ Page Title="" Language="C#"   AutoEventWireup="True" 
    CodeBehind="FinancialYear.aspx.cs" Inherits="AECLOGIC.ERP.HMS.FinancialYear" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">

        function validatesave() {

            if (document.getElementById('<%=txtName.ClientID%>').value == "") {
                alert("Enter Year Name.!");
                return false;
            }

           <%-- if (!chkName('<%=txtName.ClientID%>', "Name", true, "")) {
                return false;
            }--%>
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == "") {
                alert("Please select from date");
                document.getElementById('<%=txtFromDate.ClientID%>').focus();
                return false;
            }
            if (!chkDate('<%=txtFromDate.ClientID %>', 'FromDate', '', '')) return false;
          
            if (document.getElementById('<%=txtToDate.ClientID%>').value == "") {
                alert("Please select to date");
                document.getElementById('<%=txtToDate.ClientID%>').focus();
                return false;
            }
            if (!chkDate('<%=txtToDate.ClientID %>', 'ToDate', '', '')) return false;

        }
        function Chkdate()
        {
            var fromdate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var arr = fromdate.split("/");
            if (arr[1] == 01 || arr[1] == 04) {
                //alert("01/" + arr[1] + "/" + arr[2]);
                document.getElementById('<%=txtFromDate.ClientID%>').value = "01/" + arr[1] + "/" + arr[2];
                if (arr[1] == 01)
                    document.getElementById('<%=txtToDate.ClientID%>').value = "31/12/" + arr[2];
                if (arr[1] == 04) {
                    var year = arr[2];
                    year =  parseInt(year) + 1;
                    document.getElementById('<%=txtToDate.ClientID%>').value = "31/03/" + year;
                }
            }
            else {
                alert('Please Select Month JAN or APR ');
                document.getElementById('<%=txtFromDate.ClientID%>').value = "";
                 document.getElementById('<%=txtToDate.ClientID%>').value = "";
            }
            
        
        }

       
    </script> 
     <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
       
      
        <tr>
            <td>

                <table id="tblNew" runat="server" visible="false">
                   
                    <tr>
                        <td>
                            <b>Name </b><span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>From Date</b><span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" AccessKey="t" 
                                TabIndex="2" ToolTip="[Alt+t OR Alt+t+Enter]" ></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FrmDtFilTxtExt" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtFromDate" ValidChars="/" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="txtDOB" Format="dd/MM/yyyy" OnClientDateSelectionChanged="Chkdate">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <b> To Date</b><span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" AccessKey="y" 
                                TabIndex="3" ToolTip="[Alt+y OR Alt+y+Enter]"  ></asp:TextBox>
                             <cc1:FilteredTextBoxExtender ID="ToDtFilTxtExt" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtToDate" ValidChars="/" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="txtDOB" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                OnClick="btnSubmit_Click" 
                                OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="4" 
                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tblEdit" runat="server" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvFinancialYear" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvFinancialYear_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHoliday" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("TODate")%>'></asp:Label>
                                        </ItemTemplate>
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
            </td>
        </tr>
    </table>
 </ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
</asp:Content>
