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
    public partial class ana_menu : Form
    {
        public ana_menu()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            kullanici_giris g1 = new kullanici_giris();
            g1.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            abonelik_giris g1 = new abonelik_giris();   
            g1.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            admin_giris g1 = new admin_giris();
            g1.Show();
            this.Hide();
        }
    }
}
