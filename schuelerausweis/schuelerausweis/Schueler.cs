using System;

namespace schuelerausweis
{
    public class Schueler
    {
        public int IdAtlantis { get; internal set; }
        public string Nachname { get; internal set; }
        public string Vorname { get; internal set; }
        public DateTime Geburtsdatum { get; internal set; }
        public string Jahrgang { get; internal set; }
        public string Geburtsort { get; internal set; }
        public int Einschulungsjahr { get; internal set; }
        public string Religion { get; internal set; }
        public DateTime EntlassdatumVoraussichtlich { get; internal set; }
        public string BildPfad { get; internal set; }
        public string Klasse { get; internal set; }
    }
}