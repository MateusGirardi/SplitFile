using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitFile
{
    internal class SplitFileFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public FileStream File { get; set; }

        public SplitFileFile(string name, string path, int size, FileStream file)
        {
            Name = name;
            Path = path;
            Size = size;
            File = file;
        }
    }
}
