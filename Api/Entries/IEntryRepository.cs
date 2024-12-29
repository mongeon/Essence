namespace Essence.Api.Entries;

public interface IEntryRepository
{
    Task<IEnumerable<Entry>> GetAll();
}