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
        private bool FirstLine = false;
        private string DestinyPath = string.Empty;
        private TextBlock outPutMessage;

        public Processor(string oP, int pQ, bool fL, string dP,TextBlock oPM)
        {
            OriginPath = oP;
            PartsQuantity = pQ;
            FirstLine = fL;
            DestinyPath = dP;
            outPutMessage = oPM;
        }

        public void Read()
        {
            try
            {
                using (StreamReader sr = new StreamReader(OriginPath))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        outPutMessage.Text = line;
                        break;

                    }
                }
            }
            catch (Exception ex)
            {
                outPutMessage.Text = ex.Message;   
            }
        }
    }
}
