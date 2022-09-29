using MRI.Classes;
using MRI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRI.Views.Admin
{
    public partial class PhysicianPrefixCodes : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin) || HttpContext.Current.User.IsInRole(MyRoles.PhysicianAdmin)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        protected void lblPrefix_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvprefix.Visible = false;
            lblPrefix.Visible = false;
            lblPrefix2.Visible = false;
        }

        protected void lblInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(ddlStates.SelectedItem.Text))
            {
                isValid = false;
                lblError.Text = lblError.Text + "<br/>Organization name can not be blank.";
            }
          
            if (string.IsNullOrWhiteSpace(tbPrefixCode.Text))
            {
                isValid = false;
                lblError.Text = lblError.Text + "<br/>Physician prefix code text can not be blank.";
            }

            if (tbPrefixCode.Text.Length != 4)
            {
                isValid = false;
                lblError.Text = lblError.Text + "<br/>Physician prefix code needs to be 4 characters.";
            }

            if (string.IsNullOrWhiteSpace(tbPrefixName.Text))
            {
                isValid = false;
                lblError.Text = lblError.Text + "<br/>Physician prefix name text can not be blank.";
            }

            if (!(string.IsNullOrWhiteSpace(lblError.Text)))
            {
                phError.Visible = true;
                lblError.Text = lblError.Text.Substring(5, lblError.Text.Length-5);
            }

            //Insert record
            if (isValid)
            {
                Int32 StatesId;
                Int32.TryParse(ddlStates.SelectedValue, out StatesId);

                PhysicianPrefixCode PhysicianPrefixCodesInsert = new PhysicianPrefixCode()
                {
                    //AwardName = tbOrganizationAwardName.Text,
                    StateId = StatesId,
                    PrefixCode = tbPrefixCode.Text,
                    PrefixName = tbPrefixName.Text,
                    Active = cbActive.Checked
                    //OrganizationAwardDisplayTypeId=1
                };
                _db.PhysicianPrefixCodes.Add(PhysicianPrefixCodesInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvprefix.Visible = true;
                lblPrefix.Visible = true;
                lblPrefix2.Visible = true;
                gvprefix.DataBind();
            }
        }

        protected void lblClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvprefix.Visible = true;
            lblPrefix.Visible = true;
            lblPrefix2.Visible = true;
            gvprefix.DataBind();
        }

        protected void edsPhysicianPrefixCodes_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvprefix.DataBind();
        }

        protected void edsPhysicianPrefixCodes_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvprefix.DataBind();
        }
    }
}