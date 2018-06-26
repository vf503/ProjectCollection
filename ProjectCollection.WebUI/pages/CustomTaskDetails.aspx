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
        客户信息：<asp:TextBox ID="txtCustom" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        任务要求：<asp:TextBox ID="txtTaskRequire" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        包含课件数：<asp:TextBox ID="txtCourseAmount" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        下单者：<asp:TextBox ID="txtCreator" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
        备注：<asp:TextBox ID="txtCreateNote" runat="server" TextMode="MultiLine" Height="100px" ReadOnly="true"></asp:TextBox><br />
        进度：<asp:TextBox ID="txtProgress" runat="server" Text="" ReadOnly="true"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelCheck" runat="server" Visible="false">
        <div class="PanelName">任务预审人填写:</div>
        预审人：<asp:TextBox ID="txtSigner" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        预审时间：<asp:TextBox ID="txtCheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        预审备注：<asp:TextBox ID="txtCheckNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
    </asp:Panel>
        <asp:Panel ID="PanelFinish" runat="server" Visible="false">
        <div class="PanelName">执行人填写:</div>
        执行人：<asp:TextBox ID="txtTransactor" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行时间：<asp:TextBox ID="txtFinishDate" runat="server" ReadOnly="true"></asp:TextBox><br />
        执行备注：<asp:TextBox ID="txtFinishNote" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelBtn" runat="server">
        <%--<asp:Button ID="btnExecute" runat="server" Text="执行" OnClick="btnExecuteOnclick" Visible="false" />--%>
        <asp:HyperLink ID="aExecute" runat="server" Target="_blank" Text="执行" Visible="false"></asp:HyperLink>
        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBackOnclick" Visible="false" />
        <asp:Button ID="btnPass" runat="server" Text="审核通过" OnClick="btnPassOnclick" Visible="false" />
        <asp:Button ID="btnDel" runat="server" Text="不通过（删除重下）" OnClick="btnDelOnclick" Visible="false" />
    </asp:Panel>
</asp:Content>
