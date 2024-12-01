

namespace App.Persistence;

using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class SuffixConfiguration : IEntityTypeConfiguration<Suffix>
{

    public void Configure(EntityTypeBuilder<Suffix> builder)
    {
        builder.ToTable("Suffixes");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x=>x.Name)
            .HasMaxLength(5)
            .IsRequired();

    }
}