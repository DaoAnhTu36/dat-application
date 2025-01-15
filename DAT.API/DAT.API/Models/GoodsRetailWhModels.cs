using System.Collections;
using DAT.Common.Models.Entitties;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public string GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public Guid TransDetailId { get; set; }
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
        public Guid Id { get; set; }
        public decimal Price { get; set; }
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
        public IEnumerable<GoodsRetailModelRes> List { get; set; }
    }

    public class GoodsRetailModelRes
    {
        public string GoodsName { get; set; }
        public IEnumerable<GoodsRetailInstance> GoodsRetails { get; set; }
    }

    public class GoodsRetailInstance
    {
        public Guid Id { get; set; }
        public Guid GoodsId { get; set; }
        public string GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public Guid TransDetailId { get; set; }
    }

    public class GoodsRetailWhListModelReq : BasePageEntity
    {
    }

    #endregion model get list record

    public class GoodsRetailWhDetailModelReq
    {
        public string GoodsCode { get; set; }
    }

    public class GoodsRetailWhDetailModelRes
    {
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
    }
    public class GoodsRetailWhSearchlModelReq
    {
        public string TextSearch { get; set; }
    }

    public class GoodsRetailWhSearchlModelRes
    {
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public string UnitName { get; set; }
    }
}