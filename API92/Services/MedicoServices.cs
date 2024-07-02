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

        public async Task<Response<List<Medicos>>> GetMedicos(int RolID)
        {
            try
            {
                var parameters = new { RolID = RolID };
                List<Medicos> response = (await _context.Database.GetDbConnection().QueryAsync<Medicos>(
                    "spGetALLMedicos",
                    parameters,
                    commandType: CommandType.StoredProcedure)).ToList();

                return new Response<List<Medicos>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los medicos: " + ex.Message, ex);
            }
        }
    }
}