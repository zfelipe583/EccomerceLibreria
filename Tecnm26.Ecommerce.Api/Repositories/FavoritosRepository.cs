using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class FavoritosRepository : IFavoritosRepository
{
    private readonly IDbContext _dbContext;

    public FavoritosRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Favoritos> SaveAsync(Favoritos favorito)
    {
        favorito.Id = await _dbContext.Connection.InsertAsync(favorito);
        return favorito;
    }

    public async Task<Favoritos> UpdateAsync(Favoritos favorito)
    {
        await _dbContext.Connection.UpdateAsync(favorito);
        return favorito;
    }

    public async Task<List<Favoritos>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Favoritos WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Favoritos>(sql);
        return result.ToList();
    }

    public async Task<Favoritos> GetById(int id)
    {
        var favorito = await _dbContext.Connection.GetAsync<Favoritos>(id);
        return favorito?.IsDeleted == true ? null : favorito;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var favorito = await GetById(id);
        if (favorito == null) return false;

        favorito.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(favorito);
    }
}