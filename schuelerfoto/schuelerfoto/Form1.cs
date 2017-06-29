using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// Published under the terms of GNU GENERAL PUBLIC LICENSE Version 3 
// © 2017 Stefan Bäumer

namespace schuelerausweis
{
    public partial class Form1 : Form
    {
        Schuelers schuelers = new Schuelers();

        public Form1()
        {
            InitializeComponent();                                   
            //backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            //backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            //backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            //backgroundWorker.WorkerReportsProgress = true;
            //backgroundWorker.WorkerSupportsCancellation = true;            
        }

        AutoCompleteStringCollection vornamen = new AutoCompleteStringCollection();
        AutoCompleteStringCollection nachnamen = new AutoCompleteStringCollection();
        AutoCompleteStringCollection klassen = new AutoCompleteStringCollection();

        private void Form1_Load(object sender, EventArgs e)
        {
            using (OdbcConnection connection = new OdbcConnection("Dsn=Atlantis9;uid=DBA"))
            {
                //string query = "SELECT us_id, kennung, passwort, le_id FROM DBA.benutzer";
                string query = @"SELECT schueler.name_1, schueler.name_2, schueler.dat_geburt from DBA.schueler, schue_sj, klasse, schue_sc, schule WHERE schue_sj.kl_id = klasse.kl_id AND schue_sc.sc_id = schule.sc_id AND schue_sc.ps_id = schue_sj.ps_id AND ((schue_sj.vorgang_schuljahr = check_null (hole_schuljahr_rech ('',  0)) AND schue_sj.vorgang_akt_satz_jn = 'J' AND   ausgetreten = 'N')";
                //string query = @"SELECT schueler.pu_id, schueler.name_1, schueler.name_2, schueler.dat_geburt, schueler.klasse_eingabe FROM schueler, schue_sj, klasse, schue_sc, schule WHERE schue_sj.kl_id = klasse.kl_id AND schue_sc.sc_id = schule.sc_id AND schue_sc.ps_id = schue_sj.ps_id AND ((schue_sj.vorgang_schuljahr 	= check_null (hole_schuljahr_rech ('',  0)) AND   schue_sj.vorgang_akt_satz_jn 	= 'J' AND   ausgetreten = 'N')";
                DataSet dataSet = new DataSet();
                OdbcDataAdapter adapter = new OdbcDataAdapter(query, connection);
                
                try
                {
                    connection.Open();
                    adapter.Fill(dataSet, "DBA.schueler");

                    foreach (DataRow theRow in dataSet.Tables["DBA.schueler"].Rows)
                    {
                        Schueler schueler = new Schueler()
                        {
                            Nachname = (String)theRow["name_1"],
                            Vorname = (String)theRow["name_2"],
                            Geburtsdatum = (DateTime)theRow["dat_geburt"]//,
                            //Klasse = (String)theRow["klasse_eingabe"]
                        };
                        schuelers.Add(schueler);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                Console.ReadKey();
            }

            //AutoCompleteMode wird auf SuggestAppend gestellt damit er sich verhält wie das altbekannte "Ausführen Fenster"
            tbxKlasse.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbxVorname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbxNachname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            
            // Damit die Textbox die oben definierte Liste empfangen kann muss der SourceTyp noch auf CustomSource gestellt werden.
            tbxKlasse.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbxVorname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbxNachname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            vornamen.AddRange(new string[] { "Berlin", "Hamburg", "Bremen", "Stuttgart", "Saarbrücken", "Frankfurt a.M." });

            // Liste anhängen ...
            tbxKlasse.AutoCompleteCustomSource = vornamen;
            tbxKlasse.AutoCompleteCustomSource = nachnamen;
            tbxKlasse.AutoCompleteCustomSource = klassen;            
        }
        private void TbxKlasse_TextChanged(object sender, EventArgs e)
        {
            Go();
        }

        private void Go()
        {
            if (tbxVorname.Text.Length > 2 && tbxNachname.Text.Length > 2 && tbxKlasse.Text.Length > 3)
            {
                if ((from s in schuelers where s.Klasse == tbxKlasse.Text && s.Vorname.Contains(tbxVorname.Text) && s.Nachname == tbxVorname.Text select s).Any())
                {
                    var schueler = (from s in schuelers select s).FirstOrDefault();
                    lblGeburtsdatum.Text = schueler.Geburtsdatum.ToShortDateString();
                    btnDrucken.Enabled = true;
                }
                else
                {
                    lblGeburtsdatum.Text = "";
                    btnDrucken.Enabled = false;
                }
            }
            else
            {
                lblGeburtsdatum.Text = "";
                btnDrucken.Enabled = false;
            }        
        }

        private void TbxNachname_TextChanged(object sender, EventArgs e)
        {
            Go();
        }

        private void TbxVorname_TextChanged(object sender, EventArgs e)
        {
            Go();
        }

        private void BtnDrucken_Click(object sender, EventArgs e)
        {




            tbxKlasse.Text = "";
            tbxNachname.Text = "";
            tbxKlasse.Text = "";
        }
    }
}