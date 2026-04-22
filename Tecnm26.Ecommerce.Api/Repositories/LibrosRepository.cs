using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class LibrosRepository : ILibrosRepository
{
    private readonly IDbContext _dbContext;

    public LibrosRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Libros> SaveAsync(Libros libro)
    {
        libro.Id = await _dbContext.Connection.InsertAsync(libro);
        return libro;
    }

    public async Task<Libros> UpdateAsync(Libros libro)
    {
        await _dbContext.Connection.UpdateAsync(libro);
        return libro;
    }

    public async Task<List<Libros>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Libros WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Libros>(sql);
        return result.ToList();
    }

    public async Task<Libros> GetById(int id)
    {
        var libro = await _dbContext.Connection.GetAsync<Libros>(id);
        return libro?.IsDeleted == true ? null : libro;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var libro = await GetById(id);
        if (libro == null) return false;

        libro.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(libro);
    }
}