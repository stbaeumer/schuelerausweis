using System;
using System.Data;
using System.Windows.Forms;
using Klasse;
using Schüler;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Globalization;

// Published under the terms of GNU GENERAL PUBLIC LICENSE Version 3 
// © 2017 Stefan Bäumer

namespace schuelerausweis
{
    public partial class Form1 : Form
    {
        Klasses klasses;
        Schuelers schuelers;
        List<Schueler> gewählteSchüler = new List<Schueler>();
        string aktiveKlasse = "";
        //List<Schueler> gewählteSchülerDieserKlasse = new List<Schueler>();
        List<int> listBox1_selection = new List<int>();
        public List<Schueler> schuelerDerAktivenKlasse { get; private set; }
        public Schueler aktiverSchueler { get; private set; }

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
            toolStripStatusLabel1.Text = "© " + DateTime.Now.Year + " BM";
        }
                
        private void ListBoxKlasse_Click(object sender, EventArgs e)
        {
            if (listBoxKlasse.SelectedItem != null)
            {                
                TrackSelectionChange((ListBox)sender, listBox1_selection);
            }
                        
            //for (int i = 0; i < listBoxKlasse.Items.Count; i++)
            //{
            //    if ((from s in gewählteSchüler where s.Klasse == listBoxKlasse.Items[i].ToString() select s).Any() || listBoxKlasse.Items[i].ToString() == aktiveKlasse)
            //    {
            //        listBoxKlasse.SetSelected(i, true);
            //    }
            //    else
            //    {
            //        listBoxKlasse.SetSelected(i, false);
            //    }
            //}
            btnAlle.Text = "Alle Schüler der " + aktiveKlasse + " auswählen";
            btnAlle.Enabled = true;
        }

        private void TrackSelectionChange(ListBox lb, List<int> selection)
        {
            ListBox.SelectedIndexCollection sic = lb.SelectedIndices;
            foreach (int index in sic)
                if (!selection.Contains(index)) selection.Add(index);

            foreach (int index in new List<int>(selection))
                if (!sic.Contains(index)) selection.Remove(index);

            if (lb.Name != "listBoxSchueler")
            {
                aktiveKlasse = klasses[listBox1_selection[listBox1_selection.Count - 1]].NameAtlantis;
                schuelerDerAktivenKlasse = (from s in schuelers where s.Klasse == aktiveKlasse select s).ToList();
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
        }

        private void BtnAlle_Click(object sender, EventArgs e)
        {
            listBoxSchueler.BeginUpdate();

            if (btnAlle.Text.Contains("abwählen"))
            {
                btnAlle.Text = "Alle SuS der " + aktiveKlasse + " auswählen";

                for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                {
                    listBoxSchueler.SetSelected(i, false);
                    gewählteSchüler.Remove(schuelerDerAktivenKlasse[i]);
                }                
            }
            else
            {
                btnAlle.Text = "Alle SuS der " + aktiveKlasse + " abwählen";

                for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                {
                    listBoxSchueler.SetSelected(i, true);
                    if (!(from g in gewählteSchüler where g.Nachname == schuelerDerAktivenKlasse[i].Nachname && g.Vorname == schuelerDerAktivenKlasse[i].Vorname && schuelerDerAktivenKlasse[i].Klasse == g.Klasse select g).Any())
                    {
                        gewählteSchüler.Add(schuelerDerAktivenKlasse[i]);
                    }
                }                
            }
            listBoxSchueler.EndUpdate();
            RenderButton();
        }
        
        private void BtnDrucken_Click(object sender, EventArgs e)
        {            
            var g = (gewählteSchüler.AsQueryable().OrderBy(sc => sc.Klasse).ThenBy(sc => sc.Nachname).ThenBy(sc => sc.Vorname));

            for (int i = 0; i < g.Count(); i++)
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 1169);
                pd.PrinterSettings.PrintToFile = true;
                pd.PrinterSettings.PrinterName = "Adobe PDF";
                pd.PrintPage += delegate (object o, PrintPageEventArgs ee)
                {
                    Image img_orig = Image.FromFile(gewählteSchüler[i].BildPfad);
                    var img = ResizeImage(img_orig, new Size(150, 200)); // Originale 300 * 400
                    Point loc = new Point(430, 70);
                    ee.Graphics.DrawImage(img, loc);
                    ee.Graphics.DrawString("Name/Name/Nom", new Font("Tahoma", 6, FontStyle.Italic), Brushes.Black, 100, 100);
                    ee.Graphics.DrawString(gewählteSchüler[i].Vorname + " " + gewählteSchüler[i].Nachname, new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 100, 108);

                    ee.Graphics.DrawString("Geburtsdatum/Date of birth", new Font("Tahoma", 6, FontStyle.Italic), Brushes.Black, 300, 100);
                    ee.Graphics.DrawString(gewählteSchüler[i].Geburtsdatum.ToShortDateString(), new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 300, 108);

                    ee.Graphics.DrawString("Gültig bis/Valid until/Date d`expiration", new Font("Tahoma", 6, FontStyle.Italic), Brushes.Black, 100, 150);
                    ee.Graphics.DrawString(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(gewählteSchüler[i].EntlassdatumVoraussichtlich.Month) + " " + gewählteSchüler[i].EntlassdatumVoraussichtlich.Year, new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 100, 158);

                    img = Image.FromFile(@"\\\\fs01\\SoftwarehausHeider\\Atlantis\\Dokumente\\jpg\\schulleiterUnterschrift.jpg");
                    loc = new Point(300, 150);
                    ee.Graphics.DrawImage(img, loc);
                };

                pd.Print();
            }
        }
                
        private void PrintPage(object o, PrintPageEventArgs e)
        {            
            Image img_orig = Image.FromFile(gewählteSchüler[0].BildPfad);
            var img = ResizeImage(img_orig, new Size(150, 200)); // Originale 300 * 400
            Point loc = new Point(430, 70);
            e.Graphics.DrawImage(img, loc);
            e.Graphics.DrawString("Name/Name/Nom", new Font("Tahoma", 6, FontStyle.Italic), Brushes.Black, 100, 100);
            e.Graphics.DrawString(gewählteSchüler[0].Vorname + " " + gewählteSchüler[0].Nachname, new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 100, 108);

            e.Graphics.DrawString("Geburtsdatum/Date of birth", new Font("Tahoma", 6, FontStyle.Italic), Brushes.Black, 300, 100);
            e.Graphics.DrawString(gewählteSchüler[0].Geburtsdatum.ToShortDateString(), new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 300, 108);

            e.Graphics.DrawString("Gültig bis/Valid until/Date d`expiration", new Font("Tahoma", 6, FontStyle.Italic), Brushes.Black, 100, 150);
            e.Graphics.DrawString(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(gewählteSchüler[0].EntlassdatumVoraussichtlich.Month) + " " + gewählteSchüler[0].EntlassdatumVoraussichtlich.Year, new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 100, 158);

            img = Image.FromFile(@"\\\\fs01\\SoftwarehausHeider\\Atlantis\\Dokumente\\jpg\\schulleiterUnterschrift.jpg");
            loc = new Point(300, 150);
            e.Graphics.DrawImage(img, loc);
        }

        private static Image ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width
            int sourceWidth = imgToResize.Width;
            //Get the image current height
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private void ListBoxSchueler_Click(object sender, EventArgs e)
        {
            foreach (var item in listBoxSchueler.Items)
            {
                foreach (var s in (from g in gewählteSchüler where g.Klasse == aktiveKlasse select g).ToList())
                {
                    gewählteSchüler.Remove(s);
                }
            }

            foreach (var item in listBoxSchueler.SelectedItems)
            {
                if (!(from g in gewählteSchüler where g.Klasse == aktiveKlasse where item.ToString().Contains(g.Vorname) && item.ToString().Contains(g.Nachname) select g).Any())
                {
                    gewählteSchüler.Add((from g in schuelers where g.Klasse == aktiveKlasse where item.ToString().Contains(g.Vorname) && item.ToString().Contains(g.Nachname) select g).FirstOrDefault());
                }
            }

            RenderButton();
        }

        private void RenderButton()
        {
            if (gewählteSchüler.Count > 0)
            {
                btnDrucken.Enabled = true;
                int anzahl = (gewählteSchüler.Select(x => x.Klasse).Distinct().Count());
                btnDrucken.Text = gewählteSchüler.Count + " Schüler aus " + anzahl + " Klasse" + (anzahl == 1 ? "" : "n") + " drucken.";
            }
            else
            {
                btnDrucken.Enabled = false;
                btnDrucken.Text = "DRUCKEN";
            }
        }        
    }
}