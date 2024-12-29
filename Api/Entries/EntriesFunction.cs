using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Essence.Api.Entries;

public class EntriesFunction(ILogger<EntriesFunction> logger, IEntryRepository entryRepository)
{
    [Function("GetAllEntriesFunction")]
    public async Task<HttpResponseData> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "entries")] HttpRequestData req)
    {
        logger.LogInformation("Gettting all entries");
        var entries = await entryRepository.GetAll();

        var response = req.CreateResponse(HttpStatusCode.OK);

        var entriesDto = entries.Select(entry => new Shared.Entry
        {
            Id = entry.Id,
            Date = entry.Date,
            CreatedAt = entry.CreatedAt,
            Kilometers = entry.Kilometers,
            Liters = entry.Liters,
            TotalPrice = entry.TotalPrice,
            Notes = entry.Notes
        });

        entriesDto = CalculateConsumption(entriesDto);

        await response.WriteAsJsonAsync(entriesDto);

        return response;
    }

    private static IEnumerable<Shared.Entry> CalculateConsumption(IEnumerable<Shared.Entry> entriesDto)
    {
        // Sort by kilometers, calculate consumption based on the previous entry
        if (!entriesDto.Any())
        {
            return [];
        }
        if (entriesDto.Count() == 1)
        {
            entriesDto.First().Consumption = 0;
            return entriesDto;
        }
        else
        {
            var orderedEntries = entriesDto.OrderBy(entry => entry.Kilometers).ToArray();
            var previousEntry = orderedEntries.First();
            previousEntry.Consumption = 0;

            for (int i = 0; i < orderedEntries.Length; i++)
            {
                var entry = orderedEntries[i];
                if (i == 0)
                {
                    continue;
                }
                entry.Consumption = entry.Liters / (entry.Kilometers - previousEntry.Kilometers) * 100;
                previousEntry = entry;
            }

            return orderedEntries;
        }
    }
}
