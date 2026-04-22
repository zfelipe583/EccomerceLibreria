using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IDbContext _dbContext;

    public RoleRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Rol> SaveAsync(Rol rol)
    {
        rol.Id = await _dbContext.Connection.InsertAsync(rol);
        return rol;
    }

    public async Task<Rol> UpdateAsync(Rol rol)
    {
        await _dbContext.Connection.UpdateAsync(rol);
        return rol;
    }

    public async Task<List<Rol>> GetAllAsync()
    {
        string sql = "SELECT * FROM Rol WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Rol>(sql);
        return result.ToList();
    }

    public async Task<Rol> GetById(int id)
    {
        var rol = await _dbContext.Connection.GetAsync<Rol>(id);
        return rol?.IsDeleted == true ? null : rol;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rol = await GetById(id);
        if (rol == null) return false;

        rol.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(rol);
    }
}