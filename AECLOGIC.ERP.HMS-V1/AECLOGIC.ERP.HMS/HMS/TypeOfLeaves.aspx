<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="TypeOfLeaves.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TypeOfLeaves" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkName('<%=txtName.ClientID%>', "Name", "true", "[Short Name]"))
                return false;
            if (!chkName('<%=txtSName.ClientID%>', " Short Name", "true", "[ Short Name]"))
                return false;
            //for Worksite
            if (!chkDropDownList('<%=ddlPayType.ClientID%>', 'Pay Type'))
                return false;
            //for validation below jscode
            if (!Reval('<%=txtPayCoeff.ClientID%>', "pay cofee ", "true", "[pay cofee ]"))
                return false;
            if (!Reval('<%=txtMinServiceYrs.ClientID%>', " Min Service Yrs ", "true", "[ Min Service Yrs]"))
                return false;
            if (!Reval('<%=txtMaxEntitlementYr.ClientID%>', "   Max Entitlement / Yr", "true", "[Max Entitlement / Yr]"))
                return false;
        }
            function isNumber(evt) {
                var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                    return false;
                return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td>
                <table id="tblNew" runat="server" visible="false">
                    <tr>
                        <td colspan="2" class="pageheader">
                            Add Leave Type
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Name<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="300" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Short Name<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSName" runat="server" Width="300" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Payable Type<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPayType" CssClass="droplist" runat="server" 
                                TabIndex="3">
                                <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Payable" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Non-Payable" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Gender<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGender" CssClass="droplist" runat="server" 
                                TabIndex="3">
                                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Both" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 124px">
                            Is Accruable
                        </td>
                        <td>
                            <asp:CheckBox ID="chkisAccruable" runat="server" Checked="True" Text="Active" 
                                TabIndex="4" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Is C/Fwd
                        </td>
                        <td>
                            <asp:CheckBox ID="chkC_Fwd" runat="server" Checked="True" Text="Active" 
                                TabIndex="4" />
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 124px">
                            Is Encashable
                        </td>
                        <td>
                            <asp:CheckBox ID="chkisEncashable" runat="server" Checked="True" Text="Active" 
                                TabIndex="4" />
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 124px">
                            Pay Coeff<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPayCoeff" runat="server" Width="300" Text="1" TabIndex="2" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 124px">
                            Min Service Yrs<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMinServiceYrs" runat="server" Width="300" Text="1" TabIndex="2"  onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            <asp:comparevalidator id="txtMinServiceYrsCV" runat="server" ControlToValidate="txtMinServiceYrs"  operator="DataTypeCheck" type="Double">
	                           Entry must be NUMBER.
                            </asp:comparevalidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                           Max Entitlement / Yr<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMaxEntitlementYr" runat="server" Width="300" Text="1" TabIndex="2"  onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                             <asp:comparevalidator id="txtMaxEntitlementYrCV" runat="server" ControlToValidate="txtMaxEntitlementYr"  operator="DataTypeCheck" type="Double">
	                           Entry must be NUMBER.
                            </asp:comparevalidator>
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 124px">
                          Labour Law Ref
                        </td>
                        <td>
                            <asp:TextBox ID="txtLabourLawRef" runat="server" Width="300" Text="" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 124px">
                          Remarks
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" Width="300" Text="" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Status
                        </td>
                        <td>
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" 
                                TabIndex="4" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                OnClick="btnSubmit_Click" 
                                OnClientClick="javascript:return validatesave();" AccessKey="s" TabIndex="5" 
                                ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tblEdit" runat="server" visible="false">
                    <tr>
                        <td style="width: 100%">
                             <table  style="width: 100%">
                       <tr>
               <td style="width:165px">
                <cc1:accordion ID="NewDeptAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" 
                        TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" 
                        SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="NewDeptAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rblstStatus" runat="server" RepeatDirection="Horizontal" TabIndex="1" OnSelectedIndexChanged="rblstStatus_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:accordion>
               </td>
                </tr>
                </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                CellPadding="4" ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvLeaveProfile_RowCommand"
                                HeaderStyle-CssClass="tableHead">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payable Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayable" runat="server" Text='<%#Eval("PayType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Monthly Accrue?">
                                        <ItemTemplate>
                                            <asp:Label ID="lblisAccruable" runat="server" Text='<%#Eval("isAccruable")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="C/Fwd Bynd Ttl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCFwd" runat="server" Text='<%#Eval("CFwd")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Is Encashable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblisEncashable" runat="server" Text='<%#Eval("isEncashable")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="PayCoeft">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayCoeft" runat="server" Text='<%#Eval("PayCoeft")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Min Service Yrs">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinServiceYrs" runat="server" Text='<%#Eval("MinServiceYrs")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Max Entitlement / Yr">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxEntitlementYr" runat="server" Text='<%#Eval("MaxEntitlementYr")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Labour Law Ref">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLabourLawRef" runat="server" Text='<%#Eval("LabourLawRef")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("LeaveType")%>'
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
      <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
