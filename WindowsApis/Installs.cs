using Microsoft.Win32;
using System;
using System.Linq;

namespace WindowsApis
{
    public static class Installs
    {
        public static bool IsAppInstalled(string programName)
        {
            bool result = false;
            string uninstallKey1 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string uninstallKey2 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            result |= CheckInAddress(uninstallKey1, RegistryHive.LocalMachine, programName);
            result |= CheckInAddress(uninstallKey1, RegistryHive.CurrentUser, programName);
            result |= CheckInAddress(uninstallKey2, RegistryHive.LocalMachine, programName);
            return result;
        }
        static bool CheckInAddress(string address, RegistryHive hive, string programName)
        {
            var view = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            var localKey = RegistryKey.OpenBaseKey(hive, view).OpenSubKey(address);

            foreach (var subKey in localKey.GetSubKeyNames().Select(keyName => localKey.OpenSubKey(keyName)))
            {
                if (subKey.GetValue("DisplayName") is string displayName && displayName.Contains(programName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
