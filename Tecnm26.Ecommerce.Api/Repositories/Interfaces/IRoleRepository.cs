using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role> SaveAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    Task<List<Role>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Role> GetById(int id);
}