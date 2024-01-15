using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTOPARK_PROJESİ
{
    public partial class otopark_kusbakisii : Form
    {
        static string konum = "Data Source=VOLQAN\\SQLEXPRESS;Initial Catalog=otopark_veri_tabani;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(konum);
        GroupBox box = new GroupBox();
        public otopark_kusbakisii()
        {
            InitializeComponent();
        }
        private void otopark_kusbakisii_Load(object sender, EventArgs e)
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
            for (int i = 0; i < d1; i++)
            {
                comboBox1.Items.Add((i + 1) + ".KAT");
            }
            if (d1 > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                    label1.Text = oku1["otomobil_adet"].ToString();
                    label2.Text = oku1["tır_adet"].ToString();
                    label3.Text = oku1["motosiklet_adet"].ToString();
                    label4.Text = oku1["otobus_adet"].ToString();
                    label5.Text = oku1["diger_adet"].ToString();
                }
                baglanti.Close();
                if (label1.Text == "")
                {
                    label1.Text = "0";
                }
                if (label2.Text == "")
                {
                    label2.Text = "0";
                }
                if (label3.Text == "")
                {
                    label3.Text = "0";
                }
                if (label4.Text == "")
                {
                    label4.Text = "0";
                }
                if (label5.Text == "")
                {
                    label5.Text = "0";
                }
                top[i] = Convert.ToInt32(label1.Text) + Convert.ToInt32(label2.Text) + Convert.ToInt32(label3.Text) + Convert.ToInt32(label4.Text) + Convert.ToInt32(label5.Text);
            }
            int toplam = 0;
            for (int i = 0; i < Convert.ToInt32(d1.ToString()); i++)
            {
                toplam += top[i];
            }
            
            Button[] buttonArray = new Button[toplam];
            box.Controls.Clear();
           
            for (int c = 0; c < comboBox1.Items.Count; c++)
            {
                box.Location = new Point(10, 30);
                box.Size = new Size(1800, 800);
                box.Name = "box1";
                this.Controls.Add(box);
                if (comboBox1.SelectedIndex == c)
                {
                    int horizotal = 30;
                    int vertical = 40;

                    for (int i = 0; i < Convert.ToInt32(top[c].ToString()); i++)
                    {
                        baglanti.Open();
                        SqlCommand komut5 = new SqlCommand("SELECT*FROM kusbakisi_tablo_veri where konumunun_ismi=@ki", baglanti);
                        komut5.Parameters.AddWithValue("@ki", (comboBox1.SelectedIndex + 1) + ".KAT" + "-" + ("A" + (i + 1)));
                        SqlDataReader oku5 = komut5.ExecuteReader();
                        while (oku5.Read())
                        {
                            label6.Text = oku5["dolu_boş"].ToString();
                            label8.Text = oku5["plaka"].ToString();
                        }
                        baglanti.Close();
                        if (label6.Text != "boş")
                        {
                            if ((i + 1) % 16 == 0)
                            {
                                if ((i + 1) % 9 == 0)
                                {
                                    horizotal += 50;
                                }
                                buttonArray[i] = new Button();
                                buttonArray[i].Name = i.ToString();
                                buttonArray[i].Text = "      A" + (i + 1) + "        " + label8.Text;
                                buttonArray[i].Size = new Size(80, 110);
                                buttonArray[i].Location = new Point(horizotal, vertical);
                                buttonArray[i].BackColor = Color.Red;
                                horizotal = 30;
                                vertical += 135;
                                box.Controls.Add(buttonArray[i]);
                            }
                            else
                            {
                                if ((i + 1) % 8 == 0)
                                {
                                    horizotal += 50;
                                }
                                buttonArray[i] = new Button();
                                buttonArray[i].Name = i.ToString();
                                buttonArray[i].Text = "      A" + (i + 1) + "        " + label8.Text;
                                buttonArray[i].Size = new Size(80, 110);
                                buttonArray[i].Location = new Point(horizotal, vertical);
                                buttonArray[i].BackColor = Color.Red;
                                horizotal += 80;
                                box.Controls.Add(buttonArray[i]);
                            }
                        }
                        else
                        {
                            if ((i + 1) % 16 == 0)
                            {
                                if ((i + 1) % 9 == 0)
                                {
                                    horizotal += 50;
                                }
                                buttonArray[i] = new Button();
                                buttonArray[i].Name = i.ToString();
                                buttonArray[i].Text = "      A" + (i + 1) + "        " + label8.Text;
                                buttonArray[i].Size = new Size(80, 110);
                                buttonArray[i].Location = new Point(horizotal, vertical);
                                buttonArray[i].BackColor = Color.LightGreen;
                                horizotal = 30;
                                vertical += 135;
                                box.Controls.Add(buttonArray[i]);
                            }
                            else
                            {
                                if ((i + 1) % 8 == 0)
                                {
                                    horizotal += 50;
                                }
                                buttonArray[i] = new Button();
                                buttonArray[i].Name = i.ToString();
                                buttonArray[i].Text = "      A" + (i + 1) + "        " + label8.Text;
                                buttonArray[i].Size = new Size(80, 110);
                                buttonArray[i].Location = new Point(horizotal, vertical);
                                buttonArray[i].BackColor = Color.LightGreen;
                                horizotal += 80;
                                box.Controls.Add(buttonArray[i]);
                            }
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }
    }
}

