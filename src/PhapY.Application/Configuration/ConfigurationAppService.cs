using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using PhapY.Configuration.Dto;

namespace PhapY.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : PhapYAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
