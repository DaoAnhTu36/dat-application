using System.Collections;

namespace DAT.API.Models
{
    #region model create new record

    public class GoodsRetailWhCreateModelReq
    {
        public IEnumerable<GoodsRetailWhCreateSubModelReq> ListReq { get; set; }
    }
    public class GoodsRetailWhCreateSubModelReq
    {
        public Guid GoodsId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class GoodsRetailWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class GoodsRetailWhUpdateModelRes
    {
    }

    public class GoodsRetailWhUpdateModelReq
    {
    }

    #endregion model update record

    #region model delete record

    public class GoodsRetailWhDeleteModelReq
    {
    }

    public class GoodsRetailWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class GoodsRetailWhListModelRes
    {
    }

    public class GoodsRetailWhListModelReq
    {
    }

    #endregion model get list record
}