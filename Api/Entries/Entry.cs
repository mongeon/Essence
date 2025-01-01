using Essence.Api.Suppliers;
using Supabase.Postgrest.Attributes;

namespace Essence.Api.Entries;

[Table("entries")]
public class Entry : SupaBaseEntity
{
    [Column("date")]
    public DateTime Date { get; set; }

    [Column("kilometers")]
    public int Kilometers { get; set; }

    [Column("total_price")]
    public decimal TotalPrice { get; set; }

    [Column("liters")]
    public decimal Liters { get; set; }

    [Column("notes")]
    public string Notes { get; set; } = "";

    [Reference(typeof(Supplier), includeInQuery: true)]
    public Supplier Supplier { get; set; } = null!;
}
