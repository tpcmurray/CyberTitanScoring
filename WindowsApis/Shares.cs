using System.Management;

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

        public static bool CreateShare(string shareName, string sharePath, ShareType shareType)
        {
            ManagementClass managementClass = new ManagementClass("Win32_Share");
            ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
            inParams["Name"] = shareName;
            inParams["Path"] = sharePath;
            inParams["Type"] = shareType;
            managementClass.InvokeMethod("Create", inParams, null);

            return true;
        }
        public static bool DeleteShare(string shareName, string sharePath)
        {
            ManagementClass shares = new ManagementClass("Win32_Share", new ObjectGetOptions());
            foreach (var o in shares.GetInstances())
            {
                var share = (ManagementObject)o;
                if (share["Name"].ToString().ToLower() == shareName.ToLower())
                {
                    share.InvokeMethod("delete", null, null);
                    break;
                }
            }

            return true;
        }
    }

    public enum ShareType : uint
    {
        DISK_DRIVE = 0x0,
        PRINT_QUEUE = 0x1,
        DEVICE = 0x2,
        IPC = 0x3,
        DISK_DRIVE_ADMIN = 0x80000000,
        PRINT_QUEUE_ADMIN = 0x80000001,
        DEVICE_ADMIN = 0x80000002,
        IPC_ADMIN = 0x8000003
    }
}
