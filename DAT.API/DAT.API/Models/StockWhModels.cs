using System.ComponentModel.DataAnnotations;
using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    #region model create new record

    public class StockWhCreateModelReq
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }
    }

    public class StockWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class StockWhUpdateModelRes
    {
    }

    public class StockWhUpdateModelReq
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }
    }

    #endregion model update record

    #region model delete record

    public class StockWhDeleteModelReq
    {
        public Guid Id { get; set; }
    }

    public class StockWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class StockWhListModelRes
    {
        public IEnumerable<StockWhModel>? List { get; set; }
    }

    public class StockWhListModelReq : BasePageEntity
    {
    }

    #endregion model get list record

    #region model detail api

    public class StockWhDetailModelReq
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class StockWhDetailModelRes : StockWhModel
    {
    }

    #endregion model detail api

    public class StockWhModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class StockWhSearchListModelRes
    {
        public IEnumerable<StockWhModel>? List { get; set; }
    }

    public class StockWhSearchListModelReq : BasePageEntity
    {
        public string? TextSearch { get; set; }
    }
}