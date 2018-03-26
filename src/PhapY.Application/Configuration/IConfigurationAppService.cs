using System.Threading.Tasks;
using Abp.Application.Services;
using PhapY.Configuration.Dto;

namespace PhapY.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}