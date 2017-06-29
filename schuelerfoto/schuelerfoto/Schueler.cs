using System;

namespace schuelerausweis
{
    public class Schueler
    {
        public string Klasse { get; internal set; }
        public string Vorname { get; internal set; }
        public string Nachname { get; internal set; }
        public DateTime Geburtsdatum { get; internal set; }
    }
}