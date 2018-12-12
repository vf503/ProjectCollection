<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="CustomTaskListFliter.aspx.cs" Inherits="ProjectCollection.WebUI.pages.CustomTaskListFliter" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .BtnSelect {
            margin: 0;
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxRadioButtonList ID="rblMain" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblMain_SelectedIndexChanged">
        <Items>
            <dx:ListEditItem Text="普通工单" Value="normal" />
            <dx:ListEditItem Text="产品输出" Value="batch" Selected="true"/>
            <dx:ListEditItem Text="公开课" Value="OpenClass" />
        </Items>
    </dx:ASPxRadioButtonList>
    <dx:ASPxGridView ID="axgvProject" ClientInstanceName="axgvProject" runat="server" KeyFieldName="id"
        OnPageIndexChanged="axgvProject_PageIndexChanged" AutoGenerateColumns="False" OnCustomButtonCallback="axgvProject_CustomButtonCallback"
        OnDetailRowExpandedChanged="axgvProject_DataHandle" OnAfterPerformCallback="axgvProject_DataHandle">
        <Columns>
            <dx:GridViewDataColumn FieldName="custom" Caption="客户名称" Settings-AutoFilterCondition="Contains" VisibleIndex="0" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="CreateDate" Caption="创建日期" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="user_info.real_name" Caption="创建人" Settings-AutoFilterCondition="Contains" VisibleIndex="2" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="progress" Caption="进度" Settings-AutoFilterCondition="Contains" VisibleIndex="3" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="操作" FieldName="Operate" VisibleIndex="6">
                <Settings AllowSort="False" />
                <Settings />
                <Settings AllowAutoFilter="False"></Settings>
                <DataItemTemplate>
                    <asp:HyperLink ID="aSelect" runat="server" Text="选择"></asp:HyperLink>
<%--                    <dx:ASPxButton ID="btnShowPopup" runat="server" Text="操作" CommandArgument='<%# Eval("CustomProjectId")%>' OnCommand="btnShowPopup_Command">
                          <ClientSideEvents Click="function(s, e) { popup1.Show(); }" />
                    </dx:ASPxButton>--%>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <Templates>
        </Templates>
        <SettingsDetail ShowDetailRow="false" />
        <SettingsPager PageSize="20"></SettingsPager>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
<%--    <table id="popupArea">
        <tr>
            <td>
            
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="popup1" runat="server" AllowDragging="True" AllowResize="True"
        CloseAction="CloseButton" ContentUrl="ProjectCreateEdit.aspx"
        EnableViewState="False" PopupElementID="popupArea" PopupHorizontalAlign="Center"
        PopupVerticalAlign="Middle" ShowFooter="True" ShowOnPageLoad="True" Width="400px"
        Height="300px" FooterText="Try to resize the control using the resize grip or the control's edges"
        HeaderText="Feedback form"  ClientInstanceName="popup1" EnableHierarchyRecreation="True">
    </dx:ASPxPopupControl>--%>
</asp:Content>
