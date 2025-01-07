using System.ComponentModel.DataAnnotations;
using DAT.Common.Users;

namespace DAT.Common.Models.Entitties
{
    internal class Base
    {
    }

    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        public string CreatedBy { get; set; } = AdminInfo.Id ?? "admin";

        [Required]
        public string UpdatedBy { get; set; } = AdminInfo.Id ?? "admin";

        /// <summary>
        /// 0 actived
        /// 1 deactived
        /// 2 deleted
        /// ...
        /// </summary>
        public int Status { get; set; }
    }

    public class BasePageEntity
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}