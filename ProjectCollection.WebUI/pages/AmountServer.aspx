<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="AmountServer.aspx.cs" Inherits="ProjectCollection.WebUI.pages.AmountServer" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ControlPanel {
            width: 400px;
            height: 200px;
            border: 1px solid #b5b5b5;
            padding: 10px;
            margin: 20px 0 30px 20px;
            float: left;
            position: relative;
        }

        .ContentPanel {
            width: 1000px;
            border: none;
            padding: 0px;
            margin: 0px;
            float: left;
            position: relative;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <asp:Chart ID="Chart1" runat="server" Width="800">
                <Series>
                    <asp:Series Name="Series1" ChartType="Doughnut"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" ></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel>
    </asp:Panel>
</asp:Content>