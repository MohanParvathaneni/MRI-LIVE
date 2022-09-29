using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRI
{
    public partial class _Default : Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        protected void createButton(string newLinkURL, string newLinkName, string btnCssClass = "btn btn-primary btn-sm")
        {
            phCreateButtons.Controls.Add(new LiteralControl(@"<p>"));
            HyperLink h1 = new HyperLink() { CssClass = btnCssClass, NavigateUrl = newLinkURL, Text = newLinkName };
            phCreateButtons.Controls.Add(h1);
            phCreateButtons.Controls.Add(new LiteralControl(@"</p>"));
        }

        protected void createSection(string adminGroup, string dataEntryGroup, string dataModifyEntryGroup, string dataSubmissionReport, string dataReport, string siteName, int siteId)
        {
            
            bool developers = HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers);
            bool overAllAdmins = HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin);
            bool siteAdmins = HttpContext.Current.User.IsInRole(adminGroup);
            bool siteEntrys = HttpContext.Current.User.IsInRole(dataEntryGroup);
            bool siteModifys = HttpContext.Current.User.IsInRole(dataModifyEntryGroup);
            bool siteSubmissions = HttpContext.Current.User.IsInRole(dataSubmissionReport);
            bool siteReports = HttpContext.Current.User.IsInRole(dataReport);

            if (developers || overAllAdmins || siteAdmins || siteEntrys || siteModifys || siteSubmissions || siteReports)
            {
                //Create
                phCreateButtons.Controls.Add(new LiteralControl(@"<div class=""col-md-4""><p>"));
                phCreateButtons.Controls.Add(new LiteralControl(string.Format(@"<strong>{0}</strong></p>", siteName)));
                //createButton(string.Format(@"Views/Entry/Create.aspx?SiteId={0}", siteId), "Create a New MRI &raquo;");
                createButton(string.Format(@"Views/Entry/CreateData.aspx?SiteId={0}", siteId), "Create a New MRI &raquo;");
                if (developers || overAllAdmins || siteAdmins || siteModifys)
                {
                    createButton(string.Format(@"Views/Entry/Modify.aspx?SiteId={0}", siteId), "Modify a MRI &raquo;", "btn btn-info btn-sm");
                }

                if (developers || overAllAdmins || siteAdmins || siteSubmissions)
                {
                    createButton(string.Format(@"Views/Admin/Submission.aspx?SiteId={0}", siteId), "Create Submission Files &raquo;", "btn btn-warning btn-sm");
                }

                if (developers || overAllAdmins || siteAdmins || siteReports)
                {
                    createButton(string.Format(@"Views/Admin/Report.aspx?SiteId={0}", siteId), "Generate Report &raquo;", "btn btn-warning btn-sm");
                }

                phCreateButtons.Controls.Add(new LiteralControl("</div>"));
            }

            //if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(adminGroup) || HttpContext.Current.User.IsInRole(dataEntryGroup) || HttpContext.Current.User.IsInRole(dataModifyEntryGroup))
            //{
            //    //Create
            //    phCreateButtons.Controls.Add(new LiteralControl(@"<div class=""col-md-4""><p>"));
            //    phCreateButtons.Controls.Add(new LiteralControl(string.Format(@"<strong>{0}</strong></p>",siteName)));
            //    //createButton(string.Format(@"Views/Entry/Create.aspx?SiteId={0}", siteId), "Create a New MRI &raquo;");
            //    createButton(string.Format(@"Views/Entry/CreateData.aspx?SiteId={0}", siteId), "Create a New MRI &raquo;");
            //    if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(adminGroup) || HttpContext.Current.User.IsInRole(dataModifyEntryGroup))
            //    {
            //        createButton(string.Format(@"Views/Entry/Modify.aspx?SiteId={0}", siteId), "Modify a MRI &raquo;", "btn btn-info btn-sm");
            //    }

            //    if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(adminGroup) || HttpContext.Current.User.IsInRole(dataSubmissionReport))
            //    {
            //        createButton(string.Format(@"Views/Admin/Submission.aspx?SiteId={0}", siteId), "Create Submission Files &raquo;", "btn btn-warning btn-sm");
            //    }

            //    if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(adminGroup) || HttpContext.Current.User.IsInRole(dataReport))
            //    {
            //        createButton(string.Format(@"Views/Admin/Report.aspx?SiteId={0}", siteId), "Generate Report &raquo;", "btn btn-warning btn-sm");
            //    }

            //    phCreateButtons.Controls.Add(new LiteralControl("</div>"));
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int countSections = 0;
            //It will load for everyone just not everyone will get buttons.

            //Notice if no email setup in AD
            if (string.IsNullOrWhiteSpace(_user.Email))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + SpecialFunctions.emailAddressMessage + "');", true);
            }

            var qrySites = from x in _db.Sites where ((x.ADGroupAdmin != null) && (x.ADGroupUserEntry != null) && (x.ADGroupModifyEntry != null) && (x.ADGroupSubmissionReport != null) && (x.ADGroupReport != null) && (x.Active)) orderby x.SiteName select x;
            phCreateButtons.Controls.Add(new LiteralControl(@"<div class=""row"">"));
            if (qrySites.Any())
            {
                foreach (var item in qrySites)
                {
                    createSection(item.ADGroupAdmin, item.ADGroupUserEntry, item.ADGroupModifyEntry, item.ADGroupSubmissionReport, item.ADGroupReport ,item.SiteName, item.SiteId);
                    countSections++;
                    //Only allowing 3 columns per row.
                    if ((countSections % 3) == 0)
                    {
                        phCreateButtons.Controls.Add(new LiteralControl(@"</div>"));
                        phCreateButtons.Controls.Add(new LiteralControl(@"<hr/>"));
                        phCreateButtons.Controls.Add(new LiteralControl(@"<div class=""row"">"));
                    }
                }
            }
            phCreateButtons.Controls.Add(new LiteralControl(@"</div>"));

        }
    }
}