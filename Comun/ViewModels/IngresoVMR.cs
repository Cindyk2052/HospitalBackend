using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class IngresoVMR
    {
        public long id { get; set; }
        public System.DateTime fecha { get; set; }
        public int numeroSala { get; set; }
        public int numeroCama { get; set; }
        public string diagnostico { get; set; }
        public string observacion { get; set; }
        public long medicoId { get; set; }
        public long pacienteId { get; set; }
        public MedicoVMR medico { get; set; }
        public PacienteVMR paciente { get; set; }
    }
}
