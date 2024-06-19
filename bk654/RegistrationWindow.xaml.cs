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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
                using (connection)
                {
                    string query = $"Insert into user(name, surname, email, phone_number) values" +
                        $" ('{nameTextBox.Text}', '{surnameTextBox.Text}', '{emailTextBox.Text}', '{telephoneTextBox.Text}')";
                    MySqlCommand command = new(query, connection);
                    command.ExecuteNonQuery();
                    query = "SELECT LAST_INSERT_ID();";
                    command = new MySqlCommand(query, connection);
                    int _id = Convert.ToInt32(command.ExecuteScalar());
                    Properties.Settings.Default.id = _id;
                    MyUser.UserId = Convert.ToInt32(_id);
                    Properties.Settings.Default.Save();
                    connection.Close();
                    MessageBox.Show("Вы успешно зарегестрировались");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не получилось завершить регистрацию, проверте целлостность всех данных!\n{ex.Message}");
            }
        }
    }
}
