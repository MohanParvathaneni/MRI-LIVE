<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSites.aspx.cs" Inherits="MRI.Views.Admin.ManageSites" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content">
        <h3>Manage Sites</h3>

        <asp:GridView ID="gvSites" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SiteId" DataSourceID="edsSites"
            ForeColor="#333333" GridLines="None" Width="100%" PageSize="30">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="SiteId" HeaderText="Site Id" ReadOnly="True" SortExpression="SiteId" Visible="false" />
                <asp:BoundField DataField="SiteCode" HeaderText="Site Code" SortExpression="SiteCode" />
                <asp:BoundField DataField="SiteName" HeaderText="Site Name" SortExpression="SiteName" />
                <asp:TemplateField HeaderText="State" SortExpression="State.StateName">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="edsStates" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Bind("StateId") %>'></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("State.StateName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ServiceIdNumber" HeaderText="Service Id Number" SortExpression="ServiceIdNumber" />
                <asp:TemplateField HeaderText="Email Information">
                    <EditItemTemplate>
                        From email address: <asp:TextBox ID="tbFromEmailAddress" runat="server" text='<%# Bind("FromEmailAddress") %>'></asp:TextBox><br />
                        To email address: <asp:TextBox ID="tbToEmailAddress" runat="server" text='<%# Bind("ToEmailAddress") %>'></asp:TextBox><br />
                        Email subject: <asp:TextBox ID="tbEmailSubject" runat="server" text='<%# Bind("EmailSubject") %>'></asp:TextBox><br />
                    </EditItemTemplate>
                    <ItemTemplate>
                        From email address: <asp:Label ID="Label2" runat="server" Text='<%# Eval("FromEmailAddress") %>'></asp:Label><br />
                        To email address: <asp:Label ID="Label1" runat="server" Text='<%# Eval("ToEmailAddress") %>'></asp:Label><br />
                        Email subject: <asp:Label ID="Label3" runat="server" Text='<%# Eval("EmailSubject") %>'></asp:Label><br />
                    </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:BoundField DataField="FromEmailAddress" HeaderText="From Email Address" SortExpression="FromEmailAddress" />
                <asp:BoundField DataField="ToEmailAddress" HeaderText="To Email Address" SortExpression="ToEmailAddress" />
                <asp:BoundField DataField="EmailSubject" HeaderText="Email Subject" SortExpression="EmailSubject" />--%>
                <asp:TemplateField HeaderText="Active Directory Information">
                    <EditItemTemplate>
                        AD group admin: <asp:TextBox ID="tbADGroupAdmin" runat="server" text='<%# Bind("ADGroupAdmin") %>'></asp:TextBox><br />
                        AD group user entry: <asp:TextBox ID="tbADGroupUserEntry" runat="server" text='<%# Bind("ADGroupUserEntry") %>'></asp:TextBox><br />
                        AD group modify entry: <asp:TextBox ID="tbADGroupModifyEntry" runat="server" text='<%# Bind("ADGroupModifyEntry") %>'></asp:TextBox><br />
                        AD group submission report: <asp:TextBox ID="tbADGroupSubmissionReport" runat="server" text='<%# Bind("ADGroupSubmissionReport") %>'></asp:TextBox><br />
                        AD group report: <asp:TextBox ID="tbADGroupReport" runat="server" text='<%# Bind("ADGroupReport") %>'></asp:TextBox><br />
                    </EditItemTemplate>
                    <ItemTemplate>
                        AD group admin: <asp:Label ID="Label2" runat="server" Text='<%# Eval("ADGroupAdmin") %>'></asp:Label><br />
                        AD group user entry: <asp:Label ID="Label1" runat="server" Text='<%# Eval("ADGroupUserEntry") %>'></asp:Label><br />
                        AD group modify entry: <asp:Label ID="Label3" runat="server" Text='<%# Eval("ADGroupModifyEntry") %>'></asp:Label><br />
                        AD group submission report: <asp:Label ID="Label4" runat="server" Text='<%# Eval("ADGroupSubmissionReport") %>'></asp:Label><br />
                        AD group report: <asp:Label ID="Label5" runat="server" Text='<%# Eval("ADGroupReport") %>'></asp:Label><br />
                    </ItemTemplate>

                </asp:TemplateField>
<%--                <asp:BoundField DataField="ADGroupAdmin" HeaderText="AD Group Admin" SortExpression="ADGroupAdmin" />
                <asp:BoundField DataField="ADGroupUserEntry" HeaderText="AD Group User Entry" SortExpression="ADGroupUserEntry" />
                <asp:BoundField DataField="ADGroupModifyEntry" HeaderText="AD Group Modify Entry" SortExpression="ADGroupModifyEntry" />
                <asp:BoundField DataField="ADGroupSubmissionReport" HeaderText="AD Group Submission Report" SortExpression="ADGroupSubmissionReport" />
                <asp:BoundField DataField="ADGroupReport" HeaderText="AD Group Report" SortExpression="ADGroupReport" />--%>
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            </Columns>
        </asp:GridView>
        <asp:FormView ID="fvSites" runat="server"
            DataKeyNames="SitesId" DataSourceID="edsSites"
            DefaultMode="Insert" Visible="False">
            <InsertItemTemplate>
                <table cellpadding="5" cellspacing="5" style="width: 100%;">
                    <tr>
                        <td>
                            Site Code:
                            <asp:TextBox ID="tbSiteCode" runat="server" Text='<%# Bind("SiteCode") %>'></asp:TextBox>
                        </td>
                        <td>
                            Site Name:
                            <asp:TextBox ID="tbSiteName" runat="server" Text='<%# Bind("SiteName") %>'></asp:TextBox>
                        </td>
                        <td>
                            State:
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="edsStates" DataTextField="StateName" DataValueField="StateId" SelectedValue='<%# Bind("StateId") %>'></asp:DropDownList>
                        </td>
                        <td>
                            Service Id Number:
                            <asp:TextBox ID="tbServiceIdNumber" runat="server" Text='<%# Bind("ServiceIdNumber") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Email Address:
                            <asp:TextBox ID="tbFromEmailAddress" runat="server" Text='<%# Bind("FromEmailAddress") %>'></asp:TextBox>
                        </td>
                        <td>
                            To Email Address:
                            <asp:TextBox ID="tbToEmailAddress" runat="server" Text='<%# Bind("ToEmailAddress") %>'></asp:TextBox>
                        </td>
                        <td>
                            Email Subject:
                            <asp:TextBox ID="tbEmailSubject" runat="server" Text='<%# Bind("EmailSubject") %>'></asp:TextBox>
                        </td>
                        <td>
                            Active Directory Group Admin:
                            <asp:TextBox ID="tbADGroupAdmin" runat="server" Text='<%# Bind("ADGroupAdmin") %>'></asp:TextBox>
                        </td>
                        <td>
                            Active Directory Group User Entry:
                            <asp:TextBox ID="tbADGroupUserEntry" runat="server" Text='<%# Bind("ADGroupUserEntry") %>'></asp:TextBox>
                        </td>
                        <td>
                            Active Directory Group Modify Entry:
                            <asp:TextBox ID="tbADGroupModifyEntry" runat="server" Text='<%# Bind("ADGroupModifyEntry") %>'></asp:TextBox>
                        </td>
                        <td>
                            Active Directory Group Submission Report:
                            <asp:TextBox ID="tbADGroupSubmissionReport" runat="server" Text='<%# Bind("ADGroupSubmissionReport") %>'></asp:TextBox>
                        </td>
                        <td>
                            Active Directory Group Report:
                            <asp:TextBox ID="tbADGroupReport" runat="server" Text='<%# Bind("ADGroupReport") %>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbActive" runat="server" Checked='<%# Bind("Active") %>' Text="Active" />
                        </td>
                        <td>
                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Insert" OnClick="InsertCancelButton_Click" />
                            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                CausesValidation="False" onclick="InsertCancelButton_Click" Text="Close" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>
        <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Add a new site</asp:LinkButton>
        <asp:EntityDataSource ID="edsSites" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="Sites" OrderBy="[it].SiteName" OnInserted="edsSites_Inserted" OnUpdated="edsSites_Updated" Include="State">
        </asp:EntityDataSource>
        <asp:EntityDataSource ID="edsStates" runat="server" ConnectionString="name=MHCC_MRIEntities1" DefaultContainerName="MHCC_MRIEntities1" EnableFlattening="False" EntitySetName="States">
        </asp:EntityDataSource>
    </div>
</asp:Content>

