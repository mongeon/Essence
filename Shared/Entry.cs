using System;

namespace Essence.Shared;

public class Entry
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public DateTime CreatedAt { get; set; }
    public int Kilometers { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Liters { get; set; }

    public decimal LiterPrice => TotalPrice / Liters;
    public decimal Consumption { get; set; }
    public string Notes { get; set; } = "";
}
