using System;
using System.Data;
using System.Windows.Forms;
using Klasse;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
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
        string Version = "20190911";

        public List<Schueler> SchuelerDerAktivenKlasse { get; private set; }
        public Schueler AktiverSchueler { get; private set; }

        public Form1()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel1.Text = "© " + DateTime.Now.Year + " Stefan Bäumer          Zähler: " + Properties.Settings.Default.counter.ToString().PadRight(5) + "                         Version " + Version;
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler(HandleDoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(HandleWorkerCompleted);
                worker.ProgressChanged += new ProgressChangedEventHandler(HandleProgressChanged);
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                lblStartup.Text = ex.Message;
            }
        }

        private void HandleDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                PrinterSettings printer = new PrinterSettings()
                {
                    PrinterName = @"Magicard Rio Pro (V2)"
                };

                if (printer.IsValid)
                {
                    Console.WriteLine("Supported Resolutions:");

                    foreach (PrinterResolution resolution in
                      printer.PrinterResolutions)
                    {
                        Console.WriteLine("  {0}", resolution);
                    }
                    Console.WriteLine();

                    // Display the list of valid paper sizes.
                    Console.WriteLine("Supported Paper Sizes:");

                    foreach (PaperSize size in printer.PaperSizes)
                    {
                        if (Enum.IsDefined(size.Kind.GetType(), size.Kind))
                        {
                            Console.WriteLine("  {0}", size);
                        }
                    }
                    int aktSj = DateTime.Now.Month >= 8 ? DateTime.Now.Year : DateTime.Now.AddYears(-1).Year;
                    klasses = new Klasses(aktSj);
                    //MessageBox.Show(klasses.Count() + " Klassen");
                    schuelers = new Schuelers(aktSj);
                    //MessageBox.Show(schuelers.Count() + " Schüler");
                    for (int i = 0; i < klasses.Count; i++)
                    {
                        if (!(from s in schuelers where s.Klasse == klasses[i].NameAtlantis select s).Any())
                        {
                            klasses.Remove(klasses[i]);
                        }
                    }
                }
                else
                {
                    throw new Exception("Der Drucker ist nicht bereit. \n\nIst der Drucker '" + printer.PrinterName + "' installiert und eingeschaltet? \n\nIst das Netzwerkkabel eingesteckt? \n\nDer Drucker hat die IP 192.168.134.164");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
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
            listBoxKlasse.DataSource = (from k in klasses
                                        select k.NameAtlantis).ToList();
            listBoxKlasse.SetSelected(0, false);
            lblStartup.Visible = false;
        }

        private void ListBoxKlasse_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxKlasse.SelectedItem != null)
                {
                    TrackSelectionChange((ListBox)sender, listBox1_selection);
                }
                btnAlle.Text = "Alle Schüler der " + aktiveKlasse + " auswählen";
                btnAlle.Enabled = true;
            }
            catch (Exception ex)
            {
                lblStartup.Text = ex.Message;
            }
        }

        private void TrackSelectionChange(ListBox lb, List<int> selection)
        {
            try
            {
                ListBox.SelectedIndexCollection sic = lb.SelectedIndices;
                foreach (int index in sic)
                    if (!selection.Contains(index)) selection.Add(index);

                foreach (int index in new List<int>(selection))
                    if (!sic.Contains(index)) selection.Remove(index);

                if (lb.Name != "listBoxSchueler")
                {
                    aktiveKlasse = klasses[listBox1_selection[listBox1_selection.Count - 1]].NameAtlantis;
                    SchuelerDerAktivenKlasse = (from s in schuelers
                                                where !s.Nachname.Contains(s.Klasse)
                                                where s.Vorname.Length > 1
                                                where s.Klasse == aktiveKlasse
                                                select s).ToList();
                    listBoxSchueler.DataSource = (from s in SchuelerDerAktivenKlasse select s.Nachname + ", " + s.Vorname).ToList();

                    for (int i = 0; i < SchuelerDerAktivenKlasse.Count; i++)
                    {
                        if ((from g in gewählteSchüler where g.Nachname == SchuelerDerAktivenKlasse[i].Nachname && g.Vorname == SchuelerDerAktivenKlasse[i].Vorname && g.Klasse == SchuelerDerAktivenKlasse[0].Klasse select g).Any())
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
            catch (Exception ex)
            {
                lblStartup.Text = ex.Message;
            }
        }

        private void BtnAlle_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxSchueler.BeginUpdate();

                if (btnAlle.Text.Contains("abwählen"))
                {
                    btnAlle.Text = "Alle SuS der " + aktiveKlasse + " auswählen";

                    for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                    {
                        listBoxSchueler.SetSelected(i, false);
                        gewählteSchüler.Remove(SchuelerDerAktivenKlasse[i]);
                    }
                }
                else
                {
                    btnAlle.Text = "Alle SuS der " + aktiveKlasse + " abwählen";

                    for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                    {
                        listBoxSchueler.SetSelected(i, true);
                        if (!(from g in gewählteSchüler where g.Nachname == SchuelerDerAktivenKlasse[i].Nachname && g.Vorname == SchuelerDerAktivenKlasse[i].Vorname && SchuelerDerAktivenKlasse[i].Klasse == g.Klasse select g).Any())
                        {
                            gewählteSchüler.Add(SchuelerDerAktivenKlasse[i]);
                        }
                    }
                }

                listBoxSchueler.EndUpdate();
                RenderButton();
            }
            catch (Exception ex)
            {
                lblStartup.Text = ex.Message;
            }
        }

        private void BtnDrucken_Click(object sender, EventArgs e)
        {
            try
            {
                var gewählteS = (gewählteSchüler.AsQueryable().OrderBy(sc => sc.Klasse).ThenBy(sc => sc.Nachname).ThenBy(sc => sc.Vorname));
                double faktor = 1;
                int breiteGesamt = Convert.ToInt32(338 * faktor);
                int höheGesamt = Convert.ToInt32(214 * faktor);
                int obereLinkeEckeFotoX = Convert.ToInt32(breiteGesamt * 0.6);
                int obereLinkeEckeFotoY = Convert.ToInt32(höheGesamt * 0.25);
                int obereLinkeEckeSchulleiterX = Convert.ToInt32(breiteGesamt * 0.3);
                int obereLinkeEckeSchulleiterY = Convert.ToInt32(höheGesamt * 0.55);
                int linkeSpalteX = Convert.ToInt32(breiteGesamt * 0.05);
                int rechteSpalteX = Convert.ToInt32(breiteGesamt * 0.3);
                int ersteZeileY = Convert.ToInt32(0.3 * höheGesamt) + 5;
                int zweiteZeileY = Convert.ToInt32(höheGesamt * 0.45) + 5;
                int dritteZeileY = Convert.ToInt32(höheGesamt * 0.6) + 5;
                int imageW = Convert.ToInt32(breiteGesamt * 0.3);
                int schriftGroß = Convert.ToInt32(höheGesamt * 0.07);
                int schriftKlein = Convert.ToInt32(höheGesamt * 0.02);
                int schriftFoto = Convert.ToInt32(höheGesamt * 0.035);

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Landscape = true;

                pd.DefaultPageSettings.PaperSize = new PaperSize("CR80-Karte", 642, 1016);

                PrintDialog printDialog1 = new PrintDialog()
                {
                    Document = pd
                };

                printDialog1.PrinterSettings.PrinterName = @"Magicard Rio Pro (V2)";

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < gewählteS.Count(); i++)
                    {
                        pd.PrintPage += delegate (object o, PrintPageEventArgs printPageEventArgs)
                        {
                            Graphics graphics = printPageEventArgs.Graphics;
                            Image imageSL = Image.FromFile(@"\\\\fs01\\SoftwarehausHeider\\Atlantis\\Dokumente\\jpg\\schulleiterUnterschrift.jpg");
                            graphics.DrawImage(imageSL, 170, 100, 80, 61);

                            printPageEventArgs.Graphics.DrawString("Schulleiterin/Headmaster", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, obereLinkeEckeSchulleiterX + 75, dritteZeileY + 18);

                            if (gewählteSchüler[i].BildPfad != null)
                            {
                                graphics = printPageEventArgs.Graphics;
                                Image image = Image.FromFile(gewählteSchüler[i].BildPfad);
                                graphics.DrawImage(image, 250, 58, 76, 101);
                            }
                            else
                            {
                                string text = "Der Ausweis ist nur zusammen mit einem Lichtbildausweis gültig.";
                                using (Font font = new Font("Tahoma", schriftFoto, FontStyle.Regular, GraphicsUnit.Point))
                                {
                                    StringFormat formatter = new StringFormat()
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    };
                                    Rectangle rect = new Rectangle(250, 58, 76, 101);

                                    Pen pen = new Pen(Color.Black, 1);
                                    printPageEventArgs.Graphics.DrawRectangle(pen, rect);

                                    printPageEventArgs.Graphics.DrawString(text, font, Brushes.Black, rect, formatter);

                                }
                            }

                            printPageEventArgs.Graphics.DrawString("Name/Name", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, linkeSpalteX, ersteZeileY);
                            printPageEventArgs.Graphics.DrawString(gewählteSchüler[i].Vorname + " " + gewählteSchüler[i].Nachname, new Font("Tahoma", schriftGroß, FontStyle.Regular), Brushes.Black, linkeSpalteX, ersteZeileY + Convert.ToInt32(0.01 * höheGesamt));

                            printPageEventArgs.Graphics.DrawString("Geburtsdatum/Date of birth", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, linkeSpalteX, zweiteZeileY);
                            printPageEventArgs.Graphics.DrawString(gewählteSchüler[i].Geburtsdatum.ToShortDateString(), new Font("Tahoma", schriftGroß, FontStyle.Regular), Brushes.Black, linkeSpalteX, zweiteZeileY + Convert.ToInt32(0.01 * höheGesamt));

                            printPageEventArgs.Graphics.DrawString("Gültig bis/Valid until", new Font("Tahoma", schriftKlein, FontStyle.Italic), Brushes.Black, linkeSpalteX, dritteZeileY);
                            printPageEventArgs.Graphics.DrawString(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(gewählteSchüler[i].EntlassdatumVoraussichtlich.Month) + " " + gewählteSchüler[i].EntlassdatumVoraussichtlich.Year, new Font("Tahoma", schriftGroß, FontStyle.Regular), Brushes.Black, linkeSpalteX, dritteZeileY + Convert.ToInt32(0.01 * höheGesamt));
                            
                        };
                        pd.Print();
                        LogWriter.LogWrite(gewählteSchüler[i].Nachname + "," + gewählteSchüler[i].Vorname + "," + gewählteSchüler[i].Geburtsdatum.ToShortDateString());
                    }
                    for (int i = 0; i < listBoxSchueler.Items.Count; i++)
                    {
                        listBoxSchueler.SetSelected(i, false);
                    }
                    Properties.Settings.Default.counter += gewählteSchüler.Count();
                    Properties.Settings.Default.Save();
                    gewählteSchüler = new List<Schueler>();
                    listBoxKlasse.SetSelected(0, false);
                    RenderButton();
                }
                else
                {
                    MessageBox.Show("Sie haben den Druckvorgang abgebrochen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            toolStripStatusLabel1.Text = "© " + DateTime.Now.Year + " BM                         Zähler: " + Properties.Settings.Default.counter.ToString().PadRight(5) + "                         Version " + Version;
        }

        private void ListBoxSchueler_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                lblStartup.Text = ex.Message;
            }
        }

        private void RenderButton()
        {
            try
            {
                if (gewählteSchüler.Count > 0)
                {
                    btnDrucken.Enabled = true;

                    string klassen = "";

                    foreach (var klasse in (gewählteSchüler.Select(x => x.Klasse).Distinct()))
                    {
                        klassen += klasse + ", ";
                    }
                    klassen = klassen.TrimEnd(' ');
                    klassen = klassen.TrimEnd(',');

                    if ((gewählteSchüler.Select(x => x.Klasse).Distinct().Count() == 1))
                    {
                        klassen = "der Klasse " + klassen;
                    }
                    else
                    {
                        klassen = "den Klassen " + klassen;
                    }
                    btnDrucken.Text = "Hier klicken, um " + gewählteSchüler.Count + " Schüler aus " + klassen + " zu drucken.";
                }
                else
                {
                    btnDrucken.Enabled = false;
                    btnDrucken.Text = "Wählen Sie einen oder mehrere Schüler!";
                }
            }
            catch (Exception ex)
            {
                lblStartup.Text = ex.Message;
            }
        }
    }
}