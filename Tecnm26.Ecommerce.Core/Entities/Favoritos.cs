using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Favoritos")]
public class Favoritos : EntityBase
{
    public int IdUsuario { get; set; }
    public int IdLibro { get; set; }
}