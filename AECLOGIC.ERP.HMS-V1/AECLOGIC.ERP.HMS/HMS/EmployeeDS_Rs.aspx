<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDS_Rs.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeDS_Rs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <asp:updatepanel runat="server" ID="UpdatePanel2">
  <ContentTemplate>
     <div id="dvView" runat="server">
         <table style="width:70%">
             <tr>
                 <td>
                     <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="true" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                    <Panes>
                        <cc1:AccordionPane ID="ViewAppLstAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Employee Details</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                <asp:Label ID="lblE" runat="server" Text="Employee Name:- "></asp:Label>
                                                <asp:Label ID="lblEmpname" runat="server"></asp:Label>
                                            </td>
                                            
                                            <td colspan="2">
                                            <b>
                                                <asp:Label ID="Label1" runat="server" Text="Designation:- "></asp:Label>
                                                <asp:Label ID="lblDesi" runat="server" ></asp:Label>
                                            </td>

                                        <td colspan="2">
                                            <b>
                                                <asp:Label ID="Label2" runat="server" Text="Department:- "></asp:Label>
                                                <asp:Label ID="lblDept" runat="server" ></asp:Label>
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
                     <asp:GridView ID="gvjobterm" AutoGenerateColumns="false" runat="server"
                          GridLines="Both" CssClass="gridview" Width="100%">
                         <Columns>
                              <asp:BoundField DataField="empid" HeaderText="Employee ID" Visible="false" />
                            
                             <asp:BoundField DataField="jobdescription" HeaderText="Job Description" />
                             <asp:BoundField DataField="respdescription" HeaderText="Job Responsibilities" />
                             
                         </Columns>
                     </asp:GridView>
                 </td>
             </tr>
             <tr>
                
             <tr>
                 <td>
                     <asp:Button ID="btnSearch" runat="server" Text="Search" Visible="false" CssClass="btn btn-primary"/>
                 </td>
             </tr>
              <tr>
                            <td colspan="2" style="height: 17px">
                                <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" />
                            </td>
                        </tr>
         </table>
         </div>
    </ContentTemplate>
</asp:updatepanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
    </asp:Content>
