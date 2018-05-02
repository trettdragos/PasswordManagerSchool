using System;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PasswordManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)//cancel and exit
        {
            System.Environment.Exit(1);
        }

        private void button2_Click(object sender, EventArgs e)//try to log in
        {

            string email = emailTextBox.Text;
            string pass = passwordTextBox.Text;
            string connStr = "server=localhost;user=root;database=paswman;port=3306;password=''";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT NAME FROM users WHERE NAME='"+email+"' AND PASSWORD='"+pass+"'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    Console.WriteLine("Number of countries in the world database is: " + result);
                    DashBoard dashBoard= new DashBoard(result.ToString());
                    dashBoard.Show();
                    this.Hide();                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("incorrect username or password");
            }

            conn.Close();
            Console.WriteLine("Done.");
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
