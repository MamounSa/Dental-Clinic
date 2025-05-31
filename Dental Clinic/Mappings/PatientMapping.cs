public class PatientMapping : Profile
{
    public PatientMapping()
    {
        CreateMap<Patient, PatientDto>();
        CreateMap<PatientDto, Patient>();
    }
}


