using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApis
{
    public static class Files
    {
        public static bool IsFileInExistence(string fileAndPath)
        {
            return File.Exists(fileAndPath);
        }
    }
}
