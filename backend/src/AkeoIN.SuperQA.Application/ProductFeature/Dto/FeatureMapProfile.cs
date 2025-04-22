using AutoMapper;
using AkeoIN.SuperQA.ProductFeature;

namespace AkeoIN.SuperQA.ProductFeature.Dto
{
    public class FeatureMapProfile : Profile
    {
        public FeatureMapProfile()
        {
            // Entity to DTO
            CreateMap<Feature, FeatureDto>()
                .ForMember(dto => dto.ParentFeatureName, 
                    opt => opt.MapFrom(src => src.ParentFeature != null ? src.ParentFeature.Name : null));

            // DTO to Entity
            CreateMap<FeatureDto, Feature>();

            // CreateDTO to Entity
            CreateMap<CreateFeatureDto, Feature>();
        }
    }
} 