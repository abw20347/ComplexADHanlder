using System;
using System.Collections.Generic;
using ADGroupResolver;

namespace CommandLineTester
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                ResolveGroups rg = new ResolveGroups();

                List<string> groups = new List<string>();


                if (args.Length == 0)
                {
                    throw new Exception("No parameters specified to search with");
                    //groups = rg.GetNestedGroupMembershipsByTokenGroup("LDAP://CN=Joe Bickley,OU=Users,OU=United Kingdom,DC=qliktech,DC=com", "GC://DC=qliktech,DC=com");
                    //groups = rg.GetNestedGroupMembershipsByTokenGroup(@"LDAP://CN=Avcin\, Mića,OU=ZA,OU=Test Users,DC=serverunit,DC=performance", "GC://10.76.128.46");
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
            }
            catch(Exception ex)
            {
                Console.WriteLine("An Error Occurred:   " + ex.Message);
            }
            Console.ReadKey();
        }

        

    }
}
