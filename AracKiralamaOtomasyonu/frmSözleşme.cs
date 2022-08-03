using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string sorgu2 = "select * from Arac where durumu='BOŞ'";
            aracKiralama.Bos_Araclar(comboAraçlar, sorgu2);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if(txtTc.Text=="")
            {
                foreach(Control item in groupBox1.Controls)
                {
                    if (item is TextBox)
                        item.Text = "";
                }
            }
            string sorgu2 = "select * from Musteri where tc like '" + txtTc.Text + "'";
            aracKiralama.TC_Ara(txtTc, txtAdSoyad, txtTelefon, sorgu2);
        }
    }
}
