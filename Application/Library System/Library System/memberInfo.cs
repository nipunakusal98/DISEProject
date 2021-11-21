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
    public partial class memberinfo : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
        public memberinfo()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void memberinfo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_library_system_v0_1DataSet6.memberInfo' table. You can move, or remove it, as needed.
            this.memberInfoTableAdapter.Fill(this._library_system_v0_1DataSet6.memberInfo);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sqlst = "SELECT * FROM memberInfo WHERE MemberID='" + txtMemberID.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMemberID.Text = dr["MemberID"].ToString();
                    txtMemberName.Text = dr["Name"].ToString();
                    txtAge.Text = dr["Age"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                    if (dr["Gender"].Equals("M"))
                        rbMale.Checked = true;
                    else
                        rbFemale.Checked = true;
                    txtTelephone.Text = dr["Telephone"].ToString();
                    txtPassword.Text = dr["Password"].ToString();


                }
                else
                {
                    MessageBox.Show("Member Not Found");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void wipe()
        {
            txtMemberID.Text = " ";
            txtMemberName.Text = " ";
            txtAge.Text = "";
            txtAddress.Text = "";
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txtTelephone.Text = "";
            txtPassword.Text = "";


        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("INSERT INTO memberInfo (MemberID, Name, Age, Address, Gender, Telephone, Password) VALUES (@MemberID, @Name, @Age, @Address, @Gender, @Telephone, @Password)");
                cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@Name", txtMemberName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                string gender;
                if (rbMale.Checked)
                    gender = "M";
                else
                    gender = "F";
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.ExecuteNonQuery();
                wipe();
                MessageBox.Show("New Record Added", "database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE memberInfo SET Name=@Name, Age=@Age, Address=@Address, Gender=@Gender, Telephone=@Telephone, Password=@Password WHERE MemberID=@MemberID";
                cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@Name", txtMemberName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                string gender;
                if (rbMale.Checked)
                    gender = "M";
                else
                    gender = "F";
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated", "Database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                wipe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Record Update failed:invalid data type", "Database Update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM memberInfo WHERE MemberID='" + txtMemberID.Text + "' ";
            cmd.ExecuteNonQuery();
           
            MessageBox.Show("Record Deleted", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            wipe();
            con.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string sqlst = "SELECT * FROM memberInfo";
            SqlDataAdapter da = new SqlDataAdapter(sqlst, con);
            DataSet ds = new System.Data.DataSet();
            da.Fill(ds, "memberInfo");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmHome g = new frmHome();
            g.Show();
            this.Hide();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            memberInfoBindingSource.MoveNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            memberInfoBindingSource.MovePrevious();
        }
         
            
            
        }
    }

