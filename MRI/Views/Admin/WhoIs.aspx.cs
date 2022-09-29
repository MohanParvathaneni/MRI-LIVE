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
    public partial class WhoIs : System.Web.UI.Page
    {
        private MHCC_MRIEntities1 _db = new MHCC_MRIEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Populate ddlWhoIs
            if (!(Page.IsPostBack))
            {
                //Adding it here so when the names happen to change it will change automatically from the MyRoles.cs
                List<ListItem> optionList = new List<ListItem>();
                optionList.Add(new ListItem("Admin Over All Sites", MyRoles.OverallAdmin));
                optionList.Add(new ListItem("Physician Admin", MyRoles.PhysicianAdmin));
                optionList.Add(new ListItem("Web Developer", MyRoles.WebDevelopers));

                var qryAdmins = from x in _db.Sites where (x.ADGroupAdmin != null) select x;
                if (qryAdmins.Any())
                {
                    foreach (var item in qryAdmins)
                    {
                        optionList.Add(new ListItem(item.SiteName + " - " + item.ADGroupAdmin, item.ADGroupAdmin));
                    }
                }

                var qryDataEntry = from x in _db.Sites where (x.ADGroupUserEntry != null) select x;
                if (qryDataEntry.Any())
                {
                    foreach (var item in qryDataEntry)
                    {
                        optionList.Add(new ListItem(item.SiteName + " - " + item.ADGroupUserEntry, item.ADGroupUserEntry));
                    }
                }

                var qryModifyEntry = from x in _db.Sites where (x.ADGroupModifyEntry != null) select x;
                if (qryModifyEntry.Any())
                {
                    foreach (var item in qryModifyEntry)
                    {
                        optionList.Add(new ListItem(item.SiteName + " - " + item.ADGroupModifyEntry, item.ADGroupModifyEntry));
                    }
                }

                var qrySubmissionReport = from x in _db.Sites where (x.ADGroupSubmissionReport != null) select x;
                if (qrySubmissionReport.Any())
                {
                    foreach (var item in qrySubmissionReport)
                    {
                        optionList.Add(new ListItem(item.SiteName + " - " + item.ADGroupSubmissionReport, item.ADGroupSubmissionReport));
                    }
                }

                var qryReport = from x in _db.Sites where (x.ADGroupReport != null) select x;
                if (qryReport.Any())
                {
                    foreach (var item in qryReport)
                    {
                        optionList.Add(new ListItem(item.SiteName + " - " + item.ADGroupReport, item.ADGroupReport));
                    }
                }
                
                //Sort the list for when more are added to make sure sorted.
                List<ListItem> optionListSorted = optionList.Distinct().OrderBy(x => x.Text).ToList();

                //Assign list to ddlWhoIs
                ddlWhoIs.Items.AddRange(optionListSorted.ToArray());
                ddlWhoIs.DataBind();
            }

            lblGroup.Text = string.Format("{0} {1}.", "If you need someone added to the list you will need to ask to have that person added to the Active Directory group", ddlWhoIs.SelectedValue.ToString());

            List<ListItem> groupList = new List<ListItem>();

            var myUserList = McUser.GetUsersFromGroup(ddlWhoIs.SelectedValue);
            foreach (var user in myUserList)
            {
                groupList.Add(new ListItem(user.LastFirstName + " - " + user.Username, user.Username));
            }

            List<ListItem> groupListSorted = groupList.OrderBy(x => x.Text).ToList();

            BulletedList myList = new BulletedList();
            myList.Items.AddRange(groupListSorted.ToArray());

            pnlWho.Controls.Add(myList);
        }
    }
}