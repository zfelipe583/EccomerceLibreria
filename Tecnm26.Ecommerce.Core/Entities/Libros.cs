using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Libros")]
public class Libros : EntityBase
{
    public int IdAutora { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}