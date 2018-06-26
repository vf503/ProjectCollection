<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="amount.aspx.cs" Inherits="ProjectCollection.WebUI.pages.amount" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../script/Chart.js"></script>
    <style type="text/css">
        .ControlPanel {
            width: 300px;
            height: 200px;
            border: 1px solid #b5b5b5;
            padding: 10px;
            margin: 20px 0 30px 20px;
            float: left;
            position: relative;
        }

        .ContentPanel {
            width: 500px;
            border: none;
            padding: 0px 0 0 50px;
            margin: 0px;
            float: left;
            position: relative;
        }
    </style>
  <script>
        var BarOptions = {
            tooltips: {
                enabled:false,
                intersect: true,
                mode: 'nearest',
            },
            //showAllTooltips: true,
            legend: {
                display: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            hover: {
                animationDuration: 0
            },
            animation: {
                onComplete: function () {
                    this.chart.controller.draw();
                    drawValue(this, 1);
                },
                onProgress: function (state) {
                    var animation = state.animationObject;
                    drawValue(this, animation.currentStep / animation.numSteps);
                }
            }
        };
        var BarData = {
            labels: ["<%=ChartLabels%>"],
            datasets: [
                {
                label: '工单数',
                data: [<%=ChartCount%>],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
                }
            ]
        };
        var SubData = ["<%=ChartRate%>"];
        //Main
        $(document).ready(function () {
            var ctx = $("#BarChart");
            var BarChart = new Chart(ctx, {
                type: 'bar',
                data:BarData,
                options: BarOptions,
            });
        });
        //
        //Begin Show tooltips always even the stats are zero
        Chart.pluginService.register({
            beforeRender: function (chart) {
                if (chart.config.options.showAllTooltips) {
                    // create an array of tooltips
                    // we can't use the chart tooltip because there is only one tooltip per chart
                    chart.pluginTooltips = [];
                    chart.config.data.datasets.forEach(function (dataset, i) {
                        chart.getDatasetMeta(i).data.forEach(function (sector, j) {
                            chart.pluginTooltips.push(new Chart.Tooltip({
                                _chart: chart.chart,
                                _chartInstance: chart,
                                _data: chart.data,
                                _options: chart.options.tooltips,
                                _active: [sector]
                            }, chart));
                        });
                    });

                    // turn off normal tooltips
                    chart.options.tooltips.enabled = false;
                }
            },
            afterDraw: function (chart, easing) {
                if (chart.config.options.showAllTooltips) {
                    // we don't want the permanent tooltips to animate, so don't do anything till the animation runs atleast once
                    if (!chart.allTooltipsOnce) {
                        if (easing !== 1)
                            return;
                        chart.allTooltipsOnce = true;
                    }

                    // turn on tooltips
                    chart.options.tooltips.enabled = true;
                    Chart.helpers.each(chart.pluginTooltips, function (tooltip) {
                        tooltip.initialize();
                        tooltip.update();
                        // we don't actually need this since we are not animating tooltips
                        tooltip.pivot();
                        tooltip.transition(easing).draw();
                    });
                    chart.options.tooltips.enabled = false;
                }
            }
        });
        //End Show tooltips always even the stats are zero 

        //Begin ShowText
        // Font color for values inside the bar
        var insideFontColor = '255,255,255';
        // Font color for values above the bar
        var outsideFontColor = '0,0,0';
        // How close to the top edge bar can be before the value is put inside it
        var topThreshold = 20;

        var modifyCtx = function (ctx) {
            ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, 'normal', Chart.defaults.global.defaultFontFamily);
            ctx.textAlign = 'center';
            ctx.textBaseline = 'bottom';
            return ctx;
        };

        var fadeIn = function (ctx, obj, x, y, black, step) {
            var ctx = modifyCtx(ctx);
            var alpha = 0;
            ctx.fillStyle = black ? 'rgba(' + outsideFontColor + ',' + step + ')' : 'rgba(' + insideFontColor + ',' + step + ')';
            ctx.fillText(obj, x, y);
        };

        var drawValue = function (context, step) {
            var ctx = context.chart.ctx;

            context.data.datasets.forEach(function (dataset) {
                for (var i = 0; i < dataset.data.length; i++) {
                    var model = dataset._meta[Object.keys(dataset._meta)[0]].data[i]._model;
                    var textY = (model.y > topThreshold) ? model.y - 3 : model.y + 20;
                    fadeIn(ctx, dataset.data[i] +" (占"+SubData[i]+")", model.x, textY, model.y > topThreshold, step);
                }
            });
        };
        //End ShowText
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <H3>工单耗时统计</H3>
     <asp:Panel ID="manage" runat="server" Visible="true">
         <asp:Panel ID="PanelDate" runat="server" CssClass="ControlPanel">
            <dx:ASPxComboBox ID="cbType" runat="server" SelectedIndex="0">
                    <Items>
                    <dx:ListEditItem Value="%" Selected="True" Text="全部" />
                    <dx:ListEditItem Value="A%" Text="A类" />
                    <dx:ListEditItem Value="B%" Text="B类" />
                    <dx:ListEditItem Value="C%" Text="C类" />
                    <dx:ListEditItem Value="D%" Text="D类" />
                </Items>
                </dx:ASPxComboBox>
             <dx:ASPxComboBox ID="cbDay" runat="server" SelectedIndex="0">
                 <Items>
                     <dx:ListEditItem Value="CalendarDay" Selected="True" Text="自然日" />
                     <dx:ListEditItem Value="WorkDay" Text="工作日" />
                 </Items>
             </dx:ASPxComboBox>
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
        <asp:Panel ID="PanelCount" runat="server" CssClass="ContentPanel">
            <canvas id="BarChart" width="400" height="400"></canvas>
        </asp:Panel>
    </asp:Panel>
</asp:Content>