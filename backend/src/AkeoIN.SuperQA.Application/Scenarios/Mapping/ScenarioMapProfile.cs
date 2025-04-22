using AutoMapper;
using AkeoIN.SuperQA.Scenarios.Dtos;

namespace AkeoIN.SuperQA.Scenarios.Mapping
{
    public class ScenarioMapProfile : Profile
    {
        public ScenarioMapProfile()
        {
            // Entity to DTO
            CreateMap<Scenario, ScenarioDto>()
                .ForMember(dto => dto.FeatureName, 
                    opt => opt.MapFrom(src => src.Feature != null ? src.Feature.Name : null));

            // DTO to Entity
            CreateMap<ScenarioDto, Scenario>();

            // CreateDTO to Entity
            CreateMap<CreateScenarioDto, Scenario>();
        }
    }
} 