using System;
using System.Data;
using System.Windows.Forms;
using Klasse;
using SchuelerDLL;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;

// Published under the terms of GNU GENERAL PUBLIC LICENSE Version 3 
// © 2017 Stefan Bäumer

namespace schuelerausweis
{
    public partial class Form1 : Form
    {
        Klasses klasses;
        Schuelers schuelers;
        List<Schueler> gewählteSchüler = new List<Schueler>();
        string zuletztGewählteKlasse = "";
        List<Schueler> gewählteSchülerDieserKlasse = new List<Schueler>();


        public Form1()
        {
            InitializeComponent();                                           
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            int aktSj = DateTime.Now.Month >= 8 ? DateTime.Now.Year : DateTime.Now.AddYears(-1).Year;

            klasses = new Klasses(aktSj);
            schuelers = new Schuelers(aktSj);

            listBoxKlasse.DataSource = (from k in klasses select k.NameAtlantis).ToList();
            listBoxKlasse.SetSelected(0, false);
        }

        List<int> listBox1_selection = new List<int>();

        public List<Schueler> schuelerDerAktivenKlasse { get; private set; }

        private void ListBoxKlasse_Click(object sender, EventArgs e)
        {
            if (listBoxKlasse.SelectedItem != null)
            {
                toolStripStatusLabel1.Text = "Has " + (listBoxKlasse.SelectedItems.Count.ToString()) + " item(s) selected." + ((ListBox)sender).SelectedItem.ToString();                
                TrackSelectionChange((ListBox)sender, listBox1_selection);
            }
        }

        private void TrackSelectionChange(ListBox lb, List<int> selection)
        {
            ListBox.SelectedIndexCollection sic = lb.SelectedIndices;
            foreach (int index in sic)
                if (!selection.Contains(index)) selection.Add(index);

            foreach (int index in new List<int>(selection))
                if (!sic.Contains(index)) selection.Remove(index);
            zuletztGewählteKlasse = klasses[listBox1_selection[listBox1_selection.Count - 1]].NameAtlantis;
            schuelerDerAktivenKlasse = (from s in schuelers where s.Klasse == zuletztGewählteKlasse select s).ToList();
            listBoxSchueler.DataSource = (from s in schuelerDerAktivenKlasse select s.Nachname + ", " + s.Vorname).ToList();
                
            for (int i = 0; i < schuelerDerAktivenKlasse.Count; i++)
            {
                if ((from g in gewählteSchüler where g.Nachname == schuelerDerAktivenKlasse[i].Nachname && g.Vorname == schuelerDerAktivenKlasse[i].Vorname && g.Klasse == schuelerDerAktivenKlasse[0].Klasse select g).Any())
                {
                    listBoxSchueler.SetSelected(i, true);
                }
                else
                {
                    listBoxSchueler.SetSelected(0, false);
                }                  
            }
        }

        private void BtnAlle_Click(object sender, EventArgs e)
        {
            listBoxSchueler.BeginUpdate();

            if (btnAlle.Text == "Alle abwählen")
            {
                btnAlle.Text = "Alle auswählen";

                for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                {
                    listBoxSchueler.SetSelected(i, false);
                }
            }
            else
            {
                btnAlle.Text = "Alle abwählen";
                
                for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                {
                    listBoxSchueler.SetSelected(i, true);
                }
            }
            listBoxSchueler.EndUpdate();
        }
        
        private void BtnDrucken_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            pd.Print();
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(gewählteSchüler[0].BildPfad);
            Point loc = new Point(100, 100);
            e.Graphics.DrawImage(img, loc);
        }

        private void ListBoxSchueler_Click(object sender, EventArgs e)
        {
            TrackSelectionChange((ListBox)sender, listBox1_selection);
            //gewählteSchüler.Add(zuletztGewählterSchüler);

            gewählteSchülerDieserKlasse = new List<Schueler>();

            foreach (var item in listBoxSchueler.SelectedItems)
            {
                gewählteSchülerDieserKlasse.Add((from s in schuelerDerAktivenKlasse where item.ToString().Contains(s.Vorname) && item.ToString().Contains(s.Nachname) && s.Klasse == zuletztGewählteKlasse select s).FirstOrDefault());
            }

            btnDrucken.Text = gewählteSchüler.Count + " Schüler aus " + (listBoxKlasse.SelectedItems.Count.ToString()) + " Klassen drucken.";
        }
    }
}