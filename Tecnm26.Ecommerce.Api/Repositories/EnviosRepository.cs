using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class EnviosRepository : IEnviosRepository
{
    private readonly IDbContext _dbContext;

    public EnviosRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Envios> SaveAsync(Envios envio)
    {
        envio.Id = await _dbContext.Connection.InsertAsync(envio);
        return envio;
    }

    public async Task<Envios> UpdateAsync(Envios envio)
    {
        await _dbContext.Connection.UpdateAsync(envio);
        return envio;
    }

    public async Task<List<Envios>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Envios WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Envios>(sql);
        return result.ToList();
    }

    public async Task<Envios> GetById(int id)
    {
        var envio = await _dbContext.Connection.GetAsync<Envios>(id);
        return envio?.IsDeleted == true ? null : envio;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var envio = await GetById(id);
        if (envio == null) return false;

        envio.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(envio);
    }
}