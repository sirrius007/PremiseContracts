using DataAccess.Data.Repositories.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data.Repositories;

public class PremiseRepository : IPremiseRepository
{
    private readonly PremiseContractsContext _db;
    public PremiseRepository(PremiseContractsContext db) => _db = db;

    public async Task<Premise?> GetPremise(string code)
    {
        return await _db.Premises.FindAsync(code);
    }
}