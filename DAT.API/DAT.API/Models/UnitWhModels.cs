using System.ComponentModel.DataAnnotations;
using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    #region model create new record

    public class UnitWhCreateModelReq
    {
        [Required]
        public string? Name { get; set; }
    }

    public class UnitWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class UnitWhUpdateModelRes
    {
    }

    public class UnitWhUpdateModelReq
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    #endregion model update record

    #region model delete record

    public class UnitWhDeleteModelReq
    {
        public Guid Id { get; set; }
    }

    public class UnitWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class UnitWhListModelRes
    {
        public IEnumerable<UnitWhModel>? List { get; set; }
    }

    public class UnitWhListModelReq : BasePageEntity
    {
    }

    #endregion model get list record

    #region model detail api

    public class UnitWhDetailModelReq
    {
        public Guid? Id { get; set; }
    }

    public class UnitWhDetailModelRes : UnitWhModel
    {
    }

    #endregion model detail api

    public class UnitWhModel : BaseEntity
    {
        public string? Name { get; set; }
    }
}