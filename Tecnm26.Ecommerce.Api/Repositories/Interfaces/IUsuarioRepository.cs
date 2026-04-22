using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> SaveAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task<List<Usuario>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Usuario> GetById(int id);
}