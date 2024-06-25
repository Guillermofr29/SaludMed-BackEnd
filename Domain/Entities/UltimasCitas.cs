using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class UltimasCita
    {
        public int ID_Cita { get; set; }

        public string FechaHora { get; set; }

        public string NombrePaciente { get; set; }
    }
}