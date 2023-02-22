using DataAccess.Models;

namespace DataAccess.Data.Repositories.Interfaces;

public interface IPremiseRepository
{
    Task<Premise?> GetPremise(string code);
}