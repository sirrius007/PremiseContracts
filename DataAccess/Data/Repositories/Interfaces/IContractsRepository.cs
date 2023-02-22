
using DataAccess.Models;

namespace DataAccess.Data.Repositories.Interfaces;

public interface IContractsRepository
{
    Task<IEnumerable<Contract>> GetAllAsync();
    Task CreateAsync(Contract contract);
}