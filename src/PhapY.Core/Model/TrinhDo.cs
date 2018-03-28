using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("TrinhDo")]
    public class TrinhDo : FullAuditedEntity
    {
        [Required]
        [MaxLength(100)]
        public string TenTrinhDo { get; set; }
        [MaxLength(200)]
        public string DienGiai { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; } = new HashSet<NhanVien>();
    }
}
