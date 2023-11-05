using System.Collections.Generic;
using System.IO;

namespace WindowsApis
{
    public static class Files
    {
        public static bool IsFileInExistence(string fileAndPath)
        {
            return File.Exists(fileAndPath);
        }

        public static List<string> ReadInTextFile(string filePath)
        {
            List<string> lines = new List<string>();
            StreamReader reader = File.OpenText(filePath);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines;
        }
    }
}
