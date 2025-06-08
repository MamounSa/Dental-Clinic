using AutoMapper;

public class MedicalRecordProfile : Profile
{
    public MedicalRecordProfile()
    {
        CreateMap<MedicalRecord, MedicalRecordDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name));

        CreateMap<CreateMedicalRecordDto, MedicalRecord>();
        CreateMap<UpdateMedicalRecordDto, MedicalRecord>();
    }
}