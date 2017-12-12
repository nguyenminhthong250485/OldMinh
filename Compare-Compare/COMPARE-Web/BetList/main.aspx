<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="BetList.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BetList</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/style.css">
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700' rel='stylesheet' type='text/css'>
    <script type="text/javascript" language="javascript">
        var idle = setInterval("reloadPage()", 20000);
        function reloadPage() {
            location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblOutput" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
