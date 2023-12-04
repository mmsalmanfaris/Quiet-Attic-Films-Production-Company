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
    public partial class frmproductionstaff : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;

        public frmproductionstaff()
        {
            InitializeComponent();
        }

        private void productionstaff_Load(object sender, EventArgs e)
        {
            Combobox();
            myFillGridDetail();
        }

        private void btnsaave_Click(object sender, EventArgs e)
        {
            try
            {
                int production = 1 + int.Parse(cmbproductionid.SelectedIndex.ToString());
                int staff = 1 + int.Parse(cmbstaffid.SelectedIndex.ToString());


                string qry = "INSERT INTO Production_Staff (Production_id, Staff_id) VALUES ('" + production + "','" + staff + "')";

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
                string qry = "SELECT * FROM Production_Staff";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvproductionstaff.DataSource = dt;

                dgvproductionstaff.Columns[0].Width = 100;
                dgvproductionstaff.Columns[1].Width = 100;

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
                    string qry = "UPDATE Production_Staff SET " +
                    "Production_id = " + cmbproductionid.SelectedIndex + "," +
                    "Staff_id = " + cmbstaffid.SelectedIndex +
                    " WHERE Production_id = " + id + " AND Staff_id = " + id;


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

        private void dgvproductionstaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvproductionstaff.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Production_Staff WHERE Production_id = " + id + "AND Staff_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cmbproductionid.Text = rdr[1].ToString();
                    cmbstaffid.Text = rdr[2].ToString();
                }
                btnsaave.Enabled = false;
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
            string qry = "SELECT * FROM Production";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbproductionid.DisplayMember = "Production_Title";
            cmbproductionid.ValueMember = "Production_id";
            cmbproductionid.DataSource = dt;

            string qry1 = "SELECT * FROM Staff";
            SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cmbstaffid.DisplayMember = "Staff_Name";
            cmbstaffid.ValueMember = "Staff_id";
            cmbstaffid.DataSource = dt1;

        }
    }
}
