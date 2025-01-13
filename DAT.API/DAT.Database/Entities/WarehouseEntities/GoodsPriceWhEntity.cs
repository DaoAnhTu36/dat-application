using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("GoodsPrice", Schema = ConfigSchemaTableDatabase.WH)]
    public class GoodsPriceWhEntity : BaseEntity
    {
        public Guid GoodsId { get; set; }
        public Guid TransactionDetailId { get; set; }
        public decimal RetailPrice { get; set; }
    }
}