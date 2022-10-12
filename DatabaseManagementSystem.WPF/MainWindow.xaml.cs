using DatabaseManagementSystem.BL.DatabaseEntities;
using DatabaseManagementSystem.BL.DataServices;
using DatabaseManagementSystem.BL.FileManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace DatabaseManagementSystem.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDatabaseService _databaseService;
        private IDatabaseFileManager _databaseFileManager;

        public MainWindow(IDatabaseService databaseService, IDatabaseFileManager databaseFileManager)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _databaseFileManager = databaseFileManager;
            ToggleButtonsDatabaseStatusChanged();
            ToggleButtonsTableStatusChanged();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new List<Dog>();
            items.Add(new Dog("Fido", 10));
            items.Add(new Dog("Spark", 20));
            items.Add(new Dog("Fluffy", 4));
            var grid = sender as DataGrid;
            grid.ItemsSource = items;
        }

        private void BtnCreateDb_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog("Enter database name", @"[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+");
            if(dialog.ShowDialog() == true)
            {
                string databaseName = dialog.ResposneText;
                _databaseService.Database = new Database(databaseName);
                ToggleButtonsDatabaseStatusChanged();
                LblDatabaseName.Content = databaseName;
                _databaseFileManager.SaveDatabase(_databaseService.Database);
                TablesListBox.DataContext = _databaseService.Database?.Tables;
            }
        }

        private void ToggleButtonsDatabaseStatusChanged()
        {
            BtnDeleteDb.IsEnabled = !BtnDeleteDb.IsEnabled;
            BtnAddTable.IsEnabled = !BtnAddTable.IsEnabled;
            BtnDeleteTable.IsEnabled = !BtnDeleteTable.IsEnabled;
        }

        private void ToggleButtonsTableStatusChanged()
        {
            BtnAddField.IsEnabled = !BtnAddField.IsEnabled;
            BtnDeleteField.IsEnabled = !BtnDeleteField.IsEnabled;
        }

        private void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog("Enter table name", @"[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+");
            if(dialog.ShowDialog() == true)
            {
                string tableName = dialog.ResposneText;

            }
        }
    }

    public class Dog
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public Dog(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }


}
