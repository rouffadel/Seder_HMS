<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmployeeAttendanceDevice.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeAttendanceDevice" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidAddDevEmpID() {
            //for DeviceName
            if (!chkDropDownList('<%=ddlDeviceName.ClientID%>', 'Device Name')) {
                return false;
            }
            //for DeviceEmpID
            if (!chkNumber('<%=txtDevEmpId.ClientID %>', 'Device Employee ID', true, '')) {
                return false;
            }
            // for HMS EmpID
            if (!chkNumber('<%=txtHMSEmpID.ClientID %>', ' HMS Employee ID', true, '')) {
                return false;
            }
        }

        function ValidDdlDevice() {
            //for DeviceName
            if (!chkDropDownList('<%=ddlDevName.ClientID%>', 'Device Name')) {
                return false;
            }
        }
    
    </script>
    <asp:UpdatePanel ID="EmployeeAttDeviceUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
               
                <tr>
                    <td>
                        <div id="dvAddEmpUnderDev" runat="server">
                    <asp:panel id="pnlAdd" runat="server" CssClass="DivBorderOlive">

                            <table width="100%">
                                <tr>
                                    <td colspan="2" class="pageheader">
                                        <asp:Label ID="lblAttDevEmp" runat="server" Text="Add Employee To Device"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 113px">
                                        <asp:Label ID="lblDevEmpIdDevName" runat="server" Text="Device Name"></asp:Label>
                                        <span style="color: red">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDeviceName" runat="server" Width="200">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 113px">
                                        <asp:Label ID="lblDevEmpId" runat="server" Text="Device Emp ID"></asp:Label>
                                        <span style="color: #ff0000">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDevEmpId" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtwmDevEmpID" runat="server" TargetControlID="txtDevEmpId"
                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter DeviceEmpID]">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 113px">
                                        <asp:Label ID="lblAttDevHMSEmpID" runat="server" Text="HMS Emp ID"></asp:Label>
                                        <span style="color: #ff0000">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtHMSEmpID" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="txtwmHMSEmpID" runat="server" TargetControlID="txtHMSEmpID"
                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter HMSEmpID]">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 160Px" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" AutoPostback="true" CssClass="savebutton" Width="100px"
                                            OnClientClick="javascript:return ValidAddDevEmpID()" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:panel>

                        </div>
                        <br />
                        <div id="dvSelectDev" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                            <Panes>
                                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent">
                                                    <Header>
                                                        Search Criteria
                                                    </Header>
                                                    <Content>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                            <tr>
                                                                <td align="left" style="width: 113px">
                                                                    <asp:Label ID="lblEmpAttDevDevName" runat="server" Text="Device Name"></asp:Label>
                                                                    <span style="color: red">*</span>
                                                                </td>
                                                                <td align="left" style="width: 200px">
                                                                    <asp:DropDownList ID="ddlDevName" CssClass="droplist" runat="server" Width="200">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="padding-left: 160Px" colspan="2" align="left">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Select" CssClass="savebutton" Width="100px"
                                                                        OnClick="btnSubmit_Click" OnClientClick="javascript:return ValidDdlDevice()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </Content>
                                                </cc1:AccordionPane>
                                            </Panes>
                                        </cc1:Accordion>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div id="dvgv" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td class="pageheader">
                                        <asp:Label ID="lblEmpAttDevHeadLine" runat="server" Text="Employee Attendance Device :"
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvEmpAttDev" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="GhostWhite"
                                            HeaderStyle-CssClass="tableHead" ShowFooter="false" Width="100%" OnRowCommand="gvEmpAttDev_RowCommand"
                                            CssClass="gridview" onrowdatabound="gvEmpAttDev_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="DeviceID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeviceID" runat="server" Text='<%# Eval("DeviceID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDeviceID" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="DeviceEmpID">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeviceEmpID" runat="server" Text='<%# Eval("DeviceEmpID")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDeviceEmpID" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HMSEmpID">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtHMSEmpID" runat="server" Text='<%# Eval("HMSEmpID")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtHMSEmpID" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmployeeName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHMSEmpID" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtHMSEmpID" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="UpDate" CommandName="Upd"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DHID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDHID" runat="server" Text='<%# Eval("DHID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDeviceID" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 17px">
                                        <uc1:Paging ID="EmployeeAttendanceDevicePaging" runat="server" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
