<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="MRI.Views.Entry.Create" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phMessage" runat="server"></asp:PlaceHolder>
    <asp:Panel ID="pnlSiteList" runat="server" Visible="false">
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
                        <td>Patient name contains:</td>
                        <td><asp:TextBox ID="tbFilterPatientName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Account number contains:</td>
                        <td><asp:TextBox ID="tbFilterAccountNumber" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:Button ID="btnApplyFilter" runat="server" Text="Apply date filter &raquo;" CssClass="btn btn-primary btn-sm" OnClick="btnApplyFilter_Click" />

                <br />

                <asp:GridView ID="gvOracleList" runat="server" DataSourceID="edsOracleList" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="OraclePatientExportId" PageSize="20" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvOracleList_SelectedIndexChanged" Width="100%">
                    <AlternatingRowStyle BackColor="LightBlue" ForeColor="#284775" Width="100%" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="DateOfScan" HeaderText="Date Of Scan" ReadOnly="True" SortExpression="DateOfScan" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                        <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True" SortExpression="PatientName" />
                        <asp:BoundField DataField="AccountNumber" HeaderText="Account Number" SortExpression="AccountNumber" />
                        <asp:BoundField DataField="MedicalRecordNumber" HeaderText="Medical Record Number" SortExpression="MedicalRecordNumber" Visible="False" />
                        <asp:BoundField DataField="DescriptionOfScan" HeaderText="Description Of Scan" SortExpression="DescriptionOfScan" />
                        <asp:BoundField DataField="Sex" HeaderText="Sex" SortExpression="Sex" Visible="False" />
                        <asp:BoundField DataField="ZipCode" HeaderText="Zip code" SortExpression="ZipCode" Visible="False" />
                        <asp:BoundField DataField="PatientStatus" HeaderText="Patient Status" SortExpression="PatientStatus" Visible="False" />
                        <asp:BoundField DataField="PatientDOB" HeaderText="Patient DOB" SortExpression="PatientDOB" Visible="False" />
                        <asp:BoundField DataField="AgeOfPatient" HeaderText="Age Of Patient" SortExpression="AgeOfPatient" Visible="False" />
                        <asp:BoundField DataField="AgeCode" HeaderText="Age Code" SortExpression="AgeCode" Visible="False" />
                        <asp:BoundField DataField="MainSiteCode" HeaderText="Main Site Code" SortExpression="MainSiteCode" Visible="False" />
                        <asp:BoundField DataField="County" HeaderText="County" SortExpression="County" Visible="False" />
                    </Columns>
                    <EmptyDataTemplate>
                        <h2>There is no data for the filter used above.</h2>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:EntityDataSource ID="edsOracleList" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="OraclePatientExports">
                </asp:EntityDataSource>
                <asp:QueryExtender ID="qeedsOracleList" runat="server" TargetControlID="edsOracleList">
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
                If the MRI you are looking for does not appear in the list above click here to <asp:Button ID="btnCreateMRI" runat="server" Text="create new MRI manually entering data &raquo;" CssClass="btn btn-primary btn-sm" OnClick="btnCreateMRI_Click"  />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
