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
    public partial class Payments : Form
    {
        //pnanel moving//
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hwvd, int msg, int wPram, int Ipram);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
        double gap;
        double dayamount;
        double fine;
        public Payments()
        {
            InitializeComponent();
        }

        public void wipe()
        {
            txtbookID.Text = "";
            txtIssueID.Text = "";
            txtExceededDays.Text = "";
            txtFine4Days.Text = "";

        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dtpIssuedDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-7GKE93S;Initial Catalog=library system v0.1;Integrated Security=True");
                string sqlst = "SELECT * FROM IssueBook WHERE issueID='" + txtIssueID.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlst,con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtbookID.Text = dr["BID"].ToString();
                    dtpIssuedDate.Text = dr["issuedate"].ToString();
                    dtpReturnDate1.Text = dr["returndate"].ToString();
                    DateTime issuedate, returndate, todaydate;
                    TimeSpan datediff;
                    

                    //calculating dates
                    todaydate = DateTime.Parse(dtpDate.Text);
                    returndate = DateTime.Parse(dtpReturnDate1.Text);
                    datediff = todaydate - returndate;
                    gap = datediff.TotalDays;
                    txtExceededDays.Text = gap.ToString();
                    if (gap > 0)
                    {
                        MessageBox.Show("Member suspended!");
                    }
                }
                else 
                {
                    MessageBox.Show("Record not found");
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCalFine_Click(object sender, EventArgs e)
        {
            try
            {
                dayamount = double.Parse(txtFine4Days.Text);
                fine = dayamount * gap;
                txtfine.Text = fine.ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //making a print button!!
            try
            {
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

           
               

            
        }


            
       

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(rtRecipt.Text, new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(0, 0));
        }

        private void btnpreview_Click(object sender, EventArgs e)
        {

            try
            {
                //making a recipt
                rtRecipt.Clear();
                rtRecipt.AppendText("\t" + "\t" + "Payment Recipt" + "\n");
                rtRecipt.AppendText("=============================================" + "\n");
                rtRecipt.AppendText("Issue ID" + "\t" + "\t" + "\t" +"\t"+ txtIssueID.Text + "\n");
                rtRecipt.AppendText("Book ID" + "\t" + "\t" + "\t"+"\t" + txtbookID.Text + "\n");
                rtRecipt.AppendText("Issued Date" + "\t" + "\t" + "\t" + dtpIssuedDate.Text + "\n");
                rtRecipt.AppendText("Return Date" + "\t" + "\t" + "\t" + dtpReturnDate1.Text + "\n");
                rtRecipt.AppendText("Date" + "\t" + "\t" + "\t" + "\t" + dtpDate.Text + "\n");
                rtRecipt.AppendText("Exceeded Days" + "\t" + "\t" + "\t" + txtExceededDays.Text + "\n");
                rtRecipt.AppendText("Fine amount per day" + "\t" + "\t" + txtFine4Days.Text + "\n");
                rtRecipt.AppendText("Total fine amount" + "\t" + "\t" + "\t" + txtfine.Text + "\n");
                rtRecipt.AppendText("=============================================" + "\n");
                rtRecipt.AppendText("\t" + "\t" + "thank you!" + "\n");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
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
            frmHome home = new frmHome();
            home.Show();
            this.Hide();
        }
    }
}
