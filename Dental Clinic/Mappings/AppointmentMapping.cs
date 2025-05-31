public class AppointmentMapping : Profile
{
    public AppointmentMapping()
    {
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name)) // ✅ تحويل `DoctorId` إلى `DoctorName`
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.Name)); // ✅ تحويل `PatientId` إلى `PatientName`

        CreateMap<AppointmentDto, Appointment>()
            .ForMember(dest => dest.Doctor, opt => opt.Ignore()) // ✅ تجاهل الطبيب عند الإدخال
            .ForMember(dest => dest.Patient, opt => opt.Ignore()); // ✅ تجاهل المريض عند الإدخال
    }
}
