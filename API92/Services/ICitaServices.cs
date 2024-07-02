using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API92.Services
{
    public interface ICitaServices
    {
        public Task<Response<List<AllCitas>>> GetCitas(int medicoID);

        public Task<Response<Citas>> CrearCitas(Citas i);

        public Task<Response<Citas>> EditarCitas(Citas i);

        public Task<Response<Citas>> EliminarCitas(int id);

        public Task<Response<int>> GetTotalCitas(int medicoID);

        public Task<Response<int>> GetCitasPendientes(int medicoID);

        public Task<Response<List<UltimasCita>>> GetUltimasCincoCitas(int medicoID);

        //public Task<Response<List<MedicamentoMasRecetado>>> GetCincoMedicamentosMasRecetados();

        public Task<Response<List<MotivoConsulta>>> GetCincoMotivosConsultaMasComunes(int medicoID);

        public Task<Response<List<ProximasCitas>>> GetProximasCitas(int medicoID, int rolID);

        public Task<Response<TraerCitaID>> GetCitaPorID(int id);
    }
}

