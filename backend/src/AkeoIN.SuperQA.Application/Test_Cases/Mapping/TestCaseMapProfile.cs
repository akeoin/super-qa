using AutoMapper;
using AkeoIN.SuperQA.Test_Cases;
using AkeoIN.SuperQA.Test_Cases.Dtos;

namespace AkeoIN.SuperQA.Test_Cases.Mapping
{
    public class TestCaseMapProfile : Profile
    {
        public TestCaseMapProfile()
        {
            // Entity to DTO
            CreateMap<TestCase, TestCaseDto>()
                .ForMember(dto => dto.ScenarioName, 
                    opt => opt.MapFrom(src => src.Scenario != null ? src.Scenario.Name : null));

            // DTO to Entity
            CreateMap<TestCaseDto, TestCase>();

            // CreateDTO to Entity
            CreateMap<CreateTestCaseDto, TestCase>();
        }
    }
} 