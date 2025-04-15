using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using AkeoIN.SuperQA.Configuration.Dto;

namespace AkeoIN.SuperQA.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SuperQAAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
