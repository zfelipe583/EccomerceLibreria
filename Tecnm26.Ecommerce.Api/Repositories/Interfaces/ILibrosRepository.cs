using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface ILibrosRepository
{
    Task<Libros> SaveAsync(Libros libro);
    Task<Libros> UpdateAsync(Libros libro);
    Task<List<Libros>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Libros> GetById(int id);
}