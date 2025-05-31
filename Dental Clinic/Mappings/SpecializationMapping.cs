public class SpecializationMapping : Profile
{
    public SpecializationMapping()
    {
        CreateMap<Specialization, SpecializationDto>(); // ✅ تحويل `Specialization` إلى `SpecializationDto`
        CreateMap<SpecializationDto, Specialization>(); // ✅ تحويل `SpecializationDto` إلى `Specialization`
    }
}
