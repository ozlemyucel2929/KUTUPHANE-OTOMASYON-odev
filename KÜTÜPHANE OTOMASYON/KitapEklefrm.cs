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

namespace KÜTÜPHANE_OTOMASYON
{
    public partial class KitapEklefrm : Form
    {
        public KitapEklefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void KitapEklefrm_Load(object sender, EventArgs e)
        {

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kitap(barkodno,kitapadi,turu,Kitapsayisi,yazari,yayinevi,rafno) values (@barkodno,@kitapadi,@turu,@Kitapsayisi,@yazari,@yayinevi,@rafno)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@Kitapsayisi", txtKitapSayisi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap kaydı gerçekleşti.");
            foreach (Control item in Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                   
                        item.Text = " ";
                    
                  

                }
            }
        }

        private void txtRafNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
