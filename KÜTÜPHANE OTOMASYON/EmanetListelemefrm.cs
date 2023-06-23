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
    public partial class EmanetListelemefrm : Form
    {
        public EmanetListelemefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\CASPER\\OneDrive\\Belgeler\\KütüphaneOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
        DataSet ds = new DataSet();

        private void EmanetListelemefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
            comboBox1.SelectedIndex = 0;    
        }
        private void EmanetListele ()
            {
            baglanti.Open();
            SqlDataAdapter SqlDataAdapter = new SqlDataAdapter("select * from VerilenKitaplar", baglanti);
            SqlDataAdapter.Fill(ds, "VerilenKitaplar");
            dataGridView1.DataSource = ds.Tables["VerilenKitaplar"];
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds.Tables["VerilenKitaplar"].Clear();
            if (comboBox1.SelectedIndex == 0) {
                EmanetListele();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                baglanti.Open();
                SqlDataAdapter SqlDataAdapter = new SqlDataAdapter("select * from VerilenKitaplar where '" + DateTime.Now.ToShortDateString()+"'> iadetarihi", baglanti);
                SqlDataAdapter.Fill(ds, "VerilenKitaplar");
                dataGridView1.DataSource = ds.Tables["VerilenKitaplar"];
                baglanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                baglanti.Open();
                SqlDataAdapter SqlDataAdapter = new SqlDataAdapter("select * from VerilenKitaplar where '" + DateTime.Now.ToShortDateString() + "'<= iadetarihi", baglanti);
                SqlDataAdapter.Fill(ds, "VerilenKitaplar");
                dataGridView1.DataSource = ds.Tables["VerilenKitaplar"];
                baglanti.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
