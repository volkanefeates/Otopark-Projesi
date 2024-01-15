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
    public partial class otoparkta_duzenleme_yonlendirme : Form
    {
        public otoparkta_duzenleme_yonlendirme()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            admin_giris_ekranı g1 = new admin_giris_ekranı();
            g1.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            otopark_duzenleme g1 = new otopark_duzenleme();
            g1.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            abonelik_düzenleme g1 = new abonelik_düzenleme();
            g1.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
           otopark_duzenleme_admin_düzenleme g1 = new otopark_duzenleme_admin_düzenleme();
            g1.Show();
            this.Hide();
        }
    }
}
