<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlankFormatTemplate.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.HMS.BlankFormatTemplate" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Confirm() {
            var con = confirm("Do you want delete ?");
            if (con == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table id="tblView" width="100%" runat="server">
                            <tr>
                                <td>
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
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>Category :
                                                           <asp:DropDownList ID="ddlSCategory" runat="server" OnSelectedIndexChanged="ddlSCategory_SelectedIndexChanged"></asp:DropDownList>
                                                                DocumentName :
                                                           <asp:DropDownList ID="ddlDocName" runat="server"></asp:DropDownList>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                                    <asp:GridView ID="gvBlankDocs" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" GridLines="Both"
                                        EmptyDataText="No Records Found" OnRowCommand="gvBlankDocs_RowCommand" Width="100%" CssClass="gridview">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Doc ID" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proof" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A1" target="_blank" class="btn btn-primary" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>'
                                                        runat="server" visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>'>View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Dlt" CommandName="Delete" OnClientClick="javascript:return Confirm();"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExt" runat="server" Text='<%#Eval("Ext")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edt" CommandName="Edt" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="DownLoad" CssClass="btn btn-primary" CommandName="Download" CommandArgument='<%# Eval("Id") %>' OnClick="lnkDownload_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblAdd" width="100%" runat="server">
                            <tr>
                                <td>Category :
                            <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkAddCategory" runat="server" Text="Add New Category" OnClick="lnkAddCategory_Click"></asp:LinkButton>
                                    <asp:Label ID="lblCategaory" runat="server" Text="  Category Name :" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtCategory" runat="server" Height="21px" Visible="false">
                                    </asp:TextBox>
                                    <asp:Button ID="btnSaveCategory" runat="server" Text="Add" Visible="false" OnClick="btnSaveCategory_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>DocumentName : 
                            <asp:TextBox ID="txtDocName" runat="server" Height="21px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAdd" Text="Save" runat="server" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="gvBlankDocs" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
