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
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            CheckLogin();
        }

        public void CheckLogin()
        {
            if (MyUser.UserId != 0)
            {
                authorizationButton.Visibility = Visibility.Hidden;
                logoutButton.Visibility = Visibility.Visible;
                ShowUser();
            }
            else
            {
                logoutButton.Visibility = Visibility.Hidden;
                authorizationButton.Visibility = Visibility.Visible;
                nameTextBlock.Text = "";
            }
        }
        
        public void ShowUser()
        {
            ShowNameTextBlock();
            if (MyUser.UserId == 1)
                showStockButton.Visibility = Visibility.Visible;
        }

        private void ShowNameTextBlock()
        {
            MySqlConnection connection = new ConnectionDB("localhost", "bk654", "root", "").Connect();
            using (connection)
            {
                string query = $"Select name from user where user_id = {MyUser.UserId}";
                MySqlCommand command = new MySqlCommand(query, connection);
                var name = command.ExecuteScalar();
                nameTextBlock.Text = name.ToString();
                nameTextBlock.FontSize = 18;
                connection.Close();
            }
        }

        private void ShowStockButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowStock stock = new ShowStock();
                stock.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                MainWindow window = new MainWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorizationWindow window = new AuthorizationWindow();
                Close();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void LogoutButtob_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.Save();
            MyUser.UserId = 0;
            CheckLogin();
        }
    }
}
