<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomCount.aspx.cs" Inherits="ProjectCollection.WebUI.pages.CustomCount" %>

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
            //
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div>
                <asp:Label ID="StateMessage" runat="server" Text=""></asp:Label>
                <asp:Button ID="UpdataBtn" runat="server" Text="更新数据" OnClick="UpdataBtn_Click" />
            </div>
            <div>
                <asp:TextBox ID="StartDate" Style="width: 160px" ClientIDMode="Static" runat="server" Text="输入起始时间"></asp:TextBox>
                <asp:HiddenField ID="hidStartDate" runat="server" ClientIDMode="Static" />
                <asp:TextBox ID="EndDate" Style="width: 160px" ClientIDMode="Static" runat="server" Text="输入截止时间"></asp:TextBox>
                <asp:HiddenField ID="hidEndDate" runat="server" ClientIDMode="Static" />
                <br />
                <asp:Button ID="CustomerQueryBtn" runat="server" Text="客户统计" OnClick="CustomerQueryBtn_Click" />
                <asp:Button ID="CourseSourceBtn" runat="server" Text="课件来源分析" OnClick="CourseSourceBtn_Click" />
                <asp:Button ID="CourseCategoryBtn" runat="server" Text="课件内容分析" OnClick="CourseCategoryBtn_Click" />
                <asp:Button ID="HistoryBtn" runat="server" Text="历史数据分析" OnClick="HistoryBtn_Click" />
                <asp:Button ID="CourseBtn" runat="server" Text="课件分析" OnClick="CourseBtn_Click" />
            </div>
            <asp:Panel ID="CustomerPanel" runat="server" Visible="false">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="北方用户列表"></asp:Label>
                    <asp:GridView ID="NCustomerTable" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="名称" />
                            <asp:BoundField DataField="sort" HeaderText="类型" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="NCustomerCount" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="北方分类"></asp:Label>
                    <asp:GridView ID="NCustomerTypeTable" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="type" HeaderText="分类" />
                            <asp:BoundField DataField="count" HeaderText="数量" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="南方用户列表"></asp:Label>
                    <asp:GridView ID="SCustomerTable" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="名称" />
                            <asp:BoundField DataField="sort" HeaderText="类型" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="SCustomerCount" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="南方分类"></asp:Label>
                    <asp:GridView ID="SCustomerTypeTable" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="type" HeaderText="分类" />
                            <asp:BoundField DataField="count" HeaderText="数量" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="Label10" runat="server" Text="客户内容偏好分析"></asp:Label>
                    <asp:GridView ID="CustomerCategoryGV" runat="server" AutoGenerateColumns="False">
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="CourseSourcePanel" runat="server" Visible="false">
                <asp:Label ID="NumberOfCoursesLB" runat="server" Text="全部课件选课门数(不包括缺失来源的课件)："></asp:Label>
                <asp:Label ID="NumberOfTimesLB" runat="server" Text="全部课件选课次数(不包括缺失来源的课件)："></asp:Label>
                <asp:Label ID="RateLB" runat="server" Text="选课均值："></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="全部课件来源分析"></asp:Label>
                <asp:GridView ID="SourceTable" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="SourceName" HeaderText="来源" />
                        <asp:BoundField DataField="times" HeaderText="选课次数" />
                        <asp:BoundField DataField="TimesRatio" HeaderText="次数占比" />
                        <asp:BoundField DataField="number" HeaderText="选课集数" />
                        <asp:BoundField DataField="NumberRatio" HeaderText="集数占比" />
                        <asp:BoundField DataField="TimesAVG" HeaderText="次数均值" />      
                        <asp:BoundField DataField="GroupTimes" HeaderText="选课门数" />
                        <asp:BoundField DataField="GroupTimesRatio" HeaderText="门数占比" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="NumberOfCoursesCurrentYearLB" runat="server" Text="当年新课件选课集数(不包括缺失来源的课件)："></asp:Label>
                <asp:Label ID="NumberOfTimesCurrentYearLB" runat="server" Text="当年新课件选课次数(不包括缺失来源的课件)："></asp:Label>
                <asp:Label ID="RateCurrentYearLB" runat="server" Text="选课均值："></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label7" runat="server" Text="当年新课件来源分析"></asp:Label>
                <asp:GridView ID="SourceTableCurrentYear" runat="server" AutoGenerateColumns="False">
                    <Columns>
                         <asp:BoundField DataField="SourceName" HeaderText="来源" />
                        <asp:BoundField DataField="times" HeaderText="选课次数" />
                        <asp:BoundField DataField="TimesRatio" HeaderText="次数占比" />
                        <asp:BoundField DataField="number" HeaderText="选课集数" />
                        <asp:BoundField DataField="NumberRatio" HeaderText="集数占比" />
                        <asp:BoundField DataField="TimesAVG" HeaderText="次数均值" /> 
                        <asp:BoundField DataField="GroupTimes" HeaderText="选课门数" />
                        <asp:BoundField DataField="GroupTimesRatio" HeaderText="门数占比" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="Label6" runat="server" Text="当年新课效度分析"></asp:Label>
                <asp:GridView ID="SourceCreateTable" runat="server" AutoGenerateColumns="False">
                    <Columns>
                         <asp:BoundField DataField="type" HeaderText="来源" />
                        <asp:BoundField DataField="selled" HeaderText="选课门/集数" />
                        <asp:BoundField DataField="produce" HeaderText="开发门/集数" />
                        <asp:BoundField DataField="rate" HeaderText="占比" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="CourseCategoryPanel" runat="server" Visible="false">
                <asp:Label ID="NumberOfCoursesCategoryLB" runat="server" Text="全部课件选课集数(不包括缺失分类的课件)"></asp:Label>
                <asp:Label ID="NumberOfTimesCategoryLB" runat="server" Text="全部课件选课次数(不包括缺失分类的课件)"></asp:Label>
                <asp:Label ID="RateCategoryLB" runat="server" Text="选课均值："></asp:Label>
                 <br />
                <asp:Label ID="Label9" runat="server" Text="全部课件内容选课分析"></asp:Label>
                <asp:GridView ID="NumberOfTimesCategoryTable" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TypeTop" HeaderText="一级栏目" />
                        <asp:BoundField DataField="type" HeaderText="二级栏目" />
                        <asp:BoundField DataField="times" HeaderText="选课次数" />
                        <asp:BoundField DataField="TimesRatio" HeaderText="次数占比" />
                        <asp:BoundField DataField="number" HeaderText="选课集数" />
                        <asp:BoundField DataField="NumberRatio" HeaderText="集数占比" />
                        <asp:BoundField DataField="TimesAVG" HeaderText="次数均值" /> 
                        <asp:BoundField DataField="GroupTimes" HeaderText="选课门数" />
                        <asp:BoundField DataField="GroupTimesRatio" HeaderText="门数占比" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="NumberOfCoursesCategoryCurrentYearLB" runat="server" Text="当年新课件选课门数(不包括缺失来源的课件)"></asp:Label>
                <asp:Label ID="NumberOfTimesCategoryCurrentYearLB" runat="server" Text="当年新课件选课次数(不包括缺失来源的课件)"></asp:Label>
                <asp:Label ID="RateCategoryCurrentYearLB" runat="server" Text="选课均值："></asp:Label>
                <br />
                <asp:Label ID="Label13" runat="server" Text="当年新课件内容选课分析"></asp:Label>
                <asp:GridView ID="NumberOfTimesCategoryCurrentYearTable" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TypeTop" HeaderText="一级栏目" />
                        <asp:BoundField DataField="type" HeaderText="二级栏目" />
                        <asp:BoundField DataField="times" HeaderText="选课次数" />
                        <asp:BoundField DataField="TimesRatio" HeaderText="次数占比" />
                        <asp:BoundField DataField="number" HeaderText="选课集数" />
                        <asp:BoundField DataField="NumberRatio" HeaderText="集数占比" />
                        <asp:BoundField DataField="TimesAVG" HeaderText="次数均值" /> 
                        <asp:BoundField DataField="GroupTimes" HeaderText="选课门数" />
                        <asp:BoundField DataField="GroupTimesRatio" HeaderText="门数占比" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="Label11" runat="server" Text="内容-来源选课分析"></asp:Label>
                <asp:GridView ID="CategorySourceGV" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
                <br />
            </asp:Panel>
            <asp:Panel ID="HistoryPanel" runat="server" Visible="false">
                <asp:Label ID="Label8" runat="server" Text="历史选课发布时间（年）分析"></asp:Label>
                <asp:GridView ID="HistoryAllTable" runat="server" AutoGenerateColumns="False">
                    <Columns>
                         <asp:BoundField DataField="year" HeaderText="年" />
                        <asp:BoundField DataField="times" HeaderText="次" />
                        <asp:BoundField DataField="TimesRate" HeaderText="次占比" />
                        <asp:BoundField DataField="count" HeaderText="集" />
                        <asp:BoundField DataField="CountRate" HeaderText="集占比" />
                        <asp:BoundField DataField="group" HeaderText="门（自筹）" />
                        <asp:BoundField DataField="GroupRate" HeaderText="占比" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="Label12" runat="server" Text="时间-来源选课分析"></asp:Label>
                <asp:GridView ID="HistorySourceGV" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
                <br />
                <asp:Label ID="Label14" runat="server" Text="时间-内容选课分析"></asp:Label>
                <asp:GridView ID="HistoryCategoryGV" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="CoursePanel" runat="server" Visible="false">
                <asp:Label ID="Label15" runat="server" Text="历史选课次数"></asp:Label>
                <asp:GridView ID="HistoryCourseGV" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
                <br />
                <asp:Label ID="Label16" runat="server" Text="当年新课选课次数"></asp:Label>
                <asp:GridView ID="NewCourseGV" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
                <br />
                <asp:Label ID="Label17" runat="server" Text="客户类型-选课次数"></asp:Label>
                <asp:GridView ID="CustomerCourseGV" runat="server" AutoGenerateColumns="False">
                </asp:GridView>
                <br />
                <asp:Label ID="Label18" runat="server" Text="专题分析"></asp:Label>
                <asp:GridView ID="GroupGV" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="title" HeaderText="专题" />
                        <asp:BoundField DataField="times" HeaderText="次" />
                        <asp:BoundField DataField="groups" HeaderText="门" />
                        <asp:BoundField DataField="rate" HeaderText="均值" />                 
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
