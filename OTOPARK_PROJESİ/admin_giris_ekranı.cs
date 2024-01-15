using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTOPARK_PROJESİ
{
    public partial class admin_giris_ekranı : Form
    {
        public admin_giris_ekranı()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            arac_giris g1 = new arac_giris();   
            g1.Show();
            this.Hide();    
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ucret_hesaplama g1 = new ucret_hesaplama();
            g1.Show();  
            this.Hide();    
        }
        private void button3_Click(object sender, EventArgs e)
        {
            arac_mevcut_durum g1 = new arac_mevcut_durum();
            g1.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            otopark_kusbakisii g1 = new otopark_kusbakisii();
            g1.Show();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            kullanici_veri_kaydi g1 = new kullanici_veri_kaydi();
            g1.Show();
            this.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        { 
        otoparkta_duzenleme_yonlendirme g1 = new otoparkta_duzenleme_yonlendirme();
            g1.Show();
            this.Hide();
        }
        private void button7_Click(object sender, EventArgs e)
        { 
            ana_menu g1 = new ana_menu();   
            g1.Show();
            this.Hide();
        }
    }
}
