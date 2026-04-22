using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

[Table("Envios")]
public class Shipment : EntityBase
{
    public int SaleId { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string ShippingStatus { get; set; }
}