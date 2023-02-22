using DataAccess.Models;

namespace DataAccess.Data.Repositories.Interfaces;

public interface IEquipmentRepository
{
    Task<Equipment?> GetEquipment(string code);
}