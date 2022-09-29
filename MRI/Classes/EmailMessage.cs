using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRI.Classes
{
    public class EmailMessage
    {
        private const string xmlBuffer = "*****XML Buffer*****";
        
        private const string xmlString = "&ltRoot&gt&ltS_CASE CAS_CALLBACK=\"{0}\" CAS_IPA_PK.IPA_ID=\"User\" CAS_URG_PK.URG_ID=\"{1}\" CAS_PRI_PK.PRI_ID=\"{2}\" CAS_CAT_PK.CAT_ID=\"{3}\"/&gt&lt/Root&gt";
        
        private const string emailMessage = 
            "Contact:<br / >{0}<br /><br />" +
            "Phone:<br />{1}<br /><br />" +
            "{2}" + // Alternate phone line
            "Email:<br />{3}<br /><br />" +
            "{4}" + // Alternate email line
            "Preferred Contact Method:<br />{5}<br /><br />" + 
            "Preferred Contact Time:<br />{6}<br /><br />" +
            "Description:<br />{7}<br /><br />" +
            "{8}<br /><br />" + // xmlBuffer
            "{9}<br /><br />" + // xmlString
            "{10}"; //xmlBuffer
        
        private const string approvalMessage = "{0} has submitted an ITAR that requires your approval.  Requests can be viewed by following <a href='http://mclarennet/helpdesk/AccessRequests/Approve'>this link</a>.";

        private const string statusMessage = "{0} has {1} your ITAR request submitted on {2:d}.";

        //Changed because this is no longer going to forward soon. Also changed to getting from the web.config file since it always seems to be changing every few years.
        //public string ServiceDeskAddress = "servicedesk@antheliohealth.com";
        //public string ServiceDeskAddress = "anth-customersupport@atos.net";
        public string ServiceDeskAddress = System.Configuration.ConfigurationManager.AppSettings["ServiceDeskEMail"];

        //public string OmnicellAddress = "omnicellaccessflint@mclaren.org";
        public string OmnicellAddress = System.Configuration.ConfigurationManager.AppSettings["OmniCellEMail"];

        public string FormatXML(string caseType, string phone)
        {
            string xmlFormat = "<br/><br/>" + xmlBuffer + "<br/><br/>";
            xmlFormat += string.Format(xmlString, phone, "Medium", "3", caseType) + "<br/><br/>";
            xmlFormat += xmlBuffer;
            return xmlFormat;
        }

        public string FormatServiceDeskMessage(string caseType, string displayName, string phone, string email, string preferredMethod, string description, string preferredTime, string altPhone, string altEmail)
        {
            string xml = String.Format(xmlString, phone, "Medium", "3", caseType);
            string alternatePhone = string.Empty;
            string alternateEmail = string.Empty;
            if (!string.IsNullOrEmpty(altPhone))
            {
                alternatePhone = String.Format("Alternate Phone:<br />{0}<br /><br />", altPhone);
            }
            if (!string.IsNullOrEmpty(altEmail))
            {
                alternateEmail = String.Format("Alternate Email:<br />{0}<br /><br />", altEmail);
            }

            string msgString = String.Format(emailMessage, displayName, phone, alternatePhone, email, alternateEmail, preferredMethod, preferredTime, description, xmlBuffer, xml, xmlBuffer);

            return msgString;
        }

        public string FormatApprovalMessage(string requestor)
        {
            string msgString = String.Format(approvalMessage, requestor);

            return msgString;
        }

        public string FormatStatusNotification(string approver, string status, DateTime date)
        {
            string msgString = String.Format(statusMessage, approver, status, date);
            return msgString;
        }
    }
}