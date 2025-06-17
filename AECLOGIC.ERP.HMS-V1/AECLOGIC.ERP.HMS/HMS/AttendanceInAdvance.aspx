<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceInAdvance.aspx.cs"
    MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.AttendanceInAdvance" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gvAdvAttendance.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gvAdvAttendance.ClientID %>');
            var TargetChildControl = "chkSelect";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function GETEmp_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
        }

    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table id="tblView" runat="server" style="width: 100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                            Search Criteria</Header>
                                    <Content>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>Month:                                          
                                      <asp:DropDownList ID="ddlmonth" runat="server" Width="100" CssClass="droplist" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
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
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                            Year:
                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlyear" runat="server" CssClass="droplist" Width="100" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" />

                                                    &nbsp;&nbsp;&nbsp;                   
                                                    
                                                    <asp:Button ID="btnsearch" Width="70" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnsearch_Click" />
                                                      &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnLastWorkingDay" Text="Set Cost Center" Width="140" runat="server" CssClass="btn btn-danger" OnClick="btnLastWorkingDay_Click" />

                                                    <asp:Button ID="btnreset" Text="Reset" Width="70" runat="server" CssClass="btn btn-danger" OnClick="btnreset_Click" Visible="false" />
                                                    <asp:Button ID="btnmisngemp" Text="Missing Employees" Width="100" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="btnmisngemp_Click" />
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
            <table id="tblunsync" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:GridView ID="gvunsync" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                            OnRowCommand="gvunsync_RowCommand" OnRowDataBound="gvunsync_RowDataBound" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="ws" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwsid" runat="server" Text='<%# Eval("Site_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Worksite">
                                    <ItemTemplate>
                                        <asp:Label ID="lblws" runat="server" Text='<%# Eval("Site_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlyeargrid" Enabled="false" runat="server" Width="60" DataTextField="Name" DataValueField="ID" CssClass="droplist" DataSource='<%# bindyear()%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlmonthgrid" runat="server" Width="100" CssClass="droplist" Enabled="false">
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Computed Datetime">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllastsyncgrid" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCompute" runat="server" CssClass="btn btn-success" Text="PULL From Attendance" CommandName="com"
                                            CommandArgument='<%#Eval("Site_ID")%>' Enabled='<%# !Convert.ToBoolean(Eval("CanLock")) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Records">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdisplay" runat="server" CssClass="btn btn-primary" Text="Show" CommandName="dis"
                                            CommandArgument='<%#Eval("Site_ID")%>' ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Str Nos ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStrNos" runat="server" Text='<%# Eval("StrNos") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Miss Nos">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmisemp" runat="server" Text='<%# Eval("MissNos") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="MissNos">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkMissNo" Text='<%# Eval("MissNos") %>' CssClass="btn btn-primary" runat="server" CommandName="MissNo" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FS">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFS" Text='<%# Eval("FS") %>' CssClass="btn btn-danger" runat="server" CommandName="FS" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:Paging ID="Paging1" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                    </td>
                </tr>

            </table>
            <div id="msgempy" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblmsg" runat="server" Text="Missing Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvmsngemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="Pagingmsg" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>

                    </tr>
                </table>
            </div>
            <div id="fsemp" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblsFS" runat="server" Text="FS Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvfsemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="Pagingfs" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>

                    </tr>
                </table>
            </div>
            <div id="dvsearch" runat="server" visible="false">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td>EmpName:
                                                     <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                                                        <asp:TextBox ID="TxtEmp" Height="22px" Width="200px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="TxtEmp"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="TxtEmp"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter EmpName/ID]"></cc1:TextBoxWatermarkExtender>

                                                        <asp:Button ID="btnempsearch" Text="Search" runat="server" OnClick="btnempsearch_Click" CssClass="btn btn-primary" />


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Leave Type: 
                                                        <asp:DropDownList ID="ddlAttType" CssClass="droplist" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:Button ID="btnApplySelected" Width="70" runat="server" CssClass="btn btn-primary" Text="Apply Selected" OnClick="btnApplySelected_Click" />
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
                            <asp:GridView ID="gvAdvAttendance" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                OnRowCommand="gvAdvAttendance_RowCommand" OnRowDataBound="gvAdvAttendance_RowDataBound"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Empid" DataField="Empid" Visible="false" />--%>
                                    <asp:TemplateField HeaderText="EmpName" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemp" runat="server" Text='<%# Eval("Empid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmpName">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkemp" Text='<%# Eval("name") %>' runat="server" CommandName="att" CommandArgument='<%#Eval("Empid") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="21" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance21" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="22" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance22" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="23" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance23" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="24" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance24" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="25" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance25" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="26" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance26" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="27" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance27" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="28" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance28" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="29" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance29" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="30" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance30" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="31" Visible="true">
                                        <ItemStyle Width="50" />
                                        <ItemTemplate>
                                            <asp:DropDownList Width="50" ID="ddlAttendance31" CssClass="droplist" runat="server" DataTextField="ShortName"
                                                DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkchange" runat="server" CommandName="UPD"
                                                Text="Save" CommandArgument='<%#Bind("EmpId")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="EmployeeChangesPaging" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Table ID="tblAtt" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceInAdvance.aspx.cs" EnableEventValidation="true"
    MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.AttendanceInAdvance" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script type="text/javascript">

        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gvAdvAttendance.Rows.Count %>');
           var TargetBaseControl = document.getElementById('<%= this.gvAdvAttendance.ClientID %>');
           var TargetChildControl = "chkSelect";
           var Inputs = TargetBaseControl.getElementsByTagName("input");
           for (var iCount = 0; iCount < Inputs.length; ++iCount) {
               if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                   Inputs[iCount].checked = CheckBox.checked;
           }
       }
       function GETEmp_ID(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           //  alert(HdnKey);
           document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
        }

    </script>

    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <table id="tblView" runat="server" style="width: 100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                            Search Criteria</Header>
                                    <Content>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>Month:                                          
                                      <asp:DropDownList ID="ddlmonth" runat="server" Width="100" CssClass="droplist">
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
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                            Year:
                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:DropDownList ID="ddlyear" runat="server" CssClass="droplist" Width="100" />

                                                    &nbsp;&nbsp;&nbsp;                   
                                                    <asp:Button ID="btnLastWorkingDay" Text="Employee Salary Worksites" Width="140" runat="server" CssClass="btn btn-success" OnClick="btnLastWorkingDay_Click" />
                                                    <asp:Button ID="btnsearch" Width="70" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnsearch_Click" />
                                                    <asp:Button ID="btnreset" Text="Reset" Width="70" runat="server" CssClass="btn btn-danger" OnClick="btnreset_Click" />
                                                    <asp:Button ID="btnmisngemp" Text="Missing Employees" Width="100" runat="server" CssClass="btn btn-danger" OnClick="btnmisngemp_Click" />
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
            <table id="tblunsync" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:GridView ID="gvunsync" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                            OnRowCommand="gvunsync_RowCommand" OnRowUpdating="gvunsync_RowUpdating" OnRowDataBound="gvunsync_RowDataBound"
                            EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="ws" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwsid" runat="server" Text='<%# Eval("Site_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Worksite">
                                    <ItemTemplate>
                                        <asp:Label ID="lblws" runat="server" Text='<%# Eval("Site_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlyeargrid" Enabled="false" runat="server" Width="60" DataTextField="Name" DataValueField="ID" CssClass="droplist" DataSource='<%# bindyear()%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlmonthgrid" runat="server" Width="100" CssClass="droplist" Enabled="false">
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Computed Datetime">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllastsyncgrid" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCompute" runat="server" CssClass="btn btn-success" Text="PULL From Attendance" CommandName="com"
                                            CommandArgument='<%#Eval("Site_ID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Records">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdisplay" runat="server" CssClass="btn btn-primary" Text="Show" CommandName="dis"
                                            CommandArgument='<%#Eval("Site_ID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:Paging ID="Paging1" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                    </td>
                </tr>

            </table>
            <div id="msgempy" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblmsg" runat="server" Text="Missing Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvmsngemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="56%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="Pagingmsg" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>

                    </tr>
                </table>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="Attendviewpanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tblGvAttendance" runat="server" style="width: 100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                            Search Criteria</Header>
                                    <Content>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>EmpName:
                                                     <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                                                    <asp:TextBox ID="TxtEmp" Height="22px" Width="200px" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Employee" ServicePath="" TargetControlID="TxtEmp"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETEmp_ID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="TxtEmp"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter EmpName/ID]">
                                                    </cc1:TextBoxWatermarkExtender>

                                                    <asp:Button ID="btnempsearch" Text="Search" runat="server" OnClick="btnempsearch_Click" CssClass="btn btn-primary" />


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
                        <asp:GridView ID="gvAdvAttendance" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                            OnRowCommand="gvAdvAttendance_RowCommand" OnRowDataBound="gvAdvAttendance_RowDataBound" OnRowUpdating="gvAdvAttendance_RowUpdating"
                            EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Empid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpid" Text='<%# Eval("Empid") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Empid" DataField="Empid" Visible="false" />
                                <asp:TemplateField HeaderText="EmpName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkemp" Text='<%# Eval("name") %>' runat="server" CommandName="att" CommandArgument='<%#Eval("Empid") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="21" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance21" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="22" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance22" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="23" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance23" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="24" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance24" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="25" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance25" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="26" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance26" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="27" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance27" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="28" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance28" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="29" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance29" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="30" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance30" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="31" Visible="true">
                                    <ItemStyle Width="50" />
                                    <ItemTemplate>
                                        <asp:DropDownList Width="50" ID="ddlAttendance31" CssClass="droplist" runat="server" DataTextField="ShortName"
                                            DataValueField="ID" DataSource='<%# FillAttandanceType()%>'>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--  <asp:ButtonField Visible="true" ButtonType="Button" CommandName="UPD" Text="Save" CssClass="btn btn-primary">
                                              <%--  <asp:LinkButton ID="lnkchange" runat="server" CommandName="UPD"
                                                    Text="Save" CommandArgument='<%#Bind("EmpId")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                        </asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="AddButton" runat="server"
                                            CommandName="AddToCart"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                            Text="Add to Cart" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmployeeChangesPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Table ID="tblAtt" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                        </asp:Table>
                    </td>
                </tr>
            </table>

        </ContentTemplate>

        <%--   <Triggers>
        <%--<asp:AsyncPostBackTrigger ControlID="btnSaveUpdate" EventName="Click" />   
        <asp:AsyncPostBackTrigger ControlID="gvAdvAttendance"  />        
    </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnSaveUpdate" Width="70" runat="server" CssClass="btn btn-primary" Text="Update All" OnClick="btnSaveUpdate_Click" />

    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
--%>