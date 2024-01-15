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

namespace OTOPARK_PROJESİ
{
    public partial class kullanici_giris : Form
    {
        string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection();
        SqlCommand komut = new SqlCommand();
        SqlDataReader oku;
        public kullanici_giris()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ana_menu g1 = new ana_menu();
            g1.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
         
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * From kullanici_veri_kaydi where PLAKA='" + textBox1.Text + "'And TC_KIMLIK_NUMARASI='" + textBox2.Text + "'";
            oku = komut.ExecuteReader();

            if (oku.Read())
            {
                kullanici_giris_bilgi g1 = new kullanici_giris_bilgi();
                giris_baglanti = textBox2.Text;
                g1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("PLAKA VEYA TC KİMLİK NUMARASI YANLIŞ TEKRAR DENEYİNİZ");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            baglanti.Close();
        }
        public static string giris_baglanti;
        private void button2_Click(object sender, EventArgs e)
        {
            kullanaci_giris_kayıt g1 = new kullanaci_giris_kayıt();
            g1.Show();
            this.Hide();
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void kullanici_giris_Load(object sender, EventArgs e)
        {

        }
    }
}
