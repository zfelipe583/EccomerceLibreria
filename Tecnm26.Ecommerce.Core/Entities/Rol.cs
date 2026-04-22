using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Rol")]
public class Rol : EntityBase
{
    public string Nombre { get; set; }
}