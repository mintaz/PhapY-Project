using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhapY.MultiTenancy.Dto;

namespace PhapY.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
