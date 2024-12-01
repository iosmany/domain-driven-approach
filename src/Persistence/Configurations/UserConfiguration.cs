

namespace App.Persistence;

using System.Security.Cryptography.X509Certificates;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class Userconfiguration : IEntityTypeConfiguration<User>
{

    Email CreateEmail(string value)
    {
        return Email.Create(value)
            .Match<Email>(
                email => email,
                error => throw new Exception($"{error.Field}{error.Message}")
            );
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseHiLo(HiLoConfig.HiLoSequence);
        
        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.FirstName).HasMaxLength(80).IsRequired();
            name.Property(x => x.LastName).HasMaxLength(100);

            name.Property<long?>("SuffixId");
            name.HasOne(x => x.Suffix)
                .WithMany()
                .HasForeignKey("SuffixId")
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        builder.Property(x => x.Email)
            .HasConversion(x=> x.Value, x=> CreateEmail(x));

        builder.Property(x => x.Password).HasMaxLength(40).IsRequired();

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