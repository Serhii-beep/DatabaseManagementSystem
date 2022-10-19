using DatabaseManagementSystem.BL.DatabaseEntities;
using DatabaseManagementSystem.BL.DataServices;
using DatabaseManagementSystem.BL.Fields;
using DatabaseManagementSystem.BL.FileManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using Table = DatabaseManagementSystem.BL.DatabaseEntities.Table;

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
            DisableButtonsDatabase();
            DisableButtonsTable();
            TableDataGrid.CanUserReorderColumns = false;
            TableDataGrid.CanUserAddRows = true;
        }

        private void BtnCreateDb_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog("Enter database name", @"[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+");
            if(dialog.ShowDialog() == true)
            {
                string databaseName = dialog.ResposneText;
                _databaseService.Database = new Database(databaseName);
                EnableButtonsDatabase();
                LblDatabaseName.Content = databaseName;
                _databaseFileManager.SaveDatabase(_databaseService.Database);
                TablesListBox.DataContext = _databaseService.Database?.Tables;
            }
        }

        private void EnableButtonsDatabase()
        {
            BtnDeleteDb.IsEnabled = true;
            BtnAddTable.IsEnabled = true;
            BtnDeleteTable.IsEnabled = true;
            BtnGetIntersection.IsEnabled = true;
        }

        private void DisableButtonsDatabase()
        {
            BtnDeleteDb.IsEnabled = false;
            BtnAddTable.IsEnabled = false;
            BtnDeleteTable.IsEnabled = false;
            BtnGetIntersection.IsEnabled = false;
        }

        private void EnableButtonsTable()
        {
            BtnAddField.IsEnabled = true;
            BtnDeleteField.IsEnabled = true;
        }

        private void DisableButtonsTable()
        {
            BtnAddField.IsEnabled = false;
            BtnDeleteField.IsEnabled = false;
        }

        private void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog("Enter table name", @"[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+");
            if(dialog.ShowDialog() == true)
            {
                string tableName = dialog.ResposneText;
                try
                {
                    _databaseService.AddTable(tableName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            SelectInputDialog dialog = new SelectInputDialog(_databaseFileManager.GetAllDatabaseNames());
            if(dialog.ShowDialog() == true)
            {
                string databasename = dialog.SelectedDatabaseName;
                try
                {
                    _databaseService.Database = _databaseFileManager.LoadDatabase(databasename);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                EnableButtonsDatabase();
                LblDatabaseName.Content = _databaseService.Database?.Name;
                TablesListBox.DataContext = _databaseService.Database?.Tables;
                TableDataGrid.DataContext = null;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(_databaseService.Database == null)
                return;
            try
            {
                _databaseFileManager.SaveDatabase(_databaseService.Database);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully saved");
        }

        private void BtnDeleteDb_Click(object sender, RoutedEventArgs e)
        {
            if(_databaseService.Database == null)
                return;
            try
            {
                _databaseFileManager.DeleteDatabase(_databaseService.Database.Name);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _databaseService.Database = null;
            DisableButtonsDatabase();
            DisableButtonsTable();
            TablesListBox.DataContext = null;
            TableDataGrid.DataContext = null;
            LblDatabaseName.Content = "";
        }

        private void BtnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if(TablesListBox.SelectedItem == null)
            {
                MessageBox.Show("Choose table to be deleted");
                return;
            }
            Table t = TablesListBox.SelectedItem as Table;
            try
            {
                _databaseService.DeleteTable(t.Name);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            TableDataGrid.DataContext = null;
            if(_databaseService.Database.Tables.Count <= 0)
            {
                DisableButtonsTable();
            }
        }

        private void TablesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(TablesListBox.SelectedItem == null)
            {
                return;
            }
            Table t = TablesListBox.SelectedItem as Table;
            RenderTable(t);
            EnableButtonsTable();
        }

        private void RenderTable(Table t)
        {
            DataTable dt = new DataTable();
            foreach(var field in t.Fields)
            {
                dt.Columns.Add(field.Name + " (" + field.Type + ")");
            }
            foreach(var row in t.Rows)
            {
                DataRow dr = dt.NewRow();
                for(int i = 0; i < row.Values.Count; ++i)
                {
                    dr[i] = row.Values[i];
                }
                dt.Rows.Add(dr);
            }
            TableDataGrid.DataContext = dt;
        }

        private void BtnAddField_Click(object sender, RoutedEventArgs e)
        {
            InputDialog dialog = new InputDialog("Enter field name and type separated by colon", @"[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+:(Integer|Char|String|Money|MoneyInterval|Real)");
            if(dialog.ShowDialog() == true)
            {
                var val = dialog.ResposneText;
                var sep = val.Split(':');
                var table = TablesListBox.SelectedItem as Table;
                try
                {
                    _databaseService.AddField(table.Name, sep[0], sep[1]);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                RenderTable(table);
            }
        }

        private void BtnDeleteField_Click(object sender, RoutedEventArgs e)
        {
            InputDialog inputDialog = new InputDialog("Enter field name", @"[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+");
            if(inputDialog.ShowDialog() == true)
            {
                Table table = TablesListBox.SelectedItem as Table;
                try
                {
                    var fieldName = inputDialog.ResposneText;
                    _databaseService.DeleteField(table.Name, fieldName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                RenderTable(table);
            }
        }

        private void TableDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dg = sender as DataGrid;
            TextBox t = e.EditingElement as TextBox;
            Table current = TablesListBox.SelectedItem as Table;
            int row = e.Row.GetIndex();
            int column = e.Column.DisplayIndex;
            List<string> values = new List<string>();
            for(int i = 0; i < current.Fields.Count; ++i)
            {
                if(row < current.Rows.Count)
                {
                    values.Add(current.Rows[row].Values[i]);
                }
                else
                {
                    values.Add(string.Empty);
                }
            }
            values[column] = t.Text;
            try
            {
                current.UpdateRow(values, row);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                e.Cancel = true;
                return;
            }
        }

        private void TableDataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                RenderTable(TablesListBox.SelectedItem as Table);
            }
            if(e.Key == Key.Delete)
            {
                int ind = TableDataGrid.SelectedIndex;
                if(ind == -1)
                    return;
                Table t = TablesListBox.SelectedItem as Table;
                t.DeleteRow(ind);
                RenderTable(t);
            }
        }

        private void BtnManual_Click(object sender, RoutedEventArgs e)
        {
            string manualText = "Supported data types:\nChar (ex. a)\nInteger (ex. 1)\nMoney (ex. $4.85)\nMoneyInterval" +
                " (ex. ($2.30, $3.55))\nReal (ex. 25.5)\nString (ex. abcde)";
            MessageBox.Show(manualText);
        }

        private void BtnGetIntersection_Click(object sender, RoutedEventArgs e)
        {
            InputDialog dialog = new InputDialog("Enter tables and fields to intersect. For example table1:table2:field1:field2:field3", "[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+:[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]+[:[0-9a-zA-Z]+[_-]*[0-9a-zA-Z]]?");
            if(dialog.ShowDialog() == true)
            {
                string resp = dialog.ResposneText;
                string[] parts = resp.Split(':');
                List<string> fields = parts[2..].ToList();
                Table intersection;
                try
                {
                    intersection = _databaseService.GetTablesIntersection(parts[0], parts[1], fields);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                DataGridIntersection dgint = new DataGridIntersection(intersection);
                dgint.Show();
            }
        }
    }


}
