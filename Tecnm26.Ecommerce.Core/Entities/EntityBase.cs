using Dapper.Contrib.Extensions;

namespace Tecnm26.Ecommerce.Core.Entities;

public class EntityBase
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}