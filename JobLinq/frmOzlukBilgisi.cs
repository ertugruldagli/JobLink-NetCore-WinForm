using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobLinq
{
    public partial class frmOzlukBilgisi : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ED-INTERN;Initial Catalog=DBJobLinq;Integrated Security=True");
        string SQLQuery = "";
        public frmOzlukBilgisi()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            frmIsArayanProfil pp = new frmIsArayanProfil();
            pp.tBoxProfilEmail.Text = tboxOzlukEmail.Text;
            pp.ShowDialog();

        }

        private void Combo()
        {
            conn.Open();
            SQLQuery = "Select SehirId, SehirAdi From prmSehir order by SehirAdi asc";
           
                using (SqlDataAdapter sda=new SqlDataAdapter(SQLQuery,conn))
                {
                    using (DataSet ds=new DataSet())
                    {
                        sda.Fill(ds);
                        cBoxOzlukSehir.ValueMember = "SehirId";
                        cBoxOzlukSehir.DisplayMember = "SehirAdi";
                        cBoxOzlukSehir.DataSource = ds.Tables[0];
                    }
                   
                    
                }
            


                conn.Close();
        }


        private void ListData()
        {
            conn.Open();
            int HesapTipi;
            SQLQuery = "SELECT * FROM tblOzlukBilgisi";
       
            conn.Close();
        }

        private void frmOzlukBilgisi_Load(object sender, EventArgs e)
        {
            Combo();
        }
    }


    
}
