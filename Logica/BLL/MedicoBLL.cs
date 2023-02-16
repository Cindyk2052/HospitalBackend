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
    public class MedicoBLL
    {
        public static ListadoPaginadoVMR<MedicoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return MedicoDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }

        public static MedicoVMR LeerUno(long id)
        {
            return MedicoDAL.LeerUno(id);
        }

        public static long Crear(Medico item)
        {
            return MedicoDAL.Crear(item);

        }

        public static void Actualizar(MedicoVMR item)
        {
            MedicoDAL.Actualizar(item);

        }

        public static void Eliminar(List<long> ids)
        {
            MedicoDAL.Eliminar(ids);

        }
    }
}
