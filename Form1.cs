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

namespace ETUT_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=ATES\\SQLEXPRESS;Initial Catalog=DBETUT;Integrated Security=True");
        
        void derslistesi()
        {
            SqlDataAdapter da= new SqlDataAdapter("select *from TBLDERSLER",baglan);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDers.DisplayMember = "DERSAD";
            cmbDers.ValueMember = "DERSID";
            cmbDers.DataSource = dt;
        }
        void etutlistesi()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("execute TBLETUT", baglan);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            derslistesi();
            etutlistesi();
        }

        private void cmbDers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select *from TBLOGRETMEN WHERE BRANSID=" + cmbDers.SelectedValue, baglan);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cmbOgretmen.ValueMember = "OGRTID";
            cmbOgretmen.DisplayMember = "AD";
            cmbOgretmen.DataSource = dt2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into TBLETUT (DERSID,OGRETMENID,TARIH,SAAT) VALUES(@P1,@P2,@P3,@P4)", baglan);
            komut.Parameters.AddWithValue("@P1",cmbDers.SelectedValue);
            komut.Parameters.AddWithValue("@P2", cmbOgretmen.SelectedValue);
            komut.Parameters.AddWithValue("@P3", mskTarıh.Text);
            komut.Parameters.AddWithValue("@P4", mskSaat.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("KAYDETME ISLEMI BASARIYLA TAMAMLANDI","BILGI",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("update TBLETUT SET OGRENCIID=@P1,DURUM=@P2 WHERE ID=@P3", baglan);
            komut.Parameters.AddWithValue("@P1",txtOgrencı.Text);
            komut.Parameters.AddWithValue("@P2", "True");
            komut.Parameters.AddWithValue("@P3", txtEtut.Text);
            komut.ExecuteNonQuery();
            baglan.Close() ;
            MessageBox.Show("İSLEMİNİZ BASARIYLA TAMAMLANDI");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into TBLOGRENCI(AD,SOYAD,FOTOGRAF,SINIF,TELEFON,MAIL) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", baglan);
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", pictureBox1.ImageLocation);
            komut.Parameters.AddWithValue("@P4", txtSınıf.Text);
            komut.Parameters.AddWithValue("@P5", txtTelefon.Text);
            komut.Parameters.AddWithValue("@P6", txtMaıl.Text);
            komut.ExecuteNonQuery();
            baglan.Close() ;
            MessageBox.Show("OGRENCI KAYDETME ISLEMI TAMAMLANDI");
        }
    }
}
