using Service.DTOs;

namespace Service.Interface
{
    public interface ISampleService
    {
        Task<IEnumerable<SampleReadDTO>> GetAllAsync();
        Task<SampleReadDTO> GetByIdAsync(object id);
        Task<SampleReadDTO> AddAsync(SampleCreateUpdateDTO entity);
        Task<SampleReadDTO> UpdateAsync(Guid id,SampleCreateUpdateDTO entity);
        Task<bool> DeleteAsync(object id);
        Task CollectSamplesAsync(SampleCollectDTO dto);
        Task<IEnumerable<SampleReadDTO>> GetSampleByAppointmentIdAsync(Guid appointmentId);
        Task ReceiveSamplesAsync(SampleReceiveDTO dto);
        Task CollectOnsiteSamplesAsync(SampleCollectDTO dto);
    }
} 