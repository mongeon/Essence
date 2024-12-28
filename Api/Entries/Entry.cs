using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Api.Entries;

[Table("entries")]
public class Entry : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("kilometers")]
    public int Kilometers { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
