using AutoMapper;
using AkeoIN.SuperQA.Test_Plans.Dtos;

namespace AkeoIN.SuperQA.Test_Plans.Mapping
{
    public class TestPlanMapProfile : Profile
    {
        public TestPlanMapProfile()
        {
            CreateMap<TestPlan, TestPlanDto>()
                .ForMember(dto => dto.TestCaseCount, 
                    opt => opt.MapFrom(src => src.TestPlanTestCases != null ? src.TestPlanTestCases.Count : 0));

            CreateMap<CreateTestPlanDto, TestPlan>();
            CreateMap<TestPlanDto, TestPlan>();
        }
    }
} 