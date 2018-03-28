using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace PhapY.ChucVu.Dto
{
    [AutoMapTo(typeof(Model.ChucVu))]
    public class ChucVuDto : EntityDto
    {
        public string TenChucVu { get; set; }
        public string DienGiai { get; set; }
    }
}
