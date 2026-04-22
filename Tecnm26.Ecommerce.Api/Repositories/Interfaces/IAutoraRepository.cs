using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IAutoraRepository
{
    Task<Autora> SaveAsync(Autora autora);
    Task<Autora> UpdateAsync(Autora autora);
    Task<List<Autora>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Autora> GetById(int id);
}