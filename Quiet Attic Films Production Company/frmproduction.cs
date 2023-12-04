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
    public partial class frmproduction : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = "";

        public frmproduction()
        {
            InitializeComponent();
        }

        private void production_Load(object sender, EventArgs e)
        {
            myFillGridDetail();
            Combobox();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtproductiontitle.Text.Trim();
                string stringdays = txtnumberofdays.Text.Trim();
                int days = int.Parse(stringdays);
                int type = int.Parse(cmbproductiontypeid.SelectedValue.ToString());
                int manager = int.Parse(cmbmanagerid.SelectedValue.ToString());
                int client = int.Parse(cmbclientid.SelectedValue.ToString());


                string qry = "INSERT INTO Production " +
                    "(Production_Title, Number_of_Days, Production_Type_id, Manager_id, Client_id) VALUES" +
                    "('" + title + "','" + days + "','" + type + "','" + manager + "','" + client + "')";

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

        void myFillGridDetail()
        {
            try
            {
                string qry = "SELECT * FROM Production";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvproduction.DataSource = dt;

                dgvproduction.Columns[0].Width = 50;
                dgvproduction.Columns[1].Width = 80;
                dgvproduction.Columns[2].Width = 80;
                dgvproduction.Columns[3].Width = 80;
                dgvproduction.Columns[4].Width = 80;

            }
            catch (SqlException exex)
            {
                MessageBox.Show(exex.Message);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "UPDATE Production SET " +
                        "Production_Title = '" + txtproductiontitle.Text.Trim() + "'," +
                        "Number_of_Days = '" + txtnumberofdays.Text.Trim() + "'," +
                        "Production_Type_id = '" + cmbproductiontypeid.SelectedIndex + "'," +
                        "Manager_id = '" + cmbmanagerid.SelectedIndex + "'," +
                        "Client_id = '" + cmbclientid.SelectedIndex + "'" +
                        "WHERE Production_id = " + id;

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

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtproductiontitle.Text = "";
            txtnumberofdays.Text = "";
            cmbproductiontypeid.SelectedIndex = 0;
            cmbmanagerid.SelectedIndex = 0;
            cmbclientid.SelectedIndex = 0;
        }

        private void dgvproduction_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvproduction.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Production WHERE Production_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtproductiontitle.Text = rdr[1].ToString();
                    txtnumberofdays.Text = rdr[2].ToString();
                    cmbproductiontypeid.Text = rdr[3].ToString();
                    cmbmanagerid.Text = rdr[4].ToString();
                    cmbclientid.Text = rdr[5].ToString();
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
        void Combobox()
        {
            string qry = "SELECT * FROM Production_Type";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbproductiontypeid.DisplayMember = "Production_Type";
            cmbproductiontypeid.ValueMember = "Production_Type_id";
            cmbproductiontypeid.DataSource = dt;

            string qry1 = "SELECT * FROM manager";
            SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cmbmanagerid.DisplayMember = "User_name";
            cmbmanagerid.ValueMember = "Manager_id";
            cmbmanagerid.DataSource = dt1;

            string qry2 = "SELECT * FROM Client";
            SqlDataAdapter da2 = new SqlDataAdapter(qry2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cmbclientid.DisplayMember = "Client_Name";
            cmbclientid.ValueMember = "Client_id";
            cmbclientid.DataSource = dt2;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Production WHERE Production_id = " + id;
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
    }
}
