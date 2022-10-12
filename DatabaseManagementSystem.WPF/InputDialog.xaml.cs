using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace DatabaseManagementSystem.WPF
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        private string _pattern;

        public InputDialog(string description, string pattern="")
        {
            InitializeComponent();
            DescriptionTextBlock.Text = description;
            _pattern = pattern;
        }

        public string ResposneText
        {
            get => ResponseTextBox.Text;
            set => ResponseTextBox.Text = value;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Regex rx = new Regex(_pattern);
            if(!rx.IsMatch(ResposneText))
            {
                MessageBox.Show("Invalid input");
                return;
            }
            DialogResult = true;
        }
    }
}
