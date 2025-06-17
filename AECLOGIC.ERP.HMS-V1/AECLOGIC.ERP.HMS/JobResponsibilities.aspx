<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobResponsibilities.aspx.cs" Inherits="AECLOGIC.ERP.HMS.JobResponsibilities" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">


                <AEC:Topmenu ID="topmenu" runat="server" />
          
    <div id="dvadd" runat="server">

        <table>
            <asp:TextBox ID="txtRespID" runat="server" Width="150%" Visible="false"></asp:TextBox>
            <tr>
                <td>JobTitleId:
                <asp:DropDownList ID="ddDesignation" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>JobResponsibilities :
    <asp:TextBox ID="txtJobresponce" runat="server" Width="150%"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtwmJobresponce" runat="server" WatermarkText="[Job Responsibilities]"
                        TargetControlID="txtJobresponce">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvView" runat="server">
        <table>
               <tr>
            <td>
                <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                    <Panes>
                        <cc1:AccordionPane ID="ViewAppLstAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                <asp:Label ID="lblJobResponsibilities" runat="server" Text="JobResponsibilities"></asp:Label>
                                 
                                             <asp:TextBox ID="txtSearchWorksite"  Height="22px" Width="140px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter JobItem Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="btnSearch_Click"
                                                TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="savebutton"
                                                Width="100px" />
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
                   <asp:GridView ID="gvjobterm" AutoGenerateColumns="false" runat="server" GridLines="Both" CssClass="gridview" Width="100%" OnRowCommand="gvjobterm_RowCommand" OnRowDataBound="gvjobterm_RowDataBound">
                     <Columns>
                         <asp:BoundField DataField="Jobtitleid" HeaderText="Titleid" ItemStyle-Width="150px"/>
                         <asp:BoundField DataField="RespDescription" HeaderText="TitleName" ItemStyle-Width="350px"/>
                         <asp:BoundField DataField="Name" HeaderText="Responsibility" ItemStyle-Width="150px"/>
                          <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("RespID")%>'
                                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                     </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>

                <td style="height: 17px">
                    <uc1:Paging ID="Pagetax" Visible="true" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

