<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectPlanListEmbed.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectPlanListEmbed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../pages/common/Site.css" rel="stylesheet" />
    <script type="text/javascript" src="../../script/jquery-1.8.2.min.js"></script>
    <script src="../script/jquery-ui.js"></script>
    <link type="text/css" href="../script/jQuery-Timepicker-Addon/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="../script/jQuery-Timepicker-Addon/jquery-ui-timepicker-addon.css" type="text/css" />
    <script src="../script/jQuery-Timepicker-Addon/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="../script/js/jquery.ui.datepicker-zh-CN.js.js" charset="gb2312"></script>
    <script src="../script/js/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    // 任何需要执行的js特效 
        //    $(document).scrollTop($.getUrlParam('scrolltop'));
        //});
        (function ($) {
            $.getUrlParam
             = function (name) {
                 var reg
                  = new RegExp("(^|&)" +
                  name + "=([^&]*)(&|$)");
                 var r
                  = window.location.search.substr(1).match(reg);
                 if (r != null) return unescape(r[2]); return null;
             }
        })(jQuery);
    </script>
    <style type="text/css">
        html {
            _background-image: url(about:blank);
            _background-attachment: fixed;
        }

        .FixedDiv {
            width: 100%;
            height: 30px;
            position: fixed;
            top: 0;
            opacity: 0.75;
            _position: absolute;
            _bottom: auto;
            _top: expression(eval(document.documentElement.scrollTop));
        }

        .hidden {
            display: none;
            margin: 0;
            padding: 0;
            width: 0;
            height: 0;
        }

        .MainList {
            width: 1000px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="gvProjectPlan" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectPlanId" OnSelectedIndexChanging="gvProjectPlan_SelectedIndexChanging" OnRowDataBound="gvProjectPlan_RowDataBound"
            OnPageIndexChanging="gvProjectPlan_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Vertical" AllowPaging="True" PageSize="15" Width="1000px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ProjectPlanNo" HeaderText="项目计划编号" />
                <asp:BoundField DataField="Title" HeaderText="标题" />
                <asp:BoundField DataField="ProjectPlanTypeText" HeaderText="项目类型" />
                <asp:BoundField DataField="PlanDate" HeaderText="计划时间" DataFormatString="{0: yyyy年MM月dd日}" />
                <asp:BoundField DataField="ProgressText" HeaderText="进度" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="aSelect" runat="server" Target="_parent">选择</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#D6EBFE" Font-Bold="True" ForeColor="#3285e5" Font-Names="Microsoft YaHei" Height="40px" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </form>
</body>
</html>
