using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API92.Services
{
    public class MotivoCitaServices : IMotivoCitaServices
    {
        private readonly ApplicationDBContext _context;

        public MotivoCitaServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<MotivoCita>>> GetMotivoByName(string motivo)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("MotivoDescripcion", motivo, DbType.String);

                List<MotivoCita> result = (await _context.Database.GetDbConnection().QueryAsync<MotivoCita>("spGetMotivoByName", parameters, commandType: CommandType.StoredProcedure)).ToList();
                return new Response<List<MotivoCita>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pacientes por nombre: " + ex.Message, ex);
            }
        }
    }
}
