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
        public string connStr = "server=localhost;user=root;database=paswman;port=3306;password=''";
        int n = 0;

        private void DashBoard_Load(object sender, EventArgs e)
        { 
            
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT NAME FROM "+Username;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader result = cmd.ExecuteReader();
                if (result != null)
                {
                    Button[] b = new Button[100];
                    while (result.Read())
                    {
                        b[n] = new Button();
                        b[n].Width = 263;
                        b[n].Top = n * b[n].Height + 20;
                        b[n].Text = result["NAME"].ToString();
                        b[n].Click += new EventHandler(showPassword);
                        //b[n].Click += (sender2, e2) => showPassword(sender2, e2, n);
                        panel1.Controls.Add(b[n]);
                        n++;
                        Console.WriteLine(result["NAME"].ToString());
                    }
                    //add password names in list in the left side
                    //Console.WriteLine("passwords: " + result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
        }

        private void showPassword(object sender, EventArgs e)
        {
            Button x = (Button)sender;
            panel2.Controls.Clear();
            Label bigName = new Label();
            bigName.Text = x.Text.ToString();
            bigName.Height = 100;
            bigName.Width = 297;
            bigName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            bigName.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            panel2.Controls.Add(bigName);
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT PASSWORD FROM " + Username+" WHERE NAME = '"+ x.Text.ToString()+"'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader result = cmd.ExecuteReader();
                if (result != null)
                {
                    result.Read();
                    TextBox password = new TextBox();
                    password.Text = result["PASSWORD"].ToString();
                    password.Top = 100;
                    password.Left = (panel2.Width - password.Width) / 2;
                    Button delete = new Button();
                    delete.Width = 150;
                    delete.Click += (sender2, e2) => deletePassword(sender2, e2, x.Text.ToString());
                    delete.Top = bigName.Height + password.Height + 50;
                    delete.Left = (panel2.Width - delete.Width) / 2;
                    delete.Text = "Delete this password";
                    panel2.Controls.Add(password);
                    panel2.Controls.Add(delete);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
        }

        private void deletePassword(object sender, EventArgs e, string name)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "DELETE FROM " + Username + " WHERE NAME = '" + name + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader result = cmd.ExecuteReader();
                if (result != null)
                {
                    result.Read();
                    this.Refresh();
                    panel2.Controls.Clear();
                    panel1.Controls.Clear();
                    this.Close();
                    DashBoard dashBoard = new DashBoard(Username);
                    dashBoard.Show();
                    Console.WriteLine("succesfull");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewPass createNewPass = new CreateNewPass(Username);
            createNewPass.ShowDialog();
            this.Close();
            DashBoard dashBoard = new DashBoard(Username);
            dashBoard.Show();
        }
    }
}
