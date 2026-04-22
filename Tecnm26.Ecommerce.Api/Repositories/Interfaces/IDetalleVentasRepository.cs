using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IDetalleVentasRepository
{
    Task<Detalle_Ventas> SaveAsync(Detalle_Ventas detalle);
    Task<Detalle_Ventas> UpdateAsync(Detalle_Ventas detalle);
    Task<List<Detalle_Ventas>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Detalle_Ventas> GetById(int id);
}
