using System.ComponentModel.DataAnnotations;
using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    #region model create new record

    public class OrderWhCreateModelReq
    {
        [Required]
        public string? Name { get; set; }
    }

    public class OrderWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class OrderWhUpdateModelRes
    {
    }

    public class OrderWhUpdateModelReq
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    #endregion model update record

    #region model delete record

    public class OrderWhDeleteModelReq
    {
        public Guid Id { get; set; }
    }

    public class OrderWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class OrderWhListModelRes
    {
        public IEnumerable<OrderWhModel>? List { get; set; }
    }

    public class OrderWhListModelReq : BasePageEntity
    {
    }

    #endregion model get list record

    #region model detail api

    public class OrderWhDetailModelReq
    {
        public Guid? Id { get; set; }
    }

    public class OrderWhDetailModelRes : OrderWhModel
    {
    }

    #endregion model detail api

    public class OrderWhModel : BaseEntity
    {
        public string? Name { get; set; }
    }
}