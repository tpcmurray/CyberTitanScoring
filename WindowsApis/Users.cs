using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

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
    }
}
