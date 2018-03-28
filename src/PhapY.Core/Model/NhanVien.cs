using Abp.Domain.Entities.Auditing;
using PhapY.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("NhanVien")]
    public class NhanVien : FullAuditedEntity
    {
        [Required]
        [MaxLength(200)]
        public string HoTen { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        [MaxLength(6)]
        public string MaNv { get; set; }
        [MaxLength(100)]
        public string SoDt { get; set; }
        [MaxLength(500)]
        public string DiaChi { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }

        public bool BsChinh { get; set; }

        public int? TrinhDoId { get; set; }
        [ForeignKey("TrinhDoId")]
        public virtual TrinhDo TrinhDo { get; set; }

        public int? ChucVuId { get; set; }
        [ForeignKey("ChucVuId")]
        public virtual ChucVu ChucVu { get; set; }

        public virtual ICollection<HoSo> Hosos { get; set; } = new HashSet<HoSo>();

        public long? TaiKhoanId { get; set; }
        public virtual User TaiKhoan { get; set; }
    }
}
