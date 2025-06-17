<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="ITSavings.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ITSavings" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validatesave() {
            if (!chkDropDownList('<%= ddlSections.ClientID%>', "Section ID", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlAssessmentYear.ClientID%>', "Assessment  Year", "", ""))
                return false;
            if (!chkName('<%=txtItemName.ClientID%>', "Item Name", true, "")) {
                return false;
            }
            if (!checkdecmial('<%=txtAmount.ClientID%>', "Amount", true, "")) {
                return false;
            }
        }

    </script>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
             
                <tr>
                    <td>
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="Button2_Click"
                            Text="Back" AccessKey="b" ToolTip="[Alt+b OR Alt+b+Enter]" TabIndex="1"/>
                    </td>
                </tr>
                <tr>
                    <td class="pageheader">
                        IT Savings Declaration
                        <br />
                        <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblNew" runat="server" visible="false">
                           <tr>
                                <td style="width: 124px">
                                    Financial Year<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAssessmentYear"  CssClass="droplist"  runat="server" OnSelectedIndexChanged="ddlAssessmentYear_SelectedIndexChanged" AutoPostBack="true"
                                        TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    SectionID<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSections"  CssClass="droplist"  runat="server" 
                                        TabIndex="2">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="width: 124px">
                                    Item Name<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtItemName" runat="server" TabIndex="4"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 124px">
                                    Amount<span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAmount" runat="server" TabIndex="5"></asp:TextBox>
                                </td>
                            </tr>

                             <tr>
                                    <td style="width: 124px">
                                        Proof
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FUDProof" runat="server" TabIndex="4" />
                                    </td>
                                </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                        OnClick="btnSubmit_Click" 
                                        OnClientClick="javascript:return validatesave();" AccessKey="s" 
                                        ToolTip="[Alt+s OR Alt+s+Enter]" TabIndex="6"/>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <b>Financial Year:</b>
                                                                <asp:DropDownList ID="ddlFinYear" AutoPostBack="true"  CssClass="droplist"  runat="server" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged" TabIndex="2">
                                                                </asp:DropDownList>
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
                                <td style="width: 100%">
                                    <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead" 
                                        CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Financial Year" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssessmentYear" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWages" runat="server" Text='<%#Eval("SectionName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCentage" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Proof">
                                                <ItemTemplate>
                                                    <a id="A1" runat="server" class="btn btn-primary" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "ITSID").ToString(),DataBinder.Eval(Container.DataItem, "Proof").ToString()) %>'
                                                        target="_blank">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd" runat="server" Text="Edit" CommandArgument='<%#Eval("ITSID")%>'
                                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
