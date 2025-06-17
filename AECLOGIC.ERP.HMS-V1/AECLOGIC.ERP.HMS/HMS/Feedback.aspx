<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="Feedback.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Feedback" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function validate() {

            if (document.getElementById('<%=txtFeedback.ClientID%>').value == "") {
                alert("Please Enter Feedback!");
                document.getElementById('<%=txtFeedback.ClientID%>').focus();
                return false;
            }
        }
    </script>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
       
        <tr>
            <td class="pageheader" style="width: 130px">
                Feedback
            </td>
            <td align="right">
                <asp:Label ID="lblDate" runat="server" CssClass="pageheader"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 130px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 130px">
                <b>&nbsp;Type Of Feedback:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlFbtype" CssClass="droplist" runat="server" 
                    Style="margin-left: 0px" TabIndex="1">
                    <asp:ListItem Value="TestData1">Compliment</asp:ListItem>
                    <asp:ListItem Value="TestData2">Suggestion</asp:ListItem>
                    <asp:ListItem Value="TestData3">Enquiry</asp:ListItem>
                    <asp:ListItem Value="TestData4">Complaint</asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 130px">
                <b>&nbsp;User Type: </b>
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" CssClass="droplist" AutoPostBack="true" runat="server"
                    OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" TabIndex="2">
                    <asp:ListItem Value="Data1">Employee</asp:ListItem>
                  
                </asp:DropDownList>
                &nbsp;* Select <b>Anonimus</b> If you don&#39;t want to give your Name<br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:MultiView ID="mvanonimus" Visible="false" runat="server">
                    <asp:View ID="viewanonimus" runat="server">
                        <table>
                            <tr>
                                <td class="style2" style="width: 124px">
                                    <b>Name:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" Width="300Px" runat="server" TabIndex="3"></asp:TextBox>
                                    &nbsp;<br />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2" style="width: 124px">
                                    <b>Mobile:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhone" Width="200Px" runat="server" TabIndex="4"></asp:TextBox>
                                    &nbsp;<br />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 130px">
                <b>&nbsp;Comment:<span style="color: #ff0000">*</span></b>
            </td>
            <td>
                <asp:TextBox ID="txtFeedback" runat="server" BorderColor="#CC6600" BorderStyle="Outset"
                    Rows="10" TextMode="MultiLine" Width="60%"
                    Height="70px" TabIndex="5"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFeedback"
                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter Your Comments here..]">
                </cc1:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqStatus" ValidationGroup="save" ControlToValidate="txtFeedback" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 130px">
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 130px">
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" ValidationGroup="save" OnClick="btnSubmit_Click"
                     AccessKey="s" TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn btn-danger" 
                    OnClick="btnCancel_Click" AccessKey="b" TabIndex="7" 
                    ToolTip="[Alt+b  OR  Alt+b+Enter]" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
