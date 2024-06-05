using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bk654
{
    public class ConnectionDB
    {
        private string connectionString;

        public ConnectionDB(string server, string database, string uid, string password)
        {
            // Формирование строки подключения
            connectionString = $"server={server};database={database};userid={uid};password={password}";
        }

        public MySqlConnection Connect()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();

                return connection;
            }
            catch
            {
                return null;
            }
        }

        public void Disconnect(MySqlConnection connection)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Подключение закрыто.");
            }
        }
    }
}
