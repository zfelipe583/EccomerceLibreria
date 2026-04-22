using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Detalle_Libros")]
public class BookDetail : EntityBase
{
    public int BookId { get; set; }
    public string TargetAudience { get; set; }
    public string Synopsis { get; set; }
    public int Pages { get; set; }
}