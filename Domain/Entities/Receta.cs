using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Receta
    {
        public int ID_Receta { get; set; }
        public int PacienteID { get; set; }
        public int CitaID { get; set; }
        public string Diagnostico { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public List<RecetaMedicamento> Medicamentos { get; set; }
    }

    public class RecetaMedicamento
    {
        public int ID_RecetaMed { get; set; }
        public int RecetaID { get; set; }
        public int MedicamentoID { get; set; }
        public string Dosis { get; set; }
        public string Cantidad { get; set; }
        public string Frecuencia { get; set; }
    }
}
