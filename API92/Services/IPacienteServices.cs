using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API92.Services
{
    public interface IPacienteServices
    {
        public Task<Response<List<Pacientes>>> GetPacientes(int medicoID);
        public Task<Response<List<PacientesRecurrentes>>> GetPacientesMasRecurrentes(int medicoID, int rolID);

        public Task<Response<Pacientes>> CrearPaciente(Pacientes i);

        public Task<Response<Pacientes>> EditarPaciente(Pacientes i);

        public Task<Response<Pacientes>> EliminarPaciente(int id);

        public Task<Response<int>> GetTotalPacientes(int medicoID);

        public Task<(int mujeres, int hombres)> ContarGeneroPacientes(int medicoID);

        public Task<Response<List<Pacientes>>> GetPacientesPorNombre(string nombre, string apellido);
        public Task<Response<PacienteNombre>> GetPacientePorID(int id);

    }
}
