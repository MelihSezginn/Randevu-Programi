using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        string dosyaYolu = Path.Combine(Application.StartupPath, "randevular.txt");

        public Form1()
        {
            InitializeComponent();
            // Modern Tema
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dosyadan eski kayıtları yükle
            if (File.Exists(dosyaYolu))
            {
                string[] satirlar = File.ReadAllLines(dosyaYolu);
                foreach (string s in satirlar) listBox1.Items.Add(s);
            }

            timer1.Start();
            timer2.Start();
            label6.Text = DateTime.Now.ToShortDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Randevu tipi seçiniz!");

                string yeniKayit = $"{dateTimePicker1.Text} | {comboBox2.Text}:{comboBox3.Text} | {comboBox1.Text}: {richTextBox1.Text}";

                listBox1.Items.Add(yeniKayit);
                File.AppendAllText(dosyaYolu, yeniKayit + Environment.NewLine);

                MessageBox.Show("Randevu kaydedildi!", "Bilgi");
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string suAn = DateTime.Now.ToString("HH:mm");
            string bugun = DateTime.Now.ToShortDateString();

            foreach (var item in listBox1.Items)
            {
                if (item.ToString().Contains(bugun) && item.ToString().Contains(suAn))
                {
                    MessageBox.Show("Randevu vakti geldi!", "UYARI");
                }
            }
        }
    }
}