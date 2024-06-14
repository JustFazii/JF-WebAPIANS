using System.Collections.Generic;

namespace ans_WebAPI._dodatek.Model
{
    public class DokHandlowy_m
    {
        public DokumentHandlowy_m Dokument { get; set; }
        public List<PozycjaDokHan_m> Pozycje { get; set; }
    }
}
