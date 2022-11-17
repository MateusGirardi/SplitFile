using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SplitFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NameScope.SetNameScope(this, new NameScope());
            RegisterTextBox("TextBoxOriginFileName", TextBoxOriginFileName);
            RegisterTextBox("TextBoxDestinyPath", TextBoxDestinyPath);

            ButtonExecute.Click += ButtonExecute_Click;

            //this.DataContext = this;

        }

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            Processor processor = new Processor(TextBoxOriginFileName.Text, Int32.Parse(TextBoxPartsQuantity.Text)
                ,(bool)CheckBoxMantainFirstLine.IsChecked, TextBoxDestinyPath.Text, TextBlockLogs);
            processor.Write();

            string[] listaNomes = System.IO.Directory.GetFiles(TextBoxDestinyPath.Text);
            ListViewArchieves.ItemsSource = listaNomes;
        }

        private void TextBoxPartsQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text) == false
                && 
                ( (TextBoxPartsQuantity.Text == null ? 0 : TextBoxPartsQuantity.Text.Length) > 2) == false ? false : true;
        }

        void RegisterTextBox(string textBoxName, TextBox textBox)
        {
            if ((TextBox)this.FindName(textBoxName) != null)
                this.UnregisterName(textBoxName);
            this.RegisterName(textBoxName, textBox);
        }
    }
}
