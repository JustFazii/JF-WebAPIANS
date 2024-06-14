using Soneta.CRM;

namespace ans_WebAPI._dodatek.Model
{
    public class Kontrahent_m
    {
        public int ID { get; set; }
        public string Kod {  get; set; }
        public string Nazwa { get; set; }
        public string NIP { get; set; }
        public string Adres { get; set; }
        public Kontrahent_m() { }

        public Kontrahent_m(Kontrahent kontrEnova)
        {
            this.ID = kontrEnova.ID;
            this.Kod = kontrEnova.Kod;
            this.Nazwa = kontrEnova.Nazwa;
            this.NIP = kontrEnova.NIP;
            this.Adres = kontrEnova.Adres.ToString();
        }
    }
}
