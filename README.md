# ComplexADHanlder for Qlik Sense

This is project for administrators of Qlik Sense Server

Qlik Sense has some built in capabilities for handling Active Directory authentication and handling of AD groups for permissions.  There are however some limitations in handling complex domains where there are several sub domains.  For example if a user is in DomainA.corp.com and belongs to a group in DomainB.corp.com the group data is not retrieved by the UDC.

This project is trying out a querying the AD via the Global Cache and Token groups to get a full resoltion and combining the output into both a custom login module and a UDC connector.

In the project is...

ADGroupResolver - the core function to take a users DN, query the Global catalog for it, resolve its groups and nested groups via the Token Group feature and then return all of the group names.

ADAuthModule - a Login module for Qlik Sense that you can hook into a virtual proxy,  will authenticate the user and post the groups via session attributes on a web ticket to log the user in.  (borrows "QlikAuthNet" from Rikard Brathen for the ticket handling)

CommandLineTester - A basic command line tool to test ADGroupResolver standalone (ideal for testing)

UDC to come soon..
