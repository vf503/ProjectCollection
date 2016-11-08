<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="ProjectPlanCreateEdit.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectPlanCreateEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#planDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#planDate").change(function () {
                $("#hidPlanDate").val($(this).val());
            });

            $("#planDate").val($("#hidPlanDate").val());
            //
            $("#RecordingDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#RecordingDate").change(function () {
                $("#hidRecordingDate").val($(this).val());
            });

            $("#RecordingDate").val($("#hidRecordingDate").val());
            //
            $("#FileDeliverDate").datetimepicker({
                dateFormat: 'yy-mm-dd'
            });

            $("#FileDeliverDate").change(function () {
                $("#hidFileDeliverDate").val($(this).val());
            });

            $("#FileDeliverDate").val($("#hidFileDeliverDate").val());
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidProjectPlanId" runat="server" />
    编号：<asp:TextBox ID="txtProjectPlanNo" runat="server"></asp:TextBox><br />
    类型：<asp:DropDownList ID="ddlProjectPlanType" runat="server"></asp:DropDownList><br />
    <asp:Label ID="labMakingDate" runat="server" Text="创建日期："></asp:Label><asp:TextBox ID="txtMakingDate" runat="server" ReadOnly="True" Text="自动填充"></asp:TextBox><br />
    计划日期：
    <asp:TextBox id="planDate" style="width:160px" ClientIDMode="Static" runat="server"></asp:TextBox>
    <asp:HiddenField ID="hidPlanDate" runat="server" ClientIDMode="Static" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlanDate" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="planDate">请选择日期</asp:RequiredFieldValidator>
    <br />
    题目：<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
    主讲人：<asp:TextBox ID="txtLecturer" runat="server"></asp:TextBox><br />
    栏目：<asp:TextBox ID="txtCategory" runat="server"></asp:TextBox><br />
    学时：<asp:TextBox ID="txtCourseCount" Text="0" runat="server"></asp:TextBox>
    <asp:RangeValidator ID="CourseCountRangeValidator" style="color:red" runat="server" ErrorMessage="RangeValidator" Type="Double" ControlToValidate="txtCourseCount" MaximumValue="100" MinimumValue="1">请输入数字</asp:RangeValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCourseCount">请输入数字</asp:RequiredFieldValidator>
    <br />
    资金：<asp:TextBox ID="txtPrice" Text="0" runat="server"></asp:TextBox>
    <asp:RangeValidator ID="PriceRangeValidator" style="color:red" runat="server" ErrorMessage="RangeValidator" Type="Integer" ControlToValidate="txtPrice" MaximumValue="999999" MinimumValue="0">请输入数字</asp:RangeValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtPrice">请输入数字</asp:RequiredFieldValidator>
    <br />
    来源：<asp:TextBox ID="txtSource" runat="server"></asp:TextBox><br />
    备注：<asp:TextBox ID="txtNote" runat="server" Height="80px" TextMode="MultiLine"></asp:TextBox><br />
    特殊要求：<asp:TextBox ID="txtExtraNote" runat="server" Height="80px" TextMode="MultiLine" ForeColor="Red"></asp:TextBox><br />
    <asp:Label ID="labRecordingPersonInCharge" runat="server" Text="开发/拍摄/采购人员："></asp:Label><asp:DropDownList ID="ddlRecordingPersonInCharge" runat="server"></asp:DropDownList><br />
    <asp:Label ID="labProgress" runat="server" Text="进度："></asp:Label><asp:TextBox ID="txtProgress" runat="server" ReadOnly="True"></asp:TextBox><br />
    <%-- 
    ExecuteDate:<asp:TextBox ID="txtExecuteDate" runat="server"></asp:TextBox><br />
    LeadDate:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
    CourseCount:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
    Source:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
    --%>
    <asp:Panel ID="panelRecording" runat="server" Visible ="false">
    拍摄日期：<asp:TextBox ID="RecordingDate" style="width: 160px" ClientIDMode="Static" runat="server" ReadOnly="true"></asp:TextBox>
    <asp:HiddenField ID="hidRecordingDate" runat="server" ClientIDMode="Static" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="RecordingDate" ValidationGroup="Recording">请选择日期</asp:RequiredFieldValidator><br />
    拍摄地点：<asp:TextBox ID="txtRecordingPlace" runat="server" ></asp:TextBox><br />
    场记：<asp:DropDownList ID="ddlRecordingScriptHolder" runat="server">
            <asp:ListItem Value="true">有</asp:ListItem>
            <asp:ListItem Value="false">无</asp:ListItem>
        </asp:DropDownList><br />
    初始PPT：<asp:TextBox ID="txtRecordingLecture" runat="server"></asp:TextBox>
    <asp:RangeValidator ID="RangeValidatorRecordingLecture" style="color:red" runat="server" ErrorMessage="RangeValidator" Type="Double" ControlToValidate="txtRecordingLecture" MaximumValue="100" MinimumValue="0" ValidationGroup="Recording">请输入数字</asp:RangeValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtRecordingLecture" ValidationGroup="Recording">请输入数字</asp:RequiredFieldValidator>    
    <br />
    初始资料交付时间：<asp:TextBox ID="FileDeliverDate" style="width: 160px" ClientIDMode="Static" ReadOnly="true" runat="server"></asp:TextBox>
    <asp:HiddenField ID="hidFileDeliverDate" runat="server" ClientIDMode="Static" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="FileDeliverDate" ValidationGroup="Recording">请选择日期</asp:RequiredFieldValidator>    
    <br />
    初始资料数目：<asp:TextBox ID="txtRecordingFile" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" style="color:red" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtRecordingFile" ValidationGroup="Recording">请输入初始资料内容</asp:RequiredFieldValidator>
    <br />
    拍摄备注：<asp:TextBox ID="txtRecordingNote" runat="server" Height="100%" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
    <asp:Button ID="btnOk" runat="server" Text="修改" OnClick="btnOk_Click" />
    <asp:Button ID="btnProject" runat="server" Text="下达工单" OnClick="btnProject_Click" Visible="False" />
    <asp:Button ID="btnFinish" runat="server" Text="下单完毕" OnClick="btnFinish_Click" Visible="False" ForeColor="Red" />
    <asp:Button ID="btnRecond" runat="server" Text="完成拍摄" OnClick="btnRecond_Click" Visible="False" ValidationGroup="Recording"/>
</asp:Content>
