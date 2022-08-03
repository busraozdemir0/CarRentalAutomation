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
    public partial class frmAraçListele : Form
    {
        AracKiralama aracKiralama = new AracKiralama();
        public frmAraçListele()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satır = dataGridView1.CurrentRow;
            Plakatxt.Text = satır.Cells["plaka"].Value.ToString();
            Markacombo.Text = satır.Cells["marka"].Value.ToString();
            Sericombo.Text = satır.Cells["seri"].Value.ToString();
            Yiltxt.Text = satır.Cells["yil"].Value.ToString();
            Renktxt.Text = satır.Cells["renk"].Value.ToString();
            Kmtxt.Text = satır.Cells["km"].Value.ToString();
            Yakitcombo.Text = satır.Cells["yakit"].Value.ToString();
            Ucrettxt.Text = satır.Cells["kiraucreti"].Value.ToString();
            pictureBox2.ImageLocation = satır.Cells["resim"].Value.ToString();
        }

        private void frmAraçListele_Load(object sender, EventArgs e)
        {
            YenileAraçlarListesi();
            try
            {
                comboAraçlar.SelectedIndex = 0;
            }
            catch
            {
                ;
            }

        }

        private void YenileAraçlarListesi()
        {
            string cümle = "select * from Arac";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView1.DataSource= aracKiralama.listele(adtr2, cümle);
        }

        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog1.FileName;
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string cümle = "update Arac set marka=@marka,seri=@seri,yil=@yil,renk=@renk,km=@km,yakit=@yakit,kiraucreti=@kiraucreti,resim=@resim,tarih=@tarih where plaka=@plaka";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@plaka", Plakatxt.Text);
            komut2.Parameters.AddWithValue("@marka", Markacombo.Text);
            komut2.Parameters.AddWithValue("@seri", Sericombo.Text);
            komut2.Parameters.AddWithValue("@yil", Yiltxt.Text);
            komut2.Parameters.AddWithValue("@renk", Renktxt.Text);
            komut2.Parameters.AddWithValue("@km", Kmtxt.Text);
            komut2.Parameters.AddWithValue("@yakit", Yakitcombo.Text);
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(Ucrettxt.Text));
            komut2.Parameters.AddWithValue("@resim", pictureBox2.ImageLocation);
            komut2.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            aracKiralama.ekle_sil_guncelle(komut2, cümle);
            Sericombo.Items.Clear();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in Controls)
            {
                if (item is ComboBox)
                    item.Text = "";
            }
            pictureBox2.ImageLocation = "";
            YenileAraçlarListesi();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow satır = dataGridView1.CurrentRow;
            string cümle = "delete from Arac where plaka='"+satır.Cells["plaka"].Value.ToString()+"'";
            SqlCommand komut2 = new SqlCommand();
            aracKiralama.ekle_sil_guncelle(komut2, cümle);
            YenileAraçlarListesi();
            pictureBox2.ImageLocation = "";
            Sericombo.Items.Clear();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in Controls)
            {
                if (item is ComboBox)
                    item.Text = "";
            }
        }

        private void Markacombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sericombo.Items.Clear();
                if (Markacombo.SelectedItem.ToString() == "Opel")
                {
                    Sericombo.Items.Add("Astra");
                    Sericombo.Items.Add("Vectra");
                    Sericombo.Items.Add("Corsa");
                }
                else if (Markacombo.SelectedItem.ToString() == "Renault")
                {
                    Sericombo.Items.Add("Symbol");
                    Sericombo.Items.Add("Megane");
                    Sericombo.Items.Add("Clio");
                }
                else if (Markacombo.SelectedItem.ToString() == "Fiat")
                {
                    Sericombo.Items.Add("Stilo");
                    Sericombo.Items.Add("Linea");
                    Sericombo.Items.Add("Egea");
                }
                else if (Markacombo.SelectedItem.ToString() == "Ford")
                {
                    Sericombo.Items.Add("Fiesta");
                    Sericombo.Items.Add("Focus");
                }
                else if (Markacombo.SelectedItem.ToString() == "Volkswagen")
                {
                    Sericombo.Items.Add("Golf");
                    Sericombo.Items.Add("Jetta");
                    Sericombo.Items.Add("Polo");
                    Sericombo.Items.Add("Caddy");
                }
                else if (Markacombo.SelectedItem.ToString() == "Mercedes")
                {
                    Sericombo.Items.Add("A Serisi");
                    Sericombo.Items.Add("B Serisi");
                    Sericombo.Items.Add("C Serisi");
                    Sericombo.Items.Add("CLS Serisi");
                    Sericombo.Items.Add("E Serisi");
                }
                else if (Markacombo.SelectedItem.ToString() == "BMW")
                {
                    Sericombo.Items.Add("2 Serisi Active Tourer");
                    Sericombo.Items.Add("4 Serisi Cabrio");
                    Sericombo.Items.Add("Z4");
                }
            }
            catch
            {
                ;
            }
        }

        private void comboAraçlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(comboAraçlar.SelectedItem.ToString() == "Tüm Araçlar")
                {
                    YenileAraçlarListesi();
                }
                else if (comboAraçlar.SelectedItem.ToString()=="Boş Araçlar")
                {
                    string cümle = "select * from Arac where durumu='BOŞ'";
                    SqlDataAdapter adtr2 = new SqlDataAdapter();
                    dataGridView1.DataSource = aracKiralama.listele(adtr2, cümle);
                }
                else if (comboAraçlar.SelectedItem.ToString() == "Dolu Araçlar")
                {
                    string cümle = "select * from Arac where durumu='DOLU'";
                    SqlDataAdapter adtr2 = new SqlDataAdapter();
                    dataGridView1.DataSource = aracKiralama.listele(adtr2, cümle);
                }
            }
            catch
            {
                ;
            }
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
