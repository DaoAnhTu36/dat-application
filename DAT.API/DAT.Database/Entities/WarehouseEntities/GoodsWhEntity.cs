using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Goods", Schema = ConfigSchemaTableDatabase.CORE)]
    public class GoodsWhEntity : BaseEntity
    {
        public string? GoodsCode { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}