using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Envios")]
public class Envios : EntityBase
{
    public int IdVenta { get; set; }
    public string DireccionEnvio { get; set; }
    public string Ciudad { get; set; }
    public string CodigoPostal { get; set; }
    public string EstadoEnvio { get; set; }
}