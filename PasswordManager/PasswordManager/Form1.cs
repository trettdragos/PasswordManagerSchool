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
                conn.Open();

                string sql = "SELECT NAME FROM users WHERE NAME='"+email+"' AND PASSWORD='"+pass+"'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    Console.WriteLine("user is: " + result);
                    DashBoard dashBoard= new DashBoard(result.ToString());
                    dashBoard.Show();
                    //this.Close();
                    this.Hide();    
                }
                else
                {
                    MessageBox.Show("incorrect username or password");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateAccount create = new CreateAccount();
            create.ShowDialog();
            this.Hide();
        }
    }
}
