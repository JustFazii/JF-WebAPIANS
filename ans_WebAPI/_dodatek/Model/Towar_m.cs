using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Business;

public class Towar_m
{
    public int ID { get; set; }
    public string Kod { get; set; }
    public string Nazwa { get; set; }
    public double Cena { get; set; }
    public string Waluta { get; set; }
    public double IloscDostepna { get; set; }

    public Towar_m(Towar towarEnova)
    {
        this.ID = towarEnova.ID;
        this.Kod = towarEnova.Kod;
        this.Nazwa = towarEnova.Nazwa;
        this.Cena = towarEnova.Ceny["Podstawowa"].Brutto.Value;
        this.Waluta = towarEnova.Ceny["Podstawowa"].Brutto.Symbol;
        this.IloscDostepna = SprawdzanieStanowMagazynowych(towarEnova).Value;
    }


    private Quantity SprawdzanieStanowMagazynowych(Towar towarEnova)
    {
        Context cx = Context.Empty.Clone(towarEnova.Session);
        cx.Set(towarEnova);
        Quantity StanMagazynu = new Quantity();

        #region Workery Stany magazynowe

        StanMagazynuWorker worker = (StanMagazynuWorker)cx.CreateObject(null, typeof(StanMagazynuWorker), null);
        cx.Set(worker);
        worker.Towar = towarEnova;
        StanMagazynu += worker.StanMagazynu;

        #endregion

        return StanMagazynu;
    }

}