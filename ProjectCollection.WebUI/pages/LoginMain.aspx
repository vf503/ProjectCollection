﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMain.aspx.cs" Inherits="ProjectCollection.WebUI.pages.LoginMain"  
EnableViewState="false" %>

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
        #FuckScannerN {
            top:288px;
            left:460px;
            position: relative;
        }
        #FuckScannerP {
            top:297px;
            left:460px;
            position: relative;
        }
        #FuckScanner {
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
    <script  type="text/javascript">
    function submitThis()
    {
        console.log(document.getElementById('txtFuckScannerN').value);
        document.getElementById('HiddenFieldN').value=document.getElementById('txtFuckScannerN').value;
        document.getElementById('HiddenFieldP').value = document.getElementById('txtFuckScannerP').value;
        document.getElementById('txtFuckScannerP').value = "";
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
            <div id="FuckScannerN">
                <input ID="txtFuckScannerN" style="border:none;" size="16" /></div>
            <div id="FuckScannerP">
                <asp:TextBox ID="txtFuckScannerP" runat="server" TextMode="Password" BorderWidth="0" size="16"></asp:TextBox></div>
            <div id="FuckScanner">
                <asp:Button ID="btnFuckScanner" runat="server" Text="" OnClick="btnLogin_Click" OnClientClick="submitThis()" CssClass="LoginBtn" BorderWidth="0" 
/></div>
        </div>
        <asp:HiddenField ID="HiddenFieldN" runat="server" />
        <asp:HiddenField ID="HiddenFieldP" runat="server" />
    </form>
</body>
</html>
