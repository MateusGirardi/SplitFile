using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SplitFile
{
    internal class Processor
    {
        private string OriginPath = string.Empty;
        private int PartsQuantity = 0;
        private bool MaintainFirstLine = false;
        private string DestinyPath = string.Empty;
        private string FirstLine;
        private TextBlock outPutMessage;

        public Processor(string oP, int pQ, bool fL, string dP,TextBlock oPM)
        {
            OriginPath = oP;
            PartsQuantity = pQ;
            MaintainFirstLine = fL;
            DestinyPath = dP;
            outPutMessage = oPM;
        }
        private IEnumerable<string> Read(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }

        }
        public void Write()
        {
            decimal numberOfLines = FileHandler.GetNumberOfLines(OriginPath);
            decimal rest = ( (numberOfLines / PartsQuantity) - ( (int)(numberOfLines / PartsQuantity) ) ) * PartsQuantity;
            int linesByParts = (int)(numberOfLines / PartsQuantity);
            int parts = PartsQuantity;
            bool firstLine = MaintainFirstLine;
            SplitFileFile file = FileHandler.GenerateFile(DestinyPath); ;

            int index = 1;
            foreach(string l in Read(OriginPath))
            {
                if (FirstLine == null)
                {
                    FirstLine = l;
                }

                try
                {
                    file.File.Write(Encoding.UTF8.GetBytes(l), 0, Encoding.UTF8.GetByteCount(l));
                    file.File.Write(Encoding.UTF8.GetBytes(Environment.NewLine), 0, Encoding.UTF8.GetByteCount(Environment.NewLine));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString()); 
                }

                if( (parts > 1 && index == linesByParts)
                    || ( parts == 1 && index == (linesByParts + rest) ) )
                {
                    index = 0;
                    parts--;
                    file.File.Close();
                }

                if(parts <= 0)
                {
                    Console.WriteLine("teste");
                }

                index++;

                if (index == 1 && parts > 0)
                {
                    file = FileHandler.GenerateFile(DestinyPath);
                    if(MaintainFirstLine == true)
                    {
                        file.File.Write(Encoding.UTF8.GetBytes(FirstLine), 0, Encoding.UTF8.GetByteCount(FirstLine));
                        file.File.Write(Encoding.UTF8.GetBytes(Environment.NewLine), 0, Encoding.UTF8.GetByteCount(Environment.NewLine));
                    }
                }
            }
        }
    }
}
