<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Submission.aspx.cs" Inherits="MRI.Views.Admin.Submission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phMessage" runat="server"></asp:PlaceHolder>
    <asp:Panel ID="pnlSubmission" runat="server" Visible="false">
        <asp:Literal ID="literalTitle" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-md-2">
                Year:<br />
                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                Quarter:<br />
                <asp:RadioButtonList ID="rblQuarter" runat="server">
                    <asp:ListItem Text="1<sup>st</sup> Quarter (January to March)" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2<sup>nd</sup> Quarter (April to June)" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3<sup>rd</sup> Quarter (July to September)" Value="3"></asp:ListItem>
                    <asp:ListItem Text="<sup>4th</sup> Quarter (October to December)" Value="4"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:Button ID="btnCreateReport" runat="server" Text="Create Reports" OnClick="btnCreateReport_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:PlaceHolder ID="phFilesLinks" runat="server"></asp:PlaceHolder>
</asp:Content>
