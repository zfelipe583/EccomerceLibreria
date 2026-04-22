namespace Tecnm26.Ecommerce.Api.DataAccess.Interfaces;
using System.Data.Common;

public interface IDbContext
{
    DbConnection Connection { get; }
}