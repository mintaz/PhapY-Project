using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("ChucVu")]
    public class ChucVu : FullAuditedEntity
    {
        [Required]
        [MaxLength(100)]
        public string TenChucVu { get; set; }
        [MaxLength(200)]
        public string DienGiai { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; } = new HashSet<NhanVien>();
    }
}
