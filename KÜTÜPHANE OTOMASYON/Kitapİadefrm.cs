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
    public partial class Kitapİadefrm : Form
    {
        public Kitapİadefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        DataSet ds = new DataSet();

        private void EmanetListele()
        {
            baglanti.Open();
            SqlDataAdapter SqlDataAdapter = new SqlDataAdapter("select * from VerilenKitaplar", baglanti);
            SqlDataAdapter.Fill(ds, "VerilenKitaplar");
            dataGridView1.DataSource = ds.Tables["VerilenKitaplar"];
            baglanti.Close();
        }

        private void Kitapİadefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();    
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("delete from VerilenKitaplar where tc=@tc and barkodno=@barkodno",baglanti);
            komut1.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut1.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut1.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update kitap SET stoksayisi = stoksayisi + '" + dataGridView1.CurrentRow.Cells["kitapsayisi"].Value.ToString() +"' where barkodno=@barkodno", baglanti);
            komut2.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut2.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("İade işlemi tamamlandı.");
            ds.Tables["VerilenKitaplar"].Clear();
            EmanetListele();
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            ds.Tables["VerilenKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adptr = new SqlDataAdapter("select *from VerilenKitaplar where tc like '%" + txtTcAra.Text + "%'", baglanti);
            adptr.Fill(ds, "VerilenKitaplar");
            baglanti.Close() ;

                if  (txtTcAra.Text=="")
            {
                ds.Tables["VerilenKitaplar"].Clear();
                EmanetListele();  

            }
        }

        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            ds.Tables["VerilenKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("select *from VerilenKitaplar where barkodno like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            sqlDataAdapter2.Fill(ds, "VerilenKitaplar");
            baglanti.Close();
            if (txtBarkodNoAra.Text == "")
            {
                ds.Tables["VerilenKitaplar"].Clear();
                EmanetListele();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
