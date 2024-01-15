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

namespace OTOPARK_PROJESİ
{
    public partial class abonelik_giris : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        SqlCommand komut = new SqlCommand();
        SqlDataReader oku;
        public abonelik_giris()
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
            baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");

            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * From kullanici_veri_kaydi where PLAKA='" + textBox1.Text + "'And TC_KIMLIK_NUMARASI='" + textBox3.Text + "'";
            oku = komut.ExecuteReader();

            if (oku.Read())
            {
                abon_giris = textBox1.Text;
                abonelik_bilgi g1 = new abonelik_bilgi();
                g1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("PLAKA  VEYA TC KİMLİK YANLIŞ TEKRAR DENEYİNİZ");
                textBox1.Text = "";
                textBox3.Text = "";
            }
            baglanti.Close();
            
        }
        public static string abon_giris;
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void abonelik_giris_Load(object sender, EventArgs e)
        {

        }
    }
}
