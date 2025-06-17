<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeEvalutionBlockList.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.EmployeeEvalutionBlockList" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CalPercentage() {
            var grid = document.getElementById("<%= GridView1.ClientID%>");
            for (var i = 0; i < grid.rows.length - 1; i++) {
                var txtAmountReceive = $("input[id*=Evalution]")
                var str = txtAmountReceive[i].Value;
                var weight = $("input[id*=wtgprsnt]")
                var weight = weight[i].Value;
                var TAX = (str * weight) / 100;
            }
        }
        function GetDeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=TxtDept_hid.ClientID %>').value = HdnKey;
        }

        function GetWorkID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=Txtwrk_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 90%; height: 100%">
     
       
        <tr>
            <td>
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
                                                        <asp:TextBox ID="Txtwrk" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptWorkList" ServicePath="" TargetControlID="Txtwrk"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetWorkID">
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
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetDeptID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="TxtDept"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td>EmployeeName/ID&nbsp
                                                           <asp:TextBox ID="txtEmpID" Visible="false" runat="server">
                                                           </asp:TextBox>
                                                        <asp:TextBox ID="textsearchemp"  Height="22px" Width="250px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListemp" ServicePath="" TargetControlID="textsearchemp"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="textsearchemp"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name/ID]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        &nbsp;&nbsp
                                                    </td>
                                                  
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                            <asp:GridView ID="gvEmPCriteria" runat="server" AutoGenerateColumns="False" Width="100%"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvEmPCriteria_RowCommand" AlternatingRowStyle-BorderColor="GhostWhite">
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
                                            <asp:LinkButton ID="EmpName" CommandName="Name" CommandArgument='<%#Eval("Empid") %>' runat="server" Visible="true" Text='Select' CssClass="btn btn-primary"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
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
                        <asp:Button ID="btnEmpcrsearch" runat="server" Text="Search" OnClick="btnEmpcrsearch_Click" CssClass="btn btn-primary"/>
                          <asp:Button ID="Hide" runat="server" Text="Back" OnClick="Hide_Click"  CssClass="btn btn-primary" />
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
                                            <asp:Label ID="Label1" runat="server" Visible="true" Text='<%#Eval("Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpN" runat="server" Visible="true" Text='<%#Eval("Name") %>'></asp:Label>
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
                                            <asp:LinkButton ID="LinkButton1" CommandName="Name" CommandArgument='<%#Eval("Empid") %>' runat="server" Visible="true" Text='Select' CssClass="btn  btn-primary"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="150%"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="EmptyRowData" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" AlternatingRowStyle-BorderColor="GhostWhite">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label3" runat="server" Visible="true" Text='<%#Eval("Name") %>'></asp:Label>
                                        </HeaderTemplate>

                                    </asp:TemplateField>
                                 
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
                                            <asp:Label ID="Evalution" runat="server" Visible="true" Text='<%#Eval("Evalution") %>'></asp:Label>
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
                                  
                                </Columns>
                                <FooterStyle BackColor="#cccccc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                  <tr id="HideGrid" runat="server">
                        <td colspan="3" align="center">
                
                          
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="2" style="height: 17px">
                            <uc1:Paging ID="EmpListPaging" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    
    </table>
</asp:Content>
