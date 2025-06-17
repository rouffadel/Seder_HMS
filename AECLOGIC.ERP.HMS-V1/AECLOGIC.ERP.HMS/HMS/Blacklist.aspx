<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blacklist.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.HMS.Blacklist" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
        
   
        function GetworkID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=Txtwrk_hid.ClientID %>').value = HdnKey;
        }

        function GetDeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=TxtDept_hid.ClientID %>').value = HdnKey;
        }

        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
             //alert(HdnKey);
            document.getElementById('<%=textsearchemp_hid.ClientID %>').value = HdnKey;
        }
        function showApp(EmpID) {
            // window.showModalDialog("../HMS/Checkcustodi.aspx?Empid=" + EmpID, "", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
            window.showModalDialog("../HMS/Checkcustodi.aspx?Empid=" + EmpID, "", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
        }
    </script>

    <table>
        <tr>
            <td>
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
        <table id="tblvew" runat="server">
            <tr>
                 <td>
                      <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="110%">
                                 <Panes>
                             <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                                Search Criteria
                                           </Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td>Worksite 
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="Txtwrk_hid" runat="server" />
                                                        <asp:TextBox ID="Txtwrk" OnTextChanged="GetDept"  AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                       <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptWorkList" ServicePath="" TargetControlID="Txtwrk"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetworkID">
                                                        </cc1:AutoCompleteExtender>

                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="Txtwrk"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td>Department
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="TxtDept_hid" runat="server" />
                                                        <asp:TextBox ID="TxtDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="TxtDept"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="GetDeptID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="TxtDept"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td>EmployeeName/ID&nbsp
                                                       <asp:HiddenField ID="textsearchemp_hid" runat="server" />
                                                   <asp:TextBox ID="textsearchemp" Height="22px" Width="250px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListemp" ServicePath="" TargetControlID="textsearchemp"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempID" >
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textsearchemp"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        &nbsp;&nbsp
                                                    </td>
                                                  
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click" />
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
                            <asp:GridView ID="gvEmPCriteria2" runat="server" AutoGenerateColumns="False" Width="100%"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvEmPCriteria2_RowCommand" AlternatingRowStyle-BorderColor="GhostWhite">
                                <Columns>
                                    <asp:TemplateField HeaderText="EMpid" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Empid" runat="server" Visible="true" Text='<%#Eval("Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="site_name" HeaderText="Worksite" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                   <asp:BoundField DataField="departmentname" HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Visible="true" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EmpName" CommandName="Name" CommandArgument='<%#Eval("Empid") %>' runat="server" Visible="true" Text='Select'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                            </asp:GridView>
                        </td>
            </tr>
             
        </table>
       
                    <tr id="finyear" runat="server"><td>
                        Name:<asp:Label ID="lblEmp" runat="server" Font-Bold="true"></asp:Label>
                        <br />
                        Month
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlSMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]"  >
                                                                    <asp:ListItem Value="0">--ALL--</asp:ListItem>
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
                        <br />
                            Financial Year&nbsp
                                                                <asp:DropDownList ID="DDlFinyr" runat="server" Visible="true" Width="100px" MaxLength="50" TabIndex="2">
                                                                </asp:DropDownList>
                                                        &nbsp;&nbsp
                        <br />
                        <asp:Button ID="btnEmpcrsearch" runat="server" Text="Search" OnClick="btnEmpcrsearch_Click" CssClass="savebutton"/>
                      <%--  <asp:Button ID="btnAddCret" runat="server" Text="ADD" OnClick="btnAddCret_Click" CssClass="savebutton"/>--%>
                        </td>
                    </tr>
            
                    <tr>
                        <td>
                             <asp:GridView ID="gvEmpcrdet" runat="server" AutoGenerateColumns="False" Width="100%"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvEmpcrdet_RowCommand" AlternatingRowStyle-BorderColor="GhostWhite">
                                <Columns>
                                    <asp:TemplateField HeaderText="EMpid" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Empid" runat="server" Visible="true" Text='<%#Eval("Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Visible="true" Text='<%#Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Month" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Visible="true" Text='<%#Eval("Month") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonthName" runat="server" Visible="true" Text='<%#Eval("MonthName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FinancialYear">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Visible="true" Text='<%#Eval("FinancialYear") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Visible="true" Text='<%#Eval("Total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EmpName" CommandName="Name" CommandArgument='<%#Eval("Empid") %>' runat="server" Visible="true" Text='Select'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" AlternatingRowStyle-BorderColor="GhostWhite">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Visible="true" Text='<%#Eval("Name") %>'></asp:Label>
                                        </HeaderTemplate>

                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="EMpid" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Empid" runat="server" Visible="true" Text='<%#Eval("Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--  <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="EmpName" runat="server" Visible="true" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="CriteriaID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="CtrID" runat="server" Visible="true" Text='<%#Eval("CriteriaID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Criteria Name">
                                        <ItemTemplate>
                                            <asp:Label ID="CtrName" runat="server" Visible="true" Text='<%#Eval("Criteria") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weightage %">
                                        <ItemTemplate>
                                            <asp:Label ID="wtgprsnt" runat="server" Visible="true" Text='<%#Eval("WeightagePercent") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                <asp:Label ID="lbltotalwqty" runat="server" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Evaluation %">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Evalution" runat="server" Visible="true" Enabled="false" onkeypress="javascript:return isNumber(event)" Text='<%#Eval("Evalution") %>'></asp:TextBox>
                                            <%--<asp:Label ID="Evaluation" runat="server" Visible="true"></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Performance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperf" runat="server" Visible="true" Text='<%#Eval("Performance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty" runat="server" />

                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDEL" runat="server" Text="Delete" CommandArgument='<%#Eval("Empid") %>'
                                                CommandName="DEL"></asp:LinkButton>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    --%>
                                </Columns>
                                <FooterStyle BackColor="#cccccc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                  <%-- <tr id="HideGrid" runat="server">
                        <td colspan="3" align="center">
                            <asp:Button ID="ButtonAdd" runat="server" Text="Save"  CssClass="savebutton" OnClick="ButtonAdd_Click" />
                            <asp:Button ID="ADDCriteria" runat="server"  CssClass="savebutton" Text="ADD New Criteria" OnClick="ADD_Click" />
                            <asp:Button ID="Hide" runat="server" Text="Back" OnClick="Hide_Click" CssClass="savebutton" />
                       </td>
                    </tr>--%>


                   





    </table>

    <table>
       <tr>
                        <td colspan="2" style="height: 17px">
                            <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" />
                        </td>
                    </tr>
    </table>







</asp:Content>
