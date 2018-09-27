<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="MyTask.aspx.cs" Inherits="ProjectCollection.WebUI.pages.MyTask" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var browserVersion = window.navigator.userAgent.toUpperCase();
        var isOpera = browserVersion.indexOf("OPERA") > -1 ? true : false;
        var isFireFox = browserVersion.indexOf("FIREFOX") > -1 ? true : false;
        var isChrome = browserVersion.indexOf("CHROME") > -1 ? true : false;
        var isSafari = browserVersion.indexOf("SAFARI") > -1 ? true : false;
        var isIE = (!!window.ActiveXObject || "ActiveXObject" in window);
        var isIE9More = (! -[1, ] == false);
        function reinitIframe(iframeId, minHeight) {
            try {
                var iframe = document.getElementById(iframeId);
                var bHeight = 0;
                if (isChrome == false && isSafari == false)
                    bHeight = iframe.contentWindow.document.body.scrollHeight;

                var dHeight = 0;
                if (isFireFox == true)
                    dHeight = iframe.contentWindow.document.documentElement.offsetHeight + 2;
                else if (isIE == false && isOpera == false) {
                    //dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
                    try {
                        bHeight = iframe.contentWindow.document.body.scrollHeight;
                        //dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
                        //console.log(bHeight);
                    } catch (ex) { }
                }
                else if (isIE == true && isIE9More) {//ie9+
                    var heightDeviation = bHeight - eval("window.IE9MoreRealHeight" + iframeId);
                    if (heightDeviation == 0) {
                        bHeight += 3;
                    } else if (heightDeviation != 3) {
                        eval("window.IE9MoreRealHeight" + iframeId + "=" + bHeight);
                        bHeight += 3;
                    }
                }
                else//ie[6-8]、OPERA
                    bHeight += 3;

                var height = Math.max(bHeight, dHeight);
                if (height < minHeight) height = minHeight;
                iframe.style.height = height + "px";
            } catch (ex) { }
        }
        function startInit(iframeId, minHeight) {
            eval("window.IE9MoreRealHeight" + iframeId + "=0");
            window.setInterval("reinitIframe('" + iframeId + "'," + minHeight + ")", 500);
        }

        $(document).ready(function () {
            $("iframe").each(function () {
                startInit($(this).attr("id"), 10);
            });
        });
    </script>
    <style type="text/css">
        iframe {
            border:none;
            padding: 0px; 
            width:1000px;
            height: 1000px;
        }
        .ControlPanel {
            width:400px;
            height:200px;
            border:1px solid #b5b5b5;
            padding: 10px; 
            margin: 20px 0 30px 20px;
            float:left;
            position:relative; 
        }
        .ContentPanel {
            width:1000px;
            border:none;
            padding:0px; 
            margin:0px;
            float:none;
            position:relative; 
            clear:both;
        }
        .ModeRbl {
            margin:0 auto;
            position:relative;
            border:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="manage" runat="server" Visible="true">
        <%--<dx:ASPxTabControl ID="TabMain" runat="server" TabAlign="Center" Width="1024px" Target="_self" ActiveTabIndex="0">
            <Tabs>
                <dx:Tab NavigateUrl="~/pages/MyTask.aspx?mode=manage&range=now" Text="当前">
                </dx:Tab>
                <dx:Tab NavigateUrl="~/pages/MyTask.aspx?mode=manage&range=all" Text="历史">
                </dx:Tab>   
            </Tabs>
        </dx:ASPxTabControl>--%>
        <dx:ASPxRadioButtonList ID="rblMode" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblMode_SelectedIndexChanged" CssClass="ModeRbl">
            <Items>
                <dx:ListEditItem Text="当前" Value="now" Selected="true"/>
                <dx:ListEditItem Text="全部" Value="all" />
            </Items>
        </dx:ASPxRadioButtonList>
        <asp:Panel ID="PanelUserName" runat="server" CssClass="ControlPanel">
            <dx:ASPxListBox ID="ListBoxUser" runat="server" AutoPostBack="True" Native="True" Rows="10">
            </dx:ASPxListBox>
        </asp:Panel>
        <asp:Panel ID="PanelDate" runat="server" CssClass="ControlPanel">
            <dx:ASPxDateEdit ID="deStart" runat="server">
                <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="True" ErrorText="Start date is required"></RequiredField>
                </ValidationSettings>
            </dx:ASPxDateEdit>
            <dx:ASPxDateEdit ID="deEnd" runat="server">
                <DateRangeSettings StartDateEditID="deStart"></DateRangeSettings>
                <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="True" ErrorText="End date is required"></RequiredField>
                </ValidationSettings>
            </dx:ASPxDateEdit>
            <dx:ASPxValidationSummary ID="ASPxValidationSummary1" runat="server" ClientInstanceName="validationSummary" ShowErrorsInEditors="True">
            </dx:ASPxValidationSummary>
            <dx:ASPxButton ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" />
        </asp:Panel>
    </asp:Panel>
    <div id="accordion" class="ContentPanel">
        <asp:Panel ID="Panelrecond" runat="server" Visible="false">
            <asp:Label ID="Labelrecond" runat="server" Text="摄像"></asp:Label>
            <iframe id="Iframerecond" runat="server"></iframe>
            <asp:Label ID="LabelAmountrecond" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcapture" runat="server" Visible="false">
            <asp:Label ID="Labelcapture" runat="server" Text="采集"></asp:Label>
            <iframe id="Iframecapture" runat="server"></iframe>
            <asp:Label ID="LabelAmountcapture" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcapturecheck" runat="server" Visible="false">
            <asp:Label ID="Labelcapturecheck" runat="server" Text="制作预审"></asp:Label>
            <iframe id="Iframecapturecheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountcapturecheck" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelexecution" runat="server" Visible="false">
            <asp:Label ID="Labelexecution" runat="server" Text="派发制作"></asp:Label>
            <iframe id="Iframeexecution" runat="server"></iframe>
            <asp:Label ID="LabelAmountexecution" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelshorthand" runat="server" Visible="false">
            <asp:Label ID="Labelshorthand" runat="server" Text="速记"></asp:Label>
            <iframe id="Iframeshorthand" runat="server"></iframe>
            <asp:Label ID="LabelAmountshorthand" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcontentreceive" runat="server" Visible="false">
            <asp:Label ID="Labelcontentreceive" runat="server" Text="内容部接收"></asp:Label>
            <iframe id="Iframecontentreceive" runat="server"></iframe>
            <asp:Label ID="LabelAmountcontentreceive" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcontentfinish" runat="server" Visible="false">
            <asp:Label ID="Labelcontentfinish" runat="server" Text="内容部制作"></asp:Label>
            <iframe id="Iframecontentfinish" runat="server"></iframe>
            <asp:Label ID="LabelAmountcontentfinish" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcontentcheck" runat="server" Visible="false">
            <asp:Label ID="Labelcontentcheck" runat="server" Text="内容部初审"></asp:Label>
            <iframe id="Iframecontentcheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountcontentcheck" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcontentrecheck" runat="server" Visible="false">
            <asp:Label ID="Labelcontentrecheck" runat="server" Text="内容部复审"></asp:Label>
            <iframe id="Iframecontentrecheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountcontentrecheck" runat="server" Visible="false" Text=""></asp:Label>
            <br />
            <asp:Label ID="LabelAmountcontentcheckTotal" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelproductionreceive" runat="server" Visible="false">
            <asp:Label ID="Labelproductionreceive" runat="server" Text="技术部接收"></asp:Label>
            <iframe id="Iframeproductionreceive" runat="server"></iframe>
            <asp:Label ID="LabelAmountproductionreceive" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelproductionfinish" runat="server" Visible="false">
            <asp:Label ID="Labelproductionfinish" runat="server" Text="技术部制作"></asp:Label>
            <iframe id="Iframeproductionfinish" runat="server"></iframe>
            <asp:Label ID="LabelAmountproductionfinish" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelproductioncheck" runat="server" Visible="false">
            <asp:Label ID="Labelproductioncheck" runat="server" Text="技术部审核"></asp:Label>
            <iframe id="Iframeproductioncheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountproductioncheck" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelpublish" runat="server" Visible="false">
            <asp:Label ID="Labelpublish" runat="server" Text="发布"></asp:Label>
            <iframe id="Iframepublish" runat="server"></iframe>
            <asp:Label ID="LabelAmountpublish" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panelcheck" runat="server" Visible="false">
            <asp:Label ID="Labelcheck" runat="server" Text="审核"></asp:Label>
            <iframe id="Iframecheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountcheck" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelOpenClassReceive" runat="server" Visible="false">
            <asp:Label ID="LabelOpenClassReceive" runat="server" Text="公开课接收"></asp:Label>
            <iframe id="IframeOpenClassReceive" runat="server"></iframe>
            <asp:Label ID="LabelAmountOpenClassReceive" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelOpenClassOperation" runat="server" Visible="false">
            <asp:Label ID="LabelOpenClassOperation" runat="server" Text="公开课制作"></asp:Label>
            <iframe id="IframeOpenClassOperation" runat="server"></iframe>
            <asp:Label ID="LabelAmountOpenClassOperation" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelOpenClassPublish" runat="server" Visible="false">
            <asp:Label ID="LabelOpenClassPublish" runat="server" Text="公开课发布"></asp:Label>
            <iframe id="IframeOpenClassPublish" runat="server"></iframe>
            <asp:Label ID="LabelAmountOpenClassPublish" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelOpenClassCheck" runat="server" Visible="false">
            <asp:Label ID="LabelOpenClassCheck" runat="server" Text="公开课审核"></asp:Label>
            <iframe id="IframeOpenClassCheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountOpenClassCheck" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelCustomCheck" runat="server" Visible="false">
            <asp:Label ID="LabelCustomCheck" runat="server" Text="输出任务审核"></asp:Label>
            <iframe id="IframeCustomCheck" runat="server"></iframe>
            <asp:Label ID="LabelAmountCustomCheck" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelCustomExecute" runat="server" Visible="false">
            <asp:Label ID="LabelCustomExecute" runat="server" Text="输出任务执行"></asp:Label>
            <iframe id="IframeCustomExecute" runat="server"></iframe>
            <asp:Label ID="LabelAmountCustomExecute" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelCustomHelpExecute" runat="server" Visible="false">
            <asp:Label ID="LabelCustomHelpExecute" runat="server" Text="输出任务执行(辅助)"></asp:Label>
            <iframe id="IframeCustomHelpExecute" runat="server"></iframe>
            <asp:Label ID="LabelAmountCustomHelpExecute" runat="server" Visible="false" Text=""></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
