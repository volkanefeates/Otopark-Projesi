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
    public partial class abonelik_bilgi : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        SqlCommand komut = new SqlCommand();
        public abonelik_bilgi()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ana_menu g1 = new ana_menu();
            g1.Show();
            this.Hide();
        }
        public void doldur()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM abonelik_kayit where plaka like '"+textBox1.Text+"'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            baglanti.Close();
        }

        public void combo_doldur()
        {
            textBox1.Text = abonelik_giris.abon_giris;
            for (int i = 0; i < 100; i++)
            {
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";

                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT*FROM otopark_duzenleme_abonelik where no=@no", baglanti);
                komut.Parameters.AddWithValue("@no", i + 1);
                SqlDataReader oku;
                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    label4.Text = oku["sure"].ToString();
                    label5.Text = oku["ay_veya_yıl"].ToString();
                    label6.Text = oku["abone_fiyat"].ToString();
                }
                baglanti.Close();
                if (label4.Text != "")
                {
                    comboBox1.Items.Add(label4.Text + "  " + label5.Text);
                }
            }
        }
        private void abonelik_bilgi_Load(object sender, EventArgs e)
        {
            combo_doldur();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM kullanici_veri_kaydi where PLAKA=@plaka", baglanti);
            komut.Parameters.AddWithValue("@plaka", textBox1.Text);
            SqlDataReader oku2;
            oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                textBox2.Text = oku2["ARAC_TURU"].ToString();
            }
            baglanti.Close();
            doldur();
            if (dataGridView1.Rows.Count > 1)
            {
                MessageBox.Show("AKTİF ABONELİĞİNİZ BULUNMAKTADIR.");
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button2.Enabled = false;
                comboBox1.Enabled = false;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT*FROM otopark_duzenleme_abonelik where no=@no", baglanti);
            komut.Parameters.AddWithValue("@no", (comboBox1.SelectedIndex + 1));
            SqlDataReader oku;
            oku = komut.ExecuteReader();

            while (oku.Read())
            {
                label4.Text = oku["sure"].ToString();
                label5.Text = oku["ay_veya_yıl"].ToString();
                label6.Text = oku["abone_fiyat"].ToString();
            }
            baglanti.Close();
            textBox3.Text = label6.Text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            String tamzaman = DateTime.Now.ToString();
            if (comboBox1.SelectedIndex>-1)
            {
                baglanti.Open();
                string kayit = "insert into abonelik_kayit(plaka,abonelik_turu_no,baslangıc_tarihi,bitis_tarihi,no) values (@plaka,@atn,@bast,@bitt,@no)";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                komut.Parameters.AddWithValue("@plaka", textBox1.Text);
                komut.Parameters.AddWithValue("@atn", (Convert.ToInt32(comboBox1.SelectedIndex) + 1));
                komut.Parameters.AddWithValue("@bast", tamzaman);
                komut.Parameters.AddWithValue("@no", "abone");

                if (label5.Text == "AYLIK")
                {
                    komut.Parameters.AddWithValue("@bitt", DateTime.Now.AddMonths(Convert.ToInt32(label4.Text)));
                }
                else
                {
                    komut.Parameters.AddWithValue("@bitt", DateTime.Now.AddYears(Convert.ToInt32(label4.Text)));
                }
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("ABONELİK KAYDEDİLDİ..");
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button2.Enabled = false;
                comboBox1.Enabled = false;
                doldur();
            }
            else
            {
                MessageBox.Show("LÜTFEN ABONELİK TÜRÜ SEÇİNİZ");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update abonelik_kayit set plaka=@plaka,abonelik_turu_no=@atn,CEP_TELEFONU=@cep,ARAC_TURU=@arac where TC_KIMLIK_NUMARASI=@tc", baglanti);
            komut.Parameters.AddWithValue("@plaka", textBox1.Text);
          
            komut.ExecuteNonQuery();
           
            baglanti.Close();
            MessageBox.Show("BAŞARIYLA GÜNCELLENDİ");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count==2)
            {
                SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from abonelik_kayit where plaka=@plaka", baglanti);
                komut.Parameters.AddWithValue("@plaka", textBox1.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("BAŞARIYLA İPTAL EDİLDİ");
                MessageBox.Show("YENİ ABONELİK EKLEYEBİLİRSİNİZ");
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button2.Enabled = true;
                comboBox1.Enabled = true;
                doldur();
            }
            else
            {
                MessageBox.Show("TABLODA İPTAL EDİLECEK ABONELİK YOKTUR");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
