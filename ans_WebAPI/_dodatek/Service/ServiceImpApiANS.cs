using Soneta.Business;
using Soneta.CRM;
using ans_WebAPI._dodatek.Model;
using System.Collections.Generic;
using Soneta.Core;
using System;
using Soneta.Towary;
using Soneta.Handel;
using Soneta.Handel.RelacjeDokumentow.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace ans_WebAPI._dodatek.Service
{
    public class ServiceImpApiANS : ISerwisWymianyANS
    {
        private readonly Session _session;
        public Session Session => _session;

        public ServiceImpApiANS(Session session)
        {
            _session = session;
        }

        public string APIStatus()
        {
            return "Communication via API is Working!";
        }

        public List<Kontrahent_m> GetContractors()
        {
            List<Kontrahent_m> result = new List<Kontrahent_m>();
            CRMModule crm = CRMModule.GetInstance(Session);
            View kontrahenci = crm.Kontrahenci.CreateView();

            foreach (Kontrahent k in kontrahenci)
            {
                result.Add(new Kontrahent_m(k));
            }

            return result;
        }

        public List<Kontrahent_m_API_ID> GetContractorByID(int id)
        {
            CRMModule crm = CRMModule.GetInstance(Session);
            Kontrahent kontrahent = crm.Kontrahenci[id];

            List<Kontrahent_m_API_ID> result = new List<Kontrahent_m_API_ID>();

            if (kontrahent != null)
            {
                result.Add(new Kontrahent_m_API_ID(kontrahent));
            }

            return result;
        }

        public string AddContractor(Kontrahent_m_API kontrahent)
        {
            try
            {
                using (Session session = Session.Login.CreateSession(false, true))
                {
                    using (ITransaction transaction = session.Logout(true))
                    {
                        CRMModule crm = CRMModule.GetInstance(session);
                        Kontrahent newKontrahent = new Kontrahent();
                        crm.Kontrahenci.AddRow(newKontrahent);
                        UpdateContractorProperties(newKontrahent, kontrahent);
                        transaction.Commit();
                    }

                    session.Save();
                }

                return "Contractor has been added to db!";
            }
            catch (Exception ex)
            {
                return $"There was an error when adding Contractor: {ex.Message}";
            }
        }

        public string UpdateContractor(Kontrahent_m_API kontrahent)
        {
            try
            {
                using (Session session = Session.Login.CreateSession(false, true))
                {
                    using (ITransaction transaction = session.Logout(true))
                    {
                        CRMModule crm = CRMModule.GetInstance(session);
                        Kontrahent existingKontrahent = crm.Kontrahenci.WgKodu[kontrahent.Kod];

                        if (existingKontrahent == null)
                        {
                            return "Contractor with this code doesn't exist.";
                        }

                        UpdateContractorProperties(existingKontrahent, kontrahent);
                        transaction.Commit();
                    }

                    session.Save();
                }

                return "Contractor has been updated!";
            }
            catch (Exception ex)
            {
                return $"There was an error when updating Contractor: {ex.Message}";
            }
        }

        private void UpdateContractorProperties(Kontrahent kontrEnova, Kontrahent_m_API kontrahent)
        {
            kontrEnova.Kod = kontrahent.Kod;
            kontrEnova.Nazwa = kontrahent.Nazwa;
            kontrEnova.NIP = kontrahent.NIP;
            kontrEnova.PodatnikVAT = kontrahent.PodatnikVat.ToLower() == "tak";
            kontrEnova.RodzajPodmiotu = ToolsAPI.GetRodzajPodmiotu(kontrahent.RodzajVatSprzedaz);
            kontrEnova.RodzajPodmiotuZakup = ToolsAPI.GetRodzajPodmiotu(kontrahent.RodzajVatZakupu);
            kontrEnova.StatusPodmiotu = ToolsAPI.GetStatusPodmiotu(kontrahent.Status);
            kontrEnova.VATLiczonyOd = ToolsAPI.GetVatOd(kontrahent.VatOd);

            Adres adres = kontrEnova.Adres;
            adres.Ulica = kontrahent.Ulica;
            adres.NrDomu = kontrahent.NrDomu;
            adres.KodPocztowyS = kontrahent.KodPoczt;
            adres.Poczta = kontrahent.Poczta;
            adres.Wojewodztwo = ToolsAPI.GetWojewodztwo(kontrahent.Woj);
            adres.Miejscowosc = kontrahent.Miasto;
            adres.Kraj = kontrahent.Kraj;
        }

        public List<DokumentHandlowy_m> GetZODocuments()
        {
            List<DokumentHandlowy_m> result = new List<DokumentHandlowy_m>();

            HandelModule hm = HandelModule.GetInstance(Session);
            DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu["ZO"];

            View dokumentyWszystkie = hm.DokHandlowe.CreateView();

            View dokumentyWgDefinicja = hm.DokHandlowe.WgDefinicja[definicja].CreateView();

            dokumentyWszystkie.Condition = new FieldCondition.Equal("Definicja.Symbol", "ZO");

            foreach (DokumentHandlowy d in dokumentyWgDefinicja)
            {
                result.Add(new DokumentHandlowy_m(d));
            }

            return result;
        }

        public List<DokumentHandlowy_m> GetZDDocuments()
        {
            List<DokumentHandlowy_m> result = new List<DokumentHandlowy_m>();

            HandelModule hm = HandelModule.GetInstance(Session);
            DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu["ZD"];

            View dokumentyWszystkie = hm.DokHandlowe.CreateView();

            View dokumentyWgDefinicja = hm.DokHandlowe.WgDefinicja[definicja].CreateView();

            dokumentyWszystkie.Condition = new FieldCondition.Equal("Definicja.Symbol", "ZD");

            foreach (DokumentHandlowy d in dokumentyWgDefinicja)
            {
                result.Add(new DokumentHandlowy_m(d));
            }

            return result;
        }

        public List<PozycjaDokHan_m> GetDocumentsPositions(int ID)
        {
            List<PozycjaDokHan_m> result = new List<PozycjaDokHan_m>();

            HandelModule hm = HandelModule.GetInstance(Session);

            DokumentHandlowy dok = hm.DokHandlowe[ID];

            foreach (PozycjaDokHandlowego p in dok.Pozycje)
            {
                result.Add(new PozycjaDokHan_m(p));
            }

            return result;
        }

        public string CreateZOFVRelation(int idDokumentuZO)
        {
            try
            {
                using (Session sesja = Session.Login.CreateSession(false, true))
                {
                    HandelModule hm = sesja.GetHandel();

                    DokumentHandlowy dokument = hm.DokHandlowe[idDokumentuZO];

                    if (dokument == null)
                    {
                        return $"Document with ID {idDokumentuZO} doesn't exist.";
                    }

                    if (dokument.Stan != StanDokumentuHandlowego.Zatwierdzony)
                    {
                        using (ITransaction trans = sesja.Logout(true))
                        {
                            dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;
                            trans.Commit();
                        }
                    }

                    if (IsDocumentAlreadyExist(dokument, "FV"))
                    {
                        return "Relation ZO > FV for this document already exists.";
                    }

                    DokumentHandlowy[] dokNowy = null;

                    using (ITransaction trans = sesja.Logout(true))
                    {
                        var relacjeApi = (IRelacjeService)dokument.Session.GetRequiredService(typeof(IRelacjeService));
                        dokNowy = relacjeApi.NowyPodrzednyIndywidualny(new[] { dokument }, "FV");
                        trans.Commit();
                    }

                    if (dokNowy == null || dokNowy.Length == 0)
                    {
                        return "Failed to create a new subdocument.";
                    }

                    DokumentHandlowy faktura = dokNowy[0];

                    sesja.Save();

                    return "Successfully added document.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string CreateZDZKRelation(int idDokumentuZD)
        {
            try
            {
                using (Session sesja = Session.Login.CreateSession(false, true))
                {
                    HandelModule hm = sesja.GetHandel();

                    DokumentHandlowy dokument = hm.DokHandlowe[idDokumentuZD];

                    if (dokument == null)
                    {
                        return $"Document with ID {idDokumentuZD} doesn't exist.";
                    }

                    if (dokument.Stan != StanDokumentuHandlowego.Zatwierdzony)
                    {
                        using (ITransaction trans = sesja.Logout(true))
                        {
                            dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;
                            trans.Commit();
                        }
                    }

                    if (IsDocumentAlreadyExist(dokument, "ZK"))
                    {
                        return "Relation ZD > ZK for this document already exists.";
                    }

                    DokumentHandlowy[] dokNowy = null;

                    using (ITransaction trans = sesja.Logout(true))
                    {
                        var relacjeApi = (IRelacjeService)dokument.Session.GetRequiredService(typeof(IRelacjeService));
                        dokNowy = relacjeApi.NowyPodrzednyIndywidualny(new[] { dokument }, "ZK");
                        trans.Commit();
                    }

                    if (dokNowy == null || dokNowy.Length == 0)
                    {
                        return "Failed to create a new subdocument.";
                    }

                    DokumentHandlowy faktura = dokNowy[0];

                    sesja.Save();

                    return "Successfully added document.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GenerateInvoice(int idDokumentuZO, string typDokumentu)
        {
            try
            {
                using (Session sesja = Session.Login.CreateSession(false, true))
                {
                    HandelModule hm = sesja.GetHandel();

                    DokumentHandlowy dokument = hm.DokHandlowe[idDokumentuZO];

                    if (dokument == null)
                    {
                        return $"Document with ID {idDokumentuZO} doesn't exist.";
                    }

                    if (dokument.Stan != StanDokumentuHandlowego.Zatwierdzony)
                    {
                        using (ITransaction trans = sesja.Logout(true))
                        {
                            dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;
                            trans.Commit();
                        }
                    }

                    if (string.IsNullOrEmpty(typDokumentu))
                    {
                        return "Document type cannot be empty.";
                    }

                    if (IsDocumentAlreadyExist(dokument, typDokumentu))
                    {
                        return "Invoice for this document already exists.";
                    }

                    IRelacjeService relacjeApi = (IRelacjeService)dokument.Session.GetRequiredService(typeof(IRelacjeService));
                    if (relacjeApi == null)
                    {
                        return "Relation service is currently unavailable.";
                    }

                    DokumentHandlowy[] dokNowy = null;

                    using (ITransaction trans = sesja.Logout(true))
                    {
                        dokNowy = relacjeApi.NowyPodrzednyIndywidualny(new[] { dokument }, typDokumentu);
                        if (dokNowy == null || dokNowy.Length == 0)
                        {
                            return "Failed to create a new subdocument.";
                        }
                        trans.Commit();
                    }

                    DokumentHandlowy faktura = dokNowy[0];

                    sesja.Save();

                    return $"Successfully generated Invoice with ID: {faktura.ID}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string AddHandelDocument([FromBody] DokHandlowyRequest request)
        {
            try
            {
                if (request == null || request.Dokument == null)
                {
                    return "Invalid data";
                }

                if (string.IsNullOrEmpty(request.Dokument.Definicja))
                {
                    return "It is required to enter a value for the 'Commercial document definition'";
                }

                using (Session sesja = Session.Login.CreateSession(false, true))
                {
                    HandelModule hm = sesja.GetHandel();
                    CRMModule crm = sesja.GetCRM();

                    DokumentHandlowy dokument = null;

                    using (ITransaction trans = sesja.Logout(true))
                    {
                        dokument = new DokumentHandlowy
                        {
                            Definicja = hm.DefDokHandlowych.WgSymbolu[request.Dokument.Definicja],
                            Magazyn = hm.Magazyny.Magazyny.Firma
                        };
                        hm.DokHandlowe.AddRow(dokument);

                        dokument.Kontrahent = crm.Kontrahenci.WgKodu[request.Dokument.Kontrahent];

                        trans.Commit();
                    }

                    foreach (var poz in request.Pozycje)
                    {
                        using (ITransaction trans = sesja.Logout(true))
                        {
                            PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                            hm.PozycjeDokHan.AddRow(pozycja);

                            pozycja.Towar = hm.Towary.Towary.WgKodu[poz.KodTowaru];
                            pozycja.Ilosc = new Soneta.Towary.Quantity(poz.Ilosc, pozycja.Towar.Jednostka.Kod);
                            pozycja.Cena = new Soneta.Types.DoubleCy(poz.Cena, "PLN");

                            trans.CommitUI();
                        }
                    }

                    sesja.Save();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Successfully added document!";
        }

        public List<Towar_m> GetGoods()
        {
            List<Towar_m> result = new List<Towar_m>();

            TowaryModule tm = TowaryModule.GetInstance(Session);
            View towary = tm.Towary.CreateView();

            foreach (Towar t in towary)
            {
                result.Add(new Towar_m(t));
            }

            return result;
        }

        public List<Towar_m_API> GetGoodByID(int id)
        {
            TowaryModule tm = TowaryModule.GetInstance(Session);
            Towar towar = tm.Towary[id];

            List<Towar_m_API> result = new List<Towar_m_API>();

            if (towar != null)
            {
                result.Add(new Towar_m_API(towar));
            }

            return result;
        }

        public List<DokumentHandlowy_m> GetInvoices()
        {
            List<DokumentHandlowy_m> result = new List<DokumentHandlowy_m>();

            using (Session session = Session.Login.CreateSession(false, true))
            {
                HandelModule hm = HandelModule.GetInstance(session);
                View fakturyView = hm.DokHandlowe.CreateView();

                fakturyView.Condition = new FieldCondition.In("Definicja.Symbol", new string[] { "FV", "ZK" });

                foreach (DokumentHandlowy faktura in fakturyView)
                {
                    result.Add(new DokumentHandlowy_m(faktura));
                }
            }

            return result;
        }

        public string PassEcho(string value)
        {
            return value;
        }

        public bool IsDocumentAlreadyExist(DokumentHandlowy zrodlowy, string symbolDocelowego)
        {
            foreach (DokumentHandlowy dokRel in zrodlowy.Podrzędne)
            {
                if (dokRel.Definicja.Symbol == symbolDocelowego) return true;
            }
            return false;
        }

    }
}
