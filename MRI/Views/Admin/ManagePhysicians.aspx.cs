using MRI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace MRI.Views.Admin
{
    public partial class ManagePhysicians : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(MyRoles.PhysicianAdmin)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
            hlPhysicianLookup.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["StateLinkPhysicians"];
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            fvPhysicians.Visible = true;
            LinkButton4.Visible = false;
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            fvPhysicians.Visible = false;
            LinkButton4.Visible = true;
        }

        protected void edsPhysicians_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvPhysicians.DataBind();
        }

        protected void edsPhysicians_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvPhysicians.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvPhysicians.DataBind();
        }

        protected void FiltergvPhysicians(object sender, CustomExpressionEventArgs e)
        {

        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }

    }
}