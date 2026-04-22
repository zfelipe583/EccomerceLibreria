using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Detalle_Ventas")]
public class SaleDetail : EntityBase
{
    public int SaleId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [Write(false)]
    public decimal Subtotal { get; set; }
}