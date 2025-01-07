using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("TransactionDetail", Schema = ConfigSchemaTableDatabase.WH)]
    public class TransactionDetailWhEntity : BaseEntity
    {
        public Guid TransactionId { get; set; }
        public Guid GoodsId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid UnitId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? DateOfExpired { get; set; }
    }
}