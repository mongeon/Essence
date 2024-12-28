using Essence.Shared;
using System.Text.Json;

namespace Essence.Client.Entries;

public class EntryRepository(HttpClient client) : BaseRepository(client), IEntryRepository
{
    public async Task<Entry[]> GetEntries()
    {
        var response = await _client.GetAsync("api/entries");
        response.EnsureSuccessStatusCode();
        using var responseStream = await response.Content.ReadAsStreamAsync();
        var entries = await JsonSerializer.DeserializeAsync<Entry[]>(responseStream, _options);
        return entries ?? [];
    }
}
