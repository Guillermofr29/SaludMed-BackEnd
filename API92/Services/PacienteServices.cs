using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace API92.Services
{
    public class PacienteServices : IPacienteServices
    {
        private readonly ApplicationDBContext _context;

        public PacienteServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Pacientes>>> GetPacientes(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                List<Pacientes> response = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>(
                    "spGetAllPacientes",
                    parameters,
                    commandType: CommandType.StoredProcedure)).ToList();

                return new Response<List<Pacientes>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pacientes: " + ex.Message, ex);
            }
        }

        public async Task<Response<Pacientes>> CrearPaciente(Pacientes i)
        {
            try
            {
                Pacientes result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spPostCrearPaciente", new { i.Nombre, i.Apellido, i.Sexo, i.Edad, i.Peso, i.Estatura, i.Telefono, i.Domicilio, i.Correo, i.MedicoID }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return new Response<Pacientes>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<Pacientes>> EditarPaciente(Pacientes i)
        {
            try
            {
                Pacientes result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spPutEditarPaciente", new { i.ID_Paciente, i.Nombre, i.Apellido, i.Sexo, i.Edad, i.Peso, i.Estatura, i.Telefono, i.Domicilio, i.Correo, i.MedicoID }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return new Response<Pacientes>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<Pacientes>> EliminarPaciente(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_Paciente", id, DbType.Int32);

                Pacientes result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spDeletePaciente", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return new Response<Pacientes>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<int>> GetTotalPacientes(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                int totalPacientes = (await _context.Database.GetDbConnection().QueryAsync<int>("spGetTotalPacientes", parameters,commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return new Response<int>(totalPacientes);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<(int mujeres, int hombres)> ContarGeneroPacientes(int medicoID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID };
                var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<(int, int)>("spGetContarGeneroPacientes", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar el género de los pacientes: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<Pacientes>>> GetPacientesPorNombre(string nombre, string apellido)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Nombre", nombre, DbType.String);
                parameters.Add("Apellido", apellido, DbType.String);

                List<Pacientes> result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spGetPacientesPorNombre", parameters, commandType: CommandType.StoredProcedure)).ToList();
                return new Response<List<Pacientes>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pacientes por nombre: " + ex.Message, ex);
            }
        }

        public async Task<Response<PacienteNombre>> GetPacientePorID(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_Paciente", id, DbType.Int32);

                PacienteNombre result = (await _context.Database.GetDbConnection().QueryAsync<PacienteNombre>(
                    "spGetPacientePorID", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<PacienteNombre>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el paciente por ID: " + ex.Message, ex);
            }
        }

        public async Task<Response<List<PacientesRecurrentes>>> GetPacientesMasRecurrentes(int medicoID, int rolID)
        {
            try
            {
                var parameters = new { MedicoID = medicoID, RolID = rolID };
                var result = await _context.Database.GetDbConnection().QueryAsync<PacientesRecurrentes>(
                    "spGetPacientesMasRecurrentes", parameters,
                    commandType: CommandType.StoredProcedure
                );
                return new Response<List<PacientesRecurrentes>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

    }
}

public class PacienteNombre
{
    public int ID_Paciente { get; set; }

    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Sexo { get; set; }
    public int Edad { get; set; }

    public decimal Peso { get; set; }

    public decimal Estatura { get; set; }
    public string Telefono { get; set; }
    public string Domicilio { get; set; }
    public string Correo { get; set; }

    public string NombreMedico { get; set; }

}


public class PacientesRecurrentes
{
    public int ID_Paciente { get; set; }

    public string NombrePaciente { get; set; }
    public int NumeroDeCitas { get; set; }

}