using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApis
{
    public static class Shares
    {
        public static bool IsShareExisting(string shareName)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                string.Format("select * from win32_share where Name=\"{0}\"", shareName));
            var shares = searcher.Get();
            return shares.Count > 0;
        }
    }
}
