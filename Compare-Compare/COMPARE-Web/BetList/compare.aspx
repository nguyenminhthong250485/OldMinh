<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compare.aspx.cs" Inherits="BetList.compare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetList</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/style.css">
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700' rel='stylesheet' type='text/css'>
    <script type="text/javascript" language="javascript">
        var idle = setInterval("reloadPage()", 2000);
        function reloadPage() {
            location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3 style="color:red"><b>Non-Live</b></h3>
        <asp:Label ID="lblNonLive" runat="server" Text="Label"></asp:Label>
        <br />
        <h3 style="color:blue"><b>Live</b></h3>
        <asp:Label ID="lblLive" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>