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
    public partial class frmpropertylocation : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;

        public frmpropertylocation()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void form_propertylocation_Load(object sender, EventArgs e)
        {
            Combobox();
            myFillGridDetail();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                int property = 1 + int.Parse(cmbpropertyid.SelectedIndex.ToString());
                int location = 1 + int.Parse(cmblocationid.SelectedIndex.ToString());


                string qry = "INSERT INTO Property_Location (Property_id, Location_id) VALUES ('" + property + "','" + location + "')";

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
                string qry = "SELECT * FROM Property_Location";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvpropertylocation.DataSource = dt;

                dgvpropertylocation.Columns[0].Width = 100;
                dgvpropertylocation.Columns[1].Width = 100;

            }
            catch (SqlException exex)
            {
                MessageBox.Show(exex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "UPDATE Property_Location SET " +
                    "Property_id = " + cmbpropertyid.SelectedIndex + "," +
                    "Location_id = " + cmblocationid.SelectedIndex +
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvpropertylocation.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Property_Location WHERE Property_id = " + id + "AND Location_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cmbpropertyid.Text = rdr[1].ToString();
                    cmblocationid.Text = rdr[2].ToString();
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
            string qry = "SELECT * FROM Property";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbpropertyid.DisplayMember = "Property_Name";
            cmbpropertyid.ValueMember = "Property_id";
            cmbpropertyid.DataSource = dt;

            string qry1 = "SELECT * FROM Location";
            SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cmblocationid.DisplayMember = "Location_Name";
            cmblocationid.ValueMember = "Location_id";
            cmblocationid.DataSource = dt1;

        }

        private void txtpropertyid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
