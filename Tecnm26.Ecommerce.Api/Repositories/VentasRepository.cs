using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class VentasRepository : IVentasRepository
{
    private readonly IDbContext _dbContext;

    public VentasRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Ventas> SaveAsync(Ventas venta)
    {
        venta.Id = await _dbContext.Connection.InsertAsync(venta);
        return venta;
    }

    public async Task<Ventas> UpdateAsync(Ventas venta)
    {
        await _dbContext.Connection.UpdateAsync(venta);
        return venta;
    }

    public async Task<List<Ventas>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Ventas WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Ventas>(sql);
        return result.ToList();
    }

    public async Task<Ventas> GetById(int id)
    {
        var venta = await _dbContext.Connection.GetAsync<Ventas>(id);
        return venta?.IsDeleted == true ? null : venta;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var venta = await GetById(id);
        if (venta == null) return false;

        venta.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(venta);
    }
}