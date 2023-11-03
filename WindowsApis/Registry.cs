using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApis
{
    public static class Registry
    {
        public static string GetRegistryKey(string keyPath, string name)
        {
            var registryValue = Microsoft.Win32.Registry.GetValue(keyPath, name, "");
            return registryValue.ToString();
        }
    }
}
