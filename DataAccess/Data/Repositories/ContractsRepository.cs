using DataAccess.Data.Repositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.Repositories;

public class ContractsRepository : IContractsRepository
{
	private readonly PremiseContractsContext _db;

	public ContractsRepository(PremiseContractsContext db) => _db = db;

    public async Task<IEnumerable<Contract>> GetAllAsync()
    {
        var contracts = await _db.Contracts
            .Include(c => c.Premise)
            .Include(c => c.Equipment)
            .Select(c => c)
            .AsNoTracking()
            .ToListAsync();
        return contracts;
    }

    public async Task CreateAsync(Contract contract)
    {
        _db.Contracts.Add(contract);
        await _db.SaveChangesAsync();
    }
}