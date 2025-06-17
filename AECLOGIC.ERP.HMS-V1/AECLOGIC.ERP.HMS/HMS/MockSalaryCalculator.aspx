
<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="MockSalaryCalculator.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MockSalaryCalculator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>

    <style  type="text/css" >
         .modalBackground {
             background-color: gray;
             filter: alpha(opacity=80);
             opacity: 0.8;
             z-index: 10000;
         }
        </style>
    <style type="text/css">
    .rounded_corners
    {
       
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
    }
    .rounded_corners td
    {
       border: 0.5px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align:left;
         
    }
     .rounded_corners th
    {
        border: 0.5px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align:left;
        font-weight:bold ;
    }
     
    .rounded_corners table table td 
    {
        border-style:dotted ;
    }
    a {
    color:black ;
    font-family:Verdana ;color:black
}
    a:link {
    text-decoration: none;
}

a:visited {
    text-decoration: none;
}

a:hover {
    text-decoration: underline;
}

a:active {
    text-decoration: underline;
}
</style>
    <script type="text/javascript"  >
        function NoOfLeaveValidation() {
            if (!chkNumber('<%=txtReqLeaves.ClientID %>', ' leaves', true, ' ')) {
                return false;
            }

            if (!chkDropDownList('<%=ddlSumLeaveType.ClientID%>', 'LeaveType'))
                return false;

        }
    </script>
           <asp:UpdatePanel runat="server" ID="updpnl"   UpdateMode ="Conditional" >
             <ContentTemplate>  
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td colspan="2">
                         <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1"  AssociatedUpdatePanelID="updpnl" >
            <ProgressTemplate>
                <div class="overlay">
<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">
            <span style ="color:green ; font-weight:bold ">Loading...</span>    <img src="../IMAGES/updateProgress.gif" alt="update is in progress" />
    </div>
                   </div> 
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                    SelectedIndex="0">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                            Search Criteria</Header>
                            <Content>
                                <table width="100%">
                                    <tr>
                                        <td><b>Site</b>
                                                        <asp:DropDownList ID="ddlworksites" runat="server" CssClass="droplist" AccessKey="w"
                                                            ToolTip="[Alt+w OR Alt+w+Enter]" Width="180px" TabIndex="1">
                                                        </asp:DropDownList>
                                            &nbsp;<b>Department</b>
                                                        &nbsp;<asp:DropDownList ID="ddldepartments" runat="server" CssClass="droplist" TabIndex="2">
                                                        </asp:DropDownList>
                                            &nbsp;<b>Historical ID:</b><asp:TextBox ID="txtOldEmpID" Width="60px" runat="server" AccessKey="1"
                                                ToolTip="[Alt+1]" TabIndex="3"></asp:TextBox>
                                            &nbsp;<b>EmpID:</b>&nbsp;<asp:TextBox ID="txtEmpID" Width="60Px" runat="server" CssClass="droplist" 
                                                AccessKey="2" ToolTip="[Alt+2]" TabIndex="4"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            &nbsp;<b>Name:</b>&nbsp;<asp:TextBox ID="txtusername" Width="150px" runat="server" MaxLength="30"  
                                                CssClass="droplist" AccessKey="3" ToolTip="[Alt+3]"
                                                TabIndex="5"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"  
                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
                                            </cc1:TextBoxWatermarkExtender> &nbsp; <b>Asses.Year: </b> <asp:DropDownList ID="ddlYear" Width="60px"  runat="server"   TabIndex="3"  AccessKey="y" ToolTip="[Alt+y]" CssClass="droplist"></asp:DropDownList>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                CssClass="btn btn-primary" Width="50px" AccessKey="i" ToolTip="[Alt+i]" TabIndex="6" />

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
            <td style="vertical-align:top ;width:40%">
                <table style="vertical-align:top ;width:100%">

                    <tr>
                        <td style="vertical-align:top">

                            <table style="vertical-align:top ;width:100%"  >
                                <tr>
                                    <td style="vertical-align:top"  >
                                        <asp:Panel ID="Panel1" CssClass="box box-primary" runat="server"  width="96%">
                                            <table width="100%">
                                                <tr class="accordionHeader"  >
                                                    <td colspan="2" >Employee Details:</td>
                                                </tr>
                                                <tr>
                                                    <td>Name</td>
                                                    <td>
                                                        <asp:Label runat="server" Text="" ID="lblEMPName" Forecolor="Blue" Font-Bold="true"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfSiteID" Value="0"></asp:HiddenField>
                                                        <asp:HiddenField runat="server" ID="hfID" Value="0"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>

                                </tr>
                                  <tr>
                                    <td style="vertical-align:top"  >
                                        <asp:Panel ID="pnlCOnfigure" CssClass="box box-primary" runat="server"  width="96%">
                                            <table width="100%">
                                                <tr class="accordionHeader"  >
                                                    <td colspan="2" >Leave & Vacation Configuration(s) Alert/Info:</td>
                                                </tr>
                                                <tr>
                                                    <td colspan ="2"  style="border-bottom :dotted  ;border-bottom-width  :1px;border-bottom-color  :#0094ff" >Employee Nature:  <asp:Label runat="server" Text="" ID="lblEmpnature" Forecolor="Blue" Font-Bold="true"></asp:Label> <asp:HiddenField  runat="server" id="HDEmpnatureID" ></asp:HiddenField>
                                                         <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="lnkvwC" PopupControlID="Panel3"
                                CancelControlID="ImageButton4" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel3" runat="server" CssClass="box box-primary" Style="border-style: groove; background-color: #ffd800; display:none">

                                <div style="background-color: #0094ff; width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="left">Leave Entitlement Configuration</td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton4" runat="server" ToolTip="Close" ImageUrl="~/Images/close.png" Width="20px" Height="20px" /></td>
                                        </tr>
                                    </table>
                                    </div>
                            
                                 <table >
                                        <tr>
                                            <td >
                                                     <asp:GridView ID="gvLeaveEL" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                             HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"
                                    CssClass="gridview" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Leave Type">
                                        <ItemTemplate>
                                            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Nature">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNature" runat="server" Text='<%#Eval("Nature")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allotment Cycle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllotment" runat="server" Text='<%#Eval("Allotment")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MinDaysOfWork">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinDaysOfWork" runat="server" Text='<%#Eval("MinDaysOfWork")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MaxLeaveEligibility(Month)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxLeaveEligibility" runat="server" Text='<%#Eval("MaxLeaveEligibility")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="MaxLeaveEligibility(Year)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxLeaveEligibilityyear" runat="server" Text='<%#Eval("MaxLeaveElgYear")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                             
                            </asp:GridView>
                                            </td>
                                        </tr>
                                  </table>
                             
                                </asp:Panel> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan ="2" style="border-bottom :dotted  ;border-bottom-width  :1px;border-bottom-color  :#0094ff; vertical-align:middle " >
                                                         <asp:CheckBox  runat="server" id="chkholidaysConfig" tooltip="If Uncheck Please go to Holidays Configuration First for Employee Nature!"  Enable="false"  ></asp:CheckBox> <a  href="ListOfHolidaysConfig.aspx"  target="_blank" title="Leave & Vacation -> Configurations -> Holidays [QAID:109]"  > Holidays Configuration     </a> <asp:ImageButton  runat="server" id="ImgHoliConfig" src="../Images/eye-16.png" tooltip="View Holidays Configuration!" OnClick="View_holidays"  ></asp:ImageButton>
                                                        <br />
                                                         <asp:CheckBox  runat="server" id="chkWeekOffs" tooltip="If Uncheck Please go to WeekOffs Configuration First for Employee Nature"  Enable="false"  ></asp:CheckBox>  <a  href="WeekOffConfigByNature.aspx"  target="_blank" title="Leave & Vacation -> Configurations -> Week-Offs [QAID:1002]" >WeekOffs Configuration   </a> <asp:ImageButton  runat="server" id="ImgWeekOffs" src="../Images/eye-16.png" tooltip="View WeekOffs Configuration!" OnClick="View_WeekOffs" ></asp:ImageButton>
                                                       <br />
                                                            <asp:CheckBox  runat="server" id="ChkLeaveEntitlement" tooltip="If Uncheck Please go to Leave Entitlement Configuration First for Employee Nature"   Enable="false"  ></asp:CheckBox>    <a  href="LeaveEntitlement.aspx"  target="_blank"  title="Leave & Vacation -> Configurations -> Leave Entitlements -> View [QAID:158]" >Leave Entitlement Configuration </a> <asp:ImageButton  runat="server" id="ImgLeaveEntitle" src="../Images/eye-16.png" tooltip="View Leave Entitlement Configuration!" OnClick="View_LeaveEnti" ></asp:ImageButton>
                                                      
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="lnkvwC1" PopupControlID="panleholidays"
                                CancelControlID="ImageButton3" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="panleholidays" runat="server" CssClass="box box-primary" Style="border-style: groove; background-color: #ffd800; display:none">

                                <div style="background-color: #0094ff; width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="left">Holidays Configuration</td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="Close" ImageUrl="~/Images/close.png" Width="20px" Height="20px" /></td>
                                        </tr>
                                    </table>
                                    </div>
                            
                                 <table >
                                        <tr>
                                            <td >
                                                    <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                       HeaderStyle-CssClass="tableHead" CssClass="gridview"
                                        Width="100%">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Holiday">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHoliday" runat="server" Text='<%#Eval("Holiday")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Holiday Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHolidayType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfileType" runat="server" Text='<%#Eval("Nature")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         
                                           
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                      
                                    </asp:GridView>
                                            </td>
                                        </tr>
                                  </table>
                             
                                </asp:Panel> 
                                                         <cc1:ModalPopupExtender ID="ModalPopupExtenderk" runat="server" TargetControlID="lnkvwC2" PopupControlID="panleweekoffs"
                                CancelControlID="ImageButton2" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="panleweekoffs" runat="server" CssClass="box box-primary" Style="border-style: groove; background-color: #ffd800; display:none">

                                <div style="background-color: #0094ff; width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="left">WeekOffs Configuration</td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Close" ImageUrl="~/Images/close.png" Width="20px" Height="20px" /></td>
                                        </tr>
                                    </table>
                                    </div>

                                <center>
                                    <table>
                                        <tr>
                                            <td>
                                     <table cellpadding="0" cellspacing="0" runat="server" id="tblWeeks" class="gridview"
                    style="width: 450px">
                    <thead>
                        <tr>
                            <th>
                                &nbsp;
                            </th>
                            <th style="width: 30px">
                                SUN
                            </th>
                            <th style="width: 30px">
                                MON
                            </th>
                            <th style="width: 30px">
                                TUE
                            </th>
                            <th style="width: 30px">
                                WED
                            </th>
                            <th style="width: 30px">
                                THU
                            </th>
                            <th style="width: 30px">
                                FRI
                            </th>
                            <th style="width: 30px">
                                SAT
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <th>
                            1st Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1SUN"  runat="server" TabIndex="2"
                                Text=" " />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1MON"  runat="server"
                                Text=" " TabIndex="3" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1TUE"  runat="server"
                                Text=" " TabIndex="4" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1WED"  runat="server"
                                Text=" " TabIndex="5" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1THU"  runat="server"
                                Text=" " TabIndex="6" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1FRI"  runat="server"
                                Text=" " TabIndex="7" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1SAT" runat="server"
                                Text=" " TabIndex="8" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            2nd Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2SUN" AutoPostBack="true" 
                                runat="server" Text=" " TabIndex="9" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2MON" runat="server"
                                Text=" " TabIndex="10" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2TUE"  runat="server"
                                Text=" " TabIndex="11" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2WED" runat="server"
                                Text=" " TabIndex="12" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2THU"  runat="server"
                                Text=" " TabIndex="13" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2FRI"  runat="server"
                                Text=" " TabIndex="14" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2SAT"  runat="server"
                                Text=" " TabIndex="15" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            3rd Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3SUN"  runat="server"
                                Text=" " TabIndex="16" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3MON"  runat="server"
                                Text=" " TabIndex="17" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3TUE"  runat="server"
                                Text=" " TabIndex="18" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3WED"  runat="server"
                                Text=" " TabIndex="19" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3THU"  runat="server"
                                Text=" " TabIndex="20" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3FRI" runat="server"
                                Text=" " TabIndex="21" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3SAT"  runat="server"
                                Text=" " TabIndex="22" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            4th Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4SUN" runat="server"
                                Text=" " TabIndex="23" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4MON" runat="server"
                                Text=" " TabIndex="24" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4TUE"  runat="server"
                                Text=" " TabIndex="25" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4WED"  runat="server"
                                Text=" " TabIndex="26" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4THU"  runat="server"
                                Text=" " TabIndex="27" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4FRI"  runat="server"
                                Text=" " TabIndex="28" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4SAT"  runat="server"
                                Text=" " TabIndex="29" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            5th Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5SUN"  runat="server"
                                Text=" " TabIndex="30" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5MON"  runat="server"
                                Text=" " TabIndex="31" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5TUE"  runat="server"
                                Text=" " TabIndex="32" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5WED"  runat="server"
                                Text=" " TabIndex="33" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5THU" runat="server"
                                Text=" " TabIndex="34" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5FRI"  runat="server"
                                Text=" " TabIndex="35" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5SAT"  runat="server"
                                Text=" " TabIndex="36" />
                        </td>
                    </tr>
                </table>
                                                </td>
                                        </tr>
                                    </table>
                                </center>
                                </asp:Panel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td  style="vertical-align:top " > <img src="../Images/HandWarn.ico" title="Message" width="32px" height ="32px" /></td>  <td style="vertical-align:middle  ">[To make changes go through configurations name(s) link ]
                                                     <asp:LinkButton  id="lnkvwC" runat="server"   style="display:none " ></asp:LinkButton>
                                                    <asp:LinkButton  id="lnkvwC1" runat="server"   style="display:none " ></asp:LinkButton>
                                                        <asp:LinkButton  id="lnkvwC2" runat="server"   style="display:none " ></asp:LinkButton>
                                                             
                                                        </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="idpnl" CssClass="box box-primary" runat="server"  width="96%">
                                            <table width="100%" >
                                                <tr class="accordionHeader">
                                                    <td colspan="2"  >Create Mock Attendance</td>
                                                </tr>
                                                <tr>

                                                    <td>From Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDay" Width="90Px" placeholder="dd-MMM-yyyy"  runat="server"  ></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDayCalederExtender" runat="server" TargetControlID="txtFromDay"  Format="dd-MMM-yyyy" 
                                                            PopupButtonID="txtDOB">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>To Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDay" Width="90Px" placeholder="dd-MMM-yyyy" runat="server"  ></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtToDayCalederExtender" runat="server" TargetControlID="txtToDay" Format="dd-MMM-yyyy"
                                                            PopupButtonID="txtDOB">
                                                        </cc1:CalendarExtender> <asp:LinkButton runat="server" id="lnkabsentdates" Text="Absent Dates"  ></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan ="2" > 
<script language="C#" runat="server">
      void Selection_Change(Object sender, EventArgs e) 
      {
          btn_clear.Visible = true;
        //Label11.Text = "The selected date(s):" + "<br />";
         for (int i = 0; i <= Calendar1.SelectedDates.Count - 1; i++)
         {
           if (Label11.Text.Trim () !="")
             Label11.Text += "," + Calendar1.SelectedDates[i].ToString("dd-MMM-yyyy") + "<br />";
           else
               Label11.Text += Calendar1.SelectedDates[i].ToString("dd-MMM-yyyy") + "<br />";
               
         } 
      }
       protected void btn_clear_Click(Object sender, EventArgs e)
      {
          Label11.Text = "";
          btn_clear.Visible = false ; 
      }
       protected void btn_Done_Click(Object sender, EventArgs e)
       {
           ModalPopupExtender1.Hide();
       }
   </script><cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkabsentdates" PopupControlID="dvAddstns"
                                CancelControlID="Button4" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="dvAddstns" runat="server" CssClass="box box-primary" Style="border-style: groove; background-color: #ffd800; display: none">

                                <div style="background-color: #0094ff; width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="left">Select Absent Dates:</td>
                                            <td align="right">
                                                <asp:ImageButton ID="Button4" runat="server" ToolTip="Close" ImageUrl="~/Images/close.png" Width="20px" Height="20px" /></td>
                                        </tr>
                                    </table>

                                </div>
                                <center>
                     <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true" UpdateMode="Conditional">
                             <ContentTemplate>
                                <table style ="background-color:white;margin-left:5px;margin-right:5px;margin-top:5px;margin-bottom:5px" >
                                   <tr>
                                        <td   >
                                              <asp:Calendar ID="Calendar1" runat="server"    SelectionMode="DayWeekMonth"   ShowGridLines="True"      OnSelectionChanged="Selection_Change">
                                                <SelectedDayStyle BackColor="Yellow"  ForeColor="Red">
                                                 </SelectedDayStyle> </asp:Calendar><br />  
                                    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                <span style ="color:#0094ff;font-weight:bold;text-decoration:underline  " >The selected Absent date(s): </span><br />
                                                 <asp:Label id="Label11" runat="server"  BackColor="Yellow"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button id="btn_clear" runat="server" CssClass="btn btn-danger" Text="Clear Dates" OnClick="btn_clear_Click" Visible="false"  /> <asp:Button id="btndone" runat="server" CssClass="btn btn-success" Text="Done.."  OnClick="btn_Done_Click" />
                                        </td>
                                    </tr>
                                    </table> 
                                 </ContentTemplate> 
                         </asp:UpdatePanel> 
                                    </center>
                             
                             </asp:Panel> 


                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td  colspan ="2" >
                                                        <asp:CheckBox  runat="server" id="chkweekoff" tooltip="Check if you want to generate attendance considering to Week Off as per configure!" Text="Consider Week Off"   ></asp:CheckBox>  
                                                        
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td  colspan ="2" >
                                                        <asp:CheckBox  runat="server" id="chkPH" tooltip="Check if you want to generate attendance considering to Public Holiday as per configure!" Text="Consider Public Holiday"   ></asp:CheckBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btninsert" runat ="server" CssClass="btn btn-primary"  Text="Insert Attendance!" OnClick="btn_insert"  OnClientClick="javascript:return confirm('ALERT: \nIf you want to create mock leaves also for this month then Click [CANCEL] & follow steps: \n1. GOTO Create Mock Leaves \n2. Create Mock Attendnace  !')" ToolTip ="Submit Attendnace Forcly!" />
                                                        
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2"  CssClass="box box-primary" runat="server" width="96%">
                                            <table width="100%">
                                                <tr class="accordionHeader" >
                                                    <td colspan="2" >Create Mock Leaves</td>
                                                </tr>
                                                <tr> <td colspan="2" style="border-bottom :dotted  ;border-bottom-width  :1px;border-bottom-color  :#0094ff"  > Date of Join:  <asp:Label ID="lblDateofJoin" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label> &nbsp;&nbsp;For Year:  <asp:Label ID="Labelyr" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
               <br />
                Available Leaves: &nbsp;&nbsp;<b>CL:</b> <asp:Label ID="lblAvailable" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>EL:</b><asp:Label ID="LabelEL" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>SL:</b><asp:Label ID="LabelSL" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
            </td></tr>
                                                <tr>
                                                    <td>Leave Type </td><td >
                       &nbsp;
                            <asp:DropDownList ID="ddlSumLeaveType" CssClass="droplist" runat="server"
                                TabIndex="5">
                            </asp:DropDownList>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td  colspan ="2" style="border-width:1px;border-style:dotted ;border-color:#A1DCF2;border-top-left-radius :10px;border-bottom-right-radius :10px" >

                                                        Enter Required Leaves: <asp:TextBox ID="txtReqLeaves" runat="server" width="70px" TabIndex="1"></asp:TextBox>&nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-warning" Font-Bold="True"
                    OnClick="btnSubmit_Click" Text="Check status!" OnClientClick="javascript:return NoOfLeaveValidation();"
                    AccessKey="i" TabIndex="2" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td>From Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromData_Leav" Width="90Px" placeholder="dd-MMM-yyyy" runat="server" ></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromData_Leav" Format="dd-MMM-yyyy"
                                                            PopupButtonID="txtDOB">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>To Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtToData_Leave" Width="90Px" placeholder="dd-MMM-yyyy" runat="server"  ></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToData_Leave" Format="dd-MMM-yyyy"
                                                            PopupButtonID="txtDOB">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>

                                                <tr><td style ="vertical-align:top ">
                <b>Comment:</b>
            </td>
            <td>
                <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" BorderColor="#CC6600"
                    BorderStyle="Outset" Rows="4" Width="200px" TabIndex="7"></asp:TextBox><cc1:TextBoxWatermarkExtender
                        ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtComment" WatermarkCssClass="Watermark"
                        WatermarkText="[Enter Your Comment Here!]">
                    </cc1:TextBoxWatermarkExtender><asp:Button ID="btnleaveSubmit" runat ="server"  Text="Done Direct Approval!" OnClick="btnApply_Click" ToolTip ="Approve Leave Direct!" />
            </td> </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                               

                            
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel4" CssClass="box box-primary" runat="server" width="96%">
                                            <table width=100%" >
                                                <tr class="accordionHeader">
                                                    <td colspan="2" >Calculate Mock Payslip</td>
                                                </tr>
                                                <tr><td  colspan ="2" style="border-bottom :dotted  ;border-bottom-width  :1px;border-bottom-color  :#0094ff" >

                                                          &nbsp;Month
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp;Year
                                                                <asp:DropDownList ID="ddlyaer1" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3">
                                                                </asp:DropDownList>
                                                    </td></tr>
                                                <tr>

                                                    <td  colspan ="2" align="Right" >  <asp:Button ID="btnPayslip" Text="Generate Payslip" runat="server" CssClass="btn btn-success" OnClick="btnPayslip_Click" />
                                              
                                                        <asp:Button ID="btnCal" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnMockCreate_Click" />
                                                    </td>
                                                </tr>
                               
                                            </table>
                                        </asp:Panel>

                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel6" CssClass="box box-primary" runat="server"  width="96%">
                                            <table width="100%" >
                                                <tr class="accordionHeader">
                                                    <td colspan="2"  >Execute SQL Jobs Manualy(if required?)</td>
                                                </tr>
                                                 <tr>
                                                    <td  style="vertical-align:top " > <img src="../Images/HandWarn.ico" title="Message" width="32px" height ="32px" /></td>  <td style="vertical-align:middle  ">[These Jobs will be execute for selected Assesment Year only!]</td>
                                                </tr>
                                                <tr><td colspan="2" style="border:dotted;border-color:#0094ff;border-width:1px;border-radius:20px"  ><span style="color:green;font-weight:bold  "> <b>1.</b> Leave Account Sync. Job: </span>  </td></tr>
                                               
                                                <tr>

                                                    <td colspan ="2" >Start Date:
                                                        <asp:TextBox ID="txtassesmentstrt_date" Width="90Px" placeholder="dd-MMM-yyyy" runat="server"   tooltip="Date should be Current date of the selected year(eg.for 2015 date:01-Jan-2015)" ></asp:TextBox>
                                             <cc1:CalendarExtender ID="CalendarExtenderk" runat="server" TargetControlID="txtassesmentstrt_date" Format="dd-MMM-yyyy"    PopupButtonID="txtDOB">    </cc1:CalendarExtender>
                                                        
                                                     
                                                   </td>
                                                </tr>
                                                <tr>
                                                    <td colspan ="2" align="Right" >
                                                  <asp:Button runat="server" ID="btnjobleavecredits" width="80px" text="Execute..."   OnClick="btn_Leavecredits" CssClass="btn btn-primary"  tooltip="Execute Credits Leave jobs" />
                                                  <br />
                                                        <hr />
                                                          </td>
                                                </tr>
                                             <tr><td colspan="2" style="border:dotted;border-color:#0094ff;border-width:1px;border-radius:20px"  ><span style="color:green;font-weight:bold  "> <b>2.</b> Employee Timesheet Sync. Job: </span>  </td></tr>
                                               <tr>
                                                    <td colspan ="2" align="Right" >
                                            <asp:Button runat="server" ID="btn_TSJob" width="80px" text="Execute..."  CssClass="btn btn-primary"  OnClick="btn_TimeSheetJOb"  tooltip="Execute Attendnace Timesheet Job to maintain the status(eg. Week off, PH, Absent etc.)" />
                                                        </td> 
                                                   </tr> 
                                            </table>

                                        </asp:Panel>
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align:top ;width:60%">
                            <asp:Panel ID="Panel5" CssClass="box box-primary" runat="server" width="99%">
                                <table width="100%" style="background-color:white;">
                                    <tr>
                                        <td colspan="2" class="accordionHeader">Calculation</td>
                                    </tr>
                                    <tr>
                                        <td> <div id="divMenu" style="height: 25px; position: absolute; top: 60px; visibility: visible;width: 100px">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tbody>
                <tr>
                    <td>
                        <img border="0" alt="Print" onclick="javascript: divMenu.style.display='none'; window.print();  window.close();"
                            class="right" src="Images/print.png" /><img border="0" alt="Close" onclick="javascript: divMenu.style.display='none'; window.close();"
                                class="right" src="Images/close.png" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div align="center" >
        <table  style="width:60%;background-color:white;margin-left:50px" align="left"   >
            <tr>
                <td align="center" colspan="2" style ="border-bottom-color:#0094ff;border-bottom-left-radius:20px;border-bottom-right-radius :20px;border-bottom-width :1px;border-bottom-style :dotted ">
                    <b>
                        <asp:Label ID="lblCompanyName" style="font-weight:bold;font-family :Verdana;font-size :15px " runat="server"></asp:Label>
                    </b>
                    <br />
                    <b> <span  style="font-family :Verdana;font-size :12px "  > Pay-Slip for the month of </span>   
                        <asp:Label ID="lblmonthslip" style="font-family :Verdana;font-size :12px "    font-bold="true"  runat="server"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table align="left" width="100%">
                        <tr>
                            <td align="left"    >
                              <span  style ="font-weight :bold " > Emp. ID:</span>  <asp:Label ID="lblEmpID"  style="font-family :Verdana;font-size :10px " runat="server"></asp:Label> 
                            </td>
                           <td><span  style ="font-weight :bold " >Name:</span> <asp:Label ID="lblName" style="font-family :Verdana;font-size :10px " runat="server"></asp:Label></td>
                        </tr>
                      
                        <tr>
                            <td align="left" class="style1">
                               <span  style ="font-weight :bold " > Department:</span>  <asp:Label ID="lblDept" style="font-family :Verdana;font-size :10px " runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <span  style ="font-weight :bold " >Designation:</span> <asp:Label ID="lbldesig" style="font-family :Verdana;font-size :10px" runat="server"></asp:Label>
                            </td>
                        </tr>
                      
                        <tr>
                            <td align="left" class="style1">
                                <span  style ="font-weight :bold " > Date of Joining:</span>  <asp:Label ID="lblDOJ" style="font-family :Verdana;font-size :10px " runat="server"></asp:Label>
                            </td>
                            <td align="left">
                              <span  style ="font-weight :bold " >    Date of Last Increment:</span>  <asp:Label ID="lblDoLI" style="font-family :Verdana;font-size :10px " runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="left" class="style1">
                                Monthly Salary:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSalary" style="font-family :Verdana;font-size :10px;font-weight:bold  " runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Number of Days Worked:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNODW" runat="server"  style="font-family :Verdana;font-size :10px;font-weight:bold  "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Number of Payable Days:
                            </td>
                            <td align="left"> <asp:LinkButton id="lnkbtn" runat="server" style="display:none"  ></asp:LinkButton>
                                <asp:Label ID="lblNoofDays" runat="server" style="font-family :Verdana;font-size :10px;font-weight:bold  " ></asp:Label> <asp:LinkButton runat="server" id="lnkatt_Viewk" Text="[Show Att.Details]" OnClick="lnkatt_Viewk_click" tooltip="Click to complete attendance details"   ></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="lnkbtn" PopupControlID="Panel7"
                                CancelControlID="ImageButton1" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel7" runat="server" CssClass="box box-primary" Style="border-style: groove; background-color: #ffd800; display: none;width:50%">

                                <div style="background-color: #0094ff; width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td style="height: 10%; color: White; font-weight: bold; font-size: larger" align="left">Complete Attendance Detail(s)  &nbsp; <asp:label runat="server" id="lblmonth"></asp:label> :</td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Close" ImageUrl="~/Images/close.png" Width="20px" Height="20px" /></td>
                                        </tr>
                                    </table>

                                </div>
                             <center>
                             
                                         <div class ="rounded_corners"  >
 
                                    
                                           <table align="left" width="100%"  style="background-color:white;margin:2px 2px 2px 2px" >
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="grdattendnacestatus" runat="server" CssClass="gridview" AutoGenerateColumns="false" ForeColor="#333333" >
                                               
                                             <Columns>
                                                  
                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server" Text='<%#Eval("Leavenm")%>' Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="#Days" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("att_count")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Payble?" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label14" runat="server" Text='<%#Eval("pay_sts")%>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            <br />
                                            <span> <b> #Total Count:</b> </span><asp:Label  runat="server"  id="lblcountatt"   ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;<span style ="color:green "> <b> #Payable Days:</b> </span><asp:Label  runat="server"  id="lblpayD"   ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;<span style ="color :red "> <b> #Non-Payable Days:</b> </span><asp:Label  runat="server"   id="lblNonD"   ></asp:Label>
                                        
                                            </td>
                                        </tr>
                                                <tr  >
                                                    <td  style="vertical-align:top;border:none " > <img src="../Images/HandWarn.ico" title="Message" width="32px" height ="32px" /></td>  <td style="vertical-align:middle  "><asp:Table ID="tblAtt" width="100%" runat="server" BorderWidth="2" GridLines="Both">     </asp:Table></td>
                                                </tr>
                                               <tr><td  colspan="2" runat="server" id="pnlNonHoliday1" ><br /><span  style="font-weight:bold ;font-family:Verdana;font-size:10px "><ul>Non-Payable Holidays [From Holidays Rules] Applied Details: </ul></span></td></tr>
                                               <tr> <td  style="vertical-align:top; " runat="server" id="pnlNonHoliday2" > <img src="../Images/HandWarn.ico" title="Message" width="32px" height ="32px" /></td> 
                                                   <td  style="vertical-align:middle" runat="server" id="pnlNonHoliday" >
                                                   
                                                    <asp:GridView ID="grd_nonPayRules" runat="server" AutoGenerateColumns="false" CssClass="gridview" ForeColor="#333333" >
                                               
                                             <Columns>
                                                  
                                                    <asp:TemplateField HeaderText="On Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("date")%>' Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rule Name" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("rulename")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Found Combination" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label18" runat="server" Text='<%#Eval("comb")%>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                 <span  style="font-weight:bold ;font-family:Verdana;font-size:10px ">  [ After Holidays Rules:]</span>  <span style ="color:green "> <b> #Payable Days:</b> </span><asp:Label  runat="server"  id="lblpayDH"   ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;<span style ="color :red "> <b> #Non-Payable Days:</b> </span><asp:Label  runat="server"   id="lblnonpayDH"   ></asp:Label>
                                                   </td> </tr>
                                               <tr >
                                                   <td style="vertical-align:top;" runat="server" id="pnlNonHoliday4">
                                                       <span style ="font-weight:bold"><b>Note:</b></span></td><td runat="server" id="pnlNonHoliday3"><span style ="font-style:italic;background-color:yellow;font-size:9pt">To understand Holiday Rules All Holidays natures[ <span style ="font-weight :bold " >eg.</span>WO,PH,CL,EL] may be considered as <span style ="font-weight :bold " >Absent</span> [if any Non-Payable Holiday Rules will be applied!].<span style="font-weight:bold " > To know more click.. <a  title ="Leave & Vacation -> Configurations -> Holiday Rules -> View [QAID:161]" href="HolidayPaidRules.aspx"  target="_blank"   > HOLIDAYS RULES </a></span>  </span>
                                                   </td>
                                               </tr>
                                   
                                           
                                    </table>
                        
                                             </div>
                                    </center> 
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Ledger Closing Balance:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLBalance" runat="server"></asp:Label>
                            </td>
                        </tr>
                       <tr><td>
                           <tabe runat="server" id="tblV" Visible="False"  >
                                <tr>
                            <td align="left" class="style1">
                                Bank A/c No
                            </td>
                            <td align="left">
                                <asp:Label ID="lblbankACno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Pancard No
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPancardNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                PF A/c No
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPFACNO" runat="server"></asp:Label>
                            </td>
                        </tr>
                           </tabe>
                           </td></tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
               <div class ="rounded_corners"  >
 
                                           
                                           <table align="left" width="100%"  >
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="grdWages" runat="server" AutoGenerateColumns="false" CssClass="gridview" ForeColor="#333333"
                                                ShowFooter="true">
                                                <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="A" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Earnings: Salaries" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongName" runat="server" Text='<%#Eval("LongName")%>' Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtWages(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblWagesTot" runat="server" Text='<%# GetWages().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdAllowances" runat="server" AutoGenerateColumns="false" CssClass="gridview" ShowFooter="true">
                                                 <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                 <Columns>
                                                    <asp:TemplateField HeaderText="B" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LongName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Earnings: Allowances" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# GetAmtAllowances(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblAllowancesTot" runat="server" Text='<%# GetAllowances().ToString()%>'></asp:Label>
                                                            </b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="grdCoyContrybutions" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                ShowFooter="true">
                                                  <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="C" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deductions: Company  Contributions" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("LongName")%>' Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# GetAmtCoyContrybutions(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblCoyContrybutions" runat="server" Text='<%# GetCoyContrybutions().ToString()%>'></asp:Label>
                                                            </b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdDuductSatatutory" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                ShowFooter="true">
                                                  <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="D" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LongName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Deductions: Statutory" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# GetAmtDuductSatatutory(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblDuductSatatutory" runat="server" Text='<%# GetDuductSatatutory().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="grdITExmention" runat="server" AutoGenerateColumns="false" CssClass="gridview" ShowFooter="true">
                                                  <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="E" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IT Exemptions" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("LongName")%>' Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# GetAmtExemptions(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblExemptions" runat="server" Text='<%# GetExemptions().ToString()%>'></asp:Label>
                                                            </b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdITSavings" runat="server" AutoGenerateColumns="false" CssClass="gridview" ShowFooter="true">
                                                 <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                 <Columns>
                                                    <asp:TemplateField HeaderText="F" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SectionName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="IT-Savings" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# GetAmtSavings(decimal.Parse(Eval("Amount").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblSavings" runat="server" Text='<%# GetSavings().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="gvTDS" runat="server" AutoGenerateColumns="false" CssClass="gridview" ShowFooter="true">
                                                  <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="G" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Tax Deducted at Source" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# GetAmtTDS(decimal.Parse(Eval("value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# GetTDS().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>




                                             <br />
                                            <asp:GridView ID="grdNonCTC" runat="server" AutoGenerateColumns="false" CssClass="gridview" ShowFooter="true">
                                                 <FooterStyle Font-Bold="true"   ForeColor="black" />
                                                 <Columns>
                                                    <asp:TemplateField HeaderText="H" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LongName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Non CTC Components" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNonAmount" runat="server" Text='<%# GetAmtNonCTC(decimal.Parse(Eval("Amount").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblNonCTc" runat="server" Text='<%# GetNonCTC().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </td>
                                    </tr>
                                </table>
                                     </div>  
                                  
                           </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td >
                                <b>I</b><b>&nbsp;&nbsp;Gross Salary (A+B):</b>
                            </td>
                           <td align="left">
                             <b><asp:Label ID="lblGross" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b>J</b><b>&nbsp;&nbsp;Total Deductions (D): </b>
                            </td>
                           
                            <td align="left" >
                                <b>
                                    <asp:Label ID="lblDeductions" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b>K</b> <b>&nbsp;&nbsp;Salary before Tax ((A+B)-(C+D)): </b>
                            </td>
                          
                            <td align="left" >
                                <asp:Label ID="lblSalbfIT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b>L</b> <b>&nbsp;&nbsp;Income Tax Exemptions (E): </b>
                            </td>
                           
                            <td align="left" >
                                <asp:Label ID="lblITExemption" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b>M</b>  <b>&nbsp;&nbsp;Tax Deducted at Source (TDS):</b>
                            </td>
                            
                            <td align="left" >
                                <asp:Label ID="lblTDS" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                              <b>N</b>  <b>&nbsp;&nbsp;Education Cess On TDS:</b> 
                            </td>
                       
                            <td align="left" >
                                <asp:Label ID="lblEducess" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                              <b>O</b>  <b>&nbsp;&nbsp;Net Payable: </b>
                         </td>
                            <td align="left" >
                                <asp:Label ID="lblNetPayable" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                               <b>P</b> <b>&nbsp;&nbsp;Loans/Advance Recovery:</b>
                            </td>
                         
                            <td align="left">
                                <b>
                                    <asp:Label ID="lblLRecovery" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none ">
                            <td>
                                <b>Q</b> <b>&nbsp;&nbsp;Mobile Bill:</b>
                            </td>
                         
                            <td  align="left">
                                <b>
                                    <asp:Label ID="lblMobile" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none ">
                            <td>
                                <b>R</b> <b>&nbsp;&nbsp;Mess Bill:</b>
                            </td>
                          
                            <td align="left"  >
                                <b>
                                    <asp:Label ID="lblMess" runat="server"></asp:Label></b>
                            </td>
                        </tr>

                        <tr style="font-weight: bold;">
                            <td>
                                <b>S</b> <b>&nbsp;&nbsp;Non CTC Components: </b>
                            </td>
                         
                            <td align="left"  >
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr style="font-weight: bold;">
                            <td>
                                <b>T</b> <b>&nbsp;&nbsp;Take Home Pay: </b>
                            </td>
                         
                            <td align="left" >
                                <asp:Label ID="lblTakeHome" runat="server"></asp:Label>
                            </td>
                        </tr>


                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    * All the Salary Calculations are done with respect to Cost-To-Company(Gross) of an
                    Employee
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    * If any Reimbursement, Then the amount will be paid separately
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    Note: This is a system generated payslip and does not require authorization
                </td>
            </tr>
        </table>
    </div>
                                            <table id="tblCal" runat="server"></table>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                        </td>

                    </tr>
                </table>
         </ContentTemplate>
                <Triggers>
 <asp:PostBackTrigger ControlID="btnPayslip" />
 </Triggers>

               </asp:UpdatePanel> 

     
</asp:Content>

