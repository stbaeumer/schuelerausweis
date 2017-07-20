﻿using System;
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
using System.ComponentModel;
using System.Threading;

// Published under the terms of GNU GENERAL PUBLIC LICENSE Version 3 
// © 2017 Stefan Bäumer

namespace schuelerausweis
{
    public partial class Form1 : Form
    {
        BackgroundWorker worker = new BackgroundWorker();
        SynchronizationContext _syncContext;

        Klasses klasses;
        Schuelers schuelers;
        List<Schueler> gewählteSchüler = new List<Schueler>();
        string aktiveKlasse = "";
        List<int> listBox1_selection = new List<int>();

        public List<Schueler> schuelerDerAktivenKlasse { get; private set; }
        public Schueler aktiverSchueler { get; private set; }
        
        public Form1()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {        
            toolStripStatusLabel1.Text = "© " + DateTime.Now.Year + " BM";
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(HandleDoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(HandleWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(HandleProgressChanged);
            worker.RunWorkerAsync();
        }

        private void HandleDoWork(object sender, DoWorkEventArgs e)
        {
            int aktSj = DateTime.Now.Month >= 8 ? DateTime.Now.Year : DateTime.Now.AddYears(-1).Year;
            klasses = new Klasses(aktSj);
            schuelers = new Schuelers(aktSj);            
        }

        private void HandleProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SendOrPostCallback callback = new SendOrPostCallback((o) =>
            {
                lblStartup.Text = lblStartup.Text + ".";
            });
            _syncContext.Send(callback, null);
        }

        private void HandleWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBoxKlasse.DataSource = (from k in klasses select k.NameAtlantis).ToList();
            listBoxKlasse.SetSelected(0, false);
            lblStartup.Visible = false;         
        }

        private void ListBoxKlasse_Click(object sender, EventArgs e)
        {
            if (listBoxKlasse.SelectedItem != null)
            {                
                TrackSelectionChange((ListBox)sender, listBox1_selection);
            }                        
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

            int breiteGesamt = 1200;
            int höheGesamt = Convert.ToInt32(0.6 * breiteGesamt);
            int imageX = Convert.ToInt32(breiteGesamt * 0.7);
            int imageY = Convert.ToInt32(höheGesamt * 0.25);
            int linkeSpalteX = Convert.ToInt32(breiteGesamt * 0.05);
            int rechteSpalteX = Convert.ToInt32(breiteGesamt * 0.4);
            int ersteZeileY = Convert.ToInt32(0.3 * höheGesamt);
            int zweiteZeileY = Convert.ToInt32(höheGesamt * 0.45);
            int dritteZeileY = Convert.ToInt32(höheGesamt * 0.6);
            int imageW = Convert.ToInt32(breiteGesamt * 0.3);
            int schriftGroß = Convert.ToInt32(höheGesamt * 0.07);
            int schriftKlein = Convert.ToInt32(höheGesamt * 0.02);

            for (int i = 0; i < g.Count(); i++)
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("CreditCard", breiteGesamt, höheGesamt);
                pd.PrinterSettings.PrintToFile = true;
                pd.PrinterSettings.PrinterName = "Adobe PDF";
                pd.PrintPage += delegate (object o, PrintPageEventArgs ee)
                {
                    if (gewählteSchüler[i].BildPfad != null)
                    {
                        Image image = Image.FromFile(gewählteSchüler[i].BildPfad);
                        //var img1 = ResizeImage(imageW, (Int32)(image.Height / image.Width * imageW), image); // Originale 300 * 400
                        Point loc1 = new Point(imageX, imageY);
                        ee.Graphics.DrawImage(image, loc1);
                    }
                    else
                    {
                        string text = "Der Ausweis ist nur zusammen mit einem Lichtbildausweis gültig.";
                        using (Font font = new Font("Tahoma", schriftKlein, FontStyle.Regular, GraphicsUnit.Point))
                        {
                            RectangleF rectF = new RectangleF(imageX, imageY, Convert.ToInt32(breiteGesamt * 0.2), Convert.ToInt32(höheGesamt * 0.3));
                            StringFormat formatter = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };                            
                            ee.Graphics.DrawString(text, font, Brushes.Black, rectF, formatter);
                            ee.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF));
                        }
                    }
                    
                    ee.Graphics.DrawString("Name/Name/Nom", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, linkeSpalteX, ersteZeileY);
                    ee.Graphics.DrawString(gewählteSchüler[i].Vorname + " " + gewählteSchüler[i].Nachname, new Font("Tahoma", schriftGroß, FontStyle.Regular), Brushes.Black, linkeSpalteX, ersteZeileY + Convert.ToInt32(0.01 * höheGesamt));

                    ee.Graphics.DrawString("Geburtsdatum/Date of birth/Date de naissance", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, linkeSpalteX, zweiteZeileY);
                    ee.Graphics.DrawString(gewählteSchüler[i].Geburtsdatum.ToShortDateString(), new Font("Tahoma", schriftGroß, FontStyle.Regular), Brushes.Black, linkeSpalteX, zweiteZeileY + Convert.ToInt32(0.01 * höheGesamt));

                    ee.Graphics.DrawString("Gültig bis/Valid until/Date d`expiration", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, linkeSpalteX, dritteZeileY);
                    ee.Graphics.DrawString(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(gewählteSchüler[i].EntlassdatumVoraussichtlich.Month) + " " + gewählteSchüler[i].EntlassdatumVoraussichtlich.Year, new Font("Tahoma", schriftGroß, FontStyle.Regular), Brushes.Black, linkeSpalteX, dritteZeileY + Convert.ToInt32(0.01 * höheGesamt));

                    var img = Image.FromFile(@"\\\\fs01\\SoftwarehausHeider\\Atlantis\\Dokumente\\jpg\\schulleiterUnterschrift.jpg");
                    var loc = new Point(rechteSpalteX, zweiteZeileY);
                    ee.Graphics.DrawImage(img, loc);
                };

                pd.Print();
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public Image ResizeImage(int newWidth, int newHeight, Image image)
        {
            try
            {
                int sourceWidth = image.Width;
                int sourceHeight = image.Height;

                if (sourceWidth < sourceHeight)
                {
                    int buff = newWidth;

                    newWidth = newHeight;
                    newHeight = buff;
                }

                int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
                float nPercent = 0, nPercentW = 0, nPercentH = 0;

                nPercentW = ((float)newWidth / (float)sourceWidth);
                nPercentH = ((float)newHeight / (float)sourceHeight);
                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                    destX = System.Convert.ToInt16((newWidth - (sourceWidth * nPercent)) / 2);
                }
                else
                {
                    nPercent = nPercentW;
                    destY = System.Convert.ToInt16((newHeight - (sourceHeight * nPercent)) / 2);
                }

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap bmPhoto = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

                bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.Clear(Color.Black);
                grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                grPhoto.DrawImage(image, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
                grPhoto.Dispose();
                image.Dispose();
                return bmPhoto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            using (Bitmap bmpImage = new Bitmap(img))
            {
                return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            }
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