public class MedicalRecordMapping : Profile
{
    public MedicalRecordMapping()
    {
        CreateMap<MedicalRecord, MedicalRecordDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name)); // ✅ تحويل `PatientId` إلى `PatientName`

        CreateMap<MedicalRecordDto, MedicalRecord>()
            .ForMember(dest => dest.Patient, opt => opt.Ignore()); // ✅ تجاهل ربط `Patient` عند الإدخال
    }
}
