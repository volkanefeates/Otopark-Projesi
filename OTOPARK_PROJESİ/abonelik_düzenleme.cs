using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OTOPARK_PROJESİ
{
    public partial class abonelik_düzenleme : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
        public void doldur()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM otopark_duzenleme_abonelik", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            baglanti.Close();
        }
        private void temizle()
        {
            numericUpDown2.ResetText();
            comboBox3.Text = "SEÇİNİZ";
            textBox12.Text = "";
        }
        public abonelik_düzenleme()
        {
            InitializeComponent();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kayit = "insert into otopark_duzenleme_abonelik(sure,ay_veya_yıl,abone_fiyat) values (@sure,@ay,@fiy)";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@sure", numericUpDown2.Value.ToString());
            komut.Parameters.AddWithValue("@ay", comboBox3.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@fiy", textBox12.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            doldur();
            temizle();
            MessageBox.Show("ABONELİK KAYDEDİLDİ..");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (numericUpDown2.Value!=0)
                {
                    if (comboBox3.SelectedIndex!=-1)
                    {
                        if (textBox12.Text!="")
                        {
                            SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
                            baglanti.Open();
                            SqlCommand komut = new SqlCommand("Update otopark_duzenleme_abonelik set sure=@sure,ay_veya_yıl=@ay,abone_fiyat=@fiy where no=@no", baglanti);
                            komut.Parameters.AddWithValue("@sure", numericUpDown2.Value.ToString());
                            komut.Parameters.AddWithValue("@ay", comboBox3.SelectedItem.ToString());
                            komut.Parameters.AddWithValue("@fiy", textBox12.Text.ToString());
                            komut.Parameters.AddWithValue("@no", Convert.ToInt32(label1.Text));
                            komut.ExecuteNonQuery();
                            temizle();
                            MessageBox.Show("BAŞARIYLA DEĞİŞTİRİLDİ ");
                        }
                        else
                        {
                            MessageBox.Show("FİYAT BOŞ BIRAKILIMAZ , FİYATI 0 YAPMAK İSTİYORSANIZ 0'I SEÇİNİZ ");

                        }
                    }
                    else
                    {
                        MessageBox.Show("LÜTFEN BİRŞEY SEÇİNİZ ");

                    }
                }
                else 
                {
                    MessageBox.Show("ABONELİK SÜRESİ 0 OLAMAZ ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (baglanti != null)
                {
                    baglanti.Close();
                    doldur();
                }
            }
        }
        private void abonelik_düzenleme_Load(object sender, EventArgs e)
        {
            doldur();

            label2.Text = dataGridView1.RowCount.ToString();
            abonelik_sayisi = label2.Text;
        }
        public static string abonelik_sayisi;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox12.Text!="")
            {
                SqlConnection baglanti = new SqlConnection("Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from otopark_duzenleme_abonelik where no=@no", baglanti);

                komut.Parameters.AddWithValue("@no", Convert.ToInt32(label1.Text));
                komut.ExecuteNonQuery();

                temizle();
                baglanti.Close();
                doldur();
                MessageBox.Show("BAŞARIYLA SİLİNDİ");
            }
            else
            {
                MessageBox.Show("LÜTFEN TABLODAN SEÇİNİZ");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
     
            numericUpDown2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox12.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            label1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
