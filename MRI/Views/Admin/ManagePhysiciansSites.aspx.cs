using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace MRI.Views.Admin
{
    public partial class ManagePhysiciansSites : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvPhysicianSites.DataBind();
        }

        protected void edsPhysicianSites_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvPhysicianSites.DataBind();
        }

        protected void edsPhysicianSites_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvPhysicianSites.DataBind();
        }

        protected string GetName(object id)
        {
            string myReturn = string.Empty;
            Int32 phyId = (Int32)id;
            var qryPhysician = (from x in _db.Physicians where (x.PhysicianId == phyId) select x).FirstOrDefault();
		    myReturn = qryPhysician.LastName + ", " + qryPhysician.FirstName + " " + qryPhysician.MiddleName + " - " + qryPhysician.LicenseNumber;
            return myReturn;
        }

        protected string GetSiteName(object id)
        {
            string myReturn = string.Empty;
            Int32 siteId = (Int32)id;
            var qrySites = (from x in _db.Sites where (x.SiteId == siteId) select x).FirstOrDefault();
            myReturn = qrySites.SiteName;
            return myReturn;
        }

        protected void FiltergvPhysicians(object sender, CustomExpressionEventArgs e)
        {
            if (ddlSitesFilter.SelectedValue == "All")
            {
                //No filtering on this
            }
            else
            {
                List<Int32> siteSearch = new List<Int32>();
                siteSearch.Add(Convert.ToInt32(ddlSitesFilter.SelectedItem.Value));
                e.Query = from ex in e.Query.Cast<Entities.PhysicianSite>() where siteSearch.Contains(ex.SiteId) select ex;
            }

        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            gvPhysicianSites.DataBind();
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            fvPhysicianSites.Visible = false;
            LinkButton4.Visible = true;
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            fvPhysicianSites.Visible = true;
            LinkButton4.Visible = false;
        }

    }
}