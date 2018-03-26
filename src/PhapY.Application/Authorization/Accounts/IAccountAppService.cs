using System.Threading.Tasks;
using Abp.Application.Services;
using PhapY.Authorization.Accounts.Dto;

namespace PhapY.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
