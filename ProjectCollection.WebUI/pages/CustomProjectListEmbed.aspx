﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomProjectListEmbed.aspx.cs" Inherits="ProjectCollection.WebUI.CustomProjectListEmbed" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../pages/common/Site.css" rel="stylesheet" />
    <script type="text/javascript" src="../../script/jquery-1.8.2.min.js"></script>
    <script src="../script/jquery-ui.js"></script>
    <link type="text/css" href="../script/jQuery-Timepicker-Addon/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="../script/jQuery-Timepicker-Addon/jquery-ui-timepicker-addon.css" type="text/css" />
    <script src="../script/jQuery-Timepicker-Addon/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="../script/js/jquery.ui.datepicker-zh-CN.js.js" charset="gb2312"></script>
    <script src="../script/js/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
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

        .MainList {
            min-width: 1000px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxGridView ID="axgvProject" ClientInstanceName="axgvProject" runat="server" CssClass="MainList" KeyFieldName="CustomProjectId"
        OnPageIndexChanged="axgvProject_PageIndexChanged" AutoGenerateColumns="False" OnCustomButtonCallback="axgvProject_CustomButtonCallback"
        OnDetailRowExpandedChanged="axgvProject_DataHandle" OnAfterPerformCallback="axgvProject_DataHandle">
        <Columns>
            <dx:GridViewCommandColumn Caption="" ShowClearFilterButton="True" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn FieldName="No" Caption="编号" Settings-AutoFilterCondition="Contains" VisibleIndex="1" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Title" Caption="名称" Settings-AutoFilterCondition="Contains" VisibleIndex="2" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="SendingDate" Caption="派发日期" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="Lecturer" Caption="主讲人" Settings-AutoFilterCondition="Contains" VisibleIndex="4" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="ProgressText" Caption="进度" Settings-AutoFilterCondition="Contains" VisibleIndex="5" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="操作" FieldName="Operate" VisibleIndex="6">
                <Settings AllowSort="False" />
                <Settings />
                <Settings AllowAutoFilter="False"></Settings>
                <DataItemTemplate>
                    <asp:HyperLink ID="aSelect" runat="server" Text="选择" Target="_parent"></asp:HyperLink>
<%--                    <dx:ASPxButton ID="btnShowPopup" runat="server" Text="操作" CommandArgument='<%# Eval("CustomProjectId")%>' OnCommand="btnShowPopup_Command">
                          <ClientSideEvents Click="function(s, e) { popup1.Show(); }" />
                    </dx:ASPxButton>--%>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <Templates>
        </Templates>
        <SettingsDetail ShowDetailRow="false" />
        <SettingsPager PageSize="10"></SettingsPager>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
    </form>
</body>
</html>
