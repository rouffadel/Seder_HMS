<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="EMPGradeConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.EMPGradeConfig"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:content id="Content2" contentplaceholderid="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">

        function ShowValidate() {


            //for Worksite
           <%-- if (!chkDropDownList('<%=ddlPositionCategory.ClientID%>', ' Position Category'))
                return false;--%>
            if (!Reval("<%=txtGrade.ClientID %>", " Grade", '[enter  Grade]'))
                return false;
            if (!Reval("<%=txtBasicFrom.ClientID %>", " Basic From (Month)", '[enter Basic From (Month)]'))
                return false;
            if (!Reval("<%=txtBasicTO.ClientID %>", "  Basic To (Month)", '[enter  Basic To (Month)]'))
                return false;
            if (!Reval("<%=txtHousing.ClientID %>", " Housing (Month)(%)", '[enter  Housing (Month)(%)'))
                return false;
            if (!Reval("<%=txtTpt.ClientID %>", "  Tpt (Month) (%)", '[enter    Tpt (Month) (%)'))
                return false;
            if (!Reval("<%=txtFood.ClientID %>", "    Food (Month) ", '[enter      Food (Month) '))
                return false;
            if (!Reval("<%=txtAL.ClientID %>", "     AL", '[enter      AL '))
                return false;
            if (!Reval("<%=txtMobile.ClientID %>", "    Mobile (Month) ", '[enter     Mobile (Month) '))
                return false;
            if (!Reval("<%=txtTickets.ClientID %>", "    Tickets (Year) ", '[enter     Tickets (Year) '))
                return false;
            if (!Reval("<%=txtVISA.ClientID %>", "    VISA (Year) ", '[enter     VISA (Year) '))
                return false;
            if (!Reval("<%=txtMedicalNos.ClientID %>", "     Medical Nos ", '[enter      Medical Nos '))
                return false;
           <%-- if (!chkDropDownList('<%=ddlEntitlement.ClientID%>', 'Entitlement'))
                return false;
            if (!chkDropDownList('<%=ddlMedical.ClientID%>', ' Medical'))
                return false;--%>
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlposition_hid.ClientID %>').value = HdnKey;
        }
        function GetMedicalID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlmedical_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
                                    <asp:Label runat="server" id="lblStatus" Text="" Font-Size="14px"></asp:Label>
    <table>
       
        <tr>
            <td>
            <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false"
                            Width="100%">
                <table id="tblNew" runat="server" visible="false">
                    <tr>
                        <td>
                        <asp:Button ID="btnback" runat="server" Text="Back"  CssClass="btn btn-primary" OnClick="btnback_Click" />
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="pageheader">
                            New EMP Grades 
                        </td>
                        <td class="pageheader">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Position Category<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPositionCategory" CssClass="droplist" runat="server" 
                                TabIndex="1"  >
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Grade<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGrade" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Basic From (Month)<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasicFrom" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Basic To (Month)<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasicTO" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Housing (Month)(%)<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHousing" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            TA (Month) (%)<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTpt" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Food (Month) <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFood" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Mobile (Month)<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                         <tr>
                            <td>
                                AL<span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAL" runat="server" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    <tr>
                        <td>
                            Entitlement<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEntitlement" CssClass="droplist" runat="server" 
                                TabIndex="2">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                         <tr>
                            <td>
                               Tickets (Year)<span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTickets" runat="server" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                         <tr>
                            <td>
                                VISA (Year)<span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVISA" runat="server" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    <tr>
                        <td>
                            Medical<span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMedical" CssClass="droplist" runat="server" 
                                TabIndex="3">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                            <td>
                                Medical Nos<span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMedicalNos" runat="server" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                 <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" />
                                &nbsp;
                            </td>
                        </tr>
                     
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-success" 
                                 OnClientClick="javascript:return ShowValidate()" AccessKey="s" 
                                TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblEdit" runat="server" visible="false">
                    <tr>
                        <td>
                  <cc1:Accordion ID="DesigAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="DesigAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td style="padding-right:0px">
                                                                        <asp:HiddenField ID="ddlposition_hid" runat="server" />
                                                                        <asp:TextBox ID="txtsearchposition" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="txtsearchposition"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtsearchposition"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Position Name]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                              <td style="padding-left:0px" >
                                                                        <asp:HiddenField ID="ddlmedical_hid" runat="server" />
                                                                        <asp:TextBox ID="txtsearchmedical" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptListMedical" ServicePath="" TargetControlID="txtsearchmedical"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetMedicalID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtsearchmedical"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Medical Name]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                                  <td style="padding-right:430px">
                                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" />
                                                                   </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblDesg" AutoPostBack="true" runat="server" Font-Bold="True" TabIndex="1"
                                                                    OnSelectedIndexChanged="rblDesg_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
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
                        <td>
                            <asp:GridView ID="gvEMPTrade" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                 EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview"
                                OnRowCommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead"     BorderWidth="2px"  Width="100%" >
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"
                                    CssClass="gridview" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Position Category" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate >
                                            <asp:Label ID="lblPositionCategory" runat="server" Text='<%#Eval("PositionCategory")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grade" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Basic From" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalaryFrom" runat="server" Text='<%#Eval("SalaryFrom")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Basic TO" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalaryTo" runat="server" Text='<%#Eval("SalaryTo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Housing (%)" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHRA" runat="server" Text='<%#Eval("HRA")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tpt (%)" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Food" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFood" runat="server" Text='<%#Eval("Food")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Mobile" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AL" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAnnualLeave" runat="server" Text='<%#Eval("AnnualLeave")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Entitlement" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFamilyEntitlement" runat="server" Text='<%#Eval("FamilyEntitlement")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Tickets" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTickets" runat="server" Text='<%#Eval("Tickets")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="VISA" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExitEntryVISA" runat="server" Text='<%#Eval("ExitEntryVISA")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Medical" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMedical" runat="server" Text='<%#Eval("Medical")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                     <asp:TemplateField HeaderText="MedicalNos" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMedicalNos" runat="server" Text='<%#Eval("MedicalNos")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Edit"  CssClass="anchor__grd edit_grd "  CommandArgument='<%#Eval("ID")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDel" runat="server"  CssClass="btn btn-danger"  Text='<%#GetText()%>' CommandArgument='<%#Eval("ID")%>'
                                                        CommandName="Del"></asp:LinkButton></ItemTemplate>
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
             <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="SalariesUpdPanel">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>



