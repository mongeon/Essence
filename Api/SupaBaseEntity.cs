using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Essence.Api;
public abstract class SupaBaseEntity : BaseModel
{

    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
