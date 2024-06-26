using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public class Medicos
    {

        [Key]
        public int ID_Medico { get; set; }

        public int ClinicaID { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Especialidad { get; set; }

        public string CedulaProfesional { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

        public string Contraseña { get; set; }

    }
}



