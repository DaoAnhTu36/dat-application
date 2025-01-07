using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Supplier", Schema = ConfigSchemaTableDatabase.CORE)]
    public class SupplierWhEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}