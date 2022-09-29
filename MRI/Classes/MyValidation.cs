using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MRI.Classes
{
    public class MyValidation
    {
        public static bool cvText(string textToValidate)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(textToValidate)) //If blank it's ok will hit with the required field
            {
                isValid = true;
            }
            else
            {
                //Check that & < > ' ; ^ ? / | \ are not in the value
                if (textToValidate.Contains("&")
                    || textToValidate.Contains("<")
                    || textToValidate.Contains(">")
                    || textToValidate.Contains("'")
                    || textToValidate.Contains(";")
                    || textToValidate.Contains("^")
                    || textToValidate.Contains("?")
                    || textToValidate.Contains("(")
                    || textToValidate.Contains(")")
                    || textToValidate.Contains("%")
                    || textToValidate.Contains("+")
                    || textToValidate.Contains("\"")
                    || textToValidate.Contains("/")
                    || textToValidate.Contains("|")
                    || textToValidate.Contains("\\"))
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public static bool cvNumberValidate(string numberToValidate)
        {
            bool IsValid = numberToValidate.All(char.IsDigit);
            return IsValid;
        }

    }
}