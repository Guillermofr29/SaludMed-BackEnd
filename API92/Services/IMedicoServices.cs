using Domain.Entities;

namespace API92.Services
{
    public interface IMedicoServices
    {
        public Task<Response<List<NombreMedico>>> GetMedicos();

        public Task<Response<DataMedico>> GetMedico(int ID_Cita);

    }
}