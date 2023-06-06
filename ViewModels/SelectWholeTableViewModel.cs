using Avalonia.Controls;
using Avalonia.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace DatabaseHelper.ViewModels
{
    public class SelectWholeTableViewModel : ViewModelBase
    {
        private string _selectedTable;
        private string _selectedDatabase;
        private DataGrid _dataGrid;

        public SelectWholeTableViewModel()
        {

        }

        public SelectWholeTableViewModel(string selectedTable, string selectedDatabase, DataGrid dataGrid)
        {
            _selectedTable = selectedTable;
            _selectedDatabase = selectedDatabase;
            _dataGrid = dataGrid;
        }

        public void PopulateDataTable()
        {
            var commandq = @$"SELECT * FROM {_selectedTable}";
            using (var connection = new SqlConnection(
                       @$"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={_selectedDatabase};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                SqlCommand command = new SqlCommand(commandq, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                _dataGrid.Columns.Clear();

                foreach (DataColumn column in dataTable.Columns)
                {
                    DataGridColumn textColumn = new DataGridTextColumn
                    {
                        Header = column.ColumnName,
                        Binding = new Binding($"[{column.ColumnName}]")

                    };
                    textColumn.CanUserSort = true;
                    _dataGrid.Columns.Add(textColumn);
                }

                var collection = new ObservableCollection<Dictionary<string, object>>();
                foreach (DataRow row in dataTable.Rows)
                {
                    var rowDict = new Dictionary<string, object>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        rowDict.Add(column.ColumnName, row[column].ToString()?.Trim()!);
                    }

                    collection.Add(rowDict);
                }

                _dataGrid.Items = collection;
            }
        }
    }
}