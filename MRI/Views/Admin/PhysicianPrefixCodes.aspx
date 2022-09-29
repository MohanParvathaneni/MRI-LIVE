<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhysicianPrefixCodes.aspx.cs" Inherits="MRI.Views.Admin.PhysicianPrefixCodes" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage physician prefix codes</h3>

        <asp:LinkButton ID="lblPrefix" runat="server" onclick="lblPrefix_Click">Add a new physican prefix code</asp:LinkButton><br /><br />
        
        <asp:PlaceHolder ID="phNew" runat="server" Visible="false">
            <asp:PlaceHolder runat="server" ID="phError" Visible="false">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
            </asp:PlaceHolder>
            <div class="row">
                <div class="col-md-4">
                    States:<br />
                    <asp:DropDownList ID="ddlStates" runat="server" DataSourceID="edsStates" DataTextField="StateName" DataValueField="StateId" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select One" Selected="True" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="col-md-4">
                    Prefix code:<br />
                    <asp:TextBox ID="tbPrefixCode" runat="server" Text=""></asp:TextBox>
                </div>
                  <div class="col-md-4">
                   Prefix name:<br />
                    <asp:TextBox ID="tbPrefixName" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:CheckBox ID="cbActive" runat="server" Checked="true" Text="Active" />
                </div>
                <div class="col-md-4">
                    <br />
                    <asp:LinkButton ID="lblInsert" runat="server" Text="Insert" OnClick="lblInsert_Click" />
                    &nbsp;
                    <asp:LinkButton ID="lblClose" runat="server" onclick="lblClose_Click" Text="Close" />
                </div>
            </div>
        </asp:PlaceHolder>

        <%--<asp:EntityDataSource ID="edsSites" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Sites" OrderBy="[it].SiteName" OnInserted="edsSites_Inserted" OnUpdated="edsSites_Updated" Include="State">
        </asp:EntityDataSource>--%>
        <asp:EntityDataSource ID="edsStates" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="States">
        </asp:EntityDataSource>

        <asp:EntityDataSource ID="edsPhysicianPrefixCodes" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="PhysicianPrefixCodes" OrderBy="[it].State.StateName, [it].PrefixName" OnInserted="edsPhysicianPrefixCodes_Inserted" OnUpdated="edsPhysicianPrefixCodes_Updated" Include="State" EnableUpdate="True" EnableInsert="True">
        </asp:EntityDataSource>

        <asp:GridView ID="gvprefix" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PhysicianPrefixCodesId" DataSourceID="edsPhysicianPrefixCodes"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="30">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />

                <asp:BoundField DataField="StateId" HeaderText="State Id" ReadOnly="True" SortExpression="StateId" Visible="false" />
                <asp:TemplateField HeaderText="State" SortExpression="States.StateName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="edsStates" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Bind("StateId") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("State.StateName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Prefix codes">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PrefixCode") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#: Bind("PrefixCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="Prefix names">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%#: Bind("PrefixName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%#: Bind("PrefixName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="Active" HeaderText="Active"   SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:LinkButton ID="lblPrefix2" runat="server" onclick="lblPrefix_Click">Add a new physican prefix code</asp:LinkButton><br /><br />

    </div>

</asp:Content>
