using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PasswordManager
{
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        public string connStr = "server=localhost;user=root;database=paswman;port=3306;password=''";

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.ToString();
            string password = textBox2.Text.ToString();
            string check = textBox3.Text.ToString();
            if(password.Equals(check) && !username.Equals(""))
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    conn.Open();

                    string sql = "CREATE TABLE paswman." + username + " ( `ID` INT NOT NULL AUTO_INCREMENT, `NAME` TEXT NOT NULL , `PASSWORD` TEXT NOT NULL , PRIMARY KEY(`ID`)) ENGINE = InnoDB";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader result = cmd.ExecuteReader();
                    if (result != null)
                    {
                        while (result.Read()) ;
                        //this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
               
                try
                {
                    conn.Open();
                    string sql2 = "INSERT INTO users (`ID`, `NAME`, `PASSWORD`) VALUES (NULL, '" + username + "', '" + password + "')";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    MySqlDataReader result2 = cmd2.ExecuteReader();
                    if (result2 != null)
                    {
                        while (result2.Read()) ;
                        DashBoard dash = new DashBoard(username);
                        dash.Show();
                        this.Close();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                
            }
        }
    }
}
