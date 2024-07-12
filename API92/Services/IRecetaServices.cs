using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API92.Services
{
    public interface IRecetaService
    {
        public Task<int> CrearRecetaConMedicamentos(Receta receta);
    }
}
