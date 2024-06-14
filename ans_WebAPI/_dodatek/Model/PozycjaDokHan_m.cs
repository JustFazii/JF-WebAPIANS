using Soneta.Handel;

namespace ans_WebAPI._dodatek.Model
{
    public class PozycjaDokHan_m
    {
        public PozycjaDokHan_m(PozycjaDokHandlowego poz)
        {
            TowarID = poz.Towar.ID;
            NazwaTowaru = poz.Towar.Nazwa;
            WartoscBrutto = (double)poz.Suma.BruttoCy.Value;
            Ilosc = poz.Ilosc.Value;
            Jednostka = poz.Ilosc.Symbol;
        }

        public int TowarID {  get; set; }
        public string NazwaTowaru { get; set; }
        public double WartoscBrutto { get; set; }
        public double Ilosc { get; set;}
        public string Jednostka { get; set;}
    }
}
