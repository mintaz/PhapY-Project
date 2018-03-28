using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhapY.Model
{
    [Table("HoSo")]
    public class HoSo : FullAuditedEntity, IExtendableObject
    {
        [Required]
        [MaxLength(200)]
        public string DiaDiemXayRa { get; set; }
        [Required]
        [MaxLength(15)]
        public string SoHoSo { get; set; }
        [Required]
        [MaxLength(5)]
        public string SoQuyetDinhTcgd { get; set; }
        [Required]
        [MaxLength(100)]
        public string CoQuanTc { get; set; }
        [Required]
        public DateTime NgayQuyetDinhTrungCau { get; set; }
        [Required]
        public DateTime NgayGiamDinh { get; set; }
        [MaxLength(200)]
        public string DiaDiemGiamDinh { get; set; }
        [Required]
        public string ContentFilePath { get; set; }
        [Required]
        public int BenhNhanId { get; set; }
        [ForeignKey("BenhNhanId")]
        public virtual BenhNhan BenhNhan { get; set; }
        public int LoaiHoSoId { get; set; }
        [ForeignKey("LoaiHoSoId")]
        public virtual LoaiHoSo LoaiHoSo { get; set; }
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; } = new HashSet<HinhAnh>();
        public virtual ICollection<NhanVien> NhanViens { get; set; } = new HashSet<NhanVien>();

        public string ExtensionData { get; set; }
    }
}
