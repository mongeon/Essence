using Essence.Shared;
using System.Text.Json;
using System.Net.Http.Json;

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

    public async Task AddEntry(Entry entry)
    {
        var response = await _client.PostAsJsonAsync("api/entries", entry);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Supplier[]> GetSuppliers()
    {
        var response = await _client.GetAsync("api/suppliers");
        response.EnsureSuccessStatusCode();
        using var responseStream = await response.Content.ReadAsStreamAsync();
        var suppliers = await JsonSerializer.DeserializeAsync<Supplier[]>(responseStream, _options);
        return suppliers ?? [];
    }
}
