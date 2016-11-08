<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMain.aspx.cs" Inherits="ProjectCollection.WebUI.pages.LoginMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            background:#257fde
        }

        #main {
            background: url("../images/loginbg.png") no-repeat;
            width: 1000px;
            height: 625px;
            margin: 0 auto 0 auto;
            padding: 0;
        }
        #UserName {
            top:288px;
            left:460px;
            position: relative;
        }
        #Password {
            top:303px;
            left:460px;
            position: relative;
        }
        #Login {
            top:340px;
            left:386px;
            position: relative;
        }
        .LoginBtn {
            background: url("../images/loginbtn.gif") no-repeat;
            width: 223px;
            height: 33px;
            cursor:pointer;
        }
        input,textarea{outline:none;}
        @media screen and (-webkit-min-device-pixel-ratio:0) {
            #UserName {
                top: 287px;
                left: 460px;
                position: relative;
            }

            #Password {
                top: 298px;
                left: 460px;
                position: relative;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
            <div id="UserName">
                <asp:TextBox ID="txtLoginUserName" runat="server" BorderWidth="0"></asp:TextBox></div>
            <div id="Password">
                <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" BorderWidth="0"></asp:TextBox></div>
            <div id="Login">
                <asp:Button ID="btnLogin" runat="server" Text="" OnClick="btnLogin_Click" CssClass="LoginBtn" BorderWidth="0" /></div>
        </div>
    </form>
</body>
</html>
