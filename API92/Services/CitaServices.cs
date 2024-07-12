using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace API92.Services
{
    public class CitaServices : ICitaServices
    {
        private readonly ApplicationDBContext _context;

        public CitaServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<AllCitas>>> GetCitas(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                var result = await _context.Database.GetDbConnection().QueryAsync<AllCitas>("spGetALLCitas", parameters, commandType: CommandType.StoredProcedure);
                return new Response<List<AllCitas>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message);
            }

        }

        public async Task<Response<List<AllCitas>>> GetCitasPorIdPaciente(int pacienteID)
        {
            try
            {
                var parameters = new { PacienteID = pacienteID };
                var result = await _context.Database.GetDbConnection().QueryAsync<AllCitas>("spGetCitaPorIDPaciente", parameters, commandType: CommandType.StoredProcedure);
                return new Response<List<AllCitas>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message);
            }
        }

        public async Task<Response<TraerCitaID>> GetCitaPorID(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_Cita", id, DbType.Int32);

                TraerCitaID result = (await _context.Database.GetDbConnection().QueryAsync<TraerCitaID>(
                    "spGetCitaPorID", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<TraerCitaID>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el paciente por ID: " + ex.Message, ex);
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

                Citas result = (await _context.Database.GetDbConnection().QueryAsync<Citas>("spPutEditarCitas", new {i.ID_Cita, i.MedicoID, i.Fecha, i.Hora, i.Motivo, i.Notas, i.Estatus }, commandType: CommandType.StoredProcedure)).FirstOrDefault();

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


        public async Task<Response<int>> GetTotalCitas(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>("spGetTotalCitas", parameters, commandType: CommandType.StoredProcedure);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de citas: " + ex.Message, ex);
            }
        }

        public async Task<Response<int>> GetCitasPendientes(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>("spGetCitasPendientes", parameters, commandType: CommandType.StoredProcedure);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las citas pendientes: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<UltimasCita>>> GetUltimasCincoCitas(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                var result = await _context.Database.GetDbConnection().QueryAsync<UltimasCita>("spGetUltimasCincoCitas", parameters, commandType: CommandType.StoredProcedure);
                return new Response<List<UltimasCita>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las últimas cinco citas: " + ex.Message, ex);
            }
        }

        //public async Task<Response<List<MedicamentoMasRecetado>>> GetCincoMedicamentosMasRecetados()
        //{
        //    try
        //    {
        //        var result = await _context.Database.GetDbConnection().QueryAsync<MedicamentoMasRecetado>(
        //            "spGetCincoMedicamentosMasRecetados",
        //            commandType: CommandType.StoredProcedure
        //        );
        //        return new Response<List<MedicamentoMasRecetado>>(result.ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Sucedio un error: " + ex.Message, ex);
        //    }
        //}

        public async Task<Response<List<MotivoConsulta>>> GetCincoMotivosConsultaMasComunes(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                var result = await _context.Database.GetDbConnection().QueryAsync<MotivoConsulta>(
                    "spGetCincoMotivosConsultaMasComunes", parameters,
                    commandType: CommandType.StoredProcedure
                );
                return new Response<List<MotivoConsulta>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<ProximasCitas>>> GetProximasCitas(int medicoID, int rolID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID, RolID = rolID};
                var result = await _context.Database.GetDbConnection().QueryAsync<ProximasCitas>(
                    "spGetProximasCitas",parameters,
                    commandType: CommandType.StoredProcedure
                );
                return new Response<List<ProximasCitas>>(result.ToList());
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

public class AllCitas
{
    [Key]
    public int ID_Cita { get; set; }

    public int PacienteID { get; set; }

    public int MedicoID { get; set; }
    public string Telefono { get; set; }

    public string Correo { get; set; }

    public string NombrePaciente { get; set; }

    public string NombreMedico { get; set; }

    public string Fecha { get; set; }

    public string Hora { get; set; }

    public string Motivo { get; set; }

    public string Notas { get; set; }

    public string Estatus { get; set; }
}

public class ProximasCitas
{
    [Key]
    public int ID_Cita { get; set; }
    public string NombrePaciente { get; set; }

    public string Fecha { get; set; }

    public string Hora { get; set; }

    public string Estatus { get; set; }
}

public class EditarCita
{
    [Key]
    public int ID_Cita { get; set; }

    public string Fecha { get; set; }

    public string Hora { get; set; }

    public int MotivoID { get; set; }

    public string Notas { get; set; }

    public string Estatus { get; set; }
}

public class TraerCitaID
{
    [Key]
    public int ID_Cita { get; set; }

    public int MedicoID { get; set; }

    public int PacienteID { get; set; }
    public string NombrePaciente { get; set; }

    public string NombreMedico { get; set; }

    public string Telefono { get; set; }

    public string Correo { get; set; }

    public string Fecha { get; set; }

    public string Hora { get; set; }

    public string Motivo { get; set; }

    public string Notas { get; set; }

    public string Estatus { get; set; }
}