<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BIDashBoard.aspx.cs" Inherits="AECLOGIC.ERP.HMS.BIDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <script type="text/javascript">
        //Configure IFrame for the tile after you have an Access Token. See Default.aspx.cs to learn how to get an Access Token
        window.onload = function () {
            if ("" != document.getElementById('ContentPlaceholder1_accessToken').value)
            {
                var iframe = document.getElementById('iFrameEmbedTile');

                // To load a tile do the following:
                // Set the IFrame source to the EmbedUrl from the Get Tiles operation
                iframe.src = document.getElementById('ContentPlaceholder1_tileEmbedUrl').value;

                // Add an onload handler to submit the access token
                iframe.onload = postActionLoadTile;
            }
        };
        // Post the access token to the IFrame
        function postActionLoadTile() {
            // Construct the push message structure
            // This is where you assign the Access Token to get access to the tile visual
            var messageStructure = {
                action: "loadTile",
                accessToken: document.getElementById('ContentPlaceholder1_accessToken').value,
                height: 500,
                width: 500
            };
            message = JSON.stringify(messageStructure);

            // Push the message
            document.getElementById('iFrameEmbedTile').contentWindow.postMessage(message, "*");;
        }
    </script>
    <asp:HiddenField ID="accessToken" runat="server" />
    <asp:Button ID="getTileButton" runat="server" OnClick="getTileButton_Click" Text="Get Tile" />
    <table>
        <tr style="display:none"><td></td> <td><asp:Textbox ID="tileEmbedUrl" runat="server" Width="900px" ></asp:Textbox></td></tr>

        <tr style="display:none"><td>Dashboard Tile</td><td></td></tr>
       <%-- <tr><td></td><td>
            <iframe ID="iFrameEmbedTile" height="600px" width="1024px"></iframe>
        </td></tr>--%>
        <tr><td><asp:HiddenField ID="tilecount" ClientIDMode="Static" runat="server" /></td></tr>
        <tr><td><asp:Panel runat="server" ID="divMain"></asp:Panel></td></tr>
    </table>
</asp:Content>