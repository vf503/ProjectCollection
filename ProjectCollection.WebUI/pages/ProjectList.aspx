<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="ProjectList.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectList" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .BtnSelect {
            margin: 0;
            padding: 0;
        }

        .PanelProgress {
            width: 1024px;
            padding: 5px 0 0 0;
            background-color: white;
            position: fixed;
            bottom: 0;
            opacity: 0.90;
            _position: absolute;
            _bottom: auto;
            _top: expression(eval(document.documentElement.scrollTop));
        }

        .ProgressTitle {
            height: 30px;
        }

        .ProgressBox {
            position: relative;
            float: left;
            border-right: 2px solid white;
            padding:0 2px 0 2px;
            height: 60px;
            min-width: 75px;
            text-align: center;
        }

        .RadioButtonListPlanType {
        }

            .RadioButtonListPlanType input {
                width: 15px;
            }

            .RadioButtonListPlanType label {
                display: inline;
            }

        .MainList {
            min-width: 1000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelFilter" runat="server" Visible="false">
                <asp:RadioButtonList ID="RadioButtonListPlanType" runat="server" OnSelectedIndexChanged="SelectSetChanged" AutoPostBack="true" CssClass="RadioButtonListPlanType" RepeatDirection="Horizontal">
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000000" Selected="True">全部</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000020">自筹</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000016">外拍</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000014">采购</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000201">客户</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RadioButtonList ID="RadioButtonListProgress" runat="server" OnSelectedIndexChanged="SelectSetChanged" AutoPostBack="true" CssClass="RadioButtonListPlanType" RepeatDirection="Horizontal">
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000000" Selected="True">全部</asp:ListItem>
                    <asp:ListItem Value="99999999-9999-9999-9999-999999999999">未完成</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000119">已完成</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-000000000128">已废除</asp:ListItem>
                    <asp:ListItem Value="00000000-0000-0000-0000-999999999999">有延迟</asp:ListItem>
                </asp:RadioButtonList>
                <%--<asp:Button ID="btnFilter" runat="server" Text="查询" OnClick="btnFilter_Click" />--%>
            </asp:Panel>
            <asp:Panel ID="PanelProgress" runat="server" CssClass="PanelProgress" Visible="false">
                <asp:Panel ID="ProgressInfo" runat="server" CssClass="ProgressTitle">
                    <asp:Label ID="ProgressNo" runat="server" Text=""></asp:Label>
                    <asp:Label ID="ProgressDate" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="ProgressTitle" runat="server" Text=""></asp:Label>
                    <asp:Label ID="ProgressLecturer" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressCapture" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="CaptureInfo" runat="server" Text="采集"></asp:Label>
                    <br />
                    <asp:Label ID="CaptureDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CaptureState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressCaptureCheck" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="CaptureCheckInfo" runat="server" Text="预审"></asp:Label>
                    <br />
                    <asp:Label ID="CaptureCheckDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CaptureCheckState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressShorthand" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ShorthandInfo" runat="server" Text="速记"></asp:Label>
                    <br />
                    <asp:Label ID="ShorthandDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ShorthandState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressContentReceive" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ContentReceiveInfo" runat="server" Text="制作部接收"></asp:Label>
                    <br />
                    <asp:Label ID="ContentReceiveDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ContentReceiveState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressContentOperator" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ContentOperatorInfo" runat="server" Text="制作部制作"></asp:Label>
                    <br />
                    <asp:Label ID="ContentOperatorDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ContentOperatorState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressContentCheck" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ContentCheckInfo" runat="server" Text="制作部审核"></asp:Label>
                    <br />
                    <asp:Label ID="ContentCheckDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ContentCheckState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressContentRecheck" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ContentRecheckInfo" runat="server" Text="制作部复审"></asp:Label>
                    <br />
                    <asp:Label ID="ContentRecheckDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ContentRecheckState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressProductionReceive" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ProductionReceiveInfo" runat="server" Text="技术部接收"></asp:Label>
                    <br />
                    <asp:Label ID="ProductionReceiveDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ProductionReceiveState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressProductionOperator" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ProductionOperatorInfo" runat="server" Text="技术部制作"></asp:Label>
                    <br />
                    <asp:Label ID="ProductionOperatorDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ProductionOperatorState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressProductionCheck" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="ProductionCheckInfo" runat="server" Text="技术部审核"></asp:Label>
                    <br />
                    <asp:Label ID="ProductionCheckDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="ProductionCheckState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressPublish" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="PublishInfo" runat="server" Text="发布"></asp:Label>
                    <br />
                    <asp:Label ID="PublishDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="PublishState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressCheck" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
                    <asp:Label ID="CheckInfo" runat="server" Text="审核"></asp:Label>
                    <br />
                    <asp:Label ID="CheckDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="CheckState" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="ProgressFinal" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox" Visible="false">
                    <asp:Label ID="FinalInfo" runat="server" Text="总计"></asp:Label>
                    <br />
                    <asp:Label ID="FinalDuration" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="FinalState" runat="server" Text="无限定期限"></asp:Label>
                </asp:Panel>
            </asp:Panel>
            <%--<asp:Button ID="btnSearch" runat="server" Text="查找" OnClick="btnSearch_Click" />--%>
            <asp:GridView ID="gvProject" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="10" CssClass="MainList" DataKeyNames="ProjectId" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="gvProject_PageIndexChanging" OnRowCommand="CustomersGridView_RowCommand" OnRowDataBound="gvProject_RowDataBound" OnSelectedIndexChanging="gvProject_SelectedIndexChanging" PageSize="40">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField ControlStyle-Width="45px" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:Label ID="LabelAll" runat="server" Font-Size="14px" Text="全选:"></asp:Label>
                            <asp:CheckBox ID="CheckBoxAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxAll_Changed" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectCheckBox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProjectNo" HeaderText="工单编号" ItemStyle-Width="115px" />
                    <asp:BoundField DataField="CourseName" HeaderText="课件标题" />
                    <asp:BoundField DataField="ProjectPlanName" HeaderText="专题名" />
                    <asp:BoundField DataField="lecturer" HeaderStyle-Width="60px" HeaderText="主讲人" />
                    <%--<asp:BoundField DataField="WorkTypeText" HeaderText="用途" />--%>
                    <asp:BoundField DataField="ProgressText" HeaderText="进度" />
                    <asp:BoundField DataField="SendingDate" DataFormatString="{0: yy年MM月dd日}" HeaderText="派单时间" ItemStyle-Width="90px" />
                    <asp:CommandField ButtonType="Image" ControlStyle-BorderStyle="None" ControlStyle-CssClass="BtnSelect" ControlStyle-Height="21px" ControlStyle-Width="44px" SelectImageUrl="../images/BtnSelect.gif" SelectText="选择" ShowSelectButton="True" />
                    <asp:ButtonField ButtonType="Image" CommandName="Copy" ControlStyle-BorderStyle="None" ControlStyle-CssClass="BtnSelect" ControlStyle-Height="21px" ControlStyle-Width="44px" HeaderText="" ImageUrl="../images/BtnCopy.gif" Text="复制" Visible="false" />
                    <asp:TemplateField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Button ID="btnShowProgress" runat="server" Text="" OnClick="btnShowProgress_Click"/>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnBatchSave" runat="server" Text="批量保存" OnClick="btnBatch_Click" Visible="false" />
    <asp:Button ID="btnBatchHandle" runat="server" Text="批量处理" OnClick="btnBatch_Click" Visible="false" />
</asp:Content>
