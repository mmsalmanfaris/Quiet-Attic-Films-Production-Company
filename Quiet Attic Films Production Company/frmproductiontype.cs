﻿using System;
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
    public partial class frmproductiontype : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=SALMAN_FARIS\SQLEXPRESS;Initial Catalog=Quiet_Attic_Films;Integrated Security=True");
        string id = string.Empty;

        public frmproductiontype()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string type = txtproductiontype.Text.Trim();
                string description = txtdescription.Text.Trim();


                string qry = "INSERT INTO Production_Type (Production_Type, Prodtype_Description) VALUES('" + type + "','" + description + "')";

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
                string qry = "SELECT * FROM Production_Type";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                da.Fill(dt);
                dgvproductiontype.DataSource = dt;

                dgvproductiontype.Columns[0].Width = 50;
                dgvproductiontype.Columns[1].Width = 80;

            }
            catch (SqlException exex)
            {
                MessageBox.Show(exex.Message);
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtproductiontype.Text = "";
            txtdescription.Text = "";
            btnsave.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "UPDATE Production_Type SET " +
                        "Production_Type = '" + txtproductiontype.Text.Trim() + "'," +
                        "Prodtype_Description = '" + txtdescription.Text.Trim() + "'" +
                        "WHERE Production_Type_id = " + id;

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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you want to delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Production_Type WHERE Production_Type_id = " + id;
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

        private void dgvproductiontype_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dgvproductiontype.Rows[e.RowIndex].Cells[0].Value.ToString();

                string qry = "SELECT * FROM Production_Type WHERE Production_Type_id = " + id;
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtproductiontype.Text = rdr[1].ToString();
                    txtdescription.Text = rdr[2].ToString();
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

        private void frmproductiontype_Load(object sender, EventArgs e)
        {
            myFillGridDetail();
        }
    }
}
