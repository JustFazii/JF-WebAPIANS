using ans_WebAPI._dodatek.Model;
using ans_WebAPI._dodatek.Service;
using Soneta.Business;
using Soneta.Types.DynamicApi;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


[assembly: Service(typeof(ISerwisWymianyANS),
    typeof(ServiceImpApiANS),
    ServiceScope.Session,
    Published = true,
    MinIntervalRequest = 0)]

[assembly: DynamicApiController(typeof(ISerwisWymianyANS),
    typeof(ServiceImpApiANS),
    Summary = "API ANS")]

namespace ans_WebAPI._dodatek.Service
{
    public interface ISerwisWymianyANS
    {
        [DynamicApiMethod(HttpMethods.POST,
            nameof(APIStatus),
            Summary = "Return API status.", ImplementationNotes = "")]
        string APIStatus();

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetContractors),
            Summary = "Show all contractors via API.", ImplementationNotes = "")]
        List<Kontrahent_m> GetContractors();

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetContractorByID),
            Summary = "Show contractor by ID via API.", ImplementationNotes = "")]
        List<Kontrahent_m_API_ID> GetContractorByID(int id);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(AddContractor),
            Summary = "Add new contractor via API", ImplementationNotes = "")]
        string AddContractor(Kontrahent_m_API kontrahent);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(UpdateContractor),
            Summary = "Update Contractor via API", ImplementationNotes = "")]
        string UpdateContractor(Kontrahent_m_API kontrahent);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetZODocuments),
            Summary = "Get all ZO documents via API", ImplementationNotes = "")]
        List<DokumentHandlowy_m> GetZODocuments();

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetZDDocuments),
            Summary = "Get all ZD documents via API", ImplementationNotes = "")]
        List<DokumentHandlowy_m> GetZDDocuments();

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetDocumentsPositions),
            Summary = "Get Documents Positions via API", ImplementationNotes = "")]
        List<PozycjaDokHan_m> GetDocumentsPositions(int value);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(CreateZOFVRelation),
            Summary = "Create relation ZO > ZV via API", ImplementationNotes = "")]
        string CreateZOFVRelation(int idDokumentuZO);


        [DynamicApiMethod(HttpMethods.POST,
            nameof(CreateZDZKRelation),
            Summary = "Create relation ZD > ZK via API", ImplementationNotes = "")]
        string CreateZDZKRelation(int idDokumentuZD);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GenerateInvoice),
            Summary = "Generate invoice to order via API", ImplementationNotes = "")]
        string GenerateInvoice(int idDokumentuZO, string typDokumentu);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(AddHandelDocument),
            Summary = "Add Handel Document via API", ImplementationNotes = "")]
        string AddHandelDocument([FromBody] DokHandlowyRequest request);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetGoods),
            Summary = "Get all Commercial Goods via API", ImplementationNotes = "")]
        List<Towar_m> GetGoods();

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetGoodByID),
            Summary = "Get Good via API using ID", ImplementationNotes = "")]
        List<Towar_m_API> GetGoodByID(int id);

        [DynamicApiMethod(HttpMethods.POST,
            nameof(GetInvoices),
            Summary = "Get all Invoices via API", ImplementationNotes = "")]
        List<DokumentHandlowy_m> GetInvoices();

        [DynamicApiMethod(HttpMethods.POST,
            nameof(PassEcho),
            Summary = "Pass Echo and return it via API", ImplementationNotes = "")]
        string PassEcho(string value);
    }
}
