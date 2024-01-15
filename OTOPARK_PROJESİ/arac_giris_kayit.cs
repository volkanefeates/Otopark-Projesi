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
    public partial class arac_giris_kayit : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        public arac_giris_kayit()
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
            int sayac = 0;
            do
            {
                baglanti.Open();
                SqlCommand komut1 = new SqlCommand("select * from kullanici_veri_kaydi ", baglanti);
                SqlDataReader oku1 = komut1.ExecuteReader();
                while (oku1.Read())
                {
                    label6.Text = oku1["PLAKA"].ToString();
                    label7.Text = oku1["TC_KIMLIK_NUMARASI"].ToString();
                    label8.Text = oku1["CEP_TELEFONU"].ToString();

                    if (textBox1.Text == label6.Text)
                    {
                        MessageBox.Show("BU PLAKA KAYITLIDIR ");
                        textBox1.Text = "";
                    }
                    else if (textBox3.Text == label7.Text)
                    {
                        MessageBox.Show("BU TC KİMLİK NUMARASI KAYITLIDIR ");
                        textBox3.Text = "";
                    }
                    else if (textBox4.Text == label8.Text)
                    {
                        MessageBox.Show("BU CEP TELEFONU NUMARASI KAYITLIDIR ");
                        textBox4.Text = "";
                    }
                }
                baglanti.Close();
            } while (sayac != 0);
            if (label6.Text != textBox1.Text || label7.Text != textBox2.Text || label8.Text != textBox4.Text)
            {
                if (textBox1.Text != "")
                {
                    if (textBox2.Text != "")
                    {
                        if (textBox3.TextLength == 11)
                        {
                            if (textBox3.Text != "")
                            {
                                if (textBox4.Text != "")
                                {
                                    if (textBox4.TextLength == 11)
                                    {
                                        if (comboBox1.Text != "SEÇİNİZ")
                                        {
                                            baglanti.Open();
                                            string kayit = "insert into kullanici_veri_kaydi(PLAKA,TC_KIMLIK_NUMARASI,ISIM_SOYISIM,CEP_TELEFONU,ARAC_TURU) values (@plaka,@tc,@ad,@cep,@arac)";
                                            SqlCommand komut = new SqlCommand(kayit, baglanti);
                                            komut.Parameters.AddWithValue("@plaka", textBox1.Text.ToString());
                                            komut.Parameters.AddWithValue("@ad", textBox2.Text.ToString());
                                            komut.Parameters.AddWithValue("@tc", textBox3.Text.ToString());
                                            komut.Parameters.AddWithValue("@cep", textBox4.Text.ToString());
                                            komut.Parameters.AddWithValue("@arac", comboBox1.Text.ToString());

                                            komut.ExecuteNonQuery();
                                            baglanti.Close();
                                            MessageBox.Show("KAYIT EKLENDİ");
                                            textBox1.Text = "";
                                            textBox2.Text = "";
                                            textBox3.Text = "";
                                            textBox4.Text = "";
                                            comboBox1.SelectedIndex = -1;

                                        }
                                        else
                                        {
                                            MessageBox.Show("ARAÇ TÜRÜ  BOŞ BIRAKILAMAZ");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("EKSİK VEYA HATALI GİRDİNİZ. TELEFON NUMARASINI SIFIR GİRİP TEKRAR DENEYİNİZ");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("TELEFON NUMARASI BOŞ BIRAKILAMAZ");
                                }
                            }
                            else
                            {
                                MessageBox.Show("İSİM SOYİSİM BOŞ BIRAKILAMAZ");
                            }
                        }
                        else
                        {
                            MessageBox.Show("TC KİMLİK NUMARASI 11 HANEDEN KÜÇÜK OLAMAZ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("TC KİMLİK NUMARASI BOŞ BIRAKILAMAZ");
                    }
                }
                else
                {
                    MessageBox.Show("PLAKA BOŞ BIRAKILAMAZ");
                }
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
    }
}
