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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JobLinq
{
    public partial class frmIsverenProfil : Form
    {
        
        SqlConnection conn = new SqlConnection(@"Data Source=ED-INTERN;Initial Catalog=DBJobLinq;Integrated Security=True");
        string SQLQuery = "";

        public frmIsverenProfil()
        {
            InitializeComponent();
        }

        private void AddData()
        {
            
            conn.Open();

            SQLQuery = "INSERT INTO tblSirketBilgisi ( UserId, Ad, Sektor, Adres, Sehir, CalisanSayisi, Aciklama ) VALUES ( @UserId, @Ad, @Sektor,@Adres, @Sehir, @CalisanSayisi, @Aciklama)";

            string id;

            using (SqlCommand cmd =new SqlCommand (SQLQuery,conn))
            {

                cmd.Parameters.AddWithValue("@UserId",tBoxSirketUserId.Text);
                cmd.Parameters.AddWithValue("@Ad ", tBoxSirketAD.Text);
                cmd.Parameters.AddWithValue("@Sektor ", cBoxSirketSektor.SelectedValue);
                cmd.Parameters.AddWithValue("@Adres ", tBoxSirketAdres.Text);
                cmd.Parameters.AddWithValue("@Sehir ", cBoxSirketSehir.SelectedValue);
                cmd.Parameters.AddWithValue("@CalisanSayisi ", tBoxCalisanSayisi.Text);
                cmd.Parameters.AddWithValue("@Aciklama ", tBoxSirketAciklama.Text);

                cmd.CommandType = CommandType.Text;

                

                using (SqlDataAdapter sda =new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    sda.Fill(dataTable);

                    id = dataTable.Rows[0][0].ToString();

                    //cBoxSirketSehir.DisplayMember = "Sehir";
                    //cBoxSirketSehir.ValueMember = "Sehir";
                    //cBoxSirketSehir.DataSource = dataTable;

                    MessageBox.Show("İşleminiz başarılı");

                }

            }
         


            conn.Close();
       
        }

      
        private void ComboBox()
        {
           
            conn.Open();
            try
            {
                string query = "SELECT SehirAdi, SehirID from prmSehir order by SehirAdi asc ";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
               
                DataSet ds = new DataSet();
                da.Fill(ds);
                cBoxSirketSehir.DisplayMember = "SehirAdi";
                cBoxSirketSehir.ValueMember = "SehirID";
                cBoxSirketSehir.DataSource = ds.Tables[0];

            }
            catch (Exception)
            {

                MessageBox.Show("Error occured!");
            }
            
            try
            {
                string query1 = "select SektorID, SektorName from prmSektorBilgisi ";
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                
                DataSet ds = new DataSet();
                da.Fill(ds);
                cBoxSirketSektor.DisplayMember = "SektorName";
                cBoxSirketSektor.ValueMember = "SektorID";
                cBoxSirketSektor.DataSource = ds.Tables[0];
            }
            catch (Exception)
            {

                MessageBox.Show("Error occured!");
            }
            conn.Close();
        }

        private void btnIsVerenProfilGuncelle_Click(object sender, EventArgs e)
        {
            AddData();

        }
        private void frmIsverenProfil_Load(object sender, EventArgs e)
        {
            ComboBox();
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            frmIlanlarim frmIlanlarim= new frmIlanlarim();
            frmIlanlarim.ShowDialog();
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            frmBasvuranlar frmBasvuranlar= new frmBasvuranlar();
            frmBasvuranlar.ShowDialog();
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            frmYeniIlan frmYeniIlan = new frmYeniIlan();
            frmYeniIlan.ShowDialog();   
        }

        
    }
}
