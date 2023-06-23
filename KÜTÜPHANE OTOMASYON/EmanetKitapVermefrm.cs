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
    public partial class EmanetKitapVermefrm : Form
    {


        public EmanetKitapVermefrm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        DataSet ds = new DataSet();

        private void kitapsayisi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapsayisi) from sepet",baglanti);
            lblKitapSayi.Text = komut.ExecuteScalar().ToString();  
            baglanti.Close();   

        }
               


        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from sepet ",baglanti);
            adptr.Fill(ds, "sepet");
            dataGridView1.DataSource = ds.Tables["sepet"];  
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into sepet (barkodno,kitapadi,yazari,yayinevi,rafno,kitapsayisi,teslimtarihi,iadetarihi) values (@barkodno,@kitapadi,@yazari,@yayinevi,@rafno,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);

            komut.Parameters.AddWithValue("barkodno",txtBarkodNo.Text);
            komut.Parameters.AddWithValue("kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("yayinevi", txtYayinEvi.Text);
            komut.Parameters.AddWithValue("rafno", txtRafNo.Text);
            komut.Parameters.AddWithValue("kitapsayisi", int.Parse(txtKitapSayisi.Text));
            komut.Parameters.AddWithValue("teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("iadetarihi", dateTimePicker2.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kitap listeye eklendi","EKLEME");
            ds.Tables["sepet"].Clear();
            sepetlistele();

            lblKitapSayi.Text = " ";
            kitapsayisi();
            foreach (Control item in grpKitapBilgi.Controls)
            {
                if (item is TextBox)
                {
                    if (item!= txtKitapSayisi)
                    {
                        item.Text = " ";
                    }
                }
            }
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Uye where tc like'"+txtTc.Text+"'" ,baglanti);
            SqlDataReader read = komut.ExecuteReader();
           while (read.Read()) {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
         
          
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select sum(kitapsayisi) from VerilenKitaplar where tc='"+ txtTc.Text+"'", baglanti);
            lblKayitliKitapSayisi.Text = komut5.ExecuteScalar().ToString();
            baglanti.Close() ;  
            if (txtTc.Text == " ")
            {
                foreach (Control item in grpUyeBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = " ";
                       
                    }
                    
                }
                lblKayitliKitapSayisi.Text = " ";
            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kitap where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader(); 
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString() ;
                txtYazari.Text = read["yazari"].ToString();
                txtYayinEvi.Text = read["yayinevi"].ToString();
                txtRafNo.Text = read["rafno"].ToString();
                
            }
            baglanti.Close();
            if (txtBarkodNo.Text==" ")
            {
                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = " ";
                        }

                    }
                }
            }
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Silme İşlemi tamamlandı.","SİLME");
            ds.Tables["sepet"].Clear();
            sepetlistele();
            lblKitapSayi.Text = " ";
            kitapsayisi();
        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {

            if (lblKitapSayi.Text!=" ")
            {
                  if (lblKayitliKitapSayisi.Text == "" && int.Parse(lblKitapSayi.Text) <= 3 || lblKayitliKitapSayisi.Text != "" && int.Parse(lblKayitliKitapSayisi.Text) + int.Parse(lblKitapSayi.Text) <= 3)
                {
                   

                    if (txtTc.Text!=" " && txtAdSoyad.Text!=" " && txtTelefon.Text != " ")
                    {

                        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                        {
                            baglanti.Open();
                            SqlCommand komut = new SqlCommand("insert into VerilenKitaplar(tc,adsoyad,telefon,barkodno,kitapadi,yazari,yayinevi,rafno,kitapsayisi,teslimtarihi,iadetarihi) values(@tc,@adsoyad,@telefon,@barkodno,@kitapadi,@yazari,@yayinevi,@rafno,@kitapsayisi,@teslimtarihi,@iadetarihi) ", baglanti);
                            komut.Parameters.AddWithValue("@tc",txtTc.Text);
                            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                            komut.Parameters.AddWithValue("@yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                            komut.Parameters.AddWithValue("@yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                            komut.Parameters.AddWithValue("@rafno", dataGridView1.Rows[i].Cells["rafno"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()));
                            komut.Parameters.AddWithValue("@teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("@iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                            baglanti.Close();   
         
                            
                        }
                        baglanti.Open();
                        SqlCommand komutt = new SqlCommand("delete from sepet",baglanti);
                        komutt.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kitap verildi.");
                        ds.Tables["sepet"].Clear();
                        sepetlistele();
                        txtTc.Text = " ";
                        lblKitapSayi.Text = " ";
                        kitapsayisi();
                        lblKayitliKitapSayisi.Text = " ";
                        
                    }
                    else
                    {
                        MessageBox.Show("Üye bilgilerini doldurunuz","UYARI");
                    }
                }
                else 
                {
                    MessageBox.Show("En fazla 3 adet kitap emanet alabilirsiniz.","UYARI");  
                }
               
            }

            else
            {
                MessageBox.Show("Sepette hiç kitap bulunmamaktadır.","UYARI");
            }


            
        }

        private void EmanetKitapVermefrm_Load(object sender, EventArgs e)
        {
            sepetlistele();
            kitapsayisi();
        }
    }
}
