<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="CustomProjectCreateEdit.aspx.cs" Inherits="ProjectCollection.WebUI.CustomProjectCreateEdit" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidCustomProjectId" runat="server" />
    <asp:HiddenField ID="hidBatchCustomProjectId" runat="server" />
    <asp:Panel ID="PanelBase" runat="server">
        <asp:Label ID="labelProjectNo" runat="server" Text="派单序号："></asp:Label><asp:TextBox ID="txtProjectNo" runat="server" Text=""></asp:TextBox><br />
        派单时间：<asp:TextBox ID="txtSendingDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        是否发布：<asp:DropDownList ID="ddlPublishNeeds" runat="server"></asp:DropDownList><br />
        派单人：<asp:TextBox ID="txtInCharge" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        专题名称：<asp:TextBox ID="txtProjectPlanName" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        名称：<asp:TextBox ID="txtCourseName" runat="server" Text=""></asp:TextBox><br />
        主讲人：<asp:TextBox ID="txtlecturer" runat="server" Text=""></asp:TextBox><br />
        主讲人职务：<asp:TextBox ID="txtLecturerJob" runat="server" Text=""></asp:TextBox><br />
        课件数量：<asp:TextBox ID="txtCourseAmount" runat="server" Text=""></asp:TextBox><br />
        课件来源：<asp:TextBox ID="txtCourseSource" runat="server" Text=""></asp:TextBox><br />
        图文类标识：<asp:TextBox ID="txtTextCategory" runat="server" Text=""></asp:TextBox><br />
        备注：<asp:TextBox ID="txtCreateNote" runat="server" TextMode="MultiLine"></asp:TextBox><br />
        特殊要求：<asp:TextBox ID="txtExtraNote" runat="server" TextMode="MultiLine" ForeColor="Red"></asp:TextBox><br />
        <asp:Label ID="labelPlanNote" runat="server" Text="任务备注："></asp:Label><asp:TextBox ID="txtPlanNote" runat="server" TextMode="MultiLine"></asp:TextBox>
    </asp:Panel>
    <asp:Panel ID="PanelReceive" runat="server" Visible="false">
        <div class="PanelName">工单负责人填写:</div>
        工单负责人：<asp:TextBox ID="txtSigner" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        接收时间：<asp:TextBox ID="txtReceiveDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        备注：<asp:TextBox ID="txtReceiveNote" runat="server" Text=""></asp:TextBox><br />
        指定操作员：<asp:DropDownList ID="ddlOperator" runat="server"></asp:DropDownList><br />
    </asp:Panel>
    <asp:Panel ID="PanelOperation" runat="server" Visible="false">
        <div class="PanelName">操作员填写:</div>
        完成时间：<asp:TextBox ID="txtFinishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        备注：<asp:TextBox ID="txtOperationNote" runat="server" Text=""></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelPublish" runat="server" Visible="false">
        <div class="PanelName">发布员填写:</div>
        发布员：<asp:TextBox ID="txtPublisher" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        发布时间：<asp:TextBox ID="txtPublishDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        播放是否正常：<asp:DropDownList ID="ddlPublishCheck" runat="server"></asp:DropDownList><br />
        备注：<asp:TextBox ID="txtPublishNote" runat="server" Text=""></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelCheck" runat="server" Visible="false">
        <div class="PanelName">审核员填写:</div>
        审核员：<asp:TextBox ID="txtChecker" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        完成时间：<asp:TextBox ID="txtCheckDate" runat="server" Text="自动填充" ReadOnly="true"></asp:TextBox><br />
        栏目是否正确：<asp:DropDownList ID="ddlCategoryCheck" runat="server"></asp:DropDownList><br />
        备注：<asp:TextBox ID="txtCheckNote" runat="server" Text=""></asp:TextBox><br />
    </asp:Panel>
    <asp:Panel ID="PanelBaseBtn" runat="server">
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
        <asp:Button ID="btnOk" runat="server" Text="派发工单" OnClick="btnOk_Click" Visible="false" />
    </asp:Panel>
</asp:Content>
