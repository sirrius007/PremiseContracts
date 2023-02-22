using DataAccess.Configuration;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class PremiseContractsContext : DbContext
{
	public PremiseContractsContext(DbContextOptions options) : base(options) { }
	public DbSet<Premise> Premises { get; set; }
	public DbSet<Equipment> Equipment { get; set; }
	public DbSet<Contract> Contracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PremiseConfig());
        modelBuilder.ApplyConfiguration(new EquipmentConfig());
        modelBuilder.ApplyConfiguration(new ContractConfig());

        modelBuilder.Entity<Premise>().HasData(
            new Premise { Code = "ABCDE00001", Name = "Premise A", Area = 500.00 },
            new Premise { Code = "ABCDE00002", Name = "Premise B", Area = 400.00 },
            new Premise { Code = "ABCDE00003", Name = "Premise C", Area = 1000.00 },
            new Premise { Code = "ABCDE00004", Name = "Premise D", Area = 700.00 },
            new Premise { Code = "ABCDE00005", Name = "Premise E", Area = 800.00 },
            new Premise { Code = "ABCDE00006", Name = "Premise F", Area = 100.00 },
            new Premise { Code = "ABCDE00007", Name = "Premise G", Area = 300.00 }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Code = "EQ00000001", Name = "Equipment A", Area = 50.00 },
            new Equipment { Code = "EQ00000002", Name = "Equipment B", Area = 10.00 },
            new Equipment { Code = "EQ00000003", Name = "Equipment C", Area = 25.00 }
        );
    }
}