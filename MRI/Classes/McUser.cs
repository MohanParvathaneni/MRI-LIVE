using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace MRI.Classes
{
    class McUser
    {
        public Guid Guid { get; set; }

        public string DisplayName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string LastFirstName { get; set; }

        public string Manager { get; set; }

        public string MiddleName { get; set; }

        public string AlternatePhone { get; set; }

        public string DepartmentFloor { get; set; }

        public string MHCNumber { get; set; }

        public string Company { get; set; }

        public string DistinguishedName { get; set; }

        public string UserPrincipalName { get; set; }

        public bool UserEnabled { get; set; }

        //public Guid NMHGuid { get; set; }

        //Keep in lower case comparing with lowercase.
        public static string[] domainWhitelist = { "mclaren.org", "northernhealth.org", "atos.net", "cvrcnm.com", "michiganhvs.com", "mmponline.com", "porthuronhosp.org", "karmanos.org" };

        //This will be faster since it is not querying AD for the userid for every field
        public McUser GetUserByUsername(string username)
        {
            McUser user;
            try
            {
                if (username.Contains("@"))
                {
                    PrincipalContext ctx1 = new PrincipalContext(ContextType.Domain);
                    UserPrincipal uPrincipal1 = UserPrincipal.FindByIdentity(ctx1, IdentityType.UserPrincipalName, username);
                    if (uPrincipal1 != null)
                        username = uPrincipal1.SamAccountName;
                }

                DirectorySearcher dsr = new DirectorySearcher();

                if (username.Contains("\\"))
                {
                    username = username.Split('\\')[1];
                }
                if (username.Contains("@"))
                {
                    username = username.Split('@')[0];
                }
                dsr.Filter = "sAMAccountName=" + username;
                dsr.PropertiesToLoad.Add("displayname");
                dsr.PropertiesToLoad.Add("title");
                dsr.PropertiesToLoad.Add("telephoneNumber");
                dsr.PropertiesToLoad.Add("mail");
                dsr.PropertiesToLoad.Add("department");
                dsr.PropertiesToLoad.Add("sn");
                dsr.PropertiesToLoad.Add("givenname");
                dsr.PropertiesToLoad.Add("manager");
                dsr.PropertiesToLoad.Add("initials");
                dsr.PropertiesToLoad.Add("otherPhoneNumber");
                dsr.PropertiesToLoad.Add("PhysicalDelieveryOfficeName");
                dsr.PropertiesToLoad.Add("company");
                dsr.PropertiesToLoad.Add("DistinguishedName");
                //dsr.PropertiesToLoad.Add("extensionattribute9");
                dsr.PropertiesToLoad.Add("employeeid");

                DirectoryEntry result = dsr.FindOne().GetDirectoryEntry();

                user = new McUser
                {
                    Guid = new Guid(GetPropertyValueByUsername("objectGUID", username)),
                    Username = username,
                    DisplayName = TestValue(result.Properties["displayname"].Value),
                    JobTitle = TestValue(result.Properties["title"].Value),
                    Phone = TestValue(result.Properties["telephoneNumber"].Value),
                    Email = TestValue(result.Properties["mail"].Value),
                    Department = TestValue(result.Properties["department"].Value),
                    LastName = TestValue(result.Properties["sn"].Value),
                    FirstName = TestValue(result.Properties["givenname"].Value),
                    LastFirstName = string.Format("{0}, {1}", TestValue(result.Properties["sn"].Value), TestValue(result.Properties["givenname"].Value)),
                    Manager = TestValue(result.Properties["manager"].Value),
                    MiddleName = TestValue(result.Properties["initials"].Value),
                    AlternatePhone = TestValue(result.Properties["otherPhoneNumber"].Value),
                    DepartmentFloor = TestValue(result.Properties["PhysicalDelieveryOfficeName"].Value),
                    Company = TestValue(result.Properties["company"].Value),
                    DistinguishedName = TestValue(result.Properties["distinguishedName"].Value),
                    MHCNumber = TestValue(result.Properties["employeeid"].Value)
                    //NMHGuid = new Guid(GetPropertyValueByUsername("extensionattribute9", username))
                };

                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                UserPrincipal uPrincipal = UserPrincipal.FindByIdentity(ctx, username);

                if (uPrincipal != null)
                {
                    user.UserPrincipalName = uPrincipal.UserPrincipalName;
                    //Temporary putting the email here maybe permant will see
                    //user.Email = uPrincipal.UserPrincipalName;
                }

                if (string.IsNullOrWhiteSpace(user.UserPrincipalName))
                {
                    user.UserPrincipalName = "prajewski@northernhealth.org";
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    user.Email = uPrincipal.UserPrincipalName;
                }
                else if (user.Email.Contains('@'))
                {
                    if (domainWhitelist.Contains(user.Email.Split('@')[1].ToLower()))
                    {
                        //Email is in whitelist so okay
                    }
                    else
                    {
                        user.Email = uPrincipal.UserPrincipalName;
                    }
                }
                else
                {
                    user.Email = uPrincipal.UserPrincipalName;
                }

                //Just incase should never hit here though.
                //if (string.IsNullOrWhiteSpace(user.Email))
                //{
                //    //user.Email = "prajewski@northernhealth.org";
                //    user.Email = uPrincipal.UserPrincipalName;
                //}
            }
            catch (Exception)
            {
                DirectorySearcher dsr = new DirectorySearcher();

                string newUsername = "prajewski";
                dsr.Filter = "sAMAccountName=" + newUsername;
                dsr.PropertiesToLoad.Add("displayname");
                dsr.PropertiesToLoad.Add("title");
                dsr.PropertiesToLoad.Add("telephoneNumber");
                dsr.PropertiesToLoad.Add("mail");
                dsr.PropertiesToLoad.Add("department");
                dsr.PropertiesToLoad.Add("sn");
                dsr.PropertiesToLoad.Add("givenname");
                dsr.PropertiesToLoad.Add("initials");
                dsr.PropertiesToLoad.Add("otherPhoneNumber");
                dsr.PropertiesToLoad.Add("PhysicalDelieveryOfficeName");
                dsr.PropertiesToLoad.Add("company");
                dsr.PropertiesToLoad.Add("DistinguishedName");
                //dsr.PropertiesToLoad.Add("extensionattribute9");
                dsr.PropertiesToLoad.Add("employeeid");

                DirectoryEntry result = dsr.FindOne().GetDirectoryEntry();

                user = new McUser
                {
                    Guid = new Guid(GetPropertyValueByUsername("objectGUID", newUsername)),
                    Username = newUsername,
                    DisplayName = TestValue(result.Properties["displayname"].Value),
                    JobTitle = TestValue(result.Properties["title"].Value),
                    Phone = TestValue(result.Properties["telephone"].Value),
                    Email = TestValue(result.Properties["mail"].Value),
                    Department = TestValue(result.Properties["department"].Value),
                    LastName = TestValue(result.Properties["sn"].Value),
                    FirstName = TestValue(result.Properties["givenname"].Value),
                    LastFirstName = string.Format("{0}, {1}", TestValue(result.Properties["sn"].Value), TestValue(result.Properties["givenname"].Value)),
                    Manager = TestValue(result.Properties["manager"].Value),
                    MiddleName = TestValue(result.Properties["initials"].Value),
                    AlternatePhone = TestValue(result.Properties["otherPhoneNumber"].Value),
                    DepartmentFloor = TestValue(result.Properties["PhysicalDelieveryOfficeName"].Value),
                    Company = TestValue(result.Properties["company"].Value),
                    DistinguishedName = TestValue(result.Properties["distinguishedName"].Value),
                    MHCNumber = TestValue(result.Properties["employeeid"].Value)
                    //NMHGuid = new Guid(GetPropertyValueByUsername("extensionattribute9", username))
                };
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                UserPrincipal uPrincipal = UserPrincipal.FindByIdentity(ctx, newUsername);

                if (uPrincipal != null)
                {
                    user.UserPrincipalName = uPrincipal.UserPrincipalName;
                    //user.Email = uPrincipal.UserPrincipalName;
                }

                if (string.IsNullOrWhiteSpace(user.UserPrincipalName))
                {
                    user.UserPrincipalName = "prajewski@northernhealth.org";
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    user.Email = uPrincipal.UserPrincipalName;
                }
                else if (user.Email.Contains('@'))
                {
                    if (domainWhitelist.Contains(user.Email.Split('@')[1].ToLower()))
                    {
                        //Email is in whitelist so okay
                    }
                    else
                    {
                        user.Email = uPrincipal.UserPrincipalName;
                    }
                }
                else
                {
                    user.Email = uPrincipal.UserPrincipalName;
                }

                //if (string.IsNullOrWhiteSpace(user.Email))
                //{
                //    user.Email = "prajewski@northernhealth.org";
                //}
            }

            return user;
        }


        public McUser GetUserByUserGuid(Guid userguid)
        {
            McUser user = new McUser();

            DirectoryEntry result = new DirectoryEntry(String.Format("LDAP://<GUID={0}>", userguid));
                    
            user.Guid = userguid;
            user.Username = TestValue(result.Properties["sAMAccountName"].Value);
            user.DisplayName = TestValue(result.Properties["displayname"].Value);
            user.JobTitle = TestValue(result.Properties["title"].Value);
            user.Phone = TestValue(result.Properties["telephoneNumber"].Value);
            user.Email = TestValue(result.Properties["mail"].Value);
            user.Department = TestValue(result.Properties["department"].Value);
            user.LastName = TestValue(result.Properties["sn"].Value);
            user.FirstName = TestValue(result.Properties["givenname"].Value);
            //Don't know why but this was not returning the values so just having it look them up again.
            //user.LastFirstName = string.Format("{0}, {1}", LastName, FirstName);
            user.LastFirstName = string.Format("{0}, {1}", TestValue(result.Properties["sn"].Value), TestValue(result.Properties["givenname"].Value));
            user.Manager = TestValue(result.Properties["manager"].Value);
            user.MiddleName = TestValue(result.Properties["initials"].Value);
            user.AlternatePhone = TestValue(result.Properties["otherPhoneNumber"].Value);
            user.DepartmentFloor = TestValue(result.Properties["PhysicalDelieveryOfficeName"].Value);
            //user.MHCNumber = TestValue(result.Properties[""].Value);
            //user.NMHGuid = new Guid(GetPropertyValueByGuid("extensionattribute9", userguid));
            user.Company = TestValue(result.Properties["company"].Value);
            user.DistinguishedName = TestValue(result.Properties["distinguishedName"].Value);
            user.MHCNumber = (TestValue(result.Properties["employeeid"].Value));

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal uPrincipal = UserPrincipal.FindByIdentity(ctx, user.Username);

            if (uPrincipal != null)
            {
                user.UserPrincipalName = uPrincipal.UserPrincipalName;
                //user.Email = uPrincipal.UserPrincipalName;
            }

            if (string.IsNullOrWhiteSpace(user.UserPrincipalName))
            {
                user.UserPrincipalName = "prajewski@northernhealth.org";
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                user.Email = uPrincipal.UserPrincipalName;
            }
            else if (user.Email.Contains('@'))
            {
                if (domainWhitelist.Contains(user.Email.Split('@')[1].ToLower()))
                {
                    //Email is in whitelist so okay
                }
                else
                {
                    user.Email = uPrincipal.UserPrincipalName;
                }
            }
            else
            {
                user.Email = uPrincipal.UserPrincipalName;
            }

            //if (string.IsNullOrWhiteSpace(user.Email))
            //{
            //    user.Email = "prajewski@northernhealth.org";
            //}

            return user;
        }

        public string TestValue(object valueToTest)
        {
            string returnValue = string.Empty;
            try
            {
                if (valueToTest.GetType() == typeof(System.Byte[]))
                {
                    byte[] guidData = (byte[])valueToTest;
                    Guid propGuid = new Guid(guidData);

                    returnValue = propGuid.ToString();
                }
                else
                {
                    returnValue = valueToTest.ToString();
                }
            }
            catch (Exception)
            {
                returnValue = String.Empty;
            }
            return returnValue;
        }

        public McUser OldGetUserByUsername(string username)
        {
            McUser myReturnUser = new McUser();
            try
            {
                McUser user = new McUser
                {
                    Guid = new Guid(GetPropertyValueByUsername("objectGUID", username)),
                    Username = username,
                    DisplayName = GetPropertyValueByUsername("displayname", username),
                    JobTitle = GetPropertyValueByUsername("title", username),
                    Phone = GetPropertyValueByUsername("telephoneNumber", username),
                    Email = GetPropertyValueByUsername("mail", username),
                    Department = GetPropertyValueByUsername("department", username),
                    LastName = GetPropertyValueByUsername("sn", username),
                    FirstName = GetPropertyValueByUsername("givenname", username),
                    //Don't know why but this was not returning the values so just having it look them up again.
                    //LastFirstName = string.Format("{0}, {1}", LastName, FirstName),
                    LastFirstName = string.Format("{0}, {1}", GetPropertyValueByUsername("sn", username), GetPropertyValueByUsername("givenname", username)),
                    Manager = GetPropertyValueByUsername("manager", username),
                    MiddleName = GetPropertyValueByUsername("initials", username),
                    AlternatePhone = GetPropertyValueByUsername("otherPhoneNumber", username),
                    DepartmentFloor = GetPropertyValueByUsername("PhysicalDelieveryOfficeName", username),
                    Company = GetPropertyValueByUsername("company", username),
                    DistinguishedName = GetPropertyValueByUsername("distinguishedName", username)
                    //MHCNumber = GetPropertyValueByUsername("",username)
                    //NMHGuid = new Guid(GetPropertyValueByUsername("extensionattribute9",username))
                };
                myReturnUser = user;
            }
            catch
            {
                username = "prajewski";
                McUser user = new McUser
                {
                    Guid = new Guid(GetPropertyValueByUsername("objectGUID", username)),
                    Username = username,
                    DisplayName = GetPropertyValueByUsername("displayname", username),
                    JobTitle = GetPropertyValueByUsername("title", username),
                    Phone = GetPropertyValueByUsername("telephoneNumber", username),
                    Email = GetPropertyValueByUsername("mail", username),
                    Department = GetPropertyValueByUsername("department", username),
                    LastName = GetPropertyValueByUsername("sn", username),
                    FirstName = GetPropertyValueByUsername("givenname", username),
                    //Don't know why but this was not returning the values so just having it look them up again.
                    //LastFirstName = string.Format("{0}, {1}", LastName, FirstName),
                    LastFirstName = string.Format("{0}, {1}", GetPropertyValueByUsername("sn", username), GetPropertyValueByUsername("givenname", username)),
                    Manager = GetPropertyValueByUsername("manager", username),
                    MiddleName = GetPropertyValueByUsername("initials", username),
                    AlternatePhone = GetPropertyValueByUsername("otherPhoneNumber",username),
                    DepartmentFloor = GetPropertyValueByUsername("PhysicalDelieveryOfficeName",username),
                    Company = GetPropertyValueByUsername("company", username),
                    DistinguishedName = GetPropertyValueByUsername("distinguishedName", username)
                    //MHCNumber = GetPropertyValueByUsername("",username)
                    //NMHGuid = new Guid(GetPropertyValueByUsername("extensionattribute9",username))
                };
                myReturnUser = user;
            }

            return myReturnUser;

        }

        public McUser OldGetUserByUserGuid(Guid userguid)
        {
            McUser user = new McUser
            {
                Guid = userguid,
                Username = GetPropertyValueByGuid("sAMAccountName", userguid, false),
                DisplayName = GetPropertyValueByGuid("displayname", userguid),
                JobTitle = GetPropertyValueByGuid("title", userguid),
                Phone = GetPropertyValueByGuid("telephoneNumber", userguid),
                Email = GetPropertyValueByGuid("mail", userguid),
                Department = GetPropertyValueByGuid("department", userguid),
                LastName = GetPropertyValueByGuid("sn", userguid),
                FirstName = GetPropertyValueByGuid("givenname", userguid),
                //Don't know why but this was not returning the values so just having it look them up again.
                //LastFirstName = string.Format("{0}, {1}", LastName, FirstName),
                LastFirstName = string.Format("{0}, {1}", GetPropertyValueByGuid("sn", userguid), GetPropertyValueByGuid("givenname", userguid)),
                Manager = GetPropertyValueByGuid("manager", userguid),
                MiddleName = GetPropertyValueByGuid("initials", userguid),
                AlternatePhone = GetPropertyValueByGuid("otherPhoneNumber",userguid),
                DepartmentFloor = GetPropertyValueByGuid("PhysicalDelieveryOfficeName",userguid),
                Company = GetPropertyValueByGuid("company",userguid),
                DistinguishedName = GetPropertyValueByGuid("distinguishedName",userguid)
                //MHCNumber = GetPropertyValueByGuid("",userguid)
                //NMHGuid = new Guid(GetPropertyValueByGuid("extensionattribute9", userguid))
            };
            return user;
        }

        public McUser OldGetUserByNMHGuid(Guid userguid)
        {
            McUser user = new McUser
            {
                Guid = new Guid(GetPropertyValueByNMHGuid("guid", userguid)),
                Username = GetPropertyValueByNMHGuid("sAMAccountName", userguid, false),
                DisplayName = GetPropertyValueByNMHGuid("displayname", userguid),
                JobTitle = GetPropertyValueByNMHGuid("title", userguid),
                Phone = GetPropertyValueByNMHGuid("telephoneNumber", userguid),
                Email = GetPropertyValueByNMHGuid("mail", userguid),
                Department = GetPropertyValueByNMHGuid("department", userguid),
                LastName = GetPropertyValueByNMHGuid("sn", userguid),
                FirstName = GetPropertyValueByNMHGuid("givenname", userguid),
                LastFirstName = string.Format("{0}, {1}", LastName, FirstName),
                Manager = GetPropertyValueByNMHGuid("manager", userguid),
                MiddleName = GetPropertyValueByNMHGuid("initials", userguid),
                AlternatePhone = GetPropertyValueByNMHGuid("otherPhoneNumber",userguid),
                DepartmentFloor = GetPropertyValueByNMHGuid("PhysicalDelieveryOfficeName",userguid)
                //MHCNumber = GetPropertyValueByNMHGuid("",username)
                //NMHGuid = userguid
            };
            return user;
        }

        /*-------------------------------------------------------------------------*/
        private string GetPropertyValueByUsername(string property, string username, bool returnEmptyWhenNotFound = true)
        {
            string returnValue = username;

            try
            {
                DirectorySearcher dsr = new DirectorySearcher();

                if (username.Contains("\\"))
                {
                    username = username.Split('\\')[1];
                }
                if (username.Contains("@"))
                {
                    username = username.Split('@')[0];
                }
                dsr.Filter = "sAMAccountName=" + username;
                dsr.PropertiesToLoad.Add(property);

                DirectoryEntry result = dsr.FindOne().GetDirectoryEntry();
                var propValue = result.Properties[property].Value;

                if (propValue.GetType() == typeof(System.Byte[]))
                {
                    byte[] guidData = (byte[])propValue;
                    Guid guid = new Guid(guidData);

                    returnValue = guid.ToString();
                }
                else
                {
                    returnValue = propValue.ToString();
                }
            }
            catch
            {
                if (returnEmptyWhenNotFound)
                {
                    returnValue = String.Empty;
                }
            }

            return returnValue;
        }

        private string GetPropertyValueByGuid(string property, Guid guid, bool returnEmptyWhenNotFound = true)
        {
            string returnValue = guid.ToString();

            try
            {
                DirectoryEntry result = new DirectoryEntry(String.Format("LDAP://<GUID={0}>", guid));
                var propValue = result.Properties[property].Value;

                if (propValue.GetType() == typeof(System.Byte[]))
                {
                    byte[] guidData = (byte[])propValue;
                    Guid propGuid = new Guid(guidData);

                    returnValue = propGuid.ToString();
                }
                else
                {
                    returnValue = propValue.ToString();
                }
            }
            catch
            {
                if (returnEmptyWhenNotFound)
                {
                    returnValue = String.Empty;
                }
            }

            return returnValue;
        }

        private string GetPropertyValueByNMHGuid(string property, Guid guid, bool returnEmptyWhenNotFound = true)
        {
            string returnValue = guid.ToString();

            try
            {
                DirectoryEntry result = new DirectoryEntry(String.Format("LDAP://<extensionattribute9={0}>", guid));
                var propValue = result.Properties[property].Value;

                if (propValue.GetType() == typeof(System.Byte[]))
                {
                    byte[] guidData = (byte[])propValue;
                    Guid propGuid = new Guid(guidData);

                    returnValue = propGuid.ToString();
                }
                else
                {
                    returnValue = propValue.ToString();
                }
            }
            catch
            {
                if (returnEmptyWhenNotFound)
                {
                    returnValue = String.Empty;
                }
            }

            return returnValue;
        }

        public static IEnumerable<McUser> GetUsersFromGroup(string group, string searchExpression = "")
        {
            List<McUser> results = new List<McUser>();

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "NMH");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            GroupPrincipal searchGroup = GroupPrincipal.FindByIdentity(ctx, group);
            if (searchGroup != null)
            {
                foreach (var user in searchGroup.Members.Where(x => x.Name.Contains(searchExpression)))
                {
                    UserPrincipal theUser = user as UserPrincipal;
                    if (theUser.Enabled == true)
                    {
                        //Put the try in so incase someone adds a group in it will just ignore it
                        try
                        {
                            McUser myUser = new McUser();
                            myUser.Guid = new Guid(user.Guid.ToString());
                            myUser.DisplayName = user.DisplayName;
                            myUser.Username = user.SamAccountName;
                            myUser.JobTitle = user.Description;
                            myUser.FirstName = theUser.GivenName;
                            myUser.LastName = theUser.Surname;
                            myUser.LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName);
                            myUser.Phone = theUser.VoiceTelephoneNumber;
                            myUser.Email = theUser.EmailAddress;
                            myUser.MiddleName = theUser.MiddleName;
                            myUser.DistinguishedName = theUser.DistinguishedName;
                            myUser.UserPrincipalName = theUser.UserPrincipalName;
                            myUser.MHCNumber = theUser.EmployeeId;

                            if (string.IsNullOrWhiteSpace(myUser.Email))
                            {
                                myUser.Email = theUser.UserPrincipalName;
                            }
                            else if (myUser.Email.Contains('@'))
                            {
                                if (domainWhitelist.Contains(myUser.Email.Split('@')[1].ToLower()))
                                {
                                    //Email is in whitelist so okay
                                }
                                else
                                {
                                    myUser.Email = theUser.UserPrincipalName;
                                }
                            }
                            else
                            {
                                myUser.Email = theUser.UserPrincipalName;
                            }

                            results.Add(myUser);

                            //Changed so I could do the if part on the email to make sure if on the list or take the UPN
                            //results.Add(new McUser
                            //{
                            //    Guid = new Guid(user.Guid.ToString()),
                            //    DisplayName = user.DisplayName,
                            //    Username = user.SamAccountName,
                            //    JobTitle = user.Description,
                            //    FirstName = theUser.GivenName,
                            //    LastName = theUser.Surname,
                            //    LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName),
                            //    Phone = theUser.VoiceTelephoneNumber,
                            //    Email = theUser.EmailAddress,
                            //    //Email = theUser.UserPrincipalName,
                            //    MiddleName = theUser.MiddleName,
                            //    DistinguishedName = theUser.DistinguishedName,
                            //    UserPrincipalName = theUser.UserPrincipalName
                            //});
                        }
                        catch
                        {
                        };
                    }
                }
            }

            return results;
        }

        public static IEnumerable<McUser> GetUsersFromSearch(string searchExpression)
        {
            List<McUser> results = new List<McUser>();

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "NMH", "OU=User, DC=NMH, DC=NMRHS, DC=NET");
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "McLaren", "OU=MNM, OU=People, DC=Mclaren, DC=Org");
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "McLaren", "OU=People, DC=Mclaren, DC=Org");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            UserPrincipal searchUser = new UserPrincipal(ctx);
            string searchString = String.Empty;
            if (searchExpression.Length > 0)
            {
                searchString = String.Format("*{0}*", searchExpression);
            }
            else
            {
                searchString = "*";
            }

            searchUser.DisplayName = searchString;
            PrincipalSearcher srch = new PrincipalSearcher(searchUser);

            foreach (var match in srch.FindAll())
            {
                UserPrincipal theUser = match as UserPrincipal;

                McUser myUser = new McUser();
                myUser.Guid = new Guid(match.Guid.ToString());
                myUser.DisplayName = match.DisplayName;
                myUser.Username = match.SamAccountName;
                myUser.JobTitle = match.Description;
                myUser.FirstName = theUser.GivenName;
                myUser.LastName = theUser.Surname;
                myUser.LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName);
                myUser.Phone = theUser.VoiceTelephoneNumber;
                myUser.Email = theUser.EmailAddress;
                myUser.MiddleName = theUser.MiddleName;
                myUser.DistinguishedName = theUser.DistinguishedName;
                myUser.UserPrincipalName = theUser.UserPrincipalName;
                myUser.MHCNumber = theUser.EmployeeId;

                if (string.IsNullOrWhiteSpace(myUser.Email))
                {
                    myUser.Email = theUser.UserPrincipalName;
                }
                else if (myUser.Email.Contains('@'))
                {
                    if (domainWhitelist.Contains(myUser.Email.Split('@')[1].ToLower()))
                    {
                        //Email is in whitelist so okay
                    }
                    else
                    {
                        myUser.Email = theUser.UserPrincipalName;
                    }
                }
                else
                {
                    myUser.Email = theUser.UserPrincipalName;
                }

                results.Add(myUser);

                //Changed so I could do the check on the email field
                //results.Add(new McUser
                //{
                //    Guid = new Guid(match.Guid.ToString()),
                //    DisplayName = match.DisplayName,
                //    Username = match.SamAccountName,
                //    JobTitle = match.Description,
                //    FirstName = theUser.GivenName,
                //    LastName = theUser.Surname,
                //    LastFirstName = string.Format("{0}, {1}", theUser.Surname, theUser.GivenName),
                //    Phone = theUser.VoiceTelephoneNumber,
                //    //Email = theUser.EmailAddress,
                //    Email = theUser.UserPrincipalName,
                //    MiddleName = theUser.MiddleName,
                //    DistinguishedName = theUser.DistinguishedName,
                //    UserPrincipalName = theUser.UserPrincipalName
                //});
            }

            return results;
        }
    }
}
