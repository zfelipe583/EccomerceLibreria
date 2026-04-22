using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Detalle_Ventas")]
public class Detalle_Ventas : EntityBase
{
    public int IdVenta { get; set; }
    public int IdLibro { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}