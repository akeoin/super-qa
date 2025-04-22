using Abp.Application.Services;
using AkeoIN.SuperQA.Scenarios.Dtos;

namespace AkeoIN.SuperQA.Scenarios
{
    public interface IScenarioAppService : IAsyncCrudAppService<ScenarioDto, int, PagedScenarioResultRequestDto, CreateScenarioDto, ScenarioDto>
    {
    }
}