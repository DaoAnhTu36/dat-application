using System.ComponentModel.DataAnnotations.Schema;
using DAT.Common.Models.Configs;
using DAT.Common.Models.Entitties;

namespace DAT.Database.Entities.WarehouseEntities
{
    [Table("Transaction", Schema = ConfigSchemaTableDatabase.WH)]
    public class TransactionWhEntity : BaseEntity
    {
        public string? TransactionCode { get; set; }

        /// <summary>
        /// 0 - import
        /// 1 - export
        /// </summary>
        public string? TransactionType { get; set; }

        public DateTime? TransactionDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}