using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Stock", Schema = ConfigSchemaTableDatabase.CORE)]
    public class StockWhEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}