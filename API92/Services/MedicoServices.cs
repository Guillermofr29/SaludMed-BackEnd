using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API92.Services
{
    public class MedicoServices : IMedicoServices
    {
        private readonly ApplicationDBContext _context;

        public MedicoServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<NombreMedico>>> GetMedicos()
        {
            try
            {
                List<NombreMedico> response = (await _context.Database.GetDbConnection().QueryAsync<NombreMedico>(
                    "spGetALLMedicos",
                    commandType: CommandType.StoredProcedure)).ToList();

                return new Response<List<NombreMedico>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los medicos: " + ex.Message, ex);
            }
        }

        public async Task<Response<DataMedico>> GetMedico(int ID_Cita)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_Cita", ID_Cita, DbType.Int32);

                DataMedico result = (await _context.Database.GetDbConnection().QueryAsync<DataMedico>(
                    "spGetMedico", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<DataMedico>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el médico por la CitaID: " + ex.Message, ex);
            }
        }
    }
}