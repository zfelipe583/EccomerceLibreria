using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IDetalleLibrosRepository
{
    Task<Detalle_Libros> SaveAsync(Detalle_Libros detalle);
    Task<Detalle_Libros> UpdateAsync(Detalle_Libros detalle);
    Task<List<Detalle_Libros>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Detalle_Libros> GetById(int id);
}