using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pacientes
    {
        [Key]
        public int ID_Paciente { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad {  get; set; }

        public decimal Peso { get; set; }

        public decimal Estatura { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
        public string Correo { get; set; }

    }
}
