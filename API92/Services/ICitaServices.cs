using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API92.Services
{
    public interface ICitaServices
    {
        public Task<Response<List<Citas>>> GetCitas();

        public Task<Response<Citas>> CrearCitas(Citas i);

        public Task<Response<Citas>> EditarCitas(Citas i);

        public Task<Response<Citas>> EliminarCitas(int id);

        public Task<Response<int>> GetTotalCitas();

        public Task<Response<int>> GetCitasPendientes();

        public Task<Response<List<UltimasCita>>> GetUltimasCincoCitas();

        public Task<Response<List<MedicamentoMasRecetado>>> GetCincoMedicamentosMasRecetados();

        public Task<Response<List<MotivoConsulta>>> GetCincoMotivosConsultaMasComunes();
    }
}

