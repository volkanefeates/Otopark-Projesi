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
    public partial class kullanici_giris_bilgi : Form
    {
        static string konum ="Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        SqlCommand komut = new SqlCommand();

        public kullanici_giris_bilgi()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ana_menu g1 = new ana_menu();
            g1.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
               
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update kullanici_veri_kaydi set PLAKA=@plaka,ISIM_SOYISIM=@ad,CEP_TELEFONU=@cep,ARAC_TURU=@arac where TC_KIMLIK_NUMARASI=@tc", baglanti);
                komut.Parameters.AddWithValue("@plaka", textBox1.Text);
                komut.Parameters.AddWithValue("@tc", textBox3.Text);
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@cep", textBox4.Text);
                komut.Parameters.AddWithValue("@arac", comboBox1.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("BAŞARIYLA GÜNCELLENDİ");
            }
            else
            {
                MessageBox.Show("ARAMA YERİNDEN VEYA TABLODAN SEÇEREK GÜNCELLEYİNİZ");
            }
        }
        private void kullanici_giris_bilgi_Load(object sender, EventArgs e)
        {
            textBox3.Text = kullanici_giris.giris_baglanti;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT*FROM kullanici_veri_kaydi where TC_KIMLIK_NUMARASI=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", textBox3.Text);
            SqlDataReader oku;
            oku = komut.ExecuteReader();

            while (oku.Read())
            {
                textBox1.Text = oku["PLAKA"].ToString();
                textBox2.Text = oku["ISIM_SOYISIM"].ToString(); 
                textBox3.Text = oku["TC_KIMLIK_NUMARASI"].ToString();
                textBox4.Text = oku["CEP_TELEFONU"].ToString();
                comboBox1.Text = oku["ARAC_TURU"].ToString (); 
            }
            baglanti.Close();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)&& !char.IsSeparator(e.KeyChar);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}

