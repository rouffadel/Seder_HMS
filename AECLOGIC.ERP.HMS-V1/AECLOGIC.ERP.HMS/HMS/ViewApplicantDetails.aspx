<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewApplicantDetails.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.ViewApplicantDetails" Title="View Applicant Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">
      function back() {
          history.go(-1); 
          //window.history.forward(1);
      }
    
</script>
    <table style="width: 100%">
<tr><td> 
    <asp:Button ID="Button2" onclick="Button1_Click" runat="server" CssClass="btn btn-primary" 
        Text="Back" />
    </td></tr>
        <tr>
            <td colspan="2" style="width: 200px" class="pageheader">
                Applicant Details
            </td>
        </tr>
        <tr>
            <td>
                DATE:
            </td>
            <td>
                <asp:Label ID="lblDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Job Applied For:
            </td>
            <td>
                <asp:Label ID="lblAppliedFor" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <asp:Label ID="lblFname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Middle Name:
            </td>
            <td>
                <asp:Label ID="lblMname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Surname:
            </td>
            <td>
                <asp:Label ID="lblSurname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Email-ID
            </td>
            <td>
                <asp:Label ID="lblEmailID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Mobile
            </td>
            <td>
                <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Phone
            </td>
            <td>
                <asp:Label ID="lblPhone" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Date of Birth
            </td>
            <td>
                <asp:Label ID="lblDOB" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Father's Name
            </td>
            <td>
                <asp:Label ID="lblFathersName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Passport No
            </td>
            <td>
                <asp:Label ID="lblPassportNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Passport Date of Issue
            </td>
            <td>
                <asp:Label ID="lblpdoi" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Passport Date of Issue Place
            </td>
            <td>
                <asp:Label ID="lblpdoip" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Passport Date of Expiry
            </td>
            <td>
                <asp:Label ID="lblpdoe" runat="server"></asp:Label>
            </td>
        </tr>
     
        <tr>
            <td>
                Matrial Status
            </td>
            <td>
                <asp:Label ID="lblMaritialStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Resume
            </td>
            <td>
             
                
                 <asp:HyperLink ID="hyperlink1" runat="server" Text="View" Target="_blank" CssClass="btn btn-success"></asp:HyperLink>
            </td>
        </tr>
        
        
       
        
        
        
        <tr>
            <td colspan="2" class="tvOrgPrjMan">
                Academic Details:
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="dgvQualDetails" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                    GridLines="Both" Width="80%" CellPadding="4" HeaderStyle-CssClass="tableHead" CssClass="gridview"
                    AllowSorting="true" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                    <Columns>
                        <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQualification" runat="server" Text='<%#Eval("Qualification")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Institute" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblInstitute" runat="server" Text='<%#Eval("Institute")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YOP" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblYOP" runat="server" Text='<%#Eval("YOP")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specialization" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblSpecialization" runat="server" Text='<%#Eval("Specialization")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblPercentage" runat="server" Text='<%#Eval("Percentage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education Type" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblMode" runat="server" Text='<%#Eval("Mode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
               &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" class="tvOrgPrjMan">
                Previous Employment History:
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="dgvPreviousEmpHist" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                    ForeColor="#333333" GridLines="Both" Width="80%" CellPadding="4" HeaderStyle-CssClass="tableHead"
                    AllowSorting="true" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                    <Columns>
                        <asp:TemplateField HeaderText="Organization" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblOrganization" runat="server" Text='<%#Eval("Organization")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FromDate" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ToDate" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CTC" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblCTC" runat="server" Text='<%#Eval("CTC")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="tvOrgPrjMan">
                Remarks List
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gdvRemarks" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                    GridLines="Both" Width="80%" CellPadding="4" HeaderStyle-CssClass="tableHead"
                    AllowSorting="true" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Ranking")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                        <asp:TemplateField HeaderText="Remarked By" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarkedBY" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 20px; vertical-align: top">
                Remarks
            </td>
            <td style="height: 20px">
                <asp:TextBox ID="txtRemarks" runat="server" Height="50px" TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 19px;">
                Ranking
            </td>
            <td style="height: 19px">
                <asp:RadioButtonList RepeatDirection="Horizontal" ID="rblRanking" runat="server">
                    <asp:ListItem Selected="True">1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="height: 19px;">
                Result
            </td>
            <td style="height: 19px">
                <asp:DropDownList ID="ddlApplicantStatus" runat="server" CssClass="droplist" >
                    <asp:ListItem Text="Next Round" Value="1"></asp:ListItem> 
                    <asp:ListItem Text="Rejected" Value="0"></asp:ListItem> 
                    <asp:ListItem Text="Selected" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnRemarks" runat="server" Text="Submit" 
                    OnClick="btnRemarks_Click" CssClass="btn btn-success" />
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" 
                   onmouseup="history.go(-1)" onclick="Button1_Click" Text="Back" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <span class="SubModuleName">Current CTC </span>:
                <asp:Label ID="lblCurrentCTC" runat="server"></asp:Label>
                <br />
                <span class="SubModuleName">Expected CTC </span>:
                <asp:Label ID="lblExpectedCTC" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
