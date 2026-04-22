using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Favoritos")]
public class Favorite : EntityBase
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}