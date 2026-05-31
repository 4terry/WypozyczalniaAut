using System;
using System.Windows.Forms;
using WypozyczalaniaAut.Logika;
using WypozyczalaniaAut.Modele;

namespace WypozyczalaniaAut
{
    public partial class MainForm : Form
    {
        private Wypozyczalnia _wypozyczalnia;

        public MainForm()
        {
            InitializeComponent();

            _wypozyczalnia = new Wypozyczalnia();

            ZaladujDaneTestowe();

            MessageBox.Show($"Witaj! Za³adowano aut: {_wypozyczalnia.Auta.Count}, Klientów: {_wypozyczalnia.Klienci.Count}");
        }

        private void ZaladujDaneTestowe()
        {
            _wypozyczalnia.ZarejestrujKlienta(new Klient(1, "Wies³aw Paleta"));
            _wypozyczalnia.ZarejestrujKlienta(new Klient(2, "Jan Maczeta"));

            _wypozyczalnia.DodajAuto(new AutoSpalinowe("VIN123", "Honda Civic", 250.0));
            _wypozyczalnia.DodajAuto(new AutoSpalinowe("VIN456", "Toyota Corolla", 120.0));
            _wypozyczalnia.DodajAuto(new AutoElektryczne("VIN789", "Tesla Model Y", 300.0));
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(243, 327);
            button1.Name = "button1";
            button1.Size = new Size(335, 127);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(201, 38);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(541, 259);
            listBox1.TabIndex = 1;
            // 
            // MainForm
            // 
            ClientSize = new Size(923, 570);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Name = "MainForm";
            ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = _wypozyczalnia.Auta;
        }
    }
}