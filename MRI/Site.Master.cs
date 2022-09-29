using MRI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MRI
{
    public partial class SiteMaster : MasterPage
    {
        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string strURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            string strAppPath = HttpContext.Current.Request.ApplicationPath.ToString();

            if (strAppPath == "/")
            {
                strAppPath = string.Empty;
            }

            //lblName.Text = _user.DisplayName + "<br/>" + (string.IsNullOrWhiteSpace(_user.Email) ? "No email address in AD" : _user.Email);
            lblName.Text = _user.DisplayName;

            //Get admin tab
            if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(MyRoles.PhysicianAdmin))
            {
                HtmlGenericControl _li = new HtmlGenericControl("li");
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                //anchor.Attributes.Add("href", "/MRI/Views/Admin/Index.aspx");
                anchor.Attributes.Add("href", strURL + strAppPath + "/Views/Admin/Index.aspx");
                anchor.InnerText = "Admin";
                _li.Controls.Add(anchor);
                phMenu.Controls.Add(_li);
            }

            //Instructions link
            HtmlGenericControl lnkAnchor = new HtmlGenericControl("a");
            lnkAnchor.Attributes.Add("href", strURL + strAppPath + "/Content/pdf/MRI_Quarterly_Data_Instruction_Manual.02.25.2015_483011_7.pdf");
            lnkAnchor.Attributes.Add("target", "_blank");
            lnkAnchor.InnerText = "MRI Quarterly Data Instructions";
            phInstructionsLink.Controls.Add(lnkAnchor);

        }
    }
}