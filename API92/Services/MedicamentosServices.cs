using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API92.Services
{
    public class MedicamentosServices: IMedicamentosServices
    {
        private readonly ApplicationDBContext _context;

        public MedicamentosServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Medicamentos>>> GetMedicamentos()
        {
            try
            {
                List<Medicamentos> response = (await _context.Database.GetDbConnection().QueryAsync<Medicamentos>(
                    "spGetALLMedicamentos",
                    commandType: CommandType.StoredProcedure)).ToList();

                return new Response<List<Medicamentos>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los medicamentos: " + ex.Message, ex);
            }
        }
    }
}
