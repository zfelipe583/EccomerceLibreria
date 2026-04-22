using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Detalle_Libros")]
public class Detalle_Libros : EntityBase
{
    public int IdLibro { get; set; }
    public string PublicoDirigido { get; set; }
    public string Sinopsis { get; set; }
    public int Paginas { get; set; }
}