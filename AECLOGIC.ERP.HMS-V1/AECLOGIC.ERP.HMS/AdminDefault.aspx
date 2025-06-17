<%@ Page Title="HMS::Home" Language="C#" MasterPageFile="~/Templates/CommonMaster.master"  Theme="Profile1" AutoEventWireup="True"
    CodeBehind="AdminDefault.aspx.cs" Inherits="AECLOGIC.ERP.COMMON.AdminDefault" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    
      <%--  <cc1:Seadragon ID="Seadragon" Width="1100" Height="500" runat="server" SourceUrl="../CommonFiles/SeaDragOn/ERP.xml"
            CssClass="seadragon">
        </cc1:Seadragon>--%>
        <div>
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="1020px" height="580px">
    <param name="source" value="../MAN/ClientBin/TaskProgress.xap"/>
    <%--<param name="source" value="AEC Publish/ClientBin/AEC Practice.xap"/>--%>
    <param name="onError" value="onSilverlightError" />
    <param name="background" value="white" />
    <param name="minRuntimeVersion" value="4.0.50826.0" />
    <param name="autoUpgrade" value="true" />
          <param name="initParams"  id="initParamsobj" runat="server"  value="FName=0,FID=0,PID=0" />
    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0" style="text-decoration:none">
      <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/>
    </a>
     </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe>
    </div>
        <%-- <cc1:DragPanelExtender ID="dPanel" TargetControlID="divAttendance" runat="server">
        </cc1:DragPanelExtender>
       <asp:Panel ID="divAttendance" runat="server" Style="position: absolute; width: 300px;
            font-size: 80%; top: 20%; left: 18%;  padding-right:10px">
            <div class="DivBorderMaroon" style="background-color:InfoBackground;">
                <table id="tblChequeDashBoard" cellspacing="0" width="100%" cellpadding="0" border="0">
                    <tr style="background-color: Maroon; color: White; font-weight: bold; cursor:move;">
                        <td style="text-align: Center; width: 78px;">
                            Attendance
                        </td>
                        <td style="text-align: right">
                            <span style="cursor:pointer" onclick="javascript:return $get('<%=divAttendance.ClientID %>').style.display='none';">
                                <b>&nbsp;X&nbsp;</b></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 78px;" >
                            <b>Working</b>
                        </td>
                        <td style="width: 50px;">
                            <asp:Label ID="lblpresent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 78px" >
                            <b>Idle</b>
                        </td>
                        <td style="width: 50px;">
                            <asp:Label ID="lblabsent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 78px" >
                            <b>Break Down</b>
                        </td>
                        <td style="width: 50px;">
                            <asp:Label ID="lblBreakD" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 78px" >
                            <b>Total</b>
                        </td>
                        <td style="width: 50px;">
                            <asp:Label ID="lbltot" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>--%>
  
    <table id="tblToDo" visible="false" runat="server" width="100%">
            <tr><td colspan="1" align="left" class="pageheader">To-Do List</td></tr>
            <tr><td>
                <asp:GridView ID="gvTodoList" runat="server" AutoGenerateColumns="false" 
                    EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found" 
                    ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" 
                   
                   Width="85%" onrowcommand="gvTodoList_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ToDoID" HeaderText="TaskID" Visible="true" />
                        <asp:BoundField DataField="Subject" HeaderStyle-Width="20%" HeaderText="Task Subject" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                        <asp:BoundField DataField="Complete" HeaderText="Complete(%)" Visible="true"/>
                          <asp:BoundField DataField="Priority" HeaderText="Priority" Visible="true"/>
                        <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" />
                        <asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" 
                                    CommandArgument='<%#Eval("ToDoID")%>' CommandName="view">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" runat="server" 
                                    CommandArgument='<%#Eval("ToDoID")%>' CommandName="Del">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView> </td></tr>
               
            
            </table>
            <table width="100%">
<tr><td align="left" colspan="2">
    <asp:GridView ID="gvView" AutoGenerateColumns="false" CssClass="gridview" Width="50%" runat="server">
    <Columns>
    <asp:TemplateField HeaderText="ToDoList"><ItemTemplate>
        <asp:LinkButton ID="lnkToDoList" runat="server"></asp:LinkButton> </ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText="Self Assignments"><ItemTemplate>
        <asp:LinkButton ID="lnkSA" runat="server"></asp:LinkButton> </ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText="Not Started"><ItemTemplate>
        <asp:LinkButton ID="lnkNS" runat="server"></asp:LinkButton> </ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText="Over Due"><ItemTemplate>
        <asp:LinkButton ID="lnkOD" runat="server"></asp:LinkButton> </ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText="Not Updating"><ItemTemplate>
        <asp:LinkButton ID="lnkNU" runat="server"></asp:LinkButton> </ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText="Completed"><ItemTemplate>
        <asp:LinkButton ID="lnkCom" runat="server"></asp:LinkButton> </ItemTemplate></asp:TemplateField>
    </Columns>
    </asp:GridView>
</td></tr>

</table>
<table width="100%">
            <tr>
                <td  colspan="1" align="left">
                    <asp:GridView ID="gdvOperations" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                        DataKeyNames="OpId" CellPadding="4" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDeleting="gdvOperations_RowDeleting"
                        OnRowEditing="gdvOperations_RowEditing" OnRowUpdating="gdvOperations_RowUpdating"
                        OnRowCommand="gdvOperations_RowCommand" OnRowCancelingEdit="gdvOperations_RowCancelingEdit" Width="90%">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
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
                                ShowHeader="True" >
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="60px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Add">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" CommandArgument='<% #Eval("OpId") %>'
                                        CommandName="Add"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" />
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
                <td colspan="1" style="padding-left:200Px" align="left">
                    <div id="dvOperations" runat="server" visible="false">
                        <table >
                            <tr>
                                <td colspan="1" valign="middle">
                                    <b>Operation:</b>
                                </td>
                                <td colspan="1">
                                    <asp:TextBox ID="txtOperations" TextMode="MultiLine" Width="250" Height="50" runat="server" BorderStyle="Outset" BorderColor="#CC6600"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" align="center">
                                    &nbsp;
                                </td>
                                 <td colspan="1" style="padding-left:50Px">
                                  <asp:Button ID="btnAdd" runat="server" CssClass="savebutton" OnClick="btnAdd_OnClick" Text="Add" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
</asp:Content>
