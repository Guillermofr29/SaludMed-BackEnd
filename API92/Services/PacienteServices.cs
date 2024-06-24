using API92.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace API92.Services
{
    public class PacienteServices : IPacienteServices
    {
        private readonly ApplicationDBContext _context;

        public PacienteServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Pacientes>>> GetPacientes()
        {
            try
            {
                List<Pacientes> response = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spGetAllPacientes", commandType: CommandType.StoredProcedure)).ToList();
                return new Response<List<Pacientes>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<Pacientes>> CrearPaciente(Pacientes i)
        {
            try
            {
                Pacientes result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spPostCrearPaciente", new { i.Nombre, i.Apellido, i.Sexo, i.Edad, i.Peso, i.Estatura, i.Telefono, i.Domicilio, i.Correo }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
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
                Pacientes result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>("spPutEditarPaciente", new { i.ID_Paciente, i.Nombre, i.Apellido, i.Sexo, i.Edad, i.Peso, i.Estatura, i.Telefono, i.Domicilio, i.Correo }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
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

        public async Task<Response<int>> GetTotalPacientes()
        {
            try
            {
                int totalPacientes = (await _context.Database.GetDbConnection().QueryAsync<int>("spGetTotalPacientes", commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return new Response<int>(totalPacientes);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error: " + ex.Message, ex);
            }
        }

        public async Task<(int mujeres, int hombres)> ContarGeneroPacientes()
        {
            try
            {
                var result = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<(int, int)>("spGetContarGeneroPacientes", commandType: CommandType.StoredProcedure);
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

        public async Task<Response<Pacientes>> GetPacientePorID(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_Paciente", id, DbType.Int32);

                Pacientes result = (await _context.Database.GetDbConnection().QueryAsync<Pacientes>(
                    "spGetPacientePorID", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<Pacientes>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el paciente por ID: " + ex.Message, ex);
            }
        }

    }
}
