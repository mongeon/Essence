using System;

namespace Essence.Shared;

public abstract record BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
}