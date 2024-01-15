using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OTOPARK_PROJESİ
{
    public partial class kullanici_veri_kaydi : Form
    {
        SqlConnection baglanti=new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
        public void doldur()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kullanici_veri_kaydi", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            baglanti.Close();
        }
        public kullanici_veri_kaydi()
        {
            InitializeComponent();
        }
        public void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
        }
        private void kullanici_veri_kaydi_Load(object sender, EventArgs e)
        {
            doldur();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 =new admin_giris_ekranı();
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
                    label7.Text = oku1["PLAKA"].ToString();
                    label8.Text = oku1["TC_KIMLIK_NUMARASI"].ToString();
                    label9.Text = oku1["CEP_TELEFONU"].ToString();

                    if (textBox1.Text == label7.Text)
                    {
                        MessageBox.Show("BU PLAKA KAYITLIDIR ");
                        textBox1.Text = "";
                    }
                    else if (textBox3.Text == label8.Text)
                    {
                        MessageBox.Show("BU TC KİMLİK NUMARASI KAYITLIDIR ");
                        textBox3.Text = "";
                    }
                    else if (textBox4.Text == label9.Text)
                    {
                        MessageBox.Show("BU CEP TELEFONU NUMARASI KAYITLIDIR ");
                        textBox4.Text = "";
                    }
                }
                baglanti.Close();
            } while (sayac != 0);
            if (label7.Text != textBox1.Text || label8.Text != textBox2.Text || label9.Text != textBox4.Text)
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
            doldur();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
           textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
           textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
          
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
           
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="")
            {
                SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update kullanici_veri_kaydi set PLAKA=@plaka,ISIM_SOYISIM=@ad,CEP_TELEFONU=@cep,ARAC_TURU=@arac where TC_KIMLIK_NUMARASI=@tc", baglanti);
                komut.Parameters.AddWithValue("@plaka", textBox1.Text);
                komut.Parameters.AddWithValue("@tc", textBox2.Text);
                komut.Parameters.AddWithValue("@ad", textBox3.Text);
                komut.Parameters.AddWithValue("@cep", textBox4.Text);
                komut.Parameters.AddWithValue("@arac", comboBox1.Text);
                komut.ExecuteNonQuery();
                temizle();
                baglanti.Close();
                MessageBox.Show("BAŞARIYLA GÜNCELLENDİ");
                doldur();
            }
            else
            {
                MessageBox.Show("ARAMA YERİNDEN VEYA TABLODAN SEÇEREK GÜNCELLEYİNİZ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="")
            {
                SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from kullanici_veri_kaydi where PLAKA=@plaka", baglanti);
                komut.Parameters.AddWithValue("@plaka", textBox1.Text);
                komut.ExecuteNonQuery();
                temizle();
              
                baglanti.Close();
                MessageBox.Show("BAŞARIYLA SİLİNDİ");
                doldur();
            }
            else
            {
                MessageBox.Show("ARAMA YERİNDEN VEYA TABLODAN SEÇEREK SİLMEK İSTEDİKLERİNİZE TIKLAYIN ");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex==0)
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kullanici_veri_kaydi where PLAKA like '%"+textBox5.Text+"%'", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                baglanti.Close();
            }
            else if(comboBox2.SelectedIndex==1)
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kullanici_veri_kaydi where TC_KIMLIK_NUMARASI like '%" + textBox5.Text + "%'", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                baglanti.Close();
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kullanici_veri_kaydi where ISIM_SOYISIM like '%" + textBox5.Text + "%'", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                baglanti.Close();
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kullanici_veri_kaydi where CEP_TELEFONU like '%" + textBox5.Text + "%'", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                baglanti.Close();
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM kullanici_veri_kaydi where ARAC_TURU like '%" + textBox5.Text + "%'", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("ÖNCELİKLE ARAMA TÜRÜNÜZÜ SEÇİNİZ ");
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            doldur();
            textBox5.Text ="";
            comboBox2.SelectedIndex = -1;
        }
    }
}
