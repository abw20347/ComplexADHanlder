<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ADAuthModule.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: Arial, Helvetica, sans-serif; font-size: x-small">
        <asp:LinkButton ID="lnkLogin" runat="server" OnClick="LinkButton1_Click">Login with...</asp:LinkButton>
        <br /><br />
        User: <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
        <br />
        Groups:       
        <br />
        <asp:Label ID="lblGroups" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
