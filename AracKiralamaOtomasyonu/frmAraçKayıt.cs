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
    public partial class frmAraçKayıt : Form
    {
        AracKiralama aracKiralama = new AracKiralama();
        public frmAraçKayıt()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(); //openFileDialog ile yeni dosya açıp oradan dosya seçmemizi sağlar.
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            string cümle = "insert into Arac(plaka,marka,seri,yil,renk,km,yakit,kiraucreti,resim,tarih,durumu) values(@plaka,@marka,@seri,@yil,@renk,@km,@yakit,@kiraucreti,@resim,@tarih,@durumu)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@plaka", Plakatxt.Text);
            komut2.Parameters.AddWithValue("@marka", Markacombo.Text);
            komut2.Parameters.AddWithValue("@seri", Sericombo.Text);
            komut2.Parameters.AddWithValue("@yil", Yiltxt.Text);
            komut2.Parameters.AddWithValue("@renk", Renktxt.Text);
            komut2.Parameters.AddWithValue("@km", Kmtxt.Text);
            komut2.Parameters.AddWithValue("@yakit", Yakitcombo.Text);
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(Ucrettxt.Text));
            komut2.Parameters.AddWithValue("@resim", pictureBox1.ImageLocation);
            komut2.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            komut2.Parameters.AddWithValue("@durumu", "BOŞ");
            aracKiralama.ekle_sil_guncelle(komut2, cümle);
            Sericombo.Items.Clear();
            foreach(Control item in Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in Controls)
            {
                if (item is ComboBox)
                    item.Text = "";
            }
            pictureBox1.ImageLocation = "";
        }
    }
}
