
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<Specialization, SpecializationDto>().ReverseMap();
            CreateMap<CreateSpecializationDto, Specialization>();
            CreateMap<UpdateSpecializationDto, Specialization>();
        }
    }
