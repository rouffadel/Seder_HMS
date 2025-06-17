<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucmsg.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.Templates.ucmsg" %>

<style type="text/css">
    .messagealert {
        width: 100%;
        position: fixed;
        top: 100px;
        z-index: 100000;
        padding: 0;
        font-size: 15px;
    }
</style>
<script language="javascript" type="text/javascript">

    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            case 'Warning':
                cssclass = 'alert-warning'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 5000);//5000=5 seconds
    }

</script>
<div class="messagealert" id="alert_container" style="width: 430px; margin-left: 720px">
</div>
