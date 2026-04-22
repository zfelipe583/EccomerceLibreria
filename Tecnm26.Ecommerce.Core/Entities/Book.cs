using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Libros")]
public class Book : EntityBase
{
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}