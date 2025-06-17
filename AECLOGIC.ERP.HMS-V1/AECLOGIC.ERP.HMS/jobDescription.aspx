<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jobDescription.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.jobDescription"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

                <AEC:Topmenu ID="topmenu" runat="server" />
          
    <div id="dvadd" runat="server">

        <table>
            <asp:TextBox ID="txtdescid" runat="server" Width="150%" Visible="false"></asp:TextBox>
            <tr>
                <td>JobTitleId:
                <asp:DropDownList ID="ddDesignation" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>JobDescription : 
            <asp:TextBox ID="txtJobDescr" runat="server" Width="150%"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtwmeJobDescription" runat="server" WatermarkText="[Job Description]"
                        TargetControlID="txtJobDescr">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnADD" runat="server" Text="Save" OnClick="btnADD_Click" />
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
                                                <asp:Label ID="lblJobDescriptionItem" runat="server" Text="JobDescriptionItem"></asp:Label>
                                 
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
                    <asp:GridView ID="gvjobterm" AutoGenerateColumns="false" runat="server" GridLines="Both" CssClass="gridview" Width="100%" 
                        OnRowCommand="gvjobterm_RowCommand" OnRowDataBound="gvjobterm_RowDataBound"  HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"  
                        EmptyDataRowStyle-CssClass="EmptyRowData">
                        <Columns>
                            <asp:BoundField DataField="Jobtitleid" HeaderText="Titleid" ItemStyle-Width="150px"/>
                 <asp:BoundField DataField="DescName" HeaderText="TitleName" ItemStyle-Width="250px"/>
                        <asp:BoundField DataField="Name" HeaderText="Description" ItemStyle-Width="150px"/>
                            <asp:TemplateField>
                             <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("descid")%>'
                                                        CommandName="Edt"></asp:LinkButton>

                             </ItemTemplate>
                                          
                                

                            </asp:TemplateField>
                      
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>

                <td style="height: 17px">
                    <uc1:Paging ID="PageTax" Visible="true" runat="server" />
                </td>
            </tr>
            
        </table>
    </div>
</asp:Content>
