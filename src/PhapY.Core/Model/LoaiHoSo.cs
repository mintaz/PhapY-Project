using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("LoaiHoSo")]
    public class LoaiHoSo : FullAuditedEntity
    {
        [Required]
        [MaxLength(100)]
        public string TenLoaiHs { get; set; }
        public virtual ICollection<PhuLucHoSo> PhuLucHoSos { get; set; } = new HashSet<PhuLucHoSo>();
        public virtual ICollection<HoSo> HoSos { get; set; } = new HashSet<HoSo>();
    }
}
