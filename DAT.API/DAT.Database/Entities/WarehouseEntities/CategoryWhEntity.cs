using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Category", Schema = ConfigSchemaTableDatabase.CORE)]
    public class CategoryWhEntity : BaseEntity
    {
        public string? Name { get; set; }
        public Guid ParentId { get; set; }
    }
}