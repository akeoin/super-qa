using AutoMapper;
using AkeoIN.SuperQA.TestRuns.Dtos;

namespace AkeoIN.SuperQA.TestRuns.Mapping
{
    public class TestRunMapProfile : Profile
    {
        public TestRunMapProfile()
        {
            CreateMap<TestRun, TestRunDto>()
                .ForMember(dto => dto.TestResultCount,
                    opt => opt.MapFrom(src => src.TestResults != null ? src.TestResults.Count : 0));

            CreateMap<CreateTestRunDto, TestRun>();
            CreateMap<TestRunDto, TestRun>();
        }
    }
} 