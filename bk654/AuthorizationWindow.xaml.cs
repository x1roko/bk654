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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
                using (connection)
                {
                    string query = $"Select user_id from user where email = '{emailTextBox.Text}'";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    var _id = command.ExecuteScalar();
                    Properties.Settings.Default.id = Convert.ToInt32(_id);
                    MyUser.UserId = Convert.ToInt32(_id);
                    Properties.Settings.Default.Save();
                    connection.Close();
                }
                MessageBox.Show("Вы успешно авторизировались");
                StartWindow window = new();
                Close();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации, проверте введёные данные!\n{ex.Message}");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow window = new();
            window.ShowDialog();
        }
    }
}
