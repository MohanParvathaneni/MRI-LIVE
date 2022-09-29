using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRI.Classes
{
    public class MyRoles
    {
        public static string WebDevelopers = @"Intranet_Developers";
        public static string OverallAdmin = @"Intranet_MRI_Admin";
        public static string PhysicianAdmin = @"Intranet_MRI_PhysicianAdmin";
        
        //This will be for multiple sites users can be defined in more than one site as an Admin.
        //public static string SiteAdmins = @"Intranet_MRI_SiteName_Admins";

        //This will be for multiple sites users can be defined in more than one site as a Data Entry User.
        //public static string SiteDataEntryUsers = @"Intranet_MRI_SiteName_DataEntryUsers";
    }
}