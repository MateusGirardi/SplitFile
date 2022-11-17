using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitFile
{
    internal static class FileHandler
    {
        public static int FileIndex = 1;

        static FileHandler()
        {

        }
        private static int NextFileIndex()
        {
            return FileIndex++;
        }
        public static string GetFileName()
        {
            return "_SplitFile" + "_part-" + NextFileIndex() + "_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt";
        }
        public static SplitFileFile GenerateFile(string destinyPath)
        {
            string fileName = GetFileName();
            string path = Path.Combine(destinyPath, fileName);
            int size = 0;
            FileStream file = File.Create(path);

            return new SplitFileFile(fileName, path, size, file);
        }
        public static int GetNumberOfLines(string originPath)
        {
            using(StreamReader sr = new StreamReader(originPath)){
                int c = 0, count = 1;
                while ((c = sr.Read()) != -1)
                {
                    if (c == '\n')
                    {
                        count++;
                    }
                }
                return count;
            }
        }
    }
}
