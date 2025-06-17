<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="WeekOffConfigByNature.aspx.cs" Inherits="AECLOGIC.ERP.HMS.WeekOffConfigByNature" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceholder1" runat="Server">
     <script language="javascript" type="text/javascript">
         function WOConfig(obj, WeekNo, Day) {
             var EmpNatureID = document.getElementById("<%=ddlEmpNature.ClientID%>").value;
             if (EmpNatureID != "0") {
                 var IsActive = "0";
                 if (obj.checked)
                     IsActive = "1";
                 var Result = AjaxDAL.HR_InsUpWOConfigByEmpNature(WeekNo, Day, IsActive, EmpNatureID);
                 if (Result.value = true) {
                     alert('Done');
                 }
                 else {
                 }
             }
             else {
                 alert('Select Employee Nature!');
             }
         }
    </script>
      <asp:updatepanel runat="server" ID="UpdatePanel2">
  <ContentTemplate>

    <table width="100%">
        <tr>
            <td>
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <b>Employee Nature:</b>&nbsp;&nbsp;<asp:DropDownList ID="ddlEmpNature" AutoPostBack="true"
                    runat="server" CssClass="droplist" OnSelectedIndexChanged="ddlEmpNature_SelectedIndexChanged" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]">
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 450px">
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
                            <asp:CheckBox ID="chk1SUN" onclick="javascript:return WOConfig(this,'1','1');" runat="server" TabIndex="2"
                                Text=" " />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1MON" onclick="javascript:return WOConfig(this,'1','2');" runat="server"
                                Text=" " TabIndex="3" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1TUE" onclick="javascript:return WOConfig(this,'1','3');" runat="server"
                                Text=" " TabIndex="4" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1WED" onclick="javascript:return WOConfig(this,'1','4');" runat="server"
                                Text=" " TabIndex="5" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1THU" onclick="javascript:return WOConfig(this,'1','5');" runat="server"
                                Text=" " TabIndex="6" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1FRI" onclick="javascript:return WOConfig(this,'1','6');" runat="server"
                                Text=" " TabIndex="7" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk1SAT" onclick="javascript:return WOConfig(this,'1','7');" runat="server"
                                Text=" " TabIndex="8" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            2nd Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2SUN" AutoPostBack="true" onclick="javascript:return WOConfig(this,'2','1');"
                                runat="server" Text=" " TabIndex="9" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2MON" onclick="javascript:return WOConfig(this,'2','2');" runat="server"
                                Text=" " TabIndex="10" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2TUE" onclick="javascript:return WOConfig(this,'2','3');" runat="server"
                                Text=" " TabIndex="11" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2WED" onclick="javascript:return WOConfig(this,'2','4');" runat="server"
                                Text=" " TabIndex="12" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2THU" onclick="javascript:return WOConfig(this,'2','5');" runat="server"
                                Text=" " TabIndex="13" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2FRI" onclick="javascript:return WOConfig(this,'2','6');" runat="server"
                                Text=" " TabIndex="14" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk2SAT" onclick="javascript:return WOConfig(this,'2','7');" runat="server"
                                Text=" " TabIndex="15" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            3rd Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3SUN" onclick="javascript:return WOConfig(this,'3','1');" runat="server"
                                Text=" " TabIndex="16" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3MON" onclick="javascript:return WOConfig(this,'3','2');" runat="server"
                                Text=" " TabIndex="17" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3TUE" onclick="javascript:return WOConfig(this,'3','3');" runat="server"
                                Text=" " TabIndex="18" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3WED" onclick="javascript:return WOConfig(this,'3','4');" runat="server"
                                Text=" " TabIndex="19" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3THU" onclick="javascript:return WOConfig(this,'3','5');" runat="server"
                                Text=" " TabIndex="20" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3FRI" onclick="javascript:return WOConfig(this,'3','6');" runat="server"
                                Text=" " TabIndex="21" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk3SAT" onclick="javascript:return WOConfig(this,'3','7');" runat="server"
                                Text=" " TabIndex="22" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            4th Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4SUN" onclick="javascript:return WOConfig(this,'4','1');" runat="server"
                                Text=" " TabIndex="23" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4MON" onclick="javascript:return WOConfig(this,'4','2');" runat="server"
                                Text=" " TabIndex="24" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4TUE" onclick="javascript:return WOConfig(this,'4','3');" runat="server"
                                Text=" " TabIndex="25" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4WED" onclick="javascript:return WOConfig(this,'4','4');" runat="server"
                                Text=" " TabIndex="26" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4THU" onclick="javascript:return WOConfig(this,'4','5');" runat="server"
                                Text=" " TabIndex="27" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4FRI" onclick="javascript:return WOConfig(this,'4','6');" runat="server"
                                Text=" " TabIndex="28" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk4SAT" onclick="javascript:return WOConfig(this,'4','7');" runat="server"
                                Text=" " TabIndex="29" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            5th Week
                        </th>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5SUN" onclick="javascript:return WOConfig(this,'5','1');" runat="server"
                                Text=" " TabIndex="30" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5MON" onclick="javascript:return WOConfig(this,'5','2');" runat="server"
                                Text=" " TabIndex="31" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5TUE" onclick="javascript:return WOConfig(this,'5','3');" runat="server"
                                Text=" " TabIndex="32" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5WED" onclick="javascript:return WOConfig(this,'5','4');" runat="server"
                                Text=" " TabIndex="33" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5THU" onclick="javascript:return WOConfig(this,'5','5');" runat="server"
                                Text=" " TabIndex="34" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5FRI" onclick="javascript:return WOConfig(this,'5','6');" runat="server"
                                Text=" " TabIndex="35" />
                        </td>
                        <td style="width: 44px" align="center" valign="middle">
                            <asp:CheckBox ID="chk5SAT" onclick="javascript:return WOConfig(this,'5','7');" runat="server"
                                Text=" " TabIndex="36" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      </ContentTemplate>
</asp:updatepanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" 
ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
</asp:content>
