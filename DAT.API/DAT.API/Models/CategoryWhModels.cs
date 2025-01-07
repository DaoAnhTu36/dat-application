using System.ComponentModel.DataAnnotations;
using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    #region model create new record

    public class CategoryWhCreateModelReq
    {
        [Required]
        public string? Name { get; set; }
    }

    public class CategoryWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class CategoryWhUpdateModelRes
    {
    }

    public class CategoryWhUpdateModelReq
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    #endregion model update record

    #region model delete record

    public class CategoryWhDeleteModelReq
    {
        public Guid Id { get; set; }
    }

    public class CategoryWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class CategoryWhListModelRes
    {
        public IEnumerable<CategoryWhModel>? List { get; set; }
    }

    public class CategoryWhListModelReq : BasePageEntity
    {
    }

    #endregion model get list record

    #region model detail api

    public class CategoryWhDetailModelReq
    {
        public Guid? Id { get; set; }
    }

    public class CategoryWhDetailModelRes : CategoryWhModel
    {
    }

    #endregion model detail api

    public class CategoryWhModel : BaseEntity
    {
        public string? Name { get; set; }
    }
}