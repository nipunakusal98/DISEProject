using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Library_System
{
    public partial class frmCategory : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");

        public frmCategory()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) values(@CategoryName) ", con);
            cmd.Parameters.AddWithValue("@CategoryName",txtCatName.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Category added Successfully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
