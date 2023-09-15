using System.Collections.Generic;
using Avalonia.Controls;
using MySql.Data.MySqlClient;

namespace Library
{
    public partial class MainWindow : Window
    {
        private string _connString = "server=10.10.1.24;database=pro1_4;port=3306;User Id=user_01;password=user01pro";
        private List<Reader> _readers;
        private MySqlConnection _connection;
        public MainWindow()
        {
            InitializeComponent();
            ShowTable();
        }

        public void ShowTable()
        {
            string sql = "select ReaderID, ReaderName, GenderName from pro1_4.Reader " +
                         "join pro1_4.Gender G on G.GenderID = Reader.Gender;";
            _readers = new List<Reader>();
            _connection = new MySqlConnection(_connString);
            _connection.Open();
            MySqlCommand command = new MySqlCommand(sql, _connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentReader = new Reader()
                {
                    ReaderID = reader.GetInt32("ReaderID"),
                    ReaderName = reader.GetString("ReaderName"),
                    GenderName = reader.GetString("GenderName")
                };
                _readers.Add(currentReader);
            }
            _connection.Close();
            ReaderGrid.Items = _readers;
        }
    }
}