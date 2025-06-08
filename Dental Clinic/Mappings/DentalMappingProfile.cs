using AutoMapper;

public class DentalMappingProfile : Profile
{
    public DentalMappingProfile()
    {
        // DentalModel ↔ DTOs
        CreateMap<DentalModel, DentalModelDto>();
        CreateMap<CreateDentalModelDto, DentalModel>();
        CreateMap<UpdateDentalModelDto, DentalModel>();

        // DentalTreatment ↔ DTO
       // CreateMap<DentalTreatment, DentalTreatmentDto>();
    }
}