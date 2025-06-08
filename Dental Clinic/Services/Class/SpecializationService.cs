using AutoMapper;




    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _repo;
        private readonly IMapper _mapper;

        public SpecializationService(ISpecializationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpecializationDto>> GetAllAsync()
        {
            var specs = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<SpecializationDto>>(specs);
        }

        public async Task<SpecializationDto?> GetByIdAsync(int id)
        {
            var spec = await _repo.GetByIdAsync(id);
            return spec == null ? null : _mapper.Map<SpecializationDto>(spec);
        }

        public async Task<int> AddAsync(CreateSpecializationDto dto)
        {
            var entity = _mapper.Map<Specialization>(dto);
            return await _repo.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(UpdateSpecializationDto dto)
        {
            var entity = _mapper.Map<Specialization>(dto);
            return await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
