using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API92.Services
{
    public class CitaServices : ICitaServices
    {
        private readonly ApplicationDBContext _context;

        public CitaServices(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Response<List<Citas>>> GetCitas()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryAsync<Citas>("spGetALLCitas", commandType: CommandType.StoredProcedure);
                return new Response<List<Citas>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message);
            }
        }



        public async Task<Response<Citas>> CrearCitas(Citas i)
        {
            try
            {

                Citas result = (await _context.Database.GetDbConnection().QueryAsync<Citas>("spPostCrearCitas", new{ i.PacienteID, i.MedicoID, i.Fecha, i.Hora, i.Motivo, i.Notas, i.Estatus }, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<Citas>(result);


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message);
            }
        }

        public async Task<Response<Citas>> EditarCitas(Citas i)
        {
            try
            {

                Citas result = (await _context.Database.GetDbConnection().QueryAsync<Citas>("spPutEditarCitas", new {i.ID_Cita, i.PacienteID, i.MedicoID, i.Fecha, i.Hora, i.Motivo, i.Notas, i.Estatus }, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<Citas>(result);

            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public async Task<Response<Citas>> EliminarCitas(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_Cita", id, DbType.Int32);

                await _context.Database.GetDbConnection().ExecuteAsync("spDeleteCitas", parameters, commandType: CommandType.StoredProcedure);
                return new Response<Citas>(new Citas { ID_Cita = id });
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message, ex);
            }
        }


        public async Task<Response<int>> GetTotalCitas()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>("spGetTotalCitas", commandType: CommandType.StoredProcedure);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de citas: " + ex.Message, ex);
            }
        }

        public async Task<Response<int>> GetCitasPendientes()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>("spGetCitasPendientes", commandType: CommandType.StoredProcedure);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las citas pendientes: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<UltimasCita>>> GetUltimasCincoCitas()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryAsync<UltimasCita>("spGetUltimasCincoCitas", commandType: CommandType.StoredProcedure);
                return new Response<List<UltimasCita>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las últimas cinco citas: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<MedicamentoMasRecetado>>> GetCincoMedicamentosMasRecetados()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryAsync<MedicamentoMasRecetado>(
                    "spGetCincoMedicamentosMasRecetados",
                    commandType: CommandType.StoredProcedure
                );
                return new Response<List<MedicamentoMasRecetado>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<MotivoConsulta>>> GetCincoMotivosConsultaMasComunes()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryAsync<MotivoConsulta>(
                    "spGetCincoMotivosConsultaMasComunes",
                    commandType: CommandType.StoredProcedure
                );
                return new Response<List<MotivoConsulta>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

    }
}

public class MedicamentoMasRecetado
{
    public string Medicamento { get; set; }
    public int NumeroDeVeces { get; set; }
}

public class MotivoConsulta
{
    public string Motivo { get; set; }
    public int NumeroDeVeces { get; set; }
}