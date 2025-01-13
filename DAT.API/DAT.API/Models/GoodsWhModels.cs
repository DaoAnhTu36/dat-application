using System.ComponentModel.DataAnnotations;
using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    public class GoodsWhCreateModelReq
    {
        [Required]
        public string? GoodsCode { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }

    public class GoodsWhCreateModelRes
    {
    }

    public class GoodsWhUpdateModelRes
    {
    }

    public class GoodsWhUpdateModelReq
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class GoodsWhDeleteModelReq
    {
        public Guid Id { get; set; }
    }

    public class GoodsWhDeleteModelRes
    {
    }

    public class GoodsWhListModelRes
    {
        public IEnumerable<GoodsDetailWhModel>? List { get; set; }
    }

    public class GoodsWhListModelReq : BasePageEntity
    {
    }

    public class GoodsWhDetailModelReq
    {
        public Guid Id { get; set; }
        public string? GoodsCode { get; set; }
    }

    public class GoodsWhDetailModelRes : GoodsDetailWhModel
    {
    }

    public class GoodsDetailWhModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? GoodsCode { get; set; }
    }

    public class GoodsWhSearchListModelRes
    {
        public IEnumerable<GoodsDetailWhModel>? List { get; set; }
    }

    public class GoodsWhSearchListModelReq : BasePageEntity
    {
        public string? TextSearch { get; set; }
    }
}