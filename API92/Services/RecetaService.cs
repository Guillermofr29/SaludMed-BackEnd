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
using Microsoft.Data.SqlClient;

namespace API92.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly string _connectionString;

        public RecetaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearRecetaConMedicamentos(Receta receta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("spPostCrearRecetaConMedicamentos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PacienteID", receta.PacienteID);
                    command.Parameters.AddWithValue("@CitaID", receta.CitaID);
                    command.Parameters.AddWithValue("@Diagnostico", receta.Diagnostico);
                    command.Parameters.AddWithValue("@FechaInicio", receta.FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", receta.FechaFin);
                    command.Parameters.AddWithValue("@Recomendaciones", receta.Recomendaciones);

                    var medicamentosParam = new SqlParameter("@Medicamentos", SqlDbType.Structured);
                    medicamentosParam.TypeName = "dbo.MedicamentoTableType";
                    medicamentosParam.Value = CreateMedicamentosDataTable(receta.Medicamentos);
                    command.Parameters.Add(medicamentosParam);

                    var idRecetaParam = new SqlParameter("@ID_Receta", SqlDbType.Int);
                    idRecetaParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(idRecetaParam);

                    await command.ExecuteNonQueryAsync();

                    return (int)idRecetaParam.Value;
                }
            }
        }

        private DataTable CreateMedicamentosDataTable(List<RecetaMedicamento> medicamentos)
        {
            var dt = new DataTable();
            dt.Columns.Add("MedicamentoID", typeof(int));
            dt.Columns.Add("Dosis", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Frecuencia", typeof(string));
            dt.Columns.Add("PacienteID", typeof(int));

            foreach (var med in medicamentos)
            {
                dt.Rows.Add(med.MedicamentoID, med.Dosis, med.Cantidad, med.Frecuencia);
            }

            return dt;
        }

        public async Task<Response<List<Receta>>> GetRecetas(int medicoID, int rolID)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new { MedicoID = medicoID, Rol = rolID };
                    var result = await connection.QueryAsync<Receta>(
                        "ObtenerRecetasPorMedicoYRol",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return new Response<List<Receta>>(result.ToList());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message, ex);
            }
        }

        public async Task<Response<Receta>> EliminarReceta(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@ID_Receta", id, DbType.Int32);

                    var result = await connection.QueryFirstOrDefaultAsync<Receta>(
                        "spDeleteReceta",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return new Response<Receta>(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message, ex);
            }
        }

    }
}