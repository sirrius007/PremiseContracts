using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Configuration;

public class PremiseConfig : IEntityTypeConfiguration<Premise>
{
    public void Configure(EntityTypeBuilder<Premise> builder)
    {
        builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder
            .HasKey(p => p.Code);

        builder
            .Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.Area)
            .IsRequired()
            .HasPrecision(10, 2);
    }
}
