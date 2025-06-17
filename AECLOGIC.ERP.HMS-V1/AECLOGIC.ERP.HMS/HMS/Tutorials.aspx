<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="Tutorials.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Tutorials" %>

<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="updTutorials" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="pageheader">
                        Tutorials
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvTutirials" runat="server" EmptyDataText="No Tutorials Found"
                            CssClass="gridview" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Tutorial ID">
                                    <ItemStyle Width="20" HorizontalAlign="Left" />
                                    <HeaderStyle Width="20" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("TutorialId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tutorial">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkTutorialView" runat="server" Text='<%#Eval("TutorialName") %>'
                                            onclick='<%# ShowVideos(DataBinder.Eval(Container.DataItem,"Path").ToString())%>'
                                            CommandName="View" Target="_blank" style="cursor:pointer"></asp:HyperLink>
                                        <%-- <asp:LinkButton ID="lnkTutorialView" runat="server" Text='<%#Eval("TutorialName") %>' 
                                CommandArgument='<%# Eval("Path")%>'
                                    CommandName="View"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                    <ItemStyle Width="20" HorizontalAlign="Left" />
                                    <HeaderStyle Width="20" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("TutorialId") %>'
                                            CommandName="Del"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
