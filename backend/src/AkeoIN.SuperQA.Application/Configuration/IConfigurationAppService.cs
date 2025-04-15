using System.Threading.Tasks;
using AkeoIN.SuperQA.Configuration.Dto;

namespace AkeoIN.SuperQA.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
