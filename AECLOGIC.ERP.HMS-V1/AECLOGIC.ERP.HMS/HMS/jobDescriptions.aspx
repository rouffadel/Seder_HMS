<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jobDescriptions.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.jobDescriptions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);ddlTitle_hid
            document.getElementById('<%=ddlsemp_hid.ClientID %>').value = HdnKey;
        }
        function GetempTitleID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlTitle_hid.ClientID %>').value = HdnKey;
        }
        function CheckTextLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                alert(" Only " + long + " characters allowed");
            }
        }
      
        </script>
          <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
    <div id="dvadd" runat="server">
        <asp:Panel ID="pnltblEdit" runat="server" CssClass="box box-primary" Visible="true"
        Width="50%">
        <table>
            <asp:TextBox ID="txtdescid" runat="server" Width="150%" Visible="false"></asp:TextBox>
            <tr>
                <td><b>JobTitle With specialization: </b></td><td></td>
                    <td>
                <asp:DropDownList ID="ddDesignation" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td ><b>JobDescription:</b></td><td></td>
                    <td>
                        <%--<asp:TextBox ID="TextBox1" runat="server" Width="150%" onkeypress="return this.value.length<=250"  Height="80px"  style="resize:none" MaxLength="250" TextMode="MultiLine" ></asp:TextBox>--%>
    <asp:TextBox runat="server" ID="txtJobDescr" Height="80px" Width="150%" TextMode="MultiLine" onKeyUp="CheckTextLength(this,250)" onChange="CheckTextLength(this,250)"></asp:TextBox>
   
                    <cc1:TextBoxWatermarkExtender ID="txtwmeJobDescription" runat="server" WatermarkText="[Job Responsibilities [MAX 250 CHARACTERS ONLY] ]"
                        TargetControlID="txtJobDescr" >
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr><td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnADD" runat="server" Text="Save" CssClass="btn btn-success"  OnClick="btnADD_Click"/>
                </td>
            </tr>
        </table>
           </asp:Panel>
    </div>
     <div id="dvView" runat="server">
        <table  width="100%" >

            
               <tr>
            <td>
                <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="80%">
                    <Panes>
                        <cc1:AccordionPane ID="ViewAppLstAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;height:70%">
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                 <asp:Label ID="lblTitle" runat="server" Text="Desigination/Specialization"></asp:Label>
                                  <asp:HiddenField ID="ddlTitle_hid" runat="server" />
                                             <asp:TextBox ID="txtTitles"  Height="22px" Width="140px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListitle" ServicePath="" TargetControlID="txtTitles"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempTitleID">
                                            </cc1:AutoCompleteExtender>

                                                <asp:Label ID="lblJobDescriptionItem" runat="server" Text="Description"></asp:Label>
                                  <asp:HiddenField ID="ddlsemp_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite"  Height="22px" Width="140px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Description]">
                                            </cc1:TextBoxWatermarkExtender>
                                              &nbsp;&nbsp;&nbsp;&nbsp;
                                          
                                              &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                               Width="50"  />
                                        </td>
                                    </tr>
                                    
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </td>
        </tr>
              <table width="100%" >
                  <tr>
                <td>
                    <asp:GridView ID="gvjobterm" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="GhostWhite" runat="server" GridLines="Both" CssClass="gridview" Width="80%" 
                      OnRowCommand="gvjobterm_RowCommand" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"  
                        EmptyDataRowStyle-CssClass="EmptyRowData" >
                        <Columns>
                            <asp:BoundField DataField="Jobtitleid" HeaderText="Titleid" ItemStyle-Width="70px"/>
                 <asp:BoundField DataField="DescName" HeaderText="Desigination/Specialization" ItemStyle-Width="160px"/>
                        <asp:BoundField DataField="Name" HeaderText="Description" ItemStyle-Width="950px" />
                          
                            <asp:TemplateField>
                             <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("descid")%>'
                                                        CommandName="Edt" ></asp:LinkButton>

                             </ItemTemplate>
                                          
                                

                            </asp:TemplateField>
                      
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <table width="79%">
                     <td  style="height: 17px;width=10px">
                    <uc1:Paging ID="PageTax" Visible="true" runat="server" />

                </td>
                </table>
            </tr>
                     </table>
        </table>
      
    </div>
 </ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>

 </asp:UpdateProgress>
</asp:Content>
