<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="CustomTaskDetails.aspx.cs" Inherits="ProjectCollection.WebUI.pages.CustomTaskDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidBatchProjectId" runat="server" />
    <asp:Panel ID="PanelBase" runat="server">
        派单序号：<asp:TextBox ID="txtId" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        派单时间：<asp:TextBox ID="txtCreateDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        完成期限：<asp:TextBox ID="txtDeadLine" runat="server" ReadOnly="true"></asp:TextBox><br />
        客户信息：<asp:TextBox ID="txtCustom" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        任务要求：<asp:TextBox ID="txtTaskRequire" TextMode="MultiLine" Height="90px" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        下单者：<asp:TextBox ID="txtCreator" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        备注：<asp:TextBox ID="txtCreateNote" runat="server" TextMode="MultiLine" Height="80px" ReadOnly="true"></asp:TextBox><br />
        进度：<asp:TextBox ID="txtProgress" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        <span style="color:red; font-size:16px;">课件列表：</span><asp:HyperLink ID="aCourseList" runat="server" Target="_blank" Text=">>查看<<" ></asp:HyperLink><br />
    </asp:Panel>
    <asp:Panel ID="PanelCheck" runat="server" Visible="false">
        <div class="PanelName" style="color:green">任务预审人填写:</div>
        预审人：<asp:TextBox ID="txtSigner" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        预审时间：<asp:TextBox ID="txtCheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        预审备注：<asp:TextBox ID="txtCheckNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelFinish" runat="server" Visible="false">
        <div class="PanelName" style="color:green">执行人填写:</div>
        执行人：<asp:TextBox ID="txtTransactor" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行时间：<asp:TextBox ID="txtFinishDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行备注：<asp:TextBox ID="txtFinishNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelHelpFinish" runat="server" Visible="false">
        <div class="PanelName" style="color:green">辅助执行人填写:</div>
        执行人：<asp:TextBox ID="txtHelper" runat="server" ReadOnly="true"></asp:TextBox><br />
        派发时间：<asp:TextBox ID="txtHelpSendingDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        完成时间：<asp:TextBox ID="txtHelperFinishDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行备注：<asp:TextBox ID="txtHelperFinishNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelPicFinish" runat="server" Visible="false">
        <div class="PanelName" style="color:green">图片制作:</div>
        执行人：<asp:TextBox ID="txtPicMaker" runat="server" ReadOnly="true"></asp:TextBox><br />
        派发时间：<asp:TextBox ID="txtPicSendingDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        完成时间：<asp:TextBox ID="txtPicFinishDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行备注：<asp:TextBox ID="txtPicFinishNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelTemplateFinish" runat="server" Visible="false">
        <div class="PanelName" style="color:green">模版制作:</div>
        执行人：<asp:TextBox ID="txtTemplateMaker" runat="server" ReadOnly="true"></asp:TextBox><br />
        派发时间：<asp:TextBox ID="txtTemplateSendingDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        完成时间：<asp:TextBox ID="txtTemplateFinishDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行备注：<asp:TextBox ID="txtTemplateFinishNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelAttachmentFinish" runat="server" Visible="false">
        <div class="PanelName" style="color:green">素材输出制作:</div>
        执行人：<asp:TextBox ID="txtAttachmentMaker" runat="server" ReadOnly="true"></asp:TextBox><br />
        派发时间：<asp:TextBox ID="txtAttachmentSendingDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        完成时间：<asp:TextBox ID="txtAttachmentFinishDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行备注：<asp:TextBox ID="txtAttachmentFinishNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelBtn" runat="server">
        <%--<asp:Button ID="btnExecute" runat="server" Text="执行" OnClick="btnExecuteOnclick" Visible="false" />--%>
        <asp:HyperLink ID="aExecute" runat="server" Target="_blank" Text="执行" Visible="false"></asp:HyperLink>
        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBackOnclick" Visible="false" />
        <asp:Button ID="btnPass" runat="server" Text="审核通过" OnClick="btnPassOnclick" Visible="false" />
        <asp:Button ID="btnDel" runat="server" Text="不通过（删除重下）" OnClick="btnDelOnclick" Visible="false" />
    </asp:Panel>
</asp:Content>
