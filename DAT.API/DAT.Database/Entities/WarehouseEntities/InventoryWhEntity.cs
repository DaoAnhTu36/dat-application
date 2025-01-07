using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Inventory", Schema = ConfigSchemaTableDatabase.WH)]
    public class InventoryWhEntity : BaseEntity
    {
        public Guid? StockId { get; set; }
        public Guid? GoodsId { get; set; }
        public int Quantity { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Price { get; set; }
    }
}