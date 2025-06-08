
public interface IDentalTreatmentRepository
{
    Task<int> AddAsync(DentalTreatment treatment);
    Task<bool> UpdateAsync(DentalTreatment treatment);
    Task<bool> DeleteAsync(int id);
    Task<DentalTreatment> GetByIdAsync(int id);
    Task<IEnumerable<DentalTreatment>> GetAllAsync();
    Task<IEnumerable<DentalTreatment>> GetByDentalModelIdAsync(int dentalModelId);
}