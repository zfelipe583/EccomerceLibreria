using Dapper;
using Dapper.Contrib.Extensions;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using Tecnm26.Ecommerce.Api.Repositories.Interfaces;
using Tecnm26.Ecommerce.Core.Entities;


namespace Tecnm26.Ecommerce.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbContext _dbContext;

    public BookRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Book> SaveAsync(Book book)
    {
        book.Id = await _dbContext.Connection.InsertAsync(book);
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        await _dbContext.Connection.UpdateAsync(book);
        return book;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        // Cambiamos "Books" por "Libros"
        const string sql = "SELECT * FROM Libros WHERE IsDeleted = 0";
        var books = await _dbContext.Connection.QueryAsync<Book>(sql);
        return books.ToList();
    }

    public async Task<Book> GetById(int id)
    {
        var book = await _dbContext.Connection.GetAsync<Book>(id);
        if (book == null) return null;
        return book.IsDeleted ? null : book;
    }
    

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await GetById(id);
        if (book == null) return false;

        book.IsDeleted = true; 
        return await _dbContext.Connection.UpdateAsync(book);
    }
}