<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="OpenIDWebsite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OpenID Authentication</title>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:Panel ID="pnlSelectProvider" runat="server">
        <h1>OpenID Login</h1>
        <p>Please select your openID provider.</p>
        <ul>
            <li><asp:LinkButton ID="openIDGoogle" runat="server" onclick="openIDGoogle_Click">Google</asp:LinkButton></li>
            <li><asp:LinkButton ID="openIDAOL" runat="server" onclick="openIDAOL_Click">AOL</asp:LinkButton></li>
            <li><asp:LinkButton ID="openIDFB" runat="server" onclick="openIDFB_Click">Facebook</asp:LinkButton></li>
            <li><asp:LinkButton ID="openIDYahoo" runat="server" onclick="openIDYahoo_Click">Yahoo!</asp:LinkButton></li>
        </ul>
    </asp:Panel>
    <asp:Panel ID="pnlUserInfo" runat="server">
        <h1>OpenID User Information</h1>
        <asp:Label ID="lblFirstName" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblLastName" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblFullName" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblEmail" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblAlias" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblFriendlyIdentifier" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblIdentifier" runat="server"></asp:Label>
        <br />
        <asp:LinkButton ID="logout" runat="server" onclick="logout_Click">Logout</asp:LinkButton>
    </asp:Panel>
    </form>
</body>
</html>