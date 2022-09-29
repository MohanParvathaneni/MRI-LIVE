<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="MRI.Views.Admin.Report" MaintainScrollPositionOnPostback="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phMessage" runat="server"></asp:PlaceHolder>
    <asp:Panel ID="pnlSubmission" runat="server" Visible="false">
        <asp:Literal ID="literal1" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-md-11">
                <asp:Literal ID="literalTitle" runat="server"></asp:Literal><br />
                <table>
                    <tr>
                        <td>Date from:</td>
                        <td>
                            <asp:TextBox ID="tbFrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Id="cetbFrom" runat="server" TargetControlID="tbFrom"></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Date to: (This will get everything up to the start of this day.)</td>
                        <td>
                            <asp:TextBox ID="tbTo" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender Id="cetbTo" runat="server" TargetControlID="tbTo"></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Referring Physician:</td>
                        <td>
                            <asp:DropDownList ID="ddlReferringPhysician" runat="server" AppendDataBoundItems="True">
                                <asp:ListItem Text="All"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Patient name contains:</td>
                        <td><asp:TextBox ID="tbFilterPatientName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Account number contains:</td>
                        <td><asp:TextBox ID="tbFilterAccountNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="cblColumns" runat="server" AutoPostBack="false">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnApplyFilter" runat="server" Text="Apply date filter &raquo;" CssClass="btn btn-primary btn-sm" OnClick="btnApplyFilter_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel &raquo;" CssClass="btn btn-primary btn-sm" OnClick="btnExport_Click" />
                <br /><br />
                <asp:GridView ID="gvScanList" runat="server" DataSourceID="edsScanList" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="ScanId" PageSize="20" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvScanList_SelectedIndexChanged" Width="100%">
                    <AlternatingRowStyle BackColor="LightBlue" ForeColor="#284775" Width="100%" />
                    <Columns>
                        <asp:BoundField DataField="DateOfScan" HeaderText="Date Of Scan" ReadOnly="True" SortExpression="DateOfScan" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                        <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True" SortExpression="PatientName" />
                        <asp:BoundField DataField="AccountNumber" HeaderText="Account Number" SortExpression="AccountNumber" />
                        <asp:BoundField DataField="MedicalRecordNumber" HeaderText="Medical Record Number" SortExpression="MedicalRecordNumber" Visible="False" />
                        <asp:BoundField DataField="ScanDescription" HeaderText="Description Of Scan" SortExpression="ScanDescription" />
                        <asp:BoundField DataField="Sex.SexDescription" HeaderText="Sex" SortExpression="Sex.SexDescription" />
                        <asp:BoundField DataField="ZipCode" HeaderText="Zip Code" SortExpression="ZipCode" Visible="false"/>
                        <asp:BoundField DataField="PatientStatu.PatientStatusDescription" HeaderText="Patient Status" SortExpression="PatientStatu.PatientStatusDescription" Visible="False" />
                        <asp:BoundField DataField="AgeOfPatient" HeaderText="Age Of Patient" SortExpression="AgeOfPatient"/>
                        <asp:BoundField DataField="AgeCode.AgeCodeDescripition" HeaderText="Age Code" SortExpression="AgeCode.AgeCodeDescripition"/>
                        <asp:BoundField DataField="Site.SiteName" HeaderText="Site Name" SortExpression="Site.SiteName" Visible="False" />
                        <asp:BoundField DataField="County.CountyName" HeaderText="County" SortExpression="County.CountyName" Visible="False" />
                        <asp:BoundField DataField="ClinicalStatu.ClinicalStatusDescription" HeaderText="Clinical Status" SortExpression="ClinicalStatu.ClinicalStatusDescription"/>
                        <asp:TemplateField HeaderText="Physician" SortExpression="PhysicianSite.Physician.LastName">
                            <ItemTemplate>
                                <asp:Label ID="lblPhysician" runat="server" Text='<%# Eval("PhysicianSite.Physician.LastName") + ", " + Eval("PhysicianSite.Physician.FirstName") + " " + Eval("PhysicianSite.Physician.MiddleName") + " (" + Eval("PhysicianSite.Physician.LicenseNumber") + ")" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SourceOfPayment.SourceName" HeaderText="Source Of Payment" SortExpression="SourceOfPayment.SourceName"/>
                    </Columns>
                    <EmptyDataTemplate>
                        <h2>There is no data for the filter used above.</h2>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:EntityDataSource ID="edsScanList" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="Scans" Include="Sex, County, PatientStatu, AgeCode, Site, ClinicalStatu, PhysicianSite.Physician, SourceOfPayment" Where="[it].ScanUpdated=false">
                </asp:EntityDataSource>
                <asp:QueryExtender ID="qeedsScanList" runat="server" TargetControlID="edsScanList">
                    <asp:CustomExpression OnQuerying="FilterReport"></asp:CustomExpression>
                    <asp:RangeExpression DataField="DateOfScan" MinType="Inclusive" MaxType="Inclusive">
                        <asp:ControlParameter ControlID="tbFrom" />
                        <asp:ControlParameter ControlID="tbTo" />
                    </asp:RangeExpression>
                    <asp:SearchExpression SearchType="Contains" DataFields="PatientName">
                        <asp:ControlParameter ControlID="tbFilterPatientName" />
                    </asp:SearchExpression>
                    <asp:SearchExpression SearchType="Contains" DataFields="AccountNumber">
                        <asp:ControlParameter ControlID="tbFilterAccountNumber" />
                    </asp:SearchExpression>
                </asp:QueryExtender>
                <br />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
