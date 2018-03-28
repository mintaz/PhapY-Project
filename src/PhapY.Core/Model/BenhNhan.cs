using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace PhapY.Model
{
    [Table("BenhNhan")]
    public class BenhNhan : FullAuditedEntity, IExtendableObject
    {
        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; }
        [Required]
        public bool GioiTinh { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NamSinh { get; set; }
        [MaxLength(500)]
        public string DiaChi { get; set; }
        [MaxLength(100)]
        public string SoDt { get; set; }
        public virtual ICollection<HoSo> Hosos { get; set; } = new HashSet<HoSo>();

        public string ExtensionData { get; set; }
    }
}
