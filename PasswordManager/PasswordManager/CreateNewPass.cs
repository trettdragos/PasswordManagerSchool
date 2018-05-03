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
using System.Web.Security;

namespace PasswordManager
{
    public partial class CreateNewPass : Form
    {
        public CreateNewPass(string user)
        {
            InitializeComponent();
            Username = user;
        }

        public string connStr = "server=localhost;user=root;database=paswman;port=3306;password=''";
        public string Username { get; }

        private void CreateNewPass_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text.ToString();
                string pass = textBox2.Text.ToString();
                string check = textBox3.Text.ToString();
                if (pass.Equals(check) && !name.Equals(""))
                {
                    MySqlConnection conn = new MySqlConnection(connStr);
                    try
                    {
                        conn.Open();

                        string sql = "INSERT INTO " + Username + " (`ID`, `NAME`, `PASSWORD`) VALUES (NULL, '"+ name + "', '"+pass+"')";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader result = cmd.ExecuteReader();
                        if (result != null)
                        {
                            result.Read();
                            this.Close();
                            Console.WriteLine("succesfull");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    conn.Close();
                }
            }catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int length = rand.Next(8, 16);
            int numberOfNonAlphanumericCharacters = rand.Next(1, 6);
            string generatedPass = System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
            textBox2.Text = generatedPass;
            textBox3.Text = generatedPass;
        }
    }
}
