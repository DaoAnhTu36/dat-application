using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("TransactionRetail", Schema = ConfigSchemaTableDatabase.SHOP)]
    public class TransactionRetailWhEntity : BaseEntity
    {
        public Guid GoodsId { get; set; }
        public string GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public Guid TransDetailId { get; set; }
        public int Quantity { get; set; }
        public Guid UnitId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}