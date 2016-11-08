<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="ProjectCreateEdit.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectCreateEdit" %>

<%@ PreviousPageType VirtualPath="~/pages/ProjectListEmbed.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .RadioButtonList {
        }

            .RadioButtonList input {
                width: 15px;
            }

            .RadioButtonList label {
                display: inline;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#DeadLine").datetimepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#DeadLine").change(function () {
                $("#hidDeadLine").val($(this).val());
            });

            $("#DeadLine").val($("#hidDeadLine").val());
            //
            $("#ShorthandAudioReceiveDate").datetimepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#ShorthandAudioReceiveDate").change(function () {
                $("#hidShorthandAudioReceiveDate").val($(this).val());
            });

            $("#ShorthandAudioReceiveDate").val($("#hidShorthandAudioReceiveDate").val());
            //
            $("#ContentEstimatedDate").datetimepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#ContentEstimatedDate").change(function () {
                $("#hidContentEstimatedDate").val($(this).val());
            });

            $("#ContentEstimatedDate").val($("#hidContentEstimatedDate").val());
            //
            $("#ProductionAssignmentDate").datetimepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#ProductionAssignmentDate").change(function () {
                $("#hidProductionAssignmentDate").val($(this).val());
            });

            $("#ProductionAssignmentDate").val($("#hidProductionAssignmentDate").val());
            //
            $("#ProductionEstimatedDate").datetimepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#ProductionEstimatedDate").change(function () {
                $("#hidProductionEstimatedDate").val($(this).val());
            });

            $("#ProductionEstimatedDate").val($("#hidProductionEstimatedDate").val());
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidBatchProjectId" runat="server" />
    <asp:Panel ID="PanelBase" runat="server">
        <asp:Label ID="labelProjectNoType" runat="server" Text="派单序号："></asp:Label><asp:TextBox ID="txtProjectNo" runat="server" Text=""></asp:TextBox><br />
        派单时间：<asp:TextBox ID="txtSendingDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        记录单类型：<asp:DropDownList ID="ddlProjectType" runat="server"></asp:DropDownList><br />
        <asp:Panel ID="PanelCustom" runat="server">
            是否需要制作部环节：<asp:DropDownList ID="ddlContentNeeds" runat="server"></asp:DropDownList><br />
        </asp:Panel>
        是否发布：<asp:DropDownList ID="ddlPublishNeeds" runat="server"></asp:DropDownList><br />
        能否销售：<asp:RadioButtonList ID="rblCanBeSold" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonList">
            <asp:ListItem Selected="True" Value="00000000-0000-0000-0000-000000000042">能</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000043">否</asp:ListItem>
        </asp:RadioButtonList>
        记录单用途：<asp:DropDownList ID="ddlWorkType" runat="server"></asp:DropDownList><br />
        派单人：<asp:TextBox ID="txtInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        时限要求：<asp:DropDownList ID="ddlemergency" runat="server"></asp:DropDownList><br />
        <%--完成期限：<asp:TextBox ID="DeadLine" ClientIDMode="Static" Style="width: 160px" runat="server" ReadOnly="true"></asp:TextBox><br />
        <asp:HiddenField ID="hidDeadLine" runat="server" ClientIDMode="Static" />
        是否自定义阶段期限:<asp:RadioButtonList ID="RadioButtonListDeadLine" runat="server" OnSelectedIndexChanged="rblDeadLineSelectSetChanged" CssClass="RadioButtonList" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">是</asp:ListItem>
            <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
        </asp:RadioButtonList><br />
        <asp:Panel ID="PanelCustomDeadLine" runat="server">

        </asp:Panel>--%>
        专题名称：<asp:TextBox ID="txtProjectPlanName" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        初始课程名称：<asp:TextBox ID="txtCourseName" runat="server" Text=""></asp:TextBox><br />
        <%--初始PPT：<asp:DropDownList ID="ddllecture" runat="server"></asp:DropDownList><br />--%>
        主讲人：<asp:TextBox ID="txtlecturer" runat="server" Text=""></asp:TextBox><br />
        主讲人职务：<asp:TextBox ID="txtLecturerJob" runat="server" Text=""></asp:TextBox><br />
        制作公告：<asp:DropDownList ID="ddlnotice" runat="server"></asp:DropDownList><br />
        发布头条新闻：<asp:DropDownList ID="ddlHeadLine" runat="server"></asp:DropDownList><br />
        图文类标识：<asp:TextBox ID="txtTextCategory" runat="server" Text=""></asp:TextBox><br />
        备注：<asp:TextBox ID="txtCreateNote" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox><br />
        特殊要求：<asp:TextBox ID="txtExtraNote" runat="server" TextMode="MultiLine" Height="100px" ForeColor="Red"></asp:TextBox><br />
        <asp:Label ID="labelPlanNote" runat="server" Text="任务备注："></asp:Label><asp:TextBox ID="txtPlanNote" runat="server" TextMode="MultiLine"></asp:TextBox>
    </asp:Panel>
    <asp:Panel ID="PanelCapture" runat="server" Visible="false">
        <div class="PanelName">采集负责人填写:</div>
        采集负责人：<asp:TextBox ID="txtCapturePersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        延迟接收时间：<asp:TextBox ID="txtCaptureReceiveDelayDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        延迟接收说明：<asp:TextBox ID="txtCaptureReceiveDelayNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        接收时间：<asp:TextBox ID="txtCaptureReceiveDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        完成时间：<asp:TextBox ID="txtCaptureFinishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        时长：<asp:TextBox ID="txtCaptureDuration" Text="1" runat="server"></asp:TextBox>
        <asp:RangeValidator ID="CaptureDurationRangeValidator" Style="color: red" runat="server" ErrorMessage="RangeValidator" Type="Double" ControlToValidate="txtCaptureDuration" MinimumValue="1" MaximumValue="9999" ValidationGroup="capture">请输入数字</asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCaptureDuration" ValidationGroup="capture">请输入数字</asp:RequiredFieldValidator>
        <br />
        声道：
        <asp:DropDownList ID="ddlCaptureSoundTrack" runat="server"></asp:DropDownList><br />
        视频是否完成：<asp:DropDownList ID="ddlCaptureVideoNeeds" runat="server"></asp:DropDownList><br />
        声音质量初验：<asp:DropDownList ID="ddlCaptureVideoAudioQuality" runat="server"></asp:DropDownList><br />
        画面质量初验：<asp:DropDownList ID="ddlCaptureVideoVideoQuality" runat="server"></asp:DropDownList><br />
        音频是否完成：<asp:DropDownList ID="ddlCaptureAudioNeeds" runat="server"></asp:DropDownList><br />
        音频质量初验：<asp:DropDownList ID="ddlCaptureAudioQuality" runat="server"></asp:DropDownList><br />
        课件资料位置：<asp:TextBox ID="txtCaptureFilePath" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        采集备注：<asp:TextBox ID="txtCaptureNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelCaptureCheck" runat="server" Visible="false">
        <div class="PanelName">制作预审员填写:</div>
        采集审核人：<asp:TextBox ID="txtCaptureCheckPersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        审核时间：<asp:TextBox ID="txtCaptureCheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        反馈意见：<asp:TextBox ID="txtCaptureCheckNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelShorthand" runat="server" Visible="false">
        <div class="PanelName">速记负责人填写:</div>
        速记负责人：<asp:TextBox ID="txtShorthandPersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        接收时间：<asp:TextBox ID="txtShorthandReceiveDate" runat="server" Visible="false"></asp:TextBox><br />
        音频给出时间：<asp:TextBox ID="ShorthandAudioReceiveDate" ClientIDMode="Static" Style="width: 160px" runat="server" ReadOnly="true"></asp:TextBox>
        <asp:HiddenField ID="hidShorthandAudioReceiveDate" runat="server" ClientIDMode="Static" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="ShorthandAudioReceiveDate">请选择日期</asp:RequiredFieldValidator>
        <br />
        速记返回时间：<asp:TextBox ID="txtShorthandFinishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        速记承接单位：<asp:TextBox ID="txtShorthandPurveyor" runat="server"></asp:TextBox><br />
        速记质量评价：<asp:DropDownList ID="ddlShorthandQuality" runat="server"></asp:DropDownList><br />
        速记备注：<asp:TextBox ID="txtShorthandNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelContentReceive" runat="server" Visible="false">
        <div class="PanelName">制作部负责人填写（接收）:</div>
        制作部门负责人：<asp:TextBox ID="txtContentPersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        制作人：<asp:DropDownList ID="ddlContentOperator" runat="server"></asp:DropDownList><br />
        接收时间：<asp:TextBox ID="txtContentReceiveDate" runat="server" Visible="false"></asp:TextBox><br />
        任务分配时间：<asp:TextBox ID="txtContentAssignmentDate" runat="server" Text="自动填充"></asp:TextBox><br />
        计划完成时间：
        <asp:TextBox ID="ContentEstimatedDate" runat="server" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
        <asp:HiddenField ID="hidContentEstimatedDate" runat="server" ClientIDMode="Static" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="ContentEstimatedDate">请选择日期</asp:RequiredFieldValidator>
        <br />
        接收备注：<asp:TextBox ID="txtContentReceiveNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelContentCheck" runat="server" Visible="false">
        <div class="PanelName">制作部负责人填写（初审）:</div>
        课程简介质量：<asp:DropDownList ID="ddlContentCourseIntroductionQuality" runat="server"></asp:DropDownList><br />
        专家简历质量：<asp:DropDownList ID="ddlContentResumeQuality" runat="server"></asp:DropDownList><br />
        PPT质量：<asp:DropDownList ID="ddlContentPPTQuality" runat="server"></asp:DropDownList><br />
        考题质量：<asp:DropDownList ID="ddlContentExercisesQuality" runat="server"></asp:DropDownList><br />
        文稿整理质量：<asp:DropDownList ID="ddlContentTextQuality" runat="server"></asp:DropDownList><br />
        是否及时：<asp:DropDownList ID="ddlContentIsTimely" runat="server"></asp:DropDownList><br />
        审核时间：<asp:TextBox ID="txtContentCheckDate" runat="server" Text="自动填充"></asp:TextBox><br />
        备注：<asp:TextBox ID="txtContentCheckNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelContentRecheck" runat="server" Visible="false">
        <div class="PanelName">制作部复审员填写:</div>
        复审审核人：<asp:TextBox ID="txtContentRecheckPersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        审核时间：<asp:TextBox ID="txtContentRecheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        复审意见：<asp:TextBox ID="txtContentRecheckNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelContentOperator" runat="server" Visible="false">
        <div class="PanelName">制作部制作人员填写:</div>
        实际完成时间：<asp:TextBox ID="txtContentFinishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        未按时完成原因：<asp:TextBox ID="txtContentDelayNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        课程名称确认：<asp:DropDownList ID="ddlContentCourseNameConfirm" runat="server"></asp:DropDownList><br />
        题目名称改为：<asp:TextBox ID="txtContentChangedCourseName" runat="server"></asp:TextBox><br />
        课程是否推荐：<asp:DropDownList ID="ddlContentCourseRecommend" runat="server"></asp:DropDownList><br />
        建议做三分屏：<asp:DropDownList ID="ddlContentPPTAdvice" runat="server"></asp:DropDownList><br />
        考题数量：<asp:TextBox ID="txtContentExercises" Text="0" runat="server"></asp:TextBox>
        <asp:RangeValidator ID="RangeValidatorContentExercises" Style="color: red" runat="server" ErrorMessage="RangeValidator" Type="Integer" ControlToValidate="txtContentExercises" MinimumValue="0" MaximumValue="999">请输入数字</asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtContentExercises">请输入数字</asp:RequiredFieldValidator>
        <br />
        完成内容包含：<br />
        PPT：<asp:DropDownList ID="ddlContentPPTNeeds" runat="server"></asp:DropDownList><br />
        课程简介：<asp:DropDownList ID="ddlContentCourseIntroNeeds" runat="server"></asp:DropDownList><br />
        教师简介：<asp:DropDownList ID="ddlContentLecturerResumeNeeds" runat="server"></asp:DropDownList><br />
        考题：<asp:DropDownList ID="ddlContentExercisesNeeds" runat="server"></asp:DropDownList><br />
        文稿整理：<asp:DropDownList ID="ddlContentTextEditNeeds" runat="server"></asp:DropDownList><br />
        制作部制作员备注：<asp:TextBox ID="txtContentOperateNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelProductionReceive" runat="server" Visible="false">
        <div class="PanelName">技术部负责人填写（接收）:</div>
        技术部门负责人：<asp:TextBox ID="txtProductionPersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        延迟接收时间：<asp:TextBox ID="txtProductionReceiveDelayDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        延迟接收说明：<asp:TextBox ID="txtProductionReceiveDelayNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        接收时间：<asp:TextBox ID="txtProductionReceiveDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        接收备注：<asp:TextBox ID="txtProductionReceiveNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        任务分配时间：<asp:TextBox ID="ProductionAssignmentDate" Style="width: 160px" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
        <asp:HiddenField ID="hidProductionAssignmentDate" runat="server" ClientIDMode="Static" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Style="color: red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="ProductionAssignmentDate" ValidationGroup="ProductionReceive">请选择日期</asp:RequiredFieldValidator>
        <br />
        计划完成时间：<asp:TextBox ID="ProductionEstimatedDate" Style="width: 160px" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
        <asp:HiddenField ID="hidProductionEstimatedDate" runat="server" ClientIDMode="Static" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Style="color: red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="ProductionEstimatedDate" ValidationGroup="ProductionReceive">请选择日期</asp:RequiredFieldValidator>
        <br />
        制作人：<asp:DropDownList ID="ddlProductionOperator" runat="server"></asp:DropDownList><br />
    </asp:Panel>
    <asp:Panel ID="PanelProductionCheck" runat="server" Visible="false">
        <div class="PanelName">技术部负责人填写（审核）:</div>
        画面编辑：<asp:DropDownList ID="ddlProductionVideoEditCheck" runat="server"></asp:DropDownList><br />
        声音编辑：<asp:DropDownList ID="ddlProductionAudioEditCheck" runat="server"></asp:DropDownList><br />
        合成评价：<asp:DropDownList ID="ddlProductionProductCheck" runat="server"></asp:DropDownList><br />
        是否及时：<asp:DropDownList ID="ddlProductionIsTimely" runat="server"></asp:DropDownList><br />
        审核时间：<asp:TextBox ID="txtProductionCheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        存在问题：<asp:TextBox ID="txtProductionCheckNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelProductionOperator" runat="server" Visible="false">
        <div class="PanelName">技术部制作员填写:</div>
        画面质量：<asp:DropDownList ID="ddlProductionVideoQuality" runat="server"></asp:DropDownList><br />
        声音质量：<asp:DropDownList ID="ddlProductionAudioQuality" runat="server"></asp:DropDownList><br />
        备份：<asp:DropDownList ID="ddlProductionFileBackUp" runat="server"></asp:DropDownList><br />
        首次完成时间：<asp:TextBox ID="txtProductionFinishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        未按时完成原因：<asp:TextBox ID="txtProductionDelayNote" runat="server"></asp:TextBox><br />
        最后修改时间：<asp:TextBox ID="txtProductionLastModifyDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        技术部操作员备注：<asp:TextBox ID="txtProductionOperateNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelPublish" runat="server" Visible="false">
        <div class="PanelName">发布人员填写:</div>
        发布人：<asp:TextBox ID="txtPublishOperator" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        接收技术部资料时间：<asp:TextBox ID="txtPublishReceiveContentDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        接收制作部资料时间：<asp:TextBox ID="txtPublishReceiveProductionDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        课程发布时间：<asp:TextBox ID="txtPublishPublishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        头条新闻发布：<asp:DropDownList ID="ddlPublishTopNewsNeeds" runat="server"></asp:DropDownList><br />
        公告发布：<asp:DropDownList ID="ddlPublishNoticeNeeds" runat="server"></asp:DropDownList><br />
        通用版标注：<asp:TextBox ID="txtPublishCommonCategory" runat="server"></asp:TextBox><br />
        党政版标注：<asp:TextBox ID="txtPublishGovernmentCategory" runat="server"></asp:TextBox><br />
        财经专版标注：<asp:TextBox ID="txtPublishFinanceCategory" runat="server"></asp:TextBox><br />
        银行版标注：<asp:TextBox ID="txtPublishBankCategory" runat="server"></asp:TextBox><br />
        播放是否正常：<asp:DropDownList ID="ddlPublishPageState" runat="server"></asp:DropDownList><br />
        显示是否正常：<asp:DropDownList ID="ddlPublishPlayState" runat="server"></asp:DropDownList><br />
        发布备注：<asp:TextBox ID="txtPublishNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelCheck" runat="server" Visible="false">
        <div class="PanelName">审核人员填写:</div>
        发布审核人：<asp:TextBox ID="txtCheckPersonInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        课程审核时间：<asp:TextBox ID="txtCheckTaskCheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        <%--取消推荐时间：<asp:TextBox ID="txtCheckTaskCancelCommendDate" runat="server"></asp:TextBox><br />--%>
        确认推荐：<asp:DropDownList ID="ddlCheckTaskCourseCommend" runat="server"></asp:DropDownList><br />
        <%--取消推荐：<asp:DropDownList ID="ddlCheckTaskCourseCancelCommend" runat="server"></asp:DropDownList><br />--%>
        标注是否正确：<asp:DropDownList ID="ddlCheckTaskCategoryCheck" runat="server"></asp:DropDownList><br />
        审核备注：<asp:TextBox ID="txtCheckTaskNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelBaseBtn" runat="server">
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
        <asp:Button ID="btnOk" runat="server" Text="派发工单" OnClick="btnOk_Click" Visible="false" />
        <asp:Button ID="btnReceive" runat="server" Text="接受任务" OnClick="btnReceive_Click" Visible="false" />
        <asp:Button ID="btnSentBack" runat="server" Text="审核不通过" OnClick="btnSentBack_Click" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="PanelCaptureCheckBtn" runat="server" Visible="false">
        <%--<asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" Visible="false" />--%>
        <asp:Button ID="btnDiscard" runat="server" Text="废除" OnClick="btnDiscard_Click" Visible="false" />
    </asp:Panel>
</asp:Content>
