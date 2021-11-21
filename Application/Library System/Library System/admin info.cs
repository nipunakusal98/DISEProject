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
    public partial class admin_info : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");


        public admin_info()
        {
            InitializeComponent();
        }

        private void admin_info_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_library_system_v0_1DataSet8.Admin' table. You can move, or remove it, as needed.
            this.adminTableAdapter1.Fill(this._library_system_v0_1DataSet8.Admin);
            // TODO: This line of code loads data into the '_library_system_v0_1DataSet7.Admin' table. You can move, or remove it, as needed.
            this.adminTableAdapter.Fill(this._library_system_v0_1DataSet7.Admin);

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
            txtEmail.Text = "";
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                string sqlst = "SELECT * FROM Admin WHERE AdminID='" + txtMemberID.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMemberID.Text = dr["AdminID"].ToString();
                    txtMemberName.Text = dr["Name"].ToString();
                    txtAge.Text = dr["Age"].ToString();
                    if (dr["Gender"].Equals("M"))
                        rbMale.Checked = true;
                    else
                        rbFemale.Checked = true;
                    txtTelephone.Text = dr["Telephone"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    txtPassword.Text= dr["Password"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                 

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

        private void btnCreate_Click(object sender, EventArgs e)
        {
             
            
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("INSERT INTO Admin (AdminID, Name, Age, Gender, Telephone, Email, Password, Address) VALUES (@AdminID, @Name, @Age, @Gender, @Telephone, @Email, @Password, @Address)");
                cmd.Parameters.AddWithValue("@AdminID", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@Name", txtMemberName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                string gender;
                if (rbMale.Checked)
                    gender = "M";
                else
                    gender = "F";
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.ExecuteNonQuery();
                wipe();
                MessageBox.Show("New Record Added", "database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Admin SET Name=@Name, Age=@Age, Gender=@Gender, Telephone=@Telephone, Email=@Email, Address=@Address,  Password=@Password WHERE AdminID=@AdminID";
                cmd.Parameters.AddWithValue("@AdminID", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@Name", txtMemberName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                string gender;
                if (rbMale.Checked)
                    gender = "M";
                else
                    gender = "F";
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
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
            cmd.CommandText = "DELETE FROM Admin WHERE AdminID='" + txtMemberID.Text + "' ";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Deleted");
            wipe();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            String sqlst = "SELECT * FROM Admin";
            SqlDataAdapter da = new SqlDataAdapter(sqlst, con);
            DataSet ds = new System.Data.DataSet();
            da.Fill(ds, "Admin");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            adminBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            adminBindingSource.MoveNext();
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
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmHome c = new frmHome();
            c.Show();
            this.Hide();
        }
    }
}
