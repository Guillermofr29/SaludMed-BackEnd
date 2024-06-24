using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API92.Services
{
    public interface IPacienteServices
    {
        public Task<Response<List<Pacientes>>> GetPacientes();

        public Task<Response<Pacientes>> CrearPaciente(Pacientes i);

        public Task<Response<Pacientes>> EditarPaciente(Pacientes i);

        public Task<Response<Pacientes>> EliminarPaciente(int id);

        public Task<Response<int>> GetTotalPacientes();

        public Task<(int mujeres, int hombres)> ContarGeneroPacientes();

        public Task<Response<List<Pacientes>>> GetPacientesPorNombre(string nombre, string apellido);
        public Task<Response<Pacientes>> GetPacientePorID(int id); // Nuevo método para obtener un paciente por ID

    }
}
