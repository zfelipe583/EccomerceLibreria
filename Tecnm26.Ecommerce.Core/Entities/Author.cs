using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Autora")]
public class Author : EntityBase
{
    public string Name { get; set; }
    public string Biography { get; set; }
    public string Inspiration { get; set; }
    public int TotalBooksWritten { get; set; }
}