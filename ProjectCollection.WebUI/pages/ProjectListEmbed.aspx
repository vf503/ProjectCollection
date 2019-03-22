<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectListEmbed.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectListEmbed" %>

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

        .LinkBtn {
            cursor:pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvProject" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="10" CssClass="MainList" DataKeyNames="ProjectId" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="gvProject_PageIndexChanging" OnRowCommand="CustomersGridView_RowCommand" OnRowDataBound="gvProject_RowDataBound" OnSelectedIndexChanging="gvProject_SelectedIndexChanging" PageSize="30">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="45px" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:Label ID="LabelAll" runat="server" Font-Size="14px" Text="全选:"></asp:Label>
                                <asp:CheckBox ID="CheckBoxAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxAll_Changed" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="SelectCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="btnBatch_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectNo" HeaderText="工单编号" ItemStyle-Width="115px" />
                        <asp:BoundField DataField="CourseName" HeaderText="课件标题" />
                        <asp:BoundField DataField="ProjectPlanName" HeaderText="专题名" />
                        <asp:BoundField DataField="lecturer" HeaderStyle-Width="60px" HeaderText="主讲人" />
                        <%--<asp:BoundField DataField="WorkTypeText" HeaderText="用途" />--%>
                        <asp:BoundField DataField="ProgressText" HeaderText="进度" />
                        <asp:BoundField DataField="SendingDate" DataFormatString="{0: yy年MM月dd日}" HeaderText="派单时间" ItemStyle-Width="90px" />
                        <asp:TemplateField ControlStyle-Width="30px">
                            <ItemTemplate>
                                <asp:HyperLink ID="aSelect" runat="server" Target="_parent">选择</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span>讲师信息</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelInfo" Width="100" runat="server" Text="无讲师信息"  ToolTip="" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#D6EBFE" Font-Bold="True" Font-Names="Microsoft YaHei" ForeColor="#3285e5" Height="40px" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <asp:HyperLink ID="aBatchSave" runat="server" Target="_parent" CssClass="LinkBtn" Visible="false">批量保存</asp:HyperLink>
                <asp:HyperLink ID="aBatchHandle" runat="server" Target="_parent" CssClass="LinkBtn" Visible="false">批量处理</asp:HyperLink>
                <asp:Button ID="btnBatchDownload" runat="server" Text="打包音频" OnClick="btnBatchDownload_Click" Visible="false" />
                <asp:HyperLink ID="aDownload" runat="server"  Visible="false">点击下载</asp:HyperLink>
                <asp:Label ID="tips" runat="server"></asp:Label>
                <asp:HiddenField ID="hidBatchId" runat="server"/>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<asp:Button ID="btnBatchSave" runat="server" Text="批量保存" OnClick="btnBatch_Click" Visible="false" />
        <asp:Button ID="btnBatchHandle" runat="server" Text="批量处理" OnClick="btnBatch_Click" Visible="false" />--%>
    </form>

</body>
</html>
