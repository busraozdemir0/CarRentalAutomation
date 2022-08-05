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
    public partial class frmSatış : Form
    {
        public frmSatış()
        {
            InitializeComponent();
        }

        AracKiralama aracKiralama = new AracKiralama();
        private void frmSatış_Load(object sender, EventArgs e)
        {
            string sorgu2 = "select * from Satis";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView1.DataSource = aracKiralama.listele(adtr2,sorgu2);
            aracKiralama.SatisHesapla(label1);
        }
    }
}
