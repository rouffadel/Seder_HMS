<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="LeavesCombinations.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeavesCombinations" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function UpdateLC(ctrl, Leave1, Leave2) {
            lblmsg.innerHTML = "";
            var Result = AjaxLeaveCombinations.UpdateLeaveCombination(Leave1, Leave2, ctrl.checked);
            lblmsg.style.fontWeight = "bold";
            if (Result.value = true) {
                lblmsg.innerHTML = "&nbsp;&nbsp;Saved.&nbsp;&nbsp;";
                lblmsg.style.color = "white";
                lblmsg.style.backgroundColor = "Green";
            }
            else {
                lblmsg.innerHTML = "&nbsp;&nbsp;Failed.&nbsp;&nbsp;";
                lblmsg.style.color = "white";
                lblmsg.style.backgroundColor = "Red";
            }
            //alert(Result.value);
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnBack" CssClass="savebutton" runat="server" Text="Back" OnClick="btnBack_Click" TabIndex="1" AccessKey="b" ToolTip="[Alt+b OR Alt+b+Enter]" />
                    </td>
                </tr>
                <tr>
                    <td class="pageheader">Leave Combination
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Table ID="tblLeaveCombination" runat="server" BorderWidth="2" GridLines="Both">
                        </asp:Table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <label id="lblmsg">
                        </label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
