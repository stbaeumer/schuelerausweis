using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schuelerausweis
{
    public class Schuelers : List<Schueler>
    {
        public Schuelers(int aktSj)
        {
            var connetionStringAtlantis = @"Dsn=Atlantis17;uid=DBA";
            var bildDateien = Directory.GetFiles(@"\\fs01\SoftwarehausHeider\Atlantis\Dokumente\jpg", "*.jpg", SearchOption.AllDirectories).ToList();

            using (OdbcConnection connection = new OdbcConnection(connetionStringAtlantis))
            {
                DataSet dataSet = new DataSet();
                var aktSjAtlantis = aktSj.ToString() + "/" + (aktSj + 1 - 2000);

                OdbcDataAdapter schuelerAdapter = new OdbcDataAdapter(@"SELECT DBA.schueler.*,
DBA.klasse.*,
DBA.schue_sj.*
FROM(DBA.schue_sj JOIN DBA.klasse ON DBA.schue_sj.kl_id = DBA.klasse.kl_id) JOIN DBA.schueler ON DBA.schue_sj.pu_id = DBA.schueler.pu_id
WHERE DBA.schue_sj.vorgang_schuljahr = '" + aktSjAtlantis + "' ORDER BY DBA.schueler.name_1 ASC, DBA.schueler.name_2 ASC, DBA.klasse.klasse ASC", connection);

                try
                {
                    connection.Open();
                    schuelerAdapter.Fill(dataSet, "DBA.schueler");

                    foreach (DataRow theRow in dataSet.Tables["DBA.schueler"].Rows)
                    {
                        var schueler = new Schueler();
                        if (schueler != null)
                        {
                            schueler.IdAtlantis = theRow["pu_id"] == null ? -99 : Convert.ToInt32(theRow["pu_id"]);
                            schueler.Nachname = theRow["name_1"] == null ? "" : theRow["name_1"].ToString();
                            schueler.Vorname = theRow["name_2"] == null ? "" : theRow["name_2"].ToString();
                            schueler.Geburtsdatum = theRow["dat_geburt"].ToString().Length < 3 ? new DateTime() : DateTime.ParseExact(theRow["dat_geburt"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            schueler.Jahrgang = theRow["jahrgang"] == null ? "" : theRow["jahrgang"].ToString();
                            //schueler.KlasseNameAtlantis = schueler.GetAktuellOderZuletztBesuchteKlasse(theRow["x"] == null ? "" : theRow["x"].ToString());
                            schueler.Klasse = theRow["klasse"] == null ? "" : theRow["klasse"].ToString();
                            if (schueler.Klasse.StartsWith("DD"))
                            {
                                string asss = "";
                            }
                            //if (schueler.Klasse.Schuelers != null) schueler.Klasse.Schuelers.Add(schueler);
                            //schueler.Id = schueler.GetIdFromBarcode(theRow["x"] == null ? "" : theRow["x"].ToString());
                            //schueler.Alias = schueler.generateAlias();
                            schueler.BildPfad = (from p in bildDateien where p.Contains("_" + schueler.IdAtlantis + ".jpg") select p).FirstOrDefault();
                            //schueler.MailWebweaver = schueler.GenerateMailWebweaver();
                            //schueler.MailAtlantis = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.AnredeHerrFrau = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.DatumEintrittGrundschule = theRow["x"].ToString().Length < 3 ? new DateTime() : DateTime.ParseExact(theRow["x"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            //schueler.Jahrgang = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.Muttersprache = theRow["x"] == null ? "" : theRow["x"].ToString();
                            schueler.Geburtsort = theRow["gebort_lkrs"] == null ? "" : theRow["gebort_lkrs"].ToString();
                            //schueler.Geschlecht = theRow["x"] == null ? "" : theRow["x"].ToString();
                            string a = "";
                            //schueler.Gliederung = schueler.GetGliederung(theRow["x"] == null ? "" : theRow["x"].ToString());
                            //schueler.EintrittBildungsgang = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.Fachklasse = schueler.GetFachklasse(theRow["x"] == null ? "" : theRow["x"].ToString());

                            //schueler.FachklasseSchild = schueler.Fachklasse == "" ? "" : "170-" + schueler.Fachklasse.Substring(0, 3) + "-" + schueler.Fachklasse.Substring(3, 2);
                            ////schueler.Status = GetStatus(theRow["x"] == null ? "" : theRow["x"].ToString());
                            ////schueler.LfdNr = lfdNr; lfdNr++;
                            //schueler.Aussiedler = (theRow["x"] == null ? "" : theRow["x"].ToString()).StartsWith("A") ? "J" : "N"; // AS
                            //schueler.OrgForm = schueler.Klasse.OrgForm == null ? "" : schueler.Klasse.OrgForm; // A,B,V,E,F,...
                            //                                                                                   //schueler.AktJahrgang = getAktJahrgang(theRow["x"] == null ? "" : theRow["x"].ToString());
                            schueler.Einschulungsjahr = theRow["dat_eintritt_gs"].ToString().Length < 3 ? new DateTime().Year : DateTime.ParseExact(theRow["dat_eintritt_gs"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).Year;
                            schueler.EntlassdatumVoraussichtlich = theRow["dat_ausbild_ende"].ToString().Length < 3 ? new DateTime() : DateTime.ParseExact(theRow["dat_ausbild_ende"].ToString(), "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            //schueler.Foederschwerpunkt = "";
                            //schueler.Schwerstbehinderung = theRow["x"] == null ? "" : theRow["x"].ToString() == "" ? "N" : "J";
                            ////schueler.AllgemeineHerkunft = GetAllgemeineHerkunft(theRow["x"] == null ? "" : theRow["x"].ToString());
                            //schueler.Reformpdg = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.Jva = "0";
                            //schueler.Plz = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.Wohnort = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.Straße = theRow["x"] == null ? "" : theRow["x"].ToString();
                            //schueler.Versetzung = theRow["x"] == null ? "" : theRow["x"].ToString();
                            ////schueler.Wiederholung = GetWiederholung(theRow["x"] == null ? "" : theRow["x"].ToString());
                            //schueler.Abschluss = theRow["x"] == null ? "" : theRow["x"].ToString(); // 0A, 4H, ...
                            //                                                                        //schueler.AbschlussDatum = ini.GetDatum(CSValues[115].Replace("\"", ""));
                            //schueler.AbschlussBerufsbezogen = theRow["x"] == null ? "" : theRow["x"].ToString(); // 0, 4, ...
                            //schueler.Schwerpunkt = theRow["x"] == null ? "" : theRow["x"].ToString(); // WV, ...
                            //schueler.AbschlussZiffer = theRow["x"] == null ? "" : theRow["x"].ToString(); // A, H, ...
                            //                                                                              //schueler.AbschlussZiffer = GetAbschlussLernabschnittsdaten(theRow["x"] == null ? "" : theRow["x"].ToString());
                            //schueler.GeschlechtSimTxt = (theRow["x"] == null ? "" : theRow["x"].ToString()).ToLower() == "m" ? "3" : "4"; // 3 = männlich
                            //                                                                                                              //schueler.Staatsang = GetStaatsangehörigkeit(theRow["x"] == null ? "" : theRow["x"].ToString());
                            schueler.Religion = theRow["s_bekennt"] == null ? "" : theRow["s_bekennt"].ToString(); // KR, ER, ...
                            //                                                                       //schueler.ReligionSchild = GetReligionSchild(schueler.Religion);
                            //schueler.ReliAnmeldung = CSValues[226].Replace("\"", "") == "" ? DateTime.ParseExact(CSValues[30].Replace("\"", ""), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.ParseExact(CSValues[226].Replace("\"", ""), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture); // Wenn kein Anmeldedatum gesetzt ist, wird das Aufnahmedatum zum Reli-Anmeldedatum
                            //schueler.ReliAbmeldung = theRow["x"] == null ? "" : theRow["x"].ToString() != "" ? DateTime.ParseExact(theRow["x"] == null ? "" : theRow["x"].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) : new DateTime();
                            //schueler.Aufnahmedatum = DateTime.ParseExact(CSValues[30].Replace("\"", ""), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            //schueler.Labk = schueler.Klasse.Klassenlehrer;
                            //schueler.AusbildOrt = schueler.Gliederung.StartsWith("A0") ? CSValues[738].Replace("\"", "") : ""; // PLZ
                            //schueler.BetriebsOrt = schueler.Gliederung.StartsWith("A0") ? CSValues[739].Replace("\"", "") : ""; // Borken,....
                            //schueler.Jahreinschulung = CSValues[26].Replace("\"", "");
                            //schueler.Schulpflichterf = CSValues[111].Replace("\"", "");
                            //schueler.JahrZuzug = (ini.GetDatum(CSValues[31].Replace("\"", ""))).Year == 1 ? "" : (ini.GetDatum(CSValues[31].Replace("\"", ""))).Year.ToString();
                            //schueler.Geburtsland = GetGeburtsland(CSValues[58].Replace("\"", ""));
                            //schueler.Geburtsname = CSValues[3].Replace("\"", "");

                            //// LS steht für letzte fremde Schule. 
                            //// Nur wenn der Schüler erstmalig in diesem SJ zu uns gewechselt ist, muss LS ausgefüllt werden.
                            //// Wenn der Schüler in der alten atlantis - schueler bereits existiert, werden LS - Eigenschaften ignoriert.


                            //schueler.Lsschulform = CSValues[125].Replace("\"", ""); // R,H,GY,GE,C03,A01,...
                            //schueler.Lsschulnummer = CSValues[128].Replace("\"", ""); // Die Nummer der Schule im Jahr zuvor.  177659, ...
                            //schueler.LsGliederung = CSValues[151].Replace("\"", ""); // 3-stellig
                            //schueler.LsFachklasse = GetLsFachklasse((CSValues[150].Replace("\"", "")), (CSValues[151].Replace("\"", "")));
                            //schueler.Klassenart = "";
                            //schueler.Reformpdg = "";
                            //schueler.Zeugnisse = new Zeugnisse();

                            //// LSSchulentlassung in alter sim.txt immer 31.7. !?
                            //// schueler.LsSchulentlassung = new DateTime(schueler.Aufnahmedatum.Year, 7, 31)
                            //schueler.LsSchulentlassung = ini.GetDatum(CSValues[153].Replace("\"", ""));

                            //schueler.Lsjahrgang = GetLsJahrgang(CSValues[116].Replace("\"", ""), CSValues[159].Replace("\"", "")); // Der Jahrgang steckt in C031 in der letzten Ziffer. Die Jahrgänge sind 1,2,3,4

                            //schueler.LsQual = GetLsQual(CSValues[145].Replace("\"", "")); // Höchster allgemeinbildender Abschluss
                            //schueler.LsHoechsterAllgemeinbildenderAbschluss = GetLsQual(CSValues[145].Replace("\"", "")); // 7H

                            //schueler.LsVersetz = GetLsVersetzung(CSValues[104].Replace("\"", ""));                                
                            this.Add(schueler);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }

            //            using (OdbcConnection connection = new OdbcConnection(connetionStringAtlantis))
            //            {
            //                DataSet dataSet = new DataSet();
            //                OdbcDataAdapter schuelerAdapter = new OdbcDataAdapter(@"SELECT DBA.le_fa.le_id,
            //DBA.fach.kurztext,
            //DBA.fach.kuerzel
            //FROM DBA.fach JOIN DBA.le_fa ON DBA.fach.fa_id = DBA.le_fa.fa_id
            //ORDER BY DBA.le_fa.le_id ASC", connection);

            //                try
            //                {
            //                    connection.Open();
            //                    schuelerAdapter.Fill(dataSet, "DBA.le_fa");

            //                    foreach (DataRow theRow in dataSet.Tables["DBA.le_fa"].Rows)
            //                    {
            //                        var idAtlantis = theRow["le_id"] == null ? -99 : (Int32)theRow["le_id"];

            //                        if (idAtlantis > 0)
            //                        {
            //                            var schueler = (from s in this where s.IdAtlantis == idAtlantis select s).FirstOrDefault();
            //                            if (schueler != null)
            //                            {
            //                                if (theRow["kuerzel"] != null)
            //                                {                                    
            //                                    schueler.Lehrbefähigungs.Add(theRow["kuerzel"].ToString());
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine(ex.Message);
            //                }
            //                finally
            //                {
            //                    connection.Close();
            //                }
            //            }

            //            foreach (var l in this)
            //            {
            //                s.InitialesPasswort = s.InitialesPasswortSetzen(s.EMail);
            //                s.Alias = s.EMais.Replace("@berufskolleg-borken.de", "");

            //                try
            //                {
            //                    string v = s.EMais.Substring(0, 1);
            //                    var w = s.EMais.IndexOfAny(new char[] { '.' });
            //                    int x = s.EMais.Length;
            //                    s.MailWebweaver = v + (s.EMais.Substring(w, x - w)).Replace("berufskolleg-borken.de", "bkb.krbor.de");
            //                    s.BildPfad = (from p in bildDateien where p.Contains("LE_" + s.Nachname) where p.Contains(s.Vorname) where p.Contains(s.IdAtlantis.ToString()) select p).FirstOrDefault();
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine(ex.ToString());
            //                }
            //            }
            Console.WriteLine(this.Count + " Schueler eingelsen.");
        }

        public string SafeGetString(OleDbDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
