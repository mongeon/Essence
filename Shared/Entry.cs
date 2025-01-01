using System;

namespace Essence.Shared;

public record Entry : BaseEntity
{
    public DateTime Date { get; set; }
    public int Kilometers { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Liters { get; set; }

    public decimal LiterPrice => TotalPrice / Liters;
    public decimal Consumption { get; set; }
    public string Notes { get; set; } = "";

    public Supplier Supplier { get; set; } = null!;
}
