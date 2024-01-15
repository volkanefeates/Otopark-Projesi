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
    public partial class otopark_duzenleme_admin_düzenleme : Form
    {
        string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti=new SqlConnection();
        public otopark_duzenleme_admin_düzenleme()
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
            SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update otopark_duzenleme_admin set admin_isim=@ad,admin_sifre=@sifre where no=@no", baglanti);
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            komut.Parameters.AddWithValue("@sifre", textBox2.Text);
            komut.Parameters.AddWithValue("@no", "1");

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("BAŞARIYLA DEĞİŞTİRİLDİ ");
        }
    }
}

