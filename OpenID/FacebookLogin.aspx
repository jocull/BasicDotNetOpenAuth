<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacebookLogin.aspx.cs" Inherits="OpenID.FacebookLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml">
<head runat="server">
    <title>Facebook Login</title>
    <script type="text/javascript" src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php"></script>
    <script type="text/javascript">
        FB.init("<%= FBAPIKey %>", "xd_receiver.htm", { "reloadIfSessionStateChanged": true });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fb:login-button onlogin="window.location.reload()"></fb:login-button>
    </div>
    </form>
</body>
</html>