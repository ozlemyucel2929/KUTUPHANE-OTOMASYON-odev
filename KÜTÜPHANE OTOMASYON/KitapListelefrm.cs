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
    public partial class KitapListelefrm : Form
    {
        public KitapListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        DataSet ds = new DataSet();

        private void KitapListele()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Kitap", baglanti);
            adapter.Fill(ds, "Kitap");
            dataGridView1.DataSource = ds.Tables["Kitap"];
            baglanti.Close();

        }
        private void KitapListelefrm_Load(object sender, EventArgs e)
        {
            KitapListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from Kitap where barkodno =@barkodno", baglanti);
            cmd.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi tamamlandı.");
            ds.Tables["Kitap"].Clear();
            KitapListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox|| item is ComboBox)
                {
                    item.Text = "";

                }
            }
        }

        private void txtAraBarkodNo_TextChanged(object sender, EventArgs e)
        {
            ds.Tables["Kitap"].Clear();
            baglanti.Open();
            SqlDataAdapter adaptr = new SqlDataAdapter("SELECT * FROM Kitap WHERE barkodno like '%" + txtAraBarkodNo.Text + "%'", baglanti);
            adaptr.Fill(ds, "Kitap");
            dataGridView1.DataSource = ds.Tables["Kitap"];
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update Kitap set kitapadi=@kitapadi, turu=@turu, kitapsayisi=@kitapsayisi, yazari=@yazari, yayinevi=@yayinevi,rafno=@rafno, stoksayisi=@stoksayisi where barkodno=@barkodno", baglanti);
         
            cmd.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            cmd.Parameters.AddWithValue("@turu",comboTuru.Text);
            cmd.Parameters.AddWithValue("@kitapsayisi", txtKitapSayisi.Text);
            cmd.Parameters.AddWithValue("@yazari", txtYazari.Text);
            cmd.Parameters.AddWithValue("@yayinevi", txtYayinEvi.Text);
            cmd.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            cmd.Parameters.AddWithValue("@stoksayisi", txtStokSayisi.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi tamamlandı.");
            ds.Tables["Kitap"].Clear();
            KitapListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kitap where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {

                txtKitapAdi.Text = reader["kitapadi"].ToString();
                comboTuru.Text = reader["turu"].ToString();
                txtKitapSayisi.Text = reader["Kitapsayisi"].ToString();
                txtYazari.Text = reader["yazari"].ToString();
                txtYayinEvi.Text = reader["yayinevi"].ToString();
                txtRafNo.Text = reader["rafno"].ToString();
                txtStokSayisi.Text = reader["stoksayisi"].ToString();
            }
            baglanti.Close();
        }
    }
}