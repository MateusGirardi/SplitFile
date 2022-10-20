using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SplitFile
{
    internal class SplitFileButton : Button
    {
        public string Type { get; set; }
        public TextBox TextBox { get; set; }

        public SplitFileButton()
        {

        }

        public void Execute()
        {
            if (Type == null) { throw new ArgumentNullException("Type must not be null!"); }
            else
            {
                Type = Type.ToString();
                if (Type == "TextDocuments")
                {
                    if (TextBox == null) { throw new ArgumentNullException("TextBox must not be null!"); }
                    Click += SplitFileButtonTextDocumentsDialog_Click;
                }
            }
        }

        private void SplitFileButtonTextDocumentsDialog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialogTextDocuments(TextBox);
        }

        public void OpenFileDialogTextDocuments(TextBox textBox)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt,.csv)|*.txt;*.csv";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                textBox.Text = filename;
            }
        }

    }
}
