<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="ContributionConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ContributionConfig" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">


    <asp:HiddenField ID="hdnEMPId" runat="server" />
    <table style="width: 100%">
        <tr>
            <td>
                 <asp:RadioButtonList ID="rblstContribution" runat="server" DataTextField="NAME" DataValueField="Itemid " onselectedindexchanged="rblstContribution_SelectedIndexChanged" AutoPostBack="true">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" id="tdPayrole" runat="server" visible="false">
                <ajaxToolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <ajaxToolkit:AccordionPane ID="paneWages" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Wages</b></Header>
                            <Content>
                                <asp:CheckBoxList ID="cblWages" runat="server" DataTextField="Name" DataValueField="WagesID" >
                                </asp:CheckBoxList>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="paneAllowences" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Allowances</b></Header>
                            <Content>
                                <asp:CheckBoxList ID="cblAllowences" runat="server" DataTextField="Name" DataValueField="AllowId">
                                </asp:CheckBoxList>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="paneContribution" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                <b>Company Contribution</b></Header>
                            <Content>
                                <asp:CheckBoxList ID="cblContributions" runat="server" DataTextField="NAME" DataValueField="Itemid ">
                                </asp:CheckBoxList>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="paneDeduct" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Deduct Statutory</b></Header>
                            <Content>
                                <asp:CheckBoxList ID="cblDeductions" runat="server" DataTextField="NAME" DataValueField="Itemid ">
                                </asp:CheckBoxList>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                    </Panes>
                </ajaxToolkit:Accordion>
            </td>
        </tr>
         <tr>
                        <td>
                        <asp:Button ID="btnAll" runat="server" Text="Configure" CssClass="savebutton" 
                                onclick="btnAll_Click"/>
                        </td>
                    </tr>
    </table>
</asp:Content>

