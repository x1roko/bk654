using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace bk654
{
    /// <summary>
    /// Логика взаимодействия для ShowStock.xaml
    /// </summary>
    public partial class ShowStock : Window
    {
        public ShowStock()
        {
            InitializeComponent();
            try
            {
                MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
                using (connection)
                {
                    string query = "select * from show_stock";
                    MySqlCommand command = new(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    List<ShowStockModel> stockModels = [];
                    while (reader.Read())
                    {
                        var stock = new ShowStockModel
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("Имя"),
                            QuantityBox = reader.GetInt32("Колличество не принятых коробок"),
                            QuantityInBox = reader.GetInt32("Колличество в 1 коробке"),
                            Quantity = reader.GetInt32("Колличество на складе"),
                        };
                        stockModels.Add(stock);
                    }
                    connection.Close();
                    stockDataGrid.ItemsSource = stockModels;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
