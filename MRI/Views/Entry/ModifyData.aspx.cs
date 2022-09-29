using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRI.Views.Entry
{
    public partial class ModifyData : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        private Int32 _siteId;

        private Int32 _scanId;

        protected void phMessageText(string messageToDisplay)
        {
            phMessage.Controls.Add(new LiteralControl(string.Format(@"<div class=""col-md-11""><p>{0}</p></div>", messageToDisplay)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isInt = (Int32.TryParse(Request.QueryString["SiteId"], out _siteId));
                if (isInt)
                {
                    //See if valid SiteId
                    var qrySites = from x in _db.Sites where ((x.ADGroupAdmin != null) && (x.ADGroupModifyEntry != null) && (x.Active) && (x.SiteId == _siteId)) select x;
                    if (qrySites.Any())
                    {
                        foreach (var item in qrySites)
                        {
                            if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(item.ADGroupAdmin) || HttpContext.Current.User.IsInRole(item.ADGroupModifyEntry))
                            {
                                getPhysicians();
                                pnlModify.Visible = true;
                            }
                            else
                            {
                                pnlModify.Visible = false;
                                phMessageText(@"<h2>You do not have access to the Site id parameter that was passed.</h2>");
                            }
                        }
                    }
                    else
                    {
                        pnlModify.Visible = false;
                        phMessageText(@"<h2>Site id parameter was not found or not active or no active directory groups defined.</h2>");
                    }
                }
                else
                {
                    pnlModify.Visible = false;
                    //Display message not valid Site Id
                    phMessageText(@"<h2>Site id parameter is not valid.</h2>");
                }

                isInt = (Int32.TryParse(Request.QueryString["ScanId"], out _scanId));
                if (isInt)
                {
                    fillData();
                }
                else
                {
                    pnlModify.Visible = false;
                    phMessageText(@"<h2>Scan id parameter was not found.</h2>");
                }
            }
        }

        public void fillData()
        {
            var qry = (from x in _db.Scans where x.ScanId == _scanId select x).First();
            tbPatientName.Text = qry.PatientName;
            tbAccountNumber.Text = qry.AccountNumber;
            tbMedicalRecordNumber.Text = qry.MedicalRecordNumber;
            tbScanDescription.Text = qry.ScanDescription;
            //tbDateOfScan.Text = qry.DateOfScan.ToShortDateString();
            //Chrome needs the date in a different format - Tested with IE and this format works as well for it.
            tbDateOfScan.Text = qry.DateOfScan.ToString("yyyy-MM-dd");
            tbAgeOfPatient.Text = qry.AgeOfPatient.ToString();
            ddlAgeCode.SelectedValue = qry.AgeCodeId.ToString();
            ddlSex.SelectedValue = qry.SexId.ToString();
            tbZipcodeOfResidence.Text = qry.ZipCode;
            ddlCountyOfResidence.SelectedValue = qry.CountyId.ToString();
            ddlExpectedSourceOfPayment.SelectedValue = qry.SourceOfPaymentId.ToString();
            ddlReferringPhysician.SelectedValue = qry.PhysicianSiteId.ToString();
            ddlPatientStatus.SelectedValue = qry.PatientStatusId.ToString();
            ddlClinicalStatus.SelectedValue = qry.ClinicalStatusId.ToString();

            //Set scan information up
            var qryScan = from x in _db.ScanResults where x.ScanId == _scanId select x;
            if (qryScan.Any())
            {
                int x = 1;
                foreach (var item in qryScan)
                {
                    switch (x)
                    {
                        case 1:
                            ddlRegion1.SelectedValue = item.ScanRegionId.ToString();
                            ddlSedation1.SelectedValue = item.SedationId.ToString();
                            ddlScanContrast1.SelectedValue = item.ScanContrastId.ToString();
                            ddlComplete1.SelectedValue = item.ScanCompleteId.ToString();
                            break;
                        case 2:
                            ddlRegion2.SelectedValue = item.ScanRegionId.ToString();
                            ddlSedation2.SelectedValue = item.SedationId.ToString();
                            ddlScanContrast2.SelectedValue = item.ScanContrastId.ToString();
                            ddlComplete2.SelectedValue = item.ScanCompleteId.ToString();
                            break;
                        case 3:
                            ddlRegion3.SelectedValue = item.ScanRegionId.ToString();
                            ddlSedation3.SelectedValue = item.SedationId.ToString();
                            ddlScanContrast3.SelectedValue = item.ScanContrastId.ToString();
                            ddlComplete3.SelectedValue = item.ScanCompleteId.ToString();
                            break;
                        case 4:
                            ddlRegion4.SelectedValue = item.ScanRegionId.ToString();
                            ddlSedation4.SelectedValue = item.SedationId.ToString();
                            ddlScanContrast4.SelectedValue = item.ScanContrastId.ToString();
                            ddlComplete4.SelectedValue = item.ScanCompleteId.ToString();
                            break;
                        case 5:
                            ddlRegion5.SelectedValue = item.ScanRegionId.ToString();
                            ddlSedation5.SelectedValue = item.SedationId.ToString();
                            ddlScanContrast5.SelectedValue = item.ScanContrastId.ToString();
                            ddlComplete5.SelectedValue = item.ScanCompleteId.ToString();
                            break;
                        default:
                            break;
                    }
                    x++;
                }
            }
            pnlModify.Visible = true;
        }

        public void getPhysicians()
        {
            List<ListItem> physicianList = new List<ListItem>();
            //Active in the site and active by the physician also.  Not doing this on the modify since they might need to choose one that is no longer active or was active when entered.
            Int32 siteId;
            siteId = Int32.Parse(Request.QueryString["SiteId"]);
            var phyAllowed = (from x in _db.PhysicianPrefixCodes select x.PrefixCode).ToList<string>();
            //Only ones for this site and are active for the site and overall, also limit to valid starting licensenumbers
            //var qry = from x in _db.PhysicianSites
            //          where
            //              (
            //              (x.SiteId == siteId)
            //              && (x.Active == true)
            //              && (x.Physician.Active == true)
            //              && (
            //              (x.Physician.LicenseNumber.Substring(0, 4) == "2301")
            //              || (x.Physician.LicenseNumber.Substring(0, 4) == "2901")
            //              || (x.Physician.LicenseNumber.Substring(0, 4) == "4301")
            //              || (x.Physician.LicenseNumber.Substring(0, 4) == "5101")
            //              || (x.Physician.LicenseNumber.Substring(0, 4) == "5901")
            //              )

            //              )
            //          orderby x.Physician.LastName
            //          select x;
            var qry = from x in _db.PhysicianSites
                      where
                      (
                          (x.SiteId == siteId)
                          && (x.Active == true)
                          && (x.Physician.Active == true)
                          && (
                          phyAllowed.Contains(x.Physician.LicenseNumber.Substring(0, 4))
                      ))
                      orderby x.Physician.LastName
                      select x;

            if (qry.Any())
            {
                List<ListItem> myPhysicianList = new List<ListItem>();
                foreach (var item in qry)
                {
                    string lName = item.Physician == null ? "" : item.Physician.LastName == null ? "" : item.Physician.LastName.ToUpper();
                    string fName = item.Physician == null ? "" : item.Physician.FirstName == null ? "" : item.Physician.FirstName.ToUpper();
                    string mName = item.Physician == null ? "" : item.Physician.MiddleName == null ? "" : item.Physician.MiddleName.ToUpper();
                    string license = item.Physician == null ? "" : item.Physician.LicenseNumber == null ? "" : item.Physician.LicenseNumber.ToUpper();

                    //myPhysicianList.Add(new ListItem(string.Format("{0}, {1} {2} ({3})", lName, fName, mName, license), item.PhysicianId.ToString()));
                    myPhysicianList.Add(new ListItem(string.Format("{0}, {1} {2} ({3})", lName, fName, mName, license), item.PhysicianSiteId.ToString()));
                }
                myPhysicianList.OrderBy(x => x.Text).ToList();

                ddlReferringPhysician.Items.AddRange(myPhysicianList.ToArray());
            }
        }

        protected void clearErrorMessages()
        {
            //Blank out all the error messages
            lbltbPatientName.Text = "";
            lbltbAccountNumber.Text = "";
            lbltbMedicalRecordNumber.Text = "";
            lbltbScanDescription.Text = "";
            lbltbDateOfScan.Text = "";
            lbltbAgeOfPatient.Text = "";
            lblddlAgeCode.Text = "";
            lblddlSex.Text = "";
            lbltbZipcodeOfResidence.Text = "";
            lblddlCountyOfResidence.Text = "";
            lblddlExpectedSourceOfPayment.Text = "";
            lblddlReferringPhysician.Text = "";
            lblddlPatientStatus.Text = "";
            lblddlClinicalStatus.Text = "";

            lblddlRegion1.Text = "";
            lblddlSedation1.Text = "";
            lblddlScanContrast1.Text = "";
            lblddlComplete1.Text = "";

            lblddlRegion2.Text = "";
            lblddlSedation2.Text = "";
            lblddlScanContrast2.Text = "";
            lblddlComplete2.Text = "";

            lblddlRegion3.Text = "";
            lblddlSedation3.Text = "";
            lblddlScanContrast3.Text = "";
            lblddlComplete3.Text = "";

            lblddlRegion4.Text = "";
            lblddlSedation4.Text = "";
            lblddlScanContrast4.Text = "";
            lblddlComplete4.Text = "";

            lblddlRegion5.Text = "";
            lblddlSedation5.Text = "";
            lblddlScanContrast5.Text = "";
            lblddlComplete5.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clearErrorMessages();

            //Validate information
            bool okToProcess = true;

            if (!(MyValidation.cvText(tbPatientName.Text)))
            {
                okToProcess = false;
                lbltbPatientName.Text = "<br/>Please enter the patient name using alphanumberic values.";
            }

            if (!(MyValidation.cvText(tbAccountNumber.Text)))
            {
                okToProcess = false;
                lbltbAccountNumber.Text = "<br/>Please enter the account number using alphanumberic values.";
            }

            if (!(MyValidation.cvText(tbMedicalRecordNumber.Text)))
            {
                okToProcess = false;
                lbltbMedicalRecordNumber.Text = "<br/>Please enter the medical record number using alphanumberic values.";
            }

            if (!(MyValidation.cvText(tbScanDescription.Text)))
            {
                okToProcess = false;
                lbltbScanDescription.Text = "<br/>Please enter the scan description using alphanumberic values.";
            }


            if (string.IsNullOrWhiteSpace(tbDateOfScan.Text))
            {
                okToProcess = false;
                lbltbDateOfScan.Text = "<br/>Please enter the date of scan.";
            }
            else
            {
                DateTime myDate;
                if (!(DateTime.TryParse(tbDateOfScan.Text, out myDate)))
                {
                    okToProcess = false;
                    lbltbDateOfScan.Text = "<br/>Please enter a valid date of scan.";
                }
            }

            if (string.IsNullOrWhiteSpace(tbAgeOfPatient.Text))
            {
                okToProcess = false;
                lbltbAgeOfPatient.Text = "<br/>Please enter the age of patient.";
            }
            else
            {

                if (MyValidation.cvNumberValidate(tbAgeOfPatient.Text))
                {
                    int myNumber = Convert.ToInt32(tbAgeOfPatient.Text);
                    if (myNumber < 1 || myNumber > 99)
                    {
                        okToProcess = false;
                        lbltbAgeOfPatient.Text = "<br/>Please enter a number from 1-99.";
                    }
                }
                else
                {
                    okToProcess = false;
                    lbltbAgeOfPatient.Text = "<br/>Please enter the age of patient using numberic values.";
                }
            }

            if (ddlAgeCode.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlAgeCode.Text = "<br/>Please select an age code.";
            }

            if (ddlSex.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlSex.Text = "<br/>Please select sex of patient.";
            }

            if (string.IsNullOrWhiteSpace(tbZipcodeOfResidence.Text))
            {
                okToProcess = false;
                lbltbZipcodeOfResidence.Text = "<br/>Please enter the zipcode.";
            }
            else
            {

                if (MyValidation.cvNumberValidate(tbZipcodeOfResidence.Text))
                {
                    int myNumber = Convert.ToInt32(tbZipcodeOfResidence.Text);
                    if (myNumber < 1 || myNumber > 99999)
                    {
                        okToProcess = false;
                        lbltbZipcodeOfResidence.Text = "<br/>Please enter a number from 1-99999.";
                    }
                    else
                    {
                        //Validate zipcode in DB
                    }
                }
                else
                {
                    okToProcess = false;
                    lbltbZipcodeOfResidence.Text = "<br/>Please enter the zipcode using numberic values.";
                }
            }

            if (ddlCountyOfResidence.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlCountyOfResidence.Text = "<br/>Please select county of residence.";
            }

            if (ddlExpectedSourceOfPayment.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlExpectedSourceOfPayment.Text = "<br/>Please select the expected source of payment.";
            }

            if (ddlReferringPhysician.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlReferringPhysician.Text = "<br/>Please select a referring physician.";
            }

            if (ddlPatientStatus.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlPatientStatus.Text = "<br/>Please select patient status.";
            }

            if (ddlClinicalStatus.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlClinicalStatus.Text = "<br/>Please select clinical status.";
            }

            if (ddlRegion1.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlRegion1.Text = "<br/>Please select scan region.";
            }

            if (ddlSedation1.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlSedation1.Text = "<br/>Please select sedation.";
            }

            if (ddlScanContrast1.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlScanContrast1.Text = "<br/>Please select scan contrast.";
            }

            if (ddlComplete1.SelectedItem.Text == "Not Assigned")
            {
                okToProcess = false;
                lblddlComplete1.Text = "<br/>Please select scan complete.";
            }

            //Only verify all other scan information if scan 2 filled in - will not save scan 3-5 if scan 2 not filled in
            if (!(ddlRegion2.SelectedItem.Text == "Not Assigned"))
            {

                if (ddlSedation2.SelectedItem.Text == "Not Assigned")
                {
                    okToProcess = false;
                    lblddlSedation2.Text = "<br/>Please select sedation.";
                }

                if (ddlScanContrast2.SelectedItem.Text == "Not Assigned")
                {
                    okToProcess = false;
                    lblddlScanContrast2.Text = "<br/>Please select scan contrast.";
                }

                if (ddlComplete2.SelectedItem.Text == "Not Assigned")
                {
                    okToProcess = false;
                    lblddlComplete2.Text = "<br/>Please select scan complete.";
                }

                //Only verify all other scan information if scan 3 filled in
                if (!(ddlRegion3.SelectedItem.Text == "Not Assigned"))
                {

                    if (ddlSedation3.SelectedItem.Text == "Not Assigned")
                    {
                        okToProcess = false;
                        lblddlSedation3.Text = "<br/>Please select sedation.";
                    }

                    if (ddlScanContrast3.SelectedItem.Text == "Not Assigned")
                    {
                        okToProcess = false;
                        lblddlScanContrast3.Text = "<br/>Please select scan contrast.";
                    }

                    if (ddlComplete3.SelectedItem.Text == "Not Assigned")
                    {
                        okToProcess = false;
                        lblddlComplete3.Text = "<br/>Please select scan complete.";
                    }

                    //Only verify all other scan information if scan 4 filled in
                    if (!(ddlRegion4.SelectedItem.Text == "Not Assigned"))
                    {

                        if (ddlSedation4.SelectedItem.Text == "Not Assigned")
                        {
                            okToProcess = false;
                            lblddlSedation4.Text = "<br/>Please select sedation.";
                        }

                        if (ddlScanContrast4.SelectedItem.Text == "Not Assigned")
                        {
                            okToProcess = false;
                            lblddlScanContrast4.Text = "<br/>Please select scan contrast.";
                        }

                        if (ddlComplete4.SelectedItem.Text == "Not Assigned")
                        {
                            okToProcess = false;
                            lblddlComplete4.Text = "<br/>Please select scan complete.";
                        }

                        //Only verify all other scan information if scan 5 filled in
                        if (!(ddlRegion5.SelectedItem.Text == "Not Assigned"))
                        {

                            if (ddlSedation5.SelectedItem.Text == "Not Assigned")
                            {
                                okToProcess = false;
                                lblddlSedation5.Text = "<br/>Please select sedation.";
                            }

                            if (ddlScanContrast5.SelectedItem.Text == "Not Assigned")
                            {
                                okToProcess = false;
                                lblddlScanContrast5.Text = "<br/>Please select scan contrast.";
                            }

                            if (ddlComplete5.SelectedItem.Text == "Not Assigned")
                            {
                                okToProcess = false;
                                lblddlComplete5.Text = "<br/>Please select scan complete.";
                            }
                        }

                    }
                }
            }

            if (okToProcess)
            {
                //Move the data to the history (Set the field ScanUpdate to True, then insert new information all over.
                int scanId = Int32.Parse(Request.QueryString["ScanId"]);
                var updateRecord = from p in _db.Scans where p.ScanId == scanId select p;
                if (updateRecord.Any())
                {
                    foreach (var updateItem in updateRecord)
                    {
                        updateItem.ScanUpdated = true;
                    }
                }

                //Insert update as new record
                Scan ScanInsert = new Scan();
                ScanInsert.CreatedByGUID = _user.Guid;
                ScanInsert.CreatedByDisplayName = _user.DisplayName;
                ScanInsert.CreatedByUserId = _user.Username;
                ScanInsert.CreatedDateTime = DateTime.Now;
                Int32 siteId;
                siteId = Int32.Parse(Request.QueryString["SiteId"]);
                ScanInsert.SiteId = siteId;

                DateTime dateOfScan;
                bool isDate = DateTime.TryParse(tbDateOfScan.Text, out dateOfScan);
                if (isDate)
                {
                    ScanInsert.DateOfScan = dateOfScan.Date;
                }

                ScanInsert.AgeOfPatient = tbAgeOfPatient.Text.PadLeft(2, '0');

                ScanInsert.AgeCodeId = Convert.ToInt32(ddlAgeCode.SelectedValue);

                ScanInsert.SexId = Convert.ToInt32(ddlSex.SelectedValue);

                ScanInsert.ZipCode = tbZipcodeOfResidence.Text.PadLeft(5, '0');

                ScanInsert.CountyId = Convert.ToInt32(ddlCountyOfResidence.SelectedValue);

                ScanInsert.PatientStatusId = Convert.ToInt32(ddlPatientStatus.SelectedValue);

                ScanInsert.ClinicalStatusId = Convert.ToInt32(ddlClinicalStatus.SelectedValue);

                ScanInsert.PhysicianSiteId = Convert.ToInt32(ddlReferringPhysician.SelectedValue);

                ScanInsert.SourceOfPaymentId = Convert.ToInt32(ddlExpectedSourceOfPayment.SelectedValue);

                if (!(string.IsNullOrWhiteSpace(tbPatientName.Text)))
                {
                    ScanInsert.PatientName = tbPatientName.Text;
                }

                if (!(string.IsNullOrWhiteSpace(tbAccountNumber.Text)))
                {
                    ScanInsert.AccountNumber = tbAccountNumber.Text;
                }

                if (!(string.IsNullOrWhiteSpace(tbMedicalRecordNumber.Text)))
                {
                    ScanInsert.MedicalRecordNumber = tbMedicalRecordNumber.Text;
                }

                if (!(string.IsNullOrWhiteSpace(tbScanDescription.Text)))
                {
                    ScanInsert.ScanDescription = tbScanDescription.Text;
                }

                _db.Scans.Add(ScanInsert);
                scanId = ScanInsert.ScanId;

                //ScanResults save Scan 1
                ScanResult scanResultInsert = new ScanResult();
                scanResultInsert.ScanId = scanId;
                scanResultInsert.ScanRegionId = Convert.ToInt32(ddlRegion1.SelectedValue);
                scanResultInsert.SedationId = Convert.ToInt32(ddlSedation1.SelectedValue);
                scanResultInsert.ScanContrastId = Convert.ToInt32(ddlScanContrast1.SelectedValue);
                scanResultInsert.ScanCompleteId = Convert.ToInt32(ddlComplete1.SelectedValue);
                _db.ScanResults.Add(scanResultInsert);

                //ScanResults only save the ones entered if 2 not filled in done, if 3 not filled in done ect.
                if (!(ddlRegion2.SelectedItem.Text == "Not Assigned"))
                {
                    //ScanResults save Scan 2
                    scanResultInsert = new ScanResult();
                    scanResultInsert.ScanId = scanId;
                    scanResultInsert.ScanRegionId = Convert.ToInt32(ddlRegion2.SelectedValue);
                    scanResultInsert.SedationId = Convert.ToInt32(ddlSedation2.SelectedValue);
                    scanResultInsert.ScanContrastId = Convert.ToInt32(ddlScanContrast2.SelectedValue);
                    scanResultInsert.ScanCompleteId = Convert.ToInt32(ddlComplete2.SelectedValue);
                    _db.ScanResults.Add(scanResultInsert);

                    if (!(ddlRegion3.SelectedItem.Text == "Not Assigned"))
                    {
                        //ScanResults save Scan 2
                        scanResultInsert = new ScanResult();
                        scanResultInsert.ScanId = scanId;
                        scanResultInsert.ScanRegionId = Convert.ToInt32(ddlRegion3.SelectedValue);
                        scanResultInsert.SedationId = Convert.ToInt32(ddlSedation3.SelectedValue);
                        scanResultInsert.ScanContrastId = Convert.ToInt32(ddlScanContrast3.SelectedValue);
                        scanResultInsert.ScanCompleteId = Convert.ToInt32(ddlComplete3.SelectedValue);
                        _db.ScanResults.Add(scanResultInsert);

                        if (!(ddlRegion4.SelectedItem.Text == "Not Assigned"))
                        {
                            //ScanResults save Scan 4
                            scanResultInsert = new ScanResult();
                            scanResultInsert.ScanId = scanId;
                            scanResultInsert.ScanRegionId = Convert.ToInt32(ddlRegion4.SelectedValue);
                            scanResultInsert.SedationId = Convert.ToInt32(ddlSedation4.SelectedValue);
                            scanResultInsert.ScanContrastId = Convert.ToInt32(ddlScanContrast4.SelectedValue);
                            scanResultInsert.ScanCompleteId = Convert.ToInt32(ddlComplete4.SelectedValue);
                            _db.ScanResults.Add(scanResultInsert);

                            if (!(ddlRegion5.SelectedItem.Text == "Not Assigned"))
                            {
                                //ScanResults save Scan 5
                                scanResultInsert = new ScanResult();
                                scanResultInsert.ScanId = scanId;
                                scanResultInsert.ScanRegionId = Convert.ToInt32(ddlRegion5.SelectedValue);
                                scanResultInsert.SedationId = Convert.ToInt32(ddlSedation5.SelectedValue);
                                scanResultInsert.ScanContrastId = Convert.ToInt32(ddlScanContrast5.SelectedValue);
                                scanResultInsert.ScanCompleteId = Convert.ToInt32(ddlComplete5.SelectedValue);
                                _db.ScanResults.Add(scanResultInsert);
                            }
                        }
                    }
                }

                //Update the old record to being updated and create the new scan information
                _db.SaveChanges();

                Response.Redirect(string.Format("~/Views/Entry/Modify.aspx?SiteId={0}", siteId));
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Views/Entry/Modify.aspx?SiteId={0}", Int32.Parse(Request.QueryString["SiteId"])));
        }

    }
}