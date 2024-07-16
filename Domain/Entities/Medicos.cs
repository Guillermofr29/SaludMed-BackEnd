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

        public int RolID { get; set; }

    }

    public class NombreMedico 
    {
        [Key]
        public int ID_Medico { get; set; }
        public string? Nombre { get; set; }

        public string Apellido { get; set; }

        public int RolID { get; set; }
    }

    public class DataMedico
    {

        [Key]
        public int ID_Medico { get; set; }

        public string NombreMedico { get; set; }

        public string Especialidad { get; set; }

        public string CedulaProfesional { get; set; }

        public string NombreClinica { get; set; }
        public string Direccion { get; set; }

        public string TelefonoClinica { get; set; }

        public string CorreoClinica { get; set; }
    }
}



