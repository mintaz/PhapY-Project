using System.Threading.Tasks;
using Abp.Application.Services;
using PhapY.Sessions.Dto;

namespace PhapY.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
