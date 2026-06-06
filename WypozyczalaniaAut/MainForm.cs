using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using WypozyczalaniaAut.Dane;
using WypozyczalaniaAut.Logika;
using WypozyczalaniaAut.Modele;
using WypozyczalaniaAut.Reguly;

namespace WypozyczalaniaAut
{
    public partial class MainForm : Form
    {
        private Wypozyczalnia _wypozyczalnia;
        private IRepozytoriumDanych _repozytorium;

        public MainForm()
        {
            InitializeComponent();

            _repozytorium = new PlikoweRepozytoriumJson();
            _wypozyczalnia = _repozytorium.WczytajDane();

            comboBoxRegulaOplat.Items.Add("Dniowa");
            comboBoxRegulaOplat.Items.Add("Godzinowa");
            comboBoxRegulaOplat.SelectedIndex = 0;

            MessageBox.Show($"Za³adowane auta: {_wypozyczalnia.Auta.Count}, klienci: {_wypozyczalnia.Klienci.Count}");
        }

        private void ZapiszHistorieDoJson()
        {
            try
            {
                _repozytorium.ZapiszDane(_wypozyczalnia);
                MessageBox.Show("historia pomyœlnie zapisana");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"b³¹d zapisu: {ex.Message}");
            }
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
            textBoxNowyKlient = new TextBox();
            buttonDodajKlienta = new Button();
            label1 = new Label();
            comboBoxRegulaOplat = new ComboBox();
            this.textBoxStanPoZwrocie = new TextBox();
            this.label2 = new Label();
            SuspendLayout();
            // 
            // buttonPokazAuta
            // 
            buttonPokazAuta.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPokazAuta.Location = new Point(54, 501);
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
            listBoxKlienci.Size = new Size(429, 139);
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
            buttonWypozycz.Location = new Point(342, 501);
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
            buttonZwroc.Location = new Point(630, 501);
            buttonZwroc.Name = "buttonZwroc";
            buttonZwroc.Size = new Size(282, 127);
            buttonZwroc.TabIndex = 5;
            buttonZwroc.Text = "Zwróæ";
            buttonZwroc.UseVisualStyleBackColor = true;
            buttonZwroc.Click += buttonZwroc_Click;
            // 
            // datePickerZwrotu
            // 
            datePickerZwrotu.Location = new Point(630, 472);
            datePickerZwrotu.Name = "datePickerZwrotu";
            datePickerZwrotu.Size = new Size(205, 23);
            datePickerZwrotu.TabIndex = 6;
            // 
            // buttonHistoria
            // 
            buttonHistoria.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonHistoria.Location = new Point(918, 501);
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
            // textBoxNowyKlient
            // 
            textBoxNowyKlient.Location = new Point(771, 194);
            textBoxNowyKlient.Name = "textBoxNowyKlient";
            textBoxNowyKlient.Size = new Size(429, 23);
            textBoxNowyKlient.TabIndex = 9;
            // 
            // buttonDodajKlienta
            // 
            buttonDodajKlienta.Location = new Point(771, 223);
            buttonDodajKlienta.Name = "buttonDodajKlienta";
            buttonDodajKlienta.Size = new Size(429, 23);
            buttonDodajKlienta.TabIndex = 10;
            buttonDodajKlienta.Text = "Dodaj klienta";
            buttonDodajKlienta.UseVisualStyleBackColor = true;
            buttonDodajKlienta.Click += buttonDodajKlienta_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(439, 454);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 11;
            label1.Text = "Wybierz taryfê";
            // 
            // comboBoxRegulaOplat
            // 
            comboBoxRegulaOplat.FormattingEnabled = true;
            comboBoxRegulaOplat.Location = new Point(342, 472);
            comboBoxRegulaOplat.Name = "comboBoxRegulaOplat";
            comboBoxRegulaOplat.Size = new Size(282, 23);
            comboBoxRegulaOplat.TabIndex = 12;
            // 
            // textBoxStanPoZwrocie
            // 
            this.textBoxStanPoZwrocie.Location = new Point(841, 472);
            this.textBoxStanPoZwrocie.Name = "textBoxStanPoZwrocie";
            this.textBoxStanPoZwrocie.Size = new Size(71, 23);
            this.textBoxStanPoZwrocie.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(823, 454);
            this.label2.Name = "label2";
            this.label2.Size = new Size(105, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Stan paliwa/baterii";
            // 
            // MainForm
            // 
            ClientSize = new Size(1487, 709);
            Controls.Add(this.label2);
            Controls.Add(this.textBoxStanPoZwrocie);
            Controls.Add(comboBoxRegulaOplat);
            Controls.Add(label1);
            Controls.Add(buttonDodajKlienta);
            Controls.Add(textBoxNowyKlient);
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
                    IRegulaOplat regula;
                    if (comboBoxRegulaOplat.SelectedItem.ToString() == "Godzinowa")
                        regula = new RegulaGodzinowa();
                    else
                        regula = new RegulaDniowa();

                    _wypozyczalnia.ZarejestrujWypozyczenie(wybranyKlient, wybraneAuto, regula);
                    OdswiezWidok();
                    MessageBox.Show($"{wybranyKlient.ImieNazwisko} wypo¿yczy³ {wybraneAuto.Model}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "b³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("wybierz auto i klienta z list");
            }
        }

        private void buttonZwroc_Click(object sender, EventArgs e)
        {
            if (listBoxWypozyczenia.SelectedItem is Wypozyczenie wybraneWypozyczenie)
            {
                try
                {
                    if (!double.TryParse(textBoxStanPoZwrocie.Text, out double stanKoncowy))
                    {
                        MessageBox.Show("wpisz poprawny stan paliwa lub baterii");
                        return;
                    }

                    if (wybraneWypozyczenie.WypozyczoneAuto is AutoSpalinowe spalinowe)
                    {
                        spalinowe.ZaktualizujPaliwo(stanKoncowy);
                    }
                    else if (wybraneWypozyczenie.WypozyczoneAuto is AutoElektryczne elektryczne)
                    {
                        elektryczne.ZaktualizujBaterie(stanKoncowy);
                    }

                    DateTime wybranaData = datePickerZwrotu.Value;
                    double doZaplaty = _wypozyczalnia.ZarejestrujZwrot(wybraneWypozyczenie.IdWypozyczenia, wybranaData);

                    OdswiezWidok();
                    textBoxStanPoZwrocie.Clear();
                    MessageBox.Show($"auto zwrócone pomyœlnie\ndo zap³aty: {doZaplaty} PLN");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "b³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("wybierz wypo¿yczenie do zwrotu z listy");
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

        private void buttonDodajKlienta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxNowyKlient.Text))
            {
                int noweId = _wypozyczalnia.Klienci.Count + 1;
                Klient nowy = new Klient(noweId, textBoxNowyKlient.Text);
                _wypozyczalnia.ZarejestrujKlienta(nowy);

                File.AppendAllText("klienci.txt", $"\n{noweId};{textBoxNowyKlient.Text}");

                OdswiezWidok();
                textBoxNowyKlient.Clear();
                MessageBox.Show("dodano nowego klienta do systemu");
            }
            else
            {
                MessageBox.Show("wpisz imiê i nazwisko klienta");
            }
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

