public class MedicalImageMapping : Profile
{
    public MedicalImageMapping()
    {
        CreateMap<MedicalImage, MedicalImageDto>().ReverseMap(); // ✅ تمكين التحويل بين `Dto` و `Model`
    }
}
