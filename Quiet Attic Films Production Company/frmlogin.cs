using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiet_Attic_Films_Production_Company
{
    public partial class form_login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");

        public form_login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cb_showpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_showpassword.Checked == true)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else 
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtusername.Text.Trim() != "" && txtpassword.Text.Trim() != "")
                {
                    con.Open();
                    string qry = "SELECT * FROM Manager WHERE User_name = '" + txtusername.Text.Trim() + "' " +
                    "AND password = '" + txtpassword.Text.Trim() + "' ";

                    SqlCommand cmd = new SqlCommand(qry, con);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        MessageBox.Show("Login Success");

                        frmdashboard formmain = new frmdashboard();
                        formmain.ShowDialog();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password");
                    }
                        
                }
                else
                {
                    MessageBox.Show("Username or Password Empty");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
