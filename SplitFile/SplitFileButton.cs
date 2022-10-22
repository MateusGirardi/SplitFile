using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;

namespace SplitFile
{
    internal class SplitFileButton : Button
    {
        public string Type { get; set; }
        public TextBox TextBox { get; set; }

        private enum Types
        {
            Path,
            TextDocuments,
            Button
        }

        public SplitFileButton()
        {

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Execute();
        }

        private void Execute()
        {
            TextBox = (TextBox)this.FindName((string)Tag);
            CheckParameters(Type, TextBox);

            if (Type == "TextDocuments")
            {
                Click += SplitFileButtonTextDocumentsDialog_Click;
            }
            else if (Type == "Path")
            {
                Click += SplitFileButtonPathDialog_Click;
            }
            
        }

        private void CheckParameters(string Type,TextBox textbox)
        {
            CheckEnum(Type);
            if (Type != Enum.GetName(Types.Button))
            {
                if (TextBox == null) { throw new ArgumentNullException("TextBox must not be null!"); }
            }
        }

        private void CheckEnum(string Type)
        {
            bool valid = false;
            foreach (string s in Enum.GetNames(typeof(Types)))
            {
                if (Type == s)
                {
                    valid = true; break;
                }
            }
            if (!valid) { throw new ArgumentException("Invalid type!"); }
        }

        private void SplitFileButtonPathDialog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new WinForms.FolderBrowserDialog();
            dialog.Description =
                "Select the directory that you want to use as the default.";

            dialog.ShowNewFolderButton = true;
            dialog.RootFolder = Environment.SpecialFolder.Personal;

            WinForms.DialogResult result = dialog.ShowDialog();
            if (result == WinForms.DialogResult.OK)
            {
                string folderName = dialog.SelectedPath;
                TextBox.Text = folderName;
            }
        }

        private void SplitFileButtonTextDocumentsDialog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt,.csv)|*.txt;*.csv";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                TextBox.Text = filename;
            }
        }

    }
}
