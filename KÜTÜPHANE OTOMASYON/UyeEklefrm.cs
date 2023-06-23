using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KÜTÜPHANE_OTOMASYON
{
    public partial class UyeEklefrm : Form
    {
        public UyeEklefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti= new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnUyeEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into uye(tc,adsoyad,yas,cinsiyet,telefon,adres,email) values (@tc,@adsoyad,@yas,@cinsiyet,@telefon,@adres,@email)", baglanti);
            komut.Parameters.AddWithValue("@tc",txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@yas", txtYas.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboCinsiyet.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Üye kaydı gerçekleşti.");
            foreach (Control item in Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    
                        item.Text = " ";
                    
           
                } 
                    
                
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
