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
    public partial class MemberSearchBook : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");

        public MemberSearchBook()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                string sqlst = "SELECT * FROM Book WHERE BookName='" + txtbookName.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtbookID.Text = dr["BookID"].ToString();
                    txtbookName.Text = dr["BookName"].ToString();
                    txtPrice.Text = dr["Price"].ToString();
                    txtCategory.Text= dr["Category"].ToString();
                    txtAuthor.Text = dr["Author"].ToString();
                    txtPublisher.Text = dr["Publisher"].ToString();


                }
                else
                {
                    MessageBox.Show("Book Not Found");
                    con.Close();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtbookID.Text = "";
            txtAuthor.Text = "";
            txtbookName.Text = "";
            txtCategory.Text = "";
            txtPublisher.Text = "";
            txtPrice.Text = "";

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Member_Home x = new Member_Home();
            x.Show();
            this.Hide();
        }

        private void MemberSearchBook_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Member_Home j = new Member_Home();
            j.Show();
            this.Hide();
        }
    }
}
