using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class DetalleVentasRepository : IDetalleVentasRepository
{
    private readonly IDbContext _dbContext;

    public DetalleVentasRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Detalle_Ventas> SaveAsync(Detalle_Ventas detalle)
    {
        detalle.Id = await _dbContext.Connection.InsertAsync(detalle);
        return detalle;
    }

    public async Task<Detalle_Ventas> UpdateAsync(Detalle_Ventas detalle)
    {
        await _dbContext.Connection.UpdateAsync(detalle);
        return detalle;
    }

    public async Task<List<Detalle_Ventas>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Detalle_Ventas WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Detalle_Ventas>(sql);
        return result.ToList();
    }

    public async Task<Detalle_Ventas> GetById(int id)
    {
        var detalle = await _dbContext.Connection.GetAsync<Detalle_Ventas>(id);
        return detalle?.IsDeleted == true ? null : detalle;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var detalle = await GetById(id);
        if (detalle == null) return false;

        detalle.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(detalle);
    }
}
