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
    public partial class welcome : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");


        public welcome()
        {
            InitializeComponent();
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Admin WHERE AdminID='" + txtAdminID.Text + "'AND Password='" + txtAdminPassword.Text + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    frmHome frmhome = new frmHome();
                    frmhome.Show();
                    this.Hide();

                    con.Close();

                }
                else
                {
                    MessageBox.Show("Username or Password Incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username or Password Incorrect" + ex);
            }
            
        }

        private void btnAdminLogin_MouseDown(object sender, MouseEventArgs e)
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

        private void btnMemberlogin_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM memberInfo WHERE MemberID='" + txtMemberID.Text + "'AND Password='" + txtMemberPassword.Text + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Member_Home frmhome = new Member_Home();
                    frmhome.Show();
                    this.Hide();

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password Incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username or Password Incorrect" + ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminPasswordUpdate x = new adminPasswordUpdate();
            x.Show();
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            memberPasswordUpdate y = new memberPasswordUpdate();
            y.Show();
        }
    }
}
