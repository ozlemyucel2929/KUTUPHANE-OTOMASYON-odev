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
    public partial class UyeListelefrm : Form
    {
        public UyeListelefrm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text= dataGridView1.CurrentRow.Cells["tc"].Value.ToString();   
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from uye where tc like '" +txtTc.Text+"'",baglanti);   
            SqlDataReader reader = komut.ExecuteReader(); 
            while (reader.Read())
            {
                txtAdSoyad.Text= reader["adsoyad"].ToString();
                txtYas.Text = reader["yas"].ToString();
                comboCinsiyet.Text = reader["cinsiyet"].ToString();
                txtTelefon.Text = reader["telefon"].ToString();
                txtAdres.Text = reader["adres"].ToString();
                txtEmail.Text = reader["email"].ToString();
            }
            baglanti.Close();
        }
        DataSet ds = new DataSet(); 
        private void txtAraTc_TextChanged(object sender, EventArgs e)
        {
            ds.Tables["uye"].Clear();
            baglanti.Open();  
           SqlDataAdapter adaptr = new SqlDataAdapter("SELECT * FROM UYE WHERE tc like '%" +txtAraTc.Text+"%'",baglanti);
            adaptr.Fill(ds,"uye");
            dataGridView1.DataSource = ds.Tables["uye"];
            baglanti.Close() ;  
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from uye where tc =@tc", baglanti);
            cmd.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["TC"].Value.ToString());
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi tamamlandı.");
            ds.Tables["uye"].Clear();
            uyeListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        private void uyeListele ()
        {
            baglanti.Open ();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from uye",baglanti);
            adapter.Fill(ds, "uye");
            dataGridView1.DataSource = ds.Tables["uye"];
            baglanti.Close() ;

        }
        private void UyeListelefrm_Load(object sender, EventArgs e)
        {
            uyeListele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update uye set adsoyad=@adsoyad, yas=@yas, cinsiyet=@cinsiyet, telefon=@telefon, adres=@adres, email=@email where tc=@tc", baglanti);
            cmd.Parameters.AddWithValue("@tc", txtTc.Text);
            cmd.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("@yas", txtYas.Text);
            cmd.Parameters.AddWithValue("@cinsiyet", comboCinsiyet.Text);
            cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.ExecuteNonQuery();  
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi tamamlandı.");
            ds.Tables["uye"].Clear();
            uyeListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }


        }
    }
}
