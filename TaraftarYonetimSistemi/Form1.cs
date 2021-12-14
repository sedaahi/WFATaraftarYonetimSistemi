using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaraftarYonetimSistemi.Data;

namespace TaraftarYonetimSistemi
{
    public partial class Form1 : Form
    {
        UygulamaDbContext db = new UygulamaDbContext();
        public Form1()
        {
            InitializeComponent();
            TakimlariListele();//1
        }

        private void TakimlariListele()//1
        {
            cboTuttuguTakim.DataSource = db.Takimlar.ToList();//1

            var takimListesi = db.Takimlar.ToList();
            takimListesi.Insert(0, new Takim() //takımı olmayanları yazdırmak için takımı yok diye combobxa biz ekledik sonra kişiler eklenecek yani databasele alakası yok ama diğerleri entity frameworkten geliyor
            {
                Ad = "Takımı Yok",
                Taraftarlar = db.Kisiler.Where(k => k.TakimId == null).ToList()
            });
            cboTakim.DataSource = takimListesi;
            // cboTuttuguTakim.SelectedIndex = -1;//5 kişi ekledeki combobox otomatik birşey seçili olmasın diye //yapmasanda olur
            cboYeniTakim.DataSource = db.Takimlar.ToList();
        }

        private void cboTakim_SelectedIndexChanged(object sender, EventArgs e)//2
        {
            if (cboTakim.SelectedIndex == -1)//2
            {
                lstTaraftarlar.DataSource = null;
                return;
            }
            Takim takim = (Takim)cboTakim.SelectedItem;//2
            lstTaraftarlar.DataSource = takim.Taraftarlar?.ToList();//2
        }

        private void btnTakimEkle_Click(object sender, EventArgs e)//3
        {
            string takimAd = txtTakimAd.Text.Trim();//3
            if (takimAd == "") return;//3

            db.Takimlar.Add(new Takim() { Ad = takimAd });//3
            db.SaveChanges();//3
            txtTakimAd.Clear();
            TakimlariListele();
        }

        private void chkTakimTutmuyor_CheckedChanged(object sender, EventArgs e)//4
        {
            cboTuttuguTakim.Enabled = !chkTakimTutmuyor.Checked; //biri çekliyse diğeri enable false yani işlevsiz.(Takım tutmuyotum cheked ise combobox disable)//4
        }

        private void btnKisiEkle_Click(object sender, EventArgs e)//5
        {
            Takim takim = (Takim)cboTuttuguTakim.SelectedItem;
            string kisiAd = txtKisiAd.Text.Trim();
            if (kisiAd == "" || !chkTakimTutmuyor.Checked && takim == null) return; //check box seçili değilse 

            Kisi kisi = new Kisi()
            {
                Ad = kisiAd,
                Takim = chkTakimTutmuyor.Checked ? null : takim
            };
            db.Kisiler.Add(kisi);
            db.SaveChanges();
            txtKisiAd.Clear();
            TakimlariListele();
        }

        private void btnTakimiSil_Click(object sender, EventArgs e) //takımı silinen taraftarların takım idsi olmadıgı için takımı yok listesine eklenir
        {
            Takim takim = (Takim)cboTakim.SelectedItem;
            if (takim == null || takim.Id == 0) return;

            db.Takimlar.Remove(takim);
            db.SaveChanges();
            TakimlariListele();
        }

        private void btnTakimdanCikar_Click(object sender, EventArgs e)
        {
            Takim takim = (Takim)cboTakim.SelectedItem;
            Kisi taraftar = (Kisi)lstTaraftarlar.SelectedItem;
            if (takim == null || taraftar == null || takim.Id == 0) return;

            //taraftar.TakimId = null;// YÖNTEM 1 --Taraftarı takımdan çıkarıyoruz ve o taraftar takımı yok ta listeleniyor
            takim.Taraftarlar.Remove(taraftar); //YÖNTEM 2
            db.SaveChanges();
            TakimlariListele();

        }

        private void btnTakimDegistir_Click(object sender, EventArgs e)
        {
            Kisi taraftar = (Kisi)lstTaraftarlar.SelectedItem;
            Takim yeniTakim = (Takim)cboYeniTakim.SelectedItem;
            if (taraftar == null || yeniTakim == null) return;

            taraftar.Takim = yeniTakim;
            db.SaveChanges();
            TakimlariListele();
        }
    }
}
