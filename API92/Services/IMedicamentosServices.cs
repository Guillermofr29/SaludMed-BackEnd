using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API92.Services
{
    public interface IMedicamentosServices
    {
        public Task<Response<List<Medicamentos>>> GetMedicamentos();

    }
}

