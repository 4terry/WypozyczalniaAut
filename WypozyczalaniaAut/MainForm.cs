using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using WypozyczalaniaAut.Logika;
using WypozyczalaniaAut.Modele;
using WypozyczalaniaAut.Reguly;

namespace WypozyczalaniaAut
{
    public partial class MainForm : Form
    {
        private Wypozyczalnia _wypozyczalnia;

        public MainForm()
        {
            InitializeComponent();

            _wypozyczalnia = new Wypozyczalnia();

            WczytajDaneZPlikow();

            MessageBox.Show($"Za³adowane auta: {_wypozyczalnia.Auta.Count}, klienci: {_wypozyczalnia.Klienci.Count}");
        }

        private void WczytajDaneZPlikow()
        {
            if (File.Exists("klienci.txt"))
            {
                string[] linieKlienci = File.ReadAllLines("klienci.txt");
                foreach (string linia in linieKlienci)
                {
                    string[] dane = linia.Split(';');
                    int id = int.Parse(dane[0]);
                    string imie = dane[1];

                    _wypozyczalnia.ZarejestrujKlienta(new Klient(id, imie));
                }
            }
            else
            {
                MessageBox.Show("brak pliku klienci.txt");
            }

            if (File.Exists("auta.txt"))
            {
                string[] linieAuta = File.ReadAllLines("auta.txt");
                foreach (string linia in linieAuta)
                {
                    string[] dane = linia.Split(';');
                    string typ = dane[0];
                    string vin = dane[1];
                    string model = dane[2];
                    double stawka = double.Parse(dane[3]);

                    if (typ == "Spalinowe")
                    {
                        _wypozyczalnia.DodajAuto(new AutoSpalinowe(vin, model, stawka));
                    }
                    else if (typ == "Elektryczne")
                    {
                        _wypozyczalnia.DodajAuto(new AutoElektryczne(vin, model, stawka));
                    }
                }
            }
            else
            {
                MessageBox.Show("brak pliku auta.txt");
            }
        }

        private void ZapiszHistorieDoJson()
        {
            List<WypozyczenieTemp> daneDoZapisu = new List<WypozyczenieTemp>();

            foreach (Wypozyczenie w in _wypozyczalnia.Historia)
            {
                WypozyczenieTemp noweDane = new WypozyczenieTemp();
                noweDane.Id = w.IdWypozyczenia;
                noweDane.Klient = w.Klient.ImieNazwisko;
                noweDane.Auto = w.WypozyczoneAuto.Model;
                noweDane.DataWypozyczenia = w.DataWypozyczenia;
                noweDane.DataZwrotu = w.DataZwrotu;

                if (w.DataZwrotu.HasValue)
                {
                    noweDane.Status = "zakoñczone";
                    noweDane.CalkowityKoszt = w.ObliczCalkowityKoszt();
                }
                else
                {
                    noweDane.Status = "w trakcie";
                    noweDane.CalkowityKoszt = 0.0;
                }

                daneDoZapisu.Add(noweDane);
            }

            var opcje = new JsonSerializerOptions { WriteIndented = true };

            string jsonTekst = JsonSerializer.Serialize(daneDoZapisu, opcje);
            File.WriteAllText("historia_wypozyczen.json", jsonTekst);

            MessageBox.Show("zapisano historiê do pliku historia_wypozyczen.json");
        }

        private void InitializeComponent()
        {
            buttonPokazAuta = new Button();
            listBoxAuta = new ListBox();
            listBoxKlienci = new ListBox();
            listBoxWypozyczenia = new ListBox();
            buttonWypozycz = new Button();
            buttonZwroc = new Button();
            datePickerZwrotu = new DateTimePicker();
            buttonHistoria = new Button();
            textBoxSzukaj = new TextBox();
            SuspendLayout();
            // 
            // buttonPokazAuta
            // 
            buttonPokazAuta.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPokazAuta.Location = new Point(54, 488);
            buttonPokazAuta.Name = "buttonPokazAuta";
            buttonPokazAuta.Size = new Size(282, 127);
            buttonPokazAuta.TabIndex = 0;
            buttonPokazAuta.Text = "Poka¿ auta";
            buttonPokazAuta.UseVisualStyleBackColor = true;
            buttonPokazAuta.Click += buttonPokazAuta_Click;
            // 
            // listBoxAuta
            // 
            listBoxAuta.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxAuta.FormattingEnabled = true;
            listBoxAuta.ItemHeight = 15;
            listBoxAuta.Location = new Point(54, 47);
            listBoxAuta.Name = "listBoxAuta";
            listBoxAuta.Size = new Size(711, 199);
            listBoxAuta.TabIndex = 1;
            // 
            // listBoxKlienci
            // 
            listBoxKlienci.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxKlienci.FormattingEnabled = true;
            listBoxKlienci.ItemHeight = 15;
            listBoxKlienci.Location = new Point(771, 47);
            listBoxKlienci.Name = "listBoxKlienci";
            listBoxKlienci.Size = new Size(429, 199);
            listBoxKlienci.TabIndex = 2;
            // 
            // listBoxWypozyczenia
            // 
            listBoxWypozyczenia.FormattingEnabled = true;
            listBoxWypozyczenia.ItemHeight = 15;
            listBoxWypozyczenia.Location = new Point(54, 252);
            listBoxWypozyczenia.Name = "listBoxWypozyczenia";
            listBoxWypozyczenia.Size = new Size(1146, 199);
            listBoxWypozyczenia.TabIndex = 3;
            // 
            // buttonWypozycz
            // 
            buttonWypozycz.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonWypozycz.Location = new Point(342, 488);
            buttonWypozycz.Name = "buttonWypozycz";
            buttonWypozycz.Size = new Size(282, 127);
            buttonWypozycz.TabIndex = 4;
            buttonWypozycz.Text = "Wypo¿ycz";
            buttonWypozycz.UseVisualStyleBackColor = true;
            buttonWypozycz.Click += buttonWypozycz_Click;
            // 
            // buttonZwroc
            // 
            buttonZwroc.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonZwroc.Location = new Point(630, 488);
            buttonZwroc.Name = "buttonZwroc";
            buttonZwroc.Size = new Size(282, 127);
            buttonZwroc.TabIndex = 5;
            buttonZwroc.Text = "Zwróæ";
            buttonZwroc.UseVisualStyleBackColor = true;
            buttonZwroc.Click += buttonZwroc_Click;
            // 
            // datePickerZwrotu
            // 
            datePickerZwrotu.Location = new Point(630, 457);
            datePickerZwrotu.Name = "datePickerZwrotu";
            datePickerZwrotu.Size = new Size(282, 23);
            datePickerZwrotu.TabIndex = 6;
            // 
            // buttonHistoria
            // 
            buttonHistoria.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonHistoria.Location = new Point(918, 488);
            buttonHistoria.Name = "buttonHistoria";
            buttonHistoria.Size = new Size(282, 127);
            buttonHistoria.TabIndex = 7;
            buttonHistoria.Text = "Zapisz historiê";
            buttonHistoria.UseVisualStyleBackColor = true;
            buttonHistoria.Click += button1_Click;
            // 
            // textBoxSzukaj
            // 
            textBoxSzukaj.Location = new Point(54, 18);
            textBoxSzukaj.Name = "textBoxSzukaj";
            textBoxSzukaj.Size = new Size(711, 23);
            textBoxSzukaj.TabIndex = 8;
            textBoxSzukaj.TextChanged += textBoxSzukaj_TextChanged;
            // 
            // MainForm
            // 
            ClientSize = new Size(1284, 620);
            Controls.Add(textBoxSzukaj);
            Controls.Add(buttonHistoria);
            Controls.Add(datePickerZwrotu);
            Controls.Add(buttonZwroc);
            Controls.Add(buttonWypozycz);
            Controls.Add(listBoxWypozyczenia);
            Controls.Add(listBoxKlienci);
            Controls.Add(listBoxAuta);
            Controls.Add(buttonPokazAuta);
            Name = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        private void buttonPokazAuta_Click(object sender, EventArgs e)
        {
            listBoxAuta.DataSource = _wypozyczalnia.Auta;
            listBoxKlienci.DataSource = _wypozyczalnia.Klienci;
        }

        private void buttonWypozycz_Click(object sender, EventArgs e)
        {
            if (listBoxAuta.SelectedItem is Pojazd wybraneAuto && listBoxKlienci.SelectedItem is Klient wybranyKlient)
            {
                try
                {
                    IRegulaOplat regula = new RegulaDniowa();
                    _wypozyczalnia.ZarejestrujWypozyczenie(wybranyKlient, wybraneAuto, regula);
                    OdswiezWidok();
                    MessageBox.Show($"{wybranyKlient.ImieNazwisko} wypo¿yczy³ {wybraneAuto.Model}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz auto i klienta z list");
            }
        }

        private void buttonZwroc_Click(object sender, EventArgs e)
        {
            if (listBoxWypozyczenia.SelectedItem is Wypozyczenie wybraneWypozyczenie)
            {
                try
                {
                    DateTime wybranaData = datePickerZwrotu.Value;
                    double doZaplaty = _wypozyczalnia.ZarejestrujZwrot(wybraneWypozyczenie.IdWypozyczenia, wybranaData);

                    OdswiezWidok();
                    MessageBox.Show($"Auto zwrócone w dniu {wybranaData.ToShortDateString()}\nDo zap³aty: {doZaplaty} PLN");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz wypo¿yczenie do zwrotu");
            }
        }
        private void OdswiezWidok()
        {
            listBoxAuta.DataSource = null;
            listBoxAuta.DataSource = _wypozyczalnia.Auta;

            listBoxKlienci.DataSource = null;
            listBoxKlienci.DataSource = _wypozyczalnia.Klienci;

            listBoxWypozyczenia.DataSource = null;
            listBoxWypozyczenia.DataSource = _wypozyczalnia.Historia;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZapiszHistorieDoJson();
        }

        private void textBoxSzukaj_TextChanged(object sender, EventArgs e)
        {
            string fraza = textBoxSzukaj.Text.ToLower();

            List<Pojazd> znalezioneAuta = new List<Pojazd>();
            foreach (Pojazd auto in _wypozyczalnia.Auta)
            {
                if (auto.Model.ToLower().Contains(fraza))
                {
                    znalezioneAuta.Add(auto);
                }
            }
            listBoxAuta.DataSource = null;
            listBoxAuta.DataSource = znalezioneAuta;
        }
    }
    public class WypozyczenieTemp
    {
        public int Id { get; set; }
        public string Klient { get; set; }
        public string Auto { get; set; }
        public DateTime DataWypozyczenia { get; set; }
        public DateTime? DataZwrotu { get; set; }
        public string Status { get; set; }
        public double CalkowityKoszt { get; set; }
    }
}

