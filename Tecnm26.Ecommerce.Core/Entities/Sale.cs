using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Ventas")]
public class Sale : EntityBase
{
    public int UserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; }
}