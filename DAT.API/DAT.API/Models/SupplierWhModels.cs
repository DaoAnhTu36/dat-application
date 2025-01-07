using DAT.Common.Models.Entitties;

namespace DAT.API.Models
{
    #region model create new record

    public class SupplierWhCreateModelReq
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class SupplierWhCreateModelRes
    {
    }

    #endregion model create new record

    #region model update record

    public class SupplierWhUpdateModelRes
    {
    }

    public class SupplierWhUpdateModelReq
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    #endregion model update record

    #region model delete record

    public class SupplierWhDeleteModelReq
    {
        public Guid Id { get; set; }
    }

    public class SupplierWhDeleteModelRes
    {
    }

    #endregion model delete record

    #region model get list record

    public class SupplierWhListModelRes
    {
        public IEnumerable<SupplierModel>? List { get; set; }
    }

    public class SupplierModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class SupplierWhListModelReq : BasePageEntity
    {
    }

    #endregion model get list record

    #region model get detail record

    public class SupplierWhDetailModelReq
    {
        public Guid Id { get; set; }
    }

    public class SupplierWhDetailModelRes : SupplierModel
    {
    }

    #endregion model get detail record
}