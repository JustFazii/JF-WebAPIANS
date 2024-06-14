using System.Collections.Generic;

namespace ans_WebAPI._dodatek.Model
{
    public class DokHandlowyRequest
    {
        public DokumentRequest Dokument { get; set; }
        public List<PozycjaRequest> Pozycje { get; set; }
    }

    public class DokumentRequest
    {
        public string Definicja { get; set; }
        public string Data { get; set; }
        public string Kontrahent { get; set; }
        public string NumerDokumentu { get; set; }
    }

    public class PozycjaRequest
    {
        public string KodTowaru { get; set; }
        public double Ilosc { get; set; }
        public decimal Cena { get; set; }
    }
}
