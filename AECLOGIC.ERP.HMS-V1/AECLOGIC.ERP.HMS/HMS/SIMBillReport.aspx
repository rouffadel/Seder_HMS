<%@ Page Title="" Language="C#"   AutoEventWireup="True" 
    CodeBehind="SIMBillReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PhoneBillReport" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        //Check Number
        function chkNumber(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; }
            }
            if (val != '') {
                var rx = new RegExp("[0-9]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    //elm.value='';
                    elm.focus();
                    return false;
                }
            } return true;
        }

        //For Dropdown list
        function chkDropDownList(object, msg) {

            var elm = getObj(object);

            if (elm.selectedIndex == 0) {
                alert("Select " + msg + "!!!");
                elm.focus();
                return false;
            } return true;
        }

        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            } else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            } else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            } else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlworksites_hid.ClientID %>').value = HdnKey;
        }
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddldepartments_hid.ClientID %>').value = HdnKey;
        }
       
    </script>
   
            <table>
                
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                           
                            <tr>
                                <td align="left">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <cc1:Accordion ID="SimCardBillLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%" >
                                                    <Panes>
                                                        <cc1:AccordionPane ID="SimCardBillLstAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                            ContentCssClass="accordionContent">
                                                            <Header>
                                                                Search Criteria
                                                            </Header>
                                                            <Content>
                                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <b>Worksite:</b>
                                                                        
                                                                             <asp:HiddenField ID="ddlworksites_hid" runat="server" />
                                                                        <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                 MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchWorksite"
                                                                                          WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                                                         </cc1:TextBoxWatermarkExtender>
                                                                            <b>&nbsp;Department:</b>
                                                                          
                                                                             <asp:HiddenField ID="ddldepartments_hid" runat="server" />
                                                                         <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                             MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                 CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID"  >
                                                                         </cc1:AutoCompleteExtender>        
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdepartment"
                                                                                 WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                                                             </cc1:TextBoxWatermarkExtender>
                                                                            <b>&nbsp;Month:</b><asp:DropDownList ID="ddlMonth" CssClass="droplist"  runat="server" AccessKey="2" ToolTip="[Alt+2]" TabIndex="3">
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
                                                                            <b>&nbsp;Year:</b>
                                                                            <asp:DropDownList ID="ddlYear"  CssClass="droplist" runat="server" AccessKey="3" ToolTip="[Alt+3]" TabIndex="4">
                                                                            </asp:DropDownList>
                                                                            &nbsp;<asp:TextBox ID="txtusername" runat="server" MaxLength="50" OnTextChanged="btnSearch_Click" AccessKey="4" ToolTip="[Alt+4]" TabIndex="5"></asp:TextBox>
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtusername"
                                                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Emp Filter]">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" TabIndex="6" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                                CssClass="savebutton" Width="80px" />
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
                                <td id="tbAccruals" style="width: 100%;" class="tableHead" runat="server">
                                    <table border="0" cellpadding="0" style="width: 100%;" cellspacing="0">
                                        <tr>
                                            <td>
                                                <b>Monthly Mobile Bills</b>
                                            </td>



                                            <td style="text-align: right; padding-right: 30px; width: 80%;">
                                                <asp:Button ID="btnOutPutExcel" runat="server" Text="Export to Excel" OnClientClick="return confirm('Are u Sure to Export?')"
                                                    OnClick="BtnExportGrid_Click" CssClass="savebutton"  />
                                              
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                                    DataKeyNames="EmpId" EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found"
                                                    OnRowCommand="gveditkbipl_RowCommand">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPay" CommandName="Ded" Font-Bold="true" ForeColor="Blue" CommandArgument='<%# Eval("BillID")%>'
                                                                    ToolTip="Click to Deduct" runat="server">Deduct</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BillID" ItemStyle-HorizontalAlign="Center" HeaderText="BillID" />
                                                        <asp:BoundField DataField="TransID" HeaderText="TransID" />
                                                        <asp:BoundField DataField="SID" HeaderText="SimID" />
                                                        <asp:BoundField DataField="EmpId" HeaderText="EmpId" Visible="false"/>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Design" HeaderStyle-HorizontalAlign="Left" HeaderText="Designation" />
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lblDepartment" runat="server" ToolTip='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'
                                                                    Text='<%# GetDepartment(DataBinder.Eval(Container.DataItem, "DeptNo").ToString())%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Phone No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobile2" runat="server" Text='<%# Eval("SIMNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Month">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMonth" runat="server" Text='<%#FormatMonth(Eval("Month").ToString())%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount Limit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmountLimit" runat="server" Text='<%#Eval("AmountLimit1")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%#Eval("BillAmount1")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Extra Amount" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOverUsed" runat="server" Text='<%#Eval("OverUsedAmt")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 17px">
                                                <uc1:Paging ID="EmpListPaging" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblPay" runat="server" width="100%">
                <tr>
                    <td style="width: 94px">
                        <b>EmpID:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblEmpIDPay" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 94px">
                        <b>Name:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblNamePay" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 94px">
                        <b>Sim No:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblSimPay" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 94px">
                        <b>Amount:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblAmountPay" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 94px">
                        <b>Year:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYearPay" CssClass="droplist"  runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 94px">
                        <b>Month:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonthPay"  CssClass="droplist" runat="server">
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
                    </td>
                </tr>
                <tr>
                    <td style="width: 94px">
                    </td>
                    <td>
                        <asp:Button ID="btnPay" runat="server" Text="Submit" CssClass="savebutton" />
                    </td>
                </tr>
            </table>
       
</asp:Content>
