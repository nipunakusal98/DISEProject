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
    public partial class memberUpdatedetail : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
        public memberUpdatedetail()
        {
            InitializeComponent();
        }

        private void wipe()
        {
            txtMemID.Text = "";
            txtFullName.Text = "";
            txtAge.Text = "";
            txtAddress.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtTelephone.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
                    txtAge.Text = dr["Age"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                    if (dr["Gender"].Equals("Male"))
                    {
                        rbMale.Checked = true;
                    }
                    else
                    {
                        rbFemale.Checked = true;
                    }
                    txtTelephone.Text = dr["Telephone"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    txtPassword.Text = dr["Password"].ToString();


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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE memberInfo SET Name=Name,Age=@Age,Address=@Address,Gender=@Gender,Telephone=@Telephone,Password=@Password,Email=@Email WHERE MemberID=@MemberID";
                cmd.Parameters.AddWithValue("@MemberID", txtMemID.Text);
                cmd.Parameters.AddWithValue("@Name", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                string gender;
                if (rbMale.Checked) gender = "Male";
                else gender = "Female";
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated", "Update Succsessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wipe();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Member_Home x = new Member_Home();
            x.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Member_Home b = new Member_Home();
            b.Show();
            this.Close();
        }
    }
}
