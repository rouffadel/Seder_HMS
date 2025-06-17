<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="WhohaveSystems.aspx.cs" Inherits="AECLOGIC.ERP.HMS.WhohaveSystems" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<AEC:Topmenu ID="topmenu" runat="server" />
    <script language="javascript" type="text/javascript">
       

        function InsUpdWhohavesys(chkid, EmpID) {

        var status =document.getElementById(chkid).checked;
        var Result = AjaxDAL.HR_InsUpdWhohavesys(status, EmpID);
               
        }


         </script>
<table style="border-right: #d56511 1px solid; vertical-align: top; border-top: #d56511 1px solid;
                                        border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;" border="0"
                                        cellpadding="3" cellspacing="3">
     <tr>
                                <td align="left" valign="top">
                                    <table style="border-right: #d56511 1px solid; vertical-align: top; border-top: #d56511 1px solid;
                                        border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;" border="0"
                                        cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td colspan="2">
                                                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                                    SelectedIndex="0">
                                                    <Panes>
                                                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                                Search Criteria
                                                            </Header>
                                                            <Content>
                                                               
                                                                        <table>
                                                                            <tr>
                                                                                <td style="text-align: left; width: 490px;">
                                                                                    <strong>Worksite</strong>&nbsp;<asp:DropDownList ID="ddlworksites" runat="server" AutoPostBack="True"
                                                                                         CssClass="droplist" Width="150px">
                                                                                    </asp:DropDownList>
                                                                                &nbsp;&nbsp;
                                                                                    <strong>Department</strong>&nbsp;&nbsp;<asp:DropDownList ID="ddldepartments" runat="server"
                                                                                        AutoPostBack="True" CssClass="droplist">
                                                                                    </asp:DropDownList>
                                                                                   
                                                                                  
                                                                                     &nbsp;&nbsp;
                                                                                    <asp:Button ID="btnSearch" ToolTip="Search selected worksite & Department peoples" runat="server" CssClass="savebutton"
                                                                                        Text="Search" OnClick="btnSearch_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                   
                                                            </Content>
                                                        </cc1:AccordionPane>
                                                    </Panes>
                                                </cc1:Accordion>
                                            </td>
                                        </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top;" colspan="2">
                                   <table style="border-right: #d56511 1px solid; vertical-align: top; border-top: #d56511 1px solid;
                                        border-left: #d56511 1px solid; border-bottom: #d56511 1px solid;" border="0"
                                        cellpadding="3" cellspacing="3"><tr><td>
                                            <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                                CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None"
                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" 
                                                onrowdatabound="gdvAttend_RowDataBound">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <Columns>

                                                <asp:TemplateField>
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                            <asp:Label ID="lblChkvalue" runat="server" Text='<%#Bind("SysStatus")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="worksite" HeaderText="WorkSite" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView></td>
                                            </tr>
                                            </table>
                                        
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>

</table>


</asp:Content>

