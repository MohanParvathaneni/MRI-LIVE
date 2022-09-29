<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePhysicians.aspx.cs" Inherits="MRI.Views.Admin.ManagePhysicians" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage Physicians</h3>

        Filter by license number contains: <asp:TextBox ID="tbLicenseNumber" runat="server"></asp:TextBox>
        <br />
        Last name contains: <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
        <br />
        First name contains: <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
        &nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Apply Filters" OnClick="btnRefresh_Click" />
        <br />
        <asp:GridView ID="gvPhysicians" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PhysicianId" DataSourceID="edsPhysicians"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="30">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="PhysicianId" HeaderText="Physician Id" ReadOnly="True" SortExpression="PhysicianId" Visible="false" />
                <asp:BoundField DataField="LicenseNumber" HeaderText="License Number" SortExpression="LicenseNumber" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" SortExpression="MiddleName" />
                <asp:BoundField DataField="Suffix" HeaderText="Suffix" SortExpression="Suffix" />
                <asp:BoundField DataField="AddressNumber" HeaderText="Address Number" SortExpression="AddressNumber" />
                <asp:BoundField DataField="AddressNumberSuffix" HeaderText="Address Number Suffix" SortExpression="AddressNumberSuffix" />
                <asp:BoundField DataField="AddressLine1" HeaderText="Address Line 1" SortExpression="AddressLine1" />
                <asp:BoundField DataField="AddressSuffixType" HeaderText="Address Suffix Type" SortExpression="AddressSuffixType" />
                <asp:BoundField DataField="AddressLine2" HeaderText="Address Line 2" SortExpression="AddressLine2" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:TemplateField HeaderText="State" SortExpression="State.StateName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="edsStates" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Bind("StateId") %>'></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("State.StateName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ZipCode" HeaderText="Zipcode" SortExpression="ZipCode" />
                <asp:BoundField DataField="ZipCodeExtension" HeaderText="Zipcode Extension" SortExpression="ZipCodeExtension" />
                <asp:TemplateField HeaderText="Licensure type" SortExpression="PhysicianLicensure.PhysicianLicensureDescription">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="edsPhysicianLicensures" DataTextField="PhysicianLicensureDescription" DataValueField="PhysicianLicensureId" SelectedValue='<%# Bind("PhysicianLicensureId") %>'></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PhysicianLicensure.PhysicianLicensureDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specialty" SortExpression="PhysicianSpecialty.PhysicianSpecialtyCode, PhysicianSpecialty.Category1, PhysicianSpecialty.Category2">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="edsPhysicianSpecialties" DataTextField="DisplayName" DataValueField="PhysicianSpecialtyId" SelectedValue='<%# Bind("PhysicianSpecialtyId") %>'></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PhysicianSpecialty.PhysicianSpecialtyCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <asp:FormView ID="fvPhysicians" runat="server"
            DataKeyNames="PhysicianId" DataSourceID="edsPhysicians"
            DefaultMode="Insert" Visible="False">
            <InsertItemTemplate>
                <div class="row">
                    <div class="col-md-11">
                        <span style="color:red;">*</span>&nbsp;License Number:<br />
                        <asp:TextBox ID="tbLicenseNumber" runat="server" Text='<%# Bind("LicenseNumber") %>' MaxLength="20"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <span style="color:red;">*</span>&nbsp;Last Name:<br />
                        <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("LastName") %>' MaxLength="20"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        <span style="color:red;">*</span>&nbsp;First Name:<br />
                        <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Bind("FirstName") %>' MaxLength="20"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        Middle Name:<br />
                        <asp:TextBox ID="tbMiddleName" runat="server" Text='<%# Bind("MiddleName") %>' MaxLength="20"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        Suffix:<br />
                        <asp:TextBox ID="tbSuffix" runat="server" Text='<%# Bind("Suffix") %>' MaxLength="3"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <span style="color:red;">*</span>&nbsp;Address Number:&nbsp;<button type="button" class="btn btn-info btn-xs" data-toggle="modal" data-target="#definitionModal">?</button><br />
                        <asp:TextBox ID="tbAddressNumber" runat="server" Text='<%# Bind("AddressNumber") %>' MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        Address Number Suffix:&nbsp;<button type="button" class="btn btn-info btn-xs" data-toggle="modal" data-target="#definitionModal">?</button><br />
                        <asp:TextBox ID="tbAddressNumberSuffix" runat="server" Text='<%# Bind("AddressNumberSuffix") %>' MaxLength="6"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <span style="color:red;">*</span>&nbsp;Address Line 1:&nbsp;<button type="button" class="btn btn-info btn-xs" data-toggle="modal" data-target="#definitionModal">?</button><br />
                        <asp:TextBox ID="tbAddressLine1" runat="server" Text='<%# Bind("AddressLine1") %>' MaxLength="28"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        Address Suffix Type:&nbsp;<button type="button" class="btn btn-info btn-xs" data-toggle="modal" data-target="#definitionModal">?</button><br />
                        <asp:TextBox ID="tbAddressSuffixType" runat="server" Text='<%# Bind("AddressSuffixType") %>' MaxLength="6"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        Address Line 2:<br />
                        <asp:TextBox ID="tbAddressLine2" runat="server" Text='<%# Bind("AddressLine2") %>' MaxLength="25"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <span style="color:red;">*</span>&nbsp;City:<br />
                        <asp:TextBox ID="tbCity" runat="server" Text='<%# Bind("City") %>' MaxLength="20"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        <span style="color:red;">*</span>&nbsp;State:<br />
                        <asp:DropDownList ID="ddlState" runat="server" DataSourceID="edsStates" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Bind("StateId") %>'></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <span style="color:red;">*</span>&nbsp;Zipcode:<br />
                        <asp:TextBox ID="tbZipCode" runat="server" Text='<%# Bind("ZipCode") %>'></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        Zipcode Extension:<br />
                        <asp:TextBox ID="tbZipCodeExtension" runat="server" Text='<%# Bind("ZipCodeExtension") %>'></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <span style="color:red;">*</span>&nbsp;Physician Licensure:<br />
                        <asp:DropDownList ID="ddlPhysicianLicensure" runat="server" DataSourceID="edsPhysicianLicensures" DataTextField="PhysicianLicensureDescription" DataValueField="PhysicianLicensureId" SelectedValue='<%# Bind("PhysicianLicensureId") %>'></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-11">
                        <span style="color:red;">*</span>&nbsp;Physician Specialty:<br />
                        <asp:DropDownList ID="ddlPhysicianSpecialty" runat="server" DataSourceID="edsPhysicianSpecialties" DataTextField="DisplayName" DataValueField="PhysicianSpecialtyId" SelectedValue='<%# Bind("PhysicianSpecialtyId") %>'></asp:DropDownList>
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
                            CommandName="Insert" Text="Insert" OnClick="InsertButton_Click"/>
                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                            CausesValidation="False" onclick="InsertCancelButton_Click" Text="Close" />
                    </div>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
        <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Add a physician</asp:LinkButton>
        <asp:EntityDataSource ID="edsPhysicians" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Physicians" OrderBy="[it].LastName, [it].FirstName, [it].LicenseNumber" OnInserted="edsPhysicians_Inserted" OnUpdated="edsPhysicians_Updated" Include="State, PhysicianSpecialty, PhysicianLicensure">
        </asp:EntityDataSource>
        <asp:QueryExtender ID="qeedsPhysicians" runat="server" TargetControlID="edsPhysicians">
            <asp:CustomExpression OnQuerying="FiltergvPhysicians"></asp:CustomExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="LicenseNumber">
                <asp:ControlParameter ControlID="tbLicenseNumber" />
            </asp:SearchExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="LastName">
                <asp:ControlParameter ControlID="tbLastName" />
            </asp:SearchExpression>
            <asp:SearchExpression SearchType="Contains" DataFields="FirstName">
                <asp:ControlParameter ControlID="tbFirstName" />
            </asp:SearchExpression>
        </asp:QueryExtender>

        <asp:EntityDataSource ID="edsStates" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="States" OrderBy="[it].StateName">
        </asp:EntityDataSource>
        <asp:EntityDataSource ID="edsPhysicianLicensures" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="PhysicianLicensures" OrderBy="[it].PhysicianLicensureCode, [it].PhysicianLicensureDescription">
        </asp:EntityDataSource>
<%--        <asp:EntityDataSource ID="edsPhysicianSpecialties" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="PhysicianSpecialties" OrderBy="[it].Category1, [it].Category2, [it].Category3"
            Select="[it].PhysicianSpecialtyId, [it].Category1 + ' - ' + [it].Category2 + ' - ' + [it].Category3 AS DisplayName">
        </asp:EntityDataSource>--%>
        <asp:EntityDataSource ID="edsPhysicianSpecialties" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="vwPhysicianSpecialtyLists" OrderBy="[it].DisplayName">
        </asp:EntityDataSource>
        <br /><br />
        <asp:HyperLink ID="hlPhysicianLookup" runat="server" Target="_blank">LARA physician lookup</asp:HyperLink>
    </div>

    <!-- Definition Modal -->
    <div class="modal fade" id="definitionModal" tabindex="-1" role="dialog" aria-labelledby="definitionModalLabel">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" id="definitionModalLabel">Address Definitions</h4>
          </div>
          <div class="modal-body">
              <h3>Example address: <strong>123-A North Park Ave</strong></h3>
              <strong>Address Number</strong> equals <strong>123</strong>
              <br /><br />
              <strong>Address Number Suffix</strong> equals <strong>A</strong>
              <br /><br />
              <strong>Address Line 1</strong> equals <strong>North Park</strong>
              <br /><br />
              <strong>Address Suffix Type</strong> equals <strong>Ave</strong>
              <br /><br />
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
            <%--<button type="button" class="btn btn-primary">Save changes</button>--%>

          </div>
        </div>
      </div>
    </div>

</asp:Content>
