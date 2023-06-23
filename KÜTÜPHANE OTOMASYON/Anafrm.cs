using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace KÜTÜPHANE_OTOMASYON
{
    public partial class Anafrm : Form
    {
        public Anafrm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmanetListelemefrm liste = new EmanetListelemefrm();
            liste.ShowDialog();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UyeEklefrm uyeEklefrm = new UyeEklefrm();
            uyeEklefrm.ShowDialog();
        }

        private void btnUyeListele_Click(object sender, EventArgs e)
        {
            UyeListelefrm uyeListele= new UyeListelefrm();
            uyeListele.ShowDialog();
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            KitapEklefrm kitapEkleme = new KitapEklefrm();
            kitapEkleme.ShowDialog();
        }

        private void btnKitapListele_Click(object sender, EventArgs e)
        {
            KitapListelefrm kitapListele = new KitapListelefrm();   
            kitapListele.ShowDialog();  
        }

        private void btnEmanerVer_Click(object sender, EventArgs e)
        {
            EmanetKitapVermefrm emanetkitapver= new EmanetKitapVermefrm();
            emanetkitapver.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmanetİade_Click(object sender, EventArgs e)
        {
            Kitapİadefrm iade = new Kitapİadefrm(); 
            iade.ShowDialog();
        }
    }
}
