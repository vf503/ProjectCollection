<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCountNew.aspx.cs" Inherits="ProjectCollection.WebUI.pages.CustomCountNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link type="text/css" href="../../script/jQuery-Timepicker-Addon/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../script/jQuery-Timepicker-Addon/jquery-ui-timepicker-addon.css" type="text/css" />
    <script type="text/javascript" src="../../script/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../../script/jquery-ui.js"></script>
    <script src="../../script/jQuery-Timepicker-Addon/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/js/jquery.ui.datepicker-zh-CN.js.js" charset="gb2312"></script>
    <script src="../../script/js/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="../script/Chart.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#StartDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#StartDate").change(function () {
                $("#hidStartDate").val($(this).val());
            });
            $("#EndDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#EndDate").change(function () {
                $("#hidEndDate").val($(this).val());
            });
        });
        function CourseDistinct() {
            //Chart
            if ($("#StartDate").val() === '输入起始时间')
            { $("#hidStartDate").val("2019-01-01"); }
            if ($("#EndDate").val() === "输入截止时间")
            { $("#hidEndDate").val("2019-12-31"); }

            $("#ChartMsg").text("正在计算数据");
            $("#myChart").empty();
            $.get("http://192.168.194.88:667/InterFace/custom.ashx?method=CourseDistributionRate&start=" + $("#hidStartDate").val() + "&end=" + $("#hidEndDate").val() + "&area=" + $("#DDarea").val(), function (data) {
                var JData=JSON.parse(data);
                console.log("Data: " + data + "/" + JData['count16']);
                var ctx = document.getElementById('myChart').getContext('2d');
                var PieData = {
                datasets: [{
                data: [JData['count16'],JData['count11_15'],JData['count6_10'],JData['count5'],JData['none']], //在这里写数据
                backgroundColor:["#339966","#339933","#99CC00","#669933","#99CC99"] //在这里指定颜色
                }],
                labels: [ //这里写标签，即每个数据属于哪个种类
                '订购15次以上门数', 
                '订购11-15次门数',
                '订购6-10次门数',
                '订购5次已下门数',
                '未订购'
                ]
                };
                $("#ChartMsg").text("");
                var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: PieData
            });
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div>
                <asp:Label ID="StateMessage" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <asp:TextBox ID="StartDate" Style="width: 160px" ClientIDMode="Static" runat="server" Text="输入起始时间"></asp:TextBox>
                <asp:HiddenField ID="hidStartDate" runat="server" ClientIDMode="Static" />
                <asp:TextBox ID="EndDate" Style="width: 160px" ClientIDMode="Static" runat="server" Text="输入截止时间"></asp:TextBox>
                <asp:HiddenField ID="hidEndDate" runat="server" ClientIDMode="Static" />
                <asp:DropDownList ID="DDarea" runat="server">
                    <asp:ListItem Value="n">北方</asp:ListItem>
                    <asp:ListItem Value="s">南方</asp:ListItem>
                </asp:DropDownList>
                <br />
                <button type="button" onclick="CourseDistinct()">选课次数分布</button>
                <asp:Button ID="Button1" runat="server" Text="订购排行" OnClick="Button1_Click" />
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <span id="ChartMsg"></span>
                <canvas id="myChart" width="500px" height="150px"></canvas>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server">
                <asp:GridView ID="GridViewTop" runat="server" OnSelectedIndexChanged="GridViewTop_SelectedIndexChanged" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="订购次数" HeaderText="订购次数" />
                        <asp:BoundField DataField="课件数量" HeaderText="课件数量" />
                        <asp:CommandField ButtonType="Button" HeaderText="查看" ShowHeader="True" ShowSelectButton="True" />
                     </Columns>
                </asp:GridView>
                <asp:GridView ID="GridViewDetails" runat="server"></asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
