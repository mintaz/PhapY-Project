using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("PhuLucHoSo")]
    public class PhuLucHoSo : FullAuditedEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public int LoaiHoSoId { get; set; }
        [ForeignKey("LoaiHoSoId")]
        public virtual LoaiHoSo LoaiHoSo { get; set; }
    }
}
