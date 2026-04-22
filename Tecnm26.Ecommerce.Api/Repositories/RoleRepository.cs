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

    public async Task<Role> SaveAsync(Role role)
    {
        role.Id = await _dbContext.Connection.InsertAsync(role);
        return role;
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        await _dbContext.Connection.UpdateAsync(role);
        return role;
    }

    public async Task<List<Role>> GetAllAsync()
    {
        string sql = "SELECT * FROM Rol WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Role>(sql);
        return result.ToList();
    }

    public async Task<Role> GetById(int id)
    {
        var role = await _dbContext.Connection.GetAsync<Role>(id);
        return role?.IsDeleted == true ? null : role;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await GetById(id);
        if (role == null) return false;

        role.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(role);
    }
}