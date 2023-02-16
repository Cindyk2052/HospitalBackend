using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class PacienteBLL
    {
        public static ListadoPaginadoVMR<PacienteVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return PacienteDAL.LeerTodo(cantidad, pagina, textoBusqueda);

        }

        public static PacienteVMR LeerUno(long id)
        {
            return PacienteDAL.LeerUno(id);

        }

        public static long Crear(Paciente item)
        {
            return PacienteDAL.Crear(item);
        }

        public static void Actualizar(PacienteVMR item)
        {
            PacienteDAL.Actualizar(item);

        }

        public static void Eliminar(List<long> ids)
        {
            PacienteDAL.Eliminar(ids);

        }
    }
}
