namespace Essence.Api.Entries;
public class EntryRepository(Supabase.Client supabaseClient) : IEntryRepository
{
    public async Task<IEnumerable<Entry>> GetAll()
    {
        var response = await supabaseClient
            .From<Entry>()
            .Get();
        return response.Models;
    }
}
