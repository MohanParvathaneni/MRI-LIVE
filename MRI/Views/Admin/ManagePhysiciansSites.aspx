<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePhysiciansSites.aspx.cs" Inherits="MRI.Views.Admin.ManagePhysiciansSites" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage Physicians - Sites</h3>
        Filter by site: <asp:DropDownList ID="ddlSitesFilter" runat="server" DataSourceID="edsSites" DataTextField="SiteName" DataValueField="SiteId" AppendDataBoundItems="true">
            <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
        </asp:DropDownList>
        <br />
        Physician last name contains: <asp:TextBox ID="tbPhysicianLastName" runat="server"></asp:TextBox>
        <br />
        Physician first name contains: <asp:TextBox ID="tbPhysicianFirstName" runat="server"></asp:TextBox>
        <br />
        Physician license number contains: <asp:TextBox ID="tbPhysicianLicenseNumber" runat="server"></asp:TextBox>
        &nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Apply Filters" OnClick="btnRefresh_Click" />
        <br />
        <asp:GridView ID="gvPhysicianSites" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PhysicianSiteId" DataSourceID="edsPhysicianSites"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="30">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="PhysicianSiteId" HeaderText="Physician Site Id" ReadOnly="True" SortExpression="PhysicianSiteId" Visible="false" />
                <asp:TemplateField HeaderText="Physician" SortExpression="Physicians.Name">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="edsPhysicians" DataTextField="DisplayName" DataValueField="PhysicianId" SelectedValue='<%# Bind("PhysicianId") %>'></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# GetName(Eval("PhysicianId")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Site" SortExpression="Sites.SiteName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="edsSites" DataTextField="SiteName" DataValueField="SiteId" SelectedValue='<%# Bind("SiteId") %>'></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# GetSiteName(Eval("SiteId")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <asp:FormView ID="fvPhysicianSites" runat="server"
            DataKeyNames="PhysicianSiteId" DataSourceID="edsPhysicianSites"
            DefaultMode="Insert" Visible="False">
            <InsertItemTemplate>
                <div class="row">
                    <div class="col-md-11">
                        <span style="color:red;">*</span>&nbsp;Physician:<br />
                        <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="edsPhysicians" DataTextField="DisplayName" DataValueField="PhysicianId" SelectedValue='<%# Bind("PhysicianId") %>'></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <span style="color:red;">*</span>&nbsp;Site:<br />
                        <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="edsSites" DataTextField="SiteName" DataValueField="SiteId" SelectedValue='<%# Bind("SiteId") %>'></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%# Bind("Active") %>' Text="Active" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                            CommandName="Insert" Text="Insert" OnClick="InsertButton_Click" />
                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                            CausesValidation="False" onclick="InsertCancelButton_Click" Text="Close" />
                    </div>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
        <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Add a physician/site</asp:LinkButton>

        <asp:EntityDataSource ID="edsPhysicianSites" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="PhysicianSites" OrderBy="[it].Site.SiteName, [it].Physician.LastName, [it].Physician.FirstName" OnInserted="edsPhysicianSites_Inserted" OnUpdated="edsPhysicianSites_Updated" Include="Site, Physician">
        </asp:EntityDataSource>
        <asp:QueryExtender ID="qeedsPhysicianSites" runat="server" TargetControlID="edsPhysicianSites">
            <asp:CustomExpression OnQuerying="FiltergvPhysicians"></asp:CustomExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="Physician.LastName">
                <asp:ControlParameter ControlID="tbPhysicianLastName" />
            </asp:SearchExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="Physician.FirstName">
                <asp:ControlParameter ControlID="tbPhysicianFirstName" />
            </asp:SearchExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="Physician.LicenseNumber">
                <asp:ControlParameter ControlID="tbPhysicianLicenseNumber" />
            </asp:SearchExpression>
        </asp:QueryExtender>

        <asp:EntityDataSource ID="edsSites" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="Sites" OrderBy="[it].SiteName">
        </asp:EntityDataSource>
        <asp:EntityDataSource ID="edsPhysicians" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="vwPhysicians" OrderBy="[it].DisplayName">
        </asp:EntityDataSource>

    </div>

</asp:Content>
