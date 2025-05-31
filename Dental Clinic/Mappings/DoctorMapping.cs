public class DoctorMapping : Profile
{
    public DoctorMapping()
    {
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name)); // ✅ تحويل الاختصاص إلى `SpecializationDto`

        CreateMap<DoctorDto, Doctor>()
            .ForMember(dest => dest.Specialization, opt => opt.Ignore());
    }
}
