
public interface IDentalTreatmentService
{
    Task<int> AddAsync(CreateDentalTreatmentDto dto);
    Task<bool> UpdateAsync(UpdateDentalTreatmentDto dto);
    Task<bool> DeleteAsync(int id);
    Task<DentalTreatmentDto> GetByIdAsync(int id);
    Task<IEnumerable<DentalTreatmentDto>> GetAllAsync();
    Task<IEnumerable<DentalTreatmentDto>> GetByDentalModelIdAsync(int dentalModelId);
}