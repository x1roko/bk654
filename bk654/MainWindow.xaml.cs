using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bk654
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
            using (connection)
            {
                //connection.Open();
                /*string query = "INSERT INTO `bk654`.`user` (`name`, `surname`, `email`) VALUES ('anton', 'Nekrasov', 'asd@gmail.com');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // Получаем значение ID
                            string name = reader.GetString(1); // Получаем значение name

                            Console.WriteLine($"ID: {id}, Name: {name}");
                        }
                    }
                }*/
            }
        }
    }
}