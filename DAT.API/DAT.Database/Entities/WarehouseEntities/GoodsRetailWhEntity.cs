using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("GoodsRetail", Schema = ConfigSchemaTableDatabase.WH)]
    public class GoodsRetailWhEntity : BaseEntity
    {
        public Guid GoodsId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid TransDetailId { get; set; }
    }
}