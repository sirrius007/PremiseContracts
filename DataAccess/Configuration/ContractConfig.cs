using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Configuration;

public class ContractConfig : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder
            .HasOne(p => p.Premise)
            .WithMany(p => p.Contracts)
            .HasForeignKey(p => p.PremiseCode);

        builder
            .HasOne(p => p.Equipment)
            .WithMany(p => p.Contracts)
            .HasForeignKey(p => p.EquipmentCode);

        builder
            .Property(p => p.PremiseCode)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(p => p.EquipmentCode)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(p => p.Quantity)
            .IsRequired()
            .HasPrecision(3);
    }
}
