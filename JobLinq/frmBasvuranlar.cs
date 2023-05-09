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
    public partial class frmBasvuranlar : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=ED-INTERN;Initial Catalog=DBJobLinq;Integrated Security=True");
        string SQLQuery = "";
        public frmBasvuranlar()
        {
            InitializeComponent();
        }


        private void Property()
        {
            dgridIBasvurnlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgridIBasvurnlar.RowHeadersVisible = false;
        }


        private void DataList()
        {
        conn.Open();


        SQLQuery = "SELECT OB.Ad, OB.Soyad, OB.CepNo, I.Pozisyon, I.Departman,  I.CalismaSekli\r\nFROM Junction J \r\nINNER JOIN tblOzlukBilgisi OB ON OB.UserID= J.UserID\r\nINNER JOIN tblilan I ON I.ID=J.IlanID";

        using (SqlCommand cmd = new SqlCommand(SQLQuery, conn))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                dgridIBasvurnlar.DataSource = dataTable;
            }
        }

        conn.Close();

        }

        private void frmBasvuranlar_Load(object sender, EventArgs e)
        {
            DataList();
            Property();
        }
    }
}
