using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;


namespace ADGroupResolver
{
    public class ResolveGroups
    {
        public List<string> GetNestedGroupMembershipsByTokenGroup(string userDN, string domainroot)
        {

            List<string> nestedGroups = new List<string>();

            DirectoryEntry userEnrty = new DirectoryEntry(domainroot);

            userEnrty.Path = userDN;

            // Use RefreshCach to get the constructed attribute tokenGroups.
            userEnrty.RefreshCache(new string[] { "tokenGroups" });

            StringBuilder sb = new StringBuilder();

            //we are building an ’|’ clause
            sb.Append("(|");


            foreach (byte[] sid in userEnrty.Properties["tokenGroups"])
            {
                //append each member into the filter
                sb.AppendFormat("(objectSid={0})", BuildFilterOctetString(sid));
            }

            //end our initial filter
            sb.Append(")");


            DirectoryEntry searchRoot = new DirectoryEntry(domainroot);

            DirectorySearcher ds = new DirectorySearcher(searchRoot, sb.ToString());
            ds.PropertiesToLoad.Add("samAccountName");
            ds.ReferralChasing = ReferralChasingOption.All;

            SearchResultCollection src = ds.FindAll();

            foreach (SearchResult sr in src)
            {
                //Here is each group now...
                nestedGroups.Add(sr.Properties["samAccountName"][0].ToString() + "   -   " + sr.Path.ToString());
            }


            return nestedGroups;
        }

        private string BuildFilterOctetString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.AppendFormat(
                    "\\{0}",
                    bytes[i].ToString("X2")
                    );
            }
            return sb.ToString();
        }
    }
}
