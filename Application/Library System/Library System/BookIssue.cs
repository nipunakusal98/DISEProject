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
    public partial class BookIssue : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
        public BookIssue()
        {
            InitializeComponent();
        }
        int count = 0;
        private void wipe()
        {
            txtbookID.Text = "";
            txtbookName.Text = "";
            txtMemID.Text = "";
            txtFullName.Text = "";
            txtallocatedtime.Text = "";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int x;
                x = int.Parse(txtallocatedtime.Text);
                dtpReturnDate.Value = dtpIssuedDate.Value.AddDays(+x);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
             try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                string sqlst = "SELECT * FROM Book WHERE BookID='" + txtbookID.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtbookID.Text = dr["BookID"].ToString();
                    txtbookName.Text = dr["BookName"].ToString();
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

        private void btnSearchMember_Click(object sender, EventArgs e)
        {
             try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                string sqlst = "SELECT * FROM memberInfo WHERE MemberID='" + txtMemID.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMemID.Text = dr["MemberID"].ToString();
                    txtFullName.Text = dr["Name"].ToString();
                }
                else
                {
                    MessageBox.Show("Member Not Found");
                    con.Close();
                }

            }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO issueBook(BID,Bname,MID,mname,issuedate,returndate)VALUES(@BID,@Bname,@MID,@mname,@issuedate,@returndate)", con);
                    cmd.Parameters.AddWithValue("@BID", txtbookID.Text);
                    cmd.Parameters.AddWithValue("@Bname", txtbookName.Text);
                    cmd.Parameters.AddWithValue("@MID", txtMemID.Text);
                    cmd.Parameters.AddWithValue("@mname", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@issuedate", dtpIssuedDate.Value);
                    cmd.Parameters.AddWithValue("@returndate", dtpReturnDate.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New Record Entered", "Record created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    wipe();
                    con.Close();
                    
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void BookIssue_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_library_system_v0_1DataSet2.IssueBook' table. You can move, or remove it, as needed.
            this.issueBookTableAdapter.Fill(this._library_system_v0_1DataSet2.IssueBook);
            timer1.Start();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE IssueBook SET BID=@BID,Bname=@Bname,MID=@MID,mname=@mname,issuedate=@issuedate,returndate=@returndate WHERE issueID=@issueID ";
                cmd.Parameters.AddWithValue("@issueID", txtIssueID.Text);
                cmd.Parameters.AddWithValue("@BID", txtbookID.Text);
                cmd.Parameters.AddWithValue("@Bname", txtbookName.Text);
                cmd.Parameters.AddWithValue("@MID", txtMemID.Text);
                cmd.Parameters.AddWithValue("@mname", txtFullName.Text);
                cmd.Parameters.AddWithValue("@issuedate", dtpIssuedDate.Value);
                cmd.Parameters.AddWithValue("@returndate", dtpReturnDate.Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("record updated successfully", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wipe();
                con.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIssueIDSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                string sqlst = "SELECT * FROM IssueBook WHERE issueID='" + txtIssueID.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtbookID.Text = dr["BID"].ToString();
                    txtbookName.Text = dr["Bname"].ToString();
                    txtMemID.Text = dr["MID"].ToString();
                    txtFullName.Text = dr["mname"].ToString();
                    dtpIssuedDate.Text= dr["issuedate"].ToString();
                    dtpReturnDate.Text = dr["returndate"].ToString();
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                    con.Close();
                }
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM IssueBook WHERE issueID='" + txtIssueID.Text + "' ";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wipe();
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string sqlst = "SELECT * FROM IssueBook";
            SqlDataAdapter da = new SqlDataAdapter(sqlst, con);
            DataSet ds = new System.Data.DataSet();
            da.Fill(ds, "IssueBook");
            tblIssueBook.DataSource = ds.Tables[0];
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            home.Show();
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count = issueBookBindingSource.Count;
            lbl_status.Text = "Ststus: Total Number of Records:" + count.ToString();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
