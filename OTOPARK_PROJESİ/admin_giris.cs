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
    public partial class admin_giris : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        SqlCommand komut = new SqlCommand();
        SqlDataReader oku;
        public admin_giris()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");

            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * From otopark_duzenleme_admin where admin_isim='" + textBox1.Text + "'And admin_sifre='" + textBox2.Text + "'";
            oku = komut.ExecuteReader();

            if (oku.Read())
            {
                admin_giris_ekranı g1 = new admin_giris_ekranı();
                g1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("ADMİN ADI VEYA ŞİFRE YANLIŞ TEKRAR DENEYİNİZ");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            baglanti.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ana_menu g1 = new ana_menu();
            g1.Show();
            this.Hide();
        }

        private void admin_giris_Load(object sender, EventArgs e)
        {

        }
    }
}
