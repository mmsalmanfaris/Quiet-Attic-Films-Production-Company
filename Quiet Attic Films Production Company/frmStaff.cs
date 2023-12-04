using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiet_Attic_Films_Production_Company
{
    public partial class frmstaff : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;

        public frmstaff()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void frmstaff_Load(object sender, EventArgs e)
        {
            Combobox();
            myFillGridDetail();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtstaffname.Text = "";
            txtphoneno.Text = "";
            cmbstafftype.SelectedIndex = 0;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtstaffname.Text.Trim();
                string stringphoneno = txtphoneno.Text.Trim();
                int phoneno = int.Parse(stringphoneno);
                int stafftype = 1 + cmbstafftype.SelectedIndex;


                string qry = "INSERT INTO Staff (Staff_Name, Staff_Phone_no, Staff_Type_id) VALUES('" + name + "','" + phoneno + "','" + stafftype + "')";

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
                string qry = "SELECT * FROM Staff";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvstaff.DataSource = dt;

                dgvstaff.Columns[0].Width = 100;
                dgvstaff.Columns[1].Width = 100;

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
                    string qry = "UPDATE Staff SET " +
                        "Staff_Name = '" + txtstaffname.Text.Trim() + "'," +
                        "Staff_Phone_no = '" + txtphoneno.Text.Trim() + "'," +
                        "Staff_Type_id = '" + cmbstafftype.SelectedIndex + "'" +
                        "WHERE Staff_id = " + id;

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

        private void dgvstaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvstaff.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Staff WHERE Staff_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtstaffname.Text = rdr[1].ToString();
                    txtphoneno.Text = rdr[2].ToString();
                    cmbstafftype.Text = rdr[3].ToString();
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
            string qry = "SELECT * FROM Staff_Type";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbstafftype.DisplayMember = "Staff_Type";
            cmbstafftype.ValueMember = "Staff_Type_id";
            cmbstafftype.DataSource = dt;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you want to delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Staff WHERE Staff_id = " + id;
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
