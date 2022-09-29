using MRI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRI.Views.Admin
{
    public partial class ManageSites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            fvSites.Visible = true;
            LinkButton4.Visible = false;
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            fvSites.Visible = false;
            LinkButton4.Visible = true;
        }

        protected void edsSites_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvSites.DataBind();
        }

        protected void edsSites_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvSites.DataBind();
        }

    }
}