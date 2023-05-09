  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace JobLinq
{
    public partial class frmLogin : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ED-INTERN;Initial Catalog=DBJobLinq;Integrated Security=True");
        string SQLQuery = "";

        public string id;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Login()
        {
            conn.Open();
           
            SQLQuery = "SELECT UserId, HesapTipi FROM tblDatUser WHERE  Email=@Email and Parola=@Parola";

            using (SqlCommand cmd = new SqlCommand(SQLQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Email ", tBoxEmail.Text);
                cmd.Parameters.AddWithValue("@Parola ", tBoxPassword.Text);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                     
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                     id = dt.Rows[0][0].ToString();
                  
                    if (dt.Rows[0][1].Equals(1))
                    {
                        frmOzlukBilgisi ob = new frmOzlukBilgisi();
                      // ob.tBoxSirketUserId.Text = id;
                        ob.ShowDialog();
                    }
                    else
                    {
                        frmIsverenProfil Ib= new frmIsverenProfil();
                        Ib.tBoxSirketUserId.Text = id;
                        Ib.tBoxSirketEmail.Text = tBoxEmail.Text;
                        Ib.ShowDialog();
                    }

                }

                conn.Close();

            }
        }

            private void btnGirisYap_Click(object sender, EventArgs e)
            {
                Login();
            }
    }
    
}