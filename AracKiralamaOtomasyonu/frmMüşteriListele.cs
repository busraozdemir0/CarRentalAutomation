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

namespace AracKiralamaOtomasyonu
{
    public partial class frmMüşteriListele : Form
    {
        AracKiralama arackiralama = new AracKiralama();
        public frmMüşteriListele()
        {
            InitializeComponent();
        }

        private void frmMüşteriListele_Load(object sender, EventArgs e)
        {
            YenileListele();
        }

        private void YenileListele()
        {
            string cümle = "select * from Musteri";
            SqlDataAdapter adtr2 = new SqlDataAdapter();           
            dataGridView1.DataSource = arackiralama.listele(adtr2, cümle);
            dataGridView1.Columns[0].HeaderText = "TC";
            dataGridView1.Columns[1].HeaderText = "AD SOYAD";
            dataGridView1.Columns[3].HeaderText = "ADRES";
            dataGridView1.Columns[2].HeaderText = "TELEFON";
            dataGridView1.Columns[4].HeaderText = "E-MAİL";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string cümle = "select * from Musteri where tc like '%"+textBox1.Text+"%'";
            SqlDataAdapter adtr2 = new SqlDataAdapter();          
            dataGridView1.DataSource = arackiralama.listele(adtr2, cümle);
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satır = dataGridView1.CurrentRow;
            txtTc.Text = satır.Cells[0].Value.ToString();
            txtAdSoyad.Text = satır.Cells[1].Value.ToString();
            txtTelefon.Text = satır.Cells[2].Value.ToString();
            txtAdres.Text = satır.Cells[3].Value.ToString();
            txtEmail.Text = satır.Cells[4].Value.ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string cümle = "update Musteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", txtTc.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut2.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut2.Parameters.AddWithValue("@email", txtEmail.Text);
            arackiralama.ekle_sil_guncelle(komut2, cümle);
            foreach (Control item in Controls) 
            {
                if (item is TextBox) 
                    item.Text = "";
            }
            YenileListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow satır = dataGridView1.CurrentRow;
            string cümle="delete from Musteri where tc='"+satır.Cells["tc"].Value.ToString()+"'";
            SqlCommand komut2 = new SqlCommand();
            arackiralama.ekle_sil_guncelle(komut2, cümle);
            YenileListele();
        }
    }
}
