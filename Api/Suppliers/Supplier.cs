using Supabase.Postgrest.Attributes;

namespace Essence.Api.Suppliers;


[Table("suppliers")]
public class Supplier : SupaBaseEntity
{
    [Column("name")]
    public string Name { get; set; } = "";
}
