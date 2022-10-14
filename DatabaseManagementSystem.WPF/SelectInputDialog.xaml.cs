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
    /// Interaction logic for SelectInputDialog.xaml
    /// </summary>
    public partial class SelectInputDialog : Window
    {
        private List<string> _databaseNames;
        public SelectInputDialog(List<string> databaseNames)
        {
            InitializeComponent();
            _databaseNames = databaseNames;
            ListBoxDatabases.DataContext = _databaseNames;
        }

        public string SelectedDatabaseName => ListBoxDatabases.SelectedItem?.ToString();

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxDatabases.SelectedItem == null)
            {
                MessageBox.Show("Select database");
                return;
            }
            DialogResult = true;
        }
    }
}
