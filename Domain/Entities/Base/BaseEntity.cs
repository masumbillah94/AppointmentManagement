using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
    public class BaseEntity<IdType>
    {
        #region Public Properties

        public IdType Id { get; set; } = default(IdType)!;
        public bool IsDelete { get; set; } = false;

        [StringLength(100)]
        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        #endregion Public Properties
    }
}
