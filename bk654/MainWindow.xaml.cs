using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace bk654
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ProductModel> products = [];

        public MainWindow()
        {
            InitializeComponent();
            ShowProducts();
        }

        void ShowProducts()
        {
            MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
            using (connection)
            {
                string query = "Select * from product";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    var product = new ProductModel
                    {
                        product_id = reader.GetInt32("product_id"),
                        
                        name = reader.GetString("name"),
                        description = reader.GetString("description"),
                        price = reader.GetDouble("price")
                    };
                    products.Add(product);
                }
                connection.Close();
                DataContext = products;
                createOrderDataGrid.ItemsSource = products;//.Select(p => new { p.name, p.price, p.description, p.isAdded});
            }
        }

        void CreateOrder()
        {
            
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
            using (connection)
            {
                string query = "Insert into `order`(user_id) values (1);";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                query = "SELECT LAST_INSERT_ID();";
                command = new MySqlCommand(query, connection);
                int id = Convert.ToInt32(command.ExecuteScalar());
                string receipt = $"Номер вашего заказа - '{id}'\nСостав:\n";
                double summ = 0;
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Count > 0)
                    {
                        receipt += $"{products[i].name}\t\t{products[i].Count}x{products[i].price}\n";
                        summ += products[i].price * products[i].Count;
                        query = $"INSERT INTO order_composition (order_id, product_id, quantity) VALUES ({id}, {products[i].product_id}, {products[i].Count});";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }
                }
                receipt += $"Итоговая сумма\t{summ}";
                MessageBox.Show(receipt);
                connection.Close();
            }
        }

        private void decrement_Click(object sender, RoutedEventArgs e)
        {
            if (products[((ProductModel)createOrderDataGrid.SelectedItem).product_id-1].Count > 0)
                products[((ProductModel)createOrderDataGrid.SelectedItem).product_id-1].Count--;
        }

        private void increment_Click(object sender, RoutedEventArgs e)
        {
            if (products[((ProductModel)createOrderDataGrid.SelectedItem).product_id-1].Count < 10)
                products[((ProductModel)createOrderDataGrid.SelectedItem).product_id-1].Count++;
        }
    }
}