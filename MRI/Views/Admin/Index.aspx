<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MRI.Views.Admin.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h1>Admin page</h1>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4">
            <p><a class="btn btn-default" href="WhoIs.aspx">Who is in groups &raquo;</a></p>
            <p>Select this option if you want to know who are in the various groups.</p>
        </div>
        <div class="col-md-4">
            <asp:PlaceHolder ID="phManageSites" runat="server" Visible="false">
            <p><a class="btn btn-default" href="ManageSites.aspx">Manage sites &raquo;</a></p>
            <p>
                Select this option if you want to manage the sites list.
            </p>
            </asp:PlaceHolder>
        </div>
        <div class="col-md-4">
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManagePhysicians.aspx">Manage physicians &raquo;</a></p>
            <p>
                Select this option if you want to manage the physicians list.
            </p>
        </div>
        <div class="col-md-4">
            <p><a class="btn btn-default" href="ManagePhysiciansSites.aspx">Manage physicians sites &raquo;</a></p>
            <p>
                Select this option if you want to manage the physicians sites they are assigned to.
            </p>
        </div>
         <div class="col-md-4">
            <p><a class="btn btn-default" href="PhysicianPrefixCodes.aspx">Manage physicians prefix codes &raquo;</a></p>
            <p>
                Select this option if you want to manage the physicians prefix codes.
            </p>
        </div>
        <div class="col-md-4">
        </div>
    </div>

</asp:Content>
