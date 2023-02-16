using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class PacienteVMR
    {
        public long id { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
    }
}
