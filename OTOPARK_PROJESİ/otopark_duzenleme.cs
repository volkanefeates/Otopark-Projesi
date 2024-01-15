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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OTOPARK_PROJESİ
{

    public partial class otopark_duzenleme : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        public otopark_duzenleme()
        {
            InitializeComponent();
        }
        private void otopark_duzenleme_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT*FROM otopark_duzenleme where no=@no", baglanti);
            komut.Parameters.AddWithValue("@no", "1");
            SqlDataReader oku;
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Text = oku["otopark_turu"].ToString();
                numericUpDown1.Value = Convert.ToInt32(oku["toplam_kat"].ToString());
                textBox10.Text = oku["otomobil_fiyat"].ToString();
                textBox9.Text = oku["tir_fiyat"].ToString();
                textBox8.Text = oku["motosiklet_fiyat"].ToString();
                textBox7.Text = oku["otobus_fiyat"].ToString();
                textBox6.Text = oku["diger_fiyat"].ToString();
            }
            baglanti.Close();

            int a = Convert.ToInt32(numericUpDown1.Value);
            if (a>1)
            {
                comboBox2.SelectedIndex = 0;
            }
            for (int i = 0; i < a; i++)
            {
                if (comboBox1.SelectedIndex == i)
                {
                    baglanti.Open();
                    komut = new SqlCommand("SELECT*FROM kat_bilgi where kat_sayısı_ekleme=@kse", baglanti);
                    komut.Parameters.AddWithValue("@kse", i);
                    oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        if (oku["otomobil_adet"].ToString() != "")
                        {
                            checkBox1.Checked = true;
                        }
                        if (oku["tır_adet"].ToString() != "")
                        {
                            checkBox2.Checked = true;
                        }
                        if (oku["motosiklet_adet"].ToString() != "")
                        {
                            checkBox3.Checked = true;
                        }
                        if (oku["otobus_adet"].ToString() != "")
                        {
                            checkBox4.Checked = true;
                        }
                        if (oku["diger_adet"].ToString() != "")
                        {
                            checkBox5.Checked = true;
                        }
                        textBox1.Text = oku["otomobil_adet"].ToString();
                        textBox2.Text = oku["tır_adet"].ToString();
                        textBox3.Text = oku["motosiklet_adet"].ToString();
                        textBox4.Text = oku["otobus_adet"].ToString();
                        textBox5.Text = oku["diger_adet"].ToString();
                    }
                    baglanti.Close();
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt16(comboBox2.SelectedIndex);
            baglanti.Open();
            string kayit = "insert into kat_bilgi(no,kat_sayısı,otomobil_adet,tır_adet,motosiklet_adet,otobus_adet,diger_adet,kat_sayısı_ekleme) values (@no,@kat,@oto,@tır,@motor,@otobus,@diger,@kse)";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@no", "1");
            komut.Parameters.AddWithValue("@kat", (a + 1) + ".KAT");
            komut.Parameters.AddWithValue("@oto", textBox1.Text.ToString());
            komut.Parameters.AddWithValue("@tır", textBox2.Text.ToString());
            komut.Parameters.AddWithValue("@otobus", textBox3.Text.ToString());
            komut.Parameters.AddWithValue("@motor", textBox4.Text.ToString());
            komut.Parameters.AddWithValue("@diger", textBox5.Text.ToString());
            komut.Parameters.AddWithValue("@kse", a);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("BAŞARIYLA KAYDEDİLDİ");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from otopark_duzenleme ", baglanti);
            SqlDataReader oku1 = komut1.ExecuteReader();
            while (oku1.Read())
            {
                label5.Text = oku1["no"].ToString();
                if ("1" == label5.Text)
                {
                    MessageBox.Show("BU OTOPARK KAYITLIDIR LÜTFEN GÜNCELLEME YAPINIZ");
                    textBox1.Text = "";
                }
            }
            baglanti.Close();

            if (label5.Text != "1")
            {
                string kat_say = numericUpDown1.Value.ToString();

                baglanti.Open();
                string kayit = "insert into otopark_duzenleme(no,otopark_turu,toplam_kat,otomobil_fiyat,tir_fiyat,motosiklet_fiyat,otobus_fiyat,diger_fiyat) values (@no,@ott,@tk,@of,@tf,@mf,@otf,@df)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);

                if (comboBox1.SelectedIndex != -1)
                {
                    komut.Parameters.AddWithValue("@no", "1");
                    komut.Parameters.AddWithValue("@ott", comboBox1.Text.ToString());
                    komut.Parameters.AddWithValue("@tk", kat_say);
                    komut.Parameters.AddWithValue("@of", textBox10.Text.ToString());
                    komut.Parameters.AddWithValue("@tf", textBox9.Text.ToString());
                    komut.Parameters.AddWithValue("@mf", textBox8.Text.ToString());
                    komut.Parameters.AddWithValue("@otf", textBox7.Text.ToString());
                    komut.Parameters.AddWithValue("@df", textBox6.Text.ToString());
                }
                else
                {
                    MessageBox.Show("LÜTFEN OTOPARK TÜRÜ SEÇİNİZ");
                }
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("BAŞARIYLA KAYDEDİLDİ");
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int a = (Int32)(numericUpDown1.Value);
            comboBox2.Items.Clear();
            for (int i = 1; i <= a; i++)
            {
                comboBox2.Items.Add(i + ".KAT");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
                textBox1.Text = "";
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox1.Text = "";
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
                textBox1.Text = "";
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox1.Text = "";
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            int a = Convert.ToInt32(comboBox2.SelectedIndex);

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT*FROM kat_bilgi where kat_sayısı_ekleme=@kse", baglanti);
            SqlDataReader oku;
            komut.Parameters.AddWithValue("@kse", a);
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["otomobil_adet"].ToString() != "")
                {
                    checkBox1.Checked = true;
                }
                if (oku["tır_adet"].ToString() != "")
                {
                    checkBox2.Checked = true;
                }
                if (oku["motosiklet_adet"].ToString() != "")
                {
                    checkBox3.Checked = true;
                }
                if (oku["otobus_adet"].ToString() != "")
                {
                    checkBox4.Checked = true;
                }
                if (oku["diger_adet"].ToString() != "")
                {
                    checkBox5.Checked = true;
                }
                textBox1.Text = oku["otomobil_adet"].ToString();
                textBox2.Text = oku["tır_adet"].ToString();
                textBox3.Text = oku["motosiklet_adet"].ToString();
                textBox4.Text = oku["otobus_adet"].ToString();
                textBox5.Text = oku["diger_adet"].ToString();
            }
            baglanti.Close();
        }
        public void sil()
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("Delete from kusbakisi_tablo_veri where kat_sayisi=@kat", baglanti);
            komut3.Parameters.AddWithValue("@kat", (comboBox2.SelectedIndex + 1) + ".KAT");
            komut3.ExecuteNonQuery();
            baglanti.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update kat_bilgi set no=@no,otomobil_adet=@ota,tır_adet=@ta,motosiklet_adet=@ma,otobus_adet=@oa,diger_adet=@da where kat_sayısı_ekleme=@kat", baglanti);
            komut.Parameters.AddWithValue("@no", "1");
            komut.Parameters.AddWithValue("@kat", comboBox2.SelectedIndex);
            komut.Parameters.AddWithValue("@ota", textBox1.Text.ToString());
            komut.Parameters.AddWithValue("@ta", textBox2.Text.ToString());
            komut.Parameters.AddWithValue("@ma", textBox3.Text.ToString());
            komut.Parameters.AddWithValue("@oa", textBox4.Text.ToString());
            komut.Parameters.AddWithValue("@da", textBox5.Text.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("BAŞARIYLA GÜNCELLENDİ");

            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }
            int top = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text) + Convert.ToInt32(textBox5.Text);
            sil();
            for (int d = 0; d < top; d++)
            {
                baglanti.Open();
                string kayit = "insert into kusbakisi_tablo_veri(kat_sayisi,konumunun_ismi,dolu_boş) values (@ks,@ki,@db)";
                SqlCommand komut6 = new SqlCommand(kayit, baglanti);
                komut6.Parameters.AddWithValue("@ks", (comboBox2.SelectedIndex + 1) + ".KAT");
                komut6.Parameters.AddWithValue("@ki", (comboBox2.SelectedIndex + 1) + ".KAT" + "-" + ("A" + (d + 1)));
                komut6.Parameters.AddWithValue("@db", "boş");
                komut6.ExecuteNonQuery();
                baglanti.Close();
            }
            int a = Convert.ToInt32(comboBox2.SelectedIndex);

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT*FROM kat_bilgi where kat_sayısı_ekleme=@kse", baglanti);
            SqlDataReader oku1;
            komut2.Parameters.AddWithValue("@kse", a);
            oku1 = komut2.ExecuteReader();
            while (oku1.Read())
            {
                if (oku1["otomobil_adet"].ToString() != "")
                {
                    checkBox1.Checked = true;
                }
                if (oku1["tır_adet"].ToString() != "")
                {
                    checkBox2.Checked = true;
                }
                if (oku1["motosiklet_adet"].ToString() != "")
                {
                    checkBox3.Checked = true;
                }
                if (oku1["otobus_adet"].ToString() != "")
                {
                    checkBox4.Checked = true;
                }
                if (oku1["diger_adet"].ToString() != "")
                {
                    checkBox5.Checked = true;
                }
                textBox1.Text = oku1["otomobil_adet"].ToString();
                textBox2.Text = oku1["tır_adet"].ToString();
                textBox3.Text = oku1["motosiklet_adet"].ToString();
                textBox4.Text = oku1["otobus_adet"].ToString();
                textBox5.Text = oku1["diger_adet"].ToString();
            }
            baglanti.Close();
        }
        public static string güncelle;
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update otopark_duzenleme set otopark_turu=@ototur,toplam_kat=@tk,otomobil_fiyat=@otof,tir_fiyat=@tf,motosiklet_fiyat=@mf,otobus_fiyat=@of,diger_fiyat=@df where no=@no", baglanti);
            komut.Parameters.AddWithValue("@no", "1");
            komut.Parameters.AddWithValue("@ototur", comboBox1.Text.ToString());
            komut.Parameters.AddWithValue("@tk", numericUpDown1.Value.ToString());
            komut.Parameters.AddWithValue("@otof", textBox10.Text.ToString());
            komut.Parameters.AddWithValue("@tf", textBox9.Text.ToString());
            komut.Parameters.AddWithValue("@mf", textBox8.Text.ToString());
            komut.Parameters.AddWithValue("@of", textBox7.Text.ToString());
            komut.Parameters.AddWithValue("@df", textBox6.Text.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("BAŞARIYLA GÜNCELLENDİ");
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
