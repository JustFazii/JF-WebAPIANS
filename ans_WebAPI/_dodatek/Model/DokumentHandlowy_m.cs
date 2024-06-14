using Soneta.Handel;
using System;

namespace ans_WebAPI._dodatek.Model
{
    public class DokumentHandlowy_m
    {
       public DokumentHandlowy_m(DokumentHandlowy dok)
        {
            ID = dok.ID;
            DefinicjaSymbol = dok.Definicja.Symbol;
            NumerDokumentu = dok.NumerPelnyZapisany;
            KontrahentID = dok.Kontrahent.ID;
            KontrahentNazwa = dok.Kontrahent.Nazwa;
            WartoscBrutto = (double)dok.Suma.BruttoCy.Value;
            DataDok = dok.Data;
        }

        public int ID { get; set; }
        public string DefinicjaSymbol {  get; set; }
        public string NumerDokumentu {  get; set; }
        public DateTime DataDok {  get; set; }
        public int KontrahentID { get; set;}
        public string KontrahentNazwa {  get; set; }
        public double WartoscBrutto {  get; set; }
    }
}
