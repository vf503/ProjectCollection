<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ProjectCollection.WebUI.pages.login" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>用户名</td>
                    <td>
                        <asp:TextBox ID="txtFuckScannerN" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>密码</td>
                    <td>
                        <asp:TextBox ID="txtFuckScannerP" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnFuckScanner" runat="server" Text="登录" OnClick="btnLogin_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
