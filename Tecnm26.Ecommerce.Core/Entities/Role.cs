namespace Tecnm26.Ecommerce.Core.Entities;

using Dapper.Contrib.Extensions;

[Table("Rol")]
public class Role : EntityBase
{
    public string Name { get; set; }
}