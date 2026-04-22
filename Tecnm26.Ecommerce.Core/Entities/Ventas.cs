using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Ventas")]
public class Ventas : EntityBase
{
    public int IdUsuario { get; set; }
    public DateTime FechaVenta { get; set; }
    public decimal Total { get; set; }
    public string MetodoPago { get; set; }
}