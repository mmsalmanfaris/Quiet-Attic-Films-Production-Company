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
    public partial class frmproperty : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;

        public frmproperty()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtpropertyname.Text.Trim();
                int type = 1 + cmbpropertytype.SelectedIndex;


                string qry = "INSERT INTO Property (Property_Name, Property_Type_id) VALUES('" + name + "','" + type + "')";

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
                string qry = "SELECT * FROM Property";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvproperty.DataSource = dt;

                dgvproperty.Columns[0].Width = 50;
                dgvproperty.Columns[1].Width = 80;

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
                        "Property_Name = '" + txtpropertyname.Text.Trim() + "'," +
                        "Property_Type_id = '" + cmbpropertytype.SelectedIndex + "'" +
                        "WHERE Property_id = " + id;

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

        private void dgvproperty_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvproperty.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Property WHERE Property_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtpropertyname.Text = rdr[1].ToString();
                    cmbpropertytype.Text = rdr[2].ToString();
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
            string qry = "SELECT * FROM Property_Type";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbpropertytype.DisplayMember = "Property_Type";
            cmbpropertytype.ValueMember = "Property_Type_id";
            cmbpropertytype.DataSource = dt;
        }

        private void frmproperty_Load(object sender, EventArgs e)
        {
            myFillGridDetail();
            Combobox();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtpropertyname.Text = "";
            cmbpropertytype.SelectedIndex = 0;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

        }
    }
}
