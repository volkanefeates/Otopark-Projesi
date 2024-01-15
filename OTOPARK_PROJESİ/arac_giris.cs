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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OTOPARK_PROJESİ
{
    public partial class arac_giris : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        public arac_giris()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            arac_giris_kayit g1 = new arac_giris_kayit();
            g1.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT*FROM otopark_duzenleme where no=@no", baglanti);
            komut2.Parameters.AddWithValue("@no", "1");
            SqlDataReader oku;
            oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                label6.Text = oku["toplam_kat"].ToString();
            }
            baglanti.Close();

            int d1 = Convert.ToInt32(label6.Text);
            int[] top = new int[d1];

            for (int i = 0; i < d1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT*FROM kat_bilgi where kat_sayısı=@kat", baglanti);
                komut.Parameters.AddWithValue("@kat", (i + 1) + ".KAT");
                SqlDataReader oku1 = komut.ExecuteReader();
                while (oku1.Read())
                {
                    label4.Text = oku1["otomobil_adet"].ToString();
                    label5.Text = oku1["tır_adet"].ToString();
                    label6.Text = oku1["motosiklet_adet"].ToString();
                    label7.Text = oku1["otobus_adet"].ToString();
                    label8.Text = oku1["diger_adet"].ToString();
                }
                baglanti.Close();

                if (label4.Text == "")
                {
                    label4.Text = "0";
                }
                if (label5.Text == "")
                {
                    label5.Text = "0";
                }
                if (label6.Text == "")
                {
                    label6.Text = "0";
                }
                if (label7.Text == "")
                {
                    label7.Text = "0";
                }
                if (label8.Text == "")
                {
                    label8.Text = "0";
                }
                top[i] = Convert.ToInt32(label4.Text) + Convert.ToInt32(label5.Text) + Convert.ToInt32(label6.Text) + Convert.ToInt32(label7.Text) + Convert.ToInt32(label8.Text);
            }
            for (int i = 0; i < d1; i++)
            {
                for (int a = 0; a < top[i]; a++)
                {
                    baglanti.Open();
                    SqlCommand komut5 = new SqlCommand("SELECT*FROM kusbakisi_tablo_veri where konumunun_ismi=@ki", baglanti);
                    komut5.Parameters.AddWithValue("@ki", (i+1) + ".KAT" + "-" + ("A" + (a + 1)));
                   
                    SqlDataReader oku1 = komut5.ExecuteReader();
                    while (oku1.Read())
                    {
                        label3.Text = oku1["dolu_boş"].ToString();
                    }
                    baglanti.Close();
                   
                    if (label3.Text=="boş")
                    {
                        label9.Text = (i + 1) + ".KAT" + "-" + ("A" + (a + 1));
                        i = d1;
                        break;
                    }
                    else
                    {
                        
                    }          
                }
            } 
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            String tamzaman = DateTime.Now.ToString();
            
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT*FROM araclarin_giris_cikis_kaydi where PLAKA=@plaka", baglanti);
            komut2.Parameters.AddWithValue("@plaka", textBox1.Text);
            SqlDataReader oku;
            oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                label6.Text = oku["islem_durumu"].ToString();
            }
            baglanti.Close();

            if (label6.Text == "aktif")
            {
                MessageBox.Show("BU PLAKA İLE AKTİF BİR İŞLEM VAR ");
            }
            else
            {
                int yap = 0;
                SqlCommand komut = new SqlCommand();
                SqlDataReader oku1;
                komut = new SqlCommand();
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "Select * From kullanici_veri_kaydi where PLAKA='" + textBox1.Text + "'";
                oku1 = komut.ExecuteReader();

                if (oku1.Read())
                {
                    yap = 1;
                }
                else
                {
                    MessageBox.Show("PLAKA SİSTEMDE TANIMLI DEĞİL. ");
                    textBox1.Text = "";
                }
                baglanti.Close();

                if (yap == 1)
                {
                    if (label9.Text == "")
                    {
                        MessageBox.Show("LÜTFEN ARA TUŞUNA BASINIZ ");
                    }
                    else
                    {
                        baglanti.Open();
                        SqlCommand komut1 = new SqlCommand("Update kusbakisi_tablo_veri set dolu_boş=@db,plaka=@plaka where konumunun_ismi=@ki", baglanti);
                        komut1.Parameters.AddWithValue("@ki", label9.Text);
                        komut1.Parameters.AddWithValue("@db", "dolu");
                        komut1.Parameters.AddWithValue("@plaka", textBox1.Text);
                        MessageBox.Show("GİRİŞ ONAYLANDI  ");
                        komut1.ExecuteNonQuery();
                        baglanti.Close();



                        baglanti.Open();
                        string kayit = "insert into araclarin_giris_cikis_kaydi(PLAKA,ARACIN_KONUMU,ARACIN_GIRIS_SAATI,islem_durumu) values (@plaka,@ak,@ags,@id)";
                        SqlCommand komut6 = new SqlCommand(kayit, baglanti);
                        
                        komut6.Parameters.AddWithValue("@plaka", textBox1.Text);
                        komut6.Parameters.AddWithValue("@ak", label9.Text);
                        komut6.Parameters.AddWithValue("@ags", tamzaman);
                        komut6.Parameters.AddWithValue("@id", "aktif");

                        komut6.ExecuteNonQuery();
                        baglanti.Close();
                    }
                }
                else
                {

                }
            }
        }
    }
}
