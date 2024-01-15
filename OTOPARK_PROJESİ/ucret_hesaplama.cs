using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OTOPARK_PROJESİ
{
    public partial class ucret_hesaplama : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        TimeSpan fark;
        int a = 0;
        public ucret_hesaplama()
        {
            InitializeComponent();
        }

        private void ucret_hesaplama_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                baglanti.Open();
                SqlCommand komut6 = new SqlCommand("SELECT*FROM abonelik_kayit where plaka=@plaka", baglanti);
                komut6.Parameters.AddWithValue("@plaka", textBox1.Text);
                SqlDataReader oku6;
                oku6 = komut6.ExecuteReader();
                while (oku6.Read())
                {
                    label13.Text = oku6["no"].ToString();
                }
                baglanti.Close();

                if (label13.Text != "abone")
                {
                    baglanti.Open();
                    SqlCommand komut4 = new SqlCommand("SELECT*FROM araclarin_giris_cikis_kaydi where PLAKA=@plaka", baglanti);
                    komut4.Parameters.AddWithValue("@plaka", textBox1.Text);
                    SqlDataReader oku;
                    oku = komut4.ExecuteReader();
                    while (oku.Read())
                    {
                        label6.Text = oku["islem_durumu"].ToString();
                    }
                    baglanti.Close();


                    if (label6.Text != "aktif")
                    {
                        MessageBox.Show("BU PLAKANIN GİRİŞ KAYDI YOKTUR  ");
                        textBox1.Text = "";
                    }
                    else
                    {
                        String tamzaman = DateTime.Now.ToString();

                        baglanti.Open();
                        SqlCommand komut3 = new SqlCommand("SELECT*FROM kullanici_veri_kaydi where PLAKA=@plaka", baglanti);
                        komut3.Parameters.AddWithValue("@plaka", textBox1.Text);
                        SqlDataReader oku1;
                        oku1 = komut3.ExecuteReader();
                        while (oku1.Read())
                        {
                            label7.Text = oku1["ARAC_TURU"].ToString();
                        }
                        baglanti.Close();

                        textBox4.Text = label7.Text;

                        baglanti.Open();
                        SqlCommand komut5 = new SqlCommand("SELECT*FROM otopark_duzenleme where no=@no", baglanti);
                        komut5.Parameters.AddWithValue("@no", "1");
                        SqlDataReader oku2;
                        oku2 = komut5.ExecuteReader();
                        while (oku2.Read())
                        {
                            label8.Text = oku2["otomobil_fiyat"].ToString();
                            label9.Text = oku2["tir_fiyat"].ToString();
                            label10.Text = oku2["motosiklet_fiyat"].ToString();
                            label11.Text = oku2["otobus_fiyat"].ToString();
                            label12.Text = oku2["diger_fiyat"].ToString();

                        }
                        baglanti.Close();

                        baglanti.Open();
                        SqlCommand komut2 = new SqlCommand("SELECT*FROM araclarin_giris_cikis_kaydi where PLAKA=@plaka", baglanti);
                        komut2.Parameters.AddWithValue("@plaka", textBox1.Text);
                        SqlDataReader oku3;
                        oku3 = komut2.ExecuteReader();
                        while (oku3.Read())
                        {
                            label4.Text = oku3["islem_durumu"].ToString();
                            label5.Text = oku3["ARACIN_KONUMU"].ToString();
                            label6.Text = oku3["ARACIN_GIRIS_SAATI"].ToString();
                        }
                        baglanti.Close();

                        TimeSpan fark = DateTime.Now - DateTime.Parse(label6.Text);
                        double fiyat = 0;
                        double a = fark.TotalHours;
                        textBox3.Text = a.ToString();

                        if (label7.Text == "OTOMOBİL")
                        {
                            fiyat = (5 + (a * Convert.ToInt32(label8.Text)));
                        }
                        else if (label7.Text == "MOTOSİKLET")
                        {
                            fiyat = (5 + (a * Convert.ToInt32(label9.Text)));
                        }
                        else if (label7.Text == "TIR")
                        {
                            fiyat = (5 + (a * Convert.ToInt32(label10.Text)));
                        }
                        else if (label7.Text == "OTOBÜS")
                        {
                            fiyat = (5 + (a * Convert.ToInt32(label11.Text)));
                        }
                        else if (label7.Text == "DİĞER")
                        {
                            fiyat = (5 + (a * Convert.ToInt32(label12.Text)));
                        }

                        int sayi = Convert.ToInt32(Math.Round(fiyat, 2));
                        textBox2.Text = sayi.ToString();
                        label13.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("ABONE OLDUĞU İÇİN ÜCRET YOKTUR .");
                    a += 1;
                }
            }
            else
            {
                MessageBox.Show("PLAKA GİRİNİZ ÖNCELİKLE");     
            }
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String tamzaman = DateTime.Now.ToString();

            if (textBox4.Text!=""||a==1)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update araclarin_giris_cikis_kaydi set ODEDIGI_TUTAR=@ot,islem_durumu=@id,ARACIN_CIKIS_SAATI = @acs,ARACIN_DURDUGU_SAAT = @ads where PLAKA=@plaka", baglanti);
                komut.Parameters.AddWithValue("@plaka", textBox1.Text);
                komut.Parameters.AddWithValue("@acs", tamzaman);
                komut.Parameters.AddWithValue("@ads", fark.TotalHours);
                komut.Parameters.AddWithValue("@ot", textBox2.Text);
                komut.Parameters.AddWithValue("@id", "bitti");
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("Update kusbakisi_tablo_veri set plaka=@plaka,dolu_boş=@db where konumunun_ismi=@ki", baglanti);
                komut1.Parameters.AddWithValue("@ki", label5.Text);
                komut1.Parameters.AddWithValue("@plaka", "");
                komut1.Parameters.AddWithValue("@db", "boş");
                komut1.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("ÇIKIŞ ONAYLANDI");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("ÖNCE PLAKA GİRİN ONDAN SONRA HESAPLAYINIZ");
            }
            
           
        }
    }
}
