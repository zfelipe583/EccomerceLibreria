using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbContext _dbContext;

    public UsuarioRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Usuario> SaveAsync(Usuario usuario)
    {
        usuario.Id = await _dbContext.Connection.InsertAsync(usuario);
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        await _dbContext.Connection.UpdateAsync(usuario);
        return usuario;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Usuario WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Usuario>(sql);
        return result.ToList();
    }

    public async Task<Usuario> GetById(int id)
    {
        var usuario = await _dbContext.Connection.GetAsync<Usuario>(id);
        return usuario?.IsDeleted == true ? null : usuario;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await GetById(id);
        if (usuario == null) return false;

        usuario.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(usuario);
    }
    public async Task<Usuario> Login(string username, string password)
    {
        string sql = @"SELECT * FROM Usuario 
                   WHERE Username = @Username 
                   AND Password = @Password 
                   AND IsDeleted = 0";

        return await _dbContext.Connection.QueryFirstOrDefaultAsync<Usuario>(
            sql, 
            new { Username = username, Password = password }
        );
    }
}