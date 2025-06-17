<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="AttendanceDevice.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.AttendanceDevice" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validate() {
            if (document.getElementById('<%=txtDevName.ClientID%>').value == "") {
                alert("Please Enter Device Name ");
                return false;
            }
            if (document.getElementById('<%=txtLoc.ClientID%>').value == "") {
                alert("Please Enter Location");
                return false;
            }

        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlbiometric_hid.ClientID %>').value = HdnKey;
        }
    </script>
 <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
    <div class="DivBorderSilver" runat="server">
        <table width="100%" >
          
                <tr>
                <td>
                    <asp:LinkButton ID="lnkAdd" Font-Bold="true" runat="server" OnClick="lnkAdd_Click1">Add </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkview" Font-Bold="true" runat="server" OnClick="lnkView_Click1">View</asp:LinkButton>
                </td>
                </tr>
            </table>        
        

            <table id="dvadd" runat="server" visible="false" >
              
                <tr>
                    <td align="left" style="width: 100px">
                        <asp:Label ID="lblAttDevName" runat="server" Text="Device Name"></asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDevName" runat="server" Width="316px" MaxLength="50"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 72px">
                        <asp:Label ID="lblAttDevLoc" runat="server" Text="Location"></asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLoc" runat="server" Width="316px" MaxLength="50"
                            TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 72px" valign="top">Status
                    </td>
                    <td>
                        <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active"
                            TabIndex="3" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 125Px" colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click" CssClass="btn btn-success"
                            OnClientClick="javascript:return validate();" Width="100px" AccessKey="s"
                            TabIndex="4" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    </td>
                </tr>
            </table>
         
    </div>
    <div id="dvView" runat="server" visible="false">
        <table>
            <tr>
                <td>
                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                        <Panes>
                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                ContentCssClass="accordionContent">
                                <Header>
                                    Search Criteria</Header>
                                <Content>
                                    <table cellpadding="0" cellspacing="0" style="width: 90%">
                                        <tr>
                                                   <td style="padding-right:0px">
                                                                        <asp:HiddenField ID="ddlbiometric_hid" runat="server" />
                                                                        <asp:TextBox ID="txtsearchbiomettric" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="txtsearchbiomettric"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtsearchbiomettric"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Biometric Name]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                                  <td style="padding-right:60px">
                                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" />
                                                                   </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblAttendanceDev" AutoPostBack="True" runat="server" Font-Bold="True"
                                                    OnSelectedIndexChanged="rblDesg_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="5" >
                                                    <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
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
                <td style="width: 70%">
                
                    <asp:GridView ID="gvAttenDev" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found" ForeColor="#333333"
                                        GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowCommand="gvWages_RowCommand"
                                        CssClass="gridview" Width="480">
                        <EmptyDataRowStyle CssClass="EmptyRowData"></EmptyDataRowStyle>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                        <Columns>
                            <asp:TemplateField HeaderText="Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%# Eval("DeviceName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" style="cursor:pointer " CssClass="anchor__grd edit_grd " Text="Edit" CommandArgument='<%# Eval("DeviceID") %>'
                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkStatus" runat="server" style="cursor:pointer " CssClass="anchor__grd dlt " Text='<% #Eval("Status")%>' CommandArgument='<%# Eval("DeviceID") %>'
                                        CommandName="Sta">
                                                            </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle CssClass="tableHead"></HeaderStyle>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 17px">
                    <uc1:Paging ID="AttendanceDevicePaging" runat="server" />
                </td>
            </tr>
        </table>
    </div>
 </ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" 
/>
    please wait...
  </ProgressTemplate>

 </asp:UpdateProgress>
</asp:Content>
