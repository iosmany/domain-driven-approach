

namespace App.Persistence;

using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public static class HiLoConfig
{
    public const string HiLoSequence = "SeqHiLo";

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<long>(HiLoSequence)
            .IncrementsBy(100)
            .StartsAt(1)
            .HasMin(1)
            .IsCyclic();
    }    
}
