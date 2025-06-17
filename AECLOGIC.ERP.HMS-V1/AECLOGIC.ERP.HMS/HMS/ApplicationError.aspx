<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ApplicationError.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ApplicationError" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ MasterType VirtualPath="~/Templates/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function View(ModiD, Company) {
            window.open("aeclogic.com?Mod=" + ModiD + "&Cname=" + Company);
        }
    </script>
    <table width="100%" style="background-color: Silver">
        <tr>
            <td>
                <table width="100%" style="background-color: #efefef; border-width: 2px; border-style: outset;
                    border-color: Black; text-align: center">
                    <tr style="background-color: Silver">
                        <td align="left" style="height: 60px; vertical-align: middle; background-color: #FFFFFF;"
                            valign="middle">
                            <a href="http://www.aeclogic.com" target="_blank">
                                <img alt="AEC Logic" id="Img4" src="IMAGES/AEClogo.gif" style="border-width: 0px;
                                    vertical-align: middle; height: 60px" /></a> <span id="Span7" style="font-family: Verdana;
                                        color: #cc4a01; font-size: 20px; height: 60px">AEC</span> <span id="Span8" style="font-family: Verdana;
                                            color: #777777; font-size: 20px; height: 60px">LOGIC</span>
                            <tr>
                                <td style="font-size: large; text-align: left; height: 24px; color: Red;">
                                    <b>We are sorry to inform that some problem occurred in accessing this page.</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: medium; text-align: left; color: Black;">
                                    We have tried to remove all inconsistencies from the program, however there might
                                    be some unforeseen circumstances we had omitted in this page. We shall try to resolve
                                    this issue if you could inform us what steps you had performed to reach this page
                                    error at <a href="mailto:support@aeclogic.com">support@aeclogic.com</a> and/or post
                                    a comment/blog on this link <a id="ErrorID" runat="server" target="_blank">Here</a>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: medium; text-align: justify; color: Black;">
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: medium; text-align:left; color: Black;">
                                    <a href="AdminDefault.aspx" style="color: Blue" target="_blank" />Home page
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr style="background-color: Silver">
                                <td style="height: 60px; vertical-align: middle; background-color: #FFFFFF;" align="right">
                                    � 2011 AEC Logic Pvt Ltd. All rights reserved.
                                </td>
                            </tr>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

