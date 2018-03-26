using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PhapY.MultiTenancy;

namespace PhapY.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}