using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IVentasRepository
{
    Task<Ventas> SaveAsync(Ventas venta);
    Task<Ventas> UpdateAsync(Ventas venta);
    Task<List<Ventas>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Ventas> GetById(int id);
}