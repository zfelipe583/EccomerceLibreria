using System.Data.Common;
using MySqlConnector;
using Tecnm26.Ecommerce.Api.DataAccess.Interfaces;

namespace Tecnm26.Ecommerce.Api.DataAccess;

public class DbContext: IDbContext
{
    private readonly string _connectionString="server=localhost;user=root;password=Felipe2004;database=DistribuidoraLibros;port=3306";
    
    private MySqlConnection _connection;

    public DbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionString);
            }
            return _connection;
        }
    }
}