<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EditNMRAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EditNMRAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function EndRequestEventHandler(sender, args) {
            if (document.getElementById("<%=hdn.ClientID%>").value == "1") {
                alert("Timings Updated Successfully");
                return true;
            }

        }
        function GetOutTime(chkid, outtime, txtid, InID) {
            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;

            if (document.getElementById(InID).value == "") {
                alert("Please Enter In Time");
                document.getElementById(chkid).checked = false;
                document.getElementById(txtid).value = "";
                document.getElementById(InID).focus();
                return false;
            }

            if (document.getElementById(chkid).checked) {
                document.getElementById(txtid).value = d.toLocaleTimeString();
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }

        function GetInTime(ddlid, outtime, txtid, txtOut, chkOut) {
            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(ddlid).selectedIndex == 1) {
                document.getElementById(txtid).value = d.toLocaleTimeString();
            }
            else {
                document.getElementById(txtid).value = "";
                document.getElementById(txtOut).value = "";
                document.getElementById(chkOut).checked = false;
            }
        }
  

      function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value =HdnKey;
        }
        function DeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
       
        <tr>
            <td>
                <asp:UpdatePanel ID="updmaintable" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 440px">
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" style="width: 100%; border: 0px;">
                                     
                                    </table>
                                    <br />
                                  
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                 <asp:UpdatePanel ID="updpgridview" runat="server">
                                                    <ContentTemplate>
                                    <table style="border-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid;
                                        border-bottom: #d56511 1px solid;" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td>
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
                                                                    <tr>
                                                                        <td style="height: 26px; text-align: left;">
                                                                            <strong>Worksite&nbsp;&nbsp;&nbsp; </strong>
                                                                                
                                                                              <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" AutoPostBack="true" OnTextChanged="GetWork" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID" >
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                               
                                                                           
                                                                        </td>
                                                                        <td align="right">
                                                                            &nbsp;&nbsp;<strong> Department&nbsp;&nbsp;&nbsp;
                                                                                

                                                                           <asp:HiddenField ID="ddlDepartment_hid" runat="server" />      
                                             <asp:TextBox ID="txtsearchdept"  AutoPostBack="true" OnTextChanged="Getdept" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_dept" ServicePath="" TargetControlID="txtsearchdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="DeptID" >
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtsearchdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                              
                                                                            </strong>&nbsp; Date::
                                                                            <asp:TextBox ID="txtDay" runat="server" AutoPostBack="true" OnTextChanged="txtDay_TextChanged"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtDayCalederExtender" runat="server" TargetControlID="txtDay"
                                                                                PopupButtonID="txtDOB">
                                                                            </cc1:CalendarExtender>
                                                                            &nbsp; Name::
                                                                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_empname" ServicePath="" TargetControlID="txtName"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtName"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Filter Name]">
                                           </cc1:TextBoxWatermarkExtender>

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
                                            <td style="text-align: left" colspan="2">
                                               <%-- <asp:UpdatePanel ID="updpgridview" runat="server">
                                                    <ContentTemplate>--%>
                                                        <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1" CssClass="gridview"
                                                            CellSpacing="1" DataKeyNames="NMRID"  GridLines="None" OnRowDataBound="gdvAttend_RowDataBound"
                                                            OnRowCommand="gdvAttend_RowCommand" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                                                           
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("NMRID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="71px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                                            AutoPostBack="true" CssClass="droplist" DataTextField="ShortName" DataValueField="ID"
                                                                            DataSource='<%# BindAttendanceType()%>'>
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="btnHid" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIN" runat="server" Text='<%#Bind("InTime")%>' Width="100"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkOut" runat="server" OnCheckedChanged="chkOut_CheckedChanged"
                                                                            AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("OutTime")%>'></asp:TextBox>
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
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" CommandArgument='<%#Bind("NMRID")%>' CssClass="btn btn-warning"
                                                                            CommandName="upd"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdn" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="updmaintable">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
            </td>
        </tr>
    </table>
</asp:Content>
