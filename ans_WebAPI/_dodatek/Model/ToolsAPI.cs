using Soneta.CRM;
using Soneta.Core;

namespace ans_WebAPI._dodatek.Model
{
    internal class ToolsAPI
    {
        public static RodzajPodmiotu GetRodzajPodmiotu(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string rodzaj = value.ToLower();
                if (rodzaj == "krajowy") return RodzajPodmiotu.Krajowy;
                if (rodzaj == "unijny") return RodzajPodmiotu.Unijny;
                if (rodzaj == "eksportowy") return RodzajPodmiotu.Eksportowy;
            }
            return RodzajPodmiotu.Krajowy;
        }

        public static VatKontahentaLiczonyOd GetVatOd(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string rodzaj = value.ToLower();
                if (rodzaj == "netto") return VatKontahentaLiczonyOd.OdNetto;
                if (rodzaj == "brutto") return VatKontahentaLiczonyOd.OdBrutto;
            }
            return VatKontahentaLiczonyOd.OdNetto;
        }

        public static StatusPodmiotu GetStatusPodmiotu(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string rodzaj = value.ToLower();
                if (rodzaj == "podmiot gospodarczy") return StatusPodmiotu.PodmiotGospodarczy;
                if (rodzaj == "finalny") return StatusPodmiotu.Finalny;
            }
            return StatusPodmiotu.PodmiotGospodarczy;
        }

        public static Wojewodztwa GetWojewodztwo(string value)
        {
            if (value != null && value.Length == 0) return Wojewodztwa.nieokreślone;
            if (value == "kujawsko-pomorskie") return Wojewodztwa.kujawsko_pomorskie;
            if (value == "warminsko-mazurskie") return Wojewodztwa.warmińsko_mazurskie;
            try
            {
                return (Wojewodztwa)System.Enum.Parse(typeof(Wojewodztwa), value, true);
            }
            catch
            {
                return Wojewodztwa.nieokreślone;
            }
        }
    }
}
