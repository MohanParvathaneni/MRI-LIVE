<%@ Page Title="Who is" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WhoIs.aspx.cs" Inherits="MRI.Views.Admin.WhoIs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h1>Display users in group</h1>
        <br />
        <asp:DropDownList ID="ddlWhoIs" runat="server" AutoPostBack="true">
<%--            <asp:ListItem Text="ACG" Value="Intranet_ServicePortal_ACGs"></asp:ListItem>
            <asp:ListItem Text="Admin" Value="Intranet_ServicePortal_Admins"></asp:ListItem>
            <asp:ListItem Text="Managers" Value="Intranet_ServicePortal_Managers"></asp:ListItem>--%>
        </asp:DropDownList>
        <br /><br />
        <p><asp:Label ID="lblGroup" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></p>
        <asp:Panel ID="pnlWho" runat="server">
        </asp:Panel>
    </div>

</asp:Content>
