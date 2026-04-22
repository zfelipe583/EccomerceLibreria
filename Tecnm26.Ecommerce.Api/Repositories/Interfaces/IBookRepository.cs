using Tecnm26.Ecommerce.Core.Entities;
namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> SaveAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<List<Book>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Book> GetById(int id);
}