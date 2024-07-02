using Domain.Entities;

namespace API92.Services
{
    public interface IMotivoCitaServices
    {
        public Task<Response<List<MotivoCita>>> GetMotivoByName(string motivo);
    }
}
