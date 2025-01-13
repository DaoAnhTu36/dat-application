namespace DAT.API.Models
{
    #region model create new record

    public class GoodsPriceWhCreateModelReq
    {
        public Guid GoodsId { get; set; }
        public decimal RetailPrice { get; set; }
    }

    public class GoodsPriceWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class GoodsPriceWhUpdateModelRes
    {
    }

    public class GoodsPriceWhUpdateModelReq
    {
        public Guid Id { get; set; }
        public decimal RetailPrice { get; set; }
    }

    #endregion model update record

    #region model delete record

    public class GoodsPriceWhDeleteModelReq
    {
    }

    public class GoodsPriceWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class GoodsPriceWhListModelRes
    {
    }

    public class GoodsPriceWhListModelReq
    {
    }

    #endregion model get list record
}