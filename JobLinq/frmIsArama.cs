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
    public partial class frmIsArama : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=ED-INTERN;Initial Catalog=DBJobLinq;Integrated Security=True");


        string SQLQuery = "";
        public frmIsArama()
        {
            InitializeComponent();
        }

        private void Property()
        {
            dgridIsArama.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgridIsArama.RowHeadersVisible = false;


        }

        private void DataList()
        {
            conn.Open();
            
            SQLQuery = "SELECT S.Ad, i.Departman, i.Tecrube, i.Tecrube, i.EgitimSeviyesi, i.YabancilDil, i.CalismaSekli, i.Pozisyon, i.IlanDetay FROM tblilan i INNER JOIN tblSirketBilgisi S ON S.SirketID= i.Sirket ";

            using (SqlCommand cmd =new SqlCommand(SQLQuery,conn))
            {
                using (SqlDataAdapter sda= new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgridIsArama.DataSource= dt;
                }
            }


                conn.Close();
        }

        


        private void frmIsArama_Load(object sender, EventArgs e)
        {
            DataList();
            Property();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
           
        }
    }
}
