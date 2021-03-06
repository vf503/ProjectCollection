﻿<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="ProjectPlanListMasterDetail.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectPlanListMasterDetail"  MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .BtnSelect {
            margin: 0;
            padding: 0;
        }

        #btnProgressClose {
             Height:20px;
             Width:20px;
             font-size:12px;
             padding: 0;
             margin-right:20px;
             float:right;
             position:relative; 
        }

        .PanelProgress {
            width: 1024px;
            padding: 5px 0 0 0;
            background-color: white;
            position: fixed;
            bottom: 0;
            opacity: 0.80;
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
    </style>
    <script type="text/javascript">
        //开启后 ASPxGridView无限加载
        //$(document).ready(function () {
        //    $("div").eq(0).remove();
        //})
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxTabControl ID="ASPxTabControlMain" runat="server" TabAlign="Center" Width="1024px" Target="_self">
        <Tabs>
            <dx:Tab NavigateUrl="~/pages/ProjectPlanListMasterDetail.aspx" Text="制作中">
            </dx:Tab>
            <dx:Tab NavigateUrl="~/pages/ProjectPlanListMasterDetail.aspx?mode=browse" Text="历史">
            </dx:Tab>
        </Tabs>
    </dx:ASPxTabControl>
    <asp:Button ID="btnCreate" runat="server" Text="新建" OnClick="btnCreate_Click" Visible="false" />
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
            <asp:Label ID="ShorthandInfo" runat="server" Text="速记"></asp:Label>
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
    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" ValueType="System.String"></dx:ASPxRadioButtonList>
    <dx:ASPxGridView ID="axgvProjectPlan" ClientInstanceName="axgvProjectPlan" runat="server" KeyFieldName="ProjectPlanId"
        OnPageIndexChanged="axgvProjectPlan_PageIndexChanged" AutoGenerateColumns="False" OnCustomButtonCallback="axgvProjectPlan_CustomButtonCallback"
        OnDetailRowExpandedChanged="axgvProjectPlan_DataHandle" OnAfterPerformCallback="axgvProjectPlan_DataHandle">
        <Columns>
            <dx:GridViewCommandColumn Caption="计划" ShowClearFilterButton="True" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn FieldName="ProjectPlanNo" Caption="编号" Settings-AutoFilterCondition="Contains" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="Title" Caption="名称" Settings-AutoFilterCondition="Contains" VisibleIndex="2" />
            <dx:GridViewDataColumn FieldName="ProjectPlanTypeText" Caption="类型" Settings-AutoFilterCondition="Contains" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="MakingDate" Caption="创建日期" VisibleIndex="4" />
            <dx:GridViewDataColumn FieldName="PlanDate" Caption="计划日期" VisibleIndex="5" />
            <dx:GridViewDataColumn FieldName="CreatorName" Caption="创建人" VisibleIndex="6" />
            <dx:GridViewDataColumn FieldName="ProgressText" Caption="进度" Settings-AutoFilterCondition="Contains" VisibleIndex="7" />
            <dx:GridViewDataColumn FieldName="ProjectDelayCount" Caption="延迟工单数" Settings-ShowFilterRowMenu="True" VisibleIndex="8" />
            <dx:GridViewDataColumn Caption="工单进展统计" FieldName="" VisibleIndex="9">
                <DataItemTemplate>
                    <%# Eval("ProjectFinishCount")%>/<%# Eval("ProjectCount")%>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="操作" FieldName="Operate" VisibleIndex="10">
                <Settings AllowSort="False" />
                <Settings />
                <Settings AllowAutoFilter="False"></Settings>
                <DataItemTemplate>
                    <asp:HyperLink ID="aSelect" runat="server" Text="选择"></asp:HyperLink>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <%# Eval("Title")%>
                <dx:ASPxGridView ID="axgvProject" runat="server" KeyFieldName="ProjectId" OnBeforePerformDataSelect="axgvProject_BeforePerformDataSelect">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="ProjectNo" Caption="编号" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="CourseName" Caption="课名" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="EpisodeCount" Caption="集数" VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="lecturer" Caption="主讲人" VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="ProgressTotalText" Caption="进度" VisibleIndex="5" />
                        <dx:GridViewDataColumn Caption="操作" FieldName="Operate" Settings-AllowAutoFilter="False" VisibleIndex="5">
                            <Settings AllowAutoFilter="False" />
                            <DataItemTemplate>
                                <asp:Button ID="btnProjectProgress" runat="server" Text="显示进度" CommandArgument='<%# Eval("ProjectId")%>' CommandName="ShowProjectProgress" OnCommand="btnProjectProgress_Click" />
                                <asp:HyperLink ID="aProjectSelect" runat="server" NavigateUrl='<%#"ProjectCreateEdit.aspx?mode=browse&ProjectId="+ Eval("ProjectId")+"&type="+ Eval("ProjectTypeId")%>' Text="查看信息" Target="_blank"></asp:HyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
        <SettingsPager PageSize="20"></SettingsPager>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
</asp:Content>
