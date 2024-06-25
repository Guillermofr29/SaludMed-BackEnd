using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Citas
    {
        [Key]
        public int ID_Cita { get; set; }

        public int PacienteID { get; set; }

        public int MedicoID { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }

        public string Motivo { get; set; }

        public string Notas { get; set; }

        public string Estatus { get; set; }
    }
}
