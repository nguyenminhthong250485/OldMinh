<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compare.aspx.cs" Inherits="BetList.compare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetList</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/style.css">
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700' rel='stylesheet' type='text/css'> 
    <script src="js/jquery-1.11.1.min.js"></script>    
    <script>
        var idle = setInterval("reloadPage()", 2000);
        function reloadPage() {
            if (sessionStorage.getItem("ss_run") != "0")
                location.reload();
        }

        $(document).ready(function () {
            $("#Button1").click(function () {               
                sessionStorage.setItem("ss_run", "0");
            });
        });

        $(document).ready(function () {
            $("#Button2").click(function () {            
                sessionStorage.setItem("ss_run", "1");                
            });
        });
    </script>
</head>
<body>
    <input type="button" value="Start" name="Button2" id="Button2">
    <input type="button" value="Stop" name="Button1" id="Button1">        
    <form id="form1" runat="server">                          
        <div>
            <asp:Label ID="lblNonLive" runat="server" Text="Label"></asp:Label>            
        </div>
         <div>
            <asp:Label ID="lblMinus" runat="server" Text="Label"></asp:Label>            
        </div>
    </form>
</body>
</html>