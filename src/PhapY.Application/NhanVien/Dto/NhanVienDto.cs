using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace PhapY.NhanVien.Dto
{
    [AutoMapTo(typeof(Model.NhanVien))]
    public class NhanVienDto : EntityDto
    {
        [Required(ErrorMessage = "Nhập họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Nhập ngày sinh")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string SoDt { get; set; }

        public int? ChucVuId { get; set; }
        public int? TrinhDoId { get; set; }
        public bool BsChinh { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string GenerateMaNv(string input)
        {
            var inputLength = input.Length;
            var prefix = string.Join("", Enumerable.Repeat("0", 6 - inputLength).ToArray());
            return prefix + input;
        }
        public string TenChucVu { get; set; }
    }
}
