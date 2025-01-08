using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    #region model create new record

    public class TransactionWhCreateModelReq
    {
        public string? TransactionCode { get; set; }

        /// <summary>
        /// 0 import
        /// 1 export
        /// </summary>
        public string? TransactionType { get; set; }

        public DateTime? TransactionDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid StockId { get; set; }
        public List<SubTransactionWhCreateModelReq>? Details { get; set; }
    }

    public class SubTransactionWhCreateModelReq
    {
        public Guid GoodsId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid UnitId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? DateOfExpired { get; set; }
    }

    public class TransactionWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class TransactionWhUpdateModelRes
    {
    }

    public class TransactionWhUpdateModelReq
    {
    }

    #endregion model update record

    #region model delete record

    public class TransactionWhDeleteModelReq
    {
    }

    public class TransactionWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class TransactionWhListModelRes
    {
        public IEnumerable<TransactionWhModel>? List { get; set; }
    }

    public class TransactionWhListModelReq : BasePageEntity
    {
        /// <summary>
        /// 0 - import
        /// 1 - export
        /// </summary>
        public string? TransactionType { get; set; }
    }

    #endregion model get list record

    #region model get detail record

    public class TransactionWhDetailModelRes : BaseEntity
    {
        public string? TransactionCode { get; set; }
        public string? TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<TransactionDetailModels>? Details { get; set; }
    }

    public class TransactionWhDetailModelReq
    {
        public Guid Id { get; set; }
    }

    #endregion model get detail record

    public class TransactionWhModel : BaseEntity
    {
        public string? TransactionCode { get; set; }
        public string? TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid StockId { get; set; }
        public string StockName { get; set; }
    }
}