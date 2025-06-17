<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="EmpPrevTDS.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpPrevTDS" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ClosePopUp() {
            $find('<%=mpeView.ClientID%>').hide();
            return false;
        }
        function ShowProof(ImgID, ImgExt) {
            window.open("./EmpPreviousTDS/" + ImgID + "." + ImgExt);
//            $get('imgView').src = "./EmpPreviousTDS/" + ImgID + "." + ImgExt;
//            $find('<%=mpeView.ClientID%>').show();
            return false;
        }
    </script>
    <table>
        <tr>
            <td>
                <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSite" runat="server" Text="Worksite"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlworksites" runat="server" CssClass="droplist" AccessKey="w"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" ToolTip="[Alt+w+Enter]" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddldepartments" runat="server" CssClass="droplist" TabIndex="2"
                                                AccessKey="1" ToolTip="[Alt+1]">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblHisID" runat="server" Text="Historical ID"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOldEmpID" Width="90" runat="server" AccessKey="2" ToolTip="[Alt+2]"
                                                TabIndex="3"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmpID" runat="server" Text="EmpID"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpID" Width="60Px" runat="server" CssClass="droplist" AccessKey="3"
                                                ToolTip="[Alt+3]" TabIndex="4"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>   
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtusername" Width="90" runat="server" MaxLength="30" CssClass="droplist"
                                                AccessKey="4" ToolTip="[Alt+4]" TabIndex="5"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"
                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" Width="80px"
                                                OnClick="btnSearch_Click" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" TabIndex="6" />
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
                <asp:GridView ID="grdPrvTDS" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                    CssClass="gridview" EmptyDataText="No Records Found" OnRowCommand="grdPrvTDS_RowCommand"
                    OnRowDataBound="grdPrvTDS_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="EmpID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Historical ID" DataField="HisID" Visible="false">
                            <ControlStyle Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name") %>' ToolTip='<%#Eval("OldEmpID")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblWorksite" runat="server" Text='<%#Eval("category")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("departmentname")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="TDSAmount" runat="server" Text='<%#Eval("TDSAmt")%>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proof">
                            <ItemTemplate>
                                <asp:FileUpload ID="UploadProof" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkProof" runat="server" Text="Proof" CommandName="ViewProof"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" CommandName="Upd"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false"> 
                            <ItemTemplate>
                                <asp:Label ID="lblExt" runat="server" Text='<%#Eval("Ext") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="EmptyRowData" />
                    <HeaderStyle CssClass="tableHead" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="EmpPreTDSPaging" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnShowModalPopup" runat="server" Style="display: none" meta:resourcekey="btnShowModalPopupResource1" />
    <cc1:ModalPopupExtender ID="mpeView" Drag="true" runat="server" TargetControlID="btnShowModalPopup"
        PopupControlID="pnlView" BackgroundCssClass="Background" DropShadow="True" DynamicServicePath=""
        Enabled="True">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlView" runat="server" Style="display: none; overflow: inherit; background-color: #fff;"
        meta:resourcekey="pnlViewResource1">
        <asp:ImageButton ID="btnClose" OnClientClick="javascript:return ClosePopUp();" ImageUrl="~/IMAGES/img-close-off.gif"
            ImageAlign="Right" runat="server" meta:resourcekey="btnCloseResource1" /><br />
        <div id="Display1" style="padding: 10px 0px 30px 30px; display: inherit" runat="server">
            <img id="imgView" alt="No Image" src="" />
        </div>
    </asp:Panel>
</asp:Content>
