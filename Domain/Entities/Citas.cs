using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Citas
    {
        [Key]
        public int ID_Cita { get; set; }

        public int PacienteID { get; set; }

        public int MedicoID { get; set; }

        public DateTime FechaHora { get; set; }

        public string Motivo { get; set; }

        public string Notas { get; set; }

        public string Estatus { get; set; }
    }
}
