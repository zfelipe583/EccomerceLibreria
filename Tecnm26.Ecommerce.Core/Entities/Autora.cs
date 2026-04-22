using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Autora")]
public class Autora : EntityBase
{
    public string Nombre { get; set; }
    public string Biografia { get; set; }
    public string Inspiracion { get; set; }
    public int TotalLibrosEscritos { get; set; }
}