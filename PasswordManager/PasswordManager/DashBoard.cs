using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class DashBoard : Form
    {
        public DashBoard(string username)
        {
            InitializeComponent();
            Username = username;
        }

        public string Username { get; }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user=root;database=paswman;port=3306;password=''";
            MessageBox.Show(Username);
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT NAME FROM "+Username;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    //add password names in list in the left side
                    Console.WriteLine("Number of countries in the world database is: " + result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
