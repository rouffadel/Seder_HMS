<%@ Page Title="" Language="C#"   AutoEventWireup="True" 
    CodeBehind="Documents.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.Documents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"
    TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <asp:updatepanel runat="server">
         <ContentTemplate>
    <script language="javascript" type="text/javascript">

        function validate() {
            if (document.getElementById('<%=txtDocName.ClientID%>').value == "") {
                alert("Please Enter Document Name ");
                return false;
            }
       if (document.getElemedntById('<%=DocEditor.ClientID%>').value == "") {
                alert("Please Enter Document Text");
                return false;
            }
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDoc_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <div>
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" id="lblStatus" Text="" Font-Size="14px"></asp:Label>
                    <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="Back" 
                        OnClick="btnBack_Click" AccessKey="b" visible="false" TabIndex="1" 
                        ToolTip="[Alt+b OR Alt+b+Enter]" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID="AddView" runat="server">
                            <table id="View" runat="server">
                                <tr>
                                    <td>
                                        <table id="tblEmpID" runat="server" visible="false">
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Document Name:</b>
                                        <asp:TextBox ID="txtDocName" runat="server" Width="380px" TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Document Class:</b>
                                        <asp:DropDownList ID="ddlDocClss" CssClass="droplist" runat="server" 
                                            TabIndex="3">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="2" >
                                      <b> Note*</b>  Paste from Wordpad/Notepd (Don&#39;t use MS-Word ) to maintain stable format
                                  </td>
                                  
                               </tr>
                                <tr>
                                    <td colspan="2">
                                          <asp:TextBox runat="server" ID="DocEditor" TextMode="MultiLine" Columns="50" Rows="10" Text=" " Width="950px" Height="600px" /><br />
                                        <ajax:HtmlEditorExtender ID="htmlExtDocEditor" runat="server" EnableSanitization="false" 
                                           TargetControlID="DocEditor"    />
                                        <br />
                                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click"
                                            OnClientClick="javascript:return validate();" AccessKey="s" TabIndex="5" 
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="EditView" runat="server">
                            <table id="EditView1" runat="server"  width="95%">
                                <tr>
                                    <td>
                                        <cc1:Accordion ID="SalariesAccordion" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                            
                                            <panes>
                                            
                                            <cc1:AccordionPane ID="SalariesAccordionPane" runat="server" 
                                                ContentCssClass="accordionContent" HeaderCssClass="accordionHeader">
                                                    <header>
                                                        Search Criteria</header>
                                                    <content>
                                                        <table cellpadding="0" cellspacing="0" style="width: 70%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblClass" runat="server" Text="Document Class:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlDocClass" runat="server" AccessKey="1" 
                                                                        CssClass="droplist" TabIndex="2" ToolTip="[Alt+1]">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                                                    <asp:Label ID="lblDocName" runat="server" Text="Document Name:"></asp:Label>
                                                                </td>
                                                                <td>
                                                               

                                                                     <asp:HiddenField ID="ddlDoc_hid" runat="server" />
                                                                           <asp:TextBox ID="SearchtxtDocName" runat="server" AccessKey="2" TabIndex="3" 
                                                                        ToolTip="[Alt+2]"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="SearchtxtDocName"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="SearchtxtDocName"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter DocName]"></cc1:TextBoxWatermarkExtender>
                                                                </td>
                                                               
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbShowActive" AutoPostBack="true" runat="server" Checked="true" ToolTip="[Alt+1]"
                                                                    GroupName="show" Text="Active" OnCheckedChanged="rbShowActive_CheckedChanged" Style="font-weight: bold" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbShowInactive" AutoPostBack="true" runat="server" GroupName="show"
                                                                    Text="Deleted" OnCheckedChanged="rbShowActive_CheckedChanged" Style="font-weight: bold" />
                                                                    
                                                                </td>
                                                                 <td>
                                                                     &nbsp;&nbsp; &nbsp;
                                                                    <asp:Button ID="btnSearch" runat="server" AccessKey="i" 
                                                                        OnClick="btnSearch_Click" TabIndex="4" Text="Search" 
                                                                        ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </content>
                                                </cc1:AccordionPane>
                                            
                                            </panes>
                                            
                                        </cc1:Accordion>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdvDoc" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                            Width="95%" CellPadding="4" OnRowCommand="gdvDoc_RowCommand" HeaderStyle-CssClass="tableHead"
                                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview" >
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  />
                                            <Columns>
                                                <asp:TemplateField HeaderText="DocName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="tableHead" />
                                                </asp:TemplateField>
                                                <asp:TemplateField >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStatus" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("DocId")%>'
                                                            CommandName="Status" Text='<%# Eval("Status")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="tableHead" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd " CommandArgument='<%# Eval("DocId")%>'
                                                            CommandName="Edt" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <HeaderStyle CssClass="tableHead" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 17px">
                                        <uc1:Paging ID="DocumentsPaging" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </div>
             </ContentTemplate>
    </asp:updatepanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
