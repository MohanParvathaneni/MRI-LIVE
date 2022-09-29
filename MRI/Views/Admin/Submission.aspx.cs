using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace MRI.Views.Admin
{
    public partial class Submission : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        protected void phMessageText(string messageToDisplay)
        {
            phMessage.Controls.Add(new LiteralControl(string.Format(@"<div class=""col-md-11""><p>{0}</p></div>", messageToDisplay)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 siteId;
            bool isInt = (Int32.TryParse(Request.QueryString["SiteId"], out siteId));
            if (!Page.IsPostBack)
            {
                if (isInt)
                {
                    //See if valid SiteId
                    var qrySites = from x in _db.Sites where ((x.ADGroupAdmin != null) && (x.ADGroupSubmissionReport != null) && (x.Active) && (x.SiteId == siteId)) select x;
                    if (qrySites.Any())
                    {
                        foreach (var item in qrySites)
                        {
                            if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(item.ADGroupAdmin) || HttpContext.Current.User.IsInRole(item.ADGroupSubmissionReport))
                            {
                                literalTitle.Text = String.Format(@"<h3><strong>Create {0} MRI Scan - submission files:</strong></h3>", item.SiteName);
                                ddlYear.SelectedValue = DateTime.Now.Year.ToString();

                                pnlSubmission.Visible = true;
                            }
                            else
                            {
                                pnlSubmission.Visible = false;
                                phMessageText(@"<h2>You do not have access to the Site id parameter that was passed.</h2>");
                            }
                        }

                    }
                    else
                    {
                        pnlSubmission.Visible = false;
                        phMessageText(@"<h2>Site id parameter was not found or not active or no active directory groups defined.</h2>");
                    }
                }
                else
                {
                    pnlSubmission.Visible = false;
                    //Display message not valid Site Id
                    phMessageText(@"<h2>Site id parameter is not valid.</h2>");
                }
            }

            if (!Page.IsPostBack)
            {
                for (int i = 2005; i <= DateTime.Now.Year; i++)
                    ddlYear.Items.Insert(0, new ListItem(i.ToString(), i.ToString()));
            }

        }

        protected string getScanInformation(Int32 scanId)
        {
            string myReturn = string.Empty;
            string scanBlank = "0   ";

            var qryScanResults = from x in _db.ScanResults where (x.ScanId == scanId) select x;
            //Get all scanResults entered
            foreach (var item in qryScanResults)
            {
                myReturn += string.Format("{0}{1}{2}{3}",
                    item.ScanRegion == null ? string.Empty.PadLeft(1,' ') : item.ScanRegion.ScanRegionCode == null ? string.Empty.PadLeft(1, ' ') : item.ScanRegion.ScanRegionCode,
                    item.Sedation == null ? string.Empty.PadLeft(1, ' ') : item.Sedation.SedationCode== null ? string.Empty.PadLeft(1,' ') : item.Sedation.SedationCode,
                    item.ScanContrast == null ? string.Empty.PadLeft(1, ' ') : item.ScanContrast.ScanContrastCode == null ? string.Empty.PadLeft(1,' ') : item.ScanContrast.ScanContrastCode,
                    item.ScanComplete == null ? string.Empty.PadLeft(1, ' ') : item.ScanComplete.ScanCompleteCode == null ? string.Empty.PadLeft(1,' ') : item.ScanComplete.ScanCompleteCode
                    );
            }
            //Add number of scans not done up to 5
            for (int i = qryScanResults.Count()+1; i <= 5; i++)
			{
                myReturn += scanBlank;
			}

            return myReturn;
        }

        protected void btnCreateReport_Click(object sender, EventArgs e)
        {
            DateTime sDate = DateTime.Now; //This will change just want default set
            DateTime eDate = DateTime.Now; //This will change just want default set
            switch (Int32.Parse(rblQuarter.SelectedValue))
	        {
                case 1:
                    sDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 1, 1);
                    eDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 4, 1);//Make it look for smaller it will get the last day of the month before
                    break;
                case 2:
                    sDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 4, 1);
                    eDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 7, 1);//Make it look for smaller it will get the last day of the month before
                    break;
                case 3:
                    sDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 7, 1);
                    eDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 10, 1);//Make it look for smaller it will get the last day of the month before
                    break;
                case 4:
                    sDate = new DateTime(Int32.Parse(ddlYear.SelectedValue), 10, 1);
                    eDate = new DateTime((Int32.Parse(ddlYear.SelectedValue)+1), 1, 1);//Make it look for smaller it will get the last day of the month before
                    break;
	        }

            Int32 siteId;
            siteId = Int32.Parse(Request.QueryString["SiteId"]);
            var qrySites = (from x in _db.Sites where (x.SiteId == siteId) select x).First();

            //string folderToPutFiles = Server.MapPath("/Files")+"\\";
            string folderToPutFiles = HttpContext.Current.Request.PhysicalApplicationPath.ToLower() + @"files\";

            string scanFileName = String.Format(@"{0}Scan{1}_{2}_{3}.txt",
                folderToPutFiles,
                qrySites.ServiceIdNumber.Substring(0,Math.Min(qrySites.ServiceIdNumber.Length, 6)),
                ddlYear.SelectedValue.Substring(ddlYear.SelectedValue.Length - 2),
                rblQuarter.SelectedValue
                );

            string physicianFileName = String.Format(@"{0}Ref{1}_{2}_{3}.txt",
                folderToPutFiles,
                qrySites.ServiceIdNumber.Substring(0, Math.Min(qrySites.ServiceIdNumber.Length, 6)),
                ddlYear.SelectedValue.Substring(ddlYear.SelectedValue.Length - 2),
                rblQuarter.SelectedValue
                );

            File.Create(scanFileName).Dispose();
            File.Create(physicianFileName).Dispose();

            //Create the files with all the sites with the same 6 digit Service Id Number
            var qrySitesServiceNumber = (from x in _db.Sites where ((x.ServiceIdNumber.ToUpper().Substring(0, 6) == qrySites.ServiceIdNumber.ToUpper().Substring(0, 6)) && (x.Active == true)) select x);
            if (qrySitesServiceNumber.Any())
            {
                foreach (var item in qrySitesServiceNumber)
                {
                    string serviceIdNumber = item.ServiceIdNumber == null ? string.Empty.PadLeft(8, ' ') : item.ServiceIdNumber.ToString();
                    BuildScanFileForSite(item.SiteId, sDate, eDate, scanFileName, serviceIdNumber);
                    BuildPhysicianFileForSite(item.SiteId, sDate, eDate, physicianFileName, serviceIdNumber);
                }
                SendEmail(qrySites.FromEmailAddress, qrySites.ToEmailAddress, qrySites.EmailSubject, scanFileName, physicianFileName, ddlYear, rblQuarter);
            }

            string scanFileNameOnly = String.Format(@"Scan{0}_{1}_{2}.txt",
                qrySites.ServiceIdNumber.Substring(0, Math.Min(qrySites.ServiceIdNumber.Length, 6)),
                ddlYear.SelectedValue.Substring(ddlYear.SelectedValue.Length - 2),
                rblQuarter.SelectedValue
                );

            string physicianFileNameOnly = String.Format(@"Ref{0}_{1}_{2}.txt",
                qrySites.ServiceIdNumber.Substring(0, Math.Min(qrySites.ServiceIdNumber.Length, 6)),
                ddlYear.SelectedValue.Substring(ddlYear.SelectedValue.Length - 2),
                rblQuarter.SelectedValue
                );

            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string strURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            string strAppPath = HttpContext.Current.Request.ApplicationPath.ToString();
            if (strAppPath == "/")
            {
                strAppPath = string.Empty;
            }

            //Generate links to the files
            phFilesLinks.Controls.Add(new LiteralControl(@"<div class=""col-md-4""><p>"));
            phFilesLinks.Controls.Add(new LiteralControl(string.Format(@"<strong>{0}</strong></p>", qrySites.SiteName)));
            createButton(string.Format(@"{0}", strURL + strAppPath + "/Files/" + scanFileNameOnly), scanFileNameOnly + " &raquo;");
            createButton(string.Format(@"{0}", strURL + strAppPath + "/Files/" + physicianFileNameOnly), physicianFileNameOnly + " &raquo;");

            phFilesLinks.Controls.Add(new LiteralControl("</div>"));


        }

        private void BuildScanFileForSite(int siteId, DateTime sDate, DateTime eDate, string scanFileName, string serviceIdNumber)
        {
            //Only get the ones from the site and in the date range that have not been modified(archive to know what was changed)
            var qryScans = (from x in _db.Scans where ((x.SiteId == siteId) && (x.DateOfScan >= sDate) && (x.DateOfScan < eDate) && (x.ScanUpdated == false)) select x);

            List<string> scanLines = new List<string>();
            if (qryScans.Any())
            {
                foreach (var item in qryScans)
                {
                    //Build the lines to be submitted
                    scanLines.Add(string.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
                        serviceIdNumber == null ? string.Empty.PadLeft(8, ' ') : serviceIdNumber,
                        item.DateOfScan.ToString("yyMMdd"),
                        item.AgeOfPatient == null ? string.Empty.PadLeft(2, ' ') : String.Format("{0:D2}", item.AgeOfPatient),
                        item.AgeCode == null ? string.Empty.PadLeft(1, ' ') : item.AgeCode.SubmissionAgeCodeId == null ? string.Empty.PadLeft(1, ' ') : item.AgeCode.SubmissionAgeCodeId,
                        item.Sex.SexCode.ToString(),
                        item.ZipCode == null ? string.Empty.PadLeft(5, ' ') : item.ZipCode,
                        item.County == null ? string.Empty.PadLeft(4, ' ') : item.County.CountyCode == null ? string.Empty.PadLeft(4, ' ') : item.County.CountyCode,
                        item.PatientStatu == null ? string.Empty.PadLeft(1, ' ') : item.PatientStatu.PatientStatusCode == null ? string.Empty.PadLeft(1, ' ') : item.PatientStatu.PatientStatusCode.ToString(),
                        item.ClinicalStatu == null ? string.Empty.PadLeft(1, ' ') : item.ClinicalStatu.ClinicalStatusCode == null ? string.Empty.PadLeft(1, ' ') : item.ClinicalStatu.ClinicalStatusCode,
                        item.PhysicianSite == null ? string.Empty.PadLeft(10, ' ') : item.PhysicianSite.Physician == null ? string.Empty.PadLeft(10, ' ') : item.PhysicianSite.Physician.LicenseNumber == null ? string.Empty.PadLeft(10, ' ') : item.PhysicianSite.Physician.LicenseNumber.PadLeft(10, ' ').Substring(item.PhysicianSite.Physician.LicenseNumber.PadLeft(10, ' ').Length - 10), //Padding with 0 if needed to get it to 10 long then getting the right 10 if longer
                        getScanInformation(item.ScanId),
                        item.SourceOfPayment == null ? string.Empty.PadLeft(2, ' ') : item.SourceOfPayment.SourceCode == null ? string.Empty.PadLeft(2, ' ') : String.Format("{0:D2}", item.SourceOfPayment.SourceCode)
                       ));
                }
                //File.WriteAllLines(scanFileName, scanLines);
                File.AppendAllLines(scanFileName, scanLines);
            }
        }

        private void BuildPhysicianFileForSite(int siteId, DateTime sDate, DateTime eDate, string physicianFileName, string serviceIdNumber)
        {
            var qryPhysicians = (from x in _db.PhysicianPrefixCodes select x.PrefixCode).ToList<string>();
            //var qryPhysicians = (from x in _db.PhysicianSites
            //                     where
            //                         (
            //                         (x.SiteId == siteId)
            //                         && (x.Active == true)
            //                         && (x.Physician.Active == true)
            //                         && (
            //                         (x.Physician.LicenseNumber.Substring(0, 4) == "2301")
            //                         || (x.Physician.LicenseNumber.Substring(0, 4) == "2901")
            //                         || (x.Physician.LicenseNumber.Substring(0, 4) == "4301")
            //                         || (x.Physician.LicenseNumber.Substring(0, 4) == "5101")
            //                         || (x.Physician.LicenseNumber.Substring(0, 4) == "5901")
            //                         )
            //                         )
            //                     select x); //Physician for this site is active on site and look at physician table to see if physician is active


            var qry = from x in _db.PhysicianSites
                      where
                      (
                          (x.SiteId == siteId)
                          && (x.Active == true)
                          && (x.Physician.Active == true)
                          && (
                          qryPhysicians.Contains(x.Physician.LicenseNumber.Substring(0, 4))
                      ))
                      orderby x.Physician.LastName
                      select x;

            
            if (qry.Any())
            {
                List<string> physicianLines = new List<string>();
                foreach (var item in qry)
                {
                    //Build the lines to be submitted
                    physicianLines.Add(string.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                        serviceIdNumber == null ? string.Empty.PadLeft(8, ' ') : serviceIdNumber,
                        item.Physician.LicenseNumber == null ? string.Empty.PadLeft(10, ' ') : item.Physician.LicenseNumber.PadLeft(10, ' ').Substring(item.Physician.LicenseNumber.PadLeft(10, ' ').Length - 10), //Padding with 0 if needed to get it to 10 long then getting the right 10 if longer
                        item.Physician.LastName == null ? string.Empty.PadLeft(20, ' ') : item.Physician.LastName.ToUpper().PadRight(20, ' '), //Left justified name in all upper case
                        item.Physician.FirstName == null ? string.Empty.PadLeft(20, ' ') : item.Physician.FirstName.ToUpper().PadRight(20, ' '), //Left justified name in all upper case
                        item.Physician.MiddleName == null ? string.Empty.PadLeft(10, ' ') : item.Physician.MiddleName.ToUpper().PadRight(10, ' '), //Left justified name in all upper case
                        item.Physician.Suffix == null ? string.Empty.PadLeft(3, ' ') : item.Physician.Suffix.ToUpper().PadRight(3, ' '),
                        item.Physician.AddressNumber == null ? string.Empty.PadLeft(10, ' ') : item.Physician.AddressNumber.ToUpper().PadRight(10, ' '),
                        item.Physician.AddressNumberSuffix == null ? string.Empty.PadLeft(6, ' ') : item.Physician.AddressNumberSuffix.ToUpper().PadRight(6, ' '),
                        item.Physician.AddressLine1 == null ? string.Empty.PadLeft(28, ' ') : item.Physician.AddressLine1.ToUpper().PadRight(28, ' '),
                        item.Physician.AddressSuffixType == null ? string.Empty.PadLeft(4, ' ') : item.Physician.AddressSuffixType.ToUpper().PadRight(4, ' '),
                        item.Physician.AddressLine2 == null ? string.Empty.PadLeft(25, ' ') : item.Physician.AddressLine2.ToUpper().PadRight(25, ' '),
                        item.Physician.City == null ? string.Empty.PadLeft(20, ' ') : item.Physician.City.ToUpper().PadRight(20, ' '),
                        item.Physician.State.StateCode == null ? string.Empty.PadLeft(2, ' ') : item.Physician.State.StateCode.ToUpper().PadRight(2, ' '),
                        item.Physician.ZipCode == null ? string.Empty.PadLeft(5, ' ') : item.Physician.ZipCode.PadRight(5, ' '),
                        item.Physician.ZipCodeExtension == null ? string.Empty.PadLeft(4, ' ') : item.Physician.ZipCodeExtension.PadRight(4, ' '),
                        item.Physician.PhysicianLicensure == null ? "" : item.Physician.PhysicianLicensure.PhysicianLicensureCode == null ? "" : item.Physician.PhysicianLicensure.PhysicianLicensureCode
                    ));
                }
                //File.WriteAllLines(physicianFileName, physicianLines);
                File.AppendAllLines(physicianFileName, physicianLines);
            }
        }

        private void SendEmail(string fromEmailAddress, string toEmailAddress, string emailSubject, string scanFileName, string physicianFileName, DropDownList ddlYear, RadioButtonList rblQuarter)
        {
            //Send email if defined
            if ((!(string.IsNullOrWhiteSpace(fromEmailAddress))) && (!(string.IsNullOrWhiteSpace(toEmailAddress))))
            {
                MailMessage message = new MailMessage();

                MailAddress mFromAddress = new MailAddress(fromEmailAddress, fromEmailAddress);
                message.From = mFromAddress;

                MailAddress mToAddress = new MailAddress(toEmailAddress, toEmailAddress);
                message.To.Add(mToAddress);

                message.Subject = emailSubject + " " + ddlYear.SelectedValue.Substring(ddlYear.SelectedValue.Length - 2) + " - " + rblQuarter.SelectedValue;

                //Attach Scan File
                Attachment myFileAttachment;
                myFileAttachment = new Attachment(scanFileName.ToLower());
                message.Attachments.Add(myFileAttachment);

                //Attach Physician File
                myFileAttachment = new Attachment(physicianFileName.ToLower());
                message.Attachments.Add(myFileAttachment);

                //SmtpClient smtpClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["MailSMTPServer"]);
                //smtpClient.Send(message);
                //smtpClient.Dispose();

                ////Log to database mail sent
                //Mail MailInsert = new Mail()
                //{
                //    EmailDate = DateTime.Now,
                //    EmailFrom = fromEmailAddress,
                //    EmailTo = toEmailAddress,
                //    EmailToCC = null,
                //    EmailToBCC = null,
                //    EmailSubject = message.Subject
                //    //EmailBody = mBody.ToString()
                //};
                //MHCC_EmailEntities EMail = new MHCC_EmailEntities();
                //EMail.Mails.Add(MailInsert);
                //EMail.SaveChanges();
            }
        }

        protected void createButton(string newLinkURL, string newLinkName, string btnCssClass = "btn btn-primary btn-sm")
        {
            phFilesLinks.Controls.Add(new LiteralControl(@"<p>"));
            HyperLink h1 = new HyperLink() { CssClass = btnCssClass, NavigateUrl = newLinkURL, Text = newLinkName };
            phFilesLinks.Controls.Add(h1);
            phFilesLinks.Controls.Add(new LiteralControl(@"</p>"));
        }

    }
}