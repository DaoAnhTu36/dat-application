using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Unit", Schema = ConfigSchemaTableDatabase.CORE)]
    public class UnitWhEntity : BaseEntity
    {
        public string? Name { get; set; }
    }
}