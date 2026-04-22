using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class AutoraRepository : IAutoraRepository
{
    private readonly IDbContext _dbContext;

    public AutoraRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Autora> SaveAsync(Autora autora)
    {
        autora.Id = await _dbContext.Connection.InsertAsync(autora);
        return autora;
    }

    public async Task<Autora> UpdateAsync(Autora autora)
    {
        await _dbContext.Connection.UpdateAsync(autora);
        return autora;
    }

    public async Task<List<Autora>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Autora WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Autora>(sql);
        return result.ToList();
    }

    public async Task<Autora> GetById(int id)
    {
        var autora = await _dbContext.Connection.GetAsync<Autora>(id);
        return autora?.IsDeleted == true ? null : autora;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var autora = await GetById(id);
        if (autora == null) return false;

        autora.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(autora);
    }
}