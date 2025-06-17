<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PrintOfferLetter.aspx.cs" ValidateRequest="false" Inherits="AECLOGIC.ERP.HMS.PrintOfferLetter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function LoadContentCleanUp()
        {
            var content = opener.document.getElementById('reviewContent').innerHTML;
            document.write(content);
            var inputs = document.getElementsByTagName("INPUT");
            for (var i = 0; i < inputs.length; i++)
            {
                inputs[i].style.display = "none";
            }
        
            window.print();
            document.getElementById('printSpan').style.display = "none";
            
        }
    </script>
    <style type="text/css">
    
        .CleanTable
        {
        	border:solid 1px #333333;
        }
        .CleanTable td
        {
        	border:solid 1px #333333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <script type="text/javascript">
        LoadContentCleanUp();
    </script>
    </div>
    </form>
</body>
</html>
