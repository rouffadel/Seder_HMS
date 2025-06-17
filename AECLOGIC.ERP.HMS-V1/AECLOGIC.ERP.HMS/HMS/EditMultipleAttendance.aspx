<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="EditMultipleAttendance.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.EditMultipleAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <AEC:Topmenu ID="topmenu1" runat="server" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">


                <tr>
                    <td style="height: 26px; text-align: left;">
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                                            Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <%-- <tr>
                                                                   <td>
                                                                   Worksite&nbsp;<asp:DropDownList 
                                                                           ID="ddlWorksite" runat="server" AutoPostBack="True"
                                                                            CssClass="droplist"
                                                                            AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" 
                                                                           onselectedindexchanged="ddlWorksite_SelectedIndexChanged1">
                                                                        </asp:DropDownList>
                                                                  
                                                                   Department&nbsp;<asp:DropDownList ID="ddlDepartment"
                                                                            runat="server" AutoPostBack="True" CssClass="droplist" 
                                                                           onselectedindexchanged="ddlDepartment_SelectedIndexChanged1" >
                                                                       <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                 
                                                                   EmpNature <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist" 
                                                                   OnSelectedIndexChanged="ddlEmpNature_SelectedIndexChanged" AutoPostBack="True">
                                                                            </asp:DropDownList>
                                                                   </td>
                                                                
                                                               </tr>--%>
                                            <tr>
                                                <td>EmpID &nbsp;<asp:DropDownList ID="ddlEmp" runat="server" CssClass="droplist">
                                                </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ListSearchExtender3" QueryPattern="Contains" runat="server" TargetControlID="ddlEmp" PromptText="Type to search..." PromptCssClass="PromptText" PromptPosition="Top" IsSorted="true"></cc1:ListSearchExtender>

                                                    Date :
                                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                                        PopupButtonID="txtDOB" Format="dd MMM yyyy"></cc1:CalendarExtender>


                                                    &nbsp; Month 
                                                                  <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist">
                                                                      <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                      <asp:ListItem Value="1">January</asp:ListItem>
                                                                      <asp:ListItem Value="2">February</asp:ListItem>
                                                                      <asp:ListItem Value="3">March</asp:ListItem>
                                                                      <asp:ListItem Value="4">April</asp:ListItem>
                                                                      <asp:ListItem Value="5">May</asp:ListItem>
                                                                      <asp:ListItem Value="6">June</asp:ListItem>
                                                                      <asp:ListItem Value="7">July</asp:ListItem>
                                                                      <asp:ListItem Value="8">August</asp:ListItem>
                                                                      <asp:ListItem Value="9">September</asp:ListItem>
                                                                      <asp:ListItem Value="10">October</asp:ListItem>
                                                                      <asp:ListItem Value="11">November</asp:ListItem>
                                                                      <asp:ListItem Value="12">December</asp:ListItem>
                                                                  </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="droplist">
                                                    </asp:DropDownList>



                                                    <asp:Button ID="btnMonthReport" ToolTip="Generate Month Report" runat="server" CssClass="btn btn-primary"
                                                        OnClick="btnMonthReport_Click" Text="Month Report" />
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
                        <asp:GridView ID="gvMultipleAtt" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                            EmptyDataText="No Records Found" OnRowCommand="gdvAttend_RowCommand">
                            <Columns>

                                <asp:BoundField DataField="Name" HeaderText="EmpName" />
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="In Time">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIN" runat="server" Width="100" Text='<%#Bind("Intime")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Out Time">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("Outtime")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="18px" TextMode="MultiLine" Width="200px"
                                            Text='<%#Bind("Remarks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Update" CssClass="btn btn-primary" CommandName="save" CommandArgument='<%#Bind("MID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </td>

                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
