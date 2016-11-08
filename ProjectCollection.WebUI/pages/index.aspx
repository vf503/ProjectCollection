<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ProjectCollection.WebUI.pages.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Data {
        font-style:oblique;
        color:blue;
        padding:0 3px 0 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>1）本日新增任务<asp:Label ID="lbTodayPlan" runat="server" CssClass="Data"></asp:Label>项，本日新增工单<asp:Label ID="lbTodayProject" runat="server" CssClass="Data"></asp:Label>项</p>
    <p>2）截止目前总计任务<asp:Label ID="lbTotalPlan" runat="server" CssClass="Data"></asp:Label>项，完成<asp:Label ID="lbFinishedPlan" runat="server" CssClass="Data"></asp:Label>项，总体进度<asp:Label ID="lbPlanRate" runat="server" CssClass="Data"></asp:Label>，总计工单<asp:Label ID="lbTotalProject" runat="server" CssClass="Data"></asp:Label>项，完成<asp:Label ID="lbFinishedProject" runat="server" CssClass="Data"></asp:Label>张，总体进度<asp:Label ID="lbProjectRate" runat="server" CssClass="Data"></asp:Label></p>
    <p>3）当前有<asp:Label ID="lbDelayPlan" runat="server" CssClass="Data"></asp:Label>项任务延迟中，总体任务延迟率<asp:Label ID="lbDelayPlanRate" runat="server" CssClass="Data"></asp:Label>。当前有<asp:Label ID="lbDelayProject" runat="server" CssClass="Data"></asp:Label>张工单延迟中，总体工单延迟率<asp:Label ID="lbDelayProjectRate" runat="server" CssClass="Data"></asp:Label></p>
    <p>4）截止到本日，本年度客户定制项目<asp:Label ID="lbDPlan" runat="server" CssClass="Data"></asp:Label>项，客户定制工单<asp:Label ID="lbDProject" runat="server" CssClass="Data"></asp:Label>张。</p>
    <p>5）截止到本日，本年度自筹项目<asp:Label ID="lbAPlan" runat="server" CssClass="Data"></asp:Label>项，自筹工单<asp:Label ID="lbAProject" runat="server" CssClass="Data"></asp:Label>张。</p>
    <p>6）截止到本日，本年度外拍项目<asp:Label ID="lbBPlan" runat="server" CssClass="Data"></asp:Label>项，外拍工单<asp:Label ID="lbBProject" runat="server" CssClass="Data"></asp:Label>张。</p>
    <p>7）截止到本日，本年度采购项目<asp:Label ID="lbIPlan" runat="server" CssClass="Data"></asp:Label>项，采购工单<asp:Label ID="lbIProject" runat="server" CssClass="Data"></asp:Label>张。</p>
</asp:Content>
