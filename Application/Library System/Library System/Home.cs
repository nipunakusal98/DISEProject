using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Library_System
{
    public partial class frmHome : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frmHome()
        {
            InitializeComponent();
        }

        private void btnBookInfo_Click(object sender, EventArgs e)
        {
            frm_Book frmbook = new frm_Book();
            frmbook.Show();
            this.Hide();
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            memberinfo m = new memberinfo();
            m.Show();
            this.Hide();
        }

        private void btnBookIssue_Click(object sender, EventArgs e)
        {
            BookIssue frmBookIssue = new BookIssue();
            frmBookIssue.Show();
            this.Hide();
        }

        private void btnBookReturn_Click(object sender, EventArgs e)
        {
            Payments frmPayments = new Payments();
            frmPayments.Show();
            this.Hide();
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }

        private void frmHome_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frm_Book frmbook = new frm_Book();
            frmbook.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            welcome wlcome = new welcome();
            wlcome.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Payments frmPayments = new Payments();
            frmPayments.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            BookIssue frmBookIssue = new BookIssue();
            frmBookIssue.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdminInfo_Click(object sender, EventArgs e)
        {
            admin_info a = new admin_info();
            a.Show();
            this.Hide();
        }
    }
}
