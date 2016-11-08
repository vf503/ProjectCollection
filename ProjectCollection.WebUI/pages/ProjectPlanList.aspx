<%@ Page Title="" Language="C#" MasterPageFile="~/pages/common/Master.Master" AutoEventWireup="true" CodeBehind="ProjectPlanList.aspx.cs" Inherits="ProjectCollection.WebUI.pages.ProjectPlanList" %>

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
    <asp:Button ID="btnCreate" runat="server" Text="新建" OnClick="btnCreate_Click" Visible="false" />
    <p>
        <%--<asp:Button ID="btnSearch" runat="server" Text="查找" OnClick="btnSearch_Click" />--%>
        <asp:GridView ID="gvProjectPlan" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectPlanId" OnSelectedIndexChanging="gvProjectPlan_SelectedIndexChanging"
            OnPageIndexChanging="gvProjectPlan_PageIndexChanging" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Vertical" AllowPaging="True" PageSize="40" Width="1000px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ProjectPlanNo" HeaderText="项目计划编号"/>
                <asp:BoundField DataField="Title" HeaderText="标题" />
                <asp:BoundField DataField="ProjectPlanTypeText" HeaderText="项目类型" />
                <asp:BoundField DataField="MakingDate" HeaderText="创建时间" DataFormatString="{0: yyyy年MM月dd日}" />
                <asp:BoundField DataField="ProgressText" HeaderText="进度" />
                <asp:CommandField ShowSelectButton="True" SelectText="选择" ButtonType="Image" SelectImageUrl="../images/BtnSelect.gif" ControlStyle-Width="44px" ControlStyle-Height="21px" ControlStyle-BorderStyle="None" ControlStyle-CssClass="BtnSelect" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#D6EBFE" Font-Bold="True" ForeColor="#3285e5" Font-Names="Microsoft YaHei" Height="40px" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />

        </asp:GridView>
    </p>
</asp:Content>
