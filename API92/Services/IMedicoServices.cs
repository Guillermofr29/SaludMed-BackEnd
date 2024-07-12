using Domain.Entities;

namespace API92.Services
{
    public interface IMedicoServices
    {
        public Task<Response<List<Medicos>>> GetMedicos();
    }
}