using Tecnm26.Ecommerce.Core.Entities;

namespace Tecnm26.Ecommerce.Api.Repositories.Interfaces;

public interface IEnviosRepository
{
    Task<Envios> SaveAsync(Envios envio);
    Task<Envios> UpdateAsync(Envios envio);
    Task<List<Envios>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Envios> GetById(int id);
}