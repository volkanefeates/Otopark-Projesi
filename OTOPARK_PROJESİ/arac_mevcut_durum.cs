using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTOPARK_PROJESİ
{
    public partial class arac_mevcut_durum : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        SqlCommand komut = new SqlCommand();
        SqlDataReader oku;
        public arac_mevcut_durum()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");

            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT*FROM araclarin_giris_cikis_kaydi where PLAKA='" + textBox1.Text + "'And islem_durumu='" + "aktif" + "'";
            oku = komut.ExecuteReader();

            if (oku.Read())
            {
                textBox3.Text = oku["ARACIN_KONUMU"].ToString();
                textBox2.Text = oku["ARACIN_GIRIS_SAATI"].ToString();
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                MessageBox.Show("PLAKA MEVCUT DEĞİL VEYA ARAÇ PARK HALİNDE DEĞİLDİR. ");
            }
            baglanti.Close();
        }
    }
}
