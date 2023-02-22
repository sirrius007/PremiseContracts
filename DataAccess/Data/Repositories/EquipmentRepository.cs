using DataAccess.Data.Repositories.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly PremiseContractsContext _db;

    public EquipmentRepository(PremiseContractsContext db) => _db = db;

    public async Task<Equipment?> GetEquipment(string code)
    {
        return await _db.Equipment.FindAsync(code);
    }

}