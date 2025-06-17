<%@ Page Title="" Language="C#" AutoEventWireup="True" 
    CodeBehind="SimRegistration.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SimRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" src="Includes/JS/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function SearchModified() {
            $get('<%=hdnSearchChange.ClientID %>').value = "1";
        }

        function ValidMobileReg() {

            //for Service Provider
            if (!chkDropDownList('<%=ddlProvider.ClientID%>', 'Service Provider'))
                return false;
            //for Category
            if (!chkDropDownList('<%=ddlCat.ClientID %>', 'Category'))
                return false;
            //Sim No
            if (!chkPhoneOrMobile('<%=txtMobileNo.ClientID %>', 'Enter Phone No', true, ''))
                return false;
            //For Service From
            if (!chkDate('<%=txtSFrom.ClientID %>', 'Select Service From Date', false, '', false))
                return false;
            //        //For Service To
            //        if (!chkDate('<%=txtSUpto.ClientID %>', 'Select Service To Date', false, '', false))
            //            return false;

            //for WorkSite
            if (!chkDropDownList('<%=ddlws.ClientID%>', 'Worksite'))
                return false;

        }

        function Valid() {

            //for Service Provider
            if (!chkDropDownList('<%=ddlRMProvider.ClientID%>', 'Service Provider'))
                return false;
            //for Category
            if (!chkDropDownList('<%=ddlCategory.ClientID %>', 'Category'))
                return false;





            //For Service From
            if (!chkDate('<%=txtAllotedon.ClientID %>', 'Select Service From Date', false, '', false))
                return false;

            //Sim Amt Lmt 
            if (!checkdecmial('<%=txtAmountLimit.ClientID %>', 'Enter Amount', true, ''))
                return false;

        } 
    </script>
    <asp:UpdatePanel ID="SimRegUpdPanel" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="left" colspan="2">
                        <asp:LinkButton ID="addnew" Font-Bold="true" ForeColor="Blue" runat="server" OnClick="addnew_Click">Register New</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="lnkAlloted" onclick="javascript:return window.open('MobileAllottementList.aspx');"
                            NavigateUrl="#" ForeColor="Blue" ToolTip="Sims Allotted to Employees" Font-Bold="true"
                            runat="server">Alloted:</asp:HyperLink><asp:Label ID="lblAlloted" runat="server"
                                Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                        <asp:HyperLink onclick="javascript:return window.open('MobileAllottementList.aspx?key=1');"
                            NavigateUrl="#" ID="lnkPending" runat="server" ForeColor="Blue" ToolTip="Sims Not Alloted yet"
                            Font-Bold="true">Not Allotted:</asp:HyperLink><asp:Label ID="lblPending" runat="server"
                                Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" runat="server" id="tblConfigAdd" cellpadding="0" cellspacing="0"
                border="0">
                <tr>
                    <td colspan="2" class="pageheader">
                        SIM Registration
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Service Provider:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProvider" runat="server" CssClass="droplist" TabIndex="1">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search"
                            PromptPosition="Top"  PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                            TargetControlID="ddlProvider" />
                        &nbsp;<asp:TextBox ID="txtFilter" runat="server" TabIndex="2"></asp:TextBox>
                        &nbsp;<asp:Button ID="btnFilter" runat="server" Text="Filter" ForeColor="#CC6600"
                            OnClick="btnFilter_Click" TabIndex="3" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Category:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCat" runat="server" CssClass="droplist" TabIndex="4">
                            <asp:ListItem Text="--SELECT--" Value="0" />
                            <asp:ListItem Text="Mobile Sim" Value="1" />
                            <asp:ListItem Text="Data Card" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Phone No:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobileNo" runat="server" Width="150Px" TabIndex="5"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="fteSimno" TargetControlID="txtMobileNo" runat="server"
                            FilterType="Numbers">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Service From:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSFrom" runat="server" TabIndex="6"></asp:TextBox><cc1:CalendarExtender
                            ID="CalendarExtender2" runat="server" TargetControlID="txtSFrom" PopupButtonID="txtSFrom"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Upto:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSUpto" runat="server" TabIndex="7"></asp:TextBox><cc1:CalendarExtender
                            ID="CalendarExtender1" runat="server" TargetControlID="txtSUpto" PopupButtonID="txtSUpto"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>For Worksite:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlws" runat="server" CssClass="droplist" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Status:</b>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkStaus" Checked="true" Text="Active" Font-Bold="true" ToolTip="Un-Check if not Using"
                            runat="server" TabIndex="9" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSave_Click"
                            OnClientClick="javascript:return ValidMobileReg();" AccessKey="s" TabIndex="10"
                            ToolTip="[Alt+s OR Alt+s+Enter]" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="savebutton" Text="Cancel" AccessKey="b"
                            TabIndex="11" ToolTip="[Alt+b OR Alt+b+Enter]" />
                    </td>
                </tr>
            </table>
            <table id="tblConfigView" visible="false" runat="server" width="100%">
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
                                                <td align="left">
                                                    <asp:RadioButtonList ID="rbStatus" AutoPostBack="true" TextAlign="Right" runat="server"
                                                        TabIndex="1" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbStatus_SelectedIndexChanged">
                                                        <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="DeActivated " Value="0"></asp:ListItem>
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
                      
                           
                        <asp:GridView ID="gvSimView" Width="100%" AutoGenerateColumns="False" CssClass="gridview"
                            runat="server" EmptyDataText="No Records Found!" 
                            OnRowCommand="gvSimView_RowCommand" onrowdatabound="gvSimView_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="PID">
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Vendor" DataField="vendor_name" />
                                <asp:BoundField HeaderText="Phone No" DataField="SimNo">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Category" DataField="Category1">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="Status1">
                                    <HeaderStyle Width="25px" />
                                    <ItemStyle Width="25px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Worksite" DataField="Site_Name" />
                                <asp:BoundField HeaderText="From" DataField="ServiceFrom1">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="To" DataField="Upto1">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblEdit" CommandName="Edt" CommandArgument='<%#Eval("PID")%>'
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
              
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="SimRegistrationPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblSimAltmnt" visible="false" runat="server" width="100%">
                <tr>
                    <td colspan="2" class="pageheader">
                        Sim Allottment
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Service Provider:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRMProvider" runat="server" CssClass="droplist" TabIndex="1">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExtender3" IsSorted="true" PromptText="Type Here To Search"
                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                            TargetControlID="ddlRMProvider" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Category:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                            CssClass="droplist" TabIndex="2">
                            <asp:ListItem Text="--SELECT--" Value="0" />
                            <asp:ListItem Text="Mobile Sim" Value="1" />
                            <asp:ListItem Text="Data Card" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Phone No:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSim" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSim_SelectedIndexChanged"
                            CssClass="droplist" TabIndex="3">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="lblSimcount" ForeColor="Blue" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Allotte To:</b><span style="color: Red">*</span>
                    </td>
                    <td style="margin-left: 40px">
                        <asp:DropDownList ID="ddlEmptoAllot" runat="server" CssClass="droplist" TabIndex="4">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search"
                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                            TargetControlID="ddlEmptoAllot" />
                        &nbsp;<asp:TextBox ID="txtEmpFilter" runat="server" CssClass="droplist" TabIndex="5"></asp:TextBox>
                        &nbsp;<asp:TextBox ID="txtEmpidFilter" runat="server" CssClass="droplist"></asp:TextBox>
                          <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtEmpFilter"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter EmpName Search]">
                                                        </cc1:TextBoxWatermarkExtender>
                          <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEmpidFilter"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Empid Search]">
                                                        </cc1:TextBoxWatermarkExtender>
                        &nbsp;<asp:Button ID="btnEmpFilter" ForeColor="#cc6600" runat="server" Text="Filter"
                            AccessKey="i" TabIndex="6" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="btnEmpFilter_Click" />
                        &nbsp;* Shows only particular site employees where sim was&nbsp; registerd for worksite
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Allotted From:</b><span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAllotedon" runat="server" CssClass="droplist" TabIndex="7"></asp:TextBox><cc1:CalendarExtender
                            ID="CalendarExtender3" runat="server" TargetControlID="txtAllotedon" PopupButtonID="txtAllotedon"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <b>Upto:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAllotedUpto" runat="server" CssClass="droplist" TabIndex="8"></asp:TextBox><cc1:CalendarExtender
                            ID="CalendarExtender4" runat="server" TargetControlID="txtAllotedUpto" PopupButtonID="txtAllotedUpto"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Amount Limit:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmountLimit" runat="server" CssClass="droplist" TabIndex="9"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="fteAmtlimit" TargetControlID="txtAmountLimit" runat="server"
                            FilterType="Custom,Numbers" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b></b>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" CssClass="savebutton" runat="server" Text="Save" OnClick="btnSubmit_Click"
                            OnClientClick="javascript:return Valid();" AccessKey="s" TabIndex="10" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        &nbsp;&nbsp; &nbsp;<asp:Button ID="btnReset" CssClass="savebutton" runat="server" OnClick="btnReset_Click"
                            Text="Clear" AccessKey="b" TabIndex="11" ToolTip="[Alt+b OR Alt+b+Enter]" />
                    </td>
                </tr>
            </table>
            <table width="100%" runat="server" id="tblsimAltmentView" visible="false">
                <tr>
                    <td>
                        <asp:GridView ID="gvRSimsView" Width="100%" EmptyDataText="No Records Found!" AutoGenerateColumns="False"
                            CssClass="gridview" runat="server" OnRowCommand="gvRSimsView_RowCommand" 
                            onrowdatabound="gvRSimsView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="SID" HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AllottedTo" HeaderText="EmpID" Visible="false">
                                    <ItemStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Name">
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIMNo" HeaderText="Phone No">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Type">
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AmountLimit" HeaderText="AmountLimit">
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AllottedFrom1" HeaderText="From">
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Upto1" HeaderText="Upto">
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CommandName="Edt" CommandArgument='<%#Eval("SID")%>'
                                            runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="SimRegAllotmentPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnSearchChangeAllotment" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
