

namespace App.Persistence;

using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class Productconfiguration : IEntityTypeConfiguration<Product>
{

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseHiLo(HiLoConfig.HiLoSequence);
        
        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p=> p.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            price.Property(p=> p.DiscountPercentage)
                .HasColumnName("DiscountPercentage")
                .HasColumnType("decimal(3,2)")
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .IsRequired();

        builder.Property<byte[]>("Version")
            .IsRowVersion();
    }
}