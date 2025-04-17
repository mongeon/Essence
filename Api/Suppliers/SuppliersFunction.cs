using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Essence.Api.Suppliers;

public class SuppliersFunction
{
    private readonly Supabase.Client _supabaseClient;
    public SuppliersFunction(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    [Function("GetAllSuppliersFunction")]
    public async Task<HttpResponseData> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "suppliers")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        var suppliers = await _supabaseClient.From<Supplier>().Get();
        await response.WriteAsJsonAsync(suppliers.Models.Select(s => new Shared.Supplier
        {
            Id = s.Id,
            Name = s.Name,
            CreatedAt = s.CreatedAt
        }));
        return response;
    }
}
