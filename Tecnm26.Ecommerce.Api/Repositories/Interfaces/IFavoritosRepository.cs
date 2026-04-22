using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IFavoritosRepository
{
    Task<Favoritos> SaveAsync(Favoritos favorito);
    Task<Favoritos> UpdateAsync(Favoritos favorito);
    Task<List<Favoritos>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Favoritos> GetById(int id);
}