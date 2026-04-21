namespace Tecnm26.Ecommerce.App.DataAccess.Interfaces;
using System.Data.Common;

public interface IDbContext
{
    DbConnection Connection { get; }
}