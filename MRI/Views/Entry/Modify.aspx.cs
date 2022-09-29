using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace MRI.Views.Entry
{
    public partial class Edit : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        private Int32 _siteId;

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
                                literalTitle.Text = String.Format(@"<h3><strong>Modify a MRI Order for {0}:</strong></h3>", item.SiteName);
                                tbFrom.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                                tbTo.Text = DateTime.Now.ToShortDateString();
                                pnlScanList.Visible = true;

                                //Create Grid View List of Scans - Displaying 15-20 per page. Create New MRI for manual Entry also.

                            }
                            else
                            {
                                pnlScanList.Visible = false;
                                phMessageText(@"<h2>You do not have access to the Site id parameter that was passed.</h2>");
                            }
                        }
                    }
                    else
                    {
                        pnlScanList.Visible = false;
                        phMessageText(@"<h2>Site id parameter was not found or not active or no active directory groups defined.</h2>");
                    }
                }
                else
                {
                    pnlScanList.Visible = false;
                    //Display message not valid Site Id
                    phMessageText(@"<h2>Site id parameter is not valid.</h2>");
                }
            }
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {

        }

        protected void FilterReport(object sender, CustomExpressionEventArgs e)
        {
            e.Query = from ex in e.Query.Cast<Entities.Scan>() where ex.SiteId == _siteId select ex;
        }

        protected void gvScanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Does not currently check to make sure not already there in Scan table. Maybe add to look up by AccountNumber - Date?

            //Call ModifyData to show the data.
            Int32 scanId;
            bool isInt = Int32.TryParse(gvScanList.SelectedDataKey.Value.ToString(), out scanId);
            if (isInt) //Should always be
            {
                Response.Redirect(string.Format("~/Views/Entry/ModifyData.aspx?SiteId={0}&ScanId={1}", Int32.Parse(Request.QueryString["SiteId"]), scanId));
            }
        }
    }
}