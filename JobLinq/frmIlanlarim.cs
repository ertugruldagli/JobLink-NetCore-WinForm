using Microsoft.Win32.SafeHandles;
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
    public partial class frmIlanlarim : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=ED-INTERN;Initial Catalog=DBJobLinq;Integrated Security=True");
        string SQLQuery = "";

        public frmIlanlarim()
        {
            InitializeComponent();
        }


        private void Property()
        {
            dgridIlanlarim.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;

            dgridIlanlarim.RowHeadersVisible=false;

            
        }


       
        private void DataList()
        {
            conn.Open();
            
            
            SQLQuery = "SELECT I.ID, I.Sirket , I.Departman, I.Tecrube, I.EgitimSeviyesi, I.YabancilDil, I.CalismaSekli, I.Pozisyon, prmSehir.SehirAdi, I.IlanDetay FROM tblilan I INNER JOIN prmSehir ON prmSehir.SehirId = I.Sehir";

            using (SqlCommand cmd = new SqlCommand(SQLQuery,conn))
            {
                using (SqlDataAdapter sda =new SqlDataAdapter(cmd))
                {
                    DataTable dataTable= new DataTable();   
                    sda.Fill(dataTable);

                    dgridIlanlarim.DataSource= dataTable;
                }
            }

            
                SQLQuery = "SELECT* from prmSehir ";
                SqlCommand cmd1 = new SqlCommand(SQLQuery, conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

           
                cBoxIlanSehir.DisplayMember = "SehirAdi";
                cBoxIlanSehir.ValueMember = "SehirID";
                cBoxIlanSehir.DataSource = dt1;

           
            conn.Close();

        }

        private void dgridIlanlarim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tBoxIlanSirketId.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[1].Value.ToString();
            tBoxIlanDepartman.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[2].Value.ToString();
            tBoxIlanTecrube.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[3].Value.ToString();
            tBoxIlanEgitim.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[4].Value.ToString();
            tBoxIlanYabanciDil.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[5].Value.ToString();
            tBoxIlanCalisma.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[6].Value.ToString();
            tBoxIlanPozisyon.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[7].Value.ToString();
            //cBoxIlanSehir.SelectedValue = dgridIlanlarim.Rows[e.RowIndex].Cells[7].Value.ToString();
            tBoxIlanDetay.Text = dgridIlanlarim.Rows[e.RowIndex].Cells[9].Value.ToString();
        }





        private void frmIlanlarim_Load(object sender, EventArgs e)
        {
            DataList();
            Property();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SQLQuery = "DELETE FROM tblilan where ID=@ID";
            SqlCommand cmd = new SqlCommand(SQLQuery, conn);

            cmd.Parameters.AddWithValue("@ID", dgridIlanlarim.CurrentRow.Cells[0].Value);

            cmd.ExecuteNonQuery();
            DataList();
            MessageBox.Show("Veri Silindi");
           
            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SQLQuery = "UPDATE tblilan SET Departman=@Departman, Tecrube=@Tecrube, EgitimSeviyesi=@EgitimSeviyesi, YabancilDil=@YabancilDil," +
                " CalismaSekli=@CalismaSekli, Pozisyon=@Pozisyon,  IlanDetay=@Ilandetay  WHERE ID=@ID";

            SqlCommand cmd = new SqlCommand(SQLQuery, conn);

            cmd.Parameters.AddWithValue("@ID", dgridIlanlarim.CurrentRow.Cells[0].Value);
            cmd.Parameters.AddWithValue("@Departman", tBoxIlanDepartman.Text);
            cmd.Parameters.AddWithValue("@Tecrube", tBoxIlanTecrube.Text);
            cmd.Parameters.AddWithValue("@EgitimSeviyesi", tBoxIlanEgitim.Text);
            cmd.Parameters.AddWithValue("@YabancilDil", tBoxIlanYabanciDil.Text);
            cmd.Parameters.AddWithValue("@CalismaSekli", tBoxIlanCalisma.Text);
            cmd.Parameters.AddWithValue("@Pozisyon", tBoxIlanPozisyon.Text);
            cmd.Parameters.AddWithValue("@IlanDetay", tBoxIlanDetay.Text);


            cmd.CommandType = CommandType.Text;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();


                MessageBox.Show("İşleminiz başarılı");

                conn.Close();
                DataList();
            }
            catch (Exception hata)
            {


                MessageBox.Show(hata.ToString());
            }

        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            frmBasvuranlar frmBasvuranlar= new frmBasvuranlar();
            frmBasvuranlar.ShowDialog();
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            frmYeniIlan frmYeniIlan= new frmYeniIlan(); 
            frmYeniIlan.ShowDialog();
        }
    }
}
