using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsApis
{
    public static class Users
    {
        public static bool IsUserExisting(string username)
        {
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(
                string.Format(@"SELECT * FROM Win32_UserAccount WHERE Name LIKE '{0}'", username));
            ManagementObjectCollection users = usersSearcher.Get();
            return users.Count > 0;
        }

        public static bool IsUserDisabled(string username)
        {
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(
                string.Format(@"SELECT * FROM Win32_UserAccount WHERE Name LIKE '{0}' AND Disabled = true", username));
            ManagementObjectCollection users = usersSearcher.Get();
            return users.Count > 0;
        }

        public static bool IsUserAdmin(string username)
        {
            var localDomain = Environment.MachineName;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                string.Format("select * from Win32_GroupUser where " +
                "groupcomponent='Win32_Group.Domain=\"{0}\",Name=\"Administrators\"' and " +
                "partcomponent='Win32_UserAccount.Domain=\"{0}\",Name=\"{1}\"'", localDomain, username));
            var users = searcher.Get();
            return users.Count > 0;
        }

        public static bool CreateNonAdminUser(string username, string password)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry NewUser = AD.Children.Add(username, "user");
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", "Cyber Titan User" });
                NewUser.CommitChanges();

                DirectoryEntry grp;
                grp = AD.Children.Find("Users", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
            } 
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public static bool CreateAdminUser(string username, string password)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry NewUser = AD.Children.Add(username, "user");
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", "Cyber Titan User" });
                NewUser.CommitChanges();

                DirectoryEntry grp;
                grp = AD.Children.Find("Users", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
                grp = AD.Children.Find("Administrators", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
            } 
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public static bool DeleteUser(string username)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntries MyEntries = AD.Children;
                DirectoryEntry User = MyEntries.Find(username, "user");
                MyEntries.Remove(User);
            } 
            catch
            {
                return false;
            }
            return true;
        }
    }
}
