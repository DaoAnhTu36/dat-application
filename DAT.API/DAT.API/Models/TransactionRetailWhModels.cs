using System.ComponentModel.DataAnnotations;
using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    public class TransactionRetailWhCreateModelReq
    {
        public List<TranRetailDTO> Items { get; set; }
    }

    public class TranRetailDTO
    {
        [Required]
        public string GoodsId { get; set; }

        [Required]
        public string GoodsName { get; set; }

        [Required]
        public string GoodsCode { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string UnitId { get; set; }
        public string TransDetailId { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class TransactionRetailWhCreateModelRes
    {
    }

    public class TransactionRetailWhUpdateModelRes
    {
    }

    public class TransactionRetailWhUpdateModelReq
    {
    }

    public class TransactionRetailWhDeleteModelReq
    {
    }

    public class TransactionRetailWhDeleteModelRes
    {
    }

    public class TransactionRetailWhListModelRes
    {
    }

    public class TransactionRetailWhListModelReq : BasePageEntity
    {
    }

    public class TransactionRetailWhDetailModelReq
    {
    }

    public class TransactionRetailWhDetailModelRes : TransactionRetailWhModel
    {
    }

    public class TransactionRetailWhModel : BaseEntity
    {
    }
}