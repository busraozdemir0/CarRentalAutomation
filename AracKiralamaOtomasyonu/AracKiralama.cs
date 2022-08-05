using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu
{
    class AracKiralama
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-G8IS3SP\\SQLEXPRESS;Initial Catalog=AracKiralama;Integrated Security=True");
        DataTable tablo;
        public void ekle_sil_guncelle(SqlCommand komut,string sorgu)
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            baglanti.Close();

        }
        public DataTable listele(SqlDataAdapter adtr,string sorgu)
        {
            tablo = new DataTable();
            adtr = new SqlDataAdapter(sorgu,baglanti);
            adtr.Fill(tablo); 
            baglanti.Close();
            return tablo;
        }
        public void Bos_Araclar(ComboBox combo, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu,baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while(read.Read())
            {
                combo.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }
        public void TC_Ara(TextBox tcara, TextBox tc, TextBox adsoyad, TextBox telefon, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                tc.Text = read["tc"].ToString();
                adsoyad.Text = read["adsoyad"].ToString();
                telefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }
        public void CombodanGetir(ComboBox araclar, TextBox marka, TextBox seri, TextBox yil, TextBox renk, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                marka.Text = read["marka"].ToString();
                seri.Text = read["seri"].ToString();
                yil.Text = read["yil"].ToString();
                renk.Text = read["renk"].ToString();
            }
            baglanti.Close();
        }
        public void Ucret_Hesapla(ComboBox combokiraşekli,TextBox ucret, string sorgu)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
               if(combokiraşekli.SelectedIndex==0)
                {
                    ucret.Text = (int.Parse(read["kiraucreti"].ToString())*1).ToString();
                }
                if (combokiraşekli.SelectedIndex == 1)
                {
                    ucret.Text = (int.Parse(read["kiraucreti"].ToString())*0.80).ToString();
                }
                if (combokiraşekli.SelectedIndex == 2)
                {
                    ucret.Text = (int.Parse(read["kiraucreti"].ToString())*0.70).ToString();
                }
            }
            baglanti.Close();
        }
        public void SatisHesapla(Label lbl)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from Satis",baglanti);
            lbl.Text = "Toplam Tutar= " + komut.ExecuteScalar() + " TL";
            baglanti.Close();
        }

    }
}
