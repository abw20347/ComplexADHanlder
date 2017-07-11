# ComplexADHanlder for Qlik Sense

This is a test project for administrators of Qlik Sense Server

Qlik Sense has some built in capabilities for handling Active Directory authentication and handling of AD groups for permissions.  There are however some limitations in handling complex domains where there are several sub domains.  For example if a user is in DomainA.corp.com and belongs to a group in DomainB.corp.com the group data is not retrieved by the UDC.

This project is testing out an alternative of querying the AD via the Global Catalog and using Token groups to get a full group resoltion.  There is a command line tool for testing, and the start of a login module (and eventually a UDC connector)

In the project is...

ADGroupResolver - the core function to take a users DN, query the Global catalog for it, resolve its groups and nested groups via the Token Group feature and then return all of the group names.

ADAuthModule - a Login module for Qlik Sense that you can hook into a virtual proxy,  will authenticate the user and post the groups via session attributes on a web ticket to log the user in.  (borrows "QlikAuthNet" from Rikard Brathen for the ticket handling)

CommandLineTester - A basic command line tool to test ADGroupResolver standalone (ideal for testing)

Download the code, build and from the command line run the following subbing in a full DN for a user and the URL for the global catalog.  The exe will need to be executed as a user of the domain and should return a list of all the users groups to the console.

CommandLineTester.exe "LDAP://CN=Joe Bickley,OU=Users,OU=United Kingdom,DC=qliktech,DC=com", "GC://DC=qliktech,DC=com"




UDC to come soon..
