using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;



namespace ДемоЭкз
{
    public partial class Auth : Form
    {
        string connectionString;
        string Log;
        string Password;

        public Auth()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["ДемоЭкз.Properties.Settings._16is30ConnectionString"].ConnectionString;
        }

        private void Auth_Load(object sender, EventArgs e)
        {

        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Log = Login.Text.Trim();
            Password = Pass.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Int32 countUser = 0;
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT COUNT (*) FROM [dbo].[users] WHERE Login = @login AND Password = @pass";
                command.Connection = connection;

                command.Parameters.Add("@Login", SqlDbType.VarChar); // НУ ТУТ КАРОЧИ ПЕРЕВОД БУКАФ В СТРИНГ
                command.Parameters["@Login"].Value = Log;

                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = Password;

                try
                {
                    connection.Open();
                    countUser = (Int32)command.ExecuteScalar();
                    if (countUser == 1)
                    {
                        MessageBox.Show("Авторизация успешна.");
                    }
                    else
                    {
                        MessageBox.Show(" Ошибка авторизации.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
