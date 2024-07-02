using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MotivoCita
    {
        [Key]
        public int ID_Motivo { get; set; }

        public string MotivoDescripcion { get; set; }

    }
}
