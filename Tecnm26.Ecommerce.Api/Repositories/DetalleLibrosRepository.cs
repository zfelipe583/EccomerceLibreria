using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class DetalleLibrosRepository : IDetalleLibrosRepository
{
    private readonly IDbContext _dbContext;

    public DetalleLibrosRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Detalle_Libros> SaveAsync(Detalle_Libros detalle)
    {
        detalle.Id = await _dbContext.Connection.InsertAsync(detalle);
        return detalle;
    }

    public async Task<Detalle_Libros> UpdateAsync(Detalle_Libros detalle)
    {
        await _dbContext.Connection.UpdateAsync(detalle);
        return detalle;
    }

    public async Task<List<Detalle_Libros>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Detalle_Libros WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Detalle_Libros>(sql);
        return result.ToList();
    }

    public async Task<Detalle_Libros> GetById(int id)
    {
        var detalle = await _dbContext.Connection.GetAsync<Detalle_Libros>(id);
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