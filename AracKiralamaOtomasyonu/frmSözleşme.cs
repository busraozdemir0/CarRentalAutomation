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
    public partial class frmSözleşme : Form
    {
        AracKiralama aracKiralama = new AracKiralama();
        public frmSözleşme()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void frmSözleşme_Load(object sender, EventArgs e)
        {
            Boş_Araçlar();
            Yenile();
        }

        private void Boş_Araçlar()
        {
            string sorgu2 = "select * from Arac where durumu='BOŞ'";
            aracKiralama.Bos_Araclar(comboAraçlar, sorgu2);
        }

        private void Yenile()
        {
            string sorgu3 = "select * from Sözlesme";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView1.DataSource = aracKiralama.listele(adtr2, sorgu3);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboAraçlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu2 = "select * from Arac where plaka like '" + comboAraçlar.SelectedItem + "'";
            aracKiralama.CombodanGetir(comboAraçlar, txtMarka, txtSeri, txtYil, txtRenk, sorgu2);
        }

        private void comboKiraŞekli_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu2 = "select * from Arac where plaka like '" + comboAraçlar.SelectedItem + "'";
            aracKiralama.Ucret_Hesapla(comboKiraŞekli,txtKiraUcreti,sorgu2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan gun = DateTime.Parse(dateDönüş.Text) - DateTime.Parse(dateÇıkış.Text);
            int gun2 = gun.Days;
            txtGün.Text = gun2.ToString();
            txtTutar.Text = (gun2 * int.Parse(txtKiraUcreti.Text)).ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Temizle()
        {
            dateÇıkış.Text = DateTime.Now.ToShortDateString();
            dateDönüş.Text = DateTime.Now.ToShortDateString();
            comboKiraŞekli.Text = "";
            txtKiraUcreti.Text = "";
            txtGün.Text = "";
            txtTutar.Text = "";
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu2 = "insert into Sözlesme(tc,adsoyad,telefon,ehliyetno,e_tarih,e_yer,plaka,marka,seri,yil,renk,kirasekli,kiraücreti,gün,tutar,ctarih,dtarih) values(@tc,@adsoyad,@telefon,@ehliyetno,@e_tarih,@e_yer,@plaka,@marka,@seri,@yil,@renk,@kirasekli,@kiraücreti,@gün,@tutar,@ctarih,@dtarih)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", txtTc.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut2.Parameters.AddWithValue("@ehliyetno", txtE_No.Text);
            komut2.Parameters.AddWithValue("@e_tarih", txtE_Tarih.Text);           
            komut2.Parameters.AddWithValue("@e_yer", txtE_Yer.Text);
            komut2.Parameters.AddWithValue("@plaka", comboAraçlar.Text);
            komut2.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut2.Parameters.AddWithValue("@seri", txtSeri.Text);
            komut2.Parameters.AddWithValue("@yil", txtYil.Text);
            komut2.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut2.Parameters.AddWithValue("@kirasekli", comboKiraŞekli.Text);
            komut2.Parameters.AddWithValue("@kiraücreti", int.Parse(txtKiraUcreti.Text));
            komut2.Parameters.AddWithValue("@gün", int.Parse(txtGün.Text));
            komut2.Parameters.AddWithValue("@tutar", int.Parse(txtTutar.Text));
            komut2.Parameters.AddWithValue("@ctarih", dateÇıkış.Text);
            komut2.Parameters.AddWithValue("@dtarih", dateDönüş.Text);
            aracKiralama.ekle_sil_guncelle(komut2, sorgu2);

            string sorgu3 = "update Arac set durumu='DOLU' where plaka='" + comboAraçlar.Text + "'";
            SqlCommand komut3 = new SqlCommand();
            aracKiralama.ekle_sil_guncelle(komut3,sorgu3);
            comboAraçlar.Items.Clear();
            Boş_Araçlar();
            Yenile();
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            comboAraçlar.Text = "";
            Temizle();
            MessageBox.Show("Sözleşme Eklendi");
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            if (txtTcAra.Text == "")
            {
                foreach (Control item in groupBox1.Controls)
                {
                    if (item is TextBox)
                        item.Text = "";
                }
            }
            string sorgu2 = "select * from Musteri where tc like '" + txtTcAra.Text + "'";
            aracKiralama.TC_Ara(txtTcAra,txtTc, txtAdSoyad, txtTelefon, sorgu2);
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string sorgu2 = "update Sözlesme set tc=@tc, adsoyad=@adsoyad,telefon=@telefon,ehliyetno=@ehliyetno,e_tarih=@e_tarih,e_yer=@e_yer,marka=@marka,seri=@seri,yil=@yil,renk=@renk,kirasekli=@kirasekli,kiraücreti=@kiraücreti,gün=@gün,tutar=@tutar,ctarih=@ctarih,dtarih=@dtarih where plaka=@plaka";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", txtTc.Text);
            komut2.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut2.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut2.Parameters.AddWithValue("@ehliyetno", txtE_No.Text);
            komut2.Parameters.AddWithValue("@e_tarih", txtE_Tarih.Text);
            komut2.Parameters.AddWithValue("@e_yer", txtE_Yer.Text);
            komut2.Parameters.AddWithValue("@plaka", comboAraçlar.Text);
            komut2.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut2.Parameters.AddWithValue("@seri", txtSeri.Text);
            komut2.Parameters.AddWithValue("@yil", txtYil.Text);
            komut2.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut2.Parameters.AddWithValue("@kirasekli", comboKiraŞekli.Text);
            komut2.Parameters.AddWithValue("@kiraücreti", int.Parse(txtKiraUcreti.Text));
            komut2.Parameters.AddWithValue("@gün", int.Parse(txtGün.Text));
            komut2.Parameters.AddWithValue("@tutar", int.Parse(txtTutar.Text));
            komut2.Parameters.AddWithValue("@ctarih", dateÇıkış.Text);
            komut2.Parameters.AddWithValue("@dtarih", dateDönüş.Text);
            aracKiralama.ekle_sil_guncelle(komut2, sorgu2);        
            comboAraçlar.Items.Clear();
            Boş_Araçlar();
            Yenile();
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                    item.Text = "";
            }
            comboAraçlar.Text = "";
            Temizle();
            MessageBox.Show("Sözleşme Güncellendi");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satır = dataGridView1.CurrentRow;
            txtTc.Text = satır.Cells[0].Value.ToString();
            txtAdSoyad.Text = satır.Cells[1].Value.ToString();
            txtTelefon.Text = satır.Cells[2].Value.ToString();
            txtE_No.Text = satır.Cells[3].Value.ToString();
            txtE_Tarih.Text = satır.Cells[4].Value.ToString();
            txtE_Yer.Text = satır.Cells[5].Value.ToString();
            comboAraçlar.Text = satır.Cells[6].Value.ToString();
            txtMarka.Text = satır.Cells[7].Value.ToString();
            txtSeri.Text = satır.Cells[8].Value.ToString();
            txtYil.Text = satır.Cells[9].Value.ToString();
            txtRenk.Text = satır.Cells[10].Value.ToString();
            comboKiraŞekli.Text = satır.Cells[11].Value.ToString();
            txtKiraUcreti.Text = satır.Cells[12].Value.ToString();
            txtGün.Text = satır.Cells[13].Value.ToString();
            txtTutar.Text = satır.Cells[14].Value.ToString();
            dateÇıkış.Text = satır.Cells[15].Value.ToString();
            dateDönüş.Text = satır.Cells[16].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow satır = dataGridView1.CurrentRow;
            //Gün farkı hesaplama
            DateTime bugün = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime dönüş = DateTime.Parse(satır.Cells["dtarih"].Value.ToString());
            int ucret = int.Parse(satır.Cells["kiraücreti"].Value.ToString());
            TimeSpan gunfarki = bugün - dönüş;
            int _gunfarki = gunfarki.Days;
            int ucretfarki;
            //Ücret farkı hesaplama
            ucretfarki = _gunfarki * ucret;
            txtEkstra.Text = ucretfarki.ToString();
            
        }

        private void btnAraçTeslim_Click(object sender, EventArgs e)
        {
            //Toplam tutar hesaplama
            if(int.Parse(txtEkstra.Text)>=0 || int.Parse(txtEkstra.Text) < 0)
            {
                DataGridViewRow satır = dataGridView1.CurrentRow;
                DateTime bugün = DateTime.Parse(DateTime.Now.ToShortDateString());
                int ucret= int.Parse(satır.Cells["kiraücreti"].Value.ToString());
                int tutar= int.Parse(satır.Cells["tutar"].Value.ToString());
                DateTime çıkış = DateTime.Parse(satır.Cells["ctarih"].Value.ToString());
                TimeSpan gun = bugün - çıkış;
                int _gun = gun.Days;
                int toplamtutar = _gun * ucret;
                //Toplamtutar,_gun ve ucret satış tablosuna aktarılacak.
                string sorgu1="delete from Sözlesme where plaka='"+satır.Cells["plaka"].Value.ToString()+"'";
                SqlCommand komut = new SqlCommand();
                aracKiralama.ekle_sil_guncelle(komut, sorgu1);
                string sorgu2="update Arac set durumu='BOŞ' where plaka='"+ satır.Cells["plaka"].Value.ToString() + "'";
                SqlCommand komut3 = new SqlCommand();
                aracKiralama.ekle_sil_guncelle(komut3, sorgu2);

                string sorgu3 = "insert into Satis(tc,adsoyad,plaka,marka,seri,yil,renk,gun,tutar,tarih1,tarih2,fiyat) values(@tc,@adsoyad,@plaka,@marka,@seri,@yil,@renk,@gun,@tutar,@tarih1,@tarih2,@fiyat)";
                SqlCommand komut2 = new SqlCommand();
                komut2.Parameters.AddWithValue("@tc", satır.Cells["tc"].Value.ToString());
                komut2.Parameters.AddWithValue("@adsoyad", satır.Cells["adsoyad"].Value.ToString()); 
                komut2.Parameters.AddWithValue("@plaka", satır.Cells["plaka"].Value.ToString());
                komut2.Parameters.AddWithValue("@marka", satır.Cells["marka"].Value.ToString());
                komut2.Parameters.AddWithValue("@seri", satır.Cells["seri"].Value.ToString());
                komut2.Parameters.AddWithValue("@yil", satır.Cells["yil"].Value.ToString());
                komut2.Parameters.AddWithValue("@renk", satır.Cells["renk"].Value.ToString());
                komut2.Parameters.AddWithValue("@gun", _gun);
                komut2.Parameters.AddWithValue("@tutar", toplamtutar);
                komut2.Parameters.AddWithValue("@tarih1", satır.Cells["ctarih"].Value.ToString());
                komut2.Parameters.AddWithValue("@tarih2", DateTime.Now.ToShortDateString());
                komut2.Parameters.AddWithValue("@fiyat", ucret);
                aracKiralama.ekle_sil_guncelle(komut2, sorgu3);

                MessageBox.Show("Araç teslim edildi.");
                comboAraçlar.Items.Clear();
                Boş_Araçlar();
                Yenile();
                foreach (Control item in groupBox1.Controls)
                {
                    if (item is TextBox)
                        item.Text = "";
                }
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                        item.Text = "";
                }
                comboAraçlar.Text = "";
                Temizle();
                txtEkstra.Text = "";
            }
            else 
            {
                MessageBox.Show("Lütfen seçim yapınız!","Uyarı");
            }

        }
    }
}
