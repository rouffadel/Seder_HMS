<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="UploadControl.ascx.cs"
    Inherits="AECLOGIC.ERP.COMMON.UploadControl" %>
<%-- Register the FlashUpload control so we can use it below --%>
<%@ Register Assembly="FlashUpload" Namespace="FlashUpload" TagPrefix="FlashUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">

    function ShowUploadCtrl() {
        $find('<%=mpeUploadCtrl.ClientID %>').show();
        return false;
    }
    function CloseUploadCtrl() {
        $find('<%=mpeUploadCtrl.ClientID %>').hide();
        return false;
    }
    function UploadComplete() { __doPostBack('<%=LinkButton1.ClientID %>', ''); }
</script>

<asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
<div style="left: 0px; top: 0px; position: absolute; width: 100%; height: 100%; text-align: center;
    vertical-align: middle;">
    <cc1:modalpopupextender id="mpeUploadCtrl" runat="server" targetcontrolid="btnShowPopup"
        x="360" popupcontrolid="pnlUploadCtrl" backgroundcssclass="modalBackground" dropshadow="true"
        okcontrolid="btnUploadCtrlClose" repositionmode="RepositionOnWindowScroll" cancelcontrolid="btnUploadCtrlClose" />
    <asp:Panel ID="pnlUploadCtrl" runat="server" Width="550px" Style="display: none;
        background-color: #fff;">
        <asp:ImageButton ID="btnUploadCtrlClose" OnClientClick="javascript:return CloseUploadCtrl();"
            ImageUrl="~/Images/img-close-off.gif" ImageAlign="Right" runat="server" /><br />
        <table style="margin: 10px 10px 10px 10px; width: 100%">
            <tr>
                <th>
                    Upload files
                </th>
            </tr>
            <tr>
                <td>
                    <div>
                        <%--
            The flash upload control.  Source is located in the FlashUplod project with this solution.
            The control encapsulates the creation of the flash object and embeds the .swf file
            within the FlashUpload.dll.
            
            Parameters:
                UploadPage: the page to upload to. This can be a HttpHandler, an .aspx page, an .asp page;
                            any page that can accept uploaded files.  This sample project uses a HttpHandler
                            which in my opinion is the best option.
                OnUploadComplete:   A javascript function which is called after all the files are uploaded. Width="550" Height="375"
        --%>
                        <FlashUpload:FlashUpload ID="flashUpload" runat="server" UploadPage="Upload.axd"
                            OnUploadComplete="UploadComplete()" FileTypeDescription="Images"
                            FileTypes="*.gif; *.png; *.jpg; *.jpeg;*.pdf;*.doc;*.xls;*.docx;*.xlsx" UploadFileSizeLimit="2097152"
                            TotalUploadSizeLimit="20971520" />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
