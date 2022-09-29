<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateData.aspx.cs" Inherits="MRI.Views.Entry.CreateData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phMessage" runat="server"></asp:PlaceHolder>
    <asp:Panel id="pnlCreate" runat="server" Visible="false">
        <div class="body-content">
            <div class="row">
                <div class="col-md-11">
                    <h2>MRI Create Information</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <strong>Patient name:</strong><br />
                    <asp:TextBox ID="tbPatientName" runat="server" MaxLength="250"></asp:TextBox>
                    <asp:Label ID="lbltbPatientName" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-5">
                    <strong>Account number:</strong><br />
                    <asp:TextBox ID="tbAccountNumber" runat="server" MaxLength="250"></asp:TextBox>
                    <asp:Label ID="lbltbAccountNumber" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-6">
                    <strong>Medical record number:</strong><br />
                    <asp:TextBox ID="tbMedicalRecordNumber" runat="server" MaxLength="250"></asp:TextBox>
                    <asp:Label ID="lbltbMedicalRecordNumber" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-5">
                    <strong>Scan description:</strong><br />
                    <asp:TextBox ID="tbScanDescription" runat="server" MaxLength="250"></asp:TextBox>
                    <asp:Label ID="lbltbScanDescription" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Date of scan:</strong><br />
                    <asp:TextBox ID="tbDateOfScan" runat="server" MaxLength="10" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="lbltbDateOfScan" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Age of patient:</strong><br />
                    <asp:TextBox ID="tbAgeOfPatient" runat="server" MaxLength="2"></asp:TextBox>
                    <asp:Label ID="lbltbAgeOfPatient" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Age code:</strong><br />
                    <asp:DropDownList ID="ddlAgeCode" runat="server" AppendDataBoundItems="True" DataSourceID="edsAgeCode" DataTextField="AgeCodeDescripition" DataValueField="AgeCodeId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:EntityDataSource ID="edsAgeCode" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="AgeCodes" OrderBy="[it].AgeCodeDescripition">
                    </asp:EntityDataSource>
                    <asp:Label ID="lblddlAgeCode" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <span style="color:red;">*</span>&nbsp;<strong>Sex:</strong><br />
                    <asp:DropDownList ID="ddlSex" runat="server" AppendDataBoundItems="True" DataSourceID="edsSex" DataTextField="SexDescription" DataValueField="SexId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:EntityDataSource ID="edsSex" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="Sexes" OrderBy="[it].SexDescription">
                    </asp:EntityDataSource>
                    <asp:Label ID="lblddlSex" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Zipcode of residence:</strong><br />
                    <asp:TextBox ID="tbZipcodeOfResidence" runat="server" MaxLength="5"></asp:TextBox>
                    <asp:Label ID="lbltbZipcodeOfResidence" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>County of residence:</strong><br />
                    <asp:DropDownList ID="ddlCountyOfResidence" runat="server" AppendDataBoundItems="True" DataSourceID="edsCountyOfResidence" DataTextField="CountyName" DataValueField="CountyId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:EntityDataSource ID="edsCountyOfResidence" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="Counties" OrderBy="[it].CountyName">
                    </asp:EntityDataSource>
                    <asp:Label ID="lblddlCountyOfResidence" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Expected source of payment:</strong><br />
                    <asp:DropDownList ID="ddlExpectedSourceOfPayment" runat="server" AppendDataBoundItems="True" DataSourceID="edsSourceOfPayment" DataTextField="SourceName" DataValueField="SourceOfPaymentId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:EntityDataSource ID="edsSourceOfPayment" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="SourceOfPayments" OrderBy="[it].SourceName">
                    </asp:EntityDataSource>
                    <asp:Label ID="lblddlExpectedSourceOfPayment" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Referring physician:</strong><br />
                    <asp:DropDownList ID="ddlReferringPhysician" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlReferringPhysician" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Patient status:</strong><br />
                    <asp:DropDownList ID="ddlPatientStatus" runat="server" AppendDataBoundItems="True" DataSourceID="edsPatientStatus" DataTextField="PatientStatusDescription" DataValueField="PatientStatusId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:EntityDataSource ID="edsPatientStatus" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="PatientStatus" OrderBy="[it].PatientStatusDescription">
                    </asp:EntityDataSource>
                    <asp:Label ID="lblddlPatientStatus" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <span style="color:red;">*</span>&nbsp;<strong>Clinical/research status:</strong><br />
                    <asp:DropDownList ID="ddlClinicalStatus" runat="server" AppendDataBoundItems="True" DataSourceID="edsClinicalStatus" DataTextField="ClinicalStatusDescription" DataValueField="ClinicalStatusId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:EntityDataSource ID="edsClinicalStatus" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="ClinicalStatus" OrderBy="[it].ClinicalStatusDescription">
                    </asp:EntityDataSource>
                    <asp:Label ID="lblddlClinicalStatus" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <strong>1st Scan</strong>
            <div class="row">
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Region:</strong><br />
                    <asp:DropDownList ID="ddlRegion1" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanRegion" DataTextField="ScanRegionDescription" DataValueField="ScanRegionId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlRegion1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Sedation:</strong><br />
                    <asp:DropDownList ID="ddlSedation1" runat="server" AppendDataBoundItems="True" DataSourceID="edsSedation" DataTextField="SedationDescription" DataValueField="SedationId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlSedation1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <span style="color:red;">*</span>&nbsp;<strong>Scan contrast:</strong><br />
                    <asp:DropDownList ID="ddlScanContrast1" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanContrast" DataTextField="ScanContrastDescription" DataValueField="ScanContrastId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlScanContrast1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <span style="color:red;">*</span>&nbsp;<strong>Complete:</strong><br />
                    <asp:DropDownList ID="ddlComplete1" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanComplete" DataTextField="ScanCompleteDescription" DataValueField="ScanCompleteId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlComplete1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <strong>2nd Scan</strong>
            <div class="row">
                <div class="col-md-3">
                    <strong>Region:</strong><br />
                    <asp:DropDownList ID="ddlRegion2" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanRegion" DataTextField="ScanRegionDescription" DataValueField="ScanRegionId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlRegion2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Sedation:</strong><br />
                    <asp:DropDownList ID="ddlSedation2" runat="server" AppendDataBoundItems="True" DataSourceID="edsSedation" DataTextField="SedationDescription" DataValueField="SedationId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlSedation2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Scan contrast:</strong><br />
                    <asp:DropDownList ID="ddlScanContrast2" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanContrast" DataTextField="ScanContrastDescription" DataValueField="ScanContrastId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlScanContrast2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <strong>Complete:</strong><br />
                    <asp:DropDownList ID="ddlComplete2" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanComplete" DataTextField="ScanCompleteDescription" DataValueField="ScanCompleteId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlComplete2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>

            </div>
            <hr />
            <strong>3rd Scan</strong>
            <div class="row">
                <div class="col-md-3">
                    <strong>Region:</strong><br />
                    <asp:DropDownList ID="ddlRegion3" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanRegion" DataTextField="ScanRegionDescription" DataValueField="ScanRegionId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlRegion3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Sedation:</strong><br />
                    <asp:DropDownList ID="ddlSedation3" runat="server" AppendDataBoundItems="True" DataSourceID="edsSedation" DataTextField="SedationDescription" DataValueField="SedationId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlSedation3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Scan contrast:</strong><br />
                    <asp:DropDownList ID="ddlScanContrast3" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanContrast" DataTextField="ScanContrastDescription" DataValueField="ScanContrastId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlScanContrast3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <strong>Complete:</strong><br />
                    <asp:DropDownList ID="ddlComplete3" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanComplete" DataTextField="ScanCompleteDescription" DataValueField="ScanCompleteId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlComplete3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <strong>4th Scan</strong>
            <div class="row">
                <div class="col-md-3">
                    <strong>Region:</strong><br />
                    <asp:DropDownList ID="ddlRegion4" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanRegion" DataTextField="ScanRegionDescription" DataValueField="ScanRegionId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlRegion4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Sedation:</strong><br />
                    <asp:DropDownList ID="ddlSedation4" runat="server" AppendDataBoundItems="True" DataSourceID="edsSedation" DataTextField="SedationDescription" DataValueField="SedationId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlSedation4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Scan contrast:</strong><br />
                    <asp:DropDownList ID="ddlScanContrast4" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanContrast" DataTextField="ScanContrastDescription" DataValueField="ScanContrastId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlScanContrast4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <strong>Complete:</strong><br />
                    <asp:DropDownList ID="ddlComplete4" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanComplete" DataTextField="ScanCompleteDescription" DataValueField="ScanCompleteId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlComplete4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <strong>5th Scan</strong>
            <div class="row">
                <div class="col-md-3">
                    <strong>Region:</strong><br />
                    <asp:DropDownList ID="ddlRegion5" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanRegion" DataTextField="ScanRegionDescription" DataValueField="ScanRegionId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlRegion5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Sedation:</strong><br />
                    <asp:DropDownList ID="ddlSedation5" runat="server" AppendDataBoundItems="True" DataSourceID="edsSedation" DataTextField="SedationDescription" DataValueField="SedationId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlSedation5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <strong>Scan contrast:</strong><br />
                    <asp:DropDownList ID="ddlScanContrast5" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanContrast" DataTextField="ScanContrastDescription" DataValueField="ScanContrastId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlScanContrast5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-2">
                    <strong>Complete:</strong><br />
                    <asp:DropDownList ID="ddlComplete5" runat="server" AppendDataBoundItems="True" DataSourceID="edsScanComplete" DataTextField="ScanCompleteDescription" DataValueField="ScanCompleteId">
                        <asp:ListItem Selected="True" Text="Not Assigned"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlComplete5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
        <asp:EntityDataSource ID="edsScanRegion" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="ScanRegions" OrderBy="[it].ScanRegionDescription">
        </asp:EntityDataSource>
        <asp:EntityDataSource ID="edsSedation" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="Sedations" OrderBy="[it].SedationDescription">
        </asp:EntityDataSource>
        <asp:EntityDataSource ID="edsScanContrast" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="ScanContrasts" OrderBy="[it].ScanContrastDescription">
        </asp:EntityDataSource>
        <asp:EntityDataSource ID="edsScanComplete" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="ScanCompletes" OrderBy="[it].ScanCompleteDescription">
        </asp:EntityDataSource>
    </asp:Panel>
</asp:Content>
