<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="AttendanceDefault.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AttendanceDefault" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {


}

// ]]>
</script>

    <table width="100%" id="TABLE1" onclick="return TABLE1_onclick()">
        <tr>
            <td class="mainheader" align="center">
                Welcome To HR Management Systems(HMS)<br />
                <asp:Label ID="lblcompany" runat="server">
          
                </asp:Label>
            </td>
        </tr>
      <tr><td> <AEC:Topmenu ID="topmenu" runat="server" /></td></tr> 
        
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-size: 24pt; color: #cc4a01" align="center">
                प्रतिज्ञा
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <span style="font-size: 16pt; color: #cc4a01; line-height: 40px; padding: 3px 3px 3px 3px;
                    text-align: justify;">हम सभी प्रतिज्ञा करते हैं, की हम कार्य मैं सुरक्षा, स्वास्थ्य
                    तथा पर्यावरण के बचाव के प्रति समर्पित रहेंगे ! सभी सुरक्षा नियमों तथा बी एस एस की
                    दी गई कार्य-विधिओं का पालन करेंगे और ऐसा काम नहीं करेंगे जिससे बी एस एस के नाम पर
                    कोई आंच आए ! </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="subheader" align="center">
                Click Menu to begin your work
            </td>
        </tr>
         <tr>
        <td>
         &nbsp;
        </td>
     
     </tr>
         <tr>
            <td class="pageheader">
            Operation Schedule
                &nbsp;
            </td>
        </tr>
         <tr>
        <td>
         &nbsp;
        </td>
     
     </tr>
        <tr>
           <td>
               <table  Width="100%">
            <tr>
                <td align="center">
                    <asp:GridView ID="gdvOperations" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                        DataKeyNames="OpId" CellPadding="4" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDeleting="gdvOperations_RowDeleting"
                        OnRowEditing="gdvOperations_RowEditing" OnRowUpdating="gdvOperations_RowUpdating"
                        OnRowCommand="gdvOperations_RowCommand" OnRowCancelingEdit="gdvOperations_RowCancelingEdit" Width="100%">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>       
                            <asp:TemplateField HeaderText="Operations">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperation" runat="server" Text='<% #Eval("Operation") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOperation" TextMode="MultiLine" Width="250" Height="50" runat="server"
                                        Text='<% #Eval("Operation") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Update" ShowEditButton="True" ShowDeleteButton="True"
                                ShowHeader="True" />
                            <asp:TemplateField HeaderText="Add">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" CommandArgument='<% #Eval("OpId") %>'
                                        CommandName="Add"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                        <HeaderStyle CssClass="tableHead" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvOperations" runat="server" visible="false">
                        <table>
                            <tr>
                                <td valign="middle">
                                    Operation:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOperations" TextMode="MultiLine" Width="250" Height="50" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_OnClick" Text="Add" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
           
           </td>
        </tr>
        
        
        
    </table>
</asp:Content>

