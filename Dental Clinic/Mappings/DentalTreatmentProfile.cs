
    public class DentalTreatmentProfile : Profile
    {
        public DentalTreatmentProfile()
        {
            CreateMap<DentalTreatment, DentalTreatmentDto>().ReverseMap();
            CreateMap<CreateDentalTreatmentDto, DentalTreatment>();
            CreateMap<UpdateDentalTreatmentDto, DentalTreatment>();
        }
    }
