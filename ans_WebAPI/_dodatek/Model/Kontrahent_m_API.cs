using Soneta.CRM;

namespace ans_WebAPI._dodatek.Model
{
    public class Kontrahent_m_API
    {
        public int ID { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string NIP { get; set; }
        public string Adres { get; set; }
        public string PodatnikVat { get; set; }
        public string RodzajVatSprzedaz { get; set; }
        public string RodzajVatZakupu { get; set; }
        public string Status { get; set; }
        public string VatOd { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string KodPoczt { get; set; }
        public string Poczta { get; set; }
        public string Woj { get; set; }
        public string Miasto { get; set; }
        public string Kraj { get; set; }

        public Kontrahent_m_API() { }

        public Kontrahent_m_API(Kontrahent kontrEnova)
        {
            this.ID = kontrEnova.ID;
            this.Kod = kontrEnova.Kod;
            this.Nazwa = kontrEnova.Nazwa;
            this.NIP = kontrEnova.NIP;
            this.Adres = kontrEnova.Adres.ToString();
            this.PodatnikVat = kontrEnova.PodatnikVAT ? "tak" : "nie";
            this.RodzajVatSprzedaz = kontrEnova.RodzajPodmiotu.ToString();
            this.RodzajVatZakupu = kontrEnova.RodzajPodmiotuZakup.ToString();
            this.Status = kontrEnova.StatusPodmiotu.ToString();
            this.VatOd = kontrEnova.VATLiczonyOd.ToString();
            this.Ulica = kontrEnova.Adres.Ulica;
            this.NrDomu = kontrEnova.Adres.NrDomu;
            this.KodPoczt = kontrEnova.Adres.KodPocztowyS;
            this.Poczta = kontrEnova.Adres.Poczta;
            this.Woj = kontrEnova.Adres.Wojewodztwo.ToString();
            this.Miasto = kontrEnova.Adres.Miejscowosc;
            this.Kraj = kontrEnova.Adres.Kraj;
        }
    }
}
