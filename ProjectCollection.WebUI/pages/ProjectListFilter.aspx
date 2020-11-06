<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="ProjectListFilter.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectListFilter" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .BtnSelect {
            margin: 0;
            padding: 0;
        }
        .inline {
            position: relative;
            float: left;
        }

        #btnProgressClose {
            Height: 20px;
            Width: 20px;
            font-size: 12px;
            padding: 0;
            margin-right: 20px;
            float: right;
            position: relative;
        }

        .PanelProgress {
            width: 1024px;
            padding: 5px 0 0 0;
            background-color: white;
            position: fixed;
            bottom: 0;
            opacity: 0.80;
            z-index:99;
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
            padding: 0 2px 0 2px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelProgress" runat="server" CssClass="PanelProgress" Visible="false">
        <asp:Panel ID="ProgressInfo" runat="server" CssClass="ProgressTitle">
            <asp:Label ID="ProgressNo" runat="server" Text=""></asp:Label>
            <asp:Label ID="ProgressDate" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="ProgressTitle" runat="server" Text=""></asp:Label>
            <asp:Label ID="ProgressLecturer" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnProgressClose" runat="server" OnClick="btnProgressClose_Click" Text="X" ClientIDMode="Static"/>
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
            <asp:Label ID="ShorthandInfo" runat="server" Text="编导"></asp:Label>
            <br />
            <asp:Label ID="ShorthandDuration" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="ShorthandState" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <!--新流程OP-->
        <asp:Panel ID="NewProgressProductionReceive" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
            <asp:Label ID="NewProductionReceiveInfo" runat="server" Text="技术部接收"></asp:Label>
            <br />
            <asp:Label ID="NewProductionReceiveDuration" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="NewProductionReceiveState" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="NewProgressProductionOperator" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
            <asp:Label ID="NewProductionOperatorInfo" runat="server" Text="技术部制作"></asp:Label>
            <br />
            <asp:Label ID="NewProductionOperatorDuration" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="NewProductionOperatorState" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="NewProgressProductionCheck" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
            <asp:Label ID="NewProductionCheckInfo" runat="server" Text="技术部审核"></asp:Label>
            <br />
            <asp:Label ID="NewProductionCheckDuration" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="NewProductionCheckState" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="NewProgressSTT" runat="server" BackColor="#e3f0f6" CssClass="ProgressBox">
            <asp:Label ID="NewProgressSTTInfo" runat="server" Text="字幕制作"></asp:Label>
            <br />
            <asp:Label ID="NewProgressSTTDuration" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="NewProgressSTTState" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <!--新流程ED-->
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
    <asp:Panel ID="PanelFilter" runat="server" Visible="false">
        <asp:RadioButtonList ID="RadioButtonListPlanType" runat="server" OnSelectedIndexChanged="SelectSetChanged" AutoPostBack="true" CssClass="RadioButtonListPlanType" RepeatDirection="Horizontal">
            <asp:ListItem Value="00000000-0000-0000-0000-000000000000" Selected="True">全部</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000020">自筹</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000016">外拍</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000014">采购</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000201">客户</asp:ListItem>
            <asp:ListItem Value="99999999-9999-9999-9999-000000000000">产品输出</asp:ListItem>
            <asp:ListItem Value="99999999-9999-9999-9999-999999999999">公开课</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RadioButtonList ID="RadioButtonListProgress" runat="server" OnSelectedIndexChanged="SelectSetChanged" AutoPostBack="true" CssClass="RadioButtonListPlanType" RepeatDirection="Horizontal">
            <asp:ListItem Value="00000000-0000-0000-0000-000000000000" Selected="True">全部</asp:ListItem>
            <asp:ListItem Value="99999999-9999-9999-9999-999999999999">未完成</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000119">已完成</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-000000000128">已废除</asp:ListItem>
            <asp:ListItem Value="00000000-0000-0000-0000-999999999999">有延迟</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RadioButtonList ID="RadioButtonListDate" runat="server" OnSelectedIndexChanged="SelectSetChanged" AutoPostBack="true" CssClass="RadioButtonListPlanType" RepeatDirection="Horizontal">
            <asp:ListItem Value="3m" Selected="True">近三个月</asp:ListItem>
            <asp:ListItem Value="12m">近一年</asp:ListItem>
            <asp:ListItem Value="all">历史全部</asp:ListItem>
        </asp:RadioButtonList>
        <%--<asp:Button ID="btnFilter" runat="server" Text="查询" OnClick="btnFilter_Click" />--%>
    </asp:Panel>
    <dx:ASPxGridView ID="axgvProject" ClientInstanceName="axgvProject" runat="server" KeyFieldName="ProjectId"
        OnPageIndexChanged="axgvProject_PageIndexChanged" AutoGenerateColumns="False" OnCustomButtonCallback="axgvProject_CustomButtonCallback"
        OnDetailRowExpandedChanged="axgvProject_DataHandle" OnAfterPerformCallback="axgvProject_DataHandle">
        <SettingsSearchPanel Visible="True" />
        <Columns>
            <dx:GridViewCommandColumn Caption="" ShowClearFilterButton="True" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn FieldName="ProjectNo" Caption="编号" Settings-AutoFilterCondition="Contains" VisibleIndex="1">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="CourseName" Caption="标题" Settings-AutoFilterCondition="Contains" VisibleIndex="2">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="ProjectPlanName" Caption="专题名" Settings-AutoFilterCondition="Contains" VisibleIndex="3">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="EpisodeCount" Caption="集数" Settings-AutoFilterCondition="Contains" VisibleIndex="3">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="SendingDate" Caption="派单日期" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="lecturer" Caption="主讲人" Settings-AutoFilterCondition="Contains" VisibleIndex="4">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="ProgressTotalText" Caption="进度" Settings-AutoFilterCondition="Contains" VisibleIndex="8">
                <Settings ShowFilterRowMenu="True"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="操作" FieldName="Operate" VisibleIndex="9" Name="ColOperate">
                <Settings AllowSort="False"></Settings>
                <Settings AllowAutoFilter="False"></Settings>
                <HeaderTemplate>
                    <asp:Panel ID="panelBrowseHead" runat="server" Visible="false">
                        有延迟<dx:ASPxCheckBox ID="cbDelay" OnCheckedChanged="cbDelay_CheckedChanged" AutoPostBack="true" runat="server"></dx:ASPxCheckBox>
                         </asp:Panel>
                </HeaderTemplate>
                <DataItemTemplate>
                    <dx:ASPxButton ID="btnShowPopup" runat="server" Text="查看信息" CommandArgument='<%# Eval("ProjectId") %>' CommandName="browse" OnCommand="btnShowPopup_Command">
                    </dx:ASPxButton>
                    <asp:Panel ID="panelBrowseCol" runat="server" Visible="false" CssClass="inline">
                        <dx:ASPxButton ID="btnProjectProgress" runat="server" Text="显示进度" CommandArgument='<%# Eval("ProjectId")%>' CommandName="ShowProjectProgress" OnCommand="btnProjectProgress_Click">
                        </dx:ASPxButton>
                    </asp:Panel>
                    <asp:Panel ID="panelCopyCol" runat="server" Visible="false" CssClass="inline">
                        <dx:ASPxButton ID="btnCopy" runat="server" Text="复制工单" CommandArgument='<%# Eval("ProjectId")%>' CommandName="copy" OnCommand="btnShowPopup_Command">
                        </dx:ASPxButton>
                    </asp:Panel>
                    <asp:Panel ID="panelEndCol" runat="server" Visible="false" CssClass="inline">
                        <dx:ASPxButton ID="btnEnd" runat="server" Text="终止制作" CommandArgument='<%# Eval("ProjectId")%>' CommandName="end" OnCommand="btnProjectEnd_Click">
                        </dx:ASPxButton>
                    </asp:Panel>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <Templates>
        </Templates>
        <SettingsDetail ShowDetailRow="false" />
        <SettingsPager PageSize="20"></SettingsPager>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
    <div id="popupArea">
    </div>
    <dx:ASPxPopupControl ID="popupEdit" runat="server" AllowDragging="True" AllowResize="True"
        CloseAction="CloseButton" EnableViewState="False" PopupElementID="popupArea" PopupHorizontalAlign="Center" PopupVerticalAlign="Below"
        AutoUpdatePosition="True" ShowFooter="True" ShowOnPageLoad="False" Width="1000px"
        Height="760px" FooterText="" HeaderText="工单编辑" EnableHierarchyRecreation="True" ClientInstanceName="popupEdit" ShowMaximizeButton="True">
        <ClientSideEvents PopUp="function(s, e) { 
             var pop = document.getElementById('ContentPlaceHolder1_popupEdit_PW-1');
             pop.style.top = GetCookie('scroll')+'px';
             }" 
            Init="function(s, e) { }" />
    </dx:ASPxPopupControl>
    <asp:HiddenField ID="hidViewState" runat="server"/>
    <script>
        console.log(window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0);
        function Trim(strValue) {
            return strValue.replace(/^\s*|\s*$/g, "");
        }
        function SetCookie(sName, sValue) {
            document.cookie = sName + "=" + escape(sValue);
        }
        function GetCookie(sName) {
            var aCookie = document.cookie.split(";");
            for (var i = 0; i < aCookie.length; i++) {
                var aCrumb = aCookie[i].split("=");
                if (sName == Trim(aCrumb[0])) {
                    return unescape(aCrumb[1]);
                }
            }
            return null;
        }
        function scrollback() {
            if (GetCookie("scroll") != null) { document.body.scrollTop = GetCookie("scroll") }
        }  
        $(window).scroll(function () {
            SetCookie("scroll", window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0);
            console.log(GetCookie("scroll"));
        });
    </script>
</asp:Content>
