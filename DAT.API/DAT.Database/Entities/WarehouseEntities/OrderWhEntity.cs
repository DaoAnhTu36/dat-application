using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Order", Schema = ConfigSchemaTableDatabase.WH)]
    public class OrderWhEntity : BaseEntity
    {
        [Required]
        public string? CustomerName { get; set; }

        [Required]
        public string? CustomerAddress { get; set; }

        [Required]
        public string? CustomerPhone { get; set; }

        public string? CustomerEmail { get; set; }
        public string? CustomerNote { get; set; }
        public Guid CustomerId { get; set; }
        public Guid GoodsId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}