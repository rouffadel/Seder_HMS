<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ITDeductions.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ITDeductions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validatesave() {

            if (!chkDropDownList('<%=ddlEmployee.ClientID%>', "Employee ", "", ""))
                return false;

            if (!chkDropDownList('<%=ddlSections.ClientID%>', "Section", "", ""))
                return false;

            if (!chkDropDownList('<%=ddlAssessmentYear.ClientID%>', "Assessment  Year", "", ""))
                return false;
            if (!chkName('<%=txtItemName.ClientID%>', "Item Name", true, "")) {
                return false;
            }
            if (!checkdecmial('<%=txtAmount.ClientID%>', "Amount", true, "")) {
                return false;
            }

        }

    </script>

            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
               
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkAdd" Font-Bold="true" runat="server" OnClick="lnkAdd_Click">Add</asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                            ID="lnkView" Font-Bold="true" OnClick="lnkEdit_Click" runat="server">View</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderOlive" Visible="false"
                            Width="50%">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td colspan="2" class="pageheader">
                                        New IT Deductions
                                    </td>
                                </tr>


                                <tr>
                                                            <td style="width: 124px">
                                        Employee<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="ddlEmployee"  CssClass="droplist" runat="server"
                                                                     TabIndex="2" AccessKey="1" ToolTip="[Alt+1]">
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                                                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                                    TargetControlID="ddlEmployee" />
                                                                
                                    </td>
                                      </tr>                        

                                <tr>
                                    <td style="width: 124px">
                                        SectionID<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSections" runat="server" CssClass="droplist" TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        Financial Year<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssessmentYear" CssClass="droplist" runat="server" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        Item Name<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtItemName" runat="server" TabIndex="3"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        Proof<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FUDProof" runat="server" TabIndex="4" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        Amount<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" TabIndex="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                  
                                        <td colspan="2">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                                OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" AccessKey="s"
                                                TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        </td>
                                     
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="ITSectionsAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="ITSectionsAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td >
                                                                <b>Financial Year:</b>
                                                                <asp:DropDownList ID="ddlFinYear" AutoPostBack="true" runat="server" CssClass="droplist"
                                                                    OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged" TabIndex="7">
                                                                </asp:DropDownList>
                                                           &nbsp;&nbsp;

                                                            <b>Employee:</b>
                                                             <asp:DropDownList ID="ddlSearEmployee"  CssClass="droplist" runat="server"
                                                                     TabIndex="2" AccessKey="1" ToolTip="[Alt+1]">
                                                                 <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..."
                                                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                                    TargetControlID="ddlSearEmployee" />

                                                                     &nbsp;&nbsp;
                                                                     <asp:Button ID="btnSearch" runat="server" Text="search" OnClick="btnSearch_Click" CssClass="savebutton" />
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
                                    <asp:RadioButtonList ID="rdStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rdStatus_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Active" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                            </td>
                            </tr>
                            <tr>

                            
                                                      
                       
                       <td style="width: 100%">
                                    <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found" ForeColor="#333333"
                                        GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowCommand="gvLeaveProfile_RowCommand"
                                        CssClass="gridview" onrowdatabound="gvLeaveProfile_RowDataBound">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>

                                        <asp:BoundField HeaderText="EmpName" DataField="EmpName" />
                                        <asp:BoundField HeaderText="Worksite" DataField="Site_Name" />
                                        <asp:BoundField HeaderText="Department" DataField="DepartmentName" />

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Financial Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssessmentYear" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Section Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWages" runat="server" Text='<%#Eval("SectionName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCentage" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proof">
                                                <ItemTemplate>
                                                    <a id="A1" runat="server" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "ITSID").ToString(),DataBinder.Eval(Container.DataItem, "Proof").ToString()) %>'
                                                        target="_blank">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ITSID")%>'
                                                        CommandName="Edt" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkdel" runat="server" CommandArgument='<%#Eval("ITSID")%>'
                                                        CommandName="Del" Text='<%#GetText()%>' ></asp:LinkButton>
                                                </ItemTemplate>
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
      
</asp:Content>
