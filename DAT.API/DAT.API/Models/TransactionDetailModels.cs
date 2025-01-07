using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    public class TransactionDetailModels : BaseEntity
    {
        public string? GoodsCode { get; set; }
        public string? GoodsName { get; set; }
        public string? SupplierName { get; set; }
        public string? UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? DateOfExpired { get; set; }
    }
}