using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Web.UI.HtmlControls;

namespace MRI.Views.Admin
{
    public partial class Report : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        protected void phMessageText(string messageToDisplay)
        {
            phMessage.Controls.Add(new LiteralControl(string.Format(@"<div class=""col-md-11""><p>{0}</p></div>", messageToDisplay)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 siteId;
            bool isInt = (Int32.TryParse(Request.QueryString["SiteId"], out siteId));
            if (isInt)
            {
                //See if valid SiteId
                var qrySites = from x in _db.Sites where ((x.ADGroupAdmin != null) && (x.ADGroupReport != null) && (x.Active) && (x.SiteId == siteId)) select x;
                if (qrySites.Any())
                {
                    foreach (var item in qrySites)
                    {
                        if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(item.ADGroupAdmin) || HttpContext.Current.User.IsInRole(item.ADGroupReport))
                        {
                            literalTitle.Text = String.Format(@"<h3><strong>MRI Report for {0}:</strong></h3>", item.SiteName);
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

            if (isInt)
            {
                if (!Page.IsPostBack)
                {
                    tbFrom.Text = string.Format("{0}/01/{1}", DateTime.Today.AddMonths(-1).Month.ToString(), DateTime.Today.AddMonths(-1).Year.ToString());
                    tbTo.Text = DateTime.Today.AddDays(1).ToShortDateString();

                    getPhysicians();

                    for (int i = 0; i < gvScanList.Columns.Count; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = gvScanList.Columns[i].HeaderText;
                        item.Value = gvScanList.Columns[i].HeaderText;
                        cblColumns.Items.Add(item);
                    }
                    // Set the default column display
                    foreach (System.Web.UI.WebControls.ListItem cb in cblColumns.Items)
                    {
                        switch (cb.Text)
                        {
                            case "Date Of Scan":
                                cb.Selected = true;
                                break;
                            case "Patient Name":
                                cb.Selected = true;
                                break;
                            case "Account Number":
                                cb.Selected = true;
                                break;
                            case "Medical Record Number":
                                cb.Selected = false;
                                break;
                            case "Description Of Scan":
                                cb.Selected = true;
                                break;
                            case "Sex":
                                cb.Selected = true;
                                break;
                            case "Zip Code":
                                cb.Selected = false;
                                break;
                            case "Patient Status":
                                cb.Selected = false;
                                break;
                            case "Age Of Patient":
                                cb.Selected = true;
                                break;
                            case "Age Code":
                                cb.Selected = true;
                                break;
                            case "Site Name":
                                cb.Selected = true;
                                break;
                            case "County":
                                cb.Selected = false;
                                break;
                            case "Clinical Status":
                                cb.Selected = false;
                                break;
                            case "Physician":
                                cb.Selected = true;
                                break;
                            default:
                                cb.Selected = false;
                                break;
                        }
                    }
                }
                // Display selected columns
                int cbColumnsIndex = 0;
                foreach (System.Web.UI.WebControls.ListItem cb in cblColumns.Items)
                {
                    gvScanList.Columns[cbColumnsIndex].Visible = cb.Selected;
                    cbColumnsIndex++;
                }
                gvScanList.DataBind();

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx");
        }

        public GridView Getgrid
        {
            get
            {
                return this.gvScanList; //ViewState needs to be enabled for gridview
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvScanList.Rows.Count > 0)
            {
                string filename = string.Format("MRIReport-{0}.xls", DateTime.Now.ToString());
                gvScanList.AllowPaging = false;
                gvScanList.AllowSorting = false;
                gvScanList.DataBind();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                // If you want the option to open the Excel file without saving than 
                // comment out the line below 
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                gvScanList.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
                //gvITAR.AllowPaging = true;
                gvScanList.AllowSorting = true;
                gvScanList.DataBind();
            }
            else
            {
                string msg = "There is no data available for export.";
                System.Web.HttpContext.Current.Response.Write(string.Format("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"{0}\")</SCRIPT>", msg));
            }
        }

        public void getPhysicians()
        {
            Int32 siteId = Int32.Parse(Request.QueryString["SiteId"]);
            List<ListItem> physicianList = new List<ListItem>();
            var qry = from x in _db.PhysicianSites where (x.SiteId == siteId) select x;
            if (qry.Any())
            {
                List<ListItem> myPhysicianList = new List<ListItem>();
                foreach (var item in qry)
                {
                    //incase one of them has whitespace
                    if (!(string.IsNullOrWhiteSpace(item.Physician.LastName + item.Physician.FirstName + item.Physician.MiddleName + item.Physician.LicenseNumber)))
                    {
                        myPhysicianList.Add(new ListItem(string.Format("{0}, {1} {2} ({3})", item.Physician.LastName, item.Physician.FirstName, item.Physician.MiddleName, item.Physician.LicenseNumber), item.PhysicianId.ToString()));
                    }
                }
                myPhysicianList.OrderBy(x => x.Text).ToList();

                ddlReferringPhysician.Items.AddRange(myPhysicianList.ToArray());
            }
        }

        protected void gvScanList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {

        }

        protected void FilterReport(object sender, CustomExpressionEventArgs e)
        {
            Int32 siteId;
            bool isInt = (Int32.TryParse(Request.QueryString["SiteId"], out siteId));
            e.Query = from ex in e.Query.Cast<Entities.Scan>() where ex.SiteId == siteId select ex;

            if (ddlReferringPhysician.SelectedValue == "All")
            {
                //No filtering on this
            }
            else
            {
                List<Int32> physicianSearch = new List<Int32>();
                physicianSearch.Add(Int32.Parse(ddlReferringPhysician.SelectedValue));
                e.Query = from ex in e.Query.Cast<Entities.Scan>() where physicianSearch.Contains(ex.PhysicianSite.PhysicianId) select ex;
            }

        }


    }
}