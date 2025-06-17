<%@ Page Title="" Language="C#"   AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="ViewFeedback.aspx.cs" Inherits="AECLOGIC.ERP.HMS.VewFeedback" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table cellpadding="0" cellspacing="0" style="width: 100%;" border="0">
         
        <tr>
            <td class="pageheader" style="width: 619px">
                Feedbacks
            </td>
            <td align="right">
                <asp:Label ID="lblDate" runat="server" CssClass="pageheader"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvFeedbacks" AutoGenerateColumns="false" CssClass="gridview" runat="server"
                    OnRowCommand="gvFeedbacks_RowCommand" OnRowDataBound="gvFeedbacks_RowDataBound">
                    <Columns>
                        <asp:BoundField Visible="false" DataField="FBId" HeaderText="FBId" />
                        <asp:BoundField DataField="EmpID" HeaderText="EmpID" />
                        <asp:BoundField DataField="FeedBackType" HeaderText="Type of Feedback" />
                        <asp:BoundField DataField="UserType" HeaderText="Type of User" />
                        <asp:BoundField DataField="Date" HeaderText="Posted On" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Mobile" HeaderText="Personal Mobile" />
                        <asp:BoundField DataField="Comment" HeaderText="Comment" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" CommandName="Del" CssClass="anchor__grd dlt" CommandArgument='<%#Eval("FBId")%>'
                                    runat="server">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 600px">
                <uc1:paging id="ViewFeedbackPaging" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 600px">
                <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
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
