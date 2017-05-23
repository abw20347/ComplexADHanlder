using System;
using System.Collections.Generic;
using ADGroupResolver;

namespace TestLDAPquery
{
    class Program
    {
        static void Main(string[] args)
        {

            ResolveGroups rg = new ResolveGroups();

            List<string> groups;

            if (args.Length == 0)
            {
                groups = rg.GetNestedGroupMembershipsByTokenGroup("LDAP://CN=Joe Bickley,OU=Users,OU=United Kingdom,DC=qliktech,DC=com", "GC://DC=qliktech,DC=com");           
            }
            else
            {
                groups = rg.GetNestedGroupMembershipsByTokenGroup(args[0], args[1]);
            }


            // Print results...

            Console.WriteLine("Groups found: " + groups.Count); 

            foreach (string group in groups)
            {
                Console.WriteLine(group);
            }

            Console.ReadKey();
        }

        

    }
}
