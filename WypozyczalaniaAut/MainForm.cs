using System;
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

            ZaladujDaneTestowe();

            MessageBox.Show($"Za³adowane auta: {_wypozyczalnia.Auta.Count}, klienci: {_wypozyczalnia.Klienci.Count}");
        }

        private void ZaladujDaneTestowe()
        {
            _wypozyczalnia.ZarejestrujKlienta(new Klient(1, "Wies³aw Paleta"));
            _wypozyczalnia.ZarejestrujKlienta(new Klient(2, "Jan Maczeta"));

            _wypozyczalnia.DodajAuto(new AutoSpalinowe("EJ9DJKGWNG", "Honda Civic", 250.0));
            _wypozyczalnia.DodajAuto(new AutoSpalinowe("G23LKLGNWG", "Seat Leon", 120.0));
            _wypozyczalnia.DodajAuto(new AutoSpalinowe("CZK4MEJGNJ", "Skoda Octavia", 135.0));
            _wypozyczalnia.DodajAuto(new AutoElektryczne("US39MEKGF", "Tesla Model Y", 300.0));
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
            SuspendLayout();
            // 
            // buttonPokazAuta
            // 
            buttonPokazAuta.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPokazAuta.Location = new Point(61, 481);
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
            listBoxAuta.Location = new Point(61, 40);
            listBoxAuta.Name = "listBoxAuta";
            listBoxAuta.Size = new Size(711, 199);
            listBoxAuta.TabIndex = 1;
            // 
            // listBoxKlienci
            // 
            listBoxKlienci.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxKlienci.FormattingEnabled = true;
            listBoxKlienci.ItemHeight = 15;
            listBoxKlienci.Location = new Point(778, 40);
            listBoxKlienci.Name = "listBoxKlienci";
            listBoxKlienci.Size = new Size(443, 199);
            listBoxKlienci.TabIndex = 2;
            // 
            // listBoxWypozyczenia
            // 
            listBoxWypozyczenia.FormattingEnabled = true;
            listBoxWypozyczenia.ItemHeight = 15;
            listBoxWypozyczenia.Location = new Point(61, 245);
            listBoxWypozyczenia.Name = "listBoxWypozyczenia";
            listBoxWypozyczenia.Size = new Size(1160, 199);
            listBoxWypozyczenia.TabIndex = 3;
            // 
            // buttonWypozycz
            // 
            buttonWypozycz.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            buttonWypozycz.Location = new Point(939, 481);
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
            buttonZwroc.Location = new Point(503, 481);
            buttonZwroc.Name = "buttonZwroc";
            buttonZwroc.Size = new Size(282, 127);
            buttonZwroc.TabIndex = 5;
            buttonZwroc.Text = "Zwróæ";
            buttonZwroc.UseVisualStyleBackColor = true;
            buttonZwroc.Click += buttonZwroc_Click;
            // 
            // datePickerZwrotu
            // 
            datePickerZwrotu.Location = new Point(503, 450);
            datePickerZwrotu.Name = "datePickerZwrotu";
            datePickerZwrotu.Size = new Size(282, 23);
            datePickerZwrotu.TabIndex = 6;
            // 
            // MainForm
            // 
            ClientSize = new Size(1284, 620);
            Controls.Add(datePickerZwrotu);
            Controls.Add(buttonZwroc);
            Controls.Add(buttonWypozycz);
            Controls.Add(listBoxWypozyczenia);
            Controls.Add(listBoxKlienci);
            Controls.Add(listBoxAuta);
            Controls.Add(buttonPokazAuta);
            Name = "MainForm";
            ResumeLayout(false);
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
    }

}

