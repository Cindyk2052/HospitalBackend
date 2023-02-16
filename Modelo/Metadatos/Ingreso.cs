using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(IngresoMetadato))]
    public partial class Ingreso
    {
    }

    public class IngresoMetadato
    {
        [Required]
        public System.DateTime fecha { get; set; }
        [Required]
        public int numeroSala { get; set; }
        [Required]
        public int numeroCama { get; set; }
        [Required]
        public string diagnostico { get; set; }
        [Required]
        public long medicoId { get; set; }
        [Required]
        public long pacienteId { get; set; }
    }
}
