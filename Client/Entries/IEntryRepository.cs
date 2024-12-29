using Essence.Shared;

namespace Essence.Client.Entries;

public interface IEntryRepository
{
    Task<Entry[]> GetEntries();
}
