using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Rol> SaveAsync(Rol rol);
    Task<Rol> UpdateAsync(Rol rol);
    Task<List<Rol>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Rol> GetById(int id);
}