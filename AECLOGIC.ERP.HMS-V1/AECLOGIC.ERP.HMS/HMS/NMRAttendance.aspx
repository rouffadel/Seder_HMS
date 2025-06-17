<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="NMRAttendance.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.NMRAttendance" Title="Attendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        //        function window.onload() {
        //            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestEventHandler);
        //        }
        function CheckLeaveCombination(Status, EmpID, txtIn, ddlWS, UserID, Row, txtid) {
            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;
            if (Status.value != 1 && Status.value != 2 && Status.value != 7 && Status.value != 13 && Status.value != 14 && Status.value != 15) {
                var ResultVal = AjaxDAL.CheckAvailable(Status.value, EmpID);
                if (ResultVal.value.Result == 0)//ResultVal.erorr == null && 
                {
                    document.getElementById(txtid).value = "";
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed');
                    return false;
                }
                if (ResultVal.value.Result == 2) {
                    document.getElementById(txtid).value = "";
                    document.getElementById(Row).style.backgroundColor = "#ff9900";
                    alert('Not Allowed! Machineries/Materials were under his Care/Supervision ask him to transfer those Responsibilities!');
                    return false;
                }
                if (ResultVal.value.Result == 1) {
                    document.getElementById(Row).style.backgroundColor = "#ffffff";
                    var ResultStatus = AjaxDAL.HR_MarkAttandace(EmpID, Status.value, $get(ddlWS).value, UserID);
                    if (ResultStatus.value.Column1 == "0") {
                        alert("Not Updated");
                        return false;
                    }
                }
            }
            else {
                var ResultStatus = AjaxDAL.HR_MarkAttandace(EmpID, Status.value, $get(ddlWS).value, UserID);
                document.getElementById(Row).style.backgroundColor = "#ffffff";
                if (ResultStatus.value.Column1 == "0") {
                    alert("Not Updated");
                    return false;
                }
            }
            if (Status.value == "2") {
                document.getElementById(txtIn).value = d.toLocaleTimeString();
            }
            else {
                document.getElementById(txtIn).value = "";

            }
        }
        function GetOutTime(chkid, outtime, txtid, InID, EmpID) {
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
                var Result = AjaxDAL.HR_UpdateNMROutTime(EmpID);
            }
            else {
                document.getElementById(txtid).value = "";
            }
        }
        function GetInTime(ddlid, txtid) {
            var Result = AjaxDAL.GetServerDate();
            var d = new Date();
            d = Result.value;
            if (document.getElementById(ddlid).value == "2") {
                document.getElementById(txtid).value = d.toLocaleTimeString();
            }
            else {
                document.getElementById(txtid).value = "";

            }
        }
        function Validate(id) {
            //var reg = /^[0-2]{1,2}[:][0-9]{2}[:][0-9]{2}$/;
            //      if(reg.test(document.getElementById(id).value) == false)
            //      {
            //       alert("Please Enter Proper In Time");
            //       document.getElementById(id).focus();
            //       return false;
            //      }
        }
        function MarkNMRAtt(Status, EmpId, txtIn, txtOut, txtRemarks) {
            var ResultStatus = AjaxDAL.InsUpNMRAttendance(EmpId, $get(Status).value, $get(txtIn).value, $get(txtOut).value, $get(txtRemarks).value);
          
            if (ResultStatus.value.Column1 == "0") {
                alert("Not Updated");
                return false;
            }
            else {
                alert("Updated");
            }
        }
 

    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
       
        <tr>
            <td>
                <asp:UpdatePanel ID="updmaintable" runat="server">
                    <ContentTemplate>
                        <asp:Label runat="server" id="lblStatus" Text="" Font-Size="14px"></asp:Label>
                        <table cellspacing="0" cellpadding="0" style="width: 100%; border: 0px;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDate" runat="server" CssClass="pageheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td valign="top">
                                    <table style="border-right: #d56511 1px solid; border-top: #d56511 1px solid; border-left: #d56511 1px solid;
                                        border-bottom: #d56511 1px solid;" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td>
                                                <cc1:accordion id="MyAccordion" runat="server" headercssclass="accordionHeader" headerselectedcssclass="accordionHeaderSelected"
                                                    contentcssclass="accordionContent" autosize="None" fadetransitions="false" transitionduration="50"
                                                    framespersecond="40" requireopenedpane="false" suppressheaderpostbacks="true">
                                                        <Panes>
                                                            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                                ContentCssClass="accordionContent">
                                                                <Header>
                                                                    Search Criteria</Header>
                                                                <Content>
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="height: 26px; text-align: left;">
                                                <strong>Worksite&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlWorksite" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_SelectedIndexChanged" CssClass="droplist">
                                                </asp:DropDownList>
                                                </strong>
                                            </td>
                                            <td align="right">
                                                <strong>Department
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="droplist" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </strong>
                                            </td>
                                        </tr>
                                        </table>
                                                                </Content>
                                                            </cc1:AccordionPane>
                                                        </Panes>
                                                    </cc1:accordion>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" colspan="2">
                                                <asp:UpdatePanel ID="updpgridview" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1" CssClass="gridview"
                                                            CellSpacing="1" DataKeyNames="Empid"  GridLines="None" OnRowDataBound="gdvAttend_RowDataBound"
                                                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                                                           
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="71px" DataTextField="ShortName"
                                                                            CssClass="droplist" DataValueField="ID" DataSource='<%# BindAttendanceType()%>'>
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="btnHid" runat="server" Visible="false" CommandArgument='<%#Bind("Status")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIN" runat="server" Width="100" Text='<%#Bind("InTime")%>' Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkOut" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Out Time">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOUT" runat="server" Width="100" Text='<%#Bind("OutTime")%>' Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" Height="18px" TextMode="MultiLine" Width="107px"
                                                                            Text='<%#Bind("Remarks")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" CommandName="upd" CssClass="btn btn-warning"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
