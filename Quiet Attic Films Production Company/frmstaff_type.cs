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
    public partial class frmstafftype : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;

        public frmstafftype()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string type = txtstafftype.Text.Trim();
                string cost = txtcostperday.Text.Trim();
                


                string qry = "INSERT INTO Staff_Type (Staff_Type, Cost_Per_Day) VALUES('" + type + "','" + cost + "')";

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
                string qry = "SELECT * FROM Staff_Type";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvstafftype.DataSource = dt;

                dgvstafftype.Columns[0].Width = 50;
                dgvstafftype.Columns[1].Width = 80;

            }
            catch (SqlException exex)
            {
                MessageBox.Show(exex.Message);
            }
        }

        private void frmstafftype_Load(object sender, EventArgs e)
        {
            myFillGridDetail();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtstafftype.Text = "";
            txtcostperday.Text = "";
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "UPDATE Staff_Type SET " +
                        "Staff_Type = '" + txtstafftype.Text.Trim() + "'," +
                        "Cost_Per_Day = '" + txtcostperday.Text.Trim() + "'" +
                        "WHERE Staff_Type_id = " + id;


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

        private void dgvstafftype_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvstafftype.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Staff_Type WHERE Staff_Type_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtstafftype.Text = rdr[1].ToString();
                    txtcostperday.Text = rdr[2].ToString();
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
                    string qry = "DELETE FROM Staff_Type WHERE Staff_Type_id = " + id;
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
