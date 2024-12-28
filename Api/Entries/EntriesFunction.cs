using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Api.Entries
{
    public class EntriesFunction(ILogger<EntriesFunction> logger, IEntryRepository entryRepository)
    {
        [Function("GetAllEntriesFunction")]
        public async Task<HttpResponseData> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "entries")] HttpRequestData req)
        {
            logger.LogInformation("Gettting all entries");
            var entries = await entryRepository.GetAll();

            var response = req.CreateResponse(HttpStatusCode.OK);

            var entriesDto = entries.Select(entry => new Essence.Shared.Entry
            {
                Id = entry.Id,
                Date = entry.Date,
                CreatedAt = entry.CreatedAt
            });

            await response.WriteAsJsonAsync(entriesDto);

            return response;
        }
    }
}
