using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Collections;
using System.Text;
using QlikAuthNet;
using ADGroupResolver;

namespace ADAuthModule
{
    public partial class Default : System.Web.UI.Page
    {
        string domainURL;
        string domainname;
        List<string> groups;
        WindowsIdentity identity;
        string userName;
        string userDN;
        bool debugmode = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                debugmode = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["debugmode"]);
                domainURL = System.Configuration.ConfigurationManager.AppSettings["domainURL"];
                domainname = System.Configuration.ConfigurationManager.AppSettings["domainname"];

                identity = WindowsIdentity.GetCurrent();

                userName = identity.Name;
                int pos = userName.IndexOf(@"\");
                if (pos > 0) userName = userName.Substring(pos + 1);

                PrincipalContext domain = new PrincipalContext(ContextType.Domain, domainname);
                UserPrincipal user = UserPrincipal.FindByIdentity(domain, IdentityType.SamAccountName, userName);

                userDN = user.DistinguishedName;

                ResolveGroups rg = new ResolveGroups();

                groups = rg.GetNestedGroupMembershipsByTokenGroup("LDAP://" + userDN, domainURL);

                if (debugmode)
                {
                    lblUser.Text = userName;

                    foreach (string group in groups)
                    {
                        lblGroups.Text += group + ", ";
                    }

                }
                else
                {
                    fetchTicketandRedirect(identity.Name, groups);
                }
            }
            catch(Exception ex)
            {
                lblerror.Text = "An error occurred attempting to log you in:  " + ex.Message;
            }
        }

        static void fetchTicketandRedirect(string username, List<string> groups)
        {
            string[] userdomainandname = username.Split('\\');

            var req = new Ticket()
            {
                UserDirectory = userdomainandname[0],
                UserId = userdomainandname[1]
            };
            req.AddGroups(groups);
            req.TicketRequest();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            fetchTicketandRedirect(identity.Name, groups);
        }
    }
}