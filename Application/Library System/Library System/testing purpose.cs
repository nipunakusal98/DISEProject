using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_System
{
    public partial class testing_purpose : Form
    {
        public testing_purpose()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int x;
            x = int.Parse(txtNoOfDays.Text);

            dtpReturnDate.Value = dtpIssuedDate.Value.AddDays(+x);

        }

        private void radioButton30days_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value.AddDays(+30);
        }
    }
}
