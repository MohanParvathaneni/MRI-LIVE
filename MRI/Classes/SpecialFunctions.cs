using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using MRI.Entities;
using System.Web.UI.WebControls;

namespace MRI.Classes
{
    public class SpecialFunctions
    {
        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);

        public static bool IsManager(List<ListItem> managerList)
        {
            bool myReturn = false;
            McUser me = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);
            if (managerList.Exists(r => r.Value == (me.Username.Contains("\\") ? me.Username.Split('\\')[1] : me.Username)))
            {
                myReturn = true;
            }
            return myReturn;
        }

        public static string emailAddressMessage = "Your email address is not defined in Active Directory. Please work with National Service Desk to get this defined. MRI will not work for sending emails without this.";
    }
}