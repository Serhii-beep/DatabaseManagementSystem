using DatabaseManagementSystem.BL.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using Table = DatabaseManagementSystem.BL.DatabaseEntities.Table;

namespace DatabaseManagementSystem.WPF
{
    /// <summary>
    /// Interaction logic for DataGridIntersection.xaml
    /// </summary>
    public partial class DataGridIntersection : Window
    {
        public DataGridIntersection(Table t)
        {
            InitializeComponent();
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
    }
}
