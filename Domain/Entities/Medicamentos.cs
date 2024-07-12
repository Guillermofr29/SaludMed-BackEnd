using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medicamentos
    {
        [Key]
        public int ID_Medicamento { get; set; }

        public string Nombre { get; set; }

        public string Forma { get; set; }

        public string EfectosSecundarios { get; set; }

    }
}
