<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="empdocuments.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.empdocuments" Title="Employee Documents" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceholder1" runat="Server">
     <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
  
    <script language="javascript" type="text/javascript">




        function Document() {
            var ex = document.getElementById('<%=fuResume.ClientID%>').value;
            if (ex) {
                if (!chkAddress('<%=txtDocName.ClientID%>', "Document", true, "")) {
                    return false;
                }
            }
            var ex = document.getElementById('<%=FileUpload1.ClientID%>').value;
            if (ex) {
                if (!chkAddress('<%=txtDocName2.ClientID%>', "Document", true, "")) {
                    return false;
                }
            }
            var ex = document.getElementById('<%=FileUpload2.ClientID%>').value;
            if (ex) {
                if (!chkAddress('<%=txtDocName3.ClientID%>', "Document", true, "")) {
                    return false;
                }
            }
            var ex = document.getElementById('<%=FileUpload3.ClientID%>').value;
            if (ex) {
                if (!chkAddress('<%=txtDocName4.ClientID%>', "Document", true, "")) {
                    return false;
                }
            }
            var ex = document.getElementById('<%=FileUpload4.ClientID%>').value;
            if (ex) {
                if (!chkAddress('<%=txtDocName5.ClientID%>', "Document", true, "")) {
                    return false;
                }
            }
        }

    </script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
       
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" CssClass="savebutton btn btn-success" OnClick="Button2_Click"
                    Text="Back" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="pageheader">
                            Employee Details
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="labName" runat="server" Text="Name" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="lablDept" runat="server" Text="Department" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="lablDes" runat="server" Text="Designation" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lbldesig" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="lablDoj" runat="server" Text=" Date of Joining " Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="lablTrade" runat="server" Text=" Trade" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblTrade" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="lblcntryname" runat="server" Text=" Country Name" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblcntryname1" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="lblpan" runat="server" Text="PAN/PASSPORT/ID CARD NO" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblpan1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="Label2" runat="server" Text="Iqama NO" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblIqama" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1">
                            <strong>
                                <asp:Label ID="Label3" runat="server" Text="Iqama Exp. Date" Width="10%"></asp:Label>
                            </strong>:
                            <asp:Label ID="lblIqmaExpDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td colspan="2" class="pageheader">
                            <asp:Label ID="lblNewDoc" runat="server" Text="New Document"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr id="trDocType" runat="server">
                        <td style="text-align: left">
                            <asp:Panel ID="Panel1"  CssClass="box box-primary" runat="server">
                                <asp:Label ID="Label1" runat="server" Text="Document Type:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlDocuments" runat="server" AutoPostBack="true" Width="130px"
                                    CssClass="droplist" >
                                </asp:DropDownList>
                                &nbsp;
                                <asp:LinkButton ID="lnkDoc" Text="Add New" runat="server" OnClick="lnkDoc_Click">
                                </asp:LinkButton>
                            </asp:Panel>
                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblstStatus" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rblstStatus_SelectedIndexChanged">
                                <asp:ListItem Text="Digital" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Scaned" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="savebutton btn btn-warning"
                                Width="100px" />
                            <div id="dvScaned" runat="server">
                                <table id="tblDoc" runat="server">
                                    <tr>
                                        <td>
                                            Document 1
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocName" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDocName"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Doc Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuResume" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 11px">
                                            Document 2
                                        </td>
                                        <td style="height: 11px">
                                            <asp:TextBox ID="txtDocName2" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDocName2"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Doc Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td style="height: 11px">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </td>
                                        <td style="height: 11px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Document 3
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocName3" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDocName3"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Doc Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload2" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Document 4
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocName4" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtDocName4"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Doc Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload3" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Document 5
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocName5" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDocName5"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Doc Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload4" runat="server" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSubmint" runat="server" Text="Submit" CssClass="savebutton btn btn-success" OnClick="btnSubmint_Click"
                                                OnClientClick="javascript:return Document();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       

         <tr>
                                <td>
                                    <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50" Width="40%"
                                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                Document Name:<asp:TextBox ID="txtSDocName" runat="server" width="120px" AccessKey="3" ToolTip='[Alt+3]' TabIndex="4"></asp:TextBox>
                                                               
                                                                &nbsp;
                                                                <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="savebutton btn btn-primary" Width="70px" TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                    OnClick="btnSearch_Click" />
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
        <tr>
            <td>
                <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                    GridLines="Both" Width="40%" CellPadding="4" HeaderStyle-CssClass="tableHead"
                    OnRowCommand="gvDocs_RowCommand" CssClass="gridview">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpDocID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EmpDocID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDocID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDocName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Document Type">
                            <ItemTemplate>
                                <asp:Label ID="lblDocType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="3%">
                            <ItemTemplate>
                                <a id="A2" visible='<%#IsEditable(DataBinder.Eval(Container.DataItem, "DocType").ToString()) %>'
                                    href='<%# String.Format("Appointment.aspx?id={0}&DocID={1}&DocName={2}&EmpDocID={3}",
                                    DataBinder.Eval(Container.DataItem, "EmpId"),
                                    DataBinder.Eval(Container.DataItem, "DocID"),
                                    DataBinder.Eval(Container.DataItem, "DocName"),
                                    DataBinder.Eval(Container.DataItem, "EmpDocID")) %>'
                                    runat="server" class="btn btn-success">Edit </a>
                               <a id="A1" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "DocType").ToString(),
                                DataBinder.Eval(Container.DataItem, "EmpId").ToString(),DataBinder.Eval(Container.DataItem, "DocID").ToString(),
                                DataBinder.Eval(Container.DataItem, "EmpDocID").ToString(),DataBinder.Eval(Container.DataItem, "value").ToString()) %>'
                                        runat="server" class="btn btn-primary">View </a>
                                <%--  <a id="A2" href ='<%# String.Format("Appointment.aspx?id=" + <%#Eval("")%> + "&DocID=" +  <%#Eval("")%>') %>  runat="server" >Edit</a>
                                <a id="A1" target="_blank"  href ='<%# String.Format("AppointmentPreview.aspx?id={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'  runat="server" >View</a>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" CommandName="Del" runat="server" CssClass="btn btn-danger" CommandArgument='<%# (DataBinder.Eval(Container.DataItem,"EmpDocID")) %>'> Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>


        <tr>
            <td>
            </td>
        </tr>

        <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmployeeChangesPaging" runat="server" />
                                </td>
                            </tr>

    </table>
        </ContentTemplate>
            <Triggers>
<asp:PostBackTrigger ControlID="btnSubmint" />
</Triggers>
</asp:updatepanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
     
 </asp:UpdateProgress>
</asp:content>
