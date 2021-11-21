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
    public partial class frm_Book : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
        public frm_Book()
        {
            InitializeComponent();
        }

        int count = 0;
        private void fillCombo1()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryName FROM Category ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbCategory.DataSource = dt;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryName";
            
        }

        private void fillCombo2()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT AuthorName FROM Author ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbAuthor.DataSource = dt;
            cbAuthor.DisplayMember = "AuthorName";
            cbAuthor.ValueMember = "AuthorName";

        }

        private void fillCombo3()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT PublisherName FROM Publisher ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbPublisher.DataSource = dt;
            cbPublisher.DisplayMember = "PublisherName";
            cbPublisher.ValueMember = "PublisherName";

        }

        private void wipe() 
        {
            txtbookID.Text = "";
            txtbookName.Text = "";
            txtPrice.Text = "";
            cbCategory.Text = "";
            cbAuthor.Text = "";
            cbPublisher.Text="";

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //refresh//
            string sqlst = "SELECT * FROM Book";
            SqlDataAdapter da = new SqlDataAdapter(sqlst, con);
            DataSet ds = new System.Data.DataSet();
            da.Fill(ds, "Book");
            tblBook.DataSource = ds.Tables[0];
        }



        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Book (BookID, BookName, Price, Category, Author, Publisher) values(@BookID, @BookName, @Price, @Category, @Author, @Publisher ) ", con);
                cmd.Parameters.AddWithValue("@BookID", txtbookID.Text);
                cmd.Parameters.AddWithValue("@BookName", txtbookName.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Category", cbCategory.Text);
                cmd.Parameters.AddWithValue("@Author", cbAuthor.Text);
                cmd.Parameters.AddWithValue("@Publisher", cbPublisher.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record inserted Successfully");
                wipe();
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
                cmd.CommandText = "UPDATE Book SET BookName=@BookName, @Price=Price, @Category=Category, @Author=Author, @Publisher=Publisher WHERE @BookID=BookID";
                cmd.Parameters.AddWithValue("@BookID", txtbookID.Text);
                cmd.Parameters.AddWithValue("@BookName", txtbookName.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Category",cbCategory.Text);
                cmd.Parameters.AddWithValue("@Author", cbAuthor.Text);
                cmd.Parameters.AddWithValue("@Publisher", cbPublisher.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated", "Database Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                wipe();
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
                cmd.CommandText = "DELETE FROM Book WHERE BookID='" + txtbookID.Text + "' ";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
                wipe();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
                    txtPrice.Text = dr["Price"].ToString();
                    cbCategory.Text = dr["Category"].ToString();
                    cbAuthor.Text = dr["Author"].ToString();
                    cbPublisher.Text = dr["Publisher"].ToString();


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

        private void frm_Book_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_library_system_v0_1DataSet.Book' table. You can move, or remove it, as needed.
            this.bookTableAdapter.Fill(this._library_system_v0_1DataSet.Book);
            fillCombo1();
            fillCombo2();
            fillCombo3();

            timer1.Start();

        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            frmCategory category = new frmCategory();
            category.Show();
           
            //con.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO Category (CategoryName) values(@CategoryName) ", con);
            //cmd.Parameters.AddWithValue("@CategoryName",cbCategory.Text);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //MessageBox.Show("Category added Successfully");
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {

            frmAuthor Author = new frmAuthor();
            Author.Show();

            //con.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO Author (AuthorName) values(@AuthorName) ", con);
            //cmd.Parameters.AddWithValue("@AuthorName", cbAuthor.Text);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //MessageBox.Show("Author added Successfully");
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmHome x= new frmHome();
            x.Show();
            this.Hide();


      
        }

        private void btnAddPubnlisher_Click(object sender, EventArgs e)
        {
            frmPublisher Publisher = new frmPublisher();
            Publisher.Show();
  

            //con.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO Publisher (PublisherName) values(@PublisherName) ", con);
            //cmd.Parameters.AddWithValue("@PublisherName", cbPublisher.Text);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //MessageBox.Show("Publisher added Successfully);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bookBindingSource.MoveNext();
        }

        private void btnPreviuos_Click(object sender, EventArgs e)
        {
            bookBindingSource.MovePrevious();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count = bookBindingSource.Count;
            lbl_status.Text = "Ststus: Total Number of Records:"+count.ToString();
        }

        private void lbl_status_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bookBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
