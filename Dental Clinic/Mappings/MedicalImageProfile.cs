using AutoMapper;

public class MedicalImageProfile : Profile
{
    public MedicalImageProfile()
    {
        CreateMap<MedicalImage, MedicalImageDto>().ReverseMap();
        CreateMap<CreateMedicalImageDto, MedicalImage>();
        CreateMap<UpdateMedicalImageDto, MedicalImage>();
    }
}