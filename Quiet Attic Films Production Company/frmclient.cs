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
    public partial class frmclient : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;
        public frmclient()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtclientname.Text != "" && txtclientphoneno.Text != "")
            {
                try
                {
                    string name = txtclientname.Text.Trim();
                    string stringphoneno = txtclientphoneno.Text.Trim();
                    int phoneno = int.Parse(stringphoneno);


                    string qry = "INSERT INTO Client (Client_Name, Client_Phone_no) VALUES('" + name + "','" + phoneno + "')";

                    con.Open();
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Saved Successfully");

                    btnnew.PerformClick();

                    myFillGridDetail();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Name or Phone No is Empty");
            }

        }

        void myFillGridDetail()
        {
            try
            {
                string qry = "SELECT * FROM Client";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvclient.DataSource = dt;

                /*dgvclient.Columns[0].Visible = false;
                dgvclient.Columns[1].HeaderText = "Client Name";
                dgvclient.Columns[2].HeaderText = "Client Mobile";*/
                dgvclient.Columns[0].Width = 50;
                dgvclient.Columns[1].Width = 80;

            }
            catch (SqlException exex)
            {
                MessageBox.Show(exex.Message);
            }


        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtclientname.Text = "";
            txtclientphoneno.Text = "";
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "UPDATE Client SET Client_Name = '" + txtclientname.Text.Trim() + "'," +
                        "Client_Phone_no = '" + txtclientphoneno.Text.Trim() + "'" +
                        "WHERE Client_id = " + id;
                    

                    con.Open();
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully", "Update Operation");
                    btnnew.PerformClick();
                    myFillGridDetail();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dgvclient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvclient.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Client WHERE Client_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtclientname.Text = rdr[1].ToString();
                    txtclientphoneno.Text = rdr[2].ToString();
                }
                btnsave.Enabled = false;
                btnnew.Enabled = true;
                btndelete.Enabled = true;
                btnupdate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you want to delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM client WHERE client_id = " + id;
                    con.Open();
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Successfully", "Delete Operation");
                    btnnew.PerformClick();
                    myFillGridDetail();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtclientphoneno_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void frmclient_Load(object sender, EventArgs e)
        {
            myFillGridDetail();
        }
    }
}
