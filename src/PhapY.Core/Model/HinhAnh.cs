using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("HinhAnh")]
    public class HinhAnh : FullAuditedEntity
    {
        [Required]
        [MaxLength(100)]
        public string TenHinhAnh { get; set; }
        [Required]
        [MaxLength(200)]
        public string DuongDan { get; set; }
        public int HoSoId { get; set; }
        [ForeignKey("HoSoId")]
        public virtual HoSo HoSo { get; set; }
    }
}
