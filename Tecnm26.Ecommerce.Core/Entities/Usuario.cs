using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Usuario")]

public class Usuario : EntityBase
{
    public int IdRol { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string NombreCompleto { get; set; } 
}