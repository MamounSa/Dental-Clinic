public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<UpdateDoctorDto, Doctor>();
    }
}
