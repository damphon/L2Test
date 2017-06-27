# L2Test - Source available at HTTP://github.com/damphon/L2Test
###########################################################################
########How to install on a new server using the Install.zip folder########
###########################################################################
#Setup a Windows Server with the following parameters:
##Windows Server 2012 or newer
##Minimum of 30GB HDD space
##Minimum of 2GB Ram
#
#Install IIS: 
#	Open the Software Center
#	Install GoDaddy-IIS-Standard
#
#Unzip the Install.zip file and move the contents onto the server:
#	Move the HTML folder to the root directory of the C drive
#	Right click the ‘HTML’ folder and select ‘Properties’
#	Select ‘Security’ then click ‘Edit’
#	Select ‘Users’ and check ‘Full Control’
#	Click 'OK' then click 'OK'
#	Move 'SQLEXPR_x86_ENU.exe', 'SQLEXPR_x86_ENU.exe', and 'InstallDB.BAK' onto the server.
#
#Install Microsoft SQL:	
#Run 'SQLEXPR_x86_ENU.exe'
#Leave the default directory and click 'Ok'
#Select 'New SQL Server stand-alone installation'
#Do not check for updates, click 'Next'
#Check 'I accept the license terms.' Then click 'Next'
#Click 'Next', click 'Next', click 'Next', now click 'Next', then finally, click 'Close'.
#
#Install Microsoft SQL Server Management Studio:
#Run 'Setup-ENU.exe'
#Click 'Install' and reboot when the installation process completes.
#
#Create Database:
#	Open Microsoft SQL Server Management Studio
#	Use windows authentication and click 'Connect'
#	Expand 'Security' then right click 'Logins' and select 'New Login'
#	Select 'SQL Server authentication'
#	Enter a login name and password for the database (do not use your personal 	password)
#	Un-check 'Enforce password expiration'
#	Click 'OK'
#	Right click 'Databases' and select 'New Database…'
#	Enter "L2Test" as the database name
#	Click the '…' next to 'Owner', click 'Browse…' and select the login name you created.
#	Click 'OK', 'OK', 'OK'.  There should be a new database under 'Databases'
#
#Import Database:
#	Right click on the L2Test database and select 'Tasks -> Restore -> Database'
#	Under 'Source' select 'Device'
#	Click the browse button, make sure that ‘Backup Media Type' is listed as 'File'
#	Click Add and browse to 'InstallDB.BAK'
#	Click on 'Options' and select 'Overwrite the existing database'
#	Click the OK button, you should get a message saying the restore was successful.
#
#Database Permissions:
#	Right click on the L2Test database and select 'Properties'
#	Click 'Permissions' then click 'View Server Permissions”'
#	Select the user you created, grant all permissions
#	Click 'OK', then click 'OK'
#	Right click the server name and select 'Properties'
#	Click 'Security' then select to use 'SQL authentication'
#	Click ‘OK’, ‘OK’ 	
#Close Microsoft SQL Server Management Studio and reboot the server.
#
#Update Connection String:
#	Open the HTML folder that is in the root directory
#	Open 'Web.Config' with Wordpad
#	Find the following two lines of code:
#
#<add name="L2TestConnection" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=L2Test;Persist Security Info=True;User ID=temp;Password=Password1!" providerName="System.Data.SqlClient" />
#<add name="Restore" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=Master;Persist Security Info=True;User ID=temp;Password=Password1!" providerName="System.Data.SqlClient" />
#
#Change ‘temp’ and ‘Password1!’ to the username and password you created.
#Save and close.
#
#Set Up IIS:
#	Open Server Manager
#	Select 'Tools' > 'Internet Information Services (IIS) Manager'
#	Click on the dropdown next to your server name and click 'Sites'
#	Select 'Default' and hit delete to remove the default site.
#	Click 'Add Website'
#	Add a site name 'L2Test'
#	Under 'Physical path' select the HTML folder on the C drive. 
#	Click 'OK' 
#
#Complete:
#	You can now log in with the temporary user 'admin@admin.com / Password1!'
####################################################################################################################
########How to re-install on an existing server using the Install.zip folder and your most recent backup.zip########
####################################################################################################################
#Unzip the Install.zip file and move the contents onto the server:
#	Move the HTML folder to the root directory of the C drive
#	Right click the ‘HTML’ folder and select ‘Properties’
#	Select ‘Security’ then click ‘Edit’
#	Select ‘Users’ and check ‘Full Control’
#	Click 'OK' then click 'OK'
#
#Unzip the Backup.zip file and move the contents onto the server:
#	Move the contents of the ‘Graded’ folder to C:/HTML/Views/Tests/Graded
#	Move the contents of the ‘Ungraded’ folder to C:/HTML/Views/Tests/Ungraded
#	Move the contents of the ‘Images’  folder to C:/HTML/Content/Images
#	Move the .BAK database backup file to any location on the server
#
#Import Database:
#	Open Microsoft SQL Server Management Studio
#	Use Windows authentication and click 'Connect'
#	Right click on the L2Test database and select 'Tasks -> Restore -> Database'
#	Under 'Source' select 'Device'
#	Click the browse button, make sure that ‘Backup Media Type' is listed as 'File'
#	Click 'Add' and browse to your ‘.BAK' file.
#	Click on 'Options' and select 'Overwrite the Existing Database'
#	Click the OK button, you should get a message saying the restore was successful.
#