using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiet_Attic_Films_Production_Company
{
    public partial class frmdashboard : Form
    {
        public frmdashboard()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmpropertytype propertytype_form = new frmpropertytype();
            propertytype_form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmproperty property_form = new frmproperty();
            property_form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmstafftype stafftype_form = new frmstafftype();
            stafftype_form.ShowDialog();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmstaff staff_form = new frmstaff();
            staff_form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmproductiontype production_type_form = new frmproductiontype();
            production_type_form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmproduction production_form = new frmproduction();
            production_form.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmclient client_form = new frmclient();
            client_form.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmlocation location_form = new frmlocation();
            location_form.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmproductionstaff productionstaff_form = new frmproductionstaff();
            productionstaff_form.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            form_productionproperty productionlocation_form = new form_productionproperty();
            productionlocation_form.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            form_propertylocation propertylocation_form = new form_propertylocation();
            propertylocation_form.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
